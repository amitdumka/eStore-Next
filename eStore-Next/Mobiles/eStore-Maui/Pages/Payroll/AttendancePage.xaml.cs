using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Payroll.Models;
using eStore_MauiLib.ViewModels.Auth;
using eStore_MauiLib.ViewModels.Payroll;
using Syncfusion.Maui.DataGrid;

namespace eStore_Maui.Pages.Payroll;

public partial class AttendancePage : ContentPage
{
    public AttendanceViewModel viewModel;
    public static ColumnCollection gridColumns;

    public AttendancePage()
	{
		InitializeComponent();
        viewModel = new AttendanceViewModel();
        BindingContext = viewModel;
        RLV.BindingContext = viewModel;
        viewModel.Icon = eStore_Maui.Resources.Styles.IconFont.UserFriends;
        RLV.Cols = SetGridCols();
    }
    protected ColumnCollection SetGridCols()
    {
        gridColumns = new();
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.OnDate), MappingName = nameof(Attendance.OnDate) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.EmployeeId), MappingName = nameof(Attendance.EmployeeId) });  
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.Status), MappingName = nameof(Attendance.Status) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.EntryTime), MappingName = nameof(Attendance.EntryTime) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.StoreId), MappingName = nameof(Attendance.StoreId) });
        gridColumns.Add(new DataGridCheckBoxColumn() { HeaderText = nameof(Attendance.Remarks), MappingName = nameof(Attendance.Remarks) });
        return gridColumns;
    }
}
