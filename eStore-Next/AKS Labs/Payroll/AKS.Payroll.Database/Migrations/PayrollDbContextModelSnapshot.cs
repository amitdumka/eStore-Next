﻿// <auto-generated />
using System;
using AKS.Payroll.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AKS.Payroll.Database.Migrations
{
    [DbContext(typeof(PayrollDbContext))]
    partial class PayrollDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);
            SqlServerModelBuilderExtensions.HasDatabaseMaxSize(modelBuilder, "2 GB");

            modelBuilder.Entity("AKS.Shared.Payroll.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"), 1L, 1);

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntryStatus")
                        .HasColumnType("int");

                    b.Property<string>("EntryTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTailoring")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttendanceId");

                    b.ToTable("V1_Attendance");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsTailors")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWorking")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoiningDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("V1_Employee");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.EmployeeDetails", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdharNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntryStatus")
                        .HasColumnType("int");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighestQualification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("bit");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherIdDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PanNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("EmployeeDetails");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.MonthlyAttendance", b =>
                {
                    b.Property<int>("MonthlyAttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonthlyAttendanceId"), 1L, 1);

                    b.Property<int>("Absent")
                        .HasColumnType("int");

                    b.Property<decimal>("BillableDays")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CasualLeave")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EntryStatus")
                        .HasColumnType("int");

                    b.Property<int>("HalfDay")
                        .HasColumnType("int");

                    b.Property<int>("Holidays")
                        .HasColumnType("int");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("bit");

                    b.Property<int>("NoOfWorkingDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("OnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaidLeave")
                        .HasColumnType("int");

                    b.Property<int>("Present")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("Sunday")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeeklyLeave")
                        .HasColumnType("int");

                    b.HasKey("MonthlyAttendanceId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("V1_MonthlyAttendance");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.Salary", b =>
                {
                    b.Property<int>("SalaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryId"), 1L, 1);

                    b.Property<decimal>("BasicSalary")
                        .HasColumnType("money");

                    b.Property<DateTime?>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EntryStatus")
                        .HasColumnType("int");

                    b.Property<bool>("FullMonth")
                        .HasColumnType("bit");

                    b.Property<bool>("Incentive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEffective")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTailoring")
                        .HasColumnType("bit");

                    b.Property<bool>("LastPcs")
                        .HasColumnType("bit");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<bool>("SundayBillable")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WowBill")
                        .HasColumnType("bit");

                    b.HasKey("SalaryId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Salarys");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.SalaryPayment", b =>
                {
                    b.Property<int>("SalaryPaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryPaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PayMode")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SalaryComponet")
                        .HasColumnType("int");

                    b.Property<int>("SalaryMonth")
                        .HasColumnType("int");

                    b.HasKey("SalaryPaymentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("V1_SalaryPayment");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.StaffAdvanceReceipt", b =>
                {
                    b.Property<int>("StaffAdvanceReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffAdvanceReceiptId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EntryStatus")
                        .HasColumnType("int");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("bit");

                    b.Property<int>("PayMode")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReceiptDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffAdvanceReceiptId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("V1_StaffAdvanceReceipt");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.MonthlyAttendance", b =>
                {
                    b.HasOne("AKS.Shared.Payroll.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.Salary", b =>
                {
                    b.HasOne("AKS.Shared.Payroll.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.SalaryPayment", b =>
                {
                    b.HasOne("AKS.Shared.Payroll.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AKS.Shared.Payroll.Models.StaffAdvanceReceipt", b =>
                {
                    b.HasOne("AKS.Shared.Payroll.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
