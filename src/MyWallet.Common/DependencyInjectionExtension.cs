namespace MyWallet.Common;

public static class DependencyInjectionExtension
{
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserForAuthDto>, UserForAuthDtoValidator>();
        services.AddScoped<IValidator<UserForRegistrationDto>, UserForRegistrationDtoValidator>();
        services.AddScoped<IValidator<PasswordChangeDto>, PasswordChangeDtoValidator>();
        services.AddScoped<IValidator<RefreshTokenDto>, RefreshTokenDtoValidator>();
    }
}
