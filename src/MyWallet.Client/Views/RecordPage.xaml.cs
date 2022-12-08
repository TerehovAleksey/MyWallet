namespace MyWallet.Client.Views;

public partial class RecordPage : PageBase
{
    public RecordPage(RecordPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}