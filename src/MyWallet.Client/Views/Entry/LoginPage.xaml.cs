namespace MyWallet.Client.Views.Entry;

public partial class LoginPage : PageBase
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}