using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AssetUser",
                columns: table => new
                {
                    AssetOwnedById = table.Column<long>(type: "bigint", nullable: false),
                    OwnedAssetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetUser", x => new { x.AssetOwnedById, x.OwnedAssetsId });
                    table.ForeignKey(
                        name: "FK_AssetUser_Assets_OwnedAssetsId",
                        column: x => x.OwnedAssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetUser_Users_AssetOwnedById",
                        column: x => x.AssetOwnedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetUser_OwnedAssetsId",
                table: "AssetUser",
                column: "OwnedAssetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetUser");

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
    }
}
