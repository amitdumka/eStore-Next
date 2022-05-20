using AKS.Shared.Commons.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobileX.Core.Database
{
    /// <summary>
    /// Database Basic structure
    /// </summary>
    public class AppDBContext:DbContext
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

        public DbSet<User> Users { get; set; }
        
    }
}
