using eStore.MAUILib.DataModels;
using eStore.ViewModels.List.Dashboard;

namespace eStore.Views.Dashboard;

public partial class DashboardAdminView : ContentView
{
	public DashboardAdminView()
	{
		InitializeComponent();
		vouchers.BindingContext = this.BindingContext; 
		cashVouchers.BindingContext = this.BindingContext;

		vouchers.ItemData = ((AccountingDashboardViewModel)this.BindingContext).VoucherList;
		cashVouchers.ItemData= ((AccountingDashboardViewModel)this.BindingContext).CashVoucherList;
    }
}
