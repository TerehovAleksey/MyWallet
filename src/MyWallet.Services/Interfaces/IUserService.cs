namespace MyWallet.Services.Interfaces;

public interface IUserService
{
    public Task UpdateOrCreateDeviceAsync(Guid userId, string deviceName, string? lastIpAddress, 
        string refreshToken, DateTime refreshTokenExpiryTime);

    public Task<List<UserDeviceDto>> GetUserDevicesAsync(Guid userId);

    public Task<bool> DeleteDeviceAsync(Guid userId, string deviceName);

    /// <summary>
    /// Проверяет токен обновления на валидность,
    /// если не валиден, то запись будет удалена (выход)
    /// </summary>
    /// <returns>true если валиден, false - произведён выход или запись не найдена</returns>
    public Task<bool> CheckTokenAsync(Guid userId, string deviceName, string refreshToken);
}