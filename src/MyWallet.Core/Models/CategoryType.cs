namespace MyWallet.Core.Models;

/// <summary>
/// Характер категории/подкатегории
/// </summary>
public class CategoryType : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Категории
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    
    /// <summary>
    /// Подкатегории
    /// </summary>
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}