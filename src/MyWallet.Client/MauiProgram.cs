using MonkeyCache;
using MonkeyCache.FileStore;

namespace MyWallet.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCompatibility()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FiraSans-Light.ttf", "RegularFont");
                fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
                fonts.AddFont("FiraSans-Regular.ttf", "RegularFont");
            })
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
            events.AddAndroid(android => android.OnCreate((activity, bundle) => 
            {
                activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);
                activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            }));
#endif
            })
            .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        //Register Services
        RegisterAppServices(builder.Services);

        return builder.Build();
    }

    private static void RegisterAppServices(IServiceCollection services)
    {
        //Add Platform specific Dependencies
        services.AddSingleton<IConnectivity>(Connectivity.Current);

        //Register Cache Barrel
        Barrel.ApplicationId = Constants.ApplicationId;
        services.AddSingleton<IBarrel>(Barrel.Current);

        //Register API Service
        services.AddHttpClient<IDataService, RestDataService>();

        //Register View Models
        services.AddSingleton<MainPageViewModel>();
        services.AddTransient<AccountPageViewModel>();
        services.AddTransient<AccountsPageViewModel>();
    }

}