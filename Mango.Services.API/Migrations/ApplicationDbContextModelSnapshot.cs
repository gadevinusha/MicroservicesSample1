﻿// <auto-generated />
using Mango.Services.ProductAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mango.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Alphabets",
                            Description = "Attitude matters",
                            ImageUrl = "",
                            Name = "A",
                            Price = 10.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Alphabets",
                            Description = "Be Yourself",
                            ImageUrl = "",
                            Name = "B",
                            Price = 10.0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Alphabets",
                            Description = "Dont Compromise for unneccessary people, its worthless",
                            ImageUrl = "",
                            Name = "C",
                            Price = 10.0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Alphabets",
                            Description = "Dare and dont be afraid of anyone who is not worth of being anything to you",
                            ImageUrl = "",
                            Name = "D",
                            Price = 10.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
