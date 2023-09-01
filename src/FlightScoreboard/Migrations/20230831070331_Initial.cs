using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightScoreboard.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airplane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplane", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pilot_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirplaneByAirline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: false),
                    AirplaneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirplaneByAirline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirplaneByAirline_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirplaneByAirline_Airplane_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplane",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromCityId = table.Column<int>(type: "int", nullable: false),
                    ToCityId = table.Column<int>(type: "int", nullable: false),
                    PilotId = table.Column<int>(type: "int", nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: false),
                    AirplaneByAirlineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flight_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flight_AirplaneByAirline_AirplaneByAirlineId",
                        column: x => x.AirplaneByAirlineId,
                        principalTable: "AirplaneByAirline",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flight_City_FromCityId",
                        column: x => x.FromCityId,
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flight_City_ToCityId",
                        column: x => x.ToCityId,
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flight_Pilot_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirplaneByAirline_AirlineId",
                table: "AirplaneByAirline",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_AirplaneByAirline_AirplaneId",
                table: "AirplaneByAirline",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineId",
                table: "Flight",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirplaneByAirlineId",
                table: "Flight",
                column: "AirplaneByAirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_FromCityId",
                table: "Flight",
                column: "FromCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_PilotId",
                table: "Flight",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_ToCityId",
                table: "Flight",
                column: "ToCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pilot_AirlineId",
                table: "Pilot",
                column: "AirlineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "AirplaneByAirline");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Pilot");

            migrationBuilder.DropTable(
                name: "Airplane");

            migrationBuilder.DropTable(
                name: "Airline");
        }
    }
}
