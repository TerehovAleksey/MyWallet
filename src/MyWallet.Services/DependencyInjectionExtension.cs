namespace MyWallet.Services;

public static class DependencyInjectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));

        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IAccountTypeService, AccountTypeService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICurrencyService, CurrencyService>();
        services.AddTransient<IRecordService, RecordService>();
        services.AddTransient<IUserService, UserService>();
    }
}
