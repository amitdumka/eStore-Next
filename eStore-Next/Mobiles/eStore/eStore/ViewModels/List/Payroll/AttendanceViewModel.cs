using System;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using eStore.MAUILib.DataModels.Payroll;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Payroll
{
    public class AttendanceViewModel : BaseViewModel<Attendance, AttendanceDataModel>
    {
        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.UserCheck;
            DataModel = new AttendanceDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<Attendance>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Attendance(s)";
            DataModel.Connect();
            DefaultSortedColName = nameof(Attendance.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }
        protected async Task FetchAsync()
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.StoreManager:
                case UserType.Accountant:
                case UserType.CA:
                case UserType.PowerUser:
                    var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }

        protected override void RefreshButton()
        {
            Entities.Clear();
            Notify.NotifyShort("Refresh Attendances....");
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.EmployeeId), MappingName = nameof(Attendance.EmployeeId) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.OnDate), MappingName = nameof(Attendance.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.Status), MappingName = nameof(Attendance.Status) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.EntryTime), MappingName = nameof(Attendance.EntryTime) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.Remarks), MappingName = nameof(Attendance.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.StoreId), MappingName = nameof(Attendance.StoreId) });
           
            return gridColumns;
        }
    }

}

