using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class Banking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "V1_Banks",
                columns: table => new
                {
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "V1_ChequeeBooks",
                columns: table => new
                {
                    ChequeBookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingNumber = table.Column<long>(type: "bigint", nullable: false),
                    EndingNumber = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    NoOfChequeIssued = table.Column<int>(type: "int", nullable: false),
                    NoOfPDC = table.Column<int>(type: "int", nullable: false),
                    NoOfClearedCheques = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_ChequeeBooks", x => x.ChequeBookId);
                    table.ForeignKey(
                        name: "FK_V1_ChequeeBooks_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_ChequeeIssued",
                columns: table => new
                {
                    ChequeIssuedId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InFavourOf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeBookId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeNumber = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_ChequeeIssued", x => x.ChequeIssuedId);
                    table.ForeignKey(
                        name: "FK_V1_ChequeeIssued_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_ChequeeLogs",
                columns: table => new
                {
                    ChequeLogId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InFavourOf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeIssuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeNumber = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_ChequeeLogs", x => x.ChequeLogId);
                    table.ForeignKey(
                        name: "FK_V1_ChequeeLogs_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_BankAccountList",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SharedAccount = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_BankAccountList", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_V1_BankAccountList_V1_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "V1_Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_BankAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DefaultBank = table.Column<bool>(type: "bit", nullable: false),
                    SharedAccount = table.Column<bool>(type: "bit", nullable: false),
                    OpenningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpenningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoreId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_BankAccounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_V1_BankAccounts_V1_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "V1_Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_VendorBankAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpenningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoreId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_VendorBankAccounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_V1_VendorBankAccounts_V1_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "V1_Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_V1_BankAccountList_BankId",
                table: "V1_BankAccountList",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_BankAccounts_BankId",
                table: "V1_BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ChequeeBooks_StoreId",
                table: "V1_ChequeeBooks",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ChequeeIssued_StoreId",
                table: "V1_ChequeeIssued",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_ChequeeLogs_StoreId",
                table: "V1_ChequeeLogs",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_VendorBankAccounts_BankId",
                table: "V1_VendorBankAccounts",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V1_BankAccountList");

            migrationBuilder.DropTable(
                name: "V1_BankAccounts");

            migrationBuilder.DropTable(
                name: "V1_ChequeeBooks");

            migrationBuilder.DropTable(
                name: "V1_ChequeeIssued");

            migrationBuilder.DropTable(
                name: "V1_ChequeeLogs");

            migrationBuilder.DropTable(
                name: "V1_VendorBankAccounts");

            migrationBuilder.DropTable(
                name: "V1_Banks");
        }
    }
}
