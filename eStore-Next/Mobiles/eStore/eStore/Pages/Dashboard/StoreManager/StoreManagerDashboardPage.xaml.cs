namespace eStore.Pages.Dashboard.StoreManager;

public partial class StoreManagerDashboardPage : ContentPage
{
	public StoreManagerDashboardPage()
	{
		InitializeComponent();
		vouchers.ItemData = viewModel.VoucherList;
		cashVouchers.ItemData = viewModel.CashVoucherList;
	}
}