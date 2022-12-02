namespace MyWallet.Core.Dal.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable("UserTokens");
            
        builder.Property(x => x.UserId)
            .HasColumnType("BLOB");
    }
}