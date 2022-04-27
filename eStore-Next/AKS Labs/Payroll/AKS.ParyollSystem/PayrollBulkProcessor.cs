using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;

namespace AKS.ParyollSystem
{
    public class PayrollBulkProcessor
    {
        public static List<int> DuplicateSalaryLedger(AzurePayrollDbContext db, string empId)
        {
            List<int> salaryLedgerIds = new List<int>();
            var list = db.SalaryLedgers.Where(l => l.EmployeeId == empId).OrderBy(c => c.OnDate).ToList();
            foreach (var ls in list)
            {
                if (list.Any(l => l.OnDate == ls.OnDate && l.InAmount == ls.InAmount && l.OutAmount == ls.OutAmount))
                {
                    ls.IsReadOnly = false; ls.MarkedDeleted = true;
                    ls.Particulars = ls.Particulars + "#MarkedDuplicate";
                    db.SalaryLedgers.Update(ls);
                    salaryLedgerIds.Add(ls.Id);
                }

            }
            db.SaveChanges();
            return salaryLedgerIds;

        }
        public static bool ProcessSalaryLedger(AzurePayrollDbContext db, string empId)
        {
            List<SalaryLedger> ledgers = new List<SalaryLedger>();
            var payments = db.SalaryPayment.Where(c => c.EmployeeId == empId).OrderBy(c => c.OnDate).ToList();
            var recipts = db.StaffAdvanceReceipt.Where(c => c.EmployeeId == empId).OrderBy(c => c.OnDate).ToList();
            var salaries = db.PaySlips.Where(c => c.EmployeeId == empId).OrderBy(c => c.OnDate).ToList();
            foreach (var payment in payments)
            {
                SalaryLedger sl = new SalaryLedger
                {
                    EmployeeId = empId,
                    InAmount = 0,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    OnDate = payment.OnDate,
                    UserId = "AutoAdmin",
                    OutAmount = payment.Amount,
                    Particulars = "" + payment.SalaryMonth + "\t#" + payment.SalaryComponet.ToString()
                };
                ledgers.Add(sl);
            }
            foreach (var rec in recipts)
            {
                SalaryLedger sl = new SalaryLedger
                {
                    EmployeeId = empId,
                    OutAmount = 0,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    OnDate = rec.OnDate,
                    UserId = "AutoAdmin",
                    InAmount = rec.Amount,
                    Particulars = "" + rec.Details
                };
                ledgers.Add(sl);
            }
            foreach (var sal in salaries)
            {

                SalaryLedger sl = new SalaryLedger
                {
                    EmployeeId = empId,
                    InAmount = sal.TotalPayableSalary,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    OnDate = sal.OnDate,
                    UserId = "AutoAdmin",
                    OutAmount = 0,
                    Particulars = "Salary for Month " + sal.Month + "/" + sal.Year
                };
                ledgers.Add(sl);

            }


            db.SalaryLedgers.AddRange(ledgers.OrderBy(c => c.OnDate).ThenByDescending(c => c.InAmount).ToList());
            return db.SaveChanges() > 0;

        }
    }
}
