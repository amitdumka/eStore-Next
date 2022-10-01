using AKS.Shared.Payroll.Models;
using eStore.ViewModels.Entry.Payroll;
using eStore.ViewModels.List.Payroll;

namespace eStore.Pages.Payroll;

public partial class AttendanceEntryPage : ContentPage
{
    private AttendanceEntryViewModel entryViewModel;

    public AttendanceEntryPage(AttendanceViewModel vm, Attendance att)
	{
		InitializeComponent();
        entryViewModel = new AttendanceEntryViewModel(vm, att);
        this.BindingContext = entryViewModel;
        dataForm.DataObject = entryViewModel.Attendance;
        dataForm.PickerSourceProvider = entryViewModel;
        entryViewModel.Dfv = dataForm;
    }
}