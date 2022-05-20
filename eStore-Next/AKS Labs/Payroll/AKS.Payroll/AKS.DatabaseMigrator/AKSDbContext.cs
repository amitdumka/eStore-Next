using Microsoft.EntityFrameworkCore;
using AKS.Shared.Commons.Models.Accounts;
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
        public DbSet<Shared.Commons.Models.Store> Stores { get; set; }
        public DbSet<Shared.Commons.Models.Salesman> Salesmen { get; set; }

        public DbSet<Shared.Commons.Models.Accounts.Voucher> Vouchers { get; set; }
        public DbSet <Shared.Commons.Models.Accounts.CashVoucher> CashVouchers { get; set; }

        public DbSet<Party> Parties { get; set; }
        public DbSet<LedgerGroup> LedgerGroups { get; set; }
        public DbSet<LedgerMaster> LedgerMasters { get; set; }
        public DbSet<TranscationMode> TranscationModes { get; set; }
        public DbSet<AKS.Shared.Commons.Models.Banking.BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDatabaseMaxSize("2 GB");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
                //string connectionString = String.IsNullOrEmpty( ConfigurationManager.ConnectionStrings["AzureDb"].ConnectionString)? "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654": ConfigurationManager.ConnectionStrings["AzureDb"].ConnectionString;
                string connectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";

                optionsBuilder.UseSqlServer(connectionString);

            }

        }

    }
}