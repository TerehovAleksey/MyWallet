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
		Routing.RegisterRoute("account", typeof(AccountPage));
		Routing.RegisterRoute("accounts", typeof(AccountsPage));
		Routing.RegisterRoute("login", typeof(LoginPage));
		Routing.RegisterRoute("main", typeof(MainPage));
		Routing.RegisterRoute("user", typeof(UserPage));
		Routing.RegisterRoute("record", typeof(RecordPage));
	}

	private void SetRootPage()
	{
		var cache = ServiceHelpers.GetService<IBarrel>();
        var token = cache.Get<string>(Constants.TOKEN_KEY);
		if (string.IsNullOrEmpty(token))
		{
			AppShellContent.ContentTemplate = new DataTemplate(typeof(EntryPage));
			AppShellContent.Route = "entry";
			Routing.RegisterRoute("main", typeof(MainPage));
		}
		else
		{
			AppShellContent.ContentTemplate = new DataTemplate(typeof(MainPage));
			AppShellContent.Route = "main";
			Routing.RegisterRoute("entry", typeof(EntryPage));
        }

		OnPropertyChanged(nameof(AppShellContent));
	}
}
