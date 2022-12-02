namespace MyWallet.Common.Validators.User;

public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
{
    public RefreshTokenDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();
        
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}