namespace MyWallet.Client.ViewControls.Dialog;

public partial class DialogContainer : Grid, IDisposable
{
    private readonly DialogService _dialogService;
    private TaskCompletionSource<bool> _tcs = new(false);

    public DialogContainer()
    {
        _dialogService = (DialogService)ServiceHelpers.GetService<IDialogService>();

        IsVisible = false;
        InitializeComponent();
    }

    private void Dialog_OnClose(bool result)
    {
        IsVisible = false;
        _tcs.SetResult(result);
        _tcs = new(false);
    }

    public void Subscribe()
    {
        _dialogService.OnShow += Dialog_OnShow;
        _dialogService.OnClose += Dialog_OnClose;
    }

    public void Unsubscribe()
    {
        _dialogService.OnShow -= Dialog_OnShow;
        _dialogService.OnClose -= Dialog_OnClose;
    }

    private Task<bool> Dialog_OnShow(View content, DialogSettings settings)
    {
#if WINDOWS
        Dialog.WidthRequest = 450;
#elif ANDROID
        var width = Application.Current!.Windows[0].Width;
        Dialog.WidthRequest = width - 20;
#endif

        DialogContent.Content = content;

        TitleLabel.Text = settings.Title;
        OkButton.Text = settings.OkButtonText;
        CancelButton.IsVisible = settings.IsCancelButtonVisible;
        CancelButton.Text = settings.CancelButtonText;

        IsVisible = true;
        return _tcs.Task;
    }

    private void DialogContainerMask_Tapped(object sender, TappedEventArgs e) => CloseDialog(false);
    private void OkButton_OnClicked(object? sender, EventArgs e) => CloseDialog(true);
    private void ButtonCancel_Clicked(object sender, EventArgs e) => CloseDialog(false);

    private void CloseDialog(bool result)
    {
        _dialogService.Close(result);
        IsVisible = false;
    }

    public void Dispose() => Unsubscribe();
}