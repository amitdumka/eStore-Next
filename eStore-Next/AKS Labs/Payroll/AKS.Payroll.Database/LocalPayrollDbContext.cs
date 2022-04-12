using Microsoft.EntityFrameworkCore;

//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Database
{
    public class LocalPayrollDbContext : PayrollDbContext
    {
        private static readonly string _connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = eStoreDb_Local_Ver1_0; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        //"Server=(localdb)\\mssqllocaldb;Database=eStoreDb_Local_Ver1_0;Trusted_Connection=True;MultipleActiveResultSets=true";

        public LocalPayrollDbContext() : base(_connectionString, false)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}