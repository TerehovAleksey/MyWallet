namespace MyWallet.Core;

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
}
