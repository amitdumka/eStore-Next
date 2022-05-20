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
            // MigrateStore();
             MigrateEmployee();
            //this.MigrateSalaryPayment();
            //this.MigrateSalesman();
            // MigrateAttendance();
            //var s = db.Salesmen.Distinct();
            // Console.WriteLine(s);
        }

        private void MigrateSalesman()
        {
            var slms = db.Salesmen.ToList();
            foreach (var s in slms)
            {
                Shared.Commons.Models.Salesman man = new()
                {
                    StoreId = "ARD",
                    UserId = "AutoMigration",
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    Name = s.SalesmanName,
                    IsActive = true,
                    SalesmanId = "SMN/2016/" + s.SalesmanId,
                    EntryStatus = s.EntryStatus
                };
                man.EmployeeId = string.IsNullOrEmpty(s.EmployeeId.ToString()) ? "n/a" : s.EmployeeId.ToString();

                AKS.Salesmen.Add(man);
            }
            int x = AKS.SaveChanges();
            Console.WriteLine(x);
        }

        private void MigrateStore()
        {
            db.Stores.Load();
            if (db.Stores.Local.Count < 0)
                Console.WriteLine("errpr");
            var stores = db.Stores.Local.ToList();

            foreach (var store in stores)
            {
                Shared.Commons.Models.Store nStore = new()
                {
                    BeginDate = store.OpeningDate,
                    City = store.City,
                    Country = "India",
                    EndDate = store.ClosingDate,
                    GSTIN = store.GSTNO,
                    IsActive = store.Status,
                    PanNo = store.PanNo,
                    State = "Jharkhand",
                    StoreCode = store.StoreCode,
                    StoreEmailId = "thearvindstoredumka@gmail.com",
                    StoreId = "ARD",
                    StoreManager = store.StoreManagerName,
                    StoreManagerContactNo = store.StoreManagerPhoneNo,
                    StoreName = store.StoreName,
                    StorePhoneNumber = store.PhoneNo,
                    VatNo = "Pending",
                    ZipCode = store.PinCode
                   ,
                    MarkedDeleted = false
                };
                AKS.Stores.Add(nStore);
                int c = AKS.SaveChanges();
                Console.WriteLine(c);
            }
        }

        private void MigrateEmployee()
        {
            var emps = db.Employees.ToList();
            foreach (var emp in emps)
            {
                AKS.Shared.Payroll.Models.Employee nEmp = new()
                {
                    EmployeeId = $"ARD/{emp.JoiningDate.Year}/{emp.Category}/{emp.EmployeeId}",
                    EmpId = emp.EmployeeId,
                    AddressLine = emp.Address,
                    Category = emp.Category,
                    Gender = Gender.Male,
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
                    Title = "Mr.",
                    StoreId = "ARD",
                    MarkedDeleted = false
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
                AKS.Shared.Payroll.Models.EmployeeDetails nEmpDetails = new()
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
                    UserId = emp.UserId,
                    MarkedDeleted = false,
                    StoreId = "ARD"
                };
                AKS.EmployeeDetails.Add(nEmpDetails);
            }
            int c = AKS.SaveChanges();
            Console.WriteLine(c);
            if (c != emps.Count)
                Console.WriteLine($"C={c}\te={emps.Count}");
        }

        private void MigrateSalaryPayment()
        {
            var salP = db.SalaryPayments.ToList();
            var empList = AKS.Employees.Select(c => new { c.EmpId, c.EmployeeId }).OrderBy(c => c.EmpId).ToList();
            foreach (var sal in salP)
            {
                Shared.Payroll.Models.SalaryPayment nSal = new()
                {
                    SalaryPaymentId = $"SP/{sal.PaymentDate.Year}/{sal.PaymentDate.Month}/{sal.PaymentDate.Day}/{sal.SalaryPaymentId}",
                    Amount = sal.Amount,
                    OnDate = sal.PaymentDate,
                    SalaryComponet = sal.SalaryComponet,
                    Details = sal.Details + $"#{sal.SalaryMonth}#",
                    PayMode = sal.PayMode,
                    EmployeeId = empList.Where(x => x.EmpId == sal.EmployeeId).First().EmployeeId,
                    SalaryMonth = 0000,
                    UserId = "AutoMigration",
                    IsReadOnly = false,
                    MarkedDeleted = false,
                    EntryStatus = sal.EntryStatus,
                    StoreId = "ARD"
                };
                AKS.SalaryPayment.Add(nSal);
            }
            int x = AKS.SaveChanges();
            Console.WriteLine(x);
        }

        private void MigrateAttendance()
        {
            if (db == null)
                db = new eStoreDbContext();

            var empList = AKS.Employees.Select(c => new { c.EmployeeId, c.EmpId }).OrderBy(c => c.EmpId).ToList();
            foreach (var emp in empList)
            {
                //$"ARD/{emp.JoiningDate.Year}/{emp.Category.ToString()}/{emp.EmployeeId}"

                {
                    string errorid = $"#{emp.EmpId}#{emp.EmployeeId}# ";
                    int missed = 0;
                    var attd = db.Attendances.Where(c => c.EmployeeId == emp.EmpId).OrderBy(c => c.AttDate).ToList();
                    foreach (var item in attd)
                    {
                        Shared.Payroll.Models.Attendance Attendance = new()
                        {
                            AttendanceId = $"{item.AttDate.Year}/{item.AttDate.Month}/{item.AttDate.Day}/{emp.EmpId}",
                            EntryStatus = item.EntryStatus,
                            EmployeeId = emp.EmployeeId,
                            EntryTime = item.EntryTime,
                            IsReadOnly = item.IsReadOnly,
                            IsTailoring = item.IsTailoring,
                            OnDate = item.AttDate,
                            Remarks = item.Remarks + $"#Old_ID={item.AttendanceId}#EID={item.EmployeeId}#",
                            Status = item.Status,
                            StoreId = "ARD",
                            UserId = item.UserId,
                            MarkedDeleted = false
                        };
                        try
                        {
                            AKS.Attendances.Add(Attendance);
                        }
                        catch (Exception)
                        {
                            errorid += Attendance.OnDate.ToString() + "\n";
                            AKS.Attendances.Remove(Attendance);
                            missed++;
                        }
                    }
                    //TODO: Check if it is effecting intensions
                    //if (missed > 0)
                    //    AKS.Attendances.Local.Distinct();
                    int ctr = AKS.Attendances.Local.Count;
                    int c = AKS.SaveChanges();
                    Console.WriteLine(c);
                    if (c != attd.Count)
                        Console.WriteLine($"C={c}\te={attd.Count}");
                    if (missed > 0)
                        Console.WriteLine(errorid);
                }
            }
        }
    }
}