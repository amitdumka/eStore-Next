using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobileX.Core.Database
{
    /// <summary>
    /// Database Basic structure
    /// </summary>
    public class AppDBContext : DatabaseContext
    {
        private string DatabasePath { get; set; }
        public AppDBContext()
        {
            DatabasePath = Constants.DatabasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();

        }
        public AppDBContext(string databasePath)
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
        private string ConnectionString = "";
        public AzureDBContext(string conn)
        {

        }
        public AzureDBContext()
        {

        }
    }

    public abstract class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}
