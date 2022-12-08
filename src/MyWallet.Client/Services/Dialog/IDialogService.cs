namespace MyWallet.Client.Services.Dialog;

public interface IDialogService
{
    public Task ShowAlertAsync(string title, string message, string buttonLabel);
}