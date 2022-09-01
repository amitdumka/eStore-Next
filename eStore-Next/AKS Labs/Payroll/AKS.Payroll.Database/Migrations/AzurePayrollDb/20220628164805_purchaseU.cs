using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class purchaseU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_V1_PurchaseProducts",
                table: "V1_PurchaseProducts");

            migrationBuilder.RenameColumn(
                name: "PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems",
                newName: "PurchaseProductInwardNumber");

            migrationBuilder.RenameColumn(
                name: "InvoiceNo",
                table: "V1_PurchaseItems",
                newName: "InwardNumber");

            migrationBuilder.RenameIndex(
                name: "IX_V1_PurchaseItems_PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems",
                newName: "IX_V1_PurchaseItems_PurchaseProductInwardNumber");

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "V1_Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "V1_PurchaseProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "InwardNumber",
                table: "V1_PurchaseProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "InwardDate",
                table: "V1_PurchaseProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BrandCode",
                table: "V1_Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_V1_PurchaseProducts",
                table: "V1_PurchaseProducts",
                column: "InwardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_V1_Products_BrandCode",
                table: "V1_Products",
                column: "BrandCode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_Products_V1_Brands_BrandCode",
                table: "V1_Products",
                column: "BrandCode",
                principalTable: "V1_Brands",
                principalColumn: "BrandCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems",
                column: "PurchaseProductInwardNumber",
                principalTable: "V1_PurchaseProducts",
                principalColumn: "InwardNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_Products_V1_Brands_BrandCode",
                table: "V1_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_V1_PurchaseProducts",
                table: "V1_PurchaseProducts");

            migrationBuilder.DropIndex(
                name: "IX_V1_Products_BrandCode",
                table: "V1_Products");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "V1_Stocks");

            migrationBuilder.DropColumn(
                name: "InwardNumber",
                table: "V1_PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "InwardDate",
                table: "V1_PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "BrandCode",
                table: "V1_Products");

            migrationBuilder.RenameColumn(
                name: "PurchaseProductInwardNumber",
                table: "V1_PurchaseItems",
                newName: "PurchaseProductInvoiceNo");

            migrationBuilder.RenameColumn(
                name: "InwardNumber",
                table: "V1_PurchaseItems",
                newName: "InvoiceNo");

            migrationBuilder.RenameIndex(
                name: "IX_V1_PurchaseItems_PurchaseProductInwardNumber",
                table: "V1_PurchaseItems",
                newName: "IX_V1_PurchaseItems_PurchaseProductInvoiceNo");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "V1_PurchaseProducts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_V1_PurchaseProducts",
                table: "V1_PurchaseProducts",
                column: "InvoiceNo");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_PurchaseItems_V1_PurchaseProducts_PurchaseProductInvoiceNo",
                table: "V1_PurchaseItems",
                column: "PurchaseProductInvoiceNo",
                principalTable: "V1_PurchaseProducts",
                principalColumn: "InvoiceNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
