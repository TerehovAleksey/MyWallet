namespace MyWallet.Common.Dto;

public record CurrencyDto(string Symbol, string Description);
public record UserCurrencyDto
{
    public string Symbol { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsMain { get; set; }
}
public record CurrencyExchangeDto(string Base, string Target, decimal Value, DateTime Date);
