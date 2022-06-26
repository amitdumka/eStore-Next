using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class stocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_Stocks_V1_Products_ProductBarcode",
                table: "V1_Stocks");

            migrationBuilder.DropIndex(
                name: "IX_V1_Stocks_ProductBarcode",
                table: "V1_Stocks");

            migrationBuilder.DropColumn(
                name: "ProductBarcode",
                table: "V1_Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_Stocks_V1_Products_Barcode",
                table: "V1_Stocks",
                column: "Barcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_Stocks_V1_Products_Barcode",
                table: "V1_Stocks");

            migrationBuilder.AddColumn<string>(
                name: "ProductBarcode",
                table: "V1_Stocks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_V1_Stocks_ProductBarcode",
                table: "V1_Stocks",
                column: "ProductBarcode");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_Stocks_V1_Products_ProductBarcode",
                table: "V1_Stocks",
                column: "ProductBarcode",
                principalTable: "V1_Products",
                principalColumn: "Barcode");
        }
    }
}
