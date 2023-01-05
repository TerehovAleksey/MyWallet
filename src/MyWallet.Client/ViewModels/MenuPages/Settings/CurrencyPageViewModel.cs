﻿namespace MyWallet.Client.ViewModels.MenuPages.Settings;

[QueryProperty(nameof(Symbol), "symbol")]
public partial class CurrencyPageViewModel : ViewModelBase
{
    private readonly ICurrencyService _currencyService;
    private UserCurrencyDto? _mainCurrency;
    private CurrencyDto? _selectedCurrency;
    private decimal? _rate;
    private decimal? _reverseRate;

    [ObservableProperty]
    private string? _currencySymbol;

    [ObservableProperty]
    private string? _mainToTargetTitle = Strings.InverseRate;

    [ObservableProperty]
    private string? _targetToMainTitle = Strings.Rate;

    [ObservableProperty]
    private bool _isNewCurrency;

    public decimal? Rate
    {
        get => _rate;
        set
        {
            if (value is not null && SetProperty(ref _rate, value) && _mainCurrency is not null && _selectedCurrency is not null)
            {
                _ = _currencyService.UpdateCurrencyRate(_selectedCurrency.Symbol, _mainCurrency.Symbol, (decimal)value);
            }
        }
    }

    public decimal? ReverseRate
    {
        get => _reverseRate;
        set
        {
            if (value is not null && SetProperty(ref _reverseRate, value) && _mainCurrency is not null && _selectedCurrency is not null)
            {
                _ = _currencyService.UpdateCurrencyRate(_mainCurrency.Symbol, _selectedCurrency.Symbol, (decimal)value);
            }
        }
    }

    public ObservableCollection<CurrencyDto> Currencies { get; } = new();

    public CurrencyDto? SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            if (value != null)
            {
                SetProperty(ref _selectedCurrency, value);
                CurrencySymbol = _selectedCurrency?.Symbol;
                _ = LoadRates(value.Symbol);
            }
        }
    }

    public string? Symbol { get; set; }

    public CurrencyPageViewModel(IAppService appService, ICurrencyService currencyService) : base(appService)
    {
        _currencyService = currencyService;
    }

    public override async Task InitializeAsync()
    {
        Title = Symbol == null ? Strings.NewCurrency : Strings.EditCurrency;
        IsNewCurrency = Symbol == null;

        await IsBusyFor(async () =>
        {
            var userCurrencies = await _currencyService.GetUserCurrencyAsync();
            _mainCurrency = userCurrencies.FirstOrDefault(x => x.IsMain);

            var currencies = await _currencyService.GetAllCurrenciesAsync();
            Currencies.AddRange(currencies);

            if (Symbol != null)
            {
                SelectedCurrency = currencies.FirstOrDefault(c => c.Symbol == Symbol);
                MainToTargetTitle = $"1 {_selectedCurrency?.Symbol} =";
                TargetToMainTitle = $"1 {_mainCurrency?.Symbol} =";
            }
        });
    }

    private Task LoadRates(string currencySymbol, bool onlySaveToCache = false) => IsBusyFor(async () =>
    {
        var rates = await _currencyService.GetCurrencyRates(currencySymbol, onlySaveToCache);
        MainToTargetTitle = $"1 {rates.BaseSymbol} =";
        TargetToMainTitle = $"1 {rates.TargetSymbol} =";
        _rate = rates.TargetRate;
        _reverseRate = rates.BaseRate;
        OnPropertyChanged(nameof(Rate));
        OnPropertyChanged(nameof(ReverseRate));
    });

    [RelayCommand]
    private async Task RefreshRates()
    {
        if (CurrencySymbol is not null)
        {
            await LoadRates(CurrencySymbol, true);
        }
    }

    [RelayCommand]
    private async Task SaveAndReturn()
    {
        if (SelectedCurrency is not null)
        {
            await IsBusyFor(async () =>
            {
                await _currencyService.CreateUserCurrencyAsync(SelectedCurrency);
                await NavigationService.GoBackAsync();
            });
        }
        else
        {
            await DialogService.ShowMessageAsync(Strings.Error, Strings.NoCurrencySelected);
        }
    }

    [RelayCommand]
    private Task DeleteAndReturn() => DialogService.ShowInDevelopmentMessage();
}