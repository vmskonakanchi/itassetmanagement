using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class added_assetid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                table: "AssetRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "AssetRequests");
        }
    }
}
