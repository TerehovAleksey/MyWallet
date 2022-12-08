namespace MyWallet.Client.ViewModels;

public partial class UserPageViewModel : ViewModelBase
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private DateTime? _birthday;

    [ObservableProperty]
    private string _userGender = Gender.Empty.ToString();

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _newPassword;

    public string[] Genders { get; init; }

    public UserPageViewModel(IUserService userService, IDialogService dialogService, INavigationService navigationService) : base(dialogService,
        navigationService)
    {
        _userService = userService;
        Title = "Профиль пользователя";
        Genders = Enum.GetNames<Gender>();
    }

    public override Task InitializeAsync() =>
        IsBusyFor(async () =>
        {
            var user = await _userService.GetUserDataAsync();
            await HandleServiceResponseErrorsAsync(user);
            if (user.State == State.Success && user.Item is not null)
            {
                FirstName = user.Item.FirstName;
                LastName = user.Item.LastName;
                Email = user.Item.Email;
                Birthday = user.Item.BirthdayDate;
                UserGender = user.Item.Gender.ToString();
            }
        });

    [RelayCommand]
    private Task Save() =>
        IsBusyFor(async () =>
        {
            var data = new UserUpdateData(FirstName, LastName, Birthday, (Gender)Enum.Parse(typeof(Gender), UserGender));
            var result = await _userService.UpdateUserDataAsync(data);
            await HandleServiceResponseErrorsAsync(result);
            if (result.State == State.Success)
            {
                await DialogService.ShowAlertAsync("Изменение данных", "Данные о пользователе успешно обновлены", "OK");
            }
        });

    [RelayCommand]
    private Task Logout() =>
        IsBusyFor(async () =>
        {
            var result = await _userService.LogoutAsync();
            await HandleServiceResponseErrorsAsync(result);
            if (result.State == State.Success)
            {
                await NavigationService.GoToAsync("entry");
            }
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
            var result = await _userService.ChangePasswordAsync(data);
            await HandleServiceResponseErrorsAsync(result);
            if (result.State == State.Success)
            {
                await DialogService.ShowAlertAsync("Изменение пароля", "Пароль успешно изменён", "OK");
            }
        });
    }

    [RelayCommand]
    private Task DeleteUser() =>
        IsBusyFor(async () =>
        {
            var result = await _userService.DeleteUserDataAsync();
            await HandleServiceResponseErrorsAsync(result);
            if (result.State == State.Success)
            {
                await NavigationService.GoToAsync("entry");
            }
        });
}