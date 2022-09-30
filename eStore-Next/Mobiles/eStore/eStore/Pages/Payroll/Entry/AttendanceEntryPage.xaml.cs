using CommunityToolkit.Maui.Views;
using eStore.ViewModels.Entry.Payroll;

namespace eStore.Pages.Payroll.Entry;

public partial class AttendanceEntryPage : Popup
{
    private AttendanceEntryViewModel entryViewModel;

    public AttendanceEntryPage(AttendanceEntryViewModel vm, bool isNew)
    {
        InitializeComponent();
        entryViewModel = vm;
        this.BindingContext = entryViewModel;
        Size = new Microsoft.Maui.Graphics.Size(400, 500);
    }
}