namespace MyWallet.Client.DataService.Rest;

public class RestDataService : RestServiceBase, IDataService
{
    public RestDataService(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel) : base(httpClient, connectivity, cacheBarrel)
    {
    }

    #region Currency

    public Task<Response<List<Currency>>> GetCurrenciesAsync() => GetAsync<List<Currency>>("currency", 24);
    public Task<Response<List<Currency>>> GetUserCurrencies() => GetAsync<List<Currency>>("currency/user", 24);

    #endregion

    #region Account

    public Task<Response<List<AccountType>>> GetAccountTypesAsync() => GetAsync<List<AccountType>>("type", 24);
    public Task<Response<List<Account>>> GetAccountsAsync() => GetAsync<List<Account>>("account", 24);
    public async Task<IResponse> CreateAccountAsync(AccountCreate account)
    {
        var result = await SendAsync("account", account);
        if (result.State == State.Success)
        {
            RemoveFromCache("account");
        }
        return result;
    }
    public async Task<IResponse> UpdateAccountAsync(AccountUpdate account)
    {
        var result = await SendAsync($"account/{account.Id}", account, SendType.Put);
        if (result.State == State.Success)
        {
            RemoveFromCache("account");
        }
        return result;
    }
    public async Task<IResponse> DeleteAccountAsync(Guid id)
    {
        var result = await DeleteAsync($"account/{id}");
        if (result.State == State.Success)
        {
            RemoveFromCache("account");
        }
        return result;
    }

    #endregion

    public async Task<IResponse> CreateRecordAsync(RecordCreate record)
    {
        var result = await SendAsync("journal", record);
        if (result.State == State.Success)
        {
            RemoveFromCache("journal");
            RemoveFromCache("account");
        }
        return result;
    }

    public Task<Response<List<Category>>> GetAllCategoriesAsync() => GetAsync<List<Category>>("category", 24);

    public Task<Response<List<Record>>> GetRecordsAsync(DateTime startDate, DateTime endDate) => 
        GetAsync<List<Record>>($"journal?startDate={startDate:yyyy-MM-dd}&finishDate={endDate:yyyy-MM-dd}");
}
