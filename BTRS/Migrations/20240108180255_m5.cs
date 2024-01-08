using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTRS.Migrations
{
    public partial class m5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "bus_trips",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trip_destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bus_number = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus_trips", x => x.ID);
                    table.ForeignKey(
                        name: "FK_bus_trips_admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "admins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "buses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    captain_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    num_seat = table.Column<int>(type: "int", nullable: false),
                    Bus_TripID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_buses_bus_trips_Bus_TripID",
                        column: x => x.Bus_TripID,
                        principalTable: "bus_trips",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "passenger_BusTrips",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    Bus_TripID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passenger_BusTrips", x => x.ID);
                    table.ForeignKey(
                        name: "FK_passenger_BusTrips_bus_trips_Bus_TripID",
                        column: x => x.Bus_TripID,
                        principalTable: "bus_trips",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_passenger_BusTrips_passengers_PassengerID",
                        column: x => x.PassengerID,
                        principalTable: "passengers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bus_trips_AdminID",
                table: "bus_trips",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_buses_Bus_TripID",
                table: "buses",
                column: "Bus_TripID");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_BusTrips_Bus_TripID",
                table: "passenger_BusTrips",
                column: "Bus_TripID");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_BusTrips_PassengerID",
                table: "passenger_BusTrips",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "IX_passengers_email",
                table: "passengers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_passengers_username",
                table: "passengers",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "buses");

            migrationBuilder.DropTable(
                name: "passenger_BusTrips");

            migrationBuilder.DropTable(
                name: "bus_trips");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "admins");
        }
    }
}
