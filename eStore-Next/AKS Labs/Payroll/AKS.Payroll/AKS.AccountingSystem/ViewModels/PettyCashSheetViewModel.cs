using AKS.AccountingSystem.DataModels;
using AKS.AccountingSystem.DTO;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Templets.ViewModels;

namespace AKS.AccountingSystem.ViewModels
{
    public class PettyCashSheetViewModel : ViewModel<PettyCashSheet, CashDetail, PettyCashSheetDataModel>
    {
        #region Declaration

        public string pNar, rNar, dNar, rcNar;

        public CashDetail SavedCashDetail;

        public int TotalCurreny = 0, TotalCurrenyAmount = 0;

        public decimal tPay, tRec, tDue, tdRec;

        public List<int> YearList;

        private CashDetail cashDetail;

        private List<int> DataList;

        private bool EnableCashAdd;

        // private CommonDataModel CommonDataModel;
        private ObservableListSource<PettyCashSheet> ItemList;

        private PettyCashSheet? SavedPettyCash;

        #endregion Declaration

        #region OverrideMethods

        public override bool Delete(PettyCashSheet entity)
        {
            return DataModel.Delete(entity);
        }

        public override bool Delete(CashDetail entity)
        {
            return DataModel.Delete(entity);
        }

        public override bool DeleteRange(List<PettyCashSheet> entities)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<CashDetail> entities)
        {
            throw new NotImplementedException();
        }

        public override bool InitViewModel()
        {
            DataModel = new PettyCashSheetDataModel();
            DMMapper.InitializeAutomapper();
            this.PrimaryEntites = new List<PettyCashSheet>();
            this.PrimaryEntity = new PettyCashSheet
            {
                OnDate = DateTime.Now
            };
            //TODO: use if usefull other wise remove it
            ItemList = new ObservableListSource<PettyCashSheet>();
            YearList = DataModel.GetYearList();
            tRec = tPay = (decimal)0.0;
            DataList = new List<int>();
            DataList.Add(DateTime.Today.Year);

            UpdateItemList(DataModel.GetList(DateTime.Today.Year));
            SecondayEntites = DataModel.GetYList(DateTime.Today.Year);

            return true;
        }

        public override bool Save(PettyCashSheet entity)
        {
            SavedPettyCash = DataModel.Save(entity);
            if (SavedPettyCash == null) return false;
            return true;
        }

        public override bool Save(CashDetail entity)
        {
            SavedCashDetail = DataModel.Save(entity);
            if (SavedCashDetail == null) return false;
            return true;
        }

        #endregion OverrideMethods

        #region OpsFunctions

        public bool DeletePettyCash(PettyCashSheet pcs)
        {
            if (Delete(pcs))
            {
                ItemList.Remove(pcs);
                return true;
            }
            return false;
        }

        public void Filter(int year)
        {
            if (DataList.Contains(year) == false)
            {
                UpdateItemList(DataModel.GetList(year));
                SecondayEntites.AddRange(DataModel.GetYList(year));
                DataList.Add(year);
            }
        }

