using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobileX.Core.Database
{
    /// <summary>
    /// Database Basic structure
    /// </summary>
    public class AppDBContext : DatabaseContext
    {
        private string DatabasePath { get; set; }
        public AppDBContext() : base()
        {
            DatabasePath = Constants.DatabasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();

        }
        public AppDBContext(string databasePath) : base()
        {
            DatabasePath = databasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Filename={DatabasePath}");



    }

    public class AzureDBContext : DatabaseContext
    {
        public AzureDBContext(DbContextOptions<AzureDBContext> options) : base(options)
        {
            // ApplyMigrations(this);
        }

        public AzureDBContext(string con) : base()
        {

        }

        private static readonly string _connectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";

        public AzureDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDatabaseMaxSize("2 GB");
        }

        public void ApplyMigrations(AzureDBContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }



    }

    public abstract class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<AzureDBContext> options) : base(options)
        {

        }

    }
}
