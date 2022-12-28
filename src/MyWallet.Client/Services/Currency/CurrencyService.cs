using RestServiceBase = MyWallet.Client.Services.Rest.RestServiceBase;

namespace MyWallet.Client.Services.Currency;

public class CurrencyService : RestServiceBase, ICurrencyService
{
    private const string USER_CURRENCIES_KEY = "user_currencies";
    private const string ALL_CURRENCIES_KEY = "all_currencies";

    public CurrencyService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService) : base(httpClient, connectivity,
        storageService)
    {
    }

    public Task<List<CurrencyDto>> GetAllCurrenciesAsync() =>
        GetFromCacheOrApi<List<CurrencyDto>>(ALL_CURRENCIES_KEY, "currency");

    public Task<List<UserCurrencyDto>> GetUserCurrencyAsync() =>
        GetFromCacheOrApi<List<UserCurrencyDto>>(USER_CURRENCIES_KEY, "currency/user");

    public async Task CreateUserCurrencyAsync(CurrencyDto currency)
    {
        await SendAsync("currency/user", currency);
        await StorageService.DeleteFromCache(USER_CURRENCIES_KEY);
    }

    public async Task<CurrencyRates> GetCurrencyRates(string targetSymbol, bool onlySaveToCache = false)
    {
        var main = await GetMainCurrencyAsync().ConfigureAwait(false);

        if (main.Symbol == targetSymbol)
        {
            return new CurrencyRates(main.Symbol, targetSymbol, 1, 1);
        }

        var targetToMain = await GetFromCacheOrApi<CurrencyExchangeDto>($"{targetSymbol}_to_{main.Symbol}_rate",
            $"currency/exchange?baseSymbol={targetSymbol}&targetSymbol={main.Symbol}", onlySaveToCache).ConfigureAwait(false);
        var mainToTarget = await GetFromCacheOrApi<CurrencyExchangeDto>($"{main.Symbol}_to_{targetSymbol}_rate",
            $"currency/exchange?baseSymbol={main.Symbol}&targetSymbol={targetSymbol}", onlySaveToCache).ConfigureAwait(false);
        return new CurrencyRates(targetSymbol, main.Symbol, mainToTarget.Value, targetToMain.Value);
    }

    public async Task UpdateCurrencyRate(string mainSymbol, string targetSymbol, decimal value)
    {
        var rate = new CurrencyExchangeDto
        {
            Base = mainSymbol,
            Target = targetSymbol,
            Date = DateTime.Now,
            Value = value
        };
        await StorageService.SaveToCache($"{mainSymbol}_to_{targetSymbol}_rate", rate, TimeSpan.FromDays(30));
    }


    private async Task<CurrencyDto> GetMainCurrencyAsync()
    {
        var currencies = await GetUserCurrencyAsync().ConfigureAwait(false);
        var main = currencies.First();
        return new CurrencyDto(main.Symbol, main.Description);
    }

    private async Task<T> GetFromCacheOrApi<T>(string key, string url, bool onlySaveToCache = false) where T : class, new()
    {
        T? data;
        if (!onlySaveToCache)
        {
            data = await StorageService.LoadFromCache<T>(key).ConfigureAwait(false);
            if (data is not null)
            {
                return data;
            }
        }

        data = await GetAsync<T>(url).ConfigureAwait(false);
        await StorageService.SaveToCache(key, data, TimeSpan.FromDays(30)).ConfigureAwait(false);
        return data;
    }
}