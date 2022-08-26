using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using Microsoft.EntityFrameworkCore;


namespace AKS.Databases.PosBilling
{
    internal class PosDbContext : DbContext
    {
        private string ConnectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";
        private bool azure = false;

        public PosDbContext()
        {
            ApplyMigrations(this);
        }

        public PosDbContext(string connectionString, bool remote)
        {
            azure = remote;
            ConnectionString = connectionString;
            ApplyMigrations(this);
        }

        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
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

        public void ApplyMigrations(PosDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        //Common Table which will be shared accross Databases.
        public DbSet<Store> Stores { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<EDCTerminal> EDCTerminals { get; set; }
        public DbSet<DueRecovery> DueRecovery { get; set; }
        public DbSet<CustomerDue> CustomerDues { get; set; }
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

    }
}
