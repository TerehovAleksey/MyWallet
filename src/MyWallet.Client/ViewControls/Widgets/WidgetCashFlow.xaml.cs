namespace MyWallet.Client.ViewControls.Widgets;

public partial class WidgetCashFlow : Widget
{
    public WidgetCashFlow(IWidgetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}