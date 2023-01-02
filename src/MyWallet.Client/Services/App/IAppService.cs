namespace MyWallet.Client.Services.App;

public interface IAppService
{
    public event Action<bool>? OnAppStateChanged;
    public void SetAppState(bool isAuthenticated);

    public IDialogService Dialog { get; }
    public INavigationService Navigation { get; }
    public IStorageService Storage { get; }
    public ISettingsService Settings { get; }
}
