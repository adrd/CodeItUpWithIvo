using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerImages.Data.Migrations
{
    public partial class ImageDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OriginalFileName = table.Column<string>(nullable: true),
                    OriginalType = table.Column<string>(nullable: true),
                    OriginalContent = table.Column<byte[]>(nullable: true),
                    ThumbnailContent = table.Column<byte[]>(nullable: true),
                    FullscreenContent = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageData");
        }
    }
}
