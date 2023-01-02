using System.Collections.Specialized;

namespace MyWallet.Client.ViewModels.Widget;

public class WidgetContainerViewModel
{
    private readonly ISettingsService _settingsService;
    private readonly IDataService _dataService;

    //все записи
    private IEnumerable<Record> _records = new List<Record>();

    public ObservableCollection<IWidgetViewModel> Widgets { get; }

    //текущие счета
    public ObservableCollection<object>? CurrentAccounts { get; set; }

    //записи в выбранных счетах
    public ObservableCollection<Record> AccountsRecords { get; set; } = new();

    public event Action<IEnumerable<Record>>? RecordsUpdated;

    public WidgetContainerViewModel(ISettingsService settingsService, IDataService dataService)
    {
        _settingsService = settingsService;
        _dataService = dataService;

        Widgets = new ObservableCollection<IWidgetViewModel>();
    }

    public async Task LoadingWidgetsAsync()
    {
        Widgets.Clear();

        //при изменении счетов должен отрабатывать фильтр
        if (CurrentAccounts is not null)
        {
            CurrentAccounts.CollectionChanged += CurrentAccounts_CollectionChanged;
        }

        //получаем все записи
        _records = await _dataService.Record.GetRecordsAsync();

        //получаем настройки и создаём виджеты
        var widgetSettings = await _settingsService.GetMainWidgetsAsync();
        foreach (var setting in widgetSettings)
        {
            var widget = WidgetFabric.CreateWidget(setting);
            RecordsUpdated += widget.SetRecords;
            Widgets.Add(widget);
        }

        // синхронная инициализация вижджета
        foreach (var widget in Widgets)
        {
            widget.Init();
        }

        // асинхронная инициализация вижджета
        foreach (var widget in Widgets)
        {
            await widget.LoadingAsync();
        }
    }

    private void CurrentAccounts_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Filter();
    }

    // выбираем записи для текущих счетов
    private void Filter()
    {
        if (CurrentAccounts is not null)
        {
            var accountNames = CurrentAccounts.Select(a => ((Account)a).Name);
            var records = _records.Where(r => accountNames.Contains(r.Account));
            AccountsRecords.AddRange(records, true);
            RecordsUpdated?.Invoke(AccountsRecords);
        }
    }
}
