namespace MyWallet.Client.Services.Storage;

public class StorageService : IStorageService
{
    private const string TOKEN_KEY = "auth_data";

    private readonly IBarrel _cache;

    public StorageService(IBarrel cache)
    {
        _cache = cache;
    }

    #region Cache

    public async Task ClearCache()
    {
        var token = await GetToken();
        _cache.EmptyAll();
        await SaveToken(token);
    }

    public Task DeleteFromCache(string key)
    {
        _cache.Empty(key);
        return Task.CompletedTask;
    }

    public Task<T?> LoadFromCache<T>(string key)
    {
        return Task.FromResult(_cache.Get<T>(key));
    }

    public Task SaveToCache<T>(string key, T value, TimeSpan timeSpan)
    {
        _cache.Add(key, value, timeSpan);
        return Task.CompletedTask;
    }

    #endregion

    #region Token

    public async Task<bool> IsAuthorized()
    {
        var token = await GetToken();
        return !string.IsNullOrEmpty(token.Token) || !string.IsNullOrEmpty(token.RefreshToken) || token.TokenExpiresDate < DateTime.UtcNow;
    }

    public Task DeleteToken()
    {
        _cache.Empty(TOKEN_KEY);
        return Task.CompletedTask;
    }

    public Task<AuthResponse> GetToken()
    {
        var token = _cache.Get<AuthResponse>(TOKEN_KEY);

        //TODO: убрать после рефакторинга
        if (token == null)
        {
            var t = _cache.Get<string>(Constants.TOKEN_KEY);
            var rt = _cache.Get<string>(Constants.REFRESH_TOKEN_KEY);
            token = new AuthResponse(t, rt, DateTime.Now.AddDays(30));
        }

        return Task.FromResult(token);
    }

    public Task SaveToken(AuthResponse token)
    {
        _cache.Add(TOKEN_KEY, token, TimeSpan.FromDays(60));
        return Task.CompletedTask;
    }

    #endregion
}