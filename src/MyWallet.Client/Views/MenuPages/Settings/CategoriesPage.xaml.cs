namespace MyWallet.Client.Views.MenuPages.Settings;

public partial class CategoriesPage : PageBase
{
	public CategoriesPage(CategoriesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}