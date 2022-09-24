using AKS.Shared.Commons.Ops;
using eStore_MauiLib.Pages.Auth;
using eStore.Accounting;
using System.Windows.Input;
using AKS.Shared.Commons.Ops;
using eStore_MauiLib.Pages.Auth;
using System.Windows.Input;

namespace eStore.Accounting
{
    public partial class AppShell : Shell
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
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
    }
}



