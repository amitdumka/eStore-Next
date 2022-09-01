using AKS.Payroll.Database;
using AKS.Shared.Commons.ViewModels.Widgets;

namespace AKS.Libs.Widgets
{
    /// <summary>
    /// First line Widget Manager
    /// </summary>
    public class WidgetManager
    {
        private AzurePayrollDbContext azureDb;
        private string StoreCode = "ARD";

        public List<EmployeeInfoWidget> FetchEmployeeInfo()
        {
            try
            {
                // Employee List
                var emplist = azureDb.Employees.Where(c => c.StoreId == StoreCode && c.IsWorking)
                    .Select(c => new EmployeeInfoWidget
                    {
                        EmployeeId = c.EmployeeId,
                        Name = c.StaffName,
                        Category = c.Category
                    }).ToList();
                var smList = azureDb.Salesmen.Where(c => c.StoreId == StoreCode).Select(c => new { c.EmployeeId, c.SalesmanId }).ToList();
                // Today Attendance
                var today = azureDb.Attendances.Where(c => c.StoreId == StoreCode && c.OnDate.Date == DateTime.Today.Date)
                       .Select(c => new { c.EmployeeId, c.Status }).ToList();
                //Monthly Attendnace
                var month = azureDb.Attendances.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
                    .GroupBy(c => new { c.EmployeeId, c.Status })
                    .Select(c => new { ID = c.Key.EmployeeId, UNIT = c.Key.Status, CTR = c.Count() }).ToList();

                //Daily Sale
                var daily = azureDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Date == DateTime.Today.Date)
                    //.Select(c => new { c.Amount, c.SalesmanId })
                    .GroupBy(c => new { c.SalesmanId, c.Amount }).OrderBy(c => c.Key.SalesmanId)
                    .Select(c => new { ID = c.Key.SalesmanId, AMT = c.Sum(d => d.Amount), Count = c.Count() })
                    .ToList();

                //Monthly Sale
                var monthly = azureDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
                    //.Select(c => new { c.Amount, c.SalesmanId })
                    .GroupBy(c => new { c.SalesmanId, c.Amount }).OrderBy(c => c.Key.SalesmanId)
                    .Select(c => new { ID = c.Key.SalesmanId, AMT = c.Sum(d => d.Amount), Count = c.Count() })
                    .ToList();

                //YearlSale
                var yearly = azureDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    //.Select(c => new { c.Amount, c.SalesmanId })
                    .GroupBy(c => new { c.SalesmanId, c.Amount }).OrderBy(c => c.Key.SalesmanId)
                    .Select(c => new { ID = c.Key.SalesmanId, AMT = c.Sum(d => d.Amount), Count = c.Count() })
                    .ToList();

                var mAtt = azureDb.Attendances.
                    Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year &&
                    c.OnDate.Month == DateTime.Today.Month)
                    .GroupBy(c => new { c.EmployeeId, c.Status })
                    .Select(c => new { c.Key.EmployeeId, c.Key.Status, CTR = c.Count() })
                    .ToList();

                foreach (var emp in emplist)
                {
                    //Calucating Monthly Attendace count.
                    var eAtt = mAtt.Where(c => c.EmployeeId == emp.EmployeeId).ToList();
                    var hf = eAtt.Where(c => c.Status == AttUnit.HalfDay).Select(c => c.CTR).FirstOrDefault();


                    if (hf != null && hf > 0) hf = hf / 2;
                    else hf = 0;

                    emp.MonthlyPresent += hf;
                    emp.MonthlyAbsent += hf;

                    emp.MonthlyPresent += (decimal?)eAtt.FirstOrDefault(c => c.Status == AttUnit.Present).CTR ?? 0;
                    emp.MonthlyAbsent += (decimal?)eAtt.Where(c => c.Status == AttUnit.Absent || c.Status == AttUnit.Sunday).Select(c => c.CTR).FirstOrDefault() ?? 0;
                    // Updating Today's Attendances

                    emp.Today = (today.FirstOrDefault(c => c.EmployeeId == emp.EmployeeId)?.Status) ?? AttUnit.Absent;
                    //TODO: no of bill has issue with salesman
                    // Calculating Sale data.
                    if (emp.Category == EmpType.Salesman)
                    {
                        var smid = smList.FirstOrDefault(c => c.EmployeeId == emp.EmployeeId).SalesmanId;
                        emp.NoOfBill = monthly.Where(c => c.ID == smid).Select(c => c.Count).FirstOrDefault();
                        emp.MonthlySale = (decimal?)monthly.Where(c => c.ID == smid).Select(c => c.AMT).FirstOrDefault() ?? 0;
                        emp.DailySale = (decimal?)daily.Where(c => c.ID == smid).Select(c => c.AMT).FirstOrDefault() ?? 0;
                        emp.YearlySale = (decimal?)yearly.Where(c => c.ID == smid).Select(c => c.AMT).FirstOrDefault() ?? 0;
                        //emp.MonthlyAverage=
                    }
                    else if (emp.Category == EmpType.StoreManager)
                    {
                        emp.NoOfBill = monthly.Sum(c => c.Count);
                        emp.MonthlySale = monthly.Sum(c => c.AMT);
                        emp.DailySale = daily.Sum(c => c.AMT); ;
                        emp.YearlySale = yearly.Sum(c => c.AMT);
                    }
                }
                return emplist;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void GenerateFirstPageWidget(AzurePayrollDbContext db, string storecode)
        {
            DateTime on = DateTime.Now;
            StoreCode = storecode;
            azureDb = db;
        }
    }
}