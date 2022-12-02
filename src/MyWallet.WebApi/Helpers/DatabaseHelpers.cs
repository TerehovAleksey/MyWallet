namespace MyWallet.WebApi.Helpers;

public static class DatabaseHelpers
{
    public static async Task DatabaseMigrate(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }

    public static async Task DatabaseSeedInitData(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new Role() { Name = "Admin" });
            await roleManager.CreateAsync(new Role() { Name = "User" });
        }

        // var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        // if (!userManager.Users.Any())
        // {
        //     var user = new User
        //     {
        //         Email = "admin@mail.com",
        //         AccessFailedCount= 0,
        //         Id = Guid.NewGuid(),
        //         NormalizedEmail = "ADMIN@MAIL.COM",
        //         UserName = "Admin",
        //         NormalizedUserName = "ADMIN"
        //     };
        //   await userManager.CreateAsync(user, "Admin123_");
        //   await userManager.AddToRoleAsync(user, "User");
        // }

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (!db.AccountTypes.Any())
        {
            db.AccountTypes.AddRange(new List<AccountType>
            {
                new() { Id = Guid.NewGuid(), Name = "Общий", Order = 0 },
                new() { Id = Guid.NewGuid(), Name = "Наличные", Order = 1 },
                new() { Id = Guid.NewGuid(), Name = "Текущий счёт", Order = 2 },
                new() { Id = Guid.NewGuid(), Name = "Кредитная карточка", Order = 3 },
                new() { Id = Guid.NewGuid(), Name = "Сберегательный счёт", Order = 4 },
                new() { Id = Guid.NewGuid(), Name = "Бонус", Order = 5 },
                new() { Id = Guid.NewGuid(), Name = "Страхование", Order = 6 },
                new() { Id = Guid.NewGuid(), Name = "Инвестиции", Order = 7 },
                new() { Id = Guid.NewGuid(), Name = "Заём", Order = 8 },
                new() { Id = Guid.NewGuid(), Name = "Ипотека", Order = 9 },
                new() { Id = Guid.NewGuid(), Name = "Счёт с овердрафтом", Order = 10 }
            });
            await db.SaveChangesAsync();
        }

        if (!db.CategoryTypes.Any())
        {
            db.CategoryTypes.AddRange(new List<CategoryType>
            {
                new() { Id = Guid.NewGuid(), Name = "Обязательные"},
                new() { Id = Guid.NewGuid(), Name = "Необходимые"},
                new() { Id = Guid.NewGuid(), Name = "Желаемые"}
            });
            await db.SaveChangesAsync();
        }
    }
}