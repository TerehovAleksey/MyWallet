namespace MyWallet.Client.Models;

public class Record
{
    public Guid Id { get; set; }
    public string Category { get; set; } = default!;
    public string Subcategory { get; set; } = default!;
    public decimal Value { get; set; }

    [JsonPropertyName("dateOfCreation")]
    public DateTime DateOfRecord { get; set; }
    public string? Description { get; set; }
    public bool IsIncome { get; set; }
    public string? CurrencySymbol { get; set; }
    public string? Account { get; set; }
    public string Image { get; set; } = default!;

    [JsonPropertyName("color")]
    public string ColorString { get; set; } = default!;

    [JsonIgnore]
    public Color Color => Color.FromArgb(ColorString);

    public override string ToString() =>
        $"{(IsIncome ? string.Empty : "-")}{Value} {CurrencySymbol?.ToUpperInvariant()}";
}

public class RecordCreate
{
    public Guid AccountId { get; set; }
    public Guid SubcategoryId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime? DateTime { get; set; }
}
