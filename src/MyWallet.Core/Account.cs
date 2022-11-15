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
    /// Текущий баланс
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Валюта (USD, BYN, RUB, etc.)
    /// </summary>
    public string CurrencySymbol { get; set; } = default!;

    /// <summary>
    /// Список доходов/расходов по счёту
    /// </summary>
    public ICollection<Journal> Journals { get; set; } = new List<Journal>();
}
