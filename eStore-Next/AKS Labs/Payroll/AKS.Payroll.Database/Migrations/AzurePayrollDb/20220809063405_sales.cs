using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_CardPaymentDetails_V1_EDCMachine_EDCTerminalId",
                table: "V1_CardPaymentDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "RoundOff",
                table: "V1_ProductSales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBasicAmount",
                table: "V1_ProductSales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMRP",
                table: "V1_ProductSales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "EDCTerminalId",
                table: "V1_CardPaymentDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_CardPaymentDetails_V1_EDCMachine_EDCTerminalId",
                table: "V1_CardPaymentDetails",
                column: "EDCTerminalId",
                principalTable: "V1_EDCMachine",
                principalColumn: "EDCTerminalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_CardPaymentDetails_V1_EDCMachine_EDCTerminalId",
                table: "V1_CardPaymentDetails");

            migrationBuilder.DropColumn(
                name: "RoundOff",
                table: "V1_ProductSales");

            migrationBuilder.DropColumn(
                name: "TotalBasicAmount",
                table: "V1_ProductSales");

            migrationBuilder.DropColumn(
                name: "TotalMRP",
                table: "V1_ProductSales");

            migrationBuilder.AlterColumn<string>(
                name: "EDCTerminalId",
                table: "V1_CardPaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_V1_CardPaymentDetails_V1_EDCMachine_EDCTerminalId",
                table: "V1_CardPaymentDetails",
                column: "EDCTerminalId",
                principalTable: "V1_EDCMachine",
                principalColumn: "EDCTerminalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