        public List<CashDetail> GetCashCurrentMonth()
        { return SecondayEntites.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).ToList(); }

        public List<CashDetail> GetCashCurrentYearly()
        { return SecondayEntites.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList(); }

        public List<CashDetail> GetCashLastMonth()
        { return SecondayEntites.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.AddMonths(-1).Month).ToList(); }

        public List<CashDetail> GetCashYearly(int year)
        { return SecondayEntites.Where(c => c.OnDate.Year == year).ToList(); }

        public List<PettyCashSheet> GetCurrentMonth()
        { return ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).ToList(); }

        public List<PettyCashSheet> GetCurrentYearly()
        { return ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList(); }

        public List<PettyCashSheet> GetGridData()
        {
            return ItemList.ToList();
        }

        public List<PettyCashSheet> GetLastMonth()
        { return ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.AddMonths(-1).Month).ToList(); }

        public List<DynVM> GetStoreList()
        {
            return CommonDataModel.GetStoreList(DataModel.GetDatabaseInstance());
        }

        public List<PettyCashSheet> GetYearly(int year)
        { return ItemList.Where(c => c.OnDate.Year == year).ToList(); }

        public bool SaveCashDetail(bool read)
        {
            return Save(SecondaryEntity);
        }

        public bool SavePettyCash(bool read)
        {
            if (Save(PrimaryEntity))
            {
                if (!isNew)
                {
                    ItemList.Remove(ItemList.Where(c => c.Id == PrimaryEntity.Id).FirstOrDefault());
                }
                ItemList.Add(PrimaryEntity);
                if (isNew) EnableCashAdd = true;
            }
            else
            {
                if (isNew)
                    Delete(SavedPettyCash);
            }
            return false;
        }

        public void SetAdd()
        {
            isNew = true;
            tPay = tRec = tDue = tdRec = 0;
        }

        public void SetEdit()
        { }

        public CashDetail GetSecondary(DateTime onDate)
        {
            return DataModel.GetY(onDate);
        }

        public PettyCashSheet GetPrimary(DateTime onDate)
        {
            return DataModel.Get(onDate);
        }

        public bool FetchTodayOrYesterday()
        {
            if (PrimaryEntity == null || SecondaryEntity == null)
            {
                PrimaryEntity = GetPrimary(DateTime.Now);
                if (PrimaryEntity == null)
                {
                    PrimaryEntity = GetPrimary(DateTime.Today.AddDays(-1));
                    if (PrimaryEntity != null)
                        SecondaryEntity = GetSecondary(DateTime.Today.AddDays(-1));
                    else return false;
                    return true;
                }
            }
            return true;
        }


        public string MissingReport()
        {
           // string filename = new PettyCashSheetManager(azureDb, localDb).GenReport();
            return "";
        }
        #endregion OpsFunctions

        #region PrivateOpsFuncs

        private ObservableListSource<PettyCashSheet> UpdateItemList(List<PettyCashSheet> items)
        {
            foreach (var item in items)
            {
                ItemList.Add(item);
            }
            return ItemList;
        }
       
        public List<PettyCashSheet> GenDBSheet()
        {
            DateTime startDate = new DateTime(2021, 04, 13);
            DateTime endDate = new DateTime(2021, 06, 01);

            var dailySale = DataModel.DailySale(startDate, endDate);
            var expen = DataModel.Vouchers(startDate, endDate);
            var cash = DataModel.CashVouchers(startDate, endDate);

            decimal openbal = 3109;
            List<PettyCashSheet> cashSheet = new List<PettyCashSheet>();

            DateTime onDate = startDate;
            do
            {
                PettyCashSheet pcs = new PettyCashSheet
                {
                    OnDate = onDate,
                    CardSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.PayMode == PayMode.Card).Select(c => c.Amount).Sum(),
                    ManualSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.ManualBill).Select(c => c.Amount).Sum(),
                    DailySale = dailySale.Where(c => c.OnDate.Date == onDate.Date && !c.ManualBill && !c.TailoringBill && !c.SalesReturn).Select(c => c.Amount).Sum(),
                    NonCashSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.PayMode != PayMode.Cash && c.PayMode != PayMode.Card).Select(c => c.Amount).Sum(),

                    BankDeposit = 0,
                    BankWithdrawal = 0,
                    ClosingBalance = 0,
                    CustomerDue = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.IsDue).Select(c => c.Amount).Sum(),
                    CustomerRecovery = 0,
                    TailoringSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.TailoringBill).Select(c => c.Amount).Sum(),
                    OpeningBalance = openbal,
                    PaymentNaration = "",
                    ReceiptsNaration = "",
                    DueList = "#",
                    RecoveryList = "#",
                    TailoringPayment = 0,
                    Id = $"ARD/{onDate.Year}/{onDate.Month}/{onDate.Day}",

                    ReceiptsTotal = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt).Select(c => c.Amount).Sum(),
                    PaymentTotal = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashPayment).Select(c => c.Amount).Sum() +
                              expen.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.Expense && c.PaymentMode == PaymentMode.Cash).Select(c => c.Amount).Sum(),
                };

                var recs = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt)
                    .Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();
                var pay = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashPayment).Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();
                var exps = expen.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.Expense && c.PaymentMode == PaymentMode.Cash).Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();

                foreach (var item in recs)
                {
                    pcs.ReceiptsNaration += $"#{item.SlipNumber} / {item.Particulars}/{item.Remarks} :{item.Amount} ";
                }
                var ds = dailySale.Where(c => c.OnDate.Date == onDate.Date).ToList();
                foreach (var item in ds)
                {
                    pcs.ReceiptsNaration += $"#{item.InvoiceNumber} / {item.Remarks}/{item.PayMode} :{item.Amount} ";
                }
                // pcs.PaymentNaration = "$PAYMENT=>";
                foreach (var item in pay)
                {
                    pcs.PaymentNaration += $"#{item.SlipNumber} = {item.Particulars}={item.Remarks} :{item.Amount} ";
                }
                // pcs.PaymentNaration+= "$Expenses=>";
                foreach (var item in exps)
                {
                    pcs.PaymentNaration += $"#{item.SlipNumber} / {item.Particulars}/{item.Remarks} :{item.Amount} ";
                }

                var colbal = pcs.OpeningBalance + pcs.DailySale + pcs.ManualSale + pcs.TailoringSale + pcs.CustomerRecovery + pcs.ReceiptsTotal + pcs.BankWithdrawal;
                colbal = colbal - (pcs.PaymentTotal + pcs.CardSale + pcs.TailoringPayment + pcs.CustomerDue + pcs.BankDeposit);
                pcs.ClosingBalance = colbal;
                cashSheet.Add(pcs);
                onDate = onDate.AddDays(1);
                openbal = colbal;
            }
            while (onDate < endDate);

            return cashSheet;
        }

        #endregion PrivateOpsFuncs

        #region ReportSections

        public string GeneratePettyCashSheetPdf()
        {
            return "";
        }

        #endregion ReportSections
    }
}