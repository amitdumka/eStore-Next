using AKS.Shared.Commons.Ops;
using eStore.DatabaseSyncService.Services;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.Pages.Auth;
using eStore.Pages.Accounting.Entry;
using eStore.Pages.Accounting.Entry.Banking;
using eStore.Pages.Inventory.Entry;

namespace eStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            try
            {
                InitializeComponent();
                RegisterRoutes();
            }
            catch(Android.Views.InflateException ex)
            {
                Notify.NotifyLong("Error:" + ex.Message);
            } 
            catch (Exception ex)
            {
                Notify.NotifyLong("Error:" + ex.Message);
            }
            
        }
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("voucher/Entry", typeof(VoucherEntryPage));
            Routing.RegisterRoute("cashvoucher/Entry", typeof(CashVoucherEntryPage));
            Routing.RegisterRoute("banking/bank/Entry", typeof(BankEntryPage));
            Routing.RegisterRoute("sale/Entry", typeof(SaleEntryPage));
            //Routing.RegisterRoute("banking/banktranscations/Entry", typeof(BankEntryPage));
        }
        async void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await DisplayAlert("Logout", "Do you want to Logout!", "Yes", "No");
            if (result)
            {
                CurrentSession.Clear();
                App.Current.MainPage = new LoginPage(new AppShell());
            }
        }

        private void SyncDown_Clicked(object sender, EventArgs e)
        {
            BackgroundService service = new SyncDownService();
            service.InitService();
            service.GetInstance.RunWorkerAsync(LocalSync.All);
        }
    }
}