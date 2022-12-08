namespace MyWallet.Client.Views;

public partial class AccountsPage : PageBase
{
	public AccountsPage(AccountsPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}