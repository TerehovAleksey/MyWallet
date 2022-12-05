namespace MyWallet.Client.DataService.Rest;

public class RestDataService : RestServiceBase, IDataService
{
    public RestDataService(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel) : base(httpClient, connectivity, cacheBarrel)
    {
    }

    #region Currency, Account

    public Task<Response<List<Currency>>> GetCurrenciesAsync() => GetAsync<List<Currency>>("currency", 24);

    public Task<Response<List<Currency>>> GetUserCurrencies() => GetAsync<List<Currency>>("currency/user", 24);

    public Task<Response<List<AccountType>>> GetAccountTypesAsync() => GetAsync<List<AccountType>>("type", 24);

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
