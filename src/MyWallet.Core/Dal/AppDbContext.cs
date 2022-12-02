namespace MyWallet.Core.Dal;

public sealed partial class AppDbContext : IdentityDbContext<User, Role, Guid>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
        
        #region Identity
        
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        #endregion Identity
        
        #region Configurations

        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountTypeConfigurations());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JournalConfiguration());
        modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserCurrencyConfiguration());
        
        #endregion Configurations
    }

    public override int SaveChanges()
    {
        SetCreatedAndModified();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetCreatedAndModified();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetCreatedAndModified();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void SetCreatedAndModified()
    {
        var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            var e = (BaseEntity)entity.Entity;

            switch (entity.State)
            {
                case EntityState.Added when e.DateOfCreation == DateTime.MinValue:
                    e.DateOfCreation = DateTime.UtcNow;
                    break;
                case EntityState.Modified when (e.DateOfChange is null || e.DateOfChange == DateTime.MinValue):
                    e.DateOfChange = DateTime.UtcNow;
                    break;
            }
        }
    }
}
