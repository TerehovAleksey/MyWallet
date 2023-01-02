namespace MyWallet.Client.Services.Rest;

public abstract class RestServiceBase
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IConnectivity _connectivity;

    protected IStorageService StorageService { get; }

    protected RestServiceBase(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService)
    {
        _connectivity = connectivity;
        StorageService = storageService;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.ApiServiceUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        AddDeviceInfoHeader();
    }

    /// <summary>
    /// Получение информации с API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resource"></param>
    /// <param name="tryRefresh">Будет ли произведена попытка обновить токен, если будет ошибка доступа</param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="InternetConnectionException"></exception>
    protected async Task<T> GetAsync<T>(string resource, bool tryRefresh = true, CancellationToken token = default) where T : class, new()
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new InternetConnectionException();
        }

        //Получение данных из API
        await TryAddAuthHeader(resource)
            .ConfigureAwait(false);
        var httpResponse = await _httpClient.GetAsync(resource, token)
            .ConfigureAwait(false);

        if (httpResponse.IsSuccessStatusCode)
        {
            var jsonStream = await httpResponse.Content.ReadAsStreamAsync(token)
                .ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<T>(jsonStream, _jsonSerializerOptions, token)
                .ConfigureAwait(false);
            return result ?? new T();
        }

        //Если Unauthorized, то пытаемся обновить токен

        if (tryRefresh && httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync()
                .ConfigureAwait(false);
            if (refreshSuccess)
            {
                return await GetAsync<T>(resource, false, token)
                    .ConfigureAwait(false);
            }
        }

        if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }

        httpResponse.EnsureSuccessStatusCode();
        return new();
    }

    protected async Task SendAsync<TRequest>(string resource, TRequest data, SendType type = SendType.Post,
         bool tryRefresh = true, CancellationToken token = default)
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new InternetConnectionException();
        }

        await TryAddAuthHeader(resource)
            .ConfigureAwait(false);
        var json = JsonSerializer.Serialize(data, options: _jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponse;

        if (type == SendType.Put)
        {
            httpResponse = await _httpClient.PutAsync(resource, content, token)
                .ConfigureAwait(false);
        }
        else
        {
            httpResponse = await _httpClient.PostAsync(resource, content, token)
                .ConfigureAwait(false);
        }

        if (httpResponse.IsSuccessStatusCode)
        {
            return;
        }

        //Если Unauthorized, то пытаемся обновить токен
        else if (tryRefresh && httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync()
                .ConfigureAwait(false);
            if (refreshSuccess)
            {
                await SendAsync(resource, data, type, false, token)
                   .ConfigureAwait(false);
            }
        }

        if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }

        httpResponse.EnsureSuccessStatusCode();
    }

    protected async Task<T> SendAsync<T, TRequest>(string resource, TRequest data, SendType type = SendType.Post,
         bool tryRefresh = true, CancellationToken token = default)
        where T : class
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new InternetConnectionException();
        }

        await TryAddAuthHeader(resource)
            .ConfigureAwait(false);
        var json = JsonSerializer.Serialize(data, options: _jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponse;

        if (type == SendType.Put)
        {
            httpResponse = await _httpClient.PutAsync(resource, content, token)
                .ConfigureAwait(false);
        }
        else
        {
            httpResponse = await _httpClient.PostAsync(resource, content, token)
                .ConfigureAwait(false);
        }


        if (httpResponse.IsSuccessStatusCode)
        {
            var jsonStream = await httpResponse.Content.ReadAsStreamAsync(token)
                .ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<T>(jsonStream, _jsonSerializerOptions, token)
                .ConfigureAwait(false);
            return result ?? throw new NoDataException($"Data not received from {resource}");
        }

        //Если Unauthorized, то пытаемся обновить токен
        else if (tryRefresh && httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync()
                .ConfigureAwait(false);
            if (refreshSuccess)
            {
                return await SendAsync<T, TRequest>(resource, data, type, false, token)
                    .ConfigureAwait(false);
            }
        }

        //если пришла ошибка с сервера
        if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync(token);
            IEnumerable<string> errors = ParseErrors(jsonResponse);
            if(errors.Any())
            {
                throw new ServerResponseException(errors.First());
            }
        }

        if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }

        httpResponse.EnsureSuccessStatusCode();
        throw new NoDataException($"Data not received from {resource}");
    }

    protected async Task DeleteAsync(string resource, bool tryRefresh = true, CancellationToken token = default)
    {
        //Проверка наличия интернет соединения
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new InternetConnectionException();
        }

        await TryAddAuthHeader(resource)
            .ConfigureAwait(false);
        var httpResponse = await _httpClient.DeleteAsync(resource, token)
            .ConfigureAwait(false);

        if (tryRefresh && httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshSuccess = await TryRefreshTokenAsync()
                .ConfigureAwait(false);
            if (refreshSuccess)
            {
                await DeleteAsync(resource, false, token)
                    .ConfigureAwait(false);
            }
        }

        httpResponse.EnsureSuccessStatusCode();
    }

    private void AddHttpHeader(string key, string value) => _httpClient.DefaultRequestHeaders.Add(key, value);

    private void AddDeviceInfoHeader()
    {
        if (_httpClient.DefaultRequestHeaders.Contains("device-name"))
        {
            return;
        }
        var info = DeviceInfo.Current;
        var infoSting = $"{info.Idiom} {info.Manufacturer} {info.Model} on {info.Platform}";
        AddHttpHeader("device-name", infoSting);
    }

    private async Task TryAddAuthHeader(string resource)
    {
        if (!resource.Contains("token") && !resource.Contains("logout") && !resource.Contains("registration") && !resource.Contains("login"))
        {
            var token = await StorageService.GetToken();
            if (!string.IsNullOrEmpty(token?.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
            }
        }
    }

    private async Task<bool> TryRefreshTokenAsync()
    {
        var token = await StorageService.GetToken()
            .ConfigureAwait(false);

        var result = await SendAsync<AuthResponse, RefreshTokenRequest>("user/refresh", new RefreshTokenRequest(token.Token, token.RefreshToken), tryRefresh: false)
            .ConfigureAwait(false);
        await StorageService.SaveToken(result)
            .ConfigureAwait(false);
        return true;
    }

    private static List<string> ParseErrors(string json)
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
}
