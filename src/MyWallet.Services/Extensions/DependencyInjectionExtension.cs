using Microsoft.Extensions.DependencyInjection;
using MyWallet.Services.Mapper;

namespace MyWallet.Services.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IExpenseService, ExpenseService>();
    }
}
