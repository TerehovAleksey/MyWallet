namespace MyWallet.Client.ViewModels.Widget;

public class WidgetLastRecordsViewModel : BaseWidgetViewModel
{
    private readonly IDataService _dataService;

    public ObservableCollection<Record> Records { get; } = new();

    public WidgetLastRecordsViewModel(IDialogService dialogService, IDataService dataService) : base(dialogService)
    {
        _dataService = dataService;
    }

    public override async Task LoadingAsync()
    {
        var records = await _dataService.GetRecordsAsync(new DateTime(2020, 01, 01), new DateTime(2023, 01, 01));
        //TODO: обработка ошибки?
        if (records.Item != null)
        {
            Records.AddRange(records.Item, true);
        }
    }
}
