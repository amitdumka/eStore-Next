using System.Windows.Input;

namespace eStore.Dev.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public const string ViewName = "AboutPage";
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.devexpress.com/xamarin/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}