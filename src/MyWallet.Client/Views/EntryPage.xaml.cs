namespace MyWallet.Client.Views;

public partial class EntryPage : PageBase
{
	public EntryPage(EntryPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}