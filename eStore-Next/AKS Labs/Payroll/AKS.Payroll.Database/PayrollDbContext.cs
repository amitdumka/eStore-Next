using AKS.Shared.Commons.Models;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Database
{
    public class LocalPayrollDbContext : PayrollDbContext
    {
        public LocalPayrollDbContext()
        { ApplyMigrations(this); }

        public LocalPayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
            ApplyMigrations(this);
        }

        public void ApplyMigrations(LocalPayrollDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

    public class AzurePayrollDbContext : PayrollDbContext
    {
        public AzurePayrollDbContext()
        { ApplyMigrations(this); }

        public AzurePayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
            ApplyMigrations(this);
        }

        public void ApplyMigrations(AzurePayrollDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

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

    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext()
        {

        }

        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<MonthlyAttendance> MonthlyAttendances { get; set; }
        public DbSet<Salary> Salarys { get; set; }
        public DbSet<SalaryPayment> SalaryPayment { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipt { get; set; }

        //Common Table which will be shared accross Databases.
        public DbSet<Store> Stores { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }

        
    }

    public class ObservableListSource<T> : ObservableCollection<T>, IListSource
            where T : class
    {
        private IBindingList _bindingList;

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList()
        {
            return _bindingList ?? (_bindingList = this.ToBindingList());
        }
    }
}