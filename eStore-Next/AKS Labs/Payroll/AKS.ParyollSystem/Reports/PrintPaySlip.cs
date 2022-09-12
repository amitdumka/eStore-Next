using AKS.Payroll.Database;
using AKS.Printers.Thermals;
using Microsoft.EntityFrameworkCore;

namespace AKS.ParyollSystem.Reports
{
    public class PrintPaySlip
    {
        public static string Print(AzurePayrollDbContext db, string EmployeeId, int year, int month, bool page2Inch, bool twoCopy)
        {
            try
            {
                // Calculating Total Advance paid in last month
                var lastMonthAdv = db.SalaryPayment.Where(c => c.EmployeeId == EmployeeId && c.SalaryComponet == SalaryComponet.Advance
                && c.OnDate.Month == month && c.OnDate.Year == year)
                    .Select(c => c.Amount).Sum();
                var monthlyAdv = db.MonthlyAttendances.Where(c => c.EmployeeId == EmployeeId && c.OnDate.Year == year && c.OnDate.Month == month).FirstOrDefault();
                var payslip = db.PaySlips.Include(c => c.Employee).Where(c => c.EmployeeId == EmployeeId && c.Year == year && c.Month == month)
                    .Select(c => new PayslipPrint
                    {
                        StaffName = c.Employee.StaffName,
                        PrintType = PrintType.Payslip,
                        PaySlipNo = c.PaySlipId,
                        StoreCode = c.StoreId,
                        BasicDayRate = c.BasicSalaryRate,
                        Present = c.NoOfDaysPresent,
                        WorkingDay = c.WorkingDays,
                        Absent = monthlyAdv.DayInMonths - monthlyAdv.BillableDays,
                        CurrentMonthSalary = c.TotalPayableSalary,
                        FileName = $"Payslip_{c.Employee.StaffName}_{c.Month}_{c.Year}.pdf",
                        Year = c.Year,
                        Month = c.Month,
                        Incentive = 0,
                        IsDataSet = true,
                        LastMonthAdvance = lastMonthAdv,
                        LastPcs = 0,
                        NetPayableSalary = c.TotalPayableSalary,
                        NoOfCopy = 2,
                        Reprint = false,
                        SalaryAdvance = 0,
                        WowBill = 0,
                        Page2Inch = page2Inch,
                        PathName= $@"d:\{c.StoreId}\PaySlips\{c.Employee.StaffName}\{c.Year}\{c.Month}\"
                    })
                    .FirstOrDefault();

                return payslip.PrintPdf(twoCopy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<string> PrintCurrentMonth(AzurePayrollDbContext db, bool page2Inch, bool twoCopy)
        {
            var payslips = new List<string>();

            var empids = db.PaySlips.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
            && c.OnDate.Year == DateTime.Today.Year).Select(c => c.EmployeeId).ToList();
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month - 1;
            foreach (var emp in empids)
            {
                payslips.Add(Print(db, emp, year, month, page2Inch, twoCopy));
            }
            return payslips;
        }
    }
}