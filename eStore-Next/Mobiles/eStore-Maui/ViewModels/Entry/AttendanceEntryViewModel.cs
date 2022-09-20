using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore_MauiLib.DataModels;
using eStore_MauiLib.DataModels.Payroll;

namespace eStore_Maui.ViewModels.Entry
{
    public partial class AttendanceEntryViewModel : BaseEntryViewModel<Attendance, AttendanceDataModel>
    {
        [ObservableProperty]
        private List<DynVM> _employeeList;

        [ObservableProperty]
        private string _id;

        [ObservableProperty]
        private string _employeeId;

        [ObservableProperty]
        private DateTime _onDate;

        [ObservableProperty]
        private string _remarks;

        [ObservableProperty]
        private string _entryTime;

        [ObservableProperty]
        private AttUnit _status;

        [ObservableProperty]
        private bool _isTailor;
        [ObservableProperty]
        private List<string> _attUnitList;

        public AttendanceEntryViewModel(AttendanceDataModel dm, Attendance toEdit)
        {
            DataModel = dm;
            Title = "Add Attendance";
            if (toEdit != null)
            {
                IsNew = false;
                IsTailor = toEdit.IsTailoring;
                Status = toEdit.Status;
                EntryTime = toEdit.EntryTime;
                EmployeeId = toEdit.EmployeeId;
                Remarks = toEdit.Remarks;
                Title = "Update Attendance";
                OnDate = toEdit.OnDate;
                Id = toEdit.AttendanceId;
            }
            else {
                IsNew = true;
                OnDate = DateTime.Now; 
                EntryTime=DateTime.Now.ToShortTimeString();

            }
            InitViewModel();
        }

        protected override void Cancle()
        {
            ResetEntryViewModel();
            //Need to Close pop
        }

        protected override void InitViewModel()
        {
            EmployeeList = CommonDataModel.GetEmployeeList(DataModel.GetContextLocal());
            AttUnitList = Enum.GetNames(typeof(AttUnit)).ToList();
        }

        protected override void Save()
        {
            Attendance attendance;
            if (IsNew)
            {
                attendance = new Attendance
                {
                    EmployeeId = EmployeeId,
                    EntryStatus = EntryStatus.Added,
                    StoreId = CurrentSession.StoreCode,
                    EntryTime = EntryTime,
                    IsReadOnly = false,
                    IsTailoring = IsTailor,
                    MarkedDeleted = false,
                    OnDate = OnDate,
                    Remarks = Remarks,
                    Status = Status,
                    UserId = CurrentSession.UserName,
                    AttendanceId = $"{EmployeeId}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Second}"
                };
            }
            else
            {
                attendance = new Attendance
                {
                    EmployeeId = EmployeeId,
                    EntryStatus = EntryStatus.Updated,
                    StoreId = CurrentSession.StoreCode,
                    EntryTime = EntryTime,
                    IsReadOnly = false,
                    IsTailoring = IsTailor,
                    MarkedDeleted = false,
                    OnDate = OnDate,
                    Remarks = Remarks,
                    Status = Status,
                    UserId = CurrentSession.UserName,
                    AttendanceId = Id
                };
            }

            if (DataModel.Save(attendance, IsNew) != null)
            {
                if (IsNew)
                    Toast.Make("Attendace is saved!", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                else
                    Toast.Make("Attendace is updated!", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                ResetEntryViewModel();
            }
        }

        private void ResetEntryViewModel()
        {
            _employeeId = null;
            _id = null;
            _onDate = DateTime.Today;
            _entryTime = null;
            _remarks = null;
            _status = AttUnit.StoreClosed;
            _isNew = false;
        }
    }
}