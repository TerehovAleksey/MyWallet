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
    private CurrencyDto? _accountCurrency;

    [ObservableProperty]
    private bool _accountDisabled;

    [ObservableProperty]
    private bool _accountArchived;

    [ObservableProperty]
    private Color _accountColor = Color.FromArgb("#a2a2a2");

    public Account? Account { get; set; }

    public ObservableCollection<AccountType> AccountTypes { get; } = new();
    public ObservableCollection<CurrencyDto> Currencies { get; } = new();

    public AccountPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
        Title = Strings.NewAccount;
    }

    public override Task InitializeAsync() =>
        IsBusyFor(async () =>
        {
            var types = await _dataService.Account.GetAccountTypesAsync();
            AccountTypes.AddRange(types);

            var currencies = await _dataService.Currency.GetUserCurrencyAsync();
            Currencies.AddRange(currencies.Select(x => new CurrencyDto(x.Symbol, x.Description)));

            IsNewAccount = Account == null;
            Title = IsNewAccount ? Strings.NewAccount : Strings.EditAccount;

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
                AccountColor = Color.FromArgb(Account.ColorString);
            }
        });

    [RelayCommand]
    private async Task SaveAndReturn()
    {
        var colorString = AccountColor.ToArgbHex();

        if (_isNewAccount)
        {
            // Создание
            if (AccountType is not null && AccountCurrency is not null && !string.IsNullOrEmpty(AccountName))
            {
                await IsBusyFor(async () =>
                {
                    await _dataService.Account.CreateAccountAsync(new AccountCreate(AccountName, AccountNumber, AccountType.Id, AccountValue,
                        AccountCurrency.Symbol, colorString));
                    await NavigationService.GoBackAsync();
                });
            }
        }
        else if (Account is not null && AccountType is not null)
        {
            // Редактирование
            await IsBusyFor(async () =>
            {
                await _dataService.Account.UpdateAccountAsync(new AccountUpdate(Account.Id, AccountName, AccountNumber, AccountType.Id,
                    colorString, AccountDisabled, AccountArchived));
                await NavigationService.GoBackAsync();
            });
        }
        else
        {
            await DialogService.ShowMessageAsync("Ошибка", "Не все поля правильно заполнены");
        }
    }

    [RelayCommand]
    private async Task DeleteAndReturn()
    {
        if (Account is not null)
        {
            await IsBusyFor(async () =>
            {
                await _dataService.Account.DeleteAccountAsync(Account.Id);
                await NavigationService.GoBackAsync();
            });
        }
    }
}