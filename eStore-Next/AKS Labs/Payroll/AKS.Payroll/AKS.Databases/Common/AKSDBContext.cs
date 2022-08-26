using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Databases.Common
{
    internal class AKSDBContext : DbContext
    {
        private string ConnectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";
        private bool azure = false;

        public AKSDBContext()
        {
            ApplyMigrations(this);
        }

        public AKSDBContext(string connectionString, bool remote)
        {
            azure = remote;
            ConnectionString = connectionString;
            ApplyMigrations(this);
        }

        public AKSDBContext(DbContextOptions<AKSDBContext> options) : base(options)
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

        public void ApplyMigrations(AKSDBContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

    }
}

