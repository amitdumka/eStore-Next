﻿using AKS.Shared.Commons.Ops;
using eStore.DatabaseSyncService.Services;
using eStore.MAUILib.Pages.Auth;

namespace eStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
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