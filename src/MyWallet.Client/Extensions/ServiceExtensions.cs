namespace MyWallet.Client.Extensions;

public static class ServiceExtensions
{
    public static void RegisterAppServices(this IServiceCollection services)
    {
        //Add Platform specific Dependencies
        services.AddSingleton<IConnectivity>(Connectivity.Current);

        //Register Cache Barrel
        Barrel.ApplicationId = Constants.ApplicationId;
        services.AddSingleton<IBarrel>(Barrel.Current);

        //Register API Service
        services.AddHttpClient<IDataService, RestDataService>();
        services.AddHttpClient<IUserService, RestUserService>();

        //Register View Models
        services.AddSingleton<MainPageViewModel>();
        services.AddTransient<AccountPageViewModel>();
        services.AddTransient<AccountsPageViewModel>();
        services.AddTransient<EntryPageViewModel>();
        services.AddTransient<LoginPageViewModel>();
    }
}
