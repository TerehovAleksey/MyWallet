namespace MyWallet.Client.ViewModels.Widget;

public abstract partial class BaseWidgetViewModel : ObservableObject, IWidgetViewModel
{
    private readonly IAppService _appService;
    private IEnumerable<Record> _records = new List<Record>();

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    public Guid Id { get; set; }

    public virtual WidgetType WidgetType { get; set; } = WidgetType.None;
    public WidgetFilter Filter { get; set; } = new();
    protected ObservableCollection<Record> FilteredRecords { get; } = new();

    protected BaseWidgetViewModel(IAppService appService)
    {
        _appService = appService;
    }

    public virtual void Init()
    {
        Description = Filter.TimePeriod.ToLongDisplayString();
    }

    public virtual Task LoadingAsync() => Task.CompletedTask;

    protected virtual void OnRecordsSet()
    {
    }

    public void SetRecords(IEnumerable<Record> records)
    {
        _records = records;
        var filtered = Select(_records, Filter);
        FilteredRecords.AddRange(filtered, true);
        OnRecordsSet();
    }

    public async Task OpenFilterAsync()
    {
        var period = Enum.GetName(Filter.TimePeriod) ?? string.Empty;
        var settings = new DialogSettings(Strings.CardConfiguration)
        {
            OkButtonText = Strings.Save
        };
        var parameters = new DialogParameters
        {
            { "TimePeriod", period }
        };
        var result = await _appService.Dialog.ShowDialogAsync<WidgetSettingsDialog>(settings, parameters);
        if (result)
        {
            var newPeriod = parameters.Get<string>("TimePeriod");
            var newTimePeriod = Enum.Parse<TimePeriod>(newPeriod);
            Filter = new WidgetFilter(TimePeriod: newTimePeriod);

            await _appService.Settings.UpdateWidgetFilter(Id, Filter);

            SetRecords(_records);
            Init();
        }
    }

    public virtual Task OpenDetailsAsync() => _appService.Dialog.ShowInDevelopmentMessage();

    private static IEnumerable<Record> Select(IEnumerable<Record> records, WidgetFilter filter)
    {
        var now = DateTime.Now;
        DateTime startDate;
        var endDate = DateTime.Now;
        switch (filter.TimePeriod)
        {
            case TimePeriod.Today:
                startDate = now.Date;
                break;
            case TimePeriod.CurrentWeek:
                var dayOffset = DayOfWeek.Monday - now.DayOfWeek;
                startDate = now.AddDays(dayOffset).Date;
                break;
            case TimePeriod.CurrentMonth:
                startDate = new DateTime(now.Year, now.Month, 1);
                break;
            case TimePeriod.CurrentYear:
                startDate = new DateTime(now.Year, 1, 1);
                break;
            case TimePeriod.Days7:
                startDate = now.Date.AddDays(-7);
                break;
            case TimePeriod.Days30:
                startDate = now.Date.AddDays(-30);
                break;
            case TimePeriod.Week12:
                startDate = now.Date.AddDays(-84);
                break;
            case TimePeriod.Month6:
                startDate = now.Date.AddMonths(-6);
                break;
            case TimePeriod.Year1:
                startDate = now.Date.AddYears(-1);
                break;
            case TimePeriod.Year5:
                startDate = now.Date.AddYears(-5);
                break;
            default:
                throw new NotImplementedException($"No implementation for {Enum.GetName(filter.TimePeriod)}");
        }

        return records.Where(r => r.DateOfRecord >= startDate && r.DateOfRecord <= endDate);
    }
}