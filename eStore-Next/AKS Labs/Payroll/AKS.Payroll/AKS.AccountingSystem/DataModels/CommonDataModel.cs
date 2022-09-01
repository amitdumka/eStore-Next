using AKS.Payroll.Database;

namespace AKS.AccountingSystem.DataModels
{
    internal class CommonDataModel
    {
        public static List<DynVM> GetBankAccount(AzurePayrollDbContext db)
        {
            return db.BankAccounts.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                ValueMember = "AccountNumber",
                ValueData = c.AccountNumber,
                BoolMember = "IsActive",
                BoolValue = c.IsActive,
                DisplayData = c.AccountHolderName,
                DisplayMember = "AccountHolderName"
            }).ToList();
        }

        public static List<DynVM> GetEmployeeList(AzurePayrollDbContext db)
        {
            return db.Employees.Where(c => c.IsWorking && !c.IsTailors).Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "StaffName",
                BoolMember = "IsWorking",
                ValueMember = "EmployeeId",
                ValueData = c.EmployeeId,
                DisplayData = c.StaffName,
                BoolValue = c.IsWorking
            }).ToList();
        }

        public static List<DynVM> GetParty(AzurePayrollDbContext db)
        {
            return db.Parties.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "PartyName",
                ValueMember = "PartyId",
                DisplayData = c.PartyName,
                ValueData = c.PartyId,
            }).ToList();
        }

        public static List<DynVM> GetStoreList(AzurePayrollDbContext db)
        {
            return db.Stores.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "StoreName",
                DisplayData = c.StoreName,
                BoolValue = c.IsActive,
                BoolMember = "IsActive"
            }).ToList();
        }
        public static List<DynVM> GetTranscation(AzurePayrollDbContext db)
        {
            return db.TranscationModes.Select(c => new DynVM
            {
                DisplayMember = "TranscationName",
                ValueMember = "TranscationId",
                DisplayData = c.TranscationName,
                ValueData = c.TranscationId,
            }).ToList();
        }
    }
}