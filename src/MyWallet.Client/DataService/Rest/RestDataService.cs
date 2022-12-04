namespace MyWallet.Client.DataService.Rest;

public class RestDataService : RestServiceBase, IDataService
{
    public RestDataService(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel) : base(httpClient, connectivity, cacheBarrel)
    {
    }

    #region Currency, Account

    public Task<List<Currency>> GetCurrenciesAsync()
    {
        throw new NotImplementedException();
        //GetAsync<List<Currency>>("currency");
    }

    public List<Currency> GetCurrentCurrencies()
    {
        return new List<Currency>
        {
            new("BYN", "Belarussian Ruble"),
            new("USD", "United States Dollar")
        };
    }

    public Task<List<AccountType>> GetAccountTypesAsync()
    {
        throw new NotImplementedException();
        //GetAsync<List<AccountType>>("type");
    }

    public Task<Response<List<Account>>> GetAccountsAsync() => GetAsync<List<Account>>("account", 24);

    public Task CreateAccountAsync(AccountCreate account)
    {
        throw new NotImplementedException();
        //PostAsync("account", account);
    }

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

    public Task<List<Record>> GetRecordsAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
        //GetAsync<List<Record>>($"journal?startDate={startDate:yyyy-MM-dd}&finishDate={endDate:yyyy-MM-dd}");
    }
}
