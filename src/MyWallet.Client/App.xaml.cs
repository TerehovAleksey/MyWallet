namespace MyWallet.Client;

public partial class App : Application
{
    public App(IBarrel cache)
    {
        InitializeComponent();

        //cache.EmptyAll();
#if DEBUG
        //очистка кеша, если изменился источник данных
        var source = cache.Get<string>("ApiServiceUrl");
        if (string.IsNullOrEmpty(source) || source != Constants.ApiServiceUrl)
        {
            cache.EmptyAll();
        }
        cache.Add("ApiServiceUrl", Constants.ApiServiceUrl, TimeSpan.MaxValue);
#endif

        MainPage = new AppShell();
    }
}