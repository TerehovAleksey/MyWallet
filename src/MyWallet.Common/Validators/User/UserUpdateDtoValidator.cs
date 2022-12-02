namespace MyWallet.Common.Validators.User;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50);
        
        RuleFor(x => x.LastName)
            .MaximumLength(50);
    }    
}