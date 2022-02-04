using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalSystem.Infrastructure.Common.Persistence.Migrations
{
    public partial class UserDealershipRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Dealers_DealerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarAdView_CarAds_CarAdId",
                table: "CarAdView");

            migrationBuilder.DropForeignKey(
                name: "FK_CarAdView_Statistics_StatisticsId",
                table: "CarAdView");

            migrationBuilder.DropForeignKey(
                name: "FK_CarAdView_AspNetUsers_UserId",
                table: "CarAdView");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DealerId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarAdView",
                table: "CarAdView");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "CarAdView",
                newName: "CarAdViews");

            migrationBuilder.RenameIndex(
                name: "IX_CarAdView_UserId",
                table: "CarAdViews",
                newName: "IX_CarAdViews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarAdView_StatisticsId",
                table: "CarAdViews",
                newName: "IX_CarAdViews_StatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_CarAdView_CarAdId",
                table: "CarAdViews",
                newName: "IX_CarAdViews_CarAdId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dealers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarAdViews",
                table: "CarAdViews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAdViews_CarAds_CarAdId",
                table: "CarAdViews",
                column: "CarAdId",
                principalTable: "CarAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAdViews_Statistics_StatisticsId",
                table: "CarAdViews",
                column: "StatisticsId",
                principalTable: "Statistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAdViews_AspNetUsers_UserId",
                table: "CarAdViews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_AspNetUsers_UserId",
                table: "Dealers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarAdViews_CarAds_CarAdId",
                table: "CarAdViews");

            migrationBuilder.DropForeignKey(
                name: "FK_CarAdViews_Statistics_StatisticsId",
                table: "CarAdViews");

            migrationBuilder.DropForeignKey(
                name: "FK_CarAdViews_AspNetUsers_UserId",
                table: "CarAdViews");

            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_AspNetUsers_UserId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarAdViews",
                table: "CarAdViews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dealers");

            migrationBuilder.RenameTable(
                name: "CarAdViews",
                newName: "CarAdView");

            migrationBuilder.RenameIndex(
                name: "IX_CarAdViews_UserId",
                table: "CarAdView",
                newName: "IX_CarAdView_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarAdViews_StatisticsId",
                table: "CarAdView",
                newName: "IX_CarAdView_StatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_CarAdViews_CarAdId",
                table: "CarAdView",
                newName: "IX_CarAdView_CarAdId");

            migrationBuilder.AddColumn<int>(
                name: "DealerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarAdView",
                table: "CarAdView",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DealerId",
                table: "AspNetUsers",
                column: "DealerId",
                unique: true,
                filter: "[DealerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Dealers_DealerId",
                table: "AspNetUsers",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAdView_CarAds_CarAdId",
                table: "CarAdView",
                column: "CarAdId",
                principalTable: "CarAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAdView_Statistics_StatisticsId",
                table: "CarAdView",
                column: "StatisticsId",
                principalTable: "Statistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAdView_AspNetUsers_UserId",
                table: "CarAdView",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
