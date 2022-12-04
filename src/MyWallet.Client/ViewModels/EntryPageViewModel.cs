namespace MyWallet.Client.ViewModels;

public partial class EntryPageViewModel : AppViewModelBase
{
    [RelayCommand]
    private async Task GoogleRegister()
    {
        await PageService.DisplayAlert("В разработке","Пока не реализовано", "Понятно");
    }

    [RelayCommand]
    private async Task FacebookRegister()
    {
        await PageService.DisplayAlert("В разработке", "Пока не реализовано", "Понятно");
    }

    [RelayCommand]
    private async Task EmailRegister()
    {
        await NavigationService.PushAsync(new LoginPage(true));
    }

    [RelayCommand]
    private async Task Login()
    {
        await NavigationService.PushAsync(new LoginPage(false));
    }
}
