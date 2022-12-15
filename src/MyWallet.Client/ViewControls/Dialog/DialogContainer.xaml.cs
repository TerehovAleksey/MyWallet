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

        _dialogService.OnShow += Dialog_OnShow;
        _dialogService.OnClose += Dialog_OnClose;
    }

    private void Dialog_OnClose(bool result)
    {
        IsVisible = false;
        _tcs.SetResult(result);
        _tcs = new(false);
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
        CancelButton.Text = settings.CancelButtonText;

        IsVisible = true;
        return _tcs.Task;
    }

    private void DialogContainerMask_Tapped(object sender, TappedEventArgs e) => CloseDialog();
    private void ButtonCancel_Clicked(object sender, EventArgs e) => CloseDialog();
    private void CloseDialog()
    {
        _dialogService.Close();
        IsVisible = false;
    }
    public void Dispose()
    {
        _dialogService.OnShow -= Dialog_OnShow;
        _dialogService.OnClose -= Dialog_OnClose;
    }
}