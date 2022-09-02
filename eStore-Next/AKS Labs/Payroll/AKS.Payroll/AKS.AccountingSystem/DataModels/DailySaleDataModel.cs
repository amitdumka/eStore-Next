using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Templets.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AKS.AccountingSystem.DataModels
{
    public class DailySaleDataModel : DataModel<DailySale, CustomerDue, DueRecovery>
    {
        public bool AddBasicARDEDC()
        {
            EDCTerminal eDCTerminal = new EDCTerminal
            {
                Active = true,
                BankId = "Bank of Maharashtra",
                EDCTerminalId = "EDC/2022/003",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                MID = "NA",
                Name = "PineLab BOM",
                OnDate = DateTime.Now,
                ProviderName = "Pine Labs",
                StoreId = "ARD",
                TID = "NA",
                UserId = "AUTOADMIN"
            };
            EDCTerminal eDCTerminal2 = new EDCTerminal
            {
                Active = true,
                BankId = "AprajitaRetails_ICICI-63005500372",
                EDCTerminalId = "EDC/2022/002",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                MID = "NA",
                Name = "EasyTab ICICI",
                OnDate = DateTime.Now,
                ProviderName = "ICICI Bank",
                StoreId = "ARD",
                TID = "NA",
                UserId = "AUTOADMIN"
            };
            EDCTerminal eDCTerminal3 = new EDCTerminal
            {
                Active = true,
                BankId = "SBI_CC-37604947464",
                EDCTerminalId = "EDC/2022/001",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                MID = "NA",
                Name = "SBI",
                OnDate = DateTime.Now,
                ProviderName = "SBI",
                StoreId = "ARD",
                TID = "NA",
                UserId = "AUTOADMIN"
            };
            azureDb.EDCTerminals.Add(eDCTerminal3);
            azureDb.EDCTerminals.Add(eDCTerminal);
            azureDb.EDCTerminals.Add(eDCTerminal2);
            return (azureDb.SaveChanges() > 0);
        }

        public override DailySale Get(string id)
        {
            throw new NotImplementedException();
        }

        public override DailySale Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<DailySale> GetCurrentMonthSale()
        {
            return azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman)
                .Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year
            && c.OnDate.Month == DateTime.Today.Month).OrderByDescending(c => c.OnDate).ToList();
        }

        public List<DailySale> GetCurrentYearSale()
        {
            return azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman)
                .Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year
           ).OrderByDescending(c => c.OnDate).ToList();
        }

        public List<CustomerDue> GetDueList()
        {
            return azureDb.CustomerDues
                    .Where(c => c.StoreId == StoreCode && !c.Paid)
                    .OrderByDescending(c => c.OnDate)
                    .ToList();
        }

        public List<DailySale> GetLastMonthSale()
        {
            return azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman)
                .Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year
            && c.OnDate.Month == DateTime.Today.AddMonths(-1).Month).OrderByDescending(c => c.OnDate).ToList();
        }

        public override List<DailySale> GetList()
        {
            throw new NotImplementedException();
        }

        public List<DueRecovery> GetRecoveryList(int year)
        {
            return azureDb.DueRecovery.Where(c => c.StoreId == StoreCode &&
             c.OnDate.Year == year).OrderByDescending(c => c.OnDate).ToList();
        }

        public override DueRecovery GetSeconday(string id)
        {
            throw new NotImplementedException();
        }

        public override DueRecovery GetSeconday(int id)
        {
            throw new NotImplementedException();
        }

        public override List<DueRecovery> GetSecondayList()
        {
            throw new NotImplementedException();
        }

        public override CustomerDue GetY(string id)
        {
            throw new NotImplementedException();
        }

        public override CustomerDue GetY(int id)
        {
            throw new NotImplementedException();
        }

        public override List<CustomerDue> GetYList()
        {
            throw new NotImplementedException();
        }

        public List<int> YearList()
        {
            return azureDb.DailySales
                .Where(c => c.StoreId == StoreCode).Select(c => c.OnDate.Year)
                .Distinct().OrderBy(c => c).ToList();
        }
    }
}