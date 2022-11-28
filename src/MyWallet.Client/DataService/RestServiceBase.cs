﻿using MonkeyCache;
using System.Text;

namespace MyWallet.Client.DataService;

public abstract class RestServiceBase
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IConnectivity _connectivity;
    private readonly IBarrel _cacheBarrel;

    public RestServiceBase(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.ApiServiceUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        _connectivity = connectivity;
        _cacheBarrel = cacheBarrel;
    }

    protected void AddHttpHeader(string key, string value) => _httpClient.DefaultRequestHeaders.Add(key, value);

    protected async Task<T> GetAsync<T>(string resource, int cacheDuration = 24)
    {
        //Get Json data (from Cache or Web)
        var json = await GetJsonAsync(resource, cacheDuration);

        //Return the result
        return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }

    protected async Task<HttpResponseMessage> PostAsync<T>(string uri, T payload)
    {
        var dataToPost = JsonSerializer.Serialize(payload, _jsonSerializerOptions);
        var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        return response;
    }

    protected async Task<HttpResponseMessage> PutAsync<T>(string uri, T payload)
    {
        var dataToPost = JsonSerializer.Serialize(payload, _jsonSerializerOptions);
        var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(uri, content);

        response.EnsureSuccessStatusCode();

        return response;
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string uri)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(new Uri(_httpClient.BaseAddress, uri));

        response.EnsureSuccessStatusCode();

        return response;
    }

    private async Task<string> GetJsonAsync(string resource, int cacheDuration = 24)
    {
        var cleanCacheKey = resource.CleanCacheKey();

        //Check if Cache Barrel is enabled
        if (_cacheBarrel is not null)
        {
            //Try Get data from Cache
            var cachedData = _cacheBarrel.Get<string>(cleanCacheKey);

            if (cacheDuration > 0 && cachedData is not null && !_cacheBarrel.IsExpired(cleanCacheKey))
            {
                return cachedData;
            }

            //Check for internet connection and return cached data if possible
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return cachedData is not null ? cachedData : throw new InternetConnectionException();
            }
        }

        //No Cache Found, or Cached data was not required, or Internet connection is also available
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new InternetConnectionException();
        }

        //Extract response from URI
        var response = await _httpClient.GetAsync(resource);

        //Check for valid response
        response.EnsureSuccessStatusCode();

        //Read Response
        string json = await response.Content.ReadAsStringAsync();

        //Save to Cache if required
        if (cacheDuration > 0 && _cacheBarrel is not null)
        {
            try
            {
                _cacheBarrel.Add(cleanCacheKey, json, TimeSpan.FromHours(cacheDuration));
            }
            catch { }
        }

        //Return the result
        return json;
    }
}