namespace MyWallet.Client.Views;

public partial class LoginPage : PageBase
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}