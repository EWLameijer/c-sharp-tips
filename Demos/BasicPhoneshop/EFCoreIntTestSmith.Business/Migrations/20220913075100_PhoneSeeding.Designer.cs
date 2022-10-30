﻿// <auto-generated />
using EFCoreIntTestSmith.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreIntTestSmith.Business.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220913075100_PhoneSeeding")]
    partial class PhoneSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFCoreIntTestSmith.Business.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Huawei"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Samsung"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Apple"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Google"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Xiaomi"
                        });
                });

            modelBuilder.Entity("EFCoreIntTestSmith.Business.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Phones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            Type = "P30"
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            Type = "Galaxy A52"
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 3,
                            Type = "iPhone 11"
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 4,
                            Type = "Pixel 4a"
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 5,
                            Type = "Redmi Note 10 Pro"
                        });
                });

            modelBuilder.Entity("EFCoreIntTestSmith.Business.Phone", b =>
                {
                    b.HasOne("EFCoreIntTestSmith.Business.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });
#pragma warning restore 612, 618
        }
    }
}
