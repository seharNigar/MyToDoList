using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class finalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TheList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TheList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TheList_AppUserId",
                table: "TheList",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TheList_AspNetUsers_AppUserId",
                table: "TheList",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheList_AspNetUsers_AppUserId",
                table: "TheList");

            migrationBuilder.DropIndex(
                name: "IX_TheList_AppUserId",
                table: "TheList");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TheList");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TheList");
        }
    }
}
