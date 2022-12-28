using System.Text.Json;

namespace MyWallet.WebApi.Services;

public class HttpService : IHttpService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public HttpService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
    }

    public async Task<DeviceLocationDto?> GetDeviceLocationAsync(string ip)
    {
        var token = _configuration.GetValue<string>("IpinfoToken");
        if (!string.IsNullOrEmpty(token))
        {
            var uri = new Uri($"https://ipinfo.io/{ip}?token={token}");
            _httpClient.DefaultRequestHeaders.Clear();
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DeviceLocationDto>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
        }
        return null;
    }

    public async Task<CurrencyExchangeDto?> GetExchange(string baseSymbol, string targetSymbol)
    {
        var token = _configuration.GetValue<string>("ApilayerToken");
        if (!string.IsNullOrEmpty(token))
        {
            var uri = new Uri($"https://api.apilayer.com/exchangerates_data/latest?symbols={targetSymbol}&base={baseSymbol}");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", token);
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var exchange = JsonSerializer.Deserialize<Exchange>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                if (exchange is not null)
                {
                    var date = exchange.Date.AddSeconds(exchange.Timestamp);
                    var result = new CurrencyExchangeDto(exchange.Base, targetSymbol, exchange.Rates[targetSymbol], date);
                    return result;
                }

            }
        }

        return null;
    }

    private class Exchange
    {
        public string Base { get; set; } = default!;
        public DateTime Date { get; set; } = default!;
        public double Timestamp { get; set; }
        public Dictionary<string, decimal> Rates { get; set; } = default!;
    }
}
