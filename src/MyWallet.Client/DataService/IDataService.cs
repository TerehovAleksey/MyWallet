namespace MyWallet.Client.DataService;

public interface IDataService
{
    #region Currency, Account

    /// <summary>
    /// Получение списка всех валют
    /// </summary>
    /// <returns></returns>
    public Task<List<Currency>> GetCurrenciesAsync();

    /// <summary>
    /// Получение валют пользователя
    /// </summary>
    /// <returns></returns>
    public List<Currency> GetCurrentCurrencies();

    /// <summary>
    /// Получение списка типов счетов
    /// </summary>
    /// <returns></returns>
    public Task<List<AccountType>> GetAccountTypesAsync();

    /// <summary>
    /// Получение всех счетов пользователя
    /// </summary>
    /// <returns></returns>
    public Task<List<Account>> GetAccountsAsync();

    /// <summary>
    /// Создание счёта пользователя
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public Task CreateAccountAsync(AccountCreate account);

    #endregion

    public Task<bool> DeleteAccountAsync(Guid id);

    

    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<Category> CreateCategoryAsync(string name, string imageName);

    public Task<List<Record>> GetRecordsAsync(DateTime startDate, DateTime endDate);
    public Task<Record> CreateRecordAsync(RecordCreate record);

    
}
