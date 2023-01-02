namespace MyWallet.Client.Services.App;

public class AppService : IAppService
{
    public event Action<bool>? OnAppStateChanged;

    public IDialogService Dialog { get; }
    public INavigationService Navigation { get; }
    public IStorageService Storage { get; }
    public ISettingsService Settings { get; }

    public AppService(IDialogService dialogService, INavigationService navigationService, IStorageService storageService, ISettingsService settingsService)
    {
        Dialog = dialogService;
        Navigation = navigationService;
        Storage = storageService;
        Settings = settingsService;
    }

    public void SetAppState(bool isAuthenticated)
    {
        OnAppStateChanged?.Invoke(isAuthenticated);
    }
}
