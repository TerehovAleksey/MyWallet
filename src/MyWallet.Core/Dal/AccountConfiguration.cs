namespace MyWallet.Core.Dal;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("BLOB");

        builder.Property(x => x.DateOfCreation)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(x => x.DateOfChange)
            .HasColumnType("TEXT");

        builder.Property(x => x.Name)
            .HasColumnType("TEXT")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Number)
            .HasColumnType("TEXT")
            .HasMaxLength(50);

        builder.Property(x => x.Balance)
            .HasColumnType("REAL")
            .IsRequired();

        builder.Property(x => x.CurrencySymbol)
            .HasColumnType("TEXT")
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.AccountTypeId)
            .HasColumnType("BLOB")
            .IsRequired();

        builder.Property(x => x.Color)
            .HasColumnType("TEXT")
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.IsDisabled)
            .HasColumnType("INTEGER")
            .IsRequired();

        builder.Property(x => x.IsArchived)
            .HasColumnType("INTEGER")
            .IsRequired();
    }
}