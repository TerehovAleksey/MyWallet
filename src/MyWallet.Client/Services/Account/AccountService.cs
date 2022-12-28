using MyWallet.Client.Services.Rest;
using RestServiceBase = MyWallet.Client.Services.Rest.RestServiceBase;

namespace MyWallet.Client.Services.Account;

public class AccountService : RestServiceBase, IAccountService
{
    public const string ACCOUNTS_KEY = "accounts";
    public const string ACCOUNT_TYPES_KEY = "account_types";
    

    public AccountService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService) : base(httpClient, connectivity, storageService)
    {
    }

    public async Task<List<AccountType>> GetAccountTypesAsync()
    {
        var data = await StorageService.LoadFromCache<List<AccountType>>(ACCOUNT_TYPES_KEY).ConfigureAwait(false);
        if (data is not null)
        {
            return data;
        }
        data = await GetAsync<List<AccountType>>("type").ConfigureAwait(false);
        await StorageService.SaveToCache(ACCOUNT_TYPES_KEY, data, TimeSpan.FromDays(30)).ConfigureAwait(false);
        return data;
    }

    public async Task<List<Models.Account>> GetAccountsAsync()
    {
        var data = await StorageService.LoadFromCache<List<Models.Account>>(ACCOUNTS_KEY).ConfigureAwait(false);
        if (data is not null)
        {
            return data;
        }
        data = await GetAsync<List<Models.Account>>("account").ConfigureAwait(false);
        await StorageService.SaveToCache(ACCOUNTS_KEY, data, TimeSpan.FromDays(30)).ConfigureAwait(false);
        return data;
    }

    public async Task CreateAccountAsync(AccountCreate account)
    {
        await SendAsync("account", account).ConfigureAwait(false);
        await StorageService.DeleteFromCache(ACCOUNTS_KEY);
    }

    public async Task UpdateAccountAsync(AccountUpdate account)
    {
        await SendAsync($"account/{account.Id}", account, SendType.Put);
        await StorageService.DeleteFromCache(ACCOUNTS_KEY);
    }

    public async Task DeleteAccountAsync(Guid id)
    {
        await DeleteAsync($"account/{id}");
        await StorageService.DeleteFromCache(ACCOUNTS_KEY);
    }
}