namespace MyWallet.Services;

public interface ICurrencyService
{
    public Dictionary<string, string> GetAllSymbolsWithDescription();
}
