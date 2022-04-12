using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class payslip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "V1_PaySlips",
                columns: table => new
                {
                    PaySlipId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalaryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BasicSalaryRate = table.Column<decimal>(type: "money", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: false),
                    TotalPayableSalary = table.Column<decimal>(type: "money", nullable: false),
                    NoOfDaysPresent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkingDays = table.Column<int>(type: "int", nullable: false),
                    TotalSale = table.Column<decimal>(type: "money", nullable: false),
                    SaleIncentive = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillAmount = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillIncentive = table.Column<decimal>(type: "money", nullable: false),
                    LastPcsAmount = table.Column<decimal>(type: "money", nullable: false),
                    LastPCsIncentive = table.Column<decimal>(type: "money", nullable: false),
                    OthersIncentive = table.Column<decimal>(type: "money", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "money", nullable: false),
                    StandardDeductions = table.Column<decimal>(type: "money", nullable: false),
                    TDSDeductions = table.Column<decimal>(type: "money", nullable: false),
                    PFDeductions = table.Column<decimal>(type: "money", nullable: false),
                    AdvanceDeducations = table.Column<decimal>(type: "money", nullable: false),
                    OtherDeductions = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTailoring = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_PaySlips", x => x.PaySlipId);
                    table.ForeignKey(
                        name: "FK_V1_PaySlips_V1_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "V1_Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_V1_PaySlips_V1_Salaries_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "V1_Salaries",
                        principalColumn: "SalaryId");
                    table.ForeignKey(
                        name: "FK_V1_PaySlips_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_V1_PaySlips_EmployeeId",
                table: "V1_PaySlips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PaySlips_SalaryId",
                table: "V1_PaySlips",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_PaySlips_StoreId",
                table: "V1_PaySlips",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V1_PaySlips");
        }
    }
}
