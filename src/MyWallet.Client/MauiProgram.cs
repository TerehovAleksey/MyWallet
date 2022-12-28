﻿namespace MyWallet.Client;

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
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
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

        //use only in DEBUG https://stackoverflow.com/questions/71047509/trust-anchor-for-certification-path-not-found-in-a-net-maui-project-trying-t
#if ANDROID && DEBUG
        Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
        Platforms.Android.DangerousTrustProvider.Register();
#endif

        return builder;
    }
}