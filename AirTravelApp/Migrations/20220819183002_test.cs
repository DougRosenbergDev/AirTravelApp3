using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirTravelApp.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedFlight");

            migrationBuilder.DropTable(
                name: "DreamFlight");

            migrationBuilder.DropTable(
                name: "PurchasedFlight");

            migrationBuilder.CreateIndex(
                name: "IX_BookedFlights_FlightId",
                table: "BookedFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedFlights_PassengerId",
                table: "BookedFlights",
                column: "PassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedFlights_Flights_FlightId",
                table: "BookedFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedFlights_Passengers_PassengerId",
                table: "BookedFlights",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedFlights_Flights_FlightId",
                table: "BookedFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedFlights_Passengers_PassengerId",
                table: "BookedFlights");

            migrationBuilder.DropIndex(
                name: "IX_BookedFlights_FlightId",
                table: "BookedFlights");

            migrationBuilder.DropIndex(
                name: "IX_BookedFlights_PassengerId",
                table: "BookedFlights");

            migrationBuilder.CreateTable(
                name: "BookedFlight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedFlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookedFlight_BookedFlights_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BookedFlights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookedFlight_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DreamFlight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamFlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DreamFlight_BookedFlights_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BookedFlights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DreamFlight_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedFlight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedFlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasedFlight_BookedFlights_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BookedFlights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedFlight_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedFlight_BookingId",
                table: "BookedFlight",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedFlight_FlightId",
                table: "BookedFlight",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_DreamFlight_BookingId",
                table: "DreamFlight",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_DreamFlight_PassengerId",
                table: "DreamFlight",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedFlight_BookingId",
                table: "PurchasedFlight",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedFlight_PassengerId",
                table: "PurchasedFlight",
                column: "PassengerId");
        }
    }
}
