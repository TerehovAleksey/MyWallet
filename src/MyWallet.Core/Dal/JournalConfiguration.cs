namespace MyWallet.Core.Dal;

internal class JournalConfiguration : IEntityTypeConfiguration<Journal>
{
    public void Configure(EntityTypeBuilder<Journal> builder)
    {
        builder.ToTable("Journals")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("BLOB");

        builder.Property(x => x.SubCategoryId)
            .HasColumnType("BLOB")
            .IsRequired();

        builder.Property(x => x.AccountId)
           .HasColumnType("BLOB");

        builder.Property(x => x.DateOfCreation)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(x => x.DateOfChange)
            .HasColumnType("TEXT");

        builder.Property(x => x.Description)
            .HasColumnType("TEXT")
            .HasMaxLength(250);

        builder.Property(x => x.Value)
            .HasColumnType("REAL")
            .IsRequired();
    }
}
