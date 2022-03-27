using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
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
<<<<<<< Updated upstream


=======
>>>>>>> Stashed changes
            modelBuilder.HasDatabaseMaxSize("2 GB");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
<<<<<<< Updated upstream




                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;");
       
=======
                optionsBuilder.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");

                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["mssql1"].ConnectionString);

>>>>>>> Stashed changes
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