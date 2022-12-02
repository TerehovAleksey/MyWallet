namespace MyWallet.Core.Dal.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users")
            .HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnType("BLOB");

        builder.Property(x => x.UserName)
            .HasMaxLength(50);

        builder.Property(x => x.NormalizedUserName)
            .HasMaxLength(50);

        builder.Property(x => x.FirstName)
            .HasMaxLength(30);

        builder.Property(x => x.LastName)
            .HasMaxLength(30);

        builder.Property(x => x.Email)
            .HasMaxLength(50);

        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(50);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(1024);

        builder.Property(x => x.SecurityStamp)
            .HasMaxLength(1024);

        builder.Property(x => x.ConcurrencyStamp)
            .HasMaxLength(1024);

        builder.Property(x => x.Gender)
            .HasColumnType("INTEGER")
            .IsRequired();

        builder.HasMany(x => x.UserDevices)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Accounts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Categories)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}