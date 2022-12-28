namespace MyWallet.Client.ViewModels.Entry;

public partial class EntryPageViewModel : ViewModelBase
{
    [RelayCommand]
    private Task GoogleRegister() => DialogService.ShowInDevelopmentMessage();

    [RelayCommand]
    private Task FacebookRegister() => DialogService.ShowInDevelopmentMessage();

    [RelayCommand]
    private Task EmailRegister() => NavigationService.GoToAsync(nameof(LoginPage), new Dictionary<string, object>
    {
        { "IsRegister", true }
    });

    [RelayCommand]
    private Task Login() => NavigationService.GoToAsync(nameof(LoginPage), new Dictionary<string, object>
    {
        { "IsRegister", false }
    });

    public EntryPageViewModel(IAppService appService) : base(appService)
    {
    }
}