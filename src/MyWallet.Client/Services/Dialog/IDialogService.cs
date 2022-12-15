namespace MyWallet.Client.Services.Dialog;

public interface IDialogService
{
    public Task ShowAlertAsync(string title, string message, string buttonLabel);
    public Task<bool> ShowDialogAsync<T>(DialogSettings settings, DialogParameters? parameters = null) where T : View, IDialog;
    public void Close(bool result = false);
}