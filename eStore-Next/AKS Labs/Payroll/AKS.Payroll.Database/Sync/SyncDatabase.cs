//namespace AKS.Payroll.Database.Sync
//{
//    /// <summary>
//    /// This will help to sync database and its from remote to local and local to remote
//    /// </summary>
//    public class SyncDatabase
//    {
//        private readonly AzurePayrollDbContext _remoteContext;
//        private readonly LocalPayrollDbContext _localContext;

//        public SyncDatabase()
//        {
//            _remoteContext = new AzurePayrollDbContext();
//            _localContext = new LocalPayrollDbContext();
//        }

//        public void SyncUp()
//        { }

//        public int SyncDown()
//        {
//            int flag = 0;
//            if (SyncEmployee()) flag++; 
//            if(SyncAttendance()) flag++;
//            if (SyncSalary()) flag++;
//            if (SyncSalaryPayment()) flag++;
//            return flag;

//        }

//        private bool SyncEmployee()
//        {
//            var emps = _remoteContext.Employees.OrderBy(c => c.Id).ToList();
//            var empNo = _localContext.Employees.Count();
//            if (empNo != emps.Count)
//            {
//                foreach (var emp in emps)
//                {
//                    if (_localContext.Employees.Find(emp.EmployeeId) == null)
//                    {
//                        emp.Id = 0;
//                        _localContext.Employees.Add(emp);
//                        _localContext.EmployeeDetails.Add(_remoteContext.EmployeeDetails.Where(c => c.EmployeeId == emp.EmployeeId).FirstOrDefault());
//                    }
//                }
//                int recordSaved = _localContext.SaveChanges() / 2;
//                if (recordSaved > 0 && (empNo + recordSaved) == emps.Count) return true;
//                else return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        private bool SyncAttendance()
//        {
//            int remoteCount = _remoteContext.Attendances.Count();
//            int localCount = _localContext.Attendances.Count();
//            if (localCount != remoteCount)
//            {
//                int diff = remoteCount - localCount;
//                if (diff > 0)
//                {
//                    DateTime dt = _localContext.Attendances.OrderByDescending(c => c.OnDate).FirstOrDefault().OnDate;
//                    var attds = _remoteContext.Attendances.Where(c => c.OnDate == dt).OrderBy(c => c.OnDate).ThenBy(c => c.EmpId).ToList();
//                    if (diff != attds.Count)
//                    {
//                        Console.WriteLine("differance bwt remote data and missing data is not matching.");
//                        if (diff < attds.Count) return false;
//                    }
//                    foreach (var att in attds)
//                    {
//                        att.AttendanceId = 0;
//                    }
//                    _localContext.Attendances.AddRange(attds);
//                    if (_localContext.SaveChanges() != diff)
//                    {
//                        Console.WriteLine("no of recored add and missing data is not matching, Kindly report admin");

//                        return false;
//                    }
//                    else return true;
//                }
//                else
//                {
//                    return false;
//                    Console.WriteLine("Local has more data");
//                }
//            }
//            else return true;
//        }

//        private bool SyncSalary()
//        {
//            int remoteCount = _remoteContext.Salarys.Count();
//            int localCount = _localContext.Salarys.Count();
//            if (remoteCount != localCount)
//            {
//                if (remoteCount > localCount)
//                {
//                    int diff = remoteCount - localCount;
//                    var salaries = _remoteContext.Salarys.TakeLast(diff).ToList();
//                    foreach (var sal in salaries)
//                    {
//                        sal.SalaryId = 0;
//                    }
//                    _localContext.Salarys.AddRange(salaries);
//                    int saveRecord = _localContext.SaveChanges();
//                    if (saveRecord != diff) return false; else return true;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//            else return true;
//        }

//        private bool SyncSalaryPayment()
//        {
//            int remoteCount = _remoteContext.SalaryPayment.Count();
//            int localCount = _localContext.SalaryPayment.Count();
//            if (remoteCount != localCount)
//            {
//                if (remoteCount > localCount)
//                {
//                    var diff = remoteCount - localCount;
//                    var missingRecord=_remoteContext.SalaryPayment.TakeLast(diff).ToList();
//                    foreach (var item in missingRecord)
//                    {
//                        item.SalaryPaymentId = 0;
//                    }
//                    _localContext.SalaryPayment.AddRange(missingRecord);    
//                    int saveRecord = _localContext.SaveChanges();
//                    if(saveRecord != diff) return false; else return true;
//                }
//                else return true;
//            }
//            else return true;
//        }
//    }
//}