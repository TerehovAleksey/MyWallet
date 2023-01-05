namespace MyWallet.Client.ViewModels.Widget;

public partial class WidgetCashFlowViewModel : BaseWidgetViewModel
{
    public event Action<string, decimal> OnProgressChanged;

    private readonly IDataService _dataService;

    [ObservableProperty]
    private string _currencySymbol = string.Empty;

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

    public string TotalString => $"{Total} {_currencySymbol}";
    public string IncomeString => $"{Income} {_currencySymbol}";
    public string ExpensesString => $"-{Expenses} {_currencySymbol}";

    public WidgetCashFlowViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
        Title = Strings.CashFlow;
    }

    public override async Task LoadingAsync()
    {
        CurrencySymbol = await _dataService.Currency.GetMainCurrencySymbol();
    }

    protected override void OnRecordsSet()
    {
        Income = FilteredRecords.Where(x => x.IsIncome).Sum(x => x.Value);
        Expenses = FilteredRecords.Where(x => !x.IsIncome).Sum(x => x.Value);
        Total = Income - Expenses;

        if (Income > Expenses)
        {
            IncomeProgress = 1;
            ExpensesProgress = Expenses / Income;
        }
        else if (Income < Expenses)
        {
            ExpensesProgress = 1;
            IncomeProgress = Income / Expenses;
        }
        else if (Income == 0 && Expenses == 0)
        {
            ExpensesProgress = 0;
            IncomeProgress = 0;
        }
        else
        {
            ExpensesProgress = 1;
            IncomeProgress = 1;
        }
        OnPropertyChanged(nameof(TotalString));
        OnPropertyChanged(nameof(IncomeString));
        OnPropertyChanged(nameof(ExpensesString));
        OnProgressChanged?.Invoke("Income", IncomeProgress);
        OnProgressChanged?.Invoke("Expenses", ExpensesProgress);
    }
}