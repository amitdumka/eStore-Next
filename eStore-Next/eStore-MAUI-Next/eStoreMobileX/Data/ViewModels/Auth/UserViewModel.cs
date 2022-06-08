using System.Collections.ObjectModel;
using AKS.Shared.Commons.Models.Auth;
using eStoreMobileX.Config;
using eStoreMobileX.Data.DataModels.Auth;

namespace eStoreMobileX.Data.ViewModels.Auth
{
    public class UserViewModel : BaseViewModel
    {
        private UsersDataModel dm;
        public ObservableCollection<User> ItemList { get; set; }
        public User Item { get; set; }

        public UserViewModel()
        {
            this.Item = new User
            {
                StoreId = AppConfig.StoreId,
            };

            InitObject();
        }

        public UserViewModel(string storeId)
        {

            this.Item = new User
            {
                StoreId = storeId,
            };

            InitObject();
        }
        protected override async void RefreshDataAsync()
        {
            try
            {
                dm = new UsersDataModel(ConType);
                dm.Mode = AppConfig.DbMode;

                List<User> Data = await dm.GetItems();

                if (Data == null || Data.Count <= 0)
                {
                    if (await dm.SyncWithServer())
                    {
                        Data = await dm.GetItems();
                    }
                    // await App.Current.MainPage.DisplayAlert("Info", "Loading Data from server...", "Ok");
                    // DisplayAlert("Info", "Loading Data from Server...", "Ok");
                    //TODO: Alert user  or download from server.
                }
                if (Data != null)
                {
                    ItemList.Clear();
                    foreach (var item in Data)
                    {
                        ItemList.Add(item);
                    }
                }

            }
            catch (Exception)
            {

                //TODO: Alert user about error.
                //await App.Current.MainPage.DisplayAlert("Exception", "Error: " + e.Message, "Ok");
            }
        }

        /// <summary>
        /// Refresh Data store.
        /// </summary>
        public override void ItemsSourceRefresh()
        {
            RefreshDataAsync();
        }

        public async Task<bool> SignUP(User User, bool isNew = true)
        {
            //Alert user 
            if (await dm.Save(User, isNew))
            {
                DisplayAlert("Alert", "New user is added!", "Ok");
                return true;
            }
            else
            {
                DisplayAlert("Error", "Not able to add, Kindly try again!", "Ok");
                return false;
            }
        }

        public void PasswordChange(string username, string old, string newpass)
        {
            if (dm.PasswordChange(username, old, newpass))
            {
                DisplayAlert("Alert", "Password is changed!", "Ok");
            }
            else
            {
                DisplayAlert("Error", "Not able to change password, Kindly try again!", "Ok");
            }

        }

        public User SignIn(string userName, string password)
        {
            Item = dm.SigIn(userName, password);
            if (Item != null)
            {
                // Do Login Work like setting variables and others.
                UpdateLoginInfo();
                return Item;
            }
            else return null;
        }

        // All Initilzation work here which is common
        public override async void InitObject()
        {
            Title = "User";
            ItemList = new ObservableCollection<User>();
            ConType = ConType.HybridDB;
            dm = new UsersDataModel(ConType);
            dm.Mode =mode = AppConfig.DbMode;

        }

        public async void UpdateLoginInfo()
        {
            AppConfig.StoreId = Item.StoreId;
            AppConfig.UserName = Item.UserName;
            AppConfig.Name = Item.GuestName;
            AppConfig.Role = Item.Role;
            AppConfig.UserType = Item.UserType;
            AppConfig.EmployeeId = string.IsNullOrEmpty(Item.EmployeeId) ? Item.EmployeeId : String.Empty;
            AppConfig.LogInTime = DateTime.Now;
            AppConfig.LoggedIn = true; 
        }

    }

}
