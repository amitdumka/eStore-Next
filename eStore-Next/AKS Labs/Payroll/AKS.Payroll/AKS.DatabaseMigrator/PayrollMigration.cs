using Microsoft.EntityFrameworkCore;

namespace AKS.DatabaseMigrator
{
    public class PayrollMigration
    {
        private eStoreDbContext db;
        private AKSDbContext AKS;

        public PayrollMigration(eStoreDbContext oldDB, AKSDbContext newDB)
        {
            db = oldDB; AKS = newDB;
        }

        public PayrollMigration()
        { }

        public void Migrate()
        {
            if (AKS == null) AKS = new AKSDbContext();
            if (db == null) db = new eStoreDbContext();

          //  MigrateEmployee();
            MigrateAttendance();
        }

        private void MigrateEmployee()
        {
                
            var emps = db.Employees.ToList();
            foreach (var emp in emps)
            {
                AKS.Shared.Payroll.Models.Employee nEmp = new()
                {
                    EmployeeId = $"ARD/{emp.JoiningDate.Year}/{emp.Category.ToString()}/{emp.EmployeeId}",
                    AddressLine = emp.Address,
                    Category = emp.Category,
                    Id = emp.EmployeeId,
                    Gender = Shared.Payroll.Models.Base.Gender.Male,
                    IsTailors = emp.IsTailors,
                    City = emp.City,
                    Country = "India",
                    DOB = emp.DateOfBirth,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    IsWorking = emp.IsWorking,
                    JoiningDate = emp.JoiningDate,
                    LeavingDate = emp.LeavingDate,
                    State = emp.State,
                    StreetName = emp.Address,
                    ZipCode = "814101",
                    Title = "Mr."
                };
                switch (emp.Category)
                {
                    case EmpType.Salesman:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "SLM");
                        break;
                    case EmpType.StoreManager:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "SM");
                        break;
                    case EmpType.HouseKeeping:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "HK");
                        break;
                    case EmpType.Owner:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "OWN");
                        break;
                    case EmpType.Accounts:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "ACC");
                        break;
                    case EmpType.TailorMaster:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "TM");
                        break;
                    case EmpType.Tailors:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "TLS");
                        break;
                    case EmpType.TailoringAssistance:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "TLA");
                        break;
                    case EmpType.Others:
                        nEmp.EmployeeId = nEmp.EmployeeId.Replace(nEmp.Category.ToString(), "OTH");
                        break;
                    default:
                        break;
                }
                AKS.Employees.Add(nEmp);
                AKS.Shared.Payroll.Models.EmployeeDetails nEmpDetails = new Shared.Payroll.Models.EmployeeDetails
                {
                    AdharNumber = emp.AdharNumber,
                    DateOfBirth = emp.DateOfBirth,
                    EmployeeId = nEmp.EmployeeId,
                    EntryStatus = emp.EntryStatus,
                    FatherName = emp.FatherName,
                    HighestQualification = emp.HighestQualification,
                    IsReadOnly = emp.IsReadOnly,
                    MaritalStatus = "Married",
                    OtherIdDetails = emp.OtherIdDetails,
                    PanNo = emp.PanNo,
                    SpouseName = "NA",
                    StoreId = emp.StoreId,
                    StoreCode = emp.StoreId.ToString(),
                    UserId = emp.UserId
                };
                AKS.EmployeeDetails.Add(nEmpDetails);
            }
            int c= AKS.SaveChanges();
            Console.WriteLine(c);
            if(c!=emps.Count)
                Console.WriteLine($"C={c}\te={emps.Count}");
        }

        private void MigrateSalary()
        { }

        private void MigrateAttendance()
        {
            if (db == null)
                db = new eStoreDbContext();
            var empList = AKS.Employees.Select(c => new { c.EmployeeId, c.Id }).OrderBy(c=>c.Id).ToList();
            foreach (var emp in empList)
            {


                var attd = db.Attendances.Where(c=>c.EmployeeId==emp.Id).ToList();

                foreach (var item in attd)
                {
                    Shared.Payroll.Models.Attendance Attendance = new()
                    {
                        //AttendanceId = item.AttendanceId,
                        EntryStatus = item.EntryStatus,
                        EmpId = item.EmployeeId,
                        EmployeeId = empList.Where(e => e.Id == item.EmployeeId).First().EmployeeId,
                        EntryTime = item.EntryTime,
                        IsReadOnly = item.IsReadOnly,
                        IsTailoring = item.IsTailoring,
                        OnDate = item.AttDate,
                        Remarks = item.Remarks + $"#Old_ID={item.AttendanceId}#",
                        Status = item.Status,
                        StoreCode = item.StoreId.ToString(),
                        StoreId = item.StoreId,
                        UserId = item.UserId
                    };
                    AKS.Attendances.Add(Attendance);
                }
                AKS.Database.ExecuteSqlRaw("SET IDENTITY_INSERT V1_Attendance ON;");
                int c = AKS.SaveChanges();
                AKS.Database.ExecuteSqlRaw("SET IDENTITY_INSERT V1_Attendance OFF");
                Console.WriteLine(c);
                if (c != attd.Count)
                    Console.WriteLine($"C={c}\te={attd.Count}");
            }
        }
    }
}