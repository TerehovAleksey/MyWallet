namespace MyWallet.Common.Validators.Currency;

public class CurrencyDtoValidator : AbstractValidator<CurrencyDto>
{
	public CurrencyDtoValidator()
	{
		RuleFor(x => x.Symbol)
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(3)
			.WithName(Strings.CurrencySymbol);
	}
}
