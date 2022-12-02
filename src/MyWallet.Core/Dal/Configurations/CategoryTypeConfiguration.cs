namespace MyWallet.Core.Dal.Configurations;

public class CategoryTypeConfiguration : IEntityTypeConfiguration<CategoryType>
{
    public void Configure(EntityTypeBuilder<CategoryType> builder)
    {
        builder.ToTable("CategoryTypes")
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
        
        builder.HasMany(x => x.Categories)
            .WithOne(x => x.CategoryType)
            .HasForeignKey(x => x.CategoryTypeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.CategoryType)
            .HasForeignKey(x => x.CategoryTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}