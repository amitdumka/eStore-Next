using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;

public enum LocalSync
{ Initial, Accounting, Inventory, InitialAccounting, InitialInventory, All }

namespace eStore.DatabaseSyncService.Services
{
    public static class Sync
    {
        public static async Task<bool> Down()
        {
            var sync = new SyncService();
            bool success = false;
            success= await sync.SyncDownStoresAsync();
            success = await sync.SyncDownEmployeesAsync();
            success = await sync.SyncDownUsersAsync();
            success = await sync.SyncDownSalesmanAsync();
            success = await sync.SyncDownTranscationsAsync();
            success = await sync.SyncDownPOSAsync();
            success = await sync.SyncVoucherAsync();
            success= await sync.SyncCashAsync();
            success = await sync.SyncDuesAsync();
            success = await sync.SyncDownBanksAsync();
            success = await sync.SyncDownAttendace(CurrentSession.UserType);
            success= await sync.SyncDownBankAccountsAsync();
             await sync.SyncDownPartiesAsync();
            success = await sync.SyncSalary(CurrentSession.UserType);
            success = await sync.SyncSalaryPayment(CurrentSession.UserType);
            Preferences.Default.Set("Local", "LocalSynced");
            CurrentSession.LocalStatus = true;
            return success;
        }
    }

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

        public static void VerifyInitialSet()
        {
            //TODO: Pending
            var keyValue = Preferences.Get(nameof(User), "NO");
            if (keyValue != "NO")
            {
                var values = keyValue.Split('#');
            }
        }

        public static bool VerifyPayrollSet()
        {
            //TODO: Pending
            var keyValue = Preferences.Get(nameof(Attendance), "NO");
            if (keyValue != "NO")
            {
                var values = keyValue.Split("#");
                return true;
            }
            else return false;
        }

        public static bool SyncInventory()
        {
            //var sync = new SyncService();
            //sync.SyncDownBandandSuppliers();
            //sync.SyncDownProductItems();
            //sync.SyncDownStocks();
            //sync.SyncDownProductSubCategories();
            //sync.SyncDownProductTypes();
            Preferences.Default.Set("LocalInventory", "LocalSynced");

            return true;
        }

        public static async Task<bool> SyncAttendance()
        {
            var sync = new SyncService();
            return await sync.SyncDownAttendace(CurrentSession.UserType);
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
            if (CurrentSession.IsLoggedIn)
            {
                userType = CurrentSession.UserType;
            }
            else
            {
                Thread.Sleep(30000);
                userType = CurrentSession.UserType;
            }

            sync.SyncDownBankAccountsAsync();
            //sync.SyncDownEdcTermianl(userType);
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
            else
            {
                SyncAccounting();
                return false;
            }
        }
    }
}