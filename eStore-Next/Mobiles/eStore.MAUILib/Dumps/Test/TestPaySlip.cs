using eStore_MauiLib.Printers.Thermals;

namespace eStore_Maui.Test
{
    public class TestPaySlip
    {
        public static void Print()
        {
            PaySlipPrint ps = new PaySlipPrint
            {
                Absent = 1,
                BasicDayRate = 503,
                CurrentMonthSalary = 5000,
                FileName = "Test.pdf",
                Page2Inch = true,
                Incentive = 0,
                IsDataSet = true,
                LastMonthAdvance = 1000,
                LastPcs = 0,
                Month = 3,
                NetPayableSalary = 6000,
                NoOfCopy = 2,
                PathName = @"ARD/Payslip/",
                PaySlipNo = "Test001",
                Present = 23,
                PrintType = PrintType.Payslip,
                Reprint = false,
                SalaryAdvance = 1000,
                StaffName = "Test Name",
                StoreCode = "ARD",
                WorkingDay = 26,
                WowBill = 0,
                Year = 2022
            };
            ps.PrintPdf(false, true);
        }
    }
}