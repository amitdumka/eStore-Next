using Microsoft.EntityFrameworkCore;

namespace AKS.DatabaseMigrator
{
    public class AKSDbContext : DbContext
    {
        public DbSet<Shared.Payroll.Models.Employee> Employees { get; set; }
        public DbSet<Shared.Payroll.Models.Attendance> Attendances { get; set; }
        public DbSet<Shared.Payroll.Models.MonthlyAttendance> MonthlyAttendances { get; set; }
        public DbSet<Shared.Payroll.Models.Salary> Salarys { get; set; }
        public DbSet<Shared.Payroll.Models.SalaryPayment> SalaryPayment { get; set; }
        public DbSet<Shared.Payroll.Models.EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<Shared.Payroll.Models.StaffAdvanceReceipt> StaffAdvanceReceipt { get; set; }

    }
}