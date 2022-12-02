namespace MyWallet.Common.Validators.User;

public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
{
    public UserForRegistrationDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithName(Strings.Email);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage(Strings.UppercaseValidationMessage)
            .Matches(@"[a-z]+").WithMessage(Strings.LowecaseValidationMessage)
            .Matches(@"[0-9]+").WithMessage(Strings.NumberValidationMessage)
            .Matches(@"[\!\?\*\._]+").WithMessage(Strings.SpecialCharValidationMessage)
            .WithName(Strings.Password);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage(Strings.UppercaseValidationMessage)
            .Matches(@"[a-z]+").WithMessage(Strings.LowecaseValidationMessage)
            .Matches(@"[0-9]+").WithMessage(Strings.NumberValidationMessage)
            .Matches(@"[\!\?\*\._]+").WithMessage(Strings.SpecialCharValidationMessage)
            .Equal(x => x.Password)
            .WithName(Strings.PasswordConfirmation);
    }
}