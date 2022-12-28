namespace MyWallet.Client.ViewModels.MenuPages.Settings;

public partial class CurrenciesPageViewModel : ViewModelBase
{
    private readonly ICurrencyService _currencyService;

    private UserCurrencyDto? _selectedCurrency;

    public UserCurrencyDto? SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            if (value == null && _selectedCurrency == value)
            {
                return;
            }

            _selectedCurrency = value;
            OnPropertyChanged();
            NavigationService.GoToAsync("currency", new Dictionary<string, object>
            {
                { "UserCurrency", value }
            });
        }
    }

    public ObservableCollection<UserCurrencyDto> UserCurrencies { get; } = new();

    public CurrenciesPageViewModel(IAppService appService, ICurrencyService currencyService) : base(appService)
    {
        _currencyService = currencyService;
        OneTimeInitialized = false;
        Title = Strings.Currencies;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        _selectedCurrency = null;
        var currencies = await _currencyService.GetUserCurrencyAsync();
        UserCurrencies.AddRange(currencies, true);
    });


    [RelayCommand]
    private Task FabButton() => NavigationService.GoToAsync("currency");
}