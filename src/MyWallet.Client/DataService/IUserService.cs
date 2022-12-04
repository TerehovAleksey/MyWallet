namespace MyWallet.Client.DataService;

public interface IUserService
{
    public Task ChangePasswordAsync();
    public Task DeleteUserDataAsync();
    public Task GetUserDataAsync();

    /// <summary>
    /// Вход
    /// </summary>
    /// <param name="authData"></param>
    /// <returns></returns>
    public Task<IResponse> LoginAsync(UserAuthData authData);

    /// <summary>
    /// Выход
    /// </summary>
    /// <param name="serverLogout"></param>
    /// <returns></returns>
    public Task LogoutAsync(bool serverLogout = true);

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="registerData"></param>
    /// <returns></returns>
    public Task<IResponse> RegisterUserAsync(UserRegisterData registerData); 
    public Task UpdateUserDataAsync();
}
