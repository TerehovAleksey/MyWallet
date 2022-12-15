namespace MyWallet.Client.Views;

public partial class SettingsPage : PageBase
{
	public SettingsPage(SettingsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}