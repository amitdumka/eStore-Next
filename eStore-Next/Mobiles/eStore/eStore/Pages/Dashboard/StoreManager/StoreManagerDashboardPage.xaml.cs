namespace eStore.Pages.Dashboard.StoreManager;

public partial class StoreManagerDashboardPage : ContentPage
{
	public StoreManagerDashboardPage()
	{
		InitializeComponent();
    }

	protected override void OnAppearing()
	{
		viewModel.OnAppearing();
       // vouchers.ItemData = viewModel.VoucherList;
       // cashVouchers.ItemData = viewModel.CashVoucherList;
    }



}