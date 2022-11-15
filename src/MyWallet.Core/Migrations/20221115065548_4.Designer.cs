﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWallet.Core.Dal;

#nullable disable

namespace MyWallet.Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221115065548_4")]
    partial class _4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("MyWallet.Core.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BLOB");

                    b.Property<decimal>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("CurrencySymbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfChange")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("MyWallet.Core.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BLOB");

                    b.Property<DateTime?>("DateOfChange")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsIncome")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("MyWallet.Core.Journal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BLOB");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("BLOB");

                    b.Property<DateTime?>("DateOfChange")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubCategoryId")
                        .HasColumnType("BLOB");

                    b.Property<decimal>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Journals", (string)null);
                });

            modelBuilder.Entity("MyWallet.Core.SubCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BLOB");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("BLOB");

                    b.Property<DateTime?>("DateOfChange")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories", (string)null);
                });

            modelBuilder.Entity("MyWallet.Core.Journal", b =>
                {
                    b.HasOne("MyWallet.Core.Account", "Account")
                        .WithMany("Journals")
                        .HasForeignKey("AccountId");

                    b.HasOne("MyWallet.Core.SubCategory", "SubCategory")
                        .WithMany("Journals")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("MyWallet.Core.SubCategory", b =>
                {
                    b.HasOne("MyWallet.Core.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MyWallet.Core.Account", b =>
                {
                    b.Navigation("Journals");
                });

            modelBuilder.Entity("MyWallet.Core.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("MyWallet.Core.SubCategory", b =>
                {
                    b.Navigation("Journals");
                });
#pragma warning restore 612, 618
        }
    }
}
