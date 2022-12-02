namespace MyWallet.Core.Dal.Configurations;

public class UserCurrencyConfiguration : IEntityTypeConfiguration<UserCurrency>
{
    public void Configure(EntityTypeBuilder<UserCurrency> builder)
    {
        builder.ToTable("UserCurrencies")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("BLOB");

        builder.Property(x => x.DateOfCreation)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(x => x.DateOfChange)
            .HasColumnType("TEXT");

        builder.Property(x => x.CurrencySymbol)
            .HasColumnType("TEXT")
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(x => x.IsMain)
            .HasColumnType("INTEGER")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnType("BLOB")
            .IsRequired();
    }
}