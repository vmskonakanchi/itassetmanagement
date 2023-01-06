using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class changed_uint_to_int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "AssetRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AssetId",
                table: "AssetRequests",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
