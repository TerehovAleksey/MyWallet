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

    public Task LoginAsync()
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task RegisterUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserDataAsync()
    {
        throw new NotImplementedException();
    }
}
