namespace MyWallet.Core.Models;

/// <summary>
/// Пользователь приложения
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    public DateTime? BirthdayDate { get; set; }

    /// <summary>
    /// Пол (0 - не указано, 1 - мужской, 2 - женский)
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// Устройства пользователя
    /// </summary>
    public ICollection<UserDevice> UserDevices { get; set; } = new List<UserDevice>();
    
    /// <summary>
    /// Валюты пользователя
    /// </summary>
    public ICollection<UserCurrency> UserCurrencies { get; set; } = new List<UserCurrency>();
    
    /// <summary>
    /// Счета пользователя
    /// </summary>
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    
    /// <summary>
    /// Категории пользователя
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}