using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("SqlServer:EditionOptions", "MAXSIZE = 2 GB");

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdharNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherIdDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighestQualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "V1_Attendance",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EntryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTailoring = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Attendance", x => x.AttendanceId);
                });

            migrationBuilder.CreateTable(
                name: "V1_Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeavingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    IsTailors = table.Column<bool>(type: "bit", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Salarys",
                columns: table => new
                {
                    SalaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEffective = table.Column<bool>(type: "bit", nullable: false),
                    WowBill = table.Column<bool>(type: "bit", nullable: false),
                    Incentive = table.Column<bool>(type: "bit", nullable: false),
                    LastPcs = table.Column<bool>(type: "bit", nullable: false),
                    SundayBillable = table.Column<bool>(type: "bit", nullable: false),
                    FullMonth = table.Column<bool>(type: "bit", nullable: false),
                    IsTailoring = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salarys", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_Salarys_V1_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "V1_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_MonthlyAttendance",
                columns: table => new
                {
                    MonthlyAttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Present = table.Column<int>(type: "int", nullable: false),
                    HalfDay = table.Column<int>(type: "int", nullable: false),
                    Sunday = table.Column<int>(type: "int", nullable: false),
                    PaidLeave = table.Column<int>(type: "int", nullable: false),
                    CasualLeave = table.Column<int>(type: "int", nullable: false),
                    Absent = table.Column<int>(type: "int", nullable: false),
                    WeeklyLeave = table.Column<int>(type: "int", nullable: false),
                    Holidays = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfWorkingDays = table.Column<int>(type: "int", nullable: false),
                    BillableDays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_MonthlyAttendance", x => x.MonthlyAttendanceId);
                    table.ForeignKey(
                        name: "FK_V1_MonthlyAttendance_V1_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "V1_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_SalaryPayment",
                columns: table => new
                {
                    SalaryPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalaryMonth = table.Column<int>(type: "int", nullable: false),
                    SalaryComponet = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_SalaryPayment", x => x.SalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_V1_SalaryPayment_V1_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "V1_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "V1_StaffAdvanceReceipt",
                columns: table => new
                {
                    StaffAdvanceReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V1_StaffAdvanceReceipt", x => x.StaffAdvanceReceiptId);
                    table.ForeignKey(
                        name: "FK_V1_StaffAdvanceReceipt_V1_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "V1_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_EmployeeId",
                table: "Salarys",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_MonthlyAttendance_EmployeeId",
                table: "V1_MonthlyAttendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_SalaryPayment_EmployeeId",
                table: "V1_SalaryPayment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_V1_StaffAdvanceReceipt_EmployeeId",
                table: "V1_StaffAdvanceReceipt",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "Salarys");

            migrationBuilder.DropTable(
                name: "V1_Attendance");

            migrationBuilder.DropTable(
                name: "V1_MonthlyAttendance");

            migrationBuilder.DropTable(
                name: "V1_SalaryPayment");

            migrationBuilder.DropTable(
                name: "V1_StaffAdvanceReceipt");

            migrationBuilder.DropTable(
                name: "V1_Employee");
        }
    }
}
