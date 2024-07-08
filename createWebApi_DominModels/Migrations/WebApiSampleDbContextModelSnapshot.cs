﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using createWebApi_DominModels.Data;

#nullable disable

namespace createWebApi_DominModels.Migrations
{
    [DbContext(typeof(WebApiSampleDbContext))]
    partial class WebApiSampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("createWebApi_DominModels.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c6105626-2ed0-4f01-ba66-e9f8943e3c3c"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("19c49e0c-ad0f-40a1-a446-c8baac4e4119"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("f03ae466-c08f-4b85-80a9-22c55bcdaec7"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("createWebApi_DominModels.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b09924c5-a86d-4d5c-abd2-ede7587fcf25"),
                            Code = "Parko",
                            Name = "Parko Parco 牛肚包義大利小酒館",
                            RegionImageUrl = "https://angelababy.tw/wp-content/uploads/2022/01/DSC09440.jpg"
                        },
                        new
                        {
                            Id = new Guid("2776d88d-b0c7-467c-ad85-1eb730e083f0"),
                            Code = "GSKP",
                            Name = "金孫韓廚義大利麵",
                            RegionImageUrl = "https://upssmile.com/wp-content/uploads/2022/12/20221205-IMG_5577.jpg"
                        },
                        new
                        {
                            Id = new Guid("5351b315-43b3-4f61-83ec-c044efd9a650"),
                            Code = "Coco",
                            Name = "Coco Brother 椰兄",
                            RegionImageUrl = "https://leelife.tw/wp-content/uploads/2023/04/S__10117148.jpg"
                        });
                });

            modelBuilder.Entity("createWebApi_DominModels.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("createWebApi_DominModels.Models.Domain.Walk", b =>
                {
                    b.HasOne("createWebApi_DominModels.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("createWebApi_DominModels.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
