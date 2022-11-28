namespace MyWallet.Client.ViewModels;

public partial class AccountPageViewModel : AppViewModelBase
{
    private Account _account;

    [ObservableProperty]
    private bool _isNewAccount = false;

    [ObservableProperty]
    private string _accountName = string.Empty;

    [ObservableProperty]
    private AccountType _accountType;

    [ObservableProperty]
    private string _accountNumber = string.Empty;

    [ObservableProperty]
    private decimal _accountValue = 0;

    [ObservableProperty]
    private string _accountCurrencyType = string.Empty;

    [ObservableProperty]
    private Currency _currentCurrency;


    public Account Account
    {
        get => _account;
        set
        {
            if (SetProperty(ref _account, value))
            {
                AccountName = value.Name;
                AccountNumber = "";
                AccountValue = value.Balance;
                AccountCurrencyType = value.CurrencySymbol;
                AccountType = AccountTypes.FirstOrDefault(x => x.Name == value.AccountType);
            }
            else
            {
                AccountType = AccountTypes.First();
            }
        }
    }

    [ObservableProperty]
    private List<AccountType> _accountTypes;

    [ObservableProperty]
    private List<Currency> _currencies;

    public AccountPageViewModel(IDataService dataService) : base(dataService)
    {
        Title = "Новый счёт";
    }

    public override async void OnNavigatedTo(object parameters)
    {
        AccountTypes = await DataService.GetAccountTypesAsync();
        Currencies = DataService.GetCurrentCurrencies();

        Account = parameters as Account;
        IsNewAccount = Account == null;
        Title = IsNewAccount ? "Новый счёт" : "Изменить счёт";
    }

    [RelayCommand]
    private async Task SaveAndReturn()
    {
        SetDataLodingIndicators(true);

        //Save
        await DataService.CreateAccountAsync(new AccountCreate(AccountName, AccountNumber, AccountType.Id, AccountValue, CurrentCurrency.Symbol, "#ad1457"));

        SetDataLodingIndicators(false);

        //Return
        await NavigationService.PopAsync();
    }
}
