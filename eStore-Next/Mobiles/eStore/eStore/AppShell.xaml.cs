using AKS.Shared.Commons.Ops;
using eStore.DatabaseSyncService.Services;
using eStore.MAUILib.Pages.Auth;
using eStore.Pages.Accounting.Entry;

namespace eStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("voucher/Entry", typeof(VoucherEntryPage));
            Routing.RegisterRoute("cashvoucher/Entry", typeof(CashVoucherEntryPage));
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