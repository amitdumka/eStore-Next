using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class stocks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MRP",
                table: "V1_Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "MultiPrice",
                table: "V1_Stocks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MRP",
                table: "V1_Stocks");

            migrationBuilder.DropColumn(
                name: "MultiPrice",
                table: "V1_Stocks");
        }
    }
}
