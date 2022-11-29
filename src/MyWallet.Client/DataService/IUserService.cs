namespace MyWallet.Client.DataService;

public interface IUserService
{
    public Task ChangePasswordAsync();
    public Task DeleteUserDataAsync();
    public Task GetUserDataAsync();
    public Task LoginAsync();
    public Task LogoutAsync();
    public Task RegisterUserAsync(); 
    public Task UpdateUserDataAsync();
}
