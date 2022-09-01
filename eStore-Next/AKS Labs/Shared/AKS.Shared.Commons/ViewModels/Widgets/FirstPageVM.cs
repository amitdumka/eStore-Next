namespace AKS.Shared.Commons.ViewModels.Widgets
{
    public class FirstPageVM
    {
        public SaleWidget Sale { get; set; }
        public AccountWidget Account { get; set; }
        public EmployeeWidget Employee { get; set; }

    }
    public class SaleWidget
    {

        public DateTime OnDate { get; set; }

        public decimal Daily { get; set; }
        public decimal Monthly { get; set; }
        public decimal Yearly { get; set; }
        public decimal Qutarly { get; set; }

        public decimal DailyReturn { get; set; }
        public decimal MonthlyReturn { get; set; }
        public decimal YearlyReturn { get; set; }
        public decimal QutarlyReturn { get; set; }

        public decimal DailyManual { get; set; }
        public decimal MonthlyManual { get; set; }
        public decimal YearlyManual { get; set; }
        public decimal QutarlyManual { get; set; }

        public decimal CardSale { get; set; }
        public decimal CashSale { get; set; }
        public decimal NonCashSale { get; set; }

        public decimal CardSaleMonthly { get; set; }
        public decimal CashSaleMonthly { get; set; }
        public decimal NonCashSaleMonthly { get; set; }

        public decimal CardSaleYearly { get; set; }
        public decimal CashSaleYearly { get; set; }
        public decimal NonCashSaleYearly { get; set; }

        public decimal DailyAdjustment { get; set; }
        public decimal MonthlyAdjustment { get; set; }
        public decimal YearlyAdjustment { get; set; }


    }

    public class AccountWidget
    {

        public DateTime OnDate { get; set; }

        public decimal TotalCashPayment { get; set; }
        public decimal TotalCashExpenses { get; set; }
        public decimal TotalCashReceipt { get; set; }

        public decimal TotalPayment { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalReceipt { get; set; }

        public decimal TotalNonCashPayment { get { return TotalPayment - TotalCashPayment; } }
        public decimal TotalNonCashExpenses { get { return TotalExpenses - TotalCashExpenses; } }
        public decimal TotalNonCashReceipt { get { return TotalReceipt - TotalCashReceipt; } }

        public decimal BankDeposit { get; set; }
        public decimal BankWithdrwal { get; set; }

    }
    public class TailoringWidget
    {

        public DateTime OnDate { get; set; }

        public int NoOfBooking { get; set; }
        public int NoOfDelivery { get; set; }

        public decimal TotalBookingAmount { get; set; }
        public decimal TotalDeliveryAmount { get; set; }

        public int TotalSuitBooking { get; set; }
        public int TotalSuitDeliver { get; set; }
    }
    public class EmployeeInfoWidget
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public EmpType Category { get; set; }

        public decimal DailySale { get; set; }
        public decimal MonthlySale { get; set; }
        public decimal YearlySale { get; set; }
        public int NoOfBill { get; set; }

        public AttUnit Today { get; set; }

        public decimal MonthlyPresent { get; set; }
        public decimal MonthlyAbsent { get; set; }

        public decimal MonthlyAverage { get { return (MonthlySale / MonthlyPresent); } }
        public decimal TodayAverage { get { return (DailySale / NoOfBill); } }

    }
    public class EmployeeWidget
    {
        public DateTime OnDate { get; set; }
        public List<EmployeeInfoWidget> Info { get; set; }
    }
    public class StockWidget { }
}
