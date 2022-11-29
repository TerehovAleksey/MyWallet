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
            .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.RegisterAppServices();

        return builder.Build();
    }
}