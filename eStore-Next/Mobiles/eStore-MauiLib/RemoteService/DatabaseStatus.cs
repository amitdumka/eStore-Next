﻿using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;

namespace eStore_MauiLib.RemoteService
{
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
            var keyValue = Preferences.Get(nameof(User), "NO");
            if (keyValue != "NO")
            {
                var values = keyValue.Split('#');
            }
        }

        public static bool VerifyPayrollSet()
        {
            var keyValue = Preferences.Get(nameof(Attendance), "NO");
            if (keyValue != "NO")
            {
                var values = keyValue.Split("#");
                return true;
            }
            else return false;
        }

        public static bool SyncInitial()
        {
            var sync = new SyncService();

            sync.SyncDownStoresAsync();
            sync.SyncDownUsersAsync();
            sync.SyncDownEmployeesAsync();
            sync.SyncDownSalesmanAsync();
            
            Preferences.Default.Set("Local", "LocalSynced");
            CurrentSession.LocalStatus = true;
            return true;
        }

        public static bool SyncInventory()
        {
            var sync = new SyncService();
            sync.SyncDownBandandSuppliers();
            sync.SyncDownProductItems();
            sync.SyncDownStocks();
            sync.SyncDownProductSubCategories();
            sync.SyncDownProductTypes();
            Preferences.Default.Set("LocalInventory", "LocalSynced");

            return true;
        }

        public static async Task<bool> SyncAttendance()
        {
            var sync = new SyncService();
            return await sync.SyncAttendace(CurrentSession.UserType);
        }
        public static async Task<bool> SyncSalary()
        {
            var sync = new SyncService();
            return await sync.SyncSalary(CurrentSession.UserType);
        }
        public static async Task<bool> SyncSalaryPayment()
        {
            var sync = new SyncService();
            return await sync.SyncSalaryPayment(CurrentSession.UserType);
        }

        public static bool SyncAccounting()
        {
            var sync = new SyncService();
            sync.SyncDownTranscationsAsync();
            sync.SyncDownBanksAsync();
            sync.SyncDownPartiesAsync();
            Thread.Sleep(30000);
            UserType userType = UserType.Guest;
            if (CurrentSession.IsLoggedIn) { 
                userType=CurrentSession.UserType;
            }else
            {
                Thread.Sleep(30000);
                userType = CurrentSession.UserType;
            }
            
            
            
            sync.SyncDownBankAccountsAsync(userType);
            sync.SyncDownEdcTerminalsAsync(userType);
            //sync.SyncDownVendorsAccount();
            //sync.SyncDownAccountList();
            Preferences.Default.Set("LocalAccounting", "LocalSynced");
            return true;
        }
        public static bool VerifyOrSyncInitalAccounting()
        {
            var keyValue = Preferences.Get("LocalAccounting", "NO");
            if (keyValue == "LocalSynced")
            {
                return true;
            }
            else {
                SyncAccounting();
                return false; 
            }
        }
    }
}