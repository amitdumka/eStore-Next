using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AKS.Payroll.Database
{
    public class PayrollDbContext:DbContext
    {
        public PayrollDbContext() { }
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
            //ApplyMigrations(this);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<MonthlyAttendance> MonthlyAttendances { get; set; }
        public DbSet<Salary> Salarys { get; set; }
        public DbSet<SalaryPayment> SalaryPayment { get; set; } 
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipt { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }

        }

    }
}