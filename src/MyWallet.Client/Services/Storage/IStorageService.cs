namespace MyWallet.Client.Services.Storage;

public interface IStorageService
{
    #region Cache

    public Task SaveToCache<T>(string key, T value, TimeSpan timeSpan);
    public Task<T?> LoadFromCache<T>(string key);
    public Task DeleteFromCache(string key);
    public Task ClearCache();

    #endregion

    #region Token

    public Task<bool> IsAuthorized();
    public Task<AuthResponse> GetToken();
    public Task SaveToken(AuthResponse token);
    public Task DeleteToken(); 

    #endregion
}
