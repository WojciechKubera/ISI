using Microsoft.EntityFrameworkCore.Migrations;

namespace Isi_Backend.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "countryCondition",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "countryCondition",
                table: "Emails");
        }
    }
}
