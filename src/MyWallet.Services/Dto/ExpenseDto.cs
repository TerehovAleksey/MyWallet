namespace MyWallet.Services.Dto;

public record ExpenseDto(Guid Id, string Category, decimal Value, DateTime DateOfCreation, string? Description);
public record ExpenseCreateDto(Guid CategoryId, decimal Value, string? Description, DateTime? DateTime);

