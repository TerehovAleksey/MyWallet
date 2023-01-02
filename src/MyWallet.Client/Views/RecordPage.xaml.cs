namespace MyWallet.Client.Views;

public partial class RecordPage : PageBase
{
    public RecordPage(RecordPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}