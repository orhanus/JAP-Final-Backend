using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CorrectedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "TopMostSoldTickets",
                newName: "MediaId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "TopMostScreenings",
                newName: "MediaId");

            migrationBuilder.RenameColumn(
                name: "MovieID",
                table: "TopMostRatings",
                newName: "MediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "TopMostSoldTickets",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "TopMostScreenings",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "TopMostRatings",
                newName: "MovieID");
        }
    }
}
