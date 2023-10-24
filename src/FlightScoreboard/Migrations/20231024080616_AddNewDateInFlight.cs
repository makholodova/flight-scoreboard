using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightScoreboard.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDateInFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualArrivalTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDepartureTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BoardingEndTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BoardingStartTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInEndTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInStartTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gate",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumberOfFlight",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Terminal",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualArrivalTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ActualDepartureTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BoardingEndTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BoardingStartTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CheckInEndTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CheckInStartTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Gate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "NumberOfFlight",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Terminal",
                table: "Flights");
        }
    }
}
