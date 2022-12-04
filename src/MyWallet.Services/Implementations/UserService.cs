namespace MyWallet.Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task UpdateOrCreateDeviceAsync(Guid userId, string deviceName, string? lastIpAddress, string refreshToken, DateTime refreshTokenExpiryTime)
    {
        var device = await _context.UserDevices.FirstOrDefaultAsync(x => x.UserId == userId && x.DeviceName == deviceName);
        if (device is null)
        {
            device = new UserDevice
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DeviceName = deviceName,
                RefreshTokenExpiryTime = refreshTokenExpiryTime,
                RefreshToken = refreshToken,
                LastIpAddress = lastIpAddress,
                LastLoginDate = DateTime.Now
            };
            _context.UserDevices.Add(device);
            await _context.SaveChangesAsync();
        }
        else
        {
            //DeviceName и UserId не меняются
            device.RefreshTokenExpiryTime = refreshTokenExpiryTime;
            device.LastLoginDate = DateTime.Now;
            device.RefreshToken = refreshToken;
            device.LastIpAddress = lastIpAddress;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> DeleteDeviceAsync(Guid userId, string deviceName)
    {
        var device = await _context.UserDevices.FirstOrDefaultAsync(x => x.UserId == userId && x.DeviceName == deviceName);
        if (device is not null)
        {
            _context.UserDevices.Remove(device);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        return false;
    }

    public async Task<bool> CheckTokenAsync(Guid userId, string deviceName, string refreshToken)
    {
        var device = await _context.UserDevices.FirstOrDefaultAsync(x => x.UserId == userId && x.DeviceName == deviceName);
        if (device is not null)
        {
            if (device.RefreshToken == refreshToken && device.RefreshTokenExpiryTime > DateTime.UtcNow)
            {
                return true;
            }

            _context.UserDevices.Remove(device);
            await _context.SaveChangesAsync();
            return false;
        }

        return false;
    }
}