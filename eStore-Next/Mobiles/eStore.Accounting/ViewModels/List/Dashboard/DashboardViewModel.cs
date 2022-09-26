using AKS.Shared.Commons.Ops;
using eStore_MauiLib.ViewModels;

namespace eStore.Accounting.ViewModels.List.Dashboard
{
    public partial class AccountingDashboardViewModel : BaseDashoardViewModel<AccountWidget>
    {
        private bool _localSync;

        public AccountingDashboardViewModel()
        {
            DataModel = new eStore_MauiLib.DataModels.DashboardDataModel(ConType.Hybrid);
            DataModel.Mode = DBType.Azure;
            InitView();
            this.Icon = eStore.Accounting.Resources.Styles.IconFont.BookReader;
            this.Title = "Dashboard";
        }
        protected void InitView()
        {
            DataModel.Connect();
            Fetch();
        }
        protected void Fetch()
        {
            if (Entity == null)
            {
                var voucherData = DataModel.GetContext().Vouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    .GroupBy(c =>   c.VoucherType ).Select(c => new { VT = c.Key, TAmount = c.Sum(x => x.Amount) }).ToList();
                var cashVoucherData = DataModel.GetContext().CashVouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    .GroupBy(c => c.VoucherType).Select(c => new { VT = c.Key, TAmount = c.Sum(x => x.Amount) }).ToList();
                var due = DataModel.GetContext().CustomerDues.Where(c => c.StoreId == CurrentSession.StoreCode).Sum(c => c.Amount);
                var rec = DataModel.GetContext().DueRecovery.Where(c => c.StoreId == CurrentSession.StoreCode).Sum(c => c.Amount);

                Entity =   new AccountWidget
                {
                    OnDate = DateTime.Now,
                    TotalReceipt =   voucherData.Where(c => c.VT == VoucherType.Receipt).FirstOrDefault().TAmount,
                    TotalCashPayment = cashVoucherData.Where(c => c.VT == VoucherType.CashPayment).FirstOrDefault().TAmount,
                    TotalCashReceipt = cashVoucherData.Where(c => c.VT == VoucherType.CashReceipt).FirstOrDefault().TAmount,
                    TotalExpenses = voucherData.Where(c => c.VT == VoucherType.Expense).FirstOrDefault().TAmount,
                    TotalPayment = voucherData.Where(c => c.VT == VoucherType.Payment).FirstOrDefault().TAmount,
                    BankDeposit = -1,
                    BankWithdrwal = -1,
                    CashInBank = -1,
                    TotalDueRecorver =rec,
                    TotalDueAmount = due,
                    CashInHand = -1,
                    TotalSale = 0,
                    TotalCashSale = 0,
                    TotalMonthlyCashSale = 0,
                    TotalMonthlySale = 0

                };
                
            }
            
        }
    }

    public class AccountWidget
    {
        public DateTime OnDate { get; set; }

        public decimal TotalCashPayment { get; set; }
        public decimal TotalCashReceipt { get; set; }

        public decimal TotalPayment { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalReceipt { get; set; }

        public decimal BankDeposit { get; set; }
        public decimal BankWithdrwal { get; set; }

        public decimal CashInHand { get; set; }
        public decimal CashInBank { get; set; }

        public decimal TotalDueAmount { get; set; }
        public decimal TotalDueRecorver { get; set; }
        public decimal TotalDuePending { get { return TotalDueAmount - TotalDueRecorver; } }

        public decimal TotalSale { get; set; }
        public decimal TotalNonCashSale { get { return TotalSale - TotalCashSale; } }
        public decimal TotalCashSale { get; set; }

        public decimal TotalMonthlySale { get; set; }
        public decimal TotalMonthlyNonCashSale { get { return TotalMonthlySale - TotalMonthlyCashSale; } }
        public decimal TotalMonthlyCashSale { get; set; }

        public decimal TotalIncome { get { return TotalSale + TotalCashReceipt + TotalReceipt; } }
        public decimal TotalExpense { get { return TotalPayment + TotalCashPayment + TotalExpenses; } }

    }
}