using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations.AzurePayrollDb
{
    public partial class cashdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "V1_CashDetails",
                columns: table => new
                {
                    CashDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    N2000 = table.Column<int>(type: "int", nullable: false),
                    N1000 = table.Column<int>(type: "int", nullable: false),
                    N500 = table.Column<int>(type: "int", nullable: false),
                    N200 = table.Column<int>(type: "int", nullable: false),
                    N100 = table.Column<int>(type: "int", nullable: false),
                    N50 = table.Column<int>(type: "int", nullable: false),
                    N20 = table.Column<int>(type: "int", nullable: false),
                    N10 = table.Column<int>(type: "int", nullable: false),
                    C10 = table.Column<int>(type: "int", nullable: false),
                    C5 = table.Column<int>(type: "int", nullable: false),
                    C2 = table.Column<int>(type: "int", nullable: false),
                    C1 = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    MarkedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_CashDetails", x => x.CashDetailId);
                    table.ForeignKey(
                        name: "FK_V1_CashDetails_V1_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "V1_Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_Customers",
                columns: table => new
                {
                    MobileNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    NoOfBills = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Customers", x => x.MobileNo);
                });

            migrationBuilder.CreateIndex(
                name: "IX_V1_CashDetails_StoreId",
                table: "V1_CashDetails",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V1_CashDetails");

            migrationBuilder.DropTable(
                name: "V1_Customers");
        }
    }
}
