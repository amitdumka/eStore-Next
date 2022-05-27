using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace AKS.Database
{
    public static class Constants
    {
        public const string DatabaseFilename = "aksDBVer001.db3";
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
    // All the code in this file is included in all platforms.
    public class AKSDBContext:DatabaseContext
    {
        private string DatabasePath { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        public AKSDBContext()
        {
            DatabasePath = Constants.DatabasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }
        public AKSDBContext(string databasePath)
        {
            DatabasePath = databasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

    }

    public class AzureDbContext : DatabaseContext
    {
        public AzureDbContext()
        {

        }
        public AzureDbContext (string connectionString)
        {

        }
    }


    public abstract class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        // public DbSet<Salesman> Salesmen { get; set; }
    }
}