using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class dailySale_SM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalemanId",
                table: "V1_DailySales",
                newName: "SalesmanId");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolderName",
                table: "V1_VendorBankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ChequeBookId",
                table: "V1_ChequeeIssued",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountAccountNumber",
                table: "V1_ChequeeIssued",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountAccountNumber",
                table: "V1_ChequeeBooks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountHolderName",
                table: "V1_BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBalance",
                table: "V1_BankAccounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AccountHolderName",
                table: "V1_BankAccountList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ChequeeIssued_BankAccountAccountNumber",
                table: "V1_ChequeeIssued",
                column: "BankAccountAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ChequeeIssued_ChequeBookId",
                table: "V1_ChequeeIssued",
                column: "ChequeBookId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ChequeeBooks_BankAccountAccountNumber",
                table: "V1_ChequeeBooks",
                column: "BankAccountAccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_ChequeeBooks_V1_BankAccounts_BankAccountAccountNumber",
                table: "V1_ChequeeBooks",
                column: "BankAccountAccountNumber",
                principalTable: "V1_BankAccounts",
                principalColumn: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_ChequeeIssued_V1_BankAccounts_BankAccountAccountNumber",
                table: "V1_ChequeeIssued",
                column: "BankAccountAccountNumber",
                principalTable: "V1_BankAccounts",
                principalColumn: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_ChequeeIssued_V1_ChequeeBooks_ChequeBookId",
                table: "V1_ChequeeIssued",
                column: "ChequeBookId",
                principalTable: "V1_ChequeeBooks",
                principalColumn: "ChequeBookId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_ChequeeBooks_V1_BankAccounts_BankAccountAccountNumber",
                table: "V1_ChequeeBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_ChequeeIssued_V1_BankAccounts_BankAccountAccountNumber",
                table: "V1_ChequeeIssued");

            migrationBuilder.DropForeignKey(
                name: "FK_V1_ChequeeIssued_V1_ChequeeBooks_ChequeBookId",
                table: "V1_ChequeeIssued");

            migrationBuilder.DropIndex(
                name: "IX_V1_ChequeeIssued_BankAccountAccountNumber",
                table: "V1_ChequeeIssued");

            migrationBuilder.DropIndex(
                name: "IX_V1_ChequeeIssued_ChequeBookId",
                table: "V1_ChequeeIssued");

            migrationBuilder.DropIndex(
                name: "IX_V1_ChequeeBooks_BankAccountAccountNumber",
                table: "V1_ChequeeBooks");

            migrationBuilder.DropColumn(
                name: "AccountHolderName",
                table: "V1_VendorBankAccounts");

            migrationBuilder.DropColumn(
                name: "BankAccountAccountNumber",
                table: "V1_ChequeeIssued");

            migrationBuilder.DropColumn(
                name: "BankAccountAccountNumber",
                table: "V1_ChequeeBooks");

            migrationBuilder.DropColumn(
                name: "AccountHolderName",
                table: "V1_BankAccounts");

            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "V1_BankAccounts");

            migrationBuilder.DropColumn(
                name: "AccountHolderName",
                table: "V1_BankAccountList");

            migrationBuilder.RenameColumn(
                name: "SalesmanId",
                table: "V1_DailySales",
                newName: "SalemanId");

            migrationBuilder.AlterColumn<string>(
                name: "ChequeBookId",
                table: "V1_ChequeeIssued",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
