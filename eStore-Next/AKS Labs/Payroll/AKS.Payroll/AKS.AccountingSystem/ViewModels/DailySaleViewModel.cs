using AKS.AccountingSystem.DataModels;
using AKS.AccountingSystem.DTO;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Templets.ViewModels;

namespace AKS.AccountingSystem.ViewModels
{
    public class DailySaleViewModel : ViewModel<DailySale, CustomerDue, DailySaleVM, DailySaleDataModel>
    {
        //TODO: Use this class for CustomerDue entry  and for listing and editing for dues use dueviewmodel

        #region Declarations

        public ObservableListSource<DailySaleVM> dailySaleVMs;//PrimaryVM
        public List<int> YearList;
        public List<int> YearDataList;

        public DailySale? SavedSale { get; private set; }
        public CustomerDue? SavedDue { get; private set; }

        public string tsslMonthly = "";
        public string sslToday = "";
        public bool DueDataLoaded = false;

        #endregion Declarations

        #region OverrideMethods

        public override bool Delete(DailySale entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<DailySale> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CustomerDue> entities)
        {
            throw new NotImplementedException();
        }

        public override bool InitViewModel()
        {
            DMMapper.InitializeAutomapper();
            dailySaleVMs = new ObservableListSource<DailySaleVM>();
            YearDataList = new List<int>();
            YearList = new List<int>();
            return true;
        }

        public override bool Save(DailySale entity)
        {
            SavedSale = DataModel.Save(entity);
            if (SavedSale != null) return true;
            return false;
        }

        public override bool Save(CustomerDue entity)
        {
            SavedDue = DataModel.Save(entity);
            if (SavedDue != null) return true;
            return false;
        }

        #endregion OverrideMethods

        #region UiFunctions

        public void LoadIntialData()
        {
            UpdateSaleList(DataModel.GetCurrentMonthSale());
            YearList.AddRange(DataModel.YearList());

            if (YearList.Contains(DateTime.Today.Year) == false)
                YearList.Add(DateTime.Today.Year);
        }

        private void UpdateSaleList(List<DailySale> sales)
        {
            foreach (var sale in sales)
                dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(sale));
        }

        private void UpdateSaleList(List<DailySale> sales, int year)
        {
            if (!YearDataList.Any(c => c == year))
            {
                foreach (var sale in sales)
                    DMMapper.Mapper.Map<DailySaleVM>(sale);
            }
        }

        public List<int> GetYearList()
        {
            return DataModel.YearList();
        }

        public List<DailySaleVM> GetCurrentMonthSale()
        {
            if (dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.Month
                       && c.OnDate.Year == DateTime.Today.Year).Any() == false)
            {
                UpdateSaleList(DataModel.GetCurrentMonthSale());
            }
            return dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.Month
                       && c.OnDate.Year == DateTime.Today.Year).ToList();
        }

        public List<DailySaleVM> GetCurrentYearSale()
        {
            // need to validate from dataserver also.
            if (dailySaleVMs.Where(c => c.OnDate.Year == DateTime.Today.Year).Any() == false)
            {
                UpdateSaleList(DataModel.GetCurrentYearSale());
            }
            return dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.Month
                       && c.OnDate.Year == DateTime.Today.Year).ToList();
        }

        public List<DailySaleVM> GetLastMonthSale()
        {
            if (dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                   && c.OnDate.Year == DateTime.Today.Year).Any() == false)
            {
                UpdateSaleList(DataModel.GetLastMonthSale());
            }
            return dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                   && c.OnDate.Year == DateTime.Today.Year).ToList();
        }

        public bool SaveSale(bool read)
        {
            if (read)
            {
                if (Save(PrimaryEntity))
                {
                    dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(SavedSale));
                    if (SavedDue != null)
                    {
                        if (SavedSale.InvoiceNumber == SavedDue.InvoiceNumber)
                        {
                            SecondayEntites.Add(SavedDue);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public async void SetDisplaySale()
        {
            await Task.Delay(15000);
            Sales.FetchSaleDetails(DataModel.GetDatabaseInstance(), CurrentSession.StoreCode);
            tsslMonthly = $"Monthly [Total Sales: Rs. {Sales.MonthlySale} Cash: Rs. {Sales.MonthlyCashSale}  Non Cash: Rs. {Sales.MonthlyNonCashSale}  ] ";
            sslToday = $"Today [Total Sales: Rs. {Sales.TodaySale} Cash: Rs. {Sales.TodayCashSale}  Non Cash: Rs. {Sales.TodayNonCashSale}  ] ";
        }

        #endregion UiFunctions
    }

    //TODO: move to widget sections with proper libs
    public class Sales
    {
        public static decimal MonthlyCashSale { get; set; }
        public static decimal MonthlyNonCashSale { get; set; }
        public static decimal MonthlySale { get; set; }
        public static decimal TodayCashSale { get; set; }
        public static decimal TodayNonCashSale { get; set; }
        public static decimal TodaySale { get; set; }

        public static void FetchLocalSaleDetails(LocalPayrollDbContext db, string sc)
        {
            var today = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Date == DateTime.Today.Date)
                //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
                .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
                .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
                .ToList();

            var Monthly = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
               //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
               .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
               .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
               .ToList();

            TodaySale = today.Sum(c => c.AMT);
            MonthlySale = Monthly.Sum(c => c.AMT);

            TodayCashSale = today.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            TodayCashSale += today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            MonthlyCashSale = Monthly.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            MonthlyCashSale += Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            TodayNonCashSale = today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
            MonthlyNonCashSale = Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
        }

        public static void FetchSaleDetails(AzurePayrollDbContext db, string sc)
        {
            var today = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Date == DateTime.Today.Date)
                //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
                .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
                .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
                .ToList();

            var Monthly = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
               //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
               .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
               .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
               .ToList();

            TodaySale = today.Sum(c => c.AMT);
            MonthlySale = Monthly.Sum(c => c.AMT);

            TodayCashSale = today.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            TodayCashSale += today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            MonthlyCashSale = Monthly.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            MonthlyCashSale += Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            TodayNonCashSale = today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
            MonthlyNonCashSale = Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
        }
    }

    public class DueViewModel : ViewModel<CustomerDue, DueRecovery, DailySaleDataModel>
    {
        #region Decalarations

        //Primary Enity Due, Seconday Enity Recovery

        #endregion Decalarations

        #region OverrideMethods

        public override bool Delete(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(DueRecovery entity)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CustomerDue> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<DueRecovery> entities)
        {
            throw new NotImplementedException();
        }

        public override bool InitViewModel()
        {
            throw new NotImplementedException();
        }

        public override bool Save(CustomerDue entity)
        {
            throw new NotImplementedException();
        }

        public override bool Save(DueRecovery entity)
        {
            throw new NotImplementedException();
        }

        #endregion OverrideMethods
    }
}