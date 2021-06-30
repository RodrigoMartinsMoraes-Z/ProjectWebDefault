using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Context.Migrations
{
    public partial class ShoppingListCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ShoppingLists_CartId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Items",
                newName: "ShoppingListId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CartId",
                table: "Items",
                newName: "IX_Items_ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ShoppingLists_ShoppingListId",
                table: "Items",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ShoppingLists_ShoppingListId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ShoppingListId",
                table: "Items",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ShoppingListId",
                table: "Items",
                newName: "IX_Items_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ShoppingLists_CartId",
                table: "Items",
                column: "CartId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
