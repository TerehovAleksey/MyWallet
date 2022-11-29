namespace MyWallet.Client.ViewModels;

public partial class LoginPageViewModel : AppViewModelBase
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Caption))]
    [NotifyPropertyChangedFor(nameof(ButtonTitle))]
    [NotifyPropertyChangedFor(nameof(SwitchTitle))]
    private bool _isRegister = true;

    public string Caption => _isRegister ? "Создать новый аккаунт" : "С возвращением!";
    public string ButtonTitle => _isRegister ? "Создать счёт" : "Войти";
    public string SwitchTitle => _isRegister ? "Уже зарегестрированы? ВОЙТИ" : "Не зарегистрированы? РЕГИСТРАЦИЯ";

    public LoginPageViewModel(IDataService dataService) : base(dataService)
    {
    }

    public override void OnNavigatedTo(object? parameters)
    {
        base.OnNavigatedTo(parameters);

        IsRegister = (bool)(parameters ?? true);
    }

    [RelayCommand]
    private void Switch() => IsRegister = !IsRegister;

    [RelayCommand]
    private void Login()
    {
        if (IsRegister)
        {
            PageService.DisplayAlert("В разработке", "Пока не реализовано", "Понятно");
        }
        else
        {
            NavigationService.PushAsync(new MainPage());
        }      
    }

    [RelayCommand]
    private void RememberPassword()
    {
        PageService.DisplayAlert("В разработке", "Пока не реализовано", "Понятно");
    }
}