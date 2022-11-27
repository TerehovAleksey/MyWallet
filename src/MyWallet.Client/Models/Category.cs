namespace MyWallet.Client.Models;

public class Category : BaseCategory
{
    public bool IsIncome { get; set; }
    public List<BaseCategory> Subcategories { get; set; } = new();
}
