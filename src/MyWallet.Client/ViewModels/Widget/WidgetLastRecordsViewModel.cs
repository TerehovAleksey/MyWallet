namespace MyWallet.Client.ViewModels.Widget;

public class WidgetLastRecordsViewModel : BaseWidgetViewModel
{
    public ObservableCollection<Record> TopRecords { get; } = new();

    public WidgetLastRecordsViewModel(IAppService appService) : base(appService)
    {
        Title = Strings.LastRecordsOverview;
    }

    protected override void OnRecordsSet()
    {
        TopRecords.AddRange(FilteredRecords.Take(10), true);
    }
}
