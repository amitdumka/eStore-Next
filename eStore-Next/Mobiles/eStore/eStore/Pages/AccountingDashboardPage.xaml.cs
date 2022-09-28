using eStore.ViewModels.List.Dashboard;

namespace eStore.Pages;

public partial class AccountingDashboardPage : ContentPage
{
    public AccountingDashboardViewModel vm;

    public AccountingDashboardPage(AccountingDashboardViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = this.vm = vm;
    }
}