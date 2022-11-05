using MyWallet.Client.DataServices;
using MyWallet.Client.Pages;

namespace MyWallet.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddHttpClient<IDataService, RestDataService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CategoryPage>();
        builder.Services.AddTransient<CategoryEditPage>();

        return builder.Build();
    }
}