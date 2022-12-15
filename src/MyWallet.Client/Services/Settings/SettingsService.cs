namespace MyWallet.Client.Services.Settings;

public class SettingsService : ISettingsService
{
    public async Task<IEnumerable<WidgetSettings>> GetMainWidgetsAsync()
    {
        var settings = new List<WidgetSettings>
        {
            new(nameof(WidgetLastRecords))
        };

        return await Task.FromResult(settings);
    }
}