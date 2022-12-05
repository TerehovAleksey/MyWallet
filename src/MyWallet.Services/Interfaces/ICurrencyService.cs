namespace MyWallet.Services.Interfaces;

public interface ICurrencyService
{
    /// <summary>
    /// Получение всех валют
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetAllSymbolsWithDescription();

    /// <summary>
    /// Получение всех валют пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<IEnumerable<UserCurrencyDto>> GetUserCurrnciesAsync(Guid userId);

    /// <summary>
    /// Добавление валюты пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="currencySymbol"></param>
    /// <param name="isMain"></param>
    /// <returns></returns>
    public Task<UserCurrencyDto> CreateUserCurrencyAsync(Guid userId, string currencySymbol, bool isMain = false);
}
