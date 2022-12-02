namespace MyWallet.Common.Dto;

public record RecordDto
{
    public Guid Id { get; set; }
    public string Category { get; set; } = default!;
    public string Subcategory { get; set; } = default!;
    public decimal Value { get; set; }
    public DateTime DateOfCreation { get; set; }
    public string? Description { get; set; }
    public bool IsIncome { get; set; }
    public string? CurrencySymbol { get; set; }
    public string? Account { get; set; }
}

public record RecordCreateDto
{
    public Guid SubcategoryId { get; set; }
    public Guid AccountId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime? DateTime { get; set; }
}
public record RecordUpdateDto(Guid Id, Guid SubcategoryId, decimal Value, string? Description);
