using Microsoft.EntityFrameworkCore;
using MyWallet.Core.Dal;

namespace MyWallet.WebApi.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
        {
            var connectionString = env.IsDevelopment() ? configuration.GetConnectionString("DevConnection") : configuration.GetConnectionString("ProdConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}
