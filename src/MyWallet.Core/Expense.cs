namespace MyWallet.Core;

public class Expense
{
    public Guid Id { get; set; }
    public DateTime DateOfCreation { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}
