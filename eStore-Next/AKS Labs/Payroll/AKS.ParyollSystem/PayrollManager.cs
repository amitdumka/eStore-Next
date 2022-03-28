using AKS.Payroll.Database;

namespace AKS.ParyollSystem
{
    public class PayrollManager
    {
        /// <summary>
        /// Calculate for a month for a particular employee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="empId"></param>
        /// <param name="onDate"></param>
        public void CalculateMonthlyAttendance(PayrollDbContext db, string empId, DateTime onDate) { }
        /// <summary>
        /// Calculate for all month for an employee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="empId"></param>
        public void CalculateMonthlyAttendance(PayrollDbContext db, string empId) { }
        /// <summary>
        /// Calculate for a month for all employee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="onDate"></param>
        public void CalculateMonthlyAttendance(PayrollDbContext db, DateTime onDate) { }

    }
}