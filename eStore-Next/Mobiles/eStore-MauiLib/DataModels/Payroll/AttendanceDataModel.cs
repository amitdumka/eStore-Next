using System;
using AKS.MAUI.Databases;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;

namespace eStore_MauiLib.DataModels.Payroll
{

    //TODO: It should be different for different role.
    //either do at ViewModel level or Data level .
    //Access restriction or viewmodel shoud be changed.
    public class AttendanceDataModel : BaseDataModel<Attendance>
    {
        public AttendanceDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<Attendance>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Attendance>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }
        public override async Task<List<Attendance>> GetItems(string storeid)
        {
            AppDBContext currDb = null;
            //This Approch is not very usefull in event of API or remoteAPI
            switch (Mode)
            {
                case DBType.Local: currDb = _localDb; break;
                case DBType.Azure: currDb = _azureDb; break;
                default:
                    break;
            }

            if (CurrentSession.UserType == UserType.Employees || CurrentSession.UserType == UserType.Sales)
            {
                return await currDb.Attendances.Where(c => c.EmployeeId == CurrentSession.EmployeeId)
                    .OrderByDescending(c => c.OnDate)
                    .ToListAsync();

            }
            else if (CurrentSession.UserType == UserType.StoreManager)
            {

                return await currDb.Attendances.Where(c => c.StoreId == storeid)
                    .OrderByDescending(c => c.OnDate)
                    .ToListAsync();

            }
            else if (CurrentSession.UserType != UserType.Guest)
            {
                // Admin
                return await currDb.Attendances.Where(c=>c.StoreId==storeid)
                    .OrderByDescending(c => c.OnDate)
                    .ToListAsync();


            }
            else return null;
        }
        public override async Task<List<Attendance>> GetItems()
        {
            AppDBContext currDb = null;
            //This Approch is not very usefull in event of API or remoteAPI
            switch (Mode)
            {
                case DBType.Local: currDb = _localDb; break;
                case DBType.Azure: currDb = _azureDb; break;
                default:
                    break;
            }

            if (CurrentSession.UserType == UserType.Employees || CurrentSession.UserType == UserType.Sales)
            {
                return await currDb.Attendances.Where(c => c.EmployeeId == CurrentSession.EmployeeId 
                && c.OnDate.Year==DateTime.Today.Year && c.OnDate.Month==DateTime.Today.Month )
                    .OrderByDescending(c => c.OnDate)
                    .ToListAsync();

            }
            else if (CurrentSession.UserType == UserType.StoreManager)
            {
                return await currDb.Attendances.Where(c => c.StoreId == CurrentSession.StoreCode
                && c.OnDate.Date == DateTime.Today)
                    .OrderByDescending(c => c.OnDate)
                    .ToListAsync();
            }
            else if (CurrentSession.UserType != UserType.Guest)
            {
                // Admin
                return await currDb.Attendances
                    .OrderByDescending(c => c.OnDate)
                    .ToListAsync();
            }
            else return null;
        }

        public override List<int> GetYearList()
        {
            switch (Mode)
            {
                case DBType.Local:
                    return _localDb.Attendances.Where(c => c.StoreId == StoreCode).Select(c => c.OnDate.Year).Distinct().ToList();
                case DBType.Azure:
                    return _azureDb.Attendances.Where(c => c.StoreId == StoreCode).Select(c => c.OnDate.Year).Distinct().ToList();
                default:
                    return _azureDb.Attendances.Where(c => c.StoreId == StoreCode).Select(c => c.OnDate.Year).Distinct().ToList();
            }
        }

        public override Task<bool> InitContext()
        {
            // Datamodel setup in case requried
            return Task.FromResult(Connect());
        }
    }
}

