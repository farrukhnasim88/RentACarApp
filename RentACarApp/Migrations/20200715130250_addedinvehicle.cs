using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApp.Migrations
{
    public partial class addedinvehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Locations_LocationId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Locations_LocationId",
                table: "Vehicles",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Locations_LocationId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Locations_LocationId",
                table: "Vehicles",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
