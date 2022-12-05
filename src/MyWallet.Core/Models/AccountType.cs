namespace MyWallet.Core.Models;

/// <summary>
/// Тип счёта
/// </summary>
public class AccountType : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Иконка
    /// </summary>
    public string? ImageName { get; set; }

    /// <summary>
    /// Порядок отображения
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Счета данного типа
    /// </summary>
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}
