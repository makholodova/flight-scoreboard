using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightScoreboard.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGateAndTerminal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Terminal",
                table: "Flights",
                newName: "ToTerminal");

            migrationBuilder.RenameColumn(
                name: "Gate",
                table: "Flights",
                newName: "ToGate");

            migrationBuilder.AddColumn<string>(
                name: "FromGate",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromTerminal",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromGate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FromTerminal",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "ToTerminal",
                table: "Flights",
                newName: "Terminal");

            migrationBuilder.RenameColumn(
                name: "ToGate",
                table: "Flights",
                newName: "Gate");
        }
    }
}
