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
        var records = await _dataService.Record.GetRecordsAsync();
        Records.AddRange(records.Take(10), true);
    }
}
