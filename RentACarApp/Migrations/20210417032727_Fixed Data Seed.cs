using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApp.Migrations
{
    public partial class FixedDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Status_VehicleStatusId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleStatusId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleStatusId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehicleStatusId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleStatusId",
                table: "Vehicles",
                column: "VehicleStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Status_VehicleStatusId",
                table: "Vehicles",
                column: "VehicleStatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
