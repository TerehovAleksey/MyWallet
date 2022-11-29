namespace MyWallet.Core;

/// <summary>
/// Счёт
/// </summary>
public class Account : BaseEntity
{
    /// <summary>
    /// Название счёта
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Номер банковского счёта
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Текущий баланс
    /// </summary>
    public decimal Balance { get; set; } = decimal.Zero;

    /// <summary>
    /// Валюта (USD, BYN, RUB, etc.)
    /// </summary>
    public string CurrencySymbol { get; set; } = default!;

    /// <summary>
    /// Цает счёта
    /// </summary>
    public string Color { get; set; } = default!;

    /// <summary>
    /// Исключён из статистики
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// В архиве
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// ID типа счёта
    /// </summary>
    public Guid AccountTypeId { get; set; }

    /// <summary>
    /// Тип счёта
    /// </summary>
    public AccountType AccountType { get; set; } = default!;

    /// <summary>
    /// Список доходов/расходов по счёту
    /// </summary>
    public ICollection<Journal> Journals { get; set; } = new List<Journal>();
}
