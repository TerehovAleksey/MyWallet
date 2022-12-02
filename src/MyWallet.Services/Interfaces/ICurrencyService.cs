namespace MyWallet.Services.Interfaces;

public interface ICurrencyService
{
    public Dictionary<string, string> GetAllSymbolsWithDescription();
    public Task CreateUserCurrencyAsync(Guid userId, string currencySymbol, bool isMain = false);
}
