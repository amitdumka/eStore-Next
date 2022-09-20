using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace eStore_Maui.ViewModels
{

	[INotifyPropertyChanged]
	public partial class AboutViewModel
	{
		[ObservableProperty]
		private string _title;

        public AboutViewModel()
		{
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.google.com"));

        }

        public ICommand OpenWebCommand { get; }
    }
}

