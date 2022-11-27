namespace MyWallet.Client.Models;

public class Record
{
    public Guid Id { get; set; }
    public string Category { get; set; } = default!;

    [JsonPropertyName("subcategory")]
    public string Title { get; set; } = default!;
    public string CurrencyType { get; set; } = default!;
    public string Image { get; set; } = default!;
    public decimal Value { get; set; }
    public string Currency { get; set; } = default!;

    [JsonPropertyName("dateOfCreation")]
    public DateTime DateOfRecord { get; set; }
    public string? Description { get; set; }
    public bool IsIncome { get; set; }

    public override string ToString() =>
        $"{(IsIncome ? string.Empty : "-")}{Value} {CurrencyType?.ToUpperInvariant()}";
}

public class RecordCreate
{
    public Guid SubcategoryId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime? DateTime { get; set; }
}
