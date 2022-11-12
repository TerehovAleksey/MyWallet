namespace MyWallet.Client.Models;

public class RecordCreate
{
    public Guid SubcategoryId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime? DateTime { get; set; }
}
