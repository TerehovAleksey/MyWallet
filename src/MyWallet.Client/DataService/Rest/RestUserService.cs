namespace MyWallet.Client.DataService.Rest;

public class RestUserService : RestServiceBase, IUserService
{
    public RestUserService(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel) : base(httpClient, connectivity, cacheBarrel)
    {
    }

    public Task<IResponse> ChangePasswordAsync(PasswordChangeData data) => SendAsync("user/password", data);

    public async Task<IResponse> DeleteUserDataAsync()
    {
        var result = await DeleteAsync("user/profile");
        if (result.State == State.Success)
        {
            DeleteTokensFromCache();
        }
        return result;
    }

    public Task<Response<UserData>> GetUserDataAsync() => GetAsync<UserData>("user/profile", 24);

    public async Task<IResponse> LoginAsync(UserAuthData authData)
    {
        var result = await SendAsync<AuthResponse, UserAuthData>("user/login", authData);
        await SaveTokenOrLogout(result, false);
        return result;
    }

    public async Task<IResponse> LogoutAsync(bool serverLogout = true)
    {
        IResponse result = new Response<string>(State.Success);

        if (serverLogout)
        {
            result = await GetAsync<string>("user/logout");
        }

        DeleteTokensFromCache();
        return result;
    }

    public async Task<IResponse> RegisterUserAsync(UserRegisterData registerData)
    {
        var result = await SendAsync<AuthResponse, UserRegisterData>("user/registration", registerData);
        await SaveTokenOrLogout(result, false);
        return result;
    }

    public async Task<IResponse> UpdateUserDataAsync(UserUpdateData data)
    {
        var result = await SendAsync("user/profile", data, SendType.Put);
        if (result.State == State.Success)
        {
            RemoveFromCache("user/profile");
        }
        return result;
    }

    private async Task SaveTokenOrLogout(Response<AuthResponse> response, bool serverLogout = true)
    {
        if (response.State == State.Success && !string.IsNullOrEmpty(response.Item?.Token) && !string.IsNullOrEmpty(response.Item?.RefreshToken))
        {
            SaveTokensToCache(response.Item.Token, response.Item.RefreshToken, response.Item.TokenExpiresDate);
        }
        else
        {
            await LogoutAsync(serverLogout);
        }
    }
}
