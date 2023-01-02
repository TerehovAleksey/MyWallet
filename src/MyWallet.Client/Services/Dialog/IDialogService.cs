namespace MyWallet.Client.Services.Dialog;

public interface IDialogService
{
    public Task ShowMessageAsync(string title, string message);
    public Task<(bool Sucsess, string Value)> ShowInputTextAsync(string title, string placeholder, string value);
    public Task<(bool Sucsess, string Value)> ShowRadioInputAsync(string title, string[] values, string? value);

    public Task ShowInDevelopmentMessage();
    public Task<bool> ShowDialogAsync<T>(DialogSettings settings, DialogParameters? parameters) where T : View, IDialog;
    public void Close(bool result = false);
}