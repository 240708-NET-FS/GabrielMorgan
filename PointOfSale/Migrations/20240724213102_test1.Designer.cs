﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSaleApp.Entities;

#nullable disable

namespace PointOfSale.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240724213102_test1")]
    partial class test1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ItemPurchase", b =>
                {
                    b.Property<int>("ItemsItemID")
                        .HasColumnType("int");

                    b.Property<int>("PurchasesPurchaseID")
                        .HasColumnType("int");

                    b.HasKey("ItemsItemID", "PurchasesPurchaseID");

                    b.HasIndex("PurchasesPurchaseID");

                    b.ToTable("ItemPurchase");
                });

            modelBuilder.Entity("PointOfSaleApp.Entities.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.HasKey("ItemID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Purchase", b =>
                {
                    b.Property<int>("PurchaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseID"));

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("ItemQuantity")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptID")
                        .HasColumnType("int");

                    b.HasKey("PurchaseID");

                    b.ToTable("purchases");
                });

            modelBuilder.Entity("PurchaseReceipt", b =>
                {
                    b.Property<int>("PurchasesPurchaseID")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptsReceiptID")
                        .HasColumnType("int");

                    b.HasKey("PurchasesPurchaseID", "ReceiptsReceiptID");

                    b.HasIndex("ReceiptsReceiptID");

                    b.ToTable("PurchaseReceipt");
                });

            modelBuilder.Entity("Receipt", b =>
                {
                    b.Property<int>("ReceiptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceiptID"));

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("ReceiptID");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("ItemPurchase", b =>
                {
                    b.HasOne("PointOfSaleApp.Entities.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Purchase", null)
                        .WithMany()
                        .HasForeignKey("PurchasesPurchaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PurchaseReceipt", b =>
                {
                    b.HasOne("Purchase", null)
                        .WithMany()
                        .HasForeignKey("PurchasesPurchaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Receipt", null)
                        .WithMany()
                        .HasForeignKey("ReceiptsReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
