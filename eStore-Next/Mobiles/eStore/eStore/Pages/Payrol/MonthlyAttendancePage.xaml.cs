
using eStore.ViewModels.List.Payroll;

namespace eStore.Pages.Payrol;

public partial class MonthlyAttendancePage : ContentPage
{
    MonthlyAttendanceViewModel viewModel;

    public MonthlyAttendancePage(MonthlyAttendanceViewModel vm)
	{
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}
