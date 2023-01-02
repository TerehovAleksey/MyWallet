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
                NavigationService.GoToAsync("account", new Dictionary<string, object>
                {
                    { "Account", value }
                });
            }
        }
    }

    public ObservableCollection<Account> Accounts { get; } = new();

    public AccountsPageViewModel(IAppService appService, IDataService dataService) : base(appService)
	{
        _dataService = dataService;
        Title = Strings.AccountsSettings;
        OneTimeInitialized = false;
    }

    public override Task InitializeAsync()
    {
        return IsBusyFor(async () =>
        {
            var accounts = await _dataService.Account.GetAccountsAsync();
            Accounts.AddRange(accounts, true);
            SelectedAccount = null;
        });
    }

    #region Commands

    [RelayCommand]
    private async Task OpenAccountPage()
    {
        await NavigationService.GoToAsync("account");
    }

    #endregion
}
