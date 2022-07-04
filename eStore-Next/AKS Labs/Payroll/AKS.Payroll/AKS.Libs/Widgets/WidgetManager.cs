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

                // Today Attendance
                var today = azureDb.Attendances.Where(c => c.StoreId == StoreCode && c.OnDate.Date == DateTime.Today.Date)
                       .Select(c => new { c.EmployeeId, c.Status }).ToList();
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
                    .Select(c => new { ID = c.Key.SalesmanId, AMT = c.Sum(d => d.Amount) })
                    .ToList();

                //YearlSale
                var yearly = azureDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    //.Select(c => new { c.Amount, c.SalesmanId })
                    .GroupBy(c => new { c.SalesmanId, c.Amount }).OrderBy(c => c.Key.SalesmanId)
                    .Select(c => new { ID = c.Key.SalesmanId, AMT = c.Sum(d => d.Amount) })
                    .ToList();

                var mAtt = azureDb.Attendances.
                    Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year &&
                    c.OnDate.Month == DateTime.Today.Month)

                    .ToList();

                foreach (var emp in emplist)
                {
                    if (emp.Category == EmpType.Salesman)
                    {
                        emp.NoOfBill = daily.Where(c => c.ID == emp.EmployeeId).FirstOrDefault().Count;
                        emp.MonthlySale = monthly.Where(c => c.ID == emp.EmployeeId).FirstOrDefault().AMT;
                        emp.DailySale = daily.Where(c => c.ID == emp.EmployeeId).FirstOrDefault().AMT;
                        emp.YearlySale = yearly.Where(c => c.ID == emp.EmployeeId).FirstOrDefault().AMT;
                        //emp.MonthlyAverage=
                    }
                    else if (emp.Category == EmpType.StoreManager)
                    {
                        emp.NoOfBill = daily.Sum(c => c.Count);
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