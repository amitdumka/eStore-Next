using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class pType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "ProductSubCategories");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeId",
                table: "V1_Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ProductTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_V1_Products_ProductTypeId",
                table: "V1_Products",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_V1_Products_ProductTypes_ProductTypeId",
                table: "V1_Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_V1_Products_ProductTypes_ProductTypeId",
                table: "V1_Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_V1_Products_ProductTypeId",
                table: "V1_Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "V1_Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "ProductSubCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
