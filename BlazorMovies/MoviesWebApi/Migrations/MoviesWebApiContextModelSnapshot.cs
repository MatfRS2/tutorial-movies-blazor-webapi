﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesWebApi.Data;

#nullable disable

namespace MoviesWebApi.Migrations
{
    [DbContext(typeof(MoviesWebApiContext))]
    partial class MoviesWebApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MoviesWebApi.Models.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FilmId"), 1L, 1);

                    b.Property<DateTime>("DatumPocetkaPrikazivanja")
                        .HasColumnType("date");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal>("Ulozeno")
                        .HasColumnType("money");

                    b.Property<int>("ZanrId")
                        .HasColumnType("int");

                    b.HasKey("FilmId");

                    b.HasIndex("ZanrId");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("MoviesWebApi.Models.FilmPaket", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("PaketId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "PaketId");

                    b.HasIndex("PaketId");

                    b.ToTable("FilmPaket");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KorisnikId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Potroseno")
                        .HasColumnType("money");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("KorisnikId");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Paket", b =>
                {
                    b.Property<int>("PaketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaketId"), 1L, 1);

                    b.Property<DateTime>("DatumFormiranja")
                        .HasColumnType("date");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("PaketId");

                    b.ToTable("Paket");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Pretplata", b =>
                {
                    b.Property<int>("PretplataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PretplataId"), 1L, 1);

                    b.Property<DateTime>("DatumIsteka")
                        .HasColumnType("date");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("money");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("PaketId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PretplataId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("PaketId");

                    b.ToTable("Pretplata");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Zanr", b =>
                {
                    b.Property<int>("ZanrId")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ZanrId");

                    b.ToTable("Zanr");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Film", b =>
                {
                    b.HasOne("MoviesWebApi.Models.Zanr", "Zanr")
                        .WithMany("Filmovi")
                        .HasForeignKey("ZanrId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zanr");
                });

            modelBuilder.Entity("MoviesWebApi.Models.FilmPaket", b =>
                {
                    b.HasOne("MoviesWebApi.Models.Film", "Film")
                        .WithMany("FilmPaketi")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesWebApi.Models.Paket", "Paket")
                        .WithMany("FilmPaketi")
                        .HasForeignKey("PaketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Paket");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Pretplata", b =>
                {
                    b.HasOne("MoviesWebApi.Models.Korisnik", "Korisnik")
                        .WithMany("Pretplate")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesWebApi.Models.Paket", "Paket")
                        .WithMany("Pretplate")
                        .HasForeignKey("PaketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Paket");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Film", b =>
                {
                    b.Navigation("FilmPaketi");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Korisnik", b =>
                {
                    b.Navigation("Pretplate");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Paket", b =>
                {
                    b.Navigation("FilmPaketi");

                    b.Navigation("Pretplate");
                });

            modelBuilder.Entity("MoviesWebApi.Models.Zanr", b =>
                {
                    b.Navigation("Filmovi");
                });
#pragma warning restore 612, 618
        }
    }
}
