using System.Runtime.CompilerServices;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
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


    public enum DBType { Local, Azure, Remote, Mango, Others }
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
    }
}