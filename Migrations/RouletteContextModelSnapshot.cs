﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RuletaWebAPI.Data;

namespace RuletaWebAPI.Migrations
{
    [DbContext(typeof(RouletteContext))]
    partial class RouletteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("RuletaWebAPI.Models.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BetType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRoulette")
                        .HasColumnType("int");

                    b.Property<string>("ObtainedValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayedValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StakeValue")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BetItems");
                });

            modelBuilder.Entity("RuletaWebAPI.Models.Roulette", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WinningNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RouletteItems");
                });
#pragma warning restore 612, 618
        }
    }
}
