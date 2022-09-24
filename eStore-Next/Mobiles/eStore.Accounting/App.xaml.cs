using eStore_MauiLib.Pages.Auth;
using eStore_MauiLib.RemoteService;
using eStore_MauiLib.Services.BackgroundServices;
using System.Diagnostics;
using AKS.Shared.Commons.Ops;
namespace eStore.Accounting
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitialDatabase();
            MainPage = new LoginPage(new AppShell());
        }
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"///settings");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"err: {ex.Message}");
            }
        }

        async void InitialDatabase()
        {
            new SyncService().SyncDownEmployeesAsync();
            if (!DatabaseStatus.VerifyLocalStatus())
            {
                BackgroundService service = new SyncDownService();
                service.InitService();
                service.GetInstance.RunWorkerAsync(LocalSync.Initial);
            }
        }
    }
}