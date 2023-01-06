using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "AssetRequests");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AssetRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AssetRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "AssetRequests",
                type: "bit",
                nullable: true);
        }
    }
}
