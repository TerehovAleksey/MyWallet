namespace MyWallet.Client.ViewModels.Entry;

[QueryProperty(nameof(IsRegister), "IsRegister")]
public partial class LoginPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

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

    public string Caption => _isRegister ? Strings.CreateNewAccount : Strings.WelcomeBack;
    public string ButtonTitle => _isRegister ? Strings.CreateAccount : Strings.LogIn;
    public string SwitchTitle => _isRegister ? $"{Strings.AlreadyHaveAnAccount} {Strings.LogIn}" : $"{Strings.DontHaveAnAccount} {Strings.SignUp}";

    public LoginPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    private void Switch() => IsRegister = !IsRegister;

    [RelayCommand]
    private async Task Login()
    {
        await IsBusyFor(async () =>
        {
            if (IsRegister)
            {
                var authData = new UserRegisterData(Email, Password, PasswordConfirm);
                await _dataService.Auth.RegisterUserAsync(authData);
            }
            else
            {
                var authData = new UserAuthData(Email, Password);
                await _dataService.Auth.LoginAsync(authData);
                AppService.SetAppState(true);
            }
        });
    }

    [RelayCommand]
    private Task RememberPassword() => DialogService.ShowInDevelopmentMessage();
}