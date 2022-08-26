using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;

namespace AKS.Databases.Payroll
{
    internal class PayrollDbContext:DbContext
    {
        private string ConnectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";
        private bool azure = false;

        public PayrollDbContext()
        {
            ApplyMigrations(this);
        }

        public PayrollDbContext(string connectionString, bool remote)
        {
            azure = remote;
            ConnectionString = connectionString;
            ApplyMigrations(this);
        }

        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
            ApplyMigrations(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (azure)
                modelBuilder.HasDatabaseMaxSize("2 GB");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
                //string connectionString = String.IsNullOrEmpty( ConfigurationManager.ConnectionStrings["AzureDb"].ConnectionString)? "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654": ConfigurationManager.ConnectionStrings["AzureDb"].ConnectionString;
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        public void ApplyMigrations(PayrollDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<MonthlyAttendance> MonthlyAttendances { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<SalaryPayment> SalaryPayment { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipt { get; set; }

        public DbSet<PaySlip> PaySlips { get; set; }

        public DbSet<SalaryLedger> SalaryLedgers { get; set; }

        //Common Table which will be shared accross Databases.
        public DbSet<Store> Stores { get; set; }

        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
