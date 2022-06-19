using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class inventorySystem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_ProductItems_V1_Products_ProductItemBarcode",
                table: "V1_ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_ProductItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                table: "V1_ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_Products_ProductSubCategories_ProductSubCategorySubCategory",
                table: "V1_Products");

            migrationBuilder.DropIndex(
                name: "IX_V1_Products_ProductSubCategorySubCategory",
                table: "V1_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_V1_ProductItems",
                table: "V1_ProductItems");

            migrationBuilder.DropColumn(
                name: "ProductSubCategorySubCategory",
                table: "V1_Products");

            migrationBuilder.RenameTable(
                name: "V1_ProductItems",
                newName: "V1_PurchaseItems");

            migrationBuilder.RenameIndex(
                name: "IX_V1_ProductItems_PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems",
                newName: "IX_V1_PurchaseItems_PurchaseProductInvoiceNo");

            migrationBuilder.RenameIndex(
                name: "IX_V1_ProductItems_ProductItemBarcode",
                table: "V1_PurchaseItems",
                newName: "IX_V1_PurchaseItems_ProductItemBarcode");

            migrationBuilder.AlterColumn<string>(
                name: "SubCategory",
                table: "V1_Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_V1_PurchaseItems",
                table: "V1_PurchaseItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "V1_Brands",
                columns: table => new
                {
                    BrandCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Brands", x => x.BrandCode);
                });

            migrationBuilder.CreateTable(
                name: "V2_Suppliers",
                columns: table => new
                {
                    SupplierName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Warehouse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V2_Suppliers", x => x.SupplierName);
                });

            migrationBuilder.CreateIndex(
                name: "IX_V1_Products_SubCategory",
                table: "V1_Products",
                column: "SubCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_Products_ProductSubCategories_SubCategory",
                table: "V1_Products",
                column: "SubCategory",
                principalTable: "ProductSubCategories",
                principalColumn: "SubCategory",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_Products_ProductItemBarcode",
                table: "V1_PurchaseItems",
                column: "ProductItemBarcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems",
                column: "PurchaseProductInvoiceNo",
                principalTable: "V1_PurchaseProducts",
                principalColumn: "InvoiceNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_Products_ProductSubCategories_SubCategory",
                table: "V1_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_Products_ProductItemBarcode",
                table: "V1_PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems");

            migrationBuilder.DropTable(
                name: "V1_Brands");

            migrationBuilder.DropTable(
                name: "V2_Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_V1_Products_SubCategory",
                table: "V1_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_V1_PurchaseItems",
                table: "V1_PurchaseItems");

            migrationBuilder.RenameTable(
                name: "V1_PurchaseItems",
                newName: "V1_ProductItems");

            migrationBuilder.RenameIndex(
                name: "IX_V1_PurchaseItems_PurchaseProductInvoiceNo",
                table: "V1_ProductItems",
                newName: "IX_V1_ProductItems_PurchaseProductInvoiceNo");

            migrationBuilder.RenameIndex(
                name: "IX_V1_PurchaseItems_ProductItemBarcode",
                table: "V1_ProductItems",
                newName: "IX_V1_ProductItems_ProductItemBarcode");

            migrationBuilder.AlterColumn<string>(
                name: "SubCategory",
                table: "V1_Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductSubCategorySubCategory",
                table: "V1_Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_V1_ProductItems",
                table: "V1_ProductItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_V1_Products_ProductSubCategorySubCategory",
                table: "V1_Products",
                column: "ProductSubCategorySubCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_ProductItems_V1_Products_ProductItemBarcode",
                table: "V1_ProductItems",
                column: "ProductItemBarcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_ProductItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                table: "V1_ProductItems",
                column: "PurchaseProductInvoiceNo",
                principalTable: "V1_PurchaseProducts",
                principalColumn: "InvoiceNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_V1_Products_ProductSubCategories_ProductSubCategorySubCategory",
                table: "V1_Products",
                column: "ProductSubCategorySubCategory",
                principalTable: "ProductSubCategories",
                principalColumn: "SubCategory",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
