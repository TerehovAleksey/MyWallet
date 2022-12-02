namespace MyWallet.Core.Dal.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.ToTable("UserRoles");
        
        builder.Property(x => x.UserId)
            .HasColumnType("BLOB");

        builder.Property(x => x.RoleId)
            .HasColumnType("BLOB");
    }
}