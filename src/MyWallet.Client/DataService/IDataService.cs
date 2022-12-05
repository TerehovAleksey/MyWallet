namespace MyWallet.Client.DataService;

public interface IDataService
{
    #region Currency

    /// <summary>
    /// Получение списка всех валют
    /// </summary>
    /// <returns></returns>
    public Task<Response<List<Currency>>> GetCurrenciesAsync();

    /// <summary>
    /// Получение валют пользователя
    /// </summary>
    /// <returns></returns>
    public Task<Response<List<Currency>>> GetUserCurrencies();

    #endregion

    #region Account

    /// <summary>
    /// Получение списка типов счетов
    /// </summary>
    /// <returns></returns>
    public Task<Response<List<AccountType>>> GetAccountTypesAsync();

    /// <summary>
    /// Получение всех счетов пользователя
    /// </summary>
    /// <returns></returns>
    public Task<Response<List<Account>>> GetAccountsAsync();

    /// <summary>
    /// Создание счёта пользователя
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public Task<IResponse> CreateAccountAsync(AccountCreate account);

    /// <summary>
    /// Изменение счёта пользователя
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public Task<IResponse> UpdateAccountAsync(AccountUpdate account);

    /// <summary>
    /// Удаление счёта пользователя
    /// </summary>
    /// <param name="id">ID счёта</param>
    /// <returns></returns>
    public Task<IResponse> DeleteAccountAsync(Guid id);

    #endregion

    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<Category> CreateCategoryAsync(string name, string imageName);
    public Task<List<Record>> GetRecordsAsync(DateTime startDate, DateTime endDate);
    public Task<Record> CreateRecordAsync(RecordCreate record);  
}
