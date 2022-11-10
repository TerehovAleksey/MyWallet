using Microsoft.EntityFrameworkCore;

namespace MyWallet.Core.Dal;

public sealed class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new JournalConfiguration());
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

    public DbSet<Category> Categories => Set<Category>();
	public DbSet<SubCategory> Subcategories => Set<SubCategory>();
    public DbSet<Journal> Journals => Set<Journal>();

    private void SetCreatedAndModified()
    {
        var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).DateOfCreation = DateTime.UtcNow;
            }

            ((BaseEntity)entity.Entity).DateOfChange = DateTime.UtcNow;
        }
    }
}
