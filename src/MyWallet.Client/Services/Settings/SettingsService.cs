namespace MyWallet.Client.Services.Settings;

public class SettingsService : ISettingsService
{
    public const string KEY = "main_widgets"; 

    private readonly IStorageService _storageService;

    public SettingsService(IStorageService storageService)
    {
        _storageService= storageService;
    }


    public async Task<IEnumerable<WidgetSettings>> GetMainWidgetsAsync()
    {
        var widgets = await _storageService.LoadFromCache<List<WidgetSettings>>(KEY);
        if (widgets is not null)
        {
            return widgets;
        }

        //TODO: пока только в кэше
        widgets = new List<WidgetSettings>
        {
            new(Guid.NewGuid(), WidgetType.CashFlow, new WidgetFilter()),
            new(Guid.NewGuid(), WidgetType.LastRecords, new WidgetFilter())
        };

        await _storageService.SaveToCache(KEY, widgets, TimeSpan.MaxValue);
        return widgets;
    }

    public async Task UpdateWidgetFilter(Guid widgetId, WidgetFilter filter)
    {
        var widgets = (await GetMainWidgetsAsync()).ToArray();
        var index = Array.FindIndex(widgets, x=>x.Id == widgetId);
        var widget = widgets[index];
        widgets[index] = widget with { Filter = filter };
        await _storageService.SaveToCache(KEY, widgets.ToList(), TimeSpan.MaxValue);
    }

    public async Task DeleteWidget(Guid widgetId)
    {
        var widgets = await GetMainWidgetsAsync();
        widgets = widgets.Where(x => x.Id != widgetId);

        await _storageService.SaveToCache(KEY, widgets.ToList(), TimeSpan.MaxValue);
    }
}