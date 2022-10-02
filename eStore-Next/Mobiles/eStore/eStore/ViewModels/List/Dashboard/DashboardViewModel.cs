using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore.MAUILib.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using static eStore.Views.ListWidget;

namespace eStore.ViewModels.List.Dashboard
{
    public partial class AccountingDashboardViewModel : BaseDashoardViewModel<AccountWidget>
    {
        private bool _localSync;

        [ObservableProperty]
        private List<ItemList> _attData;
        [ObservableProperty]
        private List<ItemList> _saleData;
        [ObservableProperty]
        private ItemList _bankData;
        [ObservableProperty]
        private ItemList _incomeExpenseData;
        [ObservableProperty]
        private List<ItemList> _voucherList;
        [ObservableProperty]
        private List<ItemList> _cashVoucherList;
        
        public void OnAppearing()
        {
            InitView();
            this.Icon = eStore.Resources.Styles.IconFont.BookReader;
            this.Title = "Dashboard";
        }

        public AccountingDashboardViewModel()
        {
            DataModel = new MAUILib.DataModels.DashboardDataModel(ConType.Hybrid);
            DataModel.Mode = DBType.Azure;
            
        }

        protected void InitView()
        {
            DataModel.Connect();
            Fetch();
        }

        protected void Reload()
        {
            VoucherList = new List<ItemList> {
                new ItemList { Title = "Payment", Description = Entity.TotalPayment.ToString() },
                new ItemList { Title = "Expenses", Description = Entity.TotalExpenses.ToString() },
                new ItemList { Title = "Receipts", Description = Entity.TotalReceipt.ToString() }
            };

            CashVoucherList = new List<ItemList> {
                new ItemList { Title = "Payment", Description = Entity.TotalCashPayment.ToString() },
                new ItemList { Title = "Receipts", Description = Entity.TotalCashReceipt.ToString() }};

            BankData = new ItemList { Title = Entity.BankWithdrwal.ToString(), Description = Entity.BankDeposit.ToString() };
            IncomeExpenseData = new ItemList { Title = Entity.TotalIncome.ToString(), Description = Entity.TotalExpense.ToString() };

        }

        protected async void Fetch()
        {
            if (Entity == null)
            {
                var voucherData =await DataModel.GetContext().Vouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    .GroupBy(c => c.VoucherType).Select(c => new { VT = c.Key, TAmount = c.Sum(x => x.Amount) }).ToListAsync();
                var cashVoucherData = await DataModel.GetContext().CashVouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    .GroupBy(c => c.VoucherType).Select(c => new { VT = c.Key, TAmount = c.Sum(x => x.Amount) }).ToListAsync();
                var due = await DataModel.GetContext().CustomerDues.Where(c => c.StoreId == CurrentSession.StoreCode).SumAsync(c => c.Amount);
                var rec = await DataModel.GetContext().DueRecovery.Where(c => c.StoreId == CurrentSession.StoreCode).SumAsync(c => c.Amount);

                AttData = await DataModel.GetContext().Attendances.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Date == DateTime.Today.Date).Select(c => new ItemList { Title = c.EmployeeId, Description = c.Status.ToString() }).ToListAsync();

                Entity = new AccountWidget
                {
                    OnDate = DateTime.Now,
                    TotalReceipt = voucherData.Where(c => c.VT == VoucherType.Receipt).FirstOrDefault().TAmount,
                    TotalCashPayment = cashVoucherData.Where(c => c.VT == VoucherType.CashPayment).FirstOrDefault().TAmount,
                    TotalCashReceipt = cashVoucherData.Where(c => c.VT == VoucherType.CashReceipt).FirstOrDefault().TAmount,
                    TotalExpenses = voucherData.Where(c => c.VT == VoucherType.Expense).FirstOrDefault().TAmount,
                    TotalPayment = voucherData.Where(c => c.VT == VoucherType.Payment).FirstOrDefault().TAmount,
                    BankDeposit = -1,
                    BankWithdrwal = -1,
                    CashInBank = -1,
                    TotalDueRecorver = rec,
                    TotalDueAmount = due,
                    CashInHand = -1,
                    TotalSale = 0,
                    TotalCashSale = 0,
                    TotalMonthlyCashSale = 0,
                    TotalMonthlySale = 0
                };
                Reload();
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
        public decimal TotalDuePending
        { get { return TotalDueAmount - TotalDueRecorver; } }

        public decimal TotalSale { get; set; }
        public decimal TotalNonCashSale
        { get { return TotalSale - TotalCashSale; } }
        public decimal TotalCashSale { get; set; }

        public decimal TotalMonthlySale { get; set; }
        public decimal TotalMonthlyNonCashSale
        { get { return TotalMonthlySale - TotalMonthlyCashSale; } }
        public decimal TotalMonthlyCashSale { get; set; }

        public decimal TotalIncome
        { get { return TotalSale + TotalCashReceipt + TotalReceipt; } }
        public decimal TotalExpense
        { get { return TotalPayment + TotalCashPayment + TotalExpenses; } }
    }
}