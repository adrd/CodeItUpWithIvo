using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalSystem.Dealers.Data.Migrations
{
    public partial class CarAdDescriptionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CarAds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CarAds");
        }
    }
}
