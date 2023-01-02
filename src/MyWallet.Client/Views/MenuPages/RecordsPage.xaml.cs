namespace MyWallet.Client.Views.MenuPages;

public partial class RecordsPage : PageBase
{
	public RecordsPage(RecordsPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}