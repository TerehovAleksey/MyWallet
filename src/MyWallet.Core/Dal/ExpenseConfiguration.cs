using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWallet.Core.Dal;

internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("BLOB");

            builder.Property(x => x.CategoryId)
                .HasColumnType("BLOB")
                .IsRequired();

            builder.Property(x => x.DateOfCreation)
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.Value)
                .HasColumnType("REAL")
                .IsRequired();
        }
}
