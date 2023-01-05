namespace MyWallet.Client;

public partial class AppShell : Shell
{
    private readonly IAuthService _authService;
    private readonly IAppService _appService;

    public AppShell()
    {
        _appService = ServiceHelpers.GetService<IAppService>();
        _authService = ServiceHelpers.GetService<IDataService>().Auth;
        RegisterRouting();
        InitializeComponent();
        //todo: c FlyoutBackdrop Opacity пока проблема
    }

    protected override async void OnAppearing()
    {
        try
        {
            var user = await _authService.GetUserDataAsync();
            var userTitle = $"{user.FirstName} {user.LastName}".Trim();
            if (string.IsNullOrEmpty(userTitle))
            {
                userTitle = user.Email;
            }
            FlyoutUserTitle.Text = userTitle;
        }
        catch (UnauthorizedAccessException)
        {
            _appService.SetAppState(false);
        }
        
    }

    private static void RegisterRouting()
    {
        Routing.RegisterRoute("account", typeof(AccountPage));
        Routing.RegisterRoute("accounts", typeof(AccountsPage));
        Routing.RegisterRoute("user", typeof(UserPage));
        Routing.RegisterRoute("record", typeof(RecordPage));
        Routing.RegisterRoute("records", typeof(RecordsPage));
        Routing.RegisterRoute("devices", typeof(DevicesPage));
        Routing.RegisterRoute("currencies", typeof(CurrenciesPage));
        Routing.RegisterRoute("currency", typeof(CurrencyPage));
        Routing.RegisterRoute("categories", typeof(CategoriesPage));
        Routing.RegisterRoute("category", typeof(CategoryPage));
        Routing.RegisterRoute("notifications", typeof(NotificationsPage));
        
        Routing.RegisterRoute(nameof(WidgetTabs), typeof(WidgetTabs));
        Routing.RegisterRoute($"{nameof(WidgetTabs)}/{nameof(BalanceWidgetsPage)}", typeof(BalanceWidgetsPage));
    }
}
