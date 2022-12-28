using System.Diagnostics;

namespace MyWallet.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        RegisterRouting();
        InitializeComponent();
        //todo: c FlyoutBackdrop Opacity пока проблема
    }

    private static void RegisterRouting()
    {
        Routing.RegisterRoute("account", typeof(AccountPage));
        Routing.RegisterRoute("accounts", typeof(AccountsPage));
        Routing.RegisterRoute("user", typeof(UserPage));
        Routing.RegisterRoute("record", typeof(RecordPage));
        Routing.RegisterRoute("devices", typeof(DevicesPage));
        Routing.RegisterRoute("currencies", typeof(CurrenciesPage));
        Routing.RegisterRoute("currency", typeof(CurrencyPage));
        Routing.RegisterRoute("categories", typeof(CategoriesPage));
        Routing.RegisterRoute("category", typeof(CategoryPage));
        
        Routing.RegisterRoute(nameof(WidgetTabs), typeof(WidgetTabs));
        Routing.RegisterRoute($"{nameof(WidgetTabs)}/{nameof(BalanceWidgetsPage)}", typeof(BalanceWidgetsPage));
    }
}
