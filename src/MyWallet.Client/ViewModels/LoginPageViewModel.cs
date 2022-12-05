namespace MyWallet.Client.ViewModels;

public partial class LoginPageViewModel : AppViewModelBase
{
    private readonly IUserService _userService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Caption))]
    [NotifyPropertyChangedFor(nameof(ButtonTitle))]
    [NotifyPropertyChangedFor(nameof(SwitchTitle))]
    private bool _isRegister = true;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _passwordConfirm = string.Empty;

    public string Caption => _isRegister ? "Создать новый аккаунт" : "С возвращением!";
    public string ButtonTitle => _isRegister ? "Создать счёт" : "Войти";
    public string SwitchTitle => _isRegister ? "Уже зарегестрированы? ВОЙТИ" : "Не зарегистрированы? РЕГИСТРАЦИЯ";

    public LoginPageViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public override void OnNavigatedTo(object? parameters, bool reload)
    {
        if (!reload)
        {
            IsRegister = (bool)(parameters ?? true);
        }
    }

    [RelayCommand]
    private void Switch() => IsRegister = !IsRegister;

    [RelayCommand]
    private async Task Login()
    {
        IResponse? response;

        SetDataLoadingIndicators();
        if (IsRegister)
        {
            var authData = new UserRegisterData(Email, Password, PasswordConfirm);                 
            response = await _userService.RegisterUserAsync(authData);          
        }
        else
        {
            var authData = new UserAuthData(Email, Password);
            response = await _userService.LoginAsync(authData);
        }

        SetDataLoadingIndicators(false);
        await HandleServiceResponseErrorsAsync(response);

        if (response.State == State.Success)
        {
            await NavigationService.PushAsync(new MainPage());
        }
    }

    [RelayCommand]
    private void RememberPassword()
    {
        PageService.DisplayAlert("В разработке", "Пока не реализовано", "Понятно");
    }
}