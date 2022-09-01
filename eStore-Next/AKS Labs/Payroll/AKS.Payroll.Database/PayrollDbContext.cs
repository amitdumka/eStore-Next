using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Database
{

    public class PayrollDbContext : DbContext
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

        // Banking  Note: Some of Table will move to other part. 

        public DbSet<Bank> Banks { get; set; }
        public DbSet<VendorBankAccount> VendorBankAccounts { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountList> AccountLists { get; set; }
        public DbSet<ChequeBook> ChequeBooks { get; set; }
        public DbSet<ChequeIssued> ChequeIssued { get; set; }
        public DbSet<ChequeLog> ChequeLogs { get; set; }


        // Vouchers and Notes  Note: Some of the table will move to other parts
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<CashVoucher> CashVouchers { get; set; }
        public DbSet<Note> Notes { get; set; }

        public DbSet<TranscationMode> TranscationModes { get; set; }

        public DbSet<Party> Parties { get; set; }
        public DbSet<LedgerGroup> LedgerGroups { get; set; }
        public DbSet<LedgerMaster> LedgerMasters { get; set; }

        // Daily Sales
        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<EDCTerminal> EDCTerminals { get; set; }
        public DbSet<DueRecovery> DueRecovery { get; set; }
        public DbSet<CustomerDue> CustomerDues { get; set; }

        public DbSet<PettyCashSheet> PettyCashSheets { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<CashDetail> CashDetails { get; set; }

        //Inventory

        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<ProductSale> ProductSales { set; get; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<SalePaymentDetail> SalePaymentDetails { get; set; }
        public DbSet<CardPaymentDetail> CardPaymentDetails { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<CustomerSale> CustomerSales { get; set; }

        public DbSet<BankTranscation> BankTranscations { get; set; }

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