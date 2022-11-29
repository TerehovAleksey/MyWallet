namespace MyWallet.Core.Dal;

internal class AccountTypeConfigurations : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.ToTable("AccountTypes")
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

        builder.Property(x => x.Order)
            .HasColumnType("INTEGER")
            .IsRequired();
    }
}
