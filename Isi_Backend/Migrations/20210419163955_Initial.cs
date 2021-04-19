using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Isi_Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewConfirmed = table.Column<int>(type: "int", nullable: false),
                    TotalConfirmed = table.Column<int>(type: "int", nullable: false),
                    NewDeaths = table.Column<int>(type: "int", nullable: false),
                    TotalDeaths = table.Column<int>(type: "int", nullable: false),
                    NewRecovered = table.Column<int>(type: "int", nullable: false),
                    TotalRecovered = table.Column<int>(type: "int", nullable: false),
                    Wojewodztwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liczba_przypadkow = table.Column<int>(type: "int", nullable: false),
                    zgony_w_wyniku_covid_bez_chorob_wspolistniejacych = table.Column<int>(type: "int", nullable: false),
                    zgony_w_wyniku_covid_i_chorob_wspolistniejacych = table.Column<int>(type: "int", nullable: false),
                    liczba_zlecen_poz = table.Column<int>(type: "int", nullable: false),
                    liczba_osob_objetych_kwarantanna = table.Column<int>(type: "int", nullable: false),
                    iczba_wykonanych_testow = table.Column<int>(type: "int", nullable: false),
                    liczba_testow_z_wynikiem_pozytywnym = table.Column<int>(type: "int", nullable: false),
                    liczba_testow_z_wynikiem_negatywnym = table.Column<int>(type: "int", nullable: false),
                    liczba_pozostalych_testow = table.Column<int>(type: "int", nullable: false),
                    teryt = table.Column<int>(type: "int", nullable: false),
                    stan_rekordu_na = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
