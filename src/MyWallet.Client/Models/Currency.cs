namespace MyWallet.Client.Models;

public record Currency(string Symbol, string Description)
{
    public override string ToString() => $"{Symbol}{(string.IsNullOrEmpty(Description) ? "" : " - " + Description)}";
}
