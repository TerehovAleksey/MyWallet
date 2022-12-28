namespace MyWallet.Client.Views.MenuPages.Settings;

public partial class CategoryPage : PageBase
{
    public CategoryPage(CategoryPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}