namespace MyWallet.Client.Views;

public partial class AccountPage : PageBase
{
	public AccountPage(AccountPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}