namespace MyWallet.Client.Views.MenuPages;

public partial class SettingsPage : PageBase
{
	public SettingsPage(SettingsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}