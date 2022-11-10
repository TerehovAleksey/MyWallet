namespace MyWallet.Core;

/// <summary>
/// Категории
/// </summary>
public sealed class Category : BaseCategory
{
    /// <summary>
    /// Флаг, означающий, что это категория доходов (true),
    /// или категория расходов (false) 
    /// </summary>
    public bool IsIncome { get; set; }

    /// <summary>
    /// Подкатегории в категории
    /// </summary>
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
