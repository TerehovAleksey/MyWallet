using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyWallet.WpfClient.Http;

internal class MyWalletHttpClient
{
	private readonly HttpClient _httpClient;

	public MyWalletHttpClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
    }

	public async Task<T?> GetAsync<T>(string uri)
	{
		var response = await _httpClient.GetAsync(uri);
		var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(jsonResponse, options: new() { PropertyNameCaseInsensitive = true });
		return data;
    }
}
