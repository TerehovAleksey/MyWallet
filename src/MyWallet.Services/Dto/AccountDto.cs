namespace MyWallet.Services.Dto;

public record AccountCreateDto(string Name, decimal Balance, string CurrencySymbol);
public record AccountDto(Guid Id, string Name, decimal Balance, string CurrencySymbol)
    : AccountCreateDto(Name, Balance, CurrencySymbol);
