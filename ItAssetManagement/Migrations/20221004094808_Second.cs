using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAssetManagement.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnteredBy",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnteredBy",
                table: "Assets");
        }
    }
}
