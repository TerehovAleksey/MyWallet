namespace MyWallet.Client.Models;

public record CurrencyDto(string Symbol, string Description)
{
    public override string ToString() => $"{Symbol}{(string.IsNullOrEmpty(Description) ? "" : " - " + Description)}";
}

public record UserCurrencyDto(string Symbol, string Description, bool IsMain)
{
    public override string ToString() => $"{Symbol}{(string.IsNullOrEmpty(Description) ? "" : " - " + Description)}";
}

public record CurrencyRates(string BaseSymbol, string TargetSymbol, decimal BaseRate, decimal TargetRate);
public class CurrencyExchangeDto
{
    public string Base { get; set; } = default!;
    public string Target { get; set; } = default!;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}
