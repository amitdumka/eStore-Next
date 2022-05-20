using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class salaryledger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiptDate",
                table: "V1_StaffAdvanceReceipts",
                newName: "OnDate");

            migrationBuilder.CreateTable(
                name: "SalaryLedgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Particulars = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryLedgers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryLedgers");

            migrationBuilder.RenameColumn(
                name: "OnDate",
                table: "V1_StaffAdvanceReceipts",
                newName: "ReceiptDate");
        }
    }
}
