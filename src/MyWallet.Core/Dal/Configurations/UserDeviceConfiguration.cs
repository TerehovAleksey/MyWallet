namespace MyWallet.Core.Dal.Configurations;

internal class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(EntityTypeBuilder<UserDevice> builder)
    {
        builder.ToTable("UserDevices")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("BLOB");

        builder.Property(x => x.DateOfCreation)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(x => x.DateOfChange)
            .HasColumnType("TEXT");

        builder.Property(x => x.LastLoginDate)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(x=>x.DeviceName)
            .HasColumnType("TEXT")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x=>x.RefreshToken)
            .HasColumnType("TEXT")
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(x => x.LastIpAddress)
            .HasColumnType("TEXT")
            .HasMaxLength(50);

        builder.Property(x=>x.UserId)
            .HasColumnType("BLOB")
            .IsRequired();
    }
}
