using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class TicketsSoldAddedToScreening : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfTickets",
                table: "Screenings",
                newName: "NumberOfTicketsTotal");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTicketsSold",
                table: "Screenings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfTicketsSold",
                table: "Screenings");

            migrationBuilder.RenameColumn(
                name: "NumberOfTicketsTotal",
                table: "Screenings",
                newName: "NumberOfTickets");
        }
    }
}
