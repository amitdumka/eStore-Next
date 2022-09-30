using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using eStore.MAUILib.DataModels.Payroll;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Payroll
{
    public class EmployeeViewModel : BaseViewModel<Employee, AttendanceDataModel>
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
            Entities = new System.Collections.ObjectModel.ObservableCollection<Employee>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Employee(s)";
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
                    var data = await DataModel.GetYItems(CurrentSession.StoreCode);
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
            Notify.NotifyShort("Refresh Employees....");
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.EmployeeId), MappingName = nameof(Employee.EmployeeId) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.StaffName), MappingName = nameof(Employee.StaffName) } );
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.Category), MappingName = nameof(Employee.Category) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.IsWorking), MappingName = nameof(Employee.IsWorking) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.StoreId), MappingName = nameof(Employee.StoreId) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.IsTailors), MappingName = nameof(Employee.IsTailors) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Employee.JoiningDate), MappingName = nameof(Employee.JoiningDate), Format = "dd/MMM/yyyy" });
            return gridColumns;
        }
    }

}

