using System.Runtime.CompilerServices;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AKS.MAUI.Databases
{
    public class MUAIConstant
    {
        public const string DatabaseFile = "eStoreDBSqlite.db3";
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFile);
            }
        }
    }


    
    // All the code in this file is included in all platforms.
    public class AppDBContext : DbContext
    {
        //Remote Azure Db
        private static readonly string _connectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";
        private string DatabasePath { get; set; }
        DBType _dbType;

        public AppDBContext()
        {

        }

        public AppDBContext(DBType dBType)
        {
            _dbType = dBType;
            if (dBType == DBType.Local)
            {
                DatabasePath = MUAIConstant.DatabasePath;
                SQLitePCL.Batteries_V2.Init();
                this.Database.EnsureCreated();
            }
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (_dbType == DBType.Local)
            {
                optionsBuilder.UseSqlite($"Filename={DatabasePath}");
            }
            else if (_dbType == DBType.Azure)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (_dbType == DBType.Azure)
                modelBuilder.HasDatabaseMaxSize("2 GB");
        }

        public void ApplyMigrations(AppDBContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }


        //tables
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
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
       // public DbSet<Store> Stores { get; set; }

        public DbSet<Salesman> Salesmen { get; set; }
        //public DbSet<LocalUser> LocalUsers { get; set; }
        //public DbSet<User> Users { get; set; }

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
}