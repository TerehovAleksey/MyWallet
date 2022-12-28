using CurrenciesPageViewModel = MyWallet.Client.ViewModels.MenuPages.Settings.CurrenciesPageViewModel;

namespace MyWallet.Client.Views.MenuPages.Settings;

public partial class CurrenciesPage : PageBase
{
	public CurrenciesPage(CurrenciesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}