namespace MyWallet.Client.Views.Entry;

public partial class EntryPage : PageBase
{
	public EntryPage(EntryPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}