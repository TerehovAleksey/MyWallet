namespace MyWallet.Core.Dal.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles")
            .HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnType("BLOB");

        builder.Property(x => x.ConcurrencyStamp)
            .HasMaxLength(1024);

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.NormalizedName)
            .HasMaxLength(50);
    }
}