namespace MyWallet.Core.Models;

/// <summary>
/// Валюта пользователя
/// </summary>
public class UserCurrency : BaseEntity
{
    /// <summary>
    /// Валюта (USD, BYN, RUB, etc.)
    /// </summary>
    public string CurrencySymbol { get; set; } = default!;

    /// <summary>
    /// Описание валюты
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Основная валюта.
    /// Создаётся при регистрации и не изменяется
    /// </summary>
    public bool IsMain { get; set; }

    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = default!;
}