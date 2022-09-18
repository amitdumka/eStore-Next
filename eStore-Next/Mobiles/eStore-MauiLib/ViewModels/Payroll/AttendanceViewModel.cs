using System;
using AKS.Shared.Payroll.Models;


namespace eStore_MauiLib.ViewModels.Payroll
{
    /// <summary>
    /// TODO: Implement Role based data listing and manuplication .
    /// Employee can't be able to Add it own attendance.
    /// Employee can temp fetch Power user permission to use in event of Accoount and Store Manager is  Present
    /// This View Model strictly shoud follow role based only.
    /// // Current Month Attendance of Employee in case of SM and power user other wise employee attendance full .
    /// </summary>
    public class AttendanceViewModel : BaseViewModel<Attendance, AttendanceViewModel>
    {
        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Attendance>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<Attendance> Get(string id)
        {
            throw new NotImplementedException();
        }

        protected override Task<Attendance> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Attendance>> GetList()
        {
            throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Save(bool isNew = false)
        {
            throw new NotImplementedException();
        }
    }
}

