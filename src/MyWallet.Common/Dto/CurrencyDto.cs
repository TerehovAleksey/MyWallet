namespace MyWallet.Common.Dto;

public record CurrencyDto(string Symbol, string Description);
public record UserCurrencyDto
{
    public string Symbol { get; set; } = default!;
    public bool IsMain { get; set; }
}
