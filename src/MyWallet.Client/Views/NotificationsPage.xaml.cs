namespace MyWallet.Client.Views;

public partial class NotificationsPage : PageBase
{
    public NotificationsPage(NotificationsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}