namespace MyWallet.Core.Dal.Configurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.ToTable("UserClaims")
            .HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .HasColumnType("BLOB");
    }
}