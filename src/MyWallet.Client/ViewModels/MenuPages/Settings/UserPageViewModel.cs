namespace MyWallet.Client.ViewModels.MenuPages.Settings;

public partial class UserPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private DateTime _birthday = DateTime.Today.AddYears(-18);

    [ObservableProperty]
    private string _userGender = Gender.Empty.ToString();

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _newPassword;

    public string[] Genders { get; init; }

    public UserPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
        Title = Strings.UserProfile;
        Genders = Enum.GetNames<Gender>();
    }

    public override Task InitializeAsync() =>
        IsBusyFor(async () =>
        {
            var user = await _dataService.Auth.GetUserDataAsync();
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            if (user.BirthdayDate is not null)
            {
                Birthday = (DateTime)user.BirthdayDate;
            }
            UserGender = user.Gender.ToString();
        });

    [RelayCommand]
    private Task Save() =>
        IsBusyFor(async () =>
        {
            var data = new UserUpdateData(FirstName, LastName, Birthday, (Gender)Enum.Parse(typeof(Gender), UserGender));
            await _dataService.Auth.UpdateUserDataAsync(data);
            await DialogService.ShowMessageAsync(Strings.UserProfile, Strings.UserProfileChangedSuccess);
        });

    [RelayCommand]
    private Task Logout() =>
        IsBusyFor(async () =>
        {
            await _dataService.Auth.LogoutAsync();
            AppService.SetAppState(false);
        });

    [RelayCommand]
    private async Task ChangePassword()
    {
        if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(NewPassword))
        {
            return;
        }

        await IsBusyFor(async () =>
        {
            var data = new PasswordChangeData(Password, NewPassword, NewPassword);
            await _dataService.Auth.ChangePasswordAsync(data);
            await DialogService.ShowMessageAsync(Strings.PasswordChange, Strings.PasswordChangedSuccess);
        });
    }

    [RelayCommand]
    private Task DeleteUser() =>
        IsBusyFor(async () =>
        {
            await _dataService.Auth.DeleteUserDataAsync();
            AppService.SetAppState(false);
        });
    
    [RelayCommand]
    private Task Clear() =>
        IsBusyFor(async () =>
        {
            await AppService.Storage.ClearCache();
            await AppService.Navigation.GoToAsync("//home");
        });
}