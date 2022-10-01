using System.Collections;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
 
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore.MAUILib.DataModels;
 
using eStore.MAUILib.DataModels.Payroll;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
 
using eStore.ViewModels.List.Payroll;

//TODO: Need to stick one Construstor for better perpose.
namespace eStore.ViewModels.Entry.Payroll
{
    public partial class AttendanceEntryViewModel : BaseEntryViewModel<Attendance, AttendanceDataModel>, IPickerSourceProvider
    {
        [ObservableProperty]
        private List<DynVM> _employeeList;

        [ObservableProperty]
        private DataFormView _dfv;

        //[ObservableProperty]
        //private bool _isNew=true;

        [ObservableProperty]
        private DynVM _selectedEmployee;

        [ObservableProperty]
        private int _selectedStatusIndex;

        [ObservableProperty]
        AttendanceViewModel _viewMoldel;
         
        [ObservableProperty]
        private AttendanceEntry _attendance;

        public AttendanceEntryViewModel(AttendanceDataModel dm, Attendance toEdit)
        {
            DataModel = dm;
            Title = "Add Attendance";
            if (toEdit != null)
            {
                IsNew = false;
             Attendance.   Tailor = toEdit.IsTailoring;
                Attendance.Status = toEdit.Status;
                Attendance.EntryTime = toEdit.EntryTime;
                Attendance.EmployeeId = toEdit.EmployeeId;
                Attendance.Remarks = toEdit.Remarks; 
                Attendance.OnDate = toEdit.OnDate;
                Attendance.AttendanceId = toEdit.AttendanceId;
            }
            else
            {
                IsNew = true;
                Attendance.OnDate = DateTime.Now;
                Attendance.EntryTime = DateTime.Now.ToShortTimeString();
            }
            InitViewModel();
        }
        public AttendanceEntryViewModel(AttendanceViewModel vm, Attendance toEdit)
        {
            _viewMoldel = vm;
            DataModel = vm.GetDataModel(); ;
            Title = "Add Attendance";

            if (toEdit != null)
            {
                IsNew = false;
               Attendance.Tailor = toEdit.IsTailoring;
                Attendance.Status = toEdit.Status;
                Attendance.EntryTime = toEdit.EntryTime;
                Attendance.EmployeeId = toEdit.EmployeeId;
                Attendance.Remarks = toEdit.Remarks;
              //  Attendance.Title = "Update Attendance";
                Attendance.OnDate = toEdit.OnDate;
                Attendance.AttendanceId = toEdit.AttendanceId;
            }
            else
            {
                IsNew = true;
                Attendance. OnDate = DateTime.Now;
                Attendance. EntryTime = DateTime.Now.ToShortTimeString();
            }
            InitViewModel();
        }


        public IEnumerable GetSource(string propertyName)
        {
            try
            {
                if (propertyName == "EmployeeId")
                {
                    if (EmployeeList == null)
                        EmployeeList = CommonDataModel.GetEmployeeList(DataModel.GetContext());
                    return EmployeeList;
                }
                if (propertyName == "Status")
                {
                    return Enum.GetNames(typeof(AttUnit)).ToList();
                }
                return null;
            }
            catch (NullReferenceException ex)
            {
                Notify.NotifyShort("Error" + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Notify.NotifyShort("Error" + ex.Message);
                return null;
            }
        }

        protected override void Cancle()
        {
            ResetView();
           
        }

        protected override void InitViewModel()
        {
            if (DataModel == null)
                DataModel = new AttendanceDataModel(ConType.Hybrid, CurrentSession.Role);
            DataModel.Connect();
        }

        private void ResetView()
        {
            Dfv.DataObject = Attendance = new AttendanceEntry { OnDate = DateTime.Now};
        }

        protected override async void Save()
        {


            try
            {
                var v = await DataModel.SaveAsync(
                    new Attendance
                    {
                        EmployeeId = Attendance.EmployeeId,
                        EntryStatus = EntryStatus.Added,
                        StoreId = CurrentSession.StoreCode,
                        EntryTime = Attendance.EntryTime,
                        IsReadOnly = false,
                        IsTailoring = Attendance.Tailor,
                        MarkedDeleted = false,
                        OnDate = Attendance.OnDate,
                        Remarks = Attendance.Remarks,
                        Status = Attendance.Status,
                        UserId = CurrentSession.UserName,
                        AttendanceId = IsNew ?
                    $"{Attendance.EmployeeId}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Second}" : Attendance.AttendanceId
                    },
                    IsNew);
                if (v != null)
                {
                    Notify.NotifyVLong($"Attendace Marked as :{v.Status}");
                    // LastAttendance = v.EmployeeId;

                    //Update view model on add/update.
                    if (this._viewMoldel != null)
                    {
                        if (!IsNew)
                            _viewMoldel.Entities
                                .Remove(_viewMoldel.Entities.FirstOrDefault(c => c.AttendanceId == v.AttendanceId));
                        _viewMoldel.Entities.Add(v);
                    }

                    DataModel.SyncUp(v, IsNew, false);
                    ResetView();
                }
                else
                {


                    Notify.NotifyVLong($"Error on marking attendance :{Attendance.EmployeeId}");
                }
            }
            catch (Exception ex)
            {
                Notify.NotifyVLong($"Error  :{ex.Message}");
               
            }

           
        }



    }

    public class AttendanceEntry
    {
        [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
        public string AttendanceId { get; set; }

        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 1)]
        [DataFormDisplayOptions(LabelText = "Staff")]
        public string EmployeeId { get; set; }
        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 2)]
        [DataFormDisplayOptions(LabelText = "Time")]
        public string EntryTime { get; set; }
        [DataFormItemPosition(RowOrder = 2, ItemOrderInRow = 1)]
        [DataFormDisplayOptions(LabelText = "Date")]
        public DateTime OnDate { get; set; }
        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 2)]
        [DataFormDisplayOptions(LabelText = "Remark")]
        public string Remarks { get; set; }
        [DataFormItemPosition(RowOrder = 2, ItemOrderInRow = 2)]
        [DataFormDisplayOptions(LabelText = "Status")]
        public AttUnit Status { get; set; }
        public bool Tailor { get; set; }

    }
}