using CurrenciesPageViewModel = MyWallet.Client.ViewModels.MenuPages.Settings.CurrenciesPageViewModel;

namespace MyWallet.Client.Extensions;

public static class ServiceExtensions
{
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IAppService, AppService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
        builder.Services.AddSingleton<IStorageService, StorageService>();
        
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        //Register Cache Barrel
        Barrel.ApplicationId = Constants.ApplicationId;
        builder.Services.AddSingleton<IBarrel>(Barrel.Current);
        
        builder.Services.AddHttpClient<IDataService, DataService>();
        builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();
        builder.Services.AddHttpClient<IRecordService, RecordService>();

        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPageViewModel>();
        
        builder.Services.AddTransient<StartPageViewModel>();
        
        builder.Services.AddTransient<AccountPageViewModel>();
        builder.Services.AddTransient<AccountsPageViewModel>();
        builder.Services.AddTransient<EntryPageViewModel>();
        builder.Services.AddTransient<LoginPageViewModel>();
        builder.Services.AddTransient<UserPageViewModel>();
        builder.Services.AddTransient<DevicesPageViewModel>();
        builder.Services.AddTransient<RecordPageViewModel>();
        builder.Services.AddTransient<SettingsPageViewModel>();
        builder.Services.AddTransient<CurrenciesPageViewModel>();
        builder.Services.AddTransient<CurrencyPageViewModel>();
        builder.Services.AddTransient<CategoriesPageViewModel>();
        builder.Services.AddTransient<CategoryPageViewModel>();
        
        //Widgets
        builder.Services.AddSingleton<WidgetContainerViewModel>();
        builder.Services.AddTransient<WidgetLastRecordsViewModel>();
        builder.Services.AddTransient<WidgetCashFlowViewModel>();
        
        return builder;
    }
    
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<StartPage>();
        
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<AccountsPage>();
        builder.Services.AddTransient<CategoriesPage>();
        builder.Services.AddTransient<CategoryPage>();
        builder.Services.AddTransient<EntryPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<UserPage>();
        builder.Services.AddTransient<DevicesPage>();
        builder.Services.AddTransient<RecordPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<CurrenciesPage>();
        builder.Services.AddTransient<CurrencyPage>();

        //Dialogs
        builder.Services.AddTransient<MessageDialog>();
        builder.Services.AddTransient<WidgetSettingsDialog>();
        
        //WidgetTabs
        builder.Services.AddTransient<WidgetTabs>();
        builder.Services.AddTransient<BalanceWidgetsPage>();
        
        return builder;
    }
}
