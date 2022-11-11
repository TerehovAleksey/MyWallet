namespace MyWallet.Core;

/// <summary>
/// Подкатегории
/// </summary>
public sealed class SubCategory : BaseCategory
{
    /// <summary>
    /// ID категории
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public Category Category { get; set; } = default!;

    /// <summary>
    /// Записи о доходах/расходах
    /// </summary>
    public ICollection<Journal> Journals { get; set; } = new List<Journal>();
}