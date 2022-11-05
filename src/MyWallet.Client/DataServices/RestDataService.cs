using Android.Webkit;
using MyWallet.Client.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MyWallet.Client.DataServices;

public class RestDataService : IDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public RestDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _baseAddress = "https://mywalletapi.azurewebsites.net";
        _url = $"{_baseAddress}/api";

        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<Category> CreateCategoryAsync(string name)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");
            return null;
        }

        try
        {
            //var json = JsonSerializer.Serialize(name, _jsonSerializerOptions);
            var content = new StringContent(name, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_url}/category", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Category>(responseContent, _jsonSerializerOptions);
                return result;
            }
            else
            {
                Debug.WriteLine("---> Non Http 2xx response...");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"---> Whoops exception: {ex.Message}");
        }

        return null;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        List<Category> categories = new();

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");
            return categories;
        }

        try
        {
            var response = await _httpClient.GetAsync($"{_url}/category");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<List<Category>>(content, _jsonSerializerOptions);
            }
            else
            {
                Debug.WriteLine("---> Non Http 2xx response...");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"---> Whoops exception: {ex.Message}");
        }

        return categories;
    }
}
