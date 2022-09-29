using AKS.MAUI.Databases;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum LocalSync
{ Initial, Accounting, Inventory, InitialAccounting, InitialInventory, All }

namespace eStore.DatabaseSyncService.Services
{
    public static class Sync
    {
        public static async Task<bool> Down() {

            var sync = new SyncService();

            await sync.SyncDownStoresAsync();
            sync.SyncDownEmployeesAsync();
            await sync.SyncDownUsersAsync();
            sync.SyncDownSalesmanAsync();
            sync.SyncDownTranscationsAsync();
            await sync.SyncDownBanksAsync();
            

            
            
            
            
            
            Preferences.Default.Set("Local", "LocalSynced");
            CurrentSession.LocalStatus = true;
            return true;

        }

    }
    public class BackgroundService
    {

    }
    public class SyncDownService : BackgroundService { }
    public class DatabaseStatus
    {
        public static async Task<bool> SyncInitial()
        {
            var sync = new SyncService();

            await sync.SyncDownStoresAsync();
            sync.SyncDownEmployeesAsync();
            await sync.SyncDownUsersAsync();
            sync.SyncDownSalesmanAsync();

            Preferences.Default.Set("Local", "LocalSynced");
            CurrentSession.LocalStatus = true;
            return true;
        }
        public static bool VerifyLocalStatus()
        {
            var keyValue = Preferences.Get("Local", "NO");
            if (keyValue == "LocalSynced") return true;
            else return false;
        }
    }

    public class SyncService
    {
        private AppDBContext azure;
        private AppDBContext db;

        // Initial 

        /// <summary>
        /// First, Part of Initial DB
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncDownStoresAsync()
        {
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
                        db.Stores.AddAsync(Store);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(Store), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(Store));
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Second 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncDownEmployeesAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Employees.Count();
            var remote = azure.Employees.Count();
            if (local != remote)
            {
                var Employees = azure.EmployeeDetails.Include(c => c.Employee).ToList();

                int recordAdded = 0;
                foreach (var emp in Employees)
                {
                    if (!db.EmployeeDetails.Any(c => c.EmployeeId == emp.EmployeeId))
                    {
                        db.EmployeeDetails.AddAsync(emp);
                        recordAdded += 2;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(Employee), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(Employee));
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Third
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncDownUsersAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
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
                        db.Users.AddAsync(user);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();

                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(User));
                    return false;
                }
            }
            return true;
        }

        // Second Initial
        public async Task SyncDownBanksAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                int lCount = 0;
                int rCount = 0;
                var local = db.Banks.Count();
                var remote = azure.Banks.Count();

                if (local != remote)
                {
                    var remoteList = azure.Banks.ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.Banks.Any(c => c.BankId == att.BankId))
                        {
                            db.Banks.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;
                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(Bank), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:ALL");
                    else
                        Preferences.Remove(nameof(Bank));
                }

            }
            catch (Exception e)
            {

                //Toast//ake("Failed to sync bank List with error: " + e.Message, );
            }
        }

        public async Task<bool> SyncDownTranscationsAsync()
        {
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
                        db.TranscationModes.AddAsync(mode);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(TranscationModes), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(TranscationModes));
                    return false;
                }
            }
            return true;
        }

        // Payroll

        //Banking

        // Accouting 


        // Inventory
        public async Task<bool> SyncDownSalesmanAsync()
        {
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
                        db.Salesmen.AddAsync(sm);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded) return true; else return false;
            }
            return true;
        }



    }
}
