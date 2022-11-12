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

    public async Task<Category> CreateCategoryAsync(string name, string imageName)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");
            return null;
        }

        try
        {
            var json = JsonSerializer.Serialize(new CategoryCreate { Name = name, ImageName = imageName }, _jsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
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

    public async Task<List<Record>> GetRecordsAsync(DateTime startDate, DateTime endDate)
    {
        List<Record> records = new();

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");
            return records;
        }

        try
        {
            var uri = $"{_url}/journal?startDate={startDate.ToString("yyyy-MM-dd")}&finishDate={endDate.ToString("yyyy-MM-dd")}";
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                records = JsonSerializer.Deserialize<List<Record>>(content, _jsonSerializerOptions);
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

        return records;
    }

    public async Task<Record> CreateRecordAsync(RecordCreate record)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");
            return null;
        }

        try
        {
            var json = JsonSerializer.Serialize(record, _jsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_url}/journal", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Record>(responseContent, _jsonSerializerOptions);
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

    private class CategoryCreate
    {
        public string Name { get; set; } = default!;
        public string ImageName { get; set; } = default!;
    }
}
