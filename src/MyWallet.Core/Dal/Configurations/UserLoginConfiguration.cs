namespace MyWallet.Core.Dal.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("UserLogins");

        builder.Property(x => x.UserId)
            .HasColumnType("BLOB");
    }
}