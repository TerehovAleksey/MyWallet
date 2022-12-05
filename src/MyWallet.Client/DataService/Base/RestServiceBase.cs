using System.Net;
using System.Net.Http.Headers;

namespace MyWallet.Client.DataService.Base;

public abstract class RestServiceBase
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IConnectivity _connectivity;
    private readonly IBarrel _cacheBarrel;

    protected RestServiceBase(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.ApiServiceUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        _connectivity = connectivity;
        _cacheBarrel = cacheBarrel;

        AddDeviceInfoHeader();
    }

    protected void AddHttpHeader(string key, string value) => _httpClient.DefaultRequestHeaders.Add(key, value);

    private void AddDeviceInfoHeader()
    {
        var info = DeviceInfo.Current;
        var infoSting = $"{info.Idiom} {info.Manufacturer} {info.Model} on {info.Platform}";
        AddHttpHeader("device-name", infoSting);
    }

    private void TryAddAuthHeader(string resource)
    {
        if (!resource.Contains("token") && !resource.Contains("logout") && !resource.Contains("registration") && !resource.Contains("login"))
        {
            var token = GetTokenFromCache();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

    #region Http CRUD methods

    protected async Task<Response<T>> GetAsync<T>(string resource, int cacheDuration = 0, bool tryRefresh = true, CancellationToken token = default) where T : class
    {
        //Проверка данных в кэше
        if (cacheDuration > 0)
        {
            var cacheResult = GetFromCache<Response<T>>(resource);
            if (cacheResult is not null)
            {
                return cacheResult;
            }
        }

        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return new Response<T>(State.NoInternet);
        }

        //Получение данных из API
        TryAddAuthHeader(resource);
        var httpResponse = await _httpClient.GetAsync(resource, token);
        var result = await ToResponse<T>(httpResponse, token);

        //Сохранение в кэш
        if (result.State == State.Success)
        {
            SaveToCache(resource, result, cacheDuration);
        }

        //Если Unauthorized, то попытка обновить токен
        if (tryRefresh && result.State == State.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync();
            if (refreshSuccess)
            {
                return await GetAsync<T>(resource, cacheDuration, false, token);
            }
        }

        return result;
    }

    protected async Task<Response<T>> SendAsync<T, TRequest>(string resource, TRequest data, SendType type = SendType.Post,
         bool tryRefresh = true, CancellationToken token = default)
        where T : class
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return new Response<T>(State.NoInternet);
        }

        TryAddAuthHeader(resource);
        var httpResponse = await PostOrPutAsync(resource, data, type, token);
        var result = await ToResponse<T>(httpResponse, token);

        //Если Unauthorized, то попытка обновить токен
        if (tryRefresh && !resource.Contains("refresh") && !resource.Contains("registration") && !resource.Contains("login") && result.State == State.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync();
            if (refreshSuccess)
            {
                return await SendAsync<T, TRequest>(resource, data, type, false, token);
            }
        }

        return result;
    }

    protected async Task<IResponse> SendAsync<TRequest>(string resource, TRequest data, SendType type = SendType.Post,
        bool tryRefresh = true, CancellationToken token = default)
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return new Response<string>(State.NoInternet);
        }

        TryAddAuthHeader(resource);
        var httpResponse = await PostOrPutAsync(resource, data, type, token);
        var result = await ToResponse(httpResponse, token);

        //Если Unauthorized, то попытка обновить токен
        if (tryRefresh && !resource.Contains("refresh") && !resource.Contains("registration") && !resource.Contains("login") && result.State == State.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync();
            if (refreshSuccess)
            {
                return await SendAsync(resource, data, type, false, token);
            }
        }

        return result;
    }

    protected async Task<IResponse> DeleteAsync(string resource, CancellationToken token = default)
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return new Response<string>(State.NoInternet);
        }

        TryAddAuthHeader(resource);
        var httpResponse = await _httpClient.DeleteAsync(resource, token);
        return await ToResponse(httpResponse, token);
    }

    #endregion

    protected void SaveTokensToCache(string token, string refreshToken, DateTime tokenExpiresDate)
    {
        if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken))
        {
            SaveToCache(Constants.TOKEN_KEY, token, int.MaxValue);
            SaveToCache(Constants.TOKEN_EXPIRES_KEY, tokenExpiresDate, int.MaxValue);
            SaveToCache(Constants.REFRESH_TOKEN_KEY, refreshToken, int.MaxValue);
        }
    }
    protected string GetTokenFromCache() => _cacheBarrel.Get<string>(Constants.TOKEN_KEY);
    protected string GetRefreshTokenFromCache() => _cacheBarrel.Get<string>(Constants.REFRESH_TOKEN_KEY);
    protected void DeleteTokensFromCache()
    {
        _cacheBarrel.Empty(Constants.TOKEN_KEY);
        _cacheBarrel.Empty(Constants.REFRESH_TOKEN_KEY);
        _cacheBarrel.EmptyExpired();
    }

    #region Private cache methods

    private void SaveToCache<T>(string resource, T data, int cacheDuration)
    {
        if (cacheDuration > 0)
        {
            // максимум месяц
            if (cacheDuration > 720)
            {
                cacheDuration = 720;
            }

            try
            {
                var cleanCacheKey = resource.CleanCacheKey();
                _cacheBarrel.Add(cleanCacheKey, data, TimeSpan.FromHours(cacheDuration), _jsonSerializerOptions);
            }
            catch
            {
                // ignored
            }
        }
    }

    private T? GetFromCache<T>(string resource)
    {
        //TODO: чё за фигня?
        var cleanCacheKey = resource.CleanCacheKey();
        if (!_cacheBarrel.IsExpired(cleanCacheKey))
        {
            try
            {
                return _cacheBarrel.Get<T>(cleanCacheKey, _jsonSerializerOptions);
            }
            catch
            {
                //ignore
            }

        }
        return default;
    }

    protected void RemoveFromCache(string resource)
    {
        var cleanCacheKey = resource.CleanCacheKey();
        _cacheBarrel.Empty(cleanCacheKey);
        _cacheBarrel.EmptyExpired();
    }

    #endregion

    #region Private methods

    private async Task<HttpResponseMessage?> PostOrPutAsync<TRequest>(string url, TRequest value, SendType type = SendType.Post,
        CancellationToken token = default)
    {
        var data = JsonSerializer.Serialize(value, options: _jsonSerializerOptions);
        var content = new StringContent(data, Encoding.UTF8, "application/json");

        if (type == SendType.Put)
        {
            return await _httpClient.PutAsync(url, content, token);
        }

        return await _httpClient.PostAsync(url, content, token);
    }

    private async Task<bool> TryRefreshTokenAsync()
    {
        var token = GetTokenFromCache();
        var refreshToken = GetRefreshTokenFromCache();

        var result = await SendAsync<AuthResponse, RefreshTokenRequest>("user/refresh", new RefreshTokenRequest(token, refreshToken));
        if (result.State == State.Success && result.Item != null)
        {
            SaveTokensToCache(result.Item.Token, result.Item.RefreshToken, result.Item.TokenExpiresDate);
            return true;
        }

        DeleteTokensFromCache();
        return false;
    }

    private async Task<Response<T>> ToResponse<T>(HttpResponseMessage? httpResponse, CancellationToken token = default)
    {
        // успешно
        if (httpResponse?.IsSuccessStatusCode ?? false)
        {
            var jsonStream = await httpResponse.Content.ReadAsStreamAsync(token);
            var result = await JsonSerializer.DeserializeAsync<T>(jsonStream, _jsonSerializerOptions, token);
            if (result is not null)
            {
                return new Response<T>(result);
            }
        }

        // ошибки и валидация
        else if (httpResponse is not null)
        {
            var json = await httpResponse.Content.ReadAsStringAsync(token);
            var errors = ParseErrors(json);
            return new Response<T>(httpResponse.ResponseHandler(), errors);
        }

        return new Response<T>(httpResponse.ResponseHandler());
    }

    private static async Task<IResponse> ToResponse(HttpResponseMessage? httpResponse, CancellationToken token = default)
    {
        // успешно
        if (httpResponse?.IsSuccessStatusCode ?? false)
        {
            return new Response<string>(State.Success);
        }

        // ошибки и валидация
        else if (httpResponse is not null)
        {
            var json = await httpResponse.Content.ReadAsStringAsync(token);
            var errors = ParseErrors(json);
            return new Response<string>(httpResponse.ResponseHandler(), errors);
        }

        return new Response<string>(httpResponse.ResponseHandler());
    }

    private static IEnumerable<string> ParseErrors(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return new List<string>();
        }

        List<string> messages = new();
        var doc = JsonDocument.Parse(json);
        var hasErrors = doc.RootElement.TryGetProperty("errors", out var errors);
        if (hasErrors)
        {
            var en = errors.EnumerateObject();
            foreach (var item in en)
            {
                if (item.Value.ValueKind == JsonValueKind.Array)
                {
                    var arr = item.Value.EnumerateArray();
                    foreach (var a in arr)
                    {
                        var msg = a.GetString();
                        if (!string.IsNullOrEmpty(msg))
                        {
                            messages.Add(msg);
                        }
                    }
                }
                else
                {
                    var msg = item.Value.GetString();
                    if (!string.IsNullOrEmpty(msg))
                    {
                        messages.Add(msg);
                    }
                }
            }
        }

        return messages;
    }

    #endregion
}

internal static class Extensions
{
    internal static State ResponseHandler(this HttpResponseMessage? httpResponse)
    {
        if (httpResponse is null || httpResponse.StatusCode == HttpStatusCode.InternalServerError)
        {
            return State.Error;
        }

        return httpResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => State.NotFound,
            HttpStatusCode.Unauthorized => State.Unauthorized,
            _ => State.Error
        };
    }
}