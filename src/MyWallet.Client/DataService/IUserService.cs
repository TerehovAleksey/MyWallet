namespace MyWallet.Client.DataService;

public interface IUserService
{
    /// <summary>
    /// Изменение пароля
    /// </summary>
    /// <returns></returns>
    public Task<IResponse> ChangePasswordAsync(PasswordChangeData data);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <returns></returns>
    public Task<IResponse> DeleteUserDataAsync();

    /// <summary>
    /// Получение информации о пользователе
    /// </summary>
    /// <returns></returns>
    public Task<Response<UserData>> GetUserDataAsync();

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
    public Task<IResponse> LogoutAsync(bool serverLogout = true);

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="registerData"></param>
    /// <returns></returns>
    public Task<IResponse> RegisterUserAsync(UserRegisterData registerData); 

    /// <summary>
    /// Обновление информации о пользователе
    /// </summary>
    /// <returns></returns>
    public Task<IResponse> UpdateUserDataAsync(UserUpdateData data);
}
