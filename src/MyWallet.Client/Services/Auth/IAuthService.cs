namespace MyWallet.Client.Services.Auth;

public interface IAuthService
{
    /// <summary>
    /// Изменение пароля
    /// </summary>
    /// <returns></returns>
    public Task ChangePasswordAsync(PasswordChangeData data);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <returns></returns>
    public Task DeleteUserDataAsync();

    /// <summary>
    /// Получение информации о пользователе
    /// </summary>
    /// <returns></returns>
    public Task<UserData> GetUserDataAsync();

    /// <summary>
    /// Вход
    /// </summary>
    /// <param name="authData"></param>
    /// <returns></returns>
    public Task LoginAsync(UserAuthData authData);

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
    public Task RegisterUserAsync(UserRegisterData registerData); 

    /// <summary>
    /// Обновление информации о пользователе
    /// </summary>
    /// <returns></returns>
    public Task UpdateUserDataAsync(UserUpdateData data);

    /// <summary>
    /// Получение устройств пользователя, нв которых
    /// выполнен вход
    /// </summary>
    /// <returns></returns>
    public Task<List<UserDeviceDto>> GetUserDevicesAsync();

    /// <summary>
    /// Удаление устройства пользователя (выход на этом устройстве)
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public Task DeleteUserDevice(string deviceName);
}