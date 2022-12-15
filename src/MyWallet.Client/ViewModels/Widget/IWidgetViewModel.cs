namespace MyWallet.Client.ViewModels.Widget;

public interface IWidgetViewModel
{
    public string Title { get; }
    public Task LoadingAsync();
    public Task OpenFilterAsync();
}
