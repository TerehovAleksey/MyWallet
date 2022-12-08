namespace MyWallet.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp() =>
        MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .ConfigureEffects(
                effects =>
                {
                })
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FiraSans-Light.ttf", "RegularFont");
                fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
                fonts.AddFont("FiraSans-Regular.ttf", "RegularFont");
            })
            .UseDebug()
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews()
            .Build();

    private static MauiAppBuilder UseDebug(this MauiAppBuilder builder)
    {
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder;
    }
}