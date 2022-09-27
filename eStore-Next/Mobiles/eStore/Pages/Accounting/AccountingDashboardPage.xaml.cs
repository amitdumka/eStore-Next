using eStore.ViewModels;
using eStore.ViewModels.List.Dashboard;

namespace eStore.Pages.Accounting;

public partial class AccountingDashboardPage : ContentPage
{
	public AccountingDashboardViewModel vm;
    public AccountingDashboardPage(AccountingDashboardViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = this.vm=vm;
	}
}