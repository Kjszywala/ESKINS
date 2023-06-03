using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESKINS.API.Migrations
{
    public partial class SaleCart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "SaleCart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "SaleCart");
        }
    }
}
