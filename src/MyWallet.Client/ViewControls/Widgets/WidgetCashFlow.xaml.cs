namespace MyWallet.Client.ViewControls.Widgets;

public partial class WidgetCashFlow : Widget
{
    private double _incomeWidth;
    private double _expenseWidth;

    private WidgetCashFlowViewModel _viewModel;


    public WidgetCashFlow(IWidgetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        _incomeWidth = IncomeProgress.Width;
        _expenseWidth = ExpenseProgress.Width;

        _viewModel = (WidgetCashFlowViewModel)viewModel;
        _viewModel.OnProgressChanged += ViewModel_OnProgressChanged;
    }

    private void ExpenseContainer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is Border && e.PropertyName == "Width")
        {
            ExpenseProgressAnimate((double)_viewModel.ExpensesProgress);
        }
    }

    private void IncomeContainer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is Border && e.PropertyName == "Width")
        {
            IncomeProgressAnimate((double)_viewModel.IncomeProgress);
        }
    }

    private void ViewModel_OnProgressChanged(string name, decimal value)
    {
        if (name == "Income")
        {
            IncomeProgressAnimate((double)value);
        }
        else
        {
            ExpenseProgressAnimate((double)value);
        }
    }

    private void ExpenseProgressAnimate(double value)
    {
        var animation = new Animation(v => ExpenseProgress.WidthRequest = v, _expenseWidth, ExpenseContainer.Width * value, Easing.CubicOut);
        animation.Commit(this, "WidthExpenseAnimation");
        _expenseWidth = ExpenseContainer.Width * value;
    }

    private void IncomeProgressAnimate(double value)
    {
        var animation = new Animation(v => IncomeProgress.WidthRequest = v, _incomeWidth, IncomeContainer.Width * value, Easing.CubicOut);
        animation.Commit(this, "WidthIncomeAnimation");
        _incomeWidth = IncomeContainer.Width * value;
    }
}