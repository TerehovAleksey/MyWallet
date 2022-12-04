using System.Net;
using System.Net.Http.Headers;

namespace MyWallet.Client.DataService.Base;

public abstract class RestServiceBase
{
    private const string TOKEN_KEY = "token";
    private const string REFRESH_TOKEN_KEY = "refreshToken";

    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IConnectivity _connectivity;
    private readonly IBarrel _cacheBarrel;

    protected RestServiceBase(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.ApiServiceUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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
        if (resource is not null && !resource.Contains("token") && !resource.Contains("logout") && !resource.Contains("registration") && !resource.Contains("login") &&
            _httpClient.DefaultRequestHeaders.Authorization is null)
        {
            var token = GetTokenFromCache();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

    #region Http CRUD methods

    protected async Task<Response<T>> GetAsync<T>(string resource, int cacheDuration = 0, CancellationToken token = default) where T : class
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
            SaveToCache(result, resource, cacheDuration);
        }

        return result;
    }

    protected async Task<Response<T>> SendAsync<T, TRequest>(string resource, TRequest data, SendType type = SendType.Post,
        CancellationToken token = default)
        where T : class
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return new Response<T>(State.NoInternet);
        }

        TryAddAuthHeader(resource);
        var httpResponse = await PostOrPutAsync(resource, data, type, token);
        return await ToResponse<T>(httpResponse, token);
    }

    protected async Task<IResponse> SendAsync<TRequest>(string resource, TRequest data, SendType type = SendType.Post,
        CancellationToken token = default)
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return new Response<string>(State.NoInternet);
        }

        TryAddAuthHeader(resource);
        var httpResponse = await PostOrPutAsync(resource, data, type, token);
        return await ToResponse(httpResponse, token);
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

    protected void SaveTokensToCache(string token, string refreshToken)
    {
        if (!string.IsNullOrEmpty(token))
        {
            SaveToCache(TOKEN_KEY, token, int.MaxValue);
        }
        if (!string.IsNullOrEmpty(refreshToken))
        {
            SaveToCache(REFRESH_TOKEN_KEY, refreshToken, int.MaxValue);
        }
    }
    protected string GetTokenFromCache() => _cacheBarrel.Get<string>(TOKEN_KEY);
    protected string GetRefreshTokenFromCache() => _cacheBarrel.Get<string>(REFRESH_TOKEN_KEY);
    protected void DeleteTokensFromCache()
    {
        _cacheBarrel.Empty(TOKEN_KEY);
        _cacheBarrel.Empty(REFRESH_TOKEN_KEY);
        _cacheBarrel.EmptyExpired();
    }

    #region Private cache methods

    private void SaveToCache<T>(T data, string resource, int cacheDuration)
    {
        if (cacheDuration > 0)
        {
            try
            {
                var cleanCacheKey = resource.CleanCacheKey();
                _cacheBarrel.Add(cleanCacheKey, data, TimeSpan.FromHours(cacheDuration));
            }
            catch
            {
                // ignored
            }
        }
    }

    private T? GetFromCache<T>(string resource)
    {
        var cleanCacheKey = resource.CleanCacheKey();
        return !_cacheBarrel.IsExpired(cleanCacheKey) ? _cacheBarrel.Get<T>(cleanCacheKey) : default;
    }

    private void RemoveFromCache(string resource)
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