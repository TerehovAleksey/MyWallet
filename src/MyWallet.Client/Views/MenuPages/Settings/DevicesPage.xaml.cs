namespace MyWallet.Client.Views.MenuPages.Settings;

public partial class DevicesPage : PageBase
{
	public DevicesPage(DevicesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}