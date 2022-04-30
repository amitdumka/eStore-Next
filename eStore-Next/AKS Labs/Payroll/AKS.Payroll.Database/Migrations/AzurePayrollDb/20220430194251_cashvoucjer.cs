using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class cashvoucjer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashVouchers_V1_TranscationModes_TranscationModeTranscationId",
                table: "CashVouchers");

            migrationBuilder.DropIndex(
                name: "IX_CashVouchers_TranscationModeTranscationId",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "TranscationModeTranscationId",
                table: "CashVouchers");

            migrationBuilder.AlterColumn<string>(
                name: "TranscationId",
                table: "CashVouchers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CashVouchers_TranscationId",
                table: "CashVouchers",
                column: "TranscationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashVouchers_V1_TranscationModes_TranscationId",
                table: "CashVouchers",
                column: "TranscationId",
                principalTable: "V1_TranscationModes",
                principalColumn: "TranscationId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashVouchers_V1_TranscationModes_TranscationId",
                table: "CashVouchers");

            migrationBuilder.DropIndex(
                name: "IX_CashVouchers_TranscationId",
                table: "CashVouchers");

            migrationBuilder.AlterColumn<string>(
                name: "TranscationId",
                table: "CashVouchers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TranscationModeTranscationId",
                table: "CashVouchers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CashVouchers_TranscationModeTranscationId",
                table: "CashVouchers",
                column: "TranscationModeTranscationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashVouchers_V1_TranscationModes_TranscationModeTranscationId",
                table: "CashVouchers",
                column: "TranscationModeTranscationId",
                principalTable: "V1_TranscationModes",
                principalColumn: "TranscationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
