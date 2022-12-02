namespace MyWallet.Common.Validators.User;

public class PasswordChangeDtoValidator : AbstractValidator<PasswordChangeDto>
{
    public PasswordChangeDtoValidator()
    {
        RuleFor(x=>x.OldPassword)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage(Strings.UppercaseValidationMessage)
            .Matches(@"[a-z]+").WithMessage(Strings.LowecaseValidationMessage)
            .Matches(@"[0-9]+").WithMessage(Strings.NumberValidationMessage)
            .Matches(@"[\!\?\*\._]+").WithMessage(Strings.SpecialCharValidationMessage)
            .WithName(Strings.OldPassword);
        
        RuleFor(x=>x.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage(Strings.UppercaseValidationMessage)
            .Matches(@"[a-z]+").WithMessage(Strings.LowecaseValidationMessage)
            .Matches(@"[0-9]+").WithMessage(Strings.NumberValidationMessage)
            .Matches(@"[\!\?\*\._]+").WithMessage(Strings.SpecialCharValidationMessage)
            .WithName(Strings.NewPassword);
        
        RuleFor(x => x.NewPasswordConfirm)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage(Strings.UppercaseValidationMessage)
            .Matches(@"[a-z]+").WithMessage(Strings.LowecaseValidationMessage)
            .Matches(@"[0-9]+").WithMessage(Strings.NumberValidationMessage)
            .Matches(@"[\!\?\*\._]+").WithMessage(Strings.SpecialCharValidationMessage)
            .Equal(x => x.NewPassword)
            .WithName(Strings.PasswordConfirmation);
        
    }
}