namespace MyWallet.Client.ViewModels;

public partial class AccountsPageViewModel : AppViewModelBase
{
    private Account _selectedAccount;
    public Account SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            if (SetProperty(ref _selectedAccount, value)) 
            {
                NavigationService.PushAsync(new AccountPage(_selectedAccount));
            }
        }
    }

    public ObservableCollection<Account> Accounts { get; } = new();

    public AccountsPageViewModel(IDataService dataService) : base(dataService)
	{
        Title = "Настройки счетов";

        //Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Карта", Balance = -80, Color = Color.FromArgb("#ad1457"), CurrencySymbol = "BYN" });
        //Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Наличные", Balance = 158.40M, Color = Color.FromArgb("#039be5"), CurrencySymbol = "BYN" });
        //Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Наличные (USD)", Balance = 0, Color = Color.FromArgb("#43a047"), CurrencySymbol = "USD" });
    }

    public override async void OnNavigatedTo(object parameters)
    {
        var accounts = await DataService.GetAccountsAsync();
        Accounts.AddRange(accounts);

        SelectedAccount = null;
    }

    #region Commands

    [RelayCommand]
    private async Task OpenAccountPage()
    {
        await NavigationService.PushAsync(new AccountPage(null));
    }

    #endregion
}
