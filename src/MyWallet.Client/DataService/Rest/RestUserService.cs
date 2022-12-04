namespace MyWallet.Client.DataService.Rest;

public class RestUserService : RestServiceBase, IUserService
{
    public RestUserService(HttpClient httpClient, IConnectivity connectivity, IBarrel cacheBarrel) : base(httpClient, connectivity, cacheBarrel)
    {
    }

    public Task ChangePasswordAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetUserDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IResponse> LoginAsync(UserAuthData authData)
    {
        var result = await SendAsync<AuthResponse, UserAuthData>("user/login", authData);
        await SaveTokenOrLogout(result, false);
        return result;
    }

    public async Task LogoutAsync(bool serverLogout = true)
    {
        if (serverLogout)
        {
            await GetAsync<string>("user/logout");
        }

        DeleteTokensFromCache();
    }

    public Task<IResponse> RegisterUserAsync(UserRegisterData registerData) => SendAsync("user/registration", registerData);

    public Task UpdateUserDataAsync()
    {
        throw new NotImplementedException();
    }

    private async Task SaveTokenOrLogout(Response<AuthResponse> response, bool serverLogout = true)
    {
        if (response.State == State.Success && !string.IsNullOrEmpty(response.Item?.Token) && !string.IsNullOrEmpty(response.Item?.RefreshToken))
        {
            SaveTokensToCache(response.Item.Token, response.Item.Token);
        }
        else
        {
            await LogoutAsync(serverLogout);
        }
    }
}
