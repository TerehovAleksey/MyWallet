namespace MyWallet.Core;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsVisible { get; set; } = true;
    public string? ImageName { get; set; }

    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
