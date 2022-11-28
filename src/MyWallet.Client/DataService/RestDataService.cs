using MonkeyCache;

namespace MyWallet.Client.DataService;

public class RestDataService : RestServiceBase, IDataService
{
    public RestDataService(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel) : base(httpClient, connectivity, cacheBarrel)
    {
    }

    #region Currency, Account

    public Task<List<Currency>> GetCurrenciesAsync() => GetAsync<List<Currency>>("currency");

    public List<Currency> GetCurrentCurrencies()
    {
        return new List<Currency>
        {
            new Currency("BYN", "Belarussian Ruble"),
            new Currency("USD", "United States Dollar")
        };
    }

    public Task<List<AccountType>> GetAccountTypesAsync() => GetAsync<List<AccountType>>("type");

    public Task<List<Account>> GetAccountsAsync() => GetAsync<List<Account>>("account", 0);

    public Task CreateAccountAsync(AccountCreate account) => PostAsync("account", account);

    #endregion

    public Task<Category> CreateCategoryAsync(string name, string imageName)
    {
        throw new NotImplementedException();
    }

    public Task<Record> CreateRecordAsync(RecordCreate record)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAccountAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetAllCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Record>> GetRecordsAsync(DateTime startDate, DateTime endDate) =>
        GetAsync<List<Record>>($"journal?startDate={startDate:yyyy-MM-dd}&finishDate={endDate:yyyy-MM-dd}");
}
