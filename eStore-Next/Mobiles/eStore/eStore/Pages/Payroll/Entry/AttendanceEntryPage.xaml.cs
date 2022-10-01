using CommunityToolkit.Maui.Views;
using eStore.ViewModels.Entry.Payroll;

namespace eStore.Pages.Payroll.Entry;

public partial class AttendanceEntryPage : Popup
{
    private AttendanceEntryViewModel entryViewModel;

    public AttendanceEntryPage(AttendanceViewModel vm, Attendance att)
    {
        InitializeComponent();
        entryViewModel = new AttendanceEntryViewModel(vm,att);
        this.BindingContext = entryViewModel;
        Size = new Microsoft.Maui.Graphics.Size(400, 500);

        //BindingContext = viewModel;
        dataForm.DataObject = entryViewModel.VoucherEntry;
        dataForm.PickerSourceProvider = entryViewModel;
        entryViewModel.Dfv = dataForm;
    }
}