using AKS.Shared.Commons.Ops;
using eStore_MauiLib.ViewModels;

namespace eStore.Accounting.ViewModels.List.Dashboard
{
    public partial class AccountingDashboardViewModel : BaseDashoardViewModel<AccountWidget>
    {
        private bool _localSync;

        public AccountingDashboardViewModel() : base()
        {
            DataModel = new eStore_MauiLib.DataModels.DashboardDataModel(ConType.Hybrid);
            DataModel.Mode = DBType.Azure;
        }

        protected void Fetch()
        {
            var voucherData = DataModel.GetContext().Vouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                .GroupBy(c => new { c.VoucherType, c.Amount }).Select(c => new { VT = c.Key.VoucherType, TAmount = c.Sum(x => x.Amount) }).ToList();
            // var bankData = DataModel.GetContext().BankTranscations.Select(c => new { c.Amount,c. }).ToList();
            var dueData =
            Entity = new AccountWidget
            {
                OnDate = DateTime.Now,
                TotalReceipt = voucherData.Where(c => c.VT == VoucherType.Receipt).FirstOrDefault().TAmount,
                TotalCashPayment = voucherData.Where(c => c.VT == VoucherType.CashPayment).FirstOrDefault().TAmount,
                TotalCashReceipt = voucherData.Where(c => c.VT == VoucherType.CashReceipt).FirstOrDefault().TAmount,
                TotalExpenses = voucherData.Where(c => c.VT == VoucherType.Expense).FirstOrDefault().TAmount,
                TotalPayment = voucherData.Where(c => c.VT == VoucherType.Payment).FirstOrDefault().TAmount,
                BankDeposit = -1,
                BankWithdrwal = -1,
                CashInBank = -1,
                TotalDueRecorver =
                DataModel.GetContext().DueRecovery.Where(c => c.StoreId == CurrentSession.StoreCode).Sum(c => c.Amount),
                TotalDueAmount = DataModel.GetContext().CustomerDues.Where(c => c.StoreId == CurrentSession.StoreCode).Sum(c => c.Amount)

            };
            Entity.TotalDuePending = Entity.TotalDueAmount - Entity.TotalDueRecorver;
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
        public decimal TotalDuePending { get; set; }
    }
}