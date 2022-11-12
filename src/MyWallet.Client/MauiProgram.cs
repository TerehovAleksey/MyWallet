using MyWallet.Client.DataServices;
using MyWallet.Client.Pages;
using MyWallet.Client.ViewModels;
using CommunityToolkit.Maui;

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
            }).UseMauiCommunityToolkit();

        builder.Services.AddHttpClient<IDataService, RestDataService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<RecordPage>();
        builder.Services.AddTransient<CategoryPage>();
        builder.Services.AddTransient<CategoryEditPage>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<RecordPageViewModel>();
        builder.Services.AddTransient<CategoryEditViewModel>();

        return builder.Build();
    }
}