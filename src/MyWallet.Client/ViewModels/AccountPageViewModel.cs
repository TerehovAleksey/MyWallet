﻿namespace MyWallet.Client.ViewModels;

public partial class AccountPageViewModel : AppViewModelBase
{
    private readonly IDataService _dataService;
    
    private Account? _account;

    [ObservableProperty]
    private bool _isNewAccount;

    [ObservableProperty]
    private string _accountName = string.Empty;

    [ObservableProperty]
    private AccountType? _accountType;

    [ObservableProperty]
    private string _accountNumber = string.Empty;

    [ObservableProperty]
    private decimal _accountValue = 0;

    [ObservableProperty]
    private string _accountCurrencyType = string.Empty;

    [ObservableProperty]
    private Currency? _currentCurrency;


    public Account? Account
    {
        get => _account;
        set
        {
            if (value is not null && SetProperty(ref _account, value))
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

    public ObservableCollection<AccountType> AccountTypes { get; } = new();
    public ObservableCollection<Currency> Currencies { get; } = new();

    public AccountPageViewModel(IDataService dataService)
    {
        _dataService = dataService;
        Title = "Новый счёт";
    }

    public override async void OnNavigatedTo(object? parameters)
    {
        SetDataLoadingIndicators();

        var typesResponse = await _dataService.GetAccountTypesAsync();
        await HandleServiceResponseErrorsAsync(typesResponse);
        AccountTypes.AddRange(typesResponse.Item);

        //var currencyResponse = await _dataService.GetUserCurrencies();
        //await HandleServiceResponseErrorsAsync(currencyResponse);
        //Currencies.AddRange(currencyResponse.Item);

        SetDataLoadingIndicators(false);

        Account = parameters as Account;
        IsNewAccount = Account == null;
        Title = IsNewAccount ? "Новый счёт" : "Изменить счёт";
    }

    [RelayCommand]
    private async Task SaveAndReturn()
    {
        SetDataLoadingIndicators(true);

        //Save
        await _dataService.CreateAccountAsync(new AccountCreate(AccountName, AccountNumber, AccountType.Id, AccountValue, CurrentCurrency.Symbol, "#ad1457"));

        SetDataLoadingIndicators(false);

        //Return
        await NavigationService.PopAsync();
    }
}
