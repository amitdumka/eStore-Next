using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections;
using System.Configuration;
//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Database
{
    public class PayrollDbContext:DbContext
    {
        public PayrollDbContext() { ApplyMigrations(this); }
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
            ApplyMigrations(this);
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
        public DbSet<Salary> Salarys { get; set; }
        public DbSet<SalaryPayment> SalaryPayment { get; set; } 
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipt { get; set; }
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
                string connectionString =   "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654" ;

                optionsBuilder.UseSqlServer(connectionString);

            }

        }

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