using System;
using AKS.MAUI.Databases;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;

namespace eStore_MauiLib.RemoteService
{
	public class SyncService
	{
		AppDBContext db;//= new AppDBContext(DBType.Local);
		AppDBContext azure;// = new AppDBContext(DBType.Azure);

        public async Task<bool> SyncDownUsersAsync() {

			if(db==null)
			 db = new AppDBContext(DBType.Local);
			if(azure==null)
			 azure = new AppDBContext(DBType.Azure);

			var local = db.Users.Count();
			var remote = azure.Users.Count();
			if (local != remote)
			{
				var remoteUsers = azure.Users.ToList();
				int recordAdded = 0;
				foreach (var user in remoteUsers)
				{
					if (!db.Users.Any(c => c.UserName == user.UserName))
					{
						db.Users.Add(user);
						recordAdded++;
					}
				}
				int count = await db.SaveChangesAsync();

               

                if (count == recordAdded) {
                    Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true; } else {
                    Preferences.Remove(nameof(User));
                    return false;
                }
			}
			return true;

		}
		public async Task<bool> SyncDownTranscationsAsync() {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.TranscationModes.Count();
            var remote = azure.TranscationModes.Count();
            if (local != remote)
            {
                var TranscationModes = azure.TranscationModes.ToList();
                int recordAdded = 0;
                foreach (var mode in TranscationModes)
                {
                    if (!db.TranscationModes.Any(c => c.TranscationId == mode.TranscationId))
                    {
                        db.TranscationModes.Add(mode);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded) return true; else return false;
            }
            return true;
        }
		public async Task<bool> SyncDownEmployeesAsync() {

            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Employees.Count();
            var remote = azure.Employees.Count();
            if (local != remote)
            {
                var Employees = azure.EmployeeDetails.Include(c=>c.Employee).ToList();
                
                int recordAdded = 0;
                foreach (var emp in Employees)
                {
                    if (!db.EmployeeDetails.Any(c => c.EmployeeId == emp.EmployeeId))
                    {
                        db.EmployeeDetails.Add(emp);
                        recordAdded+=2;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded) return true; else return false;
            }
            return true;
        }
		public void SyncDownSalaries() { }
		public async Task<bool> SyncDownStoresAsync() {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Stores.Count();
            var remote = azure.Stores.Count();
            if (local != remote)
            {
                var Stores = azure.Stores.ToList();
                int recordAdded = 0;
                foreach (var Store in Stores)
                {
                    if (!db.Stores.Any(c => c.StoreCode == Store.StoreCode))
                    {
                        db.Stores.Add(Store);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded) return true; else return false;
            }
            return true;
        }
		public async Task<bool> SyncDownSalesmanAsync() {

            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Salesmen.Count();
            var remote = azure.Salesmen.Count();
            if (local != remote)
            {
                var sms = azure.Salesmen.ToList();
                int recordAdded = 0;
                foreach (var sm in sms)
                {
                    if (!db.Salesmen.Any(c => c.SalesmanId == sm.SalesmanId))
                    {
                        db.Salesmen.Add(sm);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded) return true; else return false;
            }
            return true;
        }


		public void SyncDownBanks() { }
		public void SyncDownBankAccounts() { }
		public void SyncDownVendorsAccount() { }
		public void SyncDownAccountList() { }

		public void SyncDownParties() { }
		public void SyncDownEdcTerminals() { }

		public void SyncDownProductItems() { }
		public void SyncDownStocks() { }

		public void SyncDownProductSubCategories() { }
		public void SyncDownBandandSuppliers() { }

		public void SyncDownProductTypes() { }


    }

    public class DatabaseStatus
    {

        public static bool VerifyLocalStatus()
        {
            var keyValue = Preferences.Get("Local", "NO");
            if (keyValue == "LocalSynced") return true;
            else return false;
        }
        public static void VerifyInitialSet()
        {
           var keyValue= Preferences.Get(nameof(User), "NO");
            if (keyValue != "NO")
            {
                var values = keyValue.Split('#');

            }

        }
        public static bool SyncInitial()
        {
            var sync = new SyncService();

            sync.SyncDownStoresAsync();
            sync.SyncDownUsersAsync();
            //sync.SyncDownSalesmanAsync();
            //sync.SyncDownEdcTerminals();
            //sync.SyncDownEdcTerminals();
            //sync.SyncDownEmployeesAsync();


            Preferences.Default.Set("Local", "LocalSynced");
            CurrentSession.LocalStatus = true;
            return true;
        }
    }

}

