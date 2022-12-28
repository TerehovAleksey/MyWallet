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
    [NotifyPropertyChangedFor(nameof(AccountColor))]
    private string? _accountColorString = "#a2a2a2";

    public Color AccountColor => Color.FromArgb(_accountColorString ?? "#a2a2a2");

    public Account? Account { get; set; }

    public ObservableCollection<AccountType> AccountTypes { get; } = new();
    public ObservableCollection<CurrencyDto> Currencies { get; } = new();
    public ObservableCollection<string> Colors { get; } = new();

    public AccountPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
        Title = "Новый счёт";
    }

    public override Task InitializeAsync() =>
        IsBusyFor(async () =>
        {
            var types = await _dataService.Account.GetAccountTypesAsync();
            AccountTypes.AddRange(types);

            var currencies = await _dataService.Currency.GetUserCurrencyAsync();
            Currencies.AddRange(currencies.Select(x => new CurrencyDto(x.Symbol, x.Description)));

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
                    await _dataService.Account.CreateAccountAsync(new AccountCreate(AccountName, AccountNumber, AccountType.Id, AccountValue,
                        AccountCurrency.Symbol, AccountColorString));
                    await NavigationService.GoBackAsync();
                });
            }
        }
        else if (Account is not null && AccountType is not null && !string.IsNullOrEmpty(AccountColorString))
        {
            // Редактирование
            await IsBusyFor(async () =>
            {
                await _dataService.Account.UpdateAccountAsync(new AccountUpdate(Account.Id, AccountName, AccountNumber, AccountType.Id,
                    AccountColorString, AccountDisabled, AccountArchived));
                await NavigationService.GoBackAsync();
            });
        }
        else
        {
            await DialogService.ShowMessageAsync("Ошибка", "Не все поля правильно заполнены", "Понятно");
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