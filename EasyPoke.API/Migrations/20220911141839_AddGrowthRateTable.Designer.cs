// <auto-generated />
using System;
using EasyPoke.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyPoke.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220911141839_AddGrowthRateTable")]
    partial class AddGrowthRateTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EasyPoke.API.Models.GrowthRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Formula")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GrowthRates");
                });

            modelBuilder.Entity("EasyPoke.API.Models.GrowthRateLevelExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<int?>("GrowthRateId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrowthRateId");

                    b.ToTable("GrowthRateLevelExperiences");
                });

            modelBuilder.Entity("EasyPoke.API.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("PokemonSpeciesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonSpeciesId");

                    b.HasIndex("UserId");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("EasyPoke.API.Models.PokemonSpecies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int?>("EvolvedFromId")
                        .HasColumnType("int");

                    b.Property<int>("GrowthRateId")
                        .HasColumnType("int");

                    b.Property<int>("HitPoint")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SpecialAttack")
                        .HasColumnType("int");

                    b.Property<int>("SpecialDefense")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Type1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Type2Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EvolvedFromId");

                    b.HasIndex("GrowthRateId");

                    b.HasIndex("Type1Id");

                    b.HasIndex("Type2Id");

                    b.ToTable("PokemonSpecies");
                });

            modelBuilder.Entity("EasyPoke.API.Models.PokemonType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PokemonTypes");
                });

            modelBuilder.Entity("EasyPoke.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EasyPoke.API.Models.GrowthRateLevelExperience", b =>
                {
                    b.HasOne("EasyPoke.API.Models.GrowthRate", null)
                        .WithMany("LevelExperiences")
                        .HasForeignKey("GrowthRateId");
                });

            modelBuilder.Entity("EasyPoke.API.Models.Pokemon", b =>
                {
                    b.HasOne("EasyPoke.API.Models.PokemonSpecies", "PokemonSpecies")
                        .WithMany()
                        .HasForeignKey("PokemonSpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyPoke.API.Models.User", "User")
                        .WithMany("Pokemons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PokemonSpecies");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyPoke.API.Models.PokemonSpecies", b =>
                {
                    b.HasOne("EasyPoke.API.Models.PokemonSpecies", "EvolvedFrom")
                        .WithMany()
                        .HasForeignKey("EvolvedFromId");

                    b.HasOne("EasyPoke.API.Models.GrowthRate", "GrowthRate")
                        .WithMany()
                        .HasForeignKey("GrowthRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyPoke.API.Models.PokemonType", "Type1")
                        .WithMany()
                        .HasForeignKey("Type1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyPoke.API.Models.PokemonType", "Type2")
                        .WithMany()
                        .HasForeignKey("Type2Id");

                    b.Navigation("EvolvedFrom");

                    b.Navigation("GrowthRate");

                    b.Navigation("Type1");

                    b.Navigation("Type2");
                });

            modelBuilder.Entity("EasyPoke.API.Models.GrowthRate", b =>
                {
                    b.Navigation("LevelExperiences");
                });

            modelBuilder.Entity("EasyPoke.API.Models.User", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
