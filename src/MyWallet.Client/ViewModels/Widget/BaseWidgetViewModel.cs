using System.Diagnostics;

namespace MyWallet.Client.ViewModels.Widget;

public abstract class BaseWidgetViewModel : ObservableObject, IWidgetViewModel
{
    private readonly IDialogService _dialogService;

    public string Title { get; set; } = string.Empty;

    public BaseWidgetViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public virtual Task LoadingAsync() => Task.CompletedTask;

    public async Task OpenFilterAsync()
    {
        var period = Enum.GetName(TimePeriod.Days7);
        Debug.Write($"--->Hash 1: {period?.GetHashCode()}");
        var parameters = new DialogParameters
        {
            { "TimePeriod", period }
        };
        var result = await _dialogService.ShowDialogAsync<WidgetSettingsDialog>(new DialogSettings("Настройки виджета"), parameters);
        if (result)
        {
            // сохраняем состояние виджета
            var p = period;
        }
    }
}
