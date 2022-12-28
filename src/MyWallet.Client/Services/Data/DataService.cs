namespace MyWallet.Client.Services.Data;

public class DataService : IDataService
{
    public IAuthService Auth { get; }
    public IAccountService Account { get; }
    public ICurrencyService Currency { get; }
    public IRecordService Record { get; }
    public ICategoryService Categories { get; }

    public DataService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService)
    {
        Auth = new AuthService(httpClient, connectivity, storageService);
        Account = new AccountService(httpClient, connectivity, storageService);
        Currency = new CurrencyService(httpClient, connectivity, storageService);
        Record = new RecordService(httpClient, connectivity, storageService);
        Categories = new CategoryService(httpClient, connectivity, storageService);
    }
}