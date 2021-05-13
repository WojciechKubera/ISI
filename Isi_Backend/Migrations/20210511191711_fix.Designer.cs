﻿// <auto-generated />
using System;
using Isi_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Isi_Backend.Migrations
{
    [DbContext(typeof(Isi_BackendContext))]
    [Migration("20210511191711_fix")]
    partial class fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Isi_Backend.Models.Emails", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("condition")
                        .HasColumnType("int");

                    b.Property<string>("countryCondition")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Isi_Backend.Models.Statistics", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Liczba_przypadkow")
                        .HasColumnType("int");

                    b.Property<int>("NewConfirmed")
                        .HasColumnType("int");

                    b.Property<int>("NewDeaths")
                        .HasColumnType("int");

                    b.Property<int>("NewRecovered")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalConfirmed")
                        .HasColumnType("int");

                    b.Property<int>("TotalDeaths")
                        .HasColumnType("int");

                    b.Property<int>("TotalRecovered")
                        .HasColumnType("int");

                    b.Property<string>("Wojewodztwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("liczba_osob_objetych_kwarantanna")
                        .HasColumnType("int");

                    b.Property<int>("liczba_pozostalych_testow")
                        .HasColumnType("int");

                    b.Property<int>("liczba_testow_z_wynikiem_negatywnym")
                        .HasColumnType("int");

                    b.Property<int>("liczba_testow_z_wynikiem_pozytywnym")
                        .HasColumnType("int");

                    b.Property<int>("liczba_wykonanych_testow")
                        .HasColumnType("int");

                    b.Property<int>("liczba_zlecen_poz")
                        .HasColumnType("int");

                    b.Property<int>("stan_rekordu_na")
                        .HasColumnType("int");

                    b.Property<string>("teryt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("zgony_w_wyniku_covid_bez_chorob_wspolistniejacych")
                        .HasColumnType("int");

                    b.Property<int>("zgony_w_wyniku_covid_i_chorob_wspolistniejacych")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
