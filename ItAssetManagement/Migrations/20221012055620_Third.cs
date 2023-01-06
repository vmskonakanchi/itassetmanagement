using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Assets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UserId",
                table: "Assets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_UserId",
                table: "Assets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_UserId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_UserId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Assets");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeCode",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
