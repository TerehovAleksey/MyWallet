namespace MyWallet.Client.Services.Account;

public interface IAccountService
{
    /// <summary>
    /// Получение списка типов счетов
    /// </summary>
    /// <returns></returns>
    public Task<List<AccountType>> GetAccountTypesAsync();

    /// <summary>
    /// Получение всех счетов пользователя
    /// </summary>
    /// <returns></returns>
    public Task<List<Models.Account>> GetAccountsAsync();

    /// <summary>
    /// Создание счёта пользователя
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public Task CreateAccountAsync(AccountCreate account);

    /// <summary>
    /// Изменение счёта пользователя
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public Task UpdateAccountAsync(AccountUpdate account);

    /// <summary>
    /// Удаление счёта пользователя
    /// </summary>
    /// <param name="id">ID счёта</param>
    /// <returns></returns>
    public Task DeleteAccountAsync(Guid id);
}