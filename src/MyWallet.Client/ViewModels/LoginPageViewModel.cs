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
    private string _email;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _passwordConfirm;

    public string Caption => _isRegister ? "Создать новый аккаунт" : "С возвращением!";
    public string ButtonTitle => _isRegister ? "Создать счёт" : "Войти";
    public string SwitchTitle => _isRegister ? "Уже зарегестрированы? ВОЙТИ" : "Не зарегистрированы? РЕГИСТРАЦИЯ";

    public LoginPageViewModel(IDataService dataService, IUserService userService) : base(dataService)
    {
        _userService = userService;
    }

    public override void OnNavigatedTo(object? parameters)
    {
        base.OnNavigatedTo(parameters);

        IsRegister = (bool)(parameters ?? true);
    }

    [RelayCommand]
    private void Switch() => IsRegister = !IsRegister;

    [RelayCommand]
    private async Task Login()
    {
        if (IsRegister)
        {
            var authData = new UserRegisterData(Email, Password, PasswordConfirm);
            SetDataLoadingIndicators();            
            var response = await _userService.RegisterUserAsync(authData);
            SetDataLoadingIndicators(false);
            await HandleServiceResponseErrorsAsync(response);
        }
        else
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