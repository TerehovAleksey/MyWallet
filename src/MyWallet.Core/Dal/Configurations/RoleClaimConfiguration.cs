namespace MyWallet.Core.Dal.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("RoleClaims")
            .HasKey(x => x.Id);

        builder.Property(x => x.RoleId)
            .HasColumnType("BLOB");
    }
}