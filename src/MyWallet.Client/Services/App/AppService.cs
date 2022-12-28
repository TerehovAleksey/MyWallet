namespace MyWallet.Client.Services.App;

public class AppService : IAppService
{
    public event Action<bool>? OnAppStateChanged;

    public IDialogService Dialog { get; }
    public INavigationService Navigation { get; }
    public IStorageService Storage { get; }

    public AppService(IDialogService dialogService, INavigationService navigationService, IStorageService storageService)
    {
        Dialog = dialogService;
        Navigation = navigationService;
        Storage = storageService;
    }

    public void SetAppState(bool isAuthenticated)
    {
        OnAppStateChanged?.Invoke(isAuthenticated);
    }
}
