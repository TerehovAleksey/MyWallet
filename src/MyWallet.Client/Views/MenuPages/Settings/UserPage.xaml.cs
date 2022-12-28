namespace MyWallet.Client.Views.MenuPages.Settings;

public partial class UserPage : PageBase
{
	public UserPage(UserPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}