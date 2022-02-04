namespace Catstagram.Server.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CatsTableNewName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cat_AspNetUsers_UserId",
                table: "Cat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cat",
                table: "Cat");

            migrationBuilder.RenameTable(
                name: "Cat",
                newName: "Cats");

            migrationBuilder.RenameIndex(
                name: "IX_Cat_UserId",
                table: "Cats",
                newName: "IX_Cats_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cats",
                table: "Cats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_AspNetUsers_UserId",
                table: "Cats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_AspNetUsers_UserId",
                table: "Cats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cats",
                table: "Cats");

            migrationBuilder.RenameTable(
                name: "Cats",
                newName: "Cat");

            migrationBuilder.RenameIndex(
                name: "IX_Cats_UserId",
                table: "Cat",
                newName: "IX_Cat_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cat",
                table: "Cat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_AspNetUsers_UserId",
                table: "Cat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
