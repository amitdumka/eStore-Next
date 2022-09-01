using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Templets.DataModels;
using System.Drawing;

namespace AKS.AccountingSystem.DataModels
{
    public class PettyCashSheetDataModel : DataModel<PettyCashSheet, CashDetail>
    {
        
        public List<int> GetYearList()
        {
            //TODO: StoreCode
          return  azureDb.PettyCashSheets.Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
        }
        public override PettyCashSheet Get(string id)
        {
            return azureDb.PettyCashSheets.Find(id);
        }

        public override PettyCashSheet Get(int id)
        {
            return azureDb.PettyCashSheets.Find(id);
        }
        public  PettyCashSheet Get(DateTime onDate)
        {
            return azureDb.PettyCashSheets.Where(c=>c.OnDate.Date==onDate.Date).FirstOrDefault();
        }

        public override List<PettyCashSheet> GetList()
        {
            return azureDb.PettyCashSheets.ToList();
        }

        public List<PettyCashSheet> GetList(int year)
        {
            //TODO: need to based on store
            return azureDb.PettyCashSheets.Where(c => c.OnDate.Year == year).ToList();
        }

        public List<PettyCashSheet> GetList(int year, int month)
        {
            //TODO: need to based on store
            return azureDb.PettyCashSheets.Where(c => c.OnDate.Year == year && c.OnDate.Month == month).ToList();
        }

        public override CashDetail GetY(string id)
        {
            return azureDb.CashDetails.Find(id);
        }

        public override CashDetail GetY(int id)
        {
            return azureDb.CashDetails.Find(id);
        }
        public CashDetail GetY(DateTime onDate)
        {
            return azureDb.CashDetails.Where(c => c.OnDate.Date == onDate.Date).FirstOrDefault();
        }
        public override List<CashDetail> GetYList()
        {
            return azureDb.CashDetails.Where(c => c.StoreId == StoreCode).ToList();
        }

        public List<CashDetail> GetYList(int year, int month)
        {
            return azureDb.CashDetails.Where(c => c.StoreId == StoreCode && c.OnDate.Year == year && c.OnDate.Month == month).ToList();
        }

        public List<CashDetail> GetYList(int year)
        {
            return azureDb.CashDetails.Where(c => c.StoreId == StoreCode && c.OnDate.Year == year).ToList();
        }

        public List<Shared.Commons.Models.Sales.DailySale> DailySale(DateTime startDate, DateTime endDate)
        {
            return azureDb.DailySales.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();

        }

        public List<Voucher> Vouchers(DateTime startDate, DateTime endDate)
        {
            return azureDb.Vouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
           // var cash = azureDb.CashVouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();

        }
        public List<CashVoucher> CashVouchers(DateTime startDate, DateTime endDate)
        {
           // return azureDb.Vouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            return azureDb.CashVouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();

        }
    }
}