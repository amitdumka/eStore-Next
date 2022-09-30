using eStore.ViewModels.List.Payroll;

namespace eStore.Pages.Payrol;

public partial class AttendancePage : ContentPage
{
    AttendanceViewModel viewModel;
	public AttendancePage(AttendanceViewModel vm)
	{
		InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}
