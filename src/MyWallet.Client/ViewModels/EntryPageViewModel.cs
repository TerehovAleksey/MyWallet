namespace MyWallet.Client.ViewModels;

public partial class EntryPageViewModel : ViewModelBase
{
    [RelayCommand]
    private Task GoogleRegister() =>
        DialogService.ShowAlertAsync("В разработке", "Пока не реализовано", "Понятно");

    [RelayCommand]
    private Task FacebookRegister() =>
        DialogService.ShowAlertAsync("В разработке", "Пока не реализовано", "Понятно");

    [RelayCommand]
    private Task EmailRegister() => NavigationService.GoToAsync("login", new Dictionary<string, object>
    {
        { "IsRegister", true }
    });

    [RelayCommand]
    private Task Login() => NavigationService.GoToAsync("login", new Dictionary<string, object>
    {
        { "IsRegister", false }
    });

    public EntryPageViewModel(IDialogService dialogService, INavigationService navigationService) : base(dialogService, navigationService)
    {
    }
}