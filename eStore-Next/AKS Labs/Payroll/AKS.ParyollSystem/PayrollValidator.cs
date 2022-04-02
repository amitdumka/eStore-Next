using AKS.Payroll.Database;
using AKS.Shared.Commons.Extensions.DateTimeExtensions;

namespace AKS.ParyollSystem
{
    public class PayrollValidator
    {
        /// <summary>
        /// Validate Attendance for Employee for a month return 0 if true else return differance
        /// </summary>
        /// <param name="db"></param>
        /// <param name="empId"></param>
        /// <param name="onDate"></param>
        /// <returns>return 0 if true else return differance in days</returns>
        public int ValidateAttendances(AzurePayrollDbContext db, string empId, DateTime onDate)
        {
            int noOyDays = DateTime.DaysInMonth(onDate.Year, onDate.Month);
            if (db == null) db = new AzurePayrollDbContext();
            var count = db.Attendances.Where(c => c.EmployeeId == empId && c.OnDate.Year == onDate.Year && c.OnDate.Month == onDate.Month).Count();
            return noOyDays - count;
        }

        public MissingAttendance FindMissingAttendances(AzurePayrollDbContext db, string employeeId, DateTime startDate, DateTime? endDate)
        {
            if (endDate == null) endDate = DateTime.Today;

            var emp = db.Employees.Find(employeeId);
            MissingAttendance missing = new()
            {
                EmployeeId = employeeId,
                EmployeeName = emp.StaffName,
                JoiningDate = emp.JoiningDate,
                LeavingDate = emp.LeavingDate,
                MissingDates = new List<DateTime>(),
                DuplicateDates = new List<DateTime>()
            };
            if (startDate.Month >= missing.JoiningDate.Month && startDate.Year >= missing.JoiningDate.Year)
            {
                var monthYearList = startDate.MonthsBetween(endDate.Value).ToList();
                foreach (var month in monthYearList)
                {
                    int diff = ValidateAttendances(db, employeeId, new DateTime(month.Year, month.Month, 1));
                    if (diff > 0)
                    {
                        // Missing Attendance.
                        missing.Found = true;

                        var days = db.Attendances.Where(c => c.EmployeeId == employeeId
                        && c.OnDate.Year == month.Year && c.OnDate.Month == month.Month).Select(c => c.OnDate.Date).OrderBy(c => c).ToList();

                        int noOyDays = DateTime.DaysInMonth(month.Year, month.Month);

                        missing.MissingDates.AddRange(new DateTime(month.Year, month.Month, 1)
                            .MissingDates(new DateTime(month.Year, month.Month, noOyDays), days).ToList());
                    }
                    else if (diff < 0)
                    {
                        //Possible duplciate attendance
                        missing.Duplicates = true;
                        var days = db.Attendances.Where(c => c.EmployeeId == employeeId
                       && c.OnDate.Year == month.Year && c.OnDate.Month == month.Month).GroupBy(c => c.OnDate)
                            .Select(c => new { c.Key, Count = c.Count() }).OrderBy(c => c).ToList();
                        var missdays = days.Where(c => c.Count > 1).Select(c => c.Key).ToList();
                        missing.DuplicateDates.AddRange(missdays);

                        var missingDateList = db.Attendances.Where(c => c.EmployeeId == employeeId
                      && c.OnDate.Year == month.Year && c.OnDate.Month == month.Month).GroupBy(x => new { Date = x.OnDate.Date }, s => new { InsertCount = s, })
                .Select(g => new { Date = g.Key, Count = g.Count() }).Where(c => c.Count > 10).ToList();
                    }
                }
            }
            else
            {
                missing.Found = false;
            }

            if (missing.Found && endDate.Value.Date == DateTime.Today.Date)
            {
                //TODO: need to reduce
                var dates = endDate.Value.AddDays(1).Range(new DateTime(endDate.Value.Year, endDate.Value.Month,
                    DateTime.DaysInMonth(endDate.Value.Year, endDate.Value.Month))).ToList();
                foreach (var date in dates)
                {
                    missing.MissingDates.Remove(date);
                }
            }
            return missing;
        }

        [Obsolete]
        public SortedDictionary<DateTime, int> findDuplicateDate(AzurePayrollDbContext db)
        {
            if (db == null) db = new AzurePayrollDbContext();

            var dLi = db.Attendances
                .Where(c => c.OnDate.Year == 2021)
                .GroupBy(x => new { Date = x.OnDate.Date }, s => new { InsertCount = s, })
                .Select(g => new { Date = g.Key, Count = g.Count() }).Where(c => c.Count > 1).ToList();
            SortedDictionary<DateTime, int> found = new SortedDictionary<DateTime, int>();
            foreach (var d in dLi)
                found.Add(d.Date.Date, d.Count);

            return found;
        }
    }
}

//// Initialize diff
//int diffD = days[0] - 0;
//for (int i = 0; i < noOyDays; i++)
//{
//    // Check if diff and days[i]-i
//    // both are equal or not
//    if (days[i] - i != diffD)
//    {
//        // Loop for consecutive
//        // missing elements
//        while (diffD < days[i] - i)
//        {
//            missing.MissingDates.Add(new DateTime(month.Year, month.Month, i + diffD));
//            diffD++;
//        }
//    }
//}