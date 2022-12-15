namespace MyWallet.Client.Services.Dialog;

public class DialogService : IDialogService
{
    public event Func<View, DialogSettings, Task<bool>>? OnShow;
    public event Action<bool>? OnClose;

    public Task ShowAlertAsync(string title, string message, string buttonLabel)
    {
        return Application.Current?.MainPage?.DisplayAlert(title, message, buttonLabel) ?? Task.CompletedTask;
    }

    public async Task<bool> ShowDialogAsync<T>(DialogSettings settings, DialogParameters? parameters = null) where T : View, IDialog
    {
        if (OnShow is not null)
        {
            var content = ServiceHelpers.GetService<T>();
            content.Parameters = parameters;
            var result = await OnShow.Invoke(content, settings);
            return result;
        }

        return false;
    }

    public void Close(bool result = false) => OnClose?.Invoke(result);
}