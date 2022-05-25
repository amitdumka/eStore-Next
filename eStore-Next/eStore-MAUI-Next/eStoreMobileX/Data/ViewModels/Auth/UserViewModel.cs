using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKS.Shared.Commons.Models.Auth;
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
            Title = "User";
            ItemList = new ObservableCollection<User>();
            this.Item = new User
            {
                StoreId = "ARD",
            };
            dm = new UsersDataModel();

        }

        public UserViewModel(string storeId)
        {
            Title = "User";
            ItemList = new ObservableCollection<User>();
            this.Item = new User
            {
                StoreId = storeId,
            };
            dm = new UsersDataModel();

        }
        private async void RefreshData()
        {
            try
            {
                dm = new UsersDataModel();
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
        public void ItemsSourceRefresh()
        {
            RefreshData();
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

    }

}
