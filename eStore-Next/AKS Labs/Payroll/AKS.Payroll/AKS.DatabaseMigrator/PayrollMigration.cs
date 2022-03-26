using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.DatabaseMigrator
{
    public  class PayrollMigration
    {
        private eStoreDbContext db;
        private AKSDbContext AKS;
        public PayrollMigration(eStoreDbContext oldDB, AKSDbContext newDB)
        {
            db = oldDB; AKS = newDB;
        }
        public void Migrate()
        {

        }

        private void MigrateEmployee() { }
        private void MigrateSalary() { }
        private void MigrateAttendance() { }


    }
}
