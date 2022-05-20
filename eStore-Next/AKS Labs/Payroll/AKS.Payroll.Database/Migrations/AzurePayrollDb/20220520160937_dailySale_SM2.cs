using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class dailySale_SM2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SalesmanId",
                table: "V1_DailySales",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EDCTerminalId",
                table: "V1_DailySales",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_V1_DailySales_EDCTerminalId",
                table: "V1_DailySales",
                column: "EDCTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_DailySales_SalesmanId",
                table: "V1_DailySales",
                column: "SalesmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_DailySales_V1_EDCMachine_EDCTerminalId",
                table: "V1_DailySales",
                column: "EDCTerminalId",
                principalTable: "V1_EDCMachine",
                principalColumn: "EDCTerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_DailySales_V1_Salesmen_SalesmanId",
                table: "V1_DailySales",
                column: "SalesmanId",
                principalTable: "V1_Salesmen",
                principalColumn: "SalesmanId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_DailySales_V1_EDCMachine_EDCTerminalId",
                table: "V1_DailySales");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_DailySales_V1_Salesmen_SalesmanId",
                table: "V1_DailySales");

            migrationBuilder.DropIndex(
                name: "IX_V1_DailySales_EDCTerminalId",
                table: "V1_DailySales");

            migrationBuilder.DropIndex(
                name: "IX_V1_DailySales_SalesmanId",
                table: "V1_DailySales");

            migrationBuilder.AlterColumn<string>(
                name: "SalesmanId",
                table: "V1_DailySales",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EDCTerminalId",
                table: "V1_DailySales",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
