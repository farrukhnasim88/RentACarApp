using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApp.Migrations
{
    public partial class AddedRefNoinBookingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefrenceNo",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefrenceNo",
                table: "Bookings");
        }
    }
}
