using MenuItem = MyWallet.Client.UI.MenuItem;

namespace MyWallet.Client.ViewModels.MenuPages.Settings;

public partial class CurrenciesPageViewModel : ViewModelBase
{
    private readonly ICurrencyService _currencyService;

    public ObservableCollection<MenuItem> MenuItems { get; } = new();

    public CurrenciesPageViewModel(IAppService appService, ICurrencyService currencyService) : base(appService)
    {
        _currencyService = currencyService;
        OneTimeInitialized = false;
        Title = Strings.Currencies;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        var currencies = await _currencyService.GetUserCurrencyAsync();
        var menuItems = currencies
            .Select(c => new MenuItem
            {
                Title = c.Symbol,
                Description = c.Description,
                Link = $"currency?symbol={c.Symbol}",
                HasSeparator = true,
            });
        MenuItems.AddRange(menuItems, true);
    });

    [RelayCommand]
    private async Task Navigate(string link)
    {
        if (!string.IsNullOrEmpty(link))
        {
            await AppService.Navigation.GoToAsync(link);
        }
    }

    [RelayCommand]
    private Task FabButton() => NavigationService.GoToAsync("currency");
}