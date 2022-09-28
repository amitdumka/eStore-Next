using eStore.MAUILib.Pages.Auth;

namespace eStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage(new AppShell());
        }
    }
}