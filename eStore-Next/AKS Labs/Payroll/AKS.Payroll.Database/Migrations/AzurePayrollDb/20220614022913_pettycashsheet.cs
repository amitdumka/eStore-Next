using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class pettycashsheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "V1_PettyCashSheets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankWithdrawal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailySale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TailoringSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TailoringPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ManualSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonCashSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerRecovery = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecoveryList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptsNaration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptsTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentNaration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_PettyCashSheets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V1_PettyCashSheets");
        }
    }
}
