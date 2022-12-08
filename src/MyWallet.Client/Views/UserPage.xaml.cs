namespace MyWallet.Client.Views;

public partial class UserPage : PageBase
{
	public UserPage(UserPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}