using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class salaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "V1_Salaries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "V1_Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
