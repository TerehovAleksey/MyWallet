namespace MyWallet.Client.Extensions;

public static class ServiceExtensions
{
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();

        //Register Cache Barrel
        Barrel.ApplicationId = Constants.ApplicationId;
        builder.Services.AddSingleton<IBarrel>(Barrel.Current);

        //Register API Service
        builder.Services.AddHttpClient<IDataService, RestDataService>();
        builder.Services.AddHttpClient<IUserService, RestUserService>();

        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPageViewModel>();
        
        builder.Services.AddTransient<AccountPageViewModel>();
        builder.Services.AddTransient<AccountsPageViewModel>();
        builder.Services.AddTransient<EntryPageViewModel>();
        builder.Services.AddTransient<LoginPageViewModel>();
        builder.Services.AddTransient<UserPageViewModel>();
        builder.Services.AddTransient<RecordPageViewModel>();
        
        return builder;
    }
    
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<AccountsPage>();
        builder.Services.AddTransient<EntryPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<UserPage>();
        builder.Services.AddTransient<RecordPage>();
        
        return builder;
    }
}
