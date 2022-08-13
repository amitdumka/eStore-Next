using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class saleitembug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_SaleItems_V1_Products_ProductItemBarcode",
                table: "V1_SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_SaleItems_V1_ProductSales_ProductSaleInvoiceCode",
                table: "V1_SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_SaleItems_ProductItemBarcode",
                table: "V1_SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_SaleItems_ProductSaleInvoiceCode",
                table: "V1_SaleItems");

            migrationBuilder.DropColumn(
                name: "ProductItemBarcode",
                table: "V1_SaleItems");

            migrationBuilder.DropColumn(
                name: "ProductSaleInvoiceCode",
                table: "V1_SaleItems");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceCode",
                table: "V1_SaleItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "V1_SaleItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "BasicAmount",
                table: "V1_SaleItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "V1_SaleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaxType",
                table: "V1_SaleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_V1_SaleItems_Barcode",
                table: "V1_SaleItems",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_SaleItems_InvoiceCode",
                table: "V1_SaleItems",
                column: "InvoiceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_SaleItems_V1_Products_Barcode",
                table: "V1_SaleItems",
                column: "Barcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_V1_SaleItems_V1_ProductSales_InvoiceCode",
                table: "V1_SaleItems",
                column: "InvoiceCode",
                principalTable: "V1_ProductSales",
                principalColumn: "InvoiceCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_SaleItems_V1_Products_Barcode",
                table: "V1_SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_SaleItems_V1_ProductSales_InvoiceCode",
                table: "V1_SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_SaleItems_Barcode",
                table: "V1_SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_V1_SaleItems_InvoiceCode",
                table: "V1_SaleItems");

            migrationBuilder.DropColumn(
                name: "BasicAmount",
                table: "V1_SaleItems");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "V1_SaleItems");

            migrationBuilder.DropColumn(
                name: "TaxType",
                table: "V1_SaleItems");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceCode",
                table: "V1_SaleItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "V1_SaleItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductItemBarcode",
                table: "V1_SaleItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSaleInvoiceCode",
                table: "V1_SaleItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_V1_SaleItems_ProductItemBarcode",
                table: "V1_SaleItems",
                column: "ProductItemBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_V1_SaleItems_ProductSaleInvoiceCode",
                table: "V1_SaleItems",
                column: "ProductSaleInvoiceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_SaleItems_V1_Products_ProductItemBarcode",
                table: "V1_SaleItems",
                column: "ProductItemBarcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_SaleItems_V1_ProductSales_ProductSaleInvoiceCode",
                table: "V1_SaleItems",
                column: "ProductSaleInvoiceCode",
                principalTable: "V1_ProductSales",
                principalColumn: "InvoiceCode");
        }
    }
}
