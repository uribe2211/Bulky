﻿// <auto-generated />
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace BulkyWeb.DataAccess.Migrations;

[DbContext(typeof(DbContextApp))]
partial class DbContextAppModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.2")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("BulkyWeb.Models.Category", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("DisplayOrder")
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.ToTable("Categories");

                b.HasData(
                    new
                    {
                        Id = 1,
                        DisplayOrder = 1,
                        Name = "Action"
                    },
                    new
                    {
                        Id = 2,
                        DisplayOrder = 2,
                        Name = "Drama"
                    },
                    new
                    {
                        Id = 3,
                        DisplayOrder = 3,
                        Name = "Terror"
                    },
                    new
                    {
                        Id = 4,
                        DisplayOrder = 4,
                        Name = "SCFI"
                    },
                    new
                    {
                        Id = 5,
                        DisplayOrder = 5,
                        Name = "Comedia"
                    });
            });
#pragma warning restore 612, 618
    }
}
