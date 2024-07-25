using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPurchase_purchases_PurchasespurchaseID",
                table: "ItemPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceipt_purchases_PurchasespurchaseID",
                table: "PurchaseReceipt");

            migrationBuilder.RenameColumn(
                name: "purchaseID",
                table: "purchases",
                newName: "PurchaseID");

            migrationBuilder.RenameColumn(
                name: "PurchasespurchaseID",
                table: "PurchaseReceipt",
                newName: "PurchasesPurchaseID");

            migrationBuilder.RenameColumn(
                name: "PurchasespurchaseID",
                table: "ItemPurchase",
                newName: "PurchasesPurchaseID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPurchase_PurchasespurchaseID",
                table: "ItemPurchase",
                newName: "IX_ItemPurchase_PurchasesPurchaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPurchase_purchases_PurchasesPurchaseID",
                table: "ItemPurchase",
                column: "PurchasesPurchaseID",
                principalTable: "purchases",
                principalColumn: "PurchaseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceipt_purchases_PurchasesPurchaseID",
                table: "PurchaseReceipt",
                column: "PurchasesPurchaseID",
                principalTable: "purchases",
                principalColumn: "PurchaseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPurchase_purchases_PurchasesPurchaseID",
                table: "ItemPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceipt_purchases_PurchasesPurchaseID",
                table: "PurchaseReceipt");

            migrationBuilder.RenameColumn(
                name: "PurchaseID",
                table: "purchases",
                newName: "purchaseID");

            migrationBuilder.RenameColumn(
                name: "PurchasesPurchaseID",
                table: "PurchaseReceipt",
                newName: "PurchasespurchaseID");

            migrationBuilder.RenameColumn(
                name: "PurchasesPurchaseID",
                table: "ItemPurchase",
                newName: "PurchasespurchaseID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPurchase_PurchasesPurchaseID",
                table: "ItemPurchase",
                newName: "IX_ItemPurchase_PurchasespurchaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPurchase_purchases_PurchasespurchaseID",
                table: "ItemPurchase",
                column: "PurchasespurchaseID",
                principalTable: "purchases",
                principalColumn: "purchaseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceipt_purchases_PurchasespurchaseID",
                table: "PurchaseReceipt",
                column: "PurchasespurchaseID",
                principalTable: "purchases",
                principalColumn: "purchaseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
