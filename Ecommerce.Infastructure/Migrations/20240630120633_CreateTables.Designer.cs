﻿// <auto-generated />
using System;
using Ecommerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Infastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240630120633_CreateTables")]
    partial class CreateTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Core.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Electronic items",
                            Name = "Electronics",
                            ProductId = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "Various books",
                            Name = "Books",
                            ProductId = 0
                        });
                });

            modelBuilder.Entity("Ecommerce.Core.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "ghada@example.com",
                            Name = "John Doe",
                            Password = "password",
                            Phone = "1234567890",
                            Role = "Customer"
                        },
                        new
                        {
                            Id = 2,
                            Email = "manal@example.com",
                            Name = "Jane Doe",
                            Password = "password",
                            Phone = "0987654321",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("Ecommerce.Core.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LocalUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderSatutus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocalUserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LocalUserId = 1,
                            OrderDate = new DateTime(2024, 6, 30, 15, 6, 31, 931, DateTimeKind.Local).AddTicks(5904),
                            OrderSatutus = "Completed"
                        },
                        new
                        {
                            Id = 2,
                            LocalUserId = 1,
                            OrderDate = new DateTime(2024, 6, 30, 15, 6, 31, 931, DateTimeKind.Local).AddTicks(5987),
                            OrderSatutus = "Pending"
                        });
                });

            modelBuilder.Entity("Ecommerce.Core.Models.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id", "OrderId", "ProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            price = 699.99m,
                            quantity = 1
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 3,
                            price = 19.99m,
                            quantity = 2
                        },
                        new
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 2,
                            price = 999.99m,
                            quantity = 1
                        });
                });

            modelBuilder.Entity("Ecommerce.Core.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("categoriesId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("categoriesId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Image = "smartphone.jpg",
                            Name = "Smartphone",
                            price = 699m
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Image = "laptop.jpg",
                            Name = "Laptop",
                            price = 999m
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Image = "bookA.jpg",
                            Name = "Book A",
                            price = 19m
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Image = "bookB.jpg",
                            Name = "Book B",
                            price = 29m
                        });
                });

            modelBuilder.Entity("Ecommerce.Core.Models.Order", b =>
                {
                    b.HasOne("Ecommerce.Core.Models.LocalUser", "LocalUsers")
                        .WithMany("Orders")
                        .HasForeignKey("LocalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocalUsers");
                });

            modelBuilder.Entity("Ecommerce.Core.Models.OrderDetails", b =>
                {
                    b.HasOne("Ecommerce.Core.Models.Order", "Orders")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Core.Models.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce.Core.Models.Product", b =>
                {
                    b.HasOne("Ecommerce.Core.Models.Category", "categories")
                        .WithMany()
                        .HasForeignKey("categoriesId");

                    b.Navigation("categories");
                });

            modelBuilder.Entity("Ecommerce.Core.Models.LocalUser", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
