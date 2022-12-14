namespace MyWallet.Core.Dal.Configurations;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("Subcategories")
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

        builder.Property(x => x.ImageName)
            .HasColumnType("TEXT")
            .HasMaxLength(100);

        builder.Property(x => x.IsVisible)
            .HasColumnType("INTEGER")
            .IsRequired();
        
        builder.Property(x => x.Color)
            .HasColumnType("TEXT")
            .HasMaxLength(10)
            .IsRequired();
    }
}