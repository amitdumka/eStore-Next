using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class vouchersver2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashVouchers",
                table: "CashVouchers");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "Vouchers",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "Notes",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "CashVouchers",
                newName: "StoreId");

            migrationBuilder.AddColumn<string>(
                name: "VoucherNumber",
                table: "Vouchers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EntryStatus",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadOnly",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarkedDeleted",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PartyId",
                table: "Vouchers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoteNumber",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EntryStatus",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadOnly",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarkedDeleted",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PartyId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VoucherNumber",
                table: "CashVouchers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EntryStatus",
                table: "CashVouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadOnly",
                table: "CashVouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarkedDeleted",
                table: "CashVouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PartyId",
                table: "CashVouchers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CashVouchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "VoucherNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NoteNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashVouchers",
                table: "CashVouchers",
                column: "VoucherNumber");

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    PartyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.PartyId);
                    table.ForeignKey(
                        name: "FK_Party_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_PartyId",
                table: "Vouchers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_StoreId",
                table: "Vouchers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PartyId",
                table: "Notes",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_StoreId",
                table: "Notes",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CashVouchers_PartyId",
                table: "CashVouchers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_CashVouchers_StoreId",
                table: "CashVouchers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_StoreId",
                table: "Party",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashVouchers_Party_PartyId",
                table: "CashVouchers",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CashVouchers_V1_Stores_StoreId",
                table: "CashVouchers",
                column: "StoreId",
                principalTable: "V1_Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Party_PartyId",
                table: "Notes",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_V1_Stores_StoreId",
                table: "Notes",
                column: "StoreId",
                principalTable: "V1_Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Party_PartyId",
                table: "Vouchers",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_V1_Stores_StoreId",
                table: "Vouchers",
                column: "StoreId",
                principalTable: "V1_Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashVouchers_Party_PartyId",
                table: "CashVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_CashVouchers_V1_Stores_StoreId",
                table: "CashVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Party_PartyId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_V1_Stores_StoreId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Party_PartyId",
                table: "Vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_V1_Stores_StoreId",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_PartyId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_StoreId",
                table: "Vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_PartyId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_StoreId",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashVouchers",
                table: "CashVouchers");

            migrationBuilder.DropIndex(
                name: "IX_CashVouchers_PartyId",
                table: "CashVouchers");

            migrationBuilder.DropIndex(
                name: "IX_CashVouchers_StoreId",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "VoucherNumber",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "EntryStatus",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsReadOnly",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "MarkedDeleted",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "NoteNumber",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "EntryStatus",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsReadOnly",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "MarkedDeleted",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "VoucherNumber",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "EntryStatus",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "IsReadOnly",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "MarkedDeleted",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "CashVouchers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CashVouchers");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Vouchers",
                newName: "VoucherId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Notes",
                newName: "NoteId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "CashVouchers",
                newName: "VoucherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "VoucherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashVouchers",
                table: "CashVouchers",
                column: "VoucherId");
        }
    }
}
