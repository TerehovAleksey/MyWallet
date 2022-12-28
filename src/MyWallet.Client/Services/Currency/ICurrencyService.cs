namespace MyWallet.Client.Services.Currency;

public interface ICurrencyService
{
    public Task<List<UserCurrencyDto>> GetUserCurrencyAsync();
    public Task<List<CurrencyDto>> GetAllCurrenciesAsync();
    public Task CreateUserCurrencyAsync(CurrencyDto currency);
    public Task<CurrencyRates> GetCurrencyRates(string targetSymbol, bool onlySaveToCache = false);
    public Task UpdateCurrencyRate(string mainSymbol, string targetSymbol, decimal value);
}
