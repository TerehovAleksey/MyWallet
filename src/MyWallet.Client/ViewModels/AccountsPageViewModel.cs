namespace MyWallet.Client.ViewModels;

public partial class AccountsPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            if (SetProperty(ref _selectedAccount, value) && value is not null)
            {
                NavigationService.GoToAsync(nameof(AccountPage), new Dictionary<string, object>
                {
                    { "Account", value }
                });
            }
        }
    }

    public ObservableCollection<Account> Accounts { get; } = new();

    public AccountsPageViewModel(IDataService dataService, IDialogService dialogService, INavigationService navigationService) : base(dialogService, navigationService)
	{
        _dataService = dataService;
        Title = "Настройки счетов";
        OneTimeInitialized = false;
    }

    public override Task InitializeAsync()
    {
        return IsBusyFor(async () =>
        {
            var accounts = await _dataService.GetAccountsAsync();
            Accounts.AddRange(accounts.Item, true);
            SelectedAccount = null;
        });
    }

    #region Commands

    [RelayCommand]
    private async Task OpenAccountPage()
    {
        await NavigationService.GoToAsync(nameof(AccountPage));
    }

    #endregion
}
