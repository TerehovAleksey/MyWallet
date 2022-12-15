namespace MyWallet.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		RegisterRouting();	
        InitializeComponent();
        SetRootPage();
    }

	private static void RegisterRouting()
	{
		Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
		Routing.RegisterRoute(nameof(AccountsPage), typeof(AccountsPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
		Routing.RegisterRoute(nameof(RecordPage), typeof(RecordPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		Routing.RegisterRoute(nameof(CurrenciesPage), typeof(CurrenciesPage));

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }

	private void SetRootPage()
	{
		var cache = ServiceHelpers.GetService<IBarrel>();
        var token = cache.Get<string>(Constants.TOKEN_KEY);
		if (string.IsNullOrEmpty(token))
		{
			AppShellContent.ContentTemplate = new DataTemplate(typeof(EntryPage));
			AppShellContent.Route = nameof(EntryPage);
			//Routing.RegisterRoute("main", typeof(MainPage));
		}
		else
		{
			AppShellContent.ContentTemplate = new DataTemplate(typeof(MainPage));
			AppShellContent.Route = nameof(MainPage);
			//Routing.RegisterRoute("entry", typeof(EntryPage));
        }

		OnPropertyChanged(nameof(AppShellContent));
	}
}
