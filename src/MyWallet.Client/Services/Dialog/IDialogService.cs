namespace MyWallet.Client.Services.Dialog;

public interface IDialogService
{
    [Obsolete("Нативный алерт, меняем на наш ShowMessageAsync")]
    public Task ShowAlertAsync(string title, string message, string buttonLabel);
    public Task ShowMessageAsync(string title, string message, string buttonText = "Ok");
    public Task ShowInDevelopmentMessage();
    public Task<bool> ShowDialogAsync<T>(DialogSettings settings, DialogParameters? parameters = null) where T : View, IDialog;
    public void Close(bool result = false);
}