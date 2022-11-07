using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Payroll.Models;
using eStore.SetUp.Import;

namespace eStore.SetUp.Export
{
    public enum ExportType
    { JSON, EXCEL, PDF }

    public class BackupDatabase
    {
        private string BackupPath;
        private string BasePath;
        private AzurePayrollDbContext db;
        private string StoreCode;
        private string ZipFileName;

        public async void StartBackup(string store, string path)
        {
            StoreCode = store; BackupPath = path;
            ZipFileName = $@"{store}_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}.zip";
            BasePath = Path.Combine(BackupPath, store);

            bool flag = await BackupOtherAsync();
            flag = await BackupPayrollAsync();
            flag = await BackupSalePurchaseAsync();
            flag = await BackupVouchersAndBankingAsync();
            if (flag)
            {
                ImportBasic.BackupJSon(Path.Combine(BackupPath, ZipFileName), BasePath);
            }
        }

        private async Task<bool> BackupOtherAsync()
        {
            if (db == null)
                db = new AzurePayrollDbContext();

            var path = Path.Combine(BasePath, "Stores");
            bool flag = await ImportData.ObjectsToJSONFile<Store>(db.Stores.ToList(), Path.Combine(path, "Stores.json"));
            flag = await ImportData.ObjectsToJSONFile<Salesman>(db.Salesmen.ToList(), Path.Combine(path, "Salesman.json"));
            flag = await ImportData.ObjectsToJSONFile<CashDetail>(db.CashDetails.ToList(), Path.Combine(path, "CashDetails.json"));
            flag = await ImportData.ObjectsToJSONFile<TranscationMode>(db.TranscationModes.ToList(), Path.Combine(path, "TranscationModes.json"));

            flag = await ImportData.ObjectsToJSONFile<User>(db.Users.ToList(), Path.Combine(path, "Users.json"));
            flag = await ImportData.ObjectsToJSONFile<LocalUser>(db.LocalUsers.ToList(), Path.Combine(path, "LocalUsers.json"));

            flag = await ImportData.ObjectsToJSONFile<Brand>(db.Brands.ToList(), Path.Combine(path, "Brands.json"));
            flag = await ImportData.ObjectsToJSONFile<ChequeBook>(db.ChequeBooks.ToList(), Path.Combine(path, "ChequeBooks.json"));

            flag = await ImportData.ObjectsToJSONFile<ChequeIssued>(db.ChequeIssued.ToList(), Path.Combine(path, "ChequeIssued.json"));
            flag = await ImportData.ObjectsToJSONFile<ChequeLog>(db.ChequeLogs.ToList(), Path.Combine(path, "ChequesLog.json"));

            flag = await ImportData.ObjectsToJSONFile<CustomerDue>(db.CustomerDues.ToList(), Path.Combine(path, "CustomerDues.json"));

            flag = await ImportData.ObjectsToJSONFile<DailySale>(db.DailySales.ToList(), Path.Combine(path, "DailySales.json"));
            flag = await ImportData.ObjectsToJSONFile<Customer>(db.Customers.ToList(), Path.Combine(path, "Customers.json"));
            flag = await ImportData.ObjectsToJSONFile<CustomerSale>(db.CustomerSales.ToList(), Path.Combine(path, "CustomerSales.json"));

            flag = await ImportData.ObjectsToJSONFile<DueRecovery>(db.DueRecovery.ToList(), Path.Combine(path, "DueRecovery.json"));
            flag = await ImportData.ObjectsToJSONFile<EDCTerminal>(db.EDCTerminals.ToList(), Path.Combine(path, "EDCTerminals.json"));

            flag = await ImportData.ObjectsToJSONFile<LedgerGroup>(db.LedgerGroups.ToList(), Path.Combine(path, "LedgerGroups.json"));
            flag = await ImportData.ObjectsToJSONFile<LedgerMaster>(db.LedgerMasters.ToList(), Path.Combine(path, "LedgerMasters.json"));

            flag = await ImportData.ObjectsToJSONFile<Party>(db.Parties.ToList(), Path.Combine(path, "Parties.json"));
            flag = await ImportData.ObjectsToJSONFile<Note>(db.Notes.ToList(), Path.Combine(path, "Notes.json"));
            flag = await ImportData.ObjectsToJSONFile<PettyCashSheet>(db.PettyCashSheets.ToList(), Path.Combine(path, "PettyCashSheet.json"));

            flag = await ImportData.ObjectsToJSONFile<Stock>(db.Stocks.ToList(), Path.Combine(path, "Stocks.json"));
            flag = await ImportData.ObjectsToJSONFile<Supplier>(db.Suppliers.ToList(), Path.Combine(path, "Suppliers.json"));
            flag = await ImportData.ObjectsToJSONFile<Vendor>(db.Vendors.ToList(), Path.Combine(path, "Vendor.json"));

            return flag;
        }

        private async Task<bool> BackupPayrollAsync()
        {
            if (db == null)
                db = new AzurePayrollDbContext();

            var path = Path.Combine(BasePath, "Payroll");

            bool flag = await ImportData.ObjectsToJSONFile<Employee>(db.Employees.ToList(), Path.Combine(path, "Employees.json"));
            flag = await ImportData.ObjectsToJSONFile<EmployeeDetails>(db.EmployeeDetails.ToList(), Path.Combine(path, "EmployeesDetails.json"));

            flag = await ImportData.ObjectsToJSONFile<Attendance>(db.Attendances.ToList(), Path.Combine(path, "Attendances.json"));
            flag = await ImportData.ObjectsToJSONFile<MonthlyAttendance>(db.MonthlyAttendances.ToList(), Path.Combine(path, "MonhtlyAttendances.json"));

            flag = await ImportData.ObjectsToJSONFile<Salary>(db.Salaries.ToList(), Path.Combine(path, "Salaries.json"));
            flag = await ImportData.ObjectsToJSONFile<SalaryPayment>(db.SalaryPayment.ToList(), Path.Combine(path, "SalaryPayments.json"));
            flag = await ImportData.ObjectsToJSONFile<SalaryLedger>(db.SalaryLedgers.ToList(), Path.Combine(path, "SalaryLedger.json"));

            flag = await ImportData.ObjectsToJSONFile<StaffAdvanceReceipt>(db.StaffAdvanceReceipt.ToList(), Path.Combine(path, "StaffAdvaceReceipts.json"));

            flag = await ImportData.ObjectsToJSONFile<PaySlip>(db.PaySlips.ToList(), Path.Combine(path, "PaySlips.json"));
            flag = await ImportData.ObjectsToJSONFile<TimeSheet>(db.TimeSheets.ToList(), Path.Combine(path, "TimeSheets.json"));

            return flag;
        }

        private async Task<bool> BackupSalePurchaseAsync()
        {
            if (db == null)
                db = new AzurePayrollDbContext();

            var path = Path.Combine(BasePath, "SalePurchase");
            bool flag = await ImportData.ObjectsToJSONFile<ProductItem>(db.ProductItems.ToList(), Path.Combine(path, "ProductItems.json"));
            flag = await ImportData.ObjectsToJSONFile<PurchaseProduct>(db.PurchaseProducts.ToList(), Path.Combine(path, "PurchaseProducts.json"));
            flag = await ImportData.ObjectsToJSONFile<PurchaseItem>(db.PurchaseItems.ToList(), Path.Combine(path, "PurchaseItems.json"));
            flag = await ImportData.ObjectsToJSONFile<ProductSale>(db.ProductSales.ToList(), Path.Combine(path, "ProductSales.json"));
            flag = await ImportData.ObjectsToJSONFile<SaleItem>(db.SaleItems.ToList(), Path.Combine(path, "SaleItems.json"));
            flag = await ImportData.ObjectsToJSONFile<SalePaymentDetail>(db.SalePaymentDetails.ToList(), Path.Combine(path, "SalePaymentDetails.json"));
            flag = await ImportData.ObjectsToJSONFile<CardPaymentDetail>(db.CardPaymentDetails.ToList(), Path.Combine(path, "CardPaymentDetails.json"));
            flag = await ImportData.ObjectsToJSONFile<ProductType>(db.ProductTypes.ToList(), Path.Combine(path, "ProductTypes.json"));
            flag = await ImportData.ObjectsToJSONFile<ProductSubCategory>(db.ProductSubCategories.ToList(), Path.Combine(path, "ProductSubCategory.json"));
            return flag;
        }

        private async Task<bool> BackupVouchersAndBankingAsync()
        {
            if (db == null)
                db = new AzurePayrollDbContext();

            var path = Path.Combine(BasePath, "Vouchers");
            bool flag = await ImportData.ObjectsToJSONFile<Voucher>(db.Vouchers.ToList(), Path.Combine(path, "Vouchers.json"));
            flag = await ImportData.ObjectsToJSONFile<CashVoucher>(db.CashVouchers.ToList(), Path.Combine(path, "CashVouchers.json"));
            path = Path.Combine(BasePath, "Banking");
            flag = await ImportData.ObjectsToJSONFile<Bank>(db.Banks.ToList(), Path.Combine(path, "Banks.json"));
            flag = await ImportData.ObjectsToJSONFile<BankAccount>(db.BankAccounts.ToList(), Path.Combine(path, "BankAccounts.json"));
            flag = await ImportData.ObjectsToJSONFile<BankTranscation>(db.BankTranscations.ToList(), Path.Combine(path, "BankTranscations.json"));
            flag = await ImportData.ObjectsToJSONFile<VendorBankAccount>(db.VendorBankAccounts.ToList(), Path.Combine(path, "VendorsBankAccounts.json"));
            flag = await ImportData.ObjectsToJSONFile<BankAccountList>(db.AccountLists.ToList(), Path.Combine(path, "BankAccountList.json"));

            return flag;
        }
    }

    public class ExportData
    {

    }
}