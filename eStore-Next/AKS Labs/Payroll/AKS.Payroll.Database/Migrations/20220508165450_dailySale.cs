using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class dailySale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Vouchers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "CashVouchers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "V1_CustomerDues",
                columns: table => new
                {
                    InvoiceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    ClearingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_CustomerDues", x => x.InvoiceNumber);
                    table.ForeignKey(
                        name: "FK_V1_CustomerDues_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_DailySales",
                columns: table => new
                {
                    InvoiceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonCashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    SalemanId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDue = table.Column<bool>(type: "bit", nullable: false),
                    ManualBill = table.Column<bool>(type: "bit", nullable: false),
                    SalesReturn = table.Column<bool>(type: "bit", nullable: false),
                    TailoringBill = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EDCTerminalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_DailySales", x => x.InvoiceNumber);
                    table.ForeignKey(
                        name: "FK_V1_DailySales_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_EDCMachine",
                columns: table => new
                {
                    EDCTerminalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_EDCMachine", x => x.EDCTerminalId);
                    table.ForeignKey(
                        name: "FK_V1_EDCMachine_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DueRecovery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticialPayment = table.Column<bool>(type: "bit", nullable: false),
                    DueInvoiceNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DueRecovery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DueRecovery_V1_CustomerDues_DueInvoiceNumber",
                        column: x => x.DueInvoiceNumber,
                        principalTable: "V1_CustomerDues",
                        principalColumn: "InvoiceNumber");
                    table.ForeignKey(
                        name: "FK_DueRecovery_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_EmployeeId",
                table: "Vouchers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashVouchers_EmployeeId",
                table: "CashVouchers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DueRecovery_DueInvoiceNumber",
                table: "DueRecovery",
                column: "DueInvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DueRecovery_StoreId",
                table: "DueRecovery",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_CustomerDues_StoreId",
                table: "V1_CustomerDues",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_DailySales_StoreId",
                table: "V1_DailySales",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_EDCMachine_StoreId",
                table: "V1_EDCMachine",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashVouchers_V1_Employees_EmployeeId",
                table: "CashVouchers",
                column: "EmployeeId",
                principalTable: "V1_Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_V1_Employees_EmployeeId",
                table: "Vouchers",
                column: "EmployeeId",
                principalTable: "V1_Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashVouchers_V1_Employees_EmployeeId",
                table: "CashVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_V1_Employees_EmployeeId",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "DueRecovery");

            migrationBuilder.DropTable(
                name: "V1_DailySales");

            migrationBuilder.DropTable(
                name: "V1_EDCMachine");

            migrationBuilder.DropTable(
                name: "V1_CustomerDues");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_EmployeeId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_CashVouchers_EmployeeId",
                table: "CashVouchers");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "CashVouchers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
