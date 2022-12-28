namespace MyWallet.Client.Services.Settings;

public class SettingsService : ISettingsService
{
    public async Task<IEnumerable<WidgetSettings>> GetMainWidgetsAsync()
    {
        var settings = new List<WidgetSettings>
        {
            new(Widgets.CashFlow),
            new(Widgets.LastRecords)
            
        };

        return await Task.FromResult(settings);
    }
}