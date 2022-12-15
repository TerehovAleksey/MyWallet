namespace MyWallet.Client.ViewModels;

[QueryProperty(nameof(Account), "Account")]
public partial class AccountPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private bool _isNewAccount;

    [ObservableProperty]
    private string _accountName = string.Empty;

    [ObservableProperty]
    private AccountType? _accountType;

    [ObservableProperty]
    private string? _accountNumber;

    [ObservableProperty]
    private decimal _accountValue;

    [ObservableProperty]
    private string _accountCurrencyType = string.Empty;

    [ObservableProperty]
    private Currency? _accountCurrency;

    [ObservableProperty]
    private bool _accountDisabled;

    [ObservableProperty]
    private bool _accountArchived;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccountColor))]
    private string? _accountColorString = "#a2a2a2";

    public Color AccountColor => Color.FromArgb(_accountColorString ?? "#a2a2a2");

    public Account? Account { get; set; }

    public ObservableCollection<AccountType> AccountTypes { get; } = new();
    public ObservableCollection<Currency> Currencies { get; } = new();
    public ObservableCollection<string> Colors { get; } = new();

    public AccountPageViewModel(IDataService dataService, IDialogService dialogService, INavigationService navigationService) : base(dialogService,
        navigationService)
    {
        _dataService = dataService;
        Title = "Новый счёт";
    }

    public override Task InitializeAsync() =>
        IsBusyFor(async () =>
        {
            var typesResponse = await _dataService.GetAccountTypesAsync();
            await HandleServiceResponseErrorsAsync(typesResponse);
            AccountTypes.AddRange(typesResponse.Item);

            var currencyResponse = await _dataService.GetUserCurrencies();
            await HandleServiceResponseErrorsAsync(currencyResponse);
            Currencies.AddRange(currencyResponse.Item);

            Colors.AddRange(UserColors.GetColors());

            IsNewAccount = Account == null;
            Title = IsNewAccount ? "Новый счёт" : "Изменить счёт";

            if (Account is not null)
            {
                AccountName = Account.Name;
                AccountNumber = Account.Number;
                AccountValue = Account.Balance;
                AccountCurrencyType = Account.CurrencySymbol;
                AccountArchived = Account.IsArchived;
                AccountDisabled = Account.IsDisabled;
                AccountType = AccountTypes.FirstOrDefault(x => x.Name == Account.AccountType);
                AccountCurrency = Currencies.FirstOrDefault(x => x.Symbol == Account.CurrencySymbol);
                AccountColorString = Colors.FirstOrDefault(x => x == Account.ColorString);
            }
        });

    [RelayCommand]
    private async Task SaveAndReturn()
    {
        if (_isNewAccount)
        {
            // Создание
            if (AccountType is not null && AccountCurrency is not null && !string.IsNullOrEmpty(AccountName) &&
                !string.IsNullOrEmpty(AccountColorString))
            {
                await IsBusyFor(async () =>
                {
                    var response = await _dataService.CreateAccountAsync(new AccountCreate(AccountName, AccountNumber, AccountType.Id, AccountValue,
                        AccountCurrency.Symbol, AccountColorString));
                    await HandleServiceResponseErrorsAsync(response);
                    if (response.State == State.Success)
                    {
                        await NavigationService.GoBackAsync();
                    }
                });
            }
        }
        else if (Account is not null && AccountType is not null && !string.IsNullOrEmpty(AccountColorString))
        {
            // Редактирование
            await IsBusyFor(async () =>
            {
                var response = await _dataService.UpdateAccountAsync(new AccountUpdate(Account.Id, AccountName, AccountNumber, AccountType.Id,
                    AccountColorString, AccountDisabled, AccountArchived));
                await HandleServiceResponseErrorsAsync(response);
                if (response.State == State.Success)
                {
                    await NavigationService.GoBackAsync();
                }
            });
        }
        else
        {
            await DialogService.ShowAlertAsync("Ошибка", "Не все поля правильно заполнены", "Понятно");
        }
    }

    [RelayCommand]
    private async Task DeleteAndReturn()
    {
        if (Account is not null)
        {
            await IsBusyFor(async () =>
            {
                var response = await _dataService.DeleteAccountAsync(Account.Id);
                await HandleServiceResponseErrorsAsync(response);
                if (response.State == State.Success)
                {
                    await NavigationService.GoBackAsync();
                }
            });
        }
    }
}