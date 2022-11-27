namespace MyWallet.Client.ViewModels;

public partial class AccountPageViewModel : AppViewModelBase
{
    private Account _account;

    [ObservableProperty]
    private bool _isNewAccount = false;

    [ObservableProperty]
    private string _accountName = string.Empty;

    [ObservableProperty]
    private string _accountType = string.Empty;

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
           if(SetProperty(ref _account, value))
            {
                AccountName = value.Name;
                AccountNumber = "";
                AccountValue = value.Balance;
                AccountCurrencyType = value.CurrencySymbol;
                AccountType = null;
            }
            else
            {
                AccountType = AccountTypes.First();
            }
        }
    }

    public List<string> AccountTypes { get; }
    public List<Currency> Currencies { get; }

    public AccountPageViewModel(IDataService dataService) : base(dataService)
    {
        Title = "Новый счёт";

        AccountTypes = DataService.GetAccountTypes().ToList();
        Currencies = DataService.GetCurrentCurrencies();
    }

    public override void OnNavigatedTo(object parameters)
    {
        Account = parameters as Account;
        IsNewAccount = Account == null;
        Title = IsNewAccount ? "Новый счёт" : "Изменить счёт";
    }

    [RelayCommand]
    private async Task SaveAndReturn()
    {
        SetDataLodingIndicators(true);

        //Save
        await Task.Delay(2000);

        SetDataLodingIndicators(false);

        //Return
        await NavigationService.PopAsync();
    }
}
