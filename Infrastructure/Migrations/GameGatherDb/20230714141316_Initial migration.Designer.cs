﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(GameGatherDbContext))]
    [Migration("20230714141316_Initial migration")]
    partial class Initialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BoardGameBoardGameNight", b =>
                {
                    b.Property<int>("BoardGameNightsId")
                        .HasColumnType("int");

                    b.Property<int>("BoardGamesId")
                        .HasColumnType("int");

                    b.HasKey("BoardGameNightsId", "BoardGamesId");

                    b.HasIndex("BoardGamesId");

                    b.ToTable("BoardGameBoardGameNight");
                });

            modelBuilder.Entity("BoardGameNightUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("BoardGameNightId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BoardGameNightId");

                    b.HasIndex("BoardGameNightId");

                    b.ToTable("BoardGameNightUser");
                });

            modelBuilder.Entity("Domain.Entities.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("GameType")
                        .HasColumnType("int");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsAdultOnly")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("Domain.Entities.BoardGameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodAndDrinksPreferenceId")
                        .HasColumnType("int");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdultOnly")
                        .HasColumnType("bit");

                    b.Property<int>("MaxAttendees")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FoodAndDrinksPreferenceId");

                    b.HasIndex("HostId");

                    b.ToTable("BoardGameNights");
                });

            modelBuilder.Entity("Domain.Entities.FoodAndDrinksPreference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AlcoholFree")
                        .HasColumnType("bit");

                    b.Property<bool>("LactoseFree")
                        .HasColumnType("bit");

                    b.Property<bool>("NutFree")
                        .HasColumnType("bit");

                    b.Property<bool>("Vegetarian")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("FoodAndDrinksPreferences");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardGameNightId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardGameNightId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("FoodAndDrinksPreferenceId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FoodAndDrinksPreferenceId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BoardGameBoardGameNight", b =>
                {
                    b.HasOne("Domain.Entities.BoardGameNight", null)
                        .WithMany()
                        .HasForeignKey("BoardGameNightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.BoardGame", null)
                        .WithMany()
                        .HasForeignKey("BoardGamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoardGameNightUser", b =>
                {
                    b.HasOne("Domain.Entities.BoardGameNight", null)
                        .WithMany()
                        .HasForeignKey("BoardGameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.BoardGameNight", b =>
                {
                    b.HasOne("Domain.Entities.FoodAndDrinksPreference", "FoodAndDrinksPreference")
                        .WithMany()
                        .HasForeignKey("FoodAndDrinksPreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Host")
                        .WithMany("HostingBoardGameNights")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FoodAndDrinksPreference");

                    b.Navigation("Host");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.HasOne("Domain.Entities.BoardGameNight", "BoardGameNight")
                        .WithMany("Reviews")
                        .HasForeignKey("BoardGameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BoardGameNight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.FoodAndDrinksPreference", "FoodAndDrinksPreference")
                        .WithMany()
                        .HasForeignKey("FoodAndDrinksPreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodAndDrinksPreference");
                });

            modelBuilder.Entity("Domain.Entities.BoardGameNight", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("HostingBoardGameNights");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
