namespace MyWallet.Services.Dto;

public record AccountCreateDto(string Name, string? Number, Guid AccountTypeId, decimal Balance, string CurrencySymbol, string Color);
public record AccountUpdateDto(Guid Id, string Name, string? Number, Guid AccountTypeId, string Color, bool IsDisabled, bool IsArchived);

public record AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string AccountType { get; set; } = default!;
    public decimal Balance { get; set; }
    public string CurrencySymbol { get; set; } = default!;
    public string Color { get; set; } = default!;
    public bool IsDisabled { get; set; }
    public bool IsArchived { get; set; }
}
