using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class purchaseU2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_Products_ProductItemBarcode",
                table: "V1_PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_PurchaseItems_ProductItemBarcode",
                table: "V1_PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_PurchaseItems_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems");

            migrationBuilder.DropColumn(
                name: "ProductItemBarcode",
                table: "V1_PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PurchaseProductInwardNumber",
                table: "V1_PurchaseItems");

            migrationBuilder.AddColumn<string>(
                name: "Warehouse",
                table: "V1_PurchaseProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "InwardNumber",
                table: "V1_PurchaseItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "V1_PurchaseItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PurchaseItems_Barcode",
                table: "V1_PurchaseItems",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PurchaseItems_InwardNumber",
                table: "V1_PurchaseItems",
                column: "InwardNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_Products_Barcode",
                table: "V1_PurchaseItems",
                column: "Barcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_InwardNumber",
                table: "V1_PurchaseItems",
                column: "InwardNumber",
                principalTable: "V1_PurchaseProducts",
                principalColumn: "InwardNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_Products_Barcode",
                table: "V1_PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_InwardNumber",
                table: "V1_PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_PurchaseItems_Barcode",
                table: "V1_PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_PurchaseItems_InwardNumber",
                table: "V1_PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Warehouse",
                table: "V1_PurchaseProducts");

            migrationBuilder.AlterColumn<string>(
                name: "InwardNumber",
                table: "V1_PurchaseItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "V1_PurchaseItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductItemBarcode",
                table: "V1_PurchaseItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseProductInwardNumber",
                table: "V1_PurchaseItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PurchaseItems_ProductItemBarcode",
                table: "V1_PurchaseItems",
                column: "ProductItemBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PurchaseItems_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems",
                column: "PurchaseProductInwardNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_Products_ProductItemBarcode",
                table: "V1_PurchaseItems",
                column: "ProductItemBarcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems",
                column: "PurchaseProductInwardNumber",
                principalTable: "V1_PurchaseProducts",
                principalColumn: "InwardNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
