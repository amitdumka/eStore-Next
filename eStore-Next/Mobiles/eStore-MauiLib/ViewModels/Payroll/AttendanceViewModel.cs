using System;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using CommunityToolkit.Maui.Alerts;
using eStore_MauiLib.DataModels.Payroll;

namespace eStore_MauiLib.ViewModels.Payroll
{
    /// <summary>
    /// TODO: Implement Role based data listing and manuplication .
    /// Employee can't be able to Add it own attendance.
    /// Employee can temp fetch Power user permission to use in event of Accoount and Store Manager is  Present
    /// This View Model strictly shoud follow role based only.
    /// // Current Month Attendance of Employee in case of SM and power user other wise employee attendance full .
    /// </summary>
    public class AttendanceViewModel : BaseViewModel<Attendance, AttendanceDataModel>
    {

        public AttendanceViewModel()
        {
            DataModel = new AttendanceDataModel(ConType.Hybrid);
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            DataModel.Connect();
            DataModel.Mode = DBType.Local;
            Title = "Attendance List";
            DefaultSortedColName = nameof(Entity.OnDate);
            InitViewModel();

        }

        protected override async void AddButton()
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.Accountant:
                case UserType.PowerUser:
                case UserType.StoreManager:break;
                case UserType.Sales:
                case UserType.CA:
                case UserType.Guest:
                case UserType.Employees:
                default:
                    Toast.Make("You are not authozie! Access Deninde", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    break;
            }
        }

        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.Accountant:
                case UserType.PowerUser:
                case UserType.StoreManager: break;
                case UserType.Sales:
                case UserType.CA:
                case UserType.Guest:
                case UserType.Employees:
                default:
                    Toast.Make("You are not authozie! Access Deninde", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    break;
            }
        }

        protected override Task<List<Attendance>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<Attendance> Get(string id)
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.Accountant:
                case UserType.PowerUser:
                case UserType.CA:
                case UserType.StoreManager:
                    return DataModel.GetById(id);
                    
                case UserType.Sales:
                case UserType.Employees:
                    if (id == CurrentSession.EmployeeId)
                    {
                       return DataModel.GetById(id);
                    }
                    else
                    {
                        Toast.Make("You are not authozie! Access Denide", CommunityToolkit.Maui.Core.ToastDuration.Long);
                        return null;
                    }
                    
                case UserType.Guest:
                default:
                    Toast.Make("You are not authozie! Access Denide", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    return null;
                    break;
            }
        }

        protected override Task<Attendance> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override async Task<List<Attendance>> GetList()
        {
            List<Attendance> atts;
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.CA:
                case UserType.Accountant:
                    atts = await DataModel.GetItems();

                    break;
                case UserType.StoreManager:
                case UserType.PowerUser:
                    atts = await DataModel.GetItems();
                    break;
                case UserType.Guest:
                    atts = null;
                    break;

                case UserType.Sales:
                case UserType.Employees:
                    atts = await DataModel.GetItems();
                    break;

                default:
                    atts = await DataModel.GetItems();
                    break;
            }
            return atts;
        }

        protected override void RefreshButton()
        {
            Entities.Clear();
            InitViewModel();
        }

        protected override async Task<bool> Save(bool isNew = false)
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.Accountant:
                case UserType.PowerUser:
                case UserType.StoreManager:

                   Entity=await DataModel.Save(Entity);
                    if (Entity != null) return true;
                    break;
                case UserType.Sales:
                case UserType.CA:
                case UserType.Guest:
                case UserType.Employees:
                default:
                    Toast.Make("You are not authozie! Access Deninde", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    break;
            }
            return false;
        }

        protected void UpdateEntities(List<Attendance> atts)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<Attendance>();
            foreach (var item in atts)
            {
                Entities.Add(item);
            }
            RecordCount = Entities.Count;
        }
        protected async void InitViewModel()
        {
            List<Attendance> atts;
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.CA:
                case UserType.Accountant:
                    atts= await DataModel.GetItems();
                    
                    break;
                case UserType.StoreManager:
                case UserType.PowerUser:
                    atts = await DataModel.GetItems();
                    break;
                case UserType.Guest:
                    atts = null;
                    break;

                case UserType.Sales:
                case UserType.Employees:
                    atts = await DataModel.GetItems();
                    break;
               
                default:
                    atts = await DataModel.GetItems();
                    break;
            }

            if (atts != null)
            {
                UpdateEntities(atts);
            }
        }
    }
}

