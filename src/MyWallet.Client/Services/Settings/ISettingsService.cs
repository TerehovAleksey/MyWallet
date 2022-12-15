namespace MyWallet.Client.Services.Settings;

public interface ISettingsService
{
    public Task<IEnumerable<WidgetSettings>> GetMainWidgetsAsync();
}