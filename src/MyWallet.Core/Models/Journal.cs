namespace MyWallet.Core.Models;

/// <summary>
/// Журнал доходов/расходов
/// </summary>
public class Journal : BaseEntity
{
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// ID подкатегории
    /// </summary>
    public Guid SubCategoryId { get; set; }

    /// <summary>
    /// Подкатегория
    /// </summary>
    public SubCategory SubCategory { get; set; } = default!;

    /// <summary>
    /// ID счёта
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Счёт
    /// </summary>
    public Account Account { get; set; } = default!;
}
