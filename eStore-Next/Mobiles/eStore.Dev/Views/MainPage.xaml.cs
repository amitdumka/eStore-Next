using DevExpress.Maui.CollectionView;
using eStore.Dev.ViewModels;

namespace eStore.Dev.Views
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }

        void OnCloseClicked(object sender, EventArgs e)
        {
            Current.FlyoutIsPresented = false;
        }
    }
}