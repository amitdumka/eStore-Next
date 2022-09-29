using AKS.MAUI.Databases;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eStore.DatabaseSyncService.Services
{
    public class SyncService
    {
        private AppDBContext azure;
        private AppDBContext db;

        public async Task<bool> SyncDownStoresAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Stores.Count();
            var remote = azure.Stores.Count();
            if (local != remote)
            {
                var Stores = azure.Stores.ToList();
                int recordAdded = 0;
                foreach (var Store in Stores)
                {
                    if (!db.Stores.Any(c => c.StoreCode == Store.StoreCode))
                    {
                        db.Stores.AddAsync(Store);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(Store), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(Store));
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> SyncDownPOSAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.EDCTerminals.Count();
            var remote = azure.EDCTerminals.Count();
            if (local != remote)
            {
                var lists = azure.EDCTerminals.ToList();
                int recordAdded = 0;
                foreach (var item in lists)
                {
                    if (!db.EDCTerminals.Any(c => c.EDCTerminalId == item.EDCTerminalId))
                    {
                        db.EDCTerminals.AddAsync(item);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(EDCTerminal), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(EDCTerminal));
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> SyncDownEmployeesAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Employees.Count();
            var remote = azure.Employees.Count();
            if (local != remote)
            {
                var Employees = azure.EmployeeDetails.Include(c => c.Employee).ToList();

                int recordAdded = 0;
                foreach (var emp in Employees)
                {
                    if (!db.EmployeeDetails.Any(c => c.EmployeeId == emp.EmployeeId))
                    {
                        db.EmployeeDetails.AddAsync(emp);
                        recordAdded += 2;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(Employee), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(Employee));
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> SyncDownUsersAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Users.Count();
            var remote = azure.Users.Count();
            if (local != remote)
            {
                var remoteUsers = azure.Users.ToList();
                int recordAdded = 0;
                foreach (var user in remoteUsers)
                {
                    if (!db.Users.Any(c => c.UserName == user.UserName))
                    {
                        db.Users.AddAsync(user);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();

                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(User));
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> SyncDownBanksAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                int lCount = 0;
                int rCount = 0;
                var local = db.Banks.Count();
                var remote = azure.Banks.Count();

                if (local != remote)
                {
                    var remoteList = azure.Banks.ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.Banks.Any(c => c.BankId == att.BankId))
                        {
                            db.Banks.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;
                    if (count == recordAdded)
                    {
                        Preferences.Default.Set(nameof(Bank), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:ALL");

                        return false;
                    }
                    else
                    {
                        Preferences.Remove(nameof(Bank)); return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                //Toast//ake("Failed to sync bank List with error: " + e.Message, );
            }
        }

        public async Task<bool> SyncDownTranscationsAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.TranscationModes.Count();
            var remote = azure.TranscationModes.Count();
            if (local != remote)
            {
                var TranscationModes = azure.TranscationModes.ToList();
                int recordAdded = 0;
                foreach (var mode in TranscationModes)
                {
                    if (!db.TranscationModes.Any(c => c.TranscationId == mode.TranscationId))
                    {
                        db.TranscationModes.AddAsync(mode);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded)
                {
                    Preferences.Default.Set(nameof(TranscationModes), $"{DateTime.Today}#R:{remote}#{local + recordAdded}#L");
                    return true;
                }
                else
                {
                    Preferences.Remove(nameof(TranscationModes));
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> SyncDownSalesmanAsync()
        {
            if (db == null)
                db = new AppDBContext(DBType.Local);
            if (azure == null)
                azure = new AppDBContext(DBType.Azure);

            var local = db.Salesmen.Count();
            var remote = azure.Salesmen.Count();
            if (local != remote)
            {
                var sms = azure.Salesmen.ToList();
                int recordAdded = 0;
                foreach (var sm in sms)
                {
                    if (!db.Salesmen.Any(c => c.SalesmanId == sm.SalesmanId))
                    {
                        db.Salesmen.AddAsync(sm);
                        recordAdded++;
                    }
                }
                int count = await db.SaveChangesAsync();
                if (count == recordAdded) return true; else return false;
            }
            return true;
        }

        public async Task<bool> SyncDownAttendace(UserType role)
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                switch (role)
                {
                    case UserType.CA:
                    case UserType.Admin:
                    case UserType.Owner:
                    case UserType.StoreManager:
                    case UserType.Accountant:
                    case UserType.PowerUser:
                        int lCount = 0;
                        int rCount = 0;
                        var local = db.Attendances.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        var remote = azure.Attendances.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();

                        if (local != remote)
                        {
                            var remoteList = azure.Attendances.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.Attendances.Any(c => c.AttendanceId == att.AttendanceId))
                                {
                                    db.Attendances.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(Attendance), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(Attendance));
                        }

                        local = db.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        remote = azure.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.MonthlyAttendances.Any(c => c.MonthlyAttendanceId == att.MonthlyAttendanceId))
                                {
                                    db.MonthlyAttendances.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(MonthlyAttendance), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(MonthlyAttendance));
                        }

                        if (rCount != lCount) return true; else return false;

                    case UserType.Sales:
                    case UserType.Employees:
                        lCount = 0;
                        rCount = 0;
                        local = db.Attendances.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.Attendances.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();

                        if (local != remote)
                        {
                            var remoteList = azure.Attendances.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.Attendances.Any(c => c.AttendanceId == att.AttendanceId))
                                {
                                    db.Attendances.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }

                        local = db.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.MonthlyAttendances.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.MonthlyAttendances.Any(c => c.MonthlyAttendanceId == att.MonthlyAttendanceId))
                                {
                                    db.MonthlyAttendances.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }
                        if (rCount != lCount) return true; else return false;

                    case UserType.Guest:
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                // Toast.Make("Error: " + ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    //   Toast.Make("Error Inner: " + ex.InnerException.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                    System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                }
                return false;
            }
        }

        public async Task<bool> SyncDownBankAccountsAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                switch (CurrentSession.UserType)
                {
                    case UserType.Admin:

                    case UserType.PowerUser:

                    case UserType.Owner:

                    case UserType.StoreManager:

                    case UserType.CA:

                    case UserType.Accountant:

                        int lCount = 0;
                        int rCount = 0;
                        var local = db.BankAccounts.Count();
                        var remote = azure.BankAccounts.Count();

                        if (local != remote)
                        {
                            var remoteList = azure.BankAccounts.ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.BankAccounts.Any(c => c.AccountNumber == att.AccountNumber))
                                {
                                    db.BankAccounts.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                            {
                                Preferences.Default.Set(nameof(BankAccount), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");

                                return true;
                            }
                            else
                            {
                                Preferences.Remove(nameof(BankAccount));
                                return false;
                            }
                        }

                        break;

                    case UserType.Sales:
                    case UserType.Guest:
                    case UserType.Employees:
                    default:

                        break;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SyncDownPartiesAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                int lCount = 0;
                int rCount = 0;
                var local = db.Parties.Count();
                var remote = azure.Parties.Count();

                if (local != remote)
                {
                    var remoteList = azure.Parties.ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.Parties.Any(c => c.PartyId == att.PartyId))
                        {
                            db.Parties.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;
                    if (count == recordAdded)
                    {
                        Preferences.Default.Set(nameof(Party), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:ALL");
                        return true;
                    }
                    else
                    { Preferences.Remove(nameof(Party)); return false; }
                }
                return true;
            }
            catch (Exception e)
            {
                //Toast.Make("Failed to sync bank List with error: " + e.Message, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                System.Diagnostics.Debug.WriteLine("parties " + e.Message);
                return false;
            }
        }

        ///
        public async Task<bool> SyncSalary(UserType role)
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                switch (role)
                {
                    case UserType.CA:
                    case UserType.Admin:
                    case UserType.Owner:
                    case UserType.StoreManager:
                    case UserType.Accountant:
                    case UserType.PowerUser:
                        int lCount = 0;
                        int rCount = 0;

                        var local = db.Salaries.Count();
                        var remote = azure.Salaries.Count();
                        if (local != remote)
                        {
                            var remoteList = azure.Salaries.ToList();
                            int recordAdded = 0;
                            foreach (var sal in remoteList)
                            {
                                if (!db.Salaries.Any(c => c.SalaryId == sal.SalaryId))
                                {
                                    db.Salaries.AddAsync(sal);
                                    recordAdded++;
                                }
                            }
                            int count = 0;
                            if (recordAdded > 0)
                                count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(Salary), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(Salary));
                        }

                        local = db.PaySlips.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        remote = azure.PaySlips.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.PaySlips.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.PaySlips.Any(c => c.PaySlipId == att.PaySlipId))
                                {
                                    db.PaySlips.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(PaySlip), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(PaySlip));
                        }

                        if (rCount == lCount) return true; else return false;

                    case UserType.Sales:
                    case UserType.Employees:
                        lCount = 0;
                        rCount = 0;
                        local = db.Salaries.Where(c => c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.Salaries.Where(c => c.EmployeeId == CurrentSession.EmployeeId).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.Salaries.Where(c => c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;
                            foreach (var sal in remoteList)
                            {
                                if (!db.Salaries.Any(c => c.SalaryId == sal.SalaryId))
                                {
                                    db.Salaries.AddAsync(sal);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }

                        local = db.PaySlips.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.PaySlips.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.PaySlips.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.PaySlips.Any(c => c.PaySlipId == att.PaySlipId))
                                {
                                    db.PaySlips.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }
                        if (rCount != lCount) return true; else return false;

                    case UserType.Guest:
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                //Toast.Make("Error: " + ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                System.Diagnostics.Debug.WriteLine("Err " + e.Message);
                if (e.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("err " + e.InnerException.Message);
                    //Toast.Make("Error Inner: " + ex.InnerException.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                }
                return false;
            }
        }

       
        public async Task<bool> SyncSalaryPayment(UserType role)
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                switch (role)
                {
                    case UserType.CA:
                    case UserType.Admin:
                    case UserType.Owner:
                    case UserType.StoreManager:
                    case UserType.Accountant:
                    case UserType.PowerUser:
                        int lCount = 0;
                        int rCount = 0;
                        var local = db.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        var remote = azure.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.SalaryPayment.Any(c => c.SalaryPaymentId == att.SalaryPaymentId))
                                {
                                    db.SalaryPayment.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(SalaryPayment), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(SalaryPayment));
                        }

                        local = db.StaffAdvanceReceipt.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        remote = azure.StaffAdvanceReceipt.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.StaffAdvanceReceipt.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.StaffAdvanceReceipt.Any(c => c.StaffAdvanceReceiptId == att.StaffAdvanceReceiptId))
                                {
                                    db.StaffAdvanceReceipt.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(StaffAdvanceReceipt), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(StaffAdvanceReceipt));
                        }
                        local = db.SalaryLedgers.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        remote = azure.SalaryLedgers.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.SalaryLedgers.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                            int recordAdded = 0;

                            foreach (var att in remoteList)
                            {
                                if (!db.SalaryLedgers.Any(c => c.EmployeeId == att.EmployeeId))
                                {
                                    att.Id = 0;
                                    db.SalaryLedgers.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(SalaryLedger), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(SalaryLedger));
                        }
                        if (rCount != lCount) return true; else return false;

                    case UserType.Sales:
                    case UserType.Employees:
                        lCount = 0;
                        rCount = 0;

                        local = db.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.SalaryPayment.Any(c => c.SalaryPaymentId == att.SalaryPaymentId))
                                {
                                    db.SalaryPayment.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }

                        local = db.StaffAdvanceReceipt.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.StaffAdvanceReceipt.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.StaffAdvanceReceipt.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;
                            foreach (var att in remoteList)
                            {
                                if (!db.StaffAdvanceReceipt.Any(c => c.StaffAdvanceReceiptId == att.StaffAdvanceReceiptId))
                                {
                                    db.StaffAdvanceReceipt.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }
                        local = db.SalaryLedgers.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        remote = azure.SalaryLedgers.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).Count();
                        if (local != remote)
                        {
                            var remoteList = azure.SalaryLedgers.Where(c => c.OnDate.Year == DateTime.Today.Year && c.EmployeeId == CurrentSession.EmployeeId).ToList();
                            int recordAdded = 0;

                            foreach (var att in remoteList)
                            {
                                if (!db.SalaryLedgers.Any(c => c.EmployeeId == att.EmployeeId))
                                {
                                    att.Id = 0;
                                    db.SalaryLedgers.AddAsync(att);
                                    recordAdded++;
                                }
                            }
                            int count = await db.SaveChangesAsync();
                            rCount += recordAdded;
                            lCount += recordAdded;
                            if (count == recordAdded)
                                Preferences.Default.Set(nameof(User), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{role}");
                            else
                                Preferences.Remove(nameof(User));
                        }
                        if (rCount != lCount) return true; else return false;

                    case UserType.Guest:
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                //Toast.Make("Error: " + ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                System.Diagnostics.Debug.WriteLine("err " + e.Message);
                if (e.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("err " + e.InnerException.Message);
                    //Toast.Make("Error Inner: " + ex.InnerException.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                }
                return false;
            }
        }

        public async Task<bool> SyncVoucherAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                int lCount = 0;
                int rCount = 0;
                var local = db.Vouchers.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                var remote = azure.Vouchers.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.Vouchers.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.Vouchers.Any(c => c.VoucherNumber == att.VoucherNumber))
                        {
                            db.Vouchers.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;
                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(Voucher), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(Voucher));
                }
                // lCount = 0;
                // rCount = 0;
                local = db.CashVouchers.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                remote = azure.CashVouchers.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.CashVouchers.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.CashVouchers.Any(c => c.VoucherNumber == att.VoucherNumber))
                        {
                            db.CashVouchers.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;

                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(CashVoucher), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(CashVoucher));
                }
                if (lCount == rCount) return true; else return true;

            }
            catch (Exception e)
            {

                return false;
            }
        }
        public async Task<bool> SyncCashAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                int lCount = 0;
                int rCount = 0;
                var local = db.PettyCashSheets.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                var remote = azure.PettyCashSheets.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.PettyCashSheets.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.PettyCashSheets.Any(c => c.Id == att.Id))
                        {
                            db.PettyCashSheets.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;
                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(PettyCashSheet), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(PettyCashSheet));
                }
                // lCount = 0;
                // rCount = 0;
                local = db.CashDetails.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                remote = azure.CashDetails.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.CashDetails.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.CashDetails.Any(c => c.CashDetailId == att.CashDetailId))
                        {
                            db.CashDetails.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;

                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(CashDetail), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(CashDetail));
                }
                if (lCount == rCount) return true; else return true;

            }
            catch (Exception e)
            {

                return false;
            }
        }
        public async Task<bool> SyncDuesAsync()
        {
            try
            {
                if (db == null)
                    db = new AppDBContext(DBType.Local);
                if (azure == null)
                    azure = new AppDBContext(DBType.Azure);

                int lCount = 0;
                int rCount = 0;
                var local = db.CustomerDues.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                var remote = azure.CustomerDues.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.CustomerDues.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.CustomerDues.Any(c => c.InvoiceNumber == att.InvoiceNumber))
                        {
                            db.CustomerDues.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;
                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(CustomerDue), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(CustomerDue));
                }
                // lCount = 0;
                // rCount = 0;
                local = db.DueRecovery.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                remote = azure.DueRecovery.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.DueRecovery.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.DueRecovery.Any(c => c.Id == att.Id))
                        {
                            db.DueRecovery.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;

                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(DueRecovery), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(DueRecovery));
                }
                // lCount = 0;
                // rCount = 0;
                local = db.DailySales.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                remote = azure.DailySales.Where(c => c.OnDate.Year == DateTime.Today.Year).Count();
                if (local < remote)
                {
                    var remoteList = azure.DailySales.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
                    int recordAdded = 0;
                    foreach (var att in remoteList)
                    {
                        if (!db.DailySales.Any(c => c.InvoiceNumber == att.InvoiceNumber))
                        {
                            db.DailySales.AddAsync(att);
                            recordAdded++;
                        }
                    }
                    try
                    {

                    
                    int count = await db.SaveChangesAsync();
                    rCount += recordAdded;
                    lCount += recordAdded;

                    if (count == recordAdded)
                        Preferences.Default.Set(nameof(DailySale), $"{DateTime.Today}#R:{remote}#L:{local + recordAdded}#U:{CurrentSession.UserType}");
                    else
                        Preferences.Remove(nameof(DailySale));
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                        return false;
                    }
                }

                if (lCount == rCount) return true; else return true;

            }
            catch (Exception e)
            {

                return false;
            }
        }

    }
}