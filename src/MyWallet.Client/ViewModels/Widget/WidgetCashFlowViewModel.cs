namespace MyWallet.Client.ViewModels.Widget;

public partial class WidgetCashFlowViewModel : BaseWidgetViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private decimal _total;
    
    [ObservableProperty]
    private decimal _income;

    [ObservableProperty]
    private decimal _incomeProgress;

    [ObservableProperty]
    private decimal _expenses;

    [ObservableProperty]
    private decimal _expensesProgress;

    public WidgetCashFlowViewModel(IDialogService dialogService, IDataService dataService) : base(dialogService)
    {
        _dataService = dataService;
    }

    public override async Task LoadingAsync()
    {
        var records = await _dataService.Record.GetRecordsAsync();
        Income = records.Where(x => x.IsIncome).Sum(x => x.Value);
        Expenses = records.Where(x => !x.IsIncome).Sum(x => x.Value);
        Total = Income - Expenses;

        if (Income > Expenses)
        {
            IncomeProgress = 1;
            ExpensesProgress = Expenses / Income;
        }
        else
        {
            ExpensesProgress = 1;
            IncomeProgress = Income / Expenses;
        }
    }
}