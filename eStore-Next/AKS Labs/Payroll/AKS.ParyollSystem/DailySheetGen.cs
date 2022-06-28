using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Accounts;

namespace AKS.ParyollSystem
{
    public class DailySheetGen
    {
        DateTime startDate = new DateTime(2021, 04, 13);
        DateTime endDate = new DateTime(2021, 06, 01);
        AzurePayrollDbContext db = new AzurePayrollDbContext();

        public List<PettyCashSheet> GenDBSheet()
        {
            var dailySale = db.DailySales.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            var expen = db.Vouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            var cash = db.CashVouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            decimal openbal = 3109;
            List<PettyCashSheet> cashSheet = new List<PettyCashSheet>();
     
            DateTime onDate = startDate;
            do
            {
                PettyCashSheet pcs = new PettyCashSheet
                {
                    OnDate = onDate,
                    CardSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && !c.TailoringBill && !c.ManualBill && c.PayMode == PayMode.Card).Select(c => c.Amount).Sum(),
                    BankDeposit = 0,
                    BankWithdrawal = 0,
                    ClosingBalance = 0,
                    CustomerDue = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.IsDue).Select(c => c.Amount).Sum(),
                    CustomerRecovery = 0,
                    ManualSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.ManualBill).Select(c => c.Amount).Sum(),
                    DailySale = dailySale.Where(c => c.OnDate.Date == onDate.Date && !c.ManualBill && !c.TailoringBill && !c.SalesReturn).Select(c => c.Amount).Sum(),
                    NonCashSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.PayMode != PayMode.Card && c.PayMode != PayMode.Card).Select(c => c.Amount).Sum(),
                    TailoringSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.TailoringBill).Select(c => c.Amount).Sum(),
                    OpeningBalance = openbal, DueList = "#", RecoveryList = "#", TailoringPayment = 0, Id = $"ARD/{onDate.Year}/{onDate.Month}/{onDate.Day}",
                   
                    ReceiptsTotal = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt).Select(c => c.Amount).Sum(),
                    PaymentTotal = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashPayment).Select(c => c.Amount).Sum()+
                              expen.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.Expense && c.PaymentMode==PaymentMode.Cash) .Select(c => c.Amount).Sum(), 
                 
                };
                var recs = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt).Select(c => new {c.Amount, c.Particulars ,c.SlipNumber,c.Remarks}).ToList();
                var pay = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt).Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();
                var exps=  expen.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.Expense && c.PaymentMode == PaymentMode.Cash).Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();

                var colbal = pcs.OpeningBalance + pcs.DailySale + pcs.ManualSale + pcs.TailoringSale + pcs.CustomerRecovery + pcs.ReceiptsTotal+pcs.BankWithdrawal;
                colbal = colbal-(pcs.PaymentTotal + pcs.CardSale + pcs.TailoringPayment + pcs.CustomerDue+pcs.BankDeposit);
                 pcs.ClosingBalance = colbal;
                cashSheet.Add(pcs);
                onDate = onDate.AddDays(1);
                openbal = colbal;
            }
            while (onDate < endDate);

            return cashSheet;
        }


    }
}
