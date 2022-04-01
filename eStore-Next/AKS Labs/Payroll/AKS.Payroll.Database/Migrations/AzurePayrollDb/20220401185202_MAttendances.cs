using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class MAttendances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillableDays",
                table: "V1_MonthlyAttendances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BillableDays",
                table: "V1_MonthlyAttendances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
