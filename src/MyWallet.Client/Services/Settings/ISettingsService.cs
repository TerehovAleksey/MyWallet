namespace MyWallet.Client.Services.Settings;

public interface ISettingsService
{
    public Task<IEnumerable<WidgetSettings>> GetMainWidgetsAsync();
    public Task UpdateWidgetFilter(Guid widgetId, WidgetFilter filter);
    public Task DeleteWidget(Guid widgetId);
}