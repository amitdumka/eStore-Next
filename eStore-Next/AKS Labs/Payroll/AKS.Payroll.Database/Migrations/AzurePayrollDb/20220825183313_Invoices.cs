using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class Invoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerSales",
                columns: table => new
                {
                    InvoiceCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSales", x => x.InvoiceCode);
                    table.ForeignKey(
                        name: "FK_CustomerSales_V1_Customers_MobileNo",
                        column: x => x.MobileNo,
                        principalTable: "V1_Customers",
                        principalColumn: "MobileNo",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CustomerSales_V1_ProductSales_InvoiceCode",
                        column: x => x.InvoiceCode,
                        principalTable: "V1_ProductSales",
                        principalColumn: "InvoiceCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSales_MobileNo",
                table: "CustomerSales",
                column: "MobileNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerSales");
        }
    }
}
