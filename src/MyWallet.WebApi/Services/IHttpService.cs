namespace MyWallet.WebApi.Services;

public interface IHttpService
{
    public Task<DeviceLocationDto?> GetDeviceLocationAsync(string ip);
    public Task<CurrencyExchangeDto?> GetExchange(string baseSymbol, string targetSymbol);
}
