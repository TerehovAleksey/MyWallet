namespace MyWallet.Core.Dal;

public sealed partial class AppDbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<AccountType> AccountTypes => Set<AccountType>();
    public DbSet<CategoryType> CategoryTypes => Set<CategoryType>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<SubCategory> Subcategories => Set<SubCategory>();
    public DbSet<Journal> Journals => Set<Journal>();
    public DbSet<UserCurrency> UserCurrencies => Set<UserCurrency>();
    public DbSet<UserDevice> UserDevices => Set<UserDevice>();
}