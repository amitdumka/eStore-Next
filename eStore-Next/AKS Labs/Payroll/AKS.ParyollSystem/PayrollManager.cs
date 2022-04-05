﻿using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;

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
        public bool CalculateMonthlyAttendance(AzurePayrollDbContext db, string empId, DateTime onDate)
        {
            if (db == null) db = new AzurePayrollDbContext();

            var attdList = db.Attendances.Where(x => x.EmployeeId == empId && x.OnDate.Year == onDate.Year && x.OnDate.Month == onDate.Month).ToList();

            MonthlyAttendance ma = new()
            {
                NoOfWorkingDays = 26,
                MonthlyAttendanceId = IdentityGenerator.GenerateMonthlyAttendance(onDate, empId.Split("/")[3]),
                StoreId = attdList.FirstOrDefault().StoreId,
                EmployeeId = empId,
                OnDate = onDate,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                Remarks = "AutoGenerated on " + DateTime.Now,
                Present = attdList.Where(c => c.Status == AttUnit.Present).Count(),
                Absent = attdList.Where(c => c.Status == AttUnit.Absent || c.Status == AttUnit.OnLeave).Count(),
                PaidLeave = attdList.Where(c => c.Status == AttUnit.PaidLeave || c.Status == AttUnit.SickLeave).Count(),
                HalfDay = attdList.Where(c => c.Status == AttUnit.HalfDay).Count(),
                MarkedDeleted = false,
                Sunday = attdList.Where(c => c.Status == AttUnit.Sunday).Count(),
                CasualLeave = attdList.Where(c => c.Status == AttUnit.CasualLeave).Count(),
                WeeklyLeave = attdList.Where(c => c.Status == AttUnit.SundayHoliday).Count(),
                UserId = "AutoAdmin",
                Holidays = attdList.Where(c => c.Status == AttUnit.Holiday || c.Status == AttUnit.StoreClosed).Count(),
            };

            if (!ma.Valid)
                ma.Remarks += $"#Error#";
            if (db.MonthlyAttendances.Find(ma.MonthlyAttendanceId) != null)

                db.MonthlyAttendances.Update(ma);
            else db.MonthlyAttendances.Add(ma);

            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// Calculate for all month for an employee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="empId"></param>
        public bool CalculateMonthlyAttendance(AzurePayrollDbContext db, string empId)
        {
            if (db == null) db = new AzurePayrollDbContext();
            var yearList = db.Attendances.Where(c => c.EmployeeId == empId).Select(c => c.OnDate.Year).Distinct().ToList();
            //yearList.Distinct();
            foreach (var year in yearList)
            {
                var attnds = db.Attendances.Where(c => c.EmployeeId == empId && c.OnDate.Year == year).ToList();

                for (int i = 1; i <= 12; i++)
                {
                    DateTime onDate = new DateTime(year, i, 1);

                    var attdList = attnds.Where(x => x.EmployeeId == empId && x.OnDate.Year == year
                    && x.OnDate.Month == i).ToList();
                    if (attdList != null && attdList.Count > 0)
                    {
                        MonthlyAttendance ma = new()
                        {
                            NoOfWorkingDays = 26,
                            MonthlyAttendanceId = IdentityGenerator.GenerateMonthlyAttendance(onDate, empId.Split("/")[3]),
                            StoreId = attdList.FirstOrDefault().StoreId,
                            EmployeeId = empId,
                            OnDate = onDate,
                            EntryStatus = EntryStatus.Added,
                            IsReadOnly = true,
                            Remarks = "AutoGenerated on " + DateTime.Now,
                            Present = attdList.Where(c => c.Status == AttUnit.Present).Count(),
                            Absent = attdList.Where(c => c.Status == AttUnit.Absent || c.Status == AttUnit.OnLeave).Count(),
                            PaidLeave = attdList.Where(c => c.Status == AttUnit.PaidLeave || c.Status == AttUnit.SickLeave).Count(),
                            HalfDay = attdList.Where(c => c.Status == AttUnit.HalfDay).Count(),
                            MarkedDeleted = false,
                            Sunday = attdList.Where(c => c.Status == AttUnit.Sunday).Count(),
                            CasualLeave = attdList.Where(c => c.Status == AttUnit.CasualLeave).Count(),
                            WeeklyLeave = attdList.Where(c => c.Status == AttUnit.SundayHoliday).Count(),
                            UserId = "AutoAdmin",
                            Holidays = attdList.Where(c => c.Status == AttUnit.Holiday || c.Status == AttUnit.StoreClosed).Count(),
                        };
                        if (db.MonthlyAttendances.Find(ma.MonthlyAttendanceId) != null)

                            db.MonthlyAttendances.Update(ma);
                        else db.MonthlyAttendances.Add(ma);
                    }
                }
            }
            int saveRecord = db.SaveChanges();
            db.Dispose();
            if (saveRecord > 0 && (yearList.Count * 12) == saveRecord) return true; else return false;
        }

        /// <summary>
        /// Calculate for a month for all employee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="onDate"></param>
        public bool CalculateMonthlyAttendance(AzurePayrollDbContext db, DateTime onDate)
        {
            if (db == null) db = new AzurePayrollDbContext();
            var attL = db.Attendances.Where(c => c.OnDate.Year == onDate.Year && c.OnDate.Month == onDate.Month).ToList();
      
            if (attL.Count > 0)
            {
                var empIdList = attL.Select(c => c.EmployeeId).Distinct().ToList();
            
                foreach (var empId in empIdList)
                {
                    var attdList = attL.Where(c => c.EmployeeId == empId).ToList();
                    MonthlyAttendance ma = new()
                    {
                        NoOfWorkingDays = 26,
                        MonthlyAttendanceId = IdentityGenerator.GenerateMonthlyAttendance(onDate, empId.Split("/")[3]),
                        StoreId = attdList.FirstOrDefault().StoreId,
                        EmployeeId = empId,
                        OnDate = onDate,
                        EntryStatus = EntryStatus.Added,
                        IsReadOnly = true,
                        Remarks = "AutoGenerated on " + DateTime.Now,
                        Present = attdList.Where(c => c.Status == AttUnit.Present).Count(),

                        Absent = attdList.Where(c => c.Status == AttUnit.Absent || c.Status == AttUnit.OnLeave).Count(),
                        PaidLeave = attdList.Where(c => c.Status == AttUnit.PaidLeave || c.Status == AttUnit.SickLeave).Count(),
                        HalfDay = attdList.Where(c => c.Status == AttUnit.HalfDay).Count(),
                        MarkedDeleted = false,
                        Sunday = attdList.Where(c => c.Status == AttUnit.Sunday).Count(),
                        CasualLeave = attdList.Where(c => c.Status == AttUnit.CasualLeave).Count(),
                        WeeklyLeave = attdList.Where(c => c.Status == AttUnit.SundayHoliday).Count(),
                        UserId = "AutoAdmin",
                        Holidays = attdList.Where(c => c.Status == AttUnit.Holiday || c.Status == AttUnit.StoreClosed).Count(),
                    };

                    if (!ma.Valid)
                        ma.Remarks += $"#Error#";
                    if (db.MonthlyAttendances.Where(c => c.MonthlyAttendanceId == ma.MonthlyAttendanceId).Count() > 0)

                        db.MonthlyAttendances.Update(ma);
                    else
                        db.MonthlyAttendances.Add(ma);
                }
                int saveRecord = db.SaveChanges();
                if (saveRecord > 0 && empIdList.Count == saveRecord) return true; else return false;
            }
            else return false;
        }

        public void ProcessMonthlyAttendanceForAllEmployee(AzurePayrollDbContext db)
        {
            if (db == null) db = new AzurePayrollDbContext();

            var emplist = db.Attendances.GroupBy(c => c.EmployeeId).Select(c => c.Key).ToList();
            if (emplist.Any())
            {
                foreach (var empId in emplist)
                {
                    CalculateMonthlyAttendance(db, empId);
                }
            }
        }
    }
}

 