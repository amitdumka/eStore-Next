namespace eStore.Pages.Payrol;
using eStore.ViewModels.List.Payroll;
public partial class EmployeePage : ContentPage
{
    EmployeeViewModel viewModel;

	public EmployeePage( EmployeeViewModel vm)
	{
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}
