using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPurchase");

            migrationBuilder.DropTable(
                name: "PurchaseReceipt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchases",
                table: "purchases");

            migrationBuilder.RenameTable(
                name: "purchases",
                newName: "Purchases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "PurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ItemID",
                table: "Purchases",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ReceiptID",
                table: "Purchases",
                column: "ReceiptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Items_ItemID",
                table: "Purchases",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Receipts_ReceiptID",
                table: "Purchases",
                column: "ReceiptID",
                principalTable: "Receipts",
                principalColumn: "ReceiptID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Items_ItemID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Receipts_ReceiptID",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ItemID",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ReceiptID",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "purchases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchases",
                table: "purchases",
                column: "PurchaseID");

            migrationBuilder.CreateTable(
                name: "ItemPurchase",
                columns: table => new
                {
                    ItemsItemID = table.Column<int>(type: "int", nullable: false),
                    PurchasesPurchaseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPurchase", x => new { x.ItemsItemID, x.PurchasesPurchaseID });
                    table.ForeignKey(
                        name: "FK_ItemPurchase_Items_ItemsItemID",
                        column: x => x.ItemsItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPurchase_purchases_PurchasesPurchaseID",
                        column: x => x.PurchasesPurchaseID,
                        principalTable: "purchases",
                        principalColumn: "PurchaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceipt",
                columns: table => new
                {
                    PurchasesPurchaseID = table.Column<int>(type: "int", nullable: false),
                    ReceiptsReceiptID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceipt", x => new { x.PurchasesPurchaseID, x.ReceiptsReceiptID });
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_Receipts_ReceiptsReceiptID",
                        column: x => x.ReceiptsReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_purchases_PurchasesPurchaseID",
                        column: x => x.PurchasesPurchaseID,
                        principalTable: "purchases",
                        principalColumn: "PurchaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchase_PurchasesPurchaseID",
                table: "ItemPurchase",
                column: "PurchasesPurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_ReceiptsReceiptID",
                table: "PurchaseReceipt",
                column: "ReceiptsReceiptID");
        }
    }
}
