namespace MyWallet.Core.Models;

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
    /// ID пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = default!;

    /// <summary>
    /// Подкатегории в категории
    /// </summary>
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
