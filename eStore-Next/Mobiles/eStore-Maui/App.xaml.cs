using System.Diagnostics;
using AKS.Shared.Commons.Ops;
using eStore_Maui.Pages;
using eStore_MauiLib.RemoteService;
using eStore_MauiLib.Services.BackgroundServices;

namespace eStore_Maui
{
    public partial class App : Application
    {
        public App()
        {
           ;
            InitializeComponent();
            //App.Current.UserAppTheme = AppTheme.Dark;

            //if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            //    Shell.Current.CurrentItem = PhoneTabs;
            //InitialDatabase();
            MainPage = new AppShell();
            //Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
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
            if (!DatabaseStatus.VerifyLocalStatus())
            {
                BackgroundService service= new SyncDownService();
                service.InitService();
                service.GetInstance.RunWorkerAsync(LocalSync.Initial);
            }
        }
    }

    
}