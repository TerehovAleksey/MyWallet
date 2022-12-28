using MyWallet.Client.Services.Rest;
using RestServiceBase = MyWallet.Client.Services.Rest.RestServiceBase;

namespace MyWallet.Client.Services.Auth;

public class AuthService : RestServiceBase, IAuthService
{
    private const string USER_PROFILE_KEY = "user_profile";
    
    public AuthService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService) : base(httpClient, connectivity,
        storageService)
    {
    }
    
    public Task ChangePasswordAsync(PasswordChangeData data) =>
        SendAsync("user/password", data);

    public async Task DeleteUserDataAsync()
    {
        await DeleteAsync("user/profile").ConfigureAwait(false);
        await StorageService.DeleteToken().ConfigureAwait(false);
    }

    public async Task<UserData> GetUserDataAsync()
    {
        var data = await StorageService.LoadFromCache<UserData>(USER_PROFILE_KEY).ConfigureAwait(false);
        if (data is not null)
        {
            return data;
        }
        data = await GetAsync<UserData>("user/profile").ConfigureAwait(false);
        await StorageService.SaveToCache(USER_PROFILE_KEY, data, TimeSpan.FromDays(30)).ConfigureAwait(false);
        return data;
    }

    public async Task LoginAsync(UserAuthData authData)
    {
        var result = await SendAsync<AuthResponse, UserAuthData>("user/login", authData);
        await StorageService.SaveToken(result);
    }

    public async Task LogoutAsync(bool serverLogout = true)
    {
        if (serverLogout)
        {
            await SendAsync("user/logout", new{}, tryRefresh: false);
        }

        await StorageService.DeleteToken().ConfigureAwait(false);
    }

    public async Task RegisterUserAsync(UserRegisterData registerData)
    {
        var result = await SendAsync<AuthResponse, UserRegisterData>("user/registration", registerData);
        await StorageService.SaveToken(result);
    }

    public async Task UpdateUserDataAsync(UserUpdateData data)
    {
        await SendAsync("user/profile", data, SendType.Put);
        await StorageService.DeleteFromCache(USER_PROFILE_KEY);
    }

    public Task<List<UserDeviceDto>> GetUserDevicesAsync() => GetAsync<List<UserDeviceDto>>("user/devices");

    public Task DeleteUserDevice(string deviceName) => DeleteAsync($"user/devices/{deviceName}");
}