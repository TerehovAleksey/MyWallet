namespace MyWallet.Client.Services.Dialog;

public class DialogService : IDialogService
{
    public event Func<View, DialogSettings, Task<bool>>? OnShow;
    public event Action<bool>? OnClose;

    public async Task ShowMessageAsync(string title, string message)
    {
        var settings = new DialogSettings(title)
        {
            IsCancelButtonVisible = false
        };
        var parameters = new DialogParameters()
        {
            { "Message", message }
        };
        await ShowDialogAsync<MessageDialog>(settings, parameters);
    }

    public async Task<(bool Sucsess, string Value)> ShowInputTextAsync(string title, string placeholder, string value)
    {
        var settings = new DialogSettings(title);
        var parameters = new DialogParameters()
        {
            { "Placeholder", placeholder },
            { "Value", value }
        };
        var result = await ShowDialogAsync<InputDialog>(settings, parameters);
        var newValue = parameters["Value"].ToString() ?? string.Empty;
        return (result, newValue);
    }

    public async Task<(bool Sucsess, string Value)> ShowRadioInputAsync(string title, string[] values, string? value)
    {
        var settings = new DialogSettings(title);
        var parameters = new DialogParameters()
        {
            { "Items", values },
        };
        if (!string.IsNullOrEmpty(value))
        {
            parameters.Add("SelectedItem", value);
        }
        var result = await ShowDialogAsync<RadioDialog>(settings, parameters);
        var newValue = parameters["SelectedItem"].ToString() ?? string.Empty;
        return (result, newValue);
    }

    public Task ShowInDevelopmentMessage() => ShowMessageAsync("В разработке", "обязательно появится в ближайших обновлениях!)");

    public void Close(bool result = false) => OnClose?.Invoke(result);

    public Task<bool> ShowDialogAsync<T>(DialogSettings settings, DialogParameters? parameters) where T : View, IDialog
    {
        if (OnShow is not null)
        {
            var content = ServiceHelpers.GetService<T>();
            content.Parameters = parameters;
            return OnShow.Invoke(content, settings);
        }

        return Task.FromResult(false);
    }
}