using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class addrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetUser");

            migrationBuilder.CreateTable(
                name: "AssetRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetRequests");

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
    }
}
