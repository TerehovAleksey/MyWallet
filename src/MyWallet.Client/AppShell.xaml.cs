using MyWallet.Client.Pages;

namespace MyWallet.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
        Routing.RegisterRoute(nameof(CategoryEditPage), typeof(CategoryEditPage));
    }
}