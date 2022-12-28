namespace MyWallet.Client;

public partial class EntryShell : Shell
{
	public EntryShell()
	{
        RegisterRouting();
        InitializeComponent();
        SetRootPage();
    }

    private static void RegisterRouting()
    {
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }

    private void SetRootPage()
    {
        var isFirstLaunch = VersionTracking.Default.IsFirstLaunchEver;
        if (isFirstLaunch)
        {
            Routing.RegisterRoute(nameof(EntryPage), typeof(EntryPage));

            AppShellContent.ContentTemplate = new DataTemplate(typeof(StartPage));
            AppShellContent.Route = nameof(StartPage);
        }
        else
        {
            AppShellContent.ContentTemplate = new DataTemplate(typeof(EntryPage));
            AppShellContent.Route = nameof(EntryPage);
        }
    }
}