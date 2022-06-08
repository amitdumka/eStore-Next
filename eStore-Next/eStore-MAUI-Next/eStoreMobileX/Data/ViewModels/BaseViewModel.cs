using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eStoreMobileX.Data.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Command<object> backButtonCommand;
        private Command<object> saveButtonCommand;
        protected ConType conType;
        protected DbType mode;

        private string title;

        public DbType Mode
        {
            get { return mode; }
            set { mode = value; NotifyPropertyChanged("Mode"); }
        }

        public ConType ConType
        {
            get { return conType; }
            set
            {
                conType = value; //OnPropertyChanged();
                NotifyPropertyChanged("ConType");
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                //OnPropertyChanged();
                NotifyPropertyChanged("Title");
            }
        }

        #endregion
        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Commands

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> BackButtonCommand
        {
            get
            {
                return this.backButtonCommand ?? (this.backButtonCommand = new Command<object>(this.BackButtonClicked));
            }
        }
        public Command<object> SaveButtonCommand
        {
            get
            {
                return this.saveButtonCommand ?? (this.saveButtonCommand = new Command<object>(this.SaveButtonClicked));
            }
        }

        #endregion
        #region Methods
        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }
        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {
            if (Device.RuntimePlatform == Device.UWP && Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else if (Device.RuntimePlatform != Device.UWP && Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
            {
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        /// <summary>
        /// Invoked when an Save button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SaveButtonClicked(object obj)
        {
            //TODO: Save function which is overriden in derived class.

            //if (Device.RuntimePlatform == Device.UWP && Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            //{
            //    Application.Current.MainPage.Navigation.PopAsync();
            //}
            //else if (Device.RuntimePlatform != Device.UWP && Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
            //{
            //    Application.Current.MainPage.Navigation.PopAsync();
            //}
        }

        public async void DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        #endregion

        #region AbstractFunctions
        // public abstract async void LoadData();
        public abstract  void InitObject();
        protected abstract void RefreshDataAsync();
        public abstract void ItemsSourceRefresh();
        #endregion

        #region Constructor
        public BaseViewModel()
        {

        }

        #endregion

    }

}
