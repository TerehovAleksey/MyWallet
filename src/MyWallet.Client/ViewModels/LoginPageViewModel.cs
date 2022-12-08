namespace MyWallet.Client.ViewModels;

[QueryProperty(nameof(IsRegister), "IsRegister")]
public partial class LoginPageViewModel : ViewModelBase
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

    public LoginPageViewModel(IUserService userService, IDialogService dialogService, INavigationService navigationService) : base(dialogService,
        navigationService)
    {
        _userService = userService;
    }

    [RelayCommand]
    private void Switch() => IsRegister = !IsRegister;

    [RelayCommand]
    private async Task Login()
    {
        await IsBusyFor(async () =>
        {
            IResponse? response;
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

            await HandleServiceResponseErrorsAsync(response);
        });

        await NavigationService.GoToAsync("/main");
    }

    [RelayCommand]
    private Task RememberPassword() =>
        DialogService.ShowAlertAsync("В разработке", "Пока не реализовано", "Понятно");
}