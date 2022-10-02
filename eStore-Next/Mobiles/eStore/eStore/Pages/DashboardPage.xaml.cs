using eStore.ViewModels.List.Dashboard;

namespace eStore.Pages;

public partial class DashboardPage : ContentPage
{
    public AccountingDashboardViewModel viewModel;
    public DashboardPage(AccountingDashboardViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = this.viewModel = vm;
    }
}