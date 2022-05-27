using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobileX.Core
{
    /// <summary>
    /// Applicatoin Information Logging
    /// </summary>
    public static class ApplicationContext
    {
        public static string UserName { get; set; }
        // public static EmpType Role { get; set; }
        public static bool IsLoggedIn { get; set; }

        public static string EmpId { get; set; }
        public static string StoreId { get; set; }
        public static string StoreName { get; set; }

    }
    public class Constants
    {
        public const string BackendUrl = "https://zumo-abcd1234.azurewebsites.net";
        public const string Con = "";

        //Database
        public const string DatabaseFilename = "estoreSQLiteVer5.db3";

        //public const SQLite.SQLiteOpenFlags Flags =
        //    // open the database in read/write mode
        //    SQLite.SQLiteOpenFlags.ReadWrite |
        //    // create the database if it doesn't exist
        //    SQLite.SQLiteOpenFlags.Create |
        //    // enable multi-threaded database access
        //    SQLite.SQLiteOpenFlags.SharedCache;


        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        
        //Urls
        //TODO: Subject to use. rest purge it
        public static readonly string BaseAddress = "https://www.aprajitaretails.in/api";
        public static readonly string UserUrl = $"{BaseAddress}/users";
        public static readonly string StockListurl = $"{BaseAddress}/stockLists";
        public static readonly string StoreUrl = $"{BaseAddress}/stores";
        public static readonly string EmployeeUrl = $"{BaseAddress}/employees";
        public static readonly string DailySaleUrl = $"{BaseAddress}/dailysale";
        public static readonly string AttendanceUrl = $"{BaseAddress}/attendances";

        public static readonly string EnumUrl = $"{BaseAddress}/enumvalue";
        public static readonly string RoleUrl = $"{BaseAddress}/roles";
        public static readonly string MasterViewUrl = $"{BaseAddress}/masterReport";
        public static readonly string SyncServerUrl = $"{BaseAddress}/syncServer";
        public static readonly string TranscationModeUrl = $"{BaseAddress}/TranscationModes";
        public static readonly string BankUrl = $"{BaseAddress}/banks";
        public static readonly string BankAccountUrl = $"{BaseAddress}/bankAccounts";
        public static readonly string SalesmanUrl = $"{BaseAddress}/Salesmen";

        public static readonly string PartyUrl = $"{BaseAddress}/Parties";
        public static readonly string LedgerTypeUrl = $"{BaseAddress}/LedgerTypes";
        public static readonly string LedgerMasterUrl = $"{BaseAddress}/LedgerMasters";

        //Vouchers
        public static readonly string ExpenseUrl = $"{BaseAddress}/expenses";
        public static readonly string PaymentUrl = $"{BaseAddress}/payments";
        public static readonly string ReceiptsUrl = $"{BaseAddress}/receipts";
        public static readonly string CashPaymentUrl = $"{BaseAddress}/cashPayments";
        public static readonly string CashReceiptsUrl = $"{BaseAddress}/cashReceipts";
        public static readonly string InvoicesUrl = $"{BaseAddress}/invocing";
    }
}
