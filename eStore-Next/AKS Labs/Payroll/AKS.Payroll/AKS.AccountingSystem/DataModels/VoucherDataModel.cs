using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Templets.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AKS.AccountingSystem.DataModels
{
    public class DynVM
    {
        public string StoreId { get; set; }

        public string DisplayMember { get; set; }
        public string DisplayData { get; set; }

        public string ValueMember { get; set; }
        public string ValueData { get; set; }
        public int ValueIntData { get; set; }

        public string BoolMember { get; set; }
        public bool BoolValue { get; set; }
    }

    public class CommonDataModel
    {
        public static List<DynVM> GetStoreList(AzurePayrollDbContext db)
        {
            return db.Stores.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "StoreName"
                ,
                DisplayData = c.StoreName,
                BoolValue = c.IsActive,
                BoolMember = "IsActive"
            }).ToList();
        }

        public static List<DynVM> GetEmployeeList(AzurePayrollDbContext db)
        {
            return db.Employees.Select(c => new DynVM
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

        public static List<DynVM> GetBankAccount(AzurePayrollDbContext db)
        {
            return db.BankAccounts.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "AccountNumber",
                BoolMember = "IsActive",
                DisplayData = c.AccountNumber,
                BoolValue = c.IsActive
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

    public class VoucherCashDataModel : DataModel<Voucher, CashVoucher>
    {
        public int GetVoucherCount(string storeCode, DateTime onDate, VoucherType type)
        {
            return azureDb.Vouchers.Where(c => c.VoucherType == type && c.StoreId == storeCode && c.OnDate.Month == onDate.Month
                   && c.OnDate.Year == onDate.Year).Count();
        }

        public int GetCashVoucherCount(string storeCode, DateTime onDate, VoucherType type)
        {
            return azureDb.CashVouchers.Where(c => c.VoucherType == type && c.StoreId == storeCode && c.OnDate.Month == onDate.Month
                   && c.OnDate.Year == onDate.Year).Count();
        }

        public List<Voucher> GetVouchers(VoucherType type, int year, string storeCode)
        {
            return azureDb.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.StoreId == storeCode && c.VoucherType == type && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
        }

        public List<CashVoucher> GetCashVouchers(VoucherType type, int year, string storeCode)
        {
            return azureDb.CashVouchers
                        .Include(c => c.Employee).Include(c => c.Partys).Include(c => c.TranscationMode)
                        .Where(c => c.VoucherType == type && c.OnDate.Year == year && c.StoreId == storeCode).OrderBy(c => c.OnDate).ToList();
        }

        public TranscationMode? GetTranscationMode(string id)
        {
            return azureDb.TranscationModes.Find(id);
        }

        public Party? GetParty(string id)
        {
            return azureDb.Parties.Find(id);
        }

        public Shared.Payroll.Models.Employee? GetEmployee(string id)
        {
            return azureDb.Employees.Find(id);
        }

        public override Voucher Get(string id)
        {
            return azureDb.Vouchers.Find(id);
        }

        public override Voucher Get(int id)
        {
            return azureDb.Vouchers.Find(id);
        }

        public List<Voucher> GetList(VoucherType type)
        {
            return azureDb.Vouchers.Where(c => c.VoucherType == type).ToList();
        }

        public override List<Voucher> GetList()
        {
            return azureDb.Vouchers.ToList();
        }

        public List<int> GetYearList(string storeCode)
        {
            var years = azureDb.Vouchers.Where(c => c.StoreId == storeCode).Select(c => c.OnDate.Year).Distinct().ToList();
            years.AddRange(azureDb.CashVouchers.Where(c => c.StoreId == storeCode).Select(c => c.OnDate.Year).Distinct().ToList());
            years = years.Distinct().OrderBy(c => c).ToList();
            return years;
        }

        public override CashVoucher GetY(string id)
        {
            return azureDb.CashVouchers.Find(id);
        }

        public override CashVoucher GetY(int id)
        {
            return azureDb.CashVouchers.Find(id);
        }

        public List<CashVoucher> GetYList(VoucherType type)
        {
            return azureDb.CashVouchers.Where(c => c.VoucherType == type).ToList();
        }

        public override List<CashVoucher> GetYList()
        {
            return azureDb.CashVouchers.ToList();
        }
    }
}