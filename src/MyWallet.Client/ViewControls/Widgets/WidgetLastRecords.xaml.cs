namespace MyWallet.Client.ViewControls.Widgets;

public partial class WidgetLastRecords : Widget
{
	public WidgetLastRecords(IWidgetViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}