using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESKINS.API.Migrations
{
    public partial class ItemRemovedFromBuyCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyCarts_Items_ItemId",
                table: "BuyCarts");

            migrationBuilder.DropIndex(
                name: "IX_BuyCarts_ItemId",
                table: "BuyCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BuyCarts_ItemId",
                table: "BuyCarts",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyCarts_Items_ItemId",
                table: "BuyCarts",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
