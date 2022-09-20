using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eStore_MauiLib.DataModels.Auths;
using System.ComponentModel.DataAnnotations;

namespace eStore_MauiLib.ViewModels.Auth
{
    public partial class UsersViewModel : BaseViewModel<User, AuthDataModel>
    {
        #region PropertyFields

        [ObservableProperty]
        //[NotifyPropertyChangedRecipients]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        [EmailAddress]
        private string _userName;

        [ObservableProperty]
        //[NotifyPropertyChangedRecipients]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(4)]
        [MaxLength(12)]
        private string _password;
        
        [ObservableProperty]
        private static Shell _appShell;

        [ObservableProperty]
        private string _guestName;
        #endregion PropertyFields

        public UsersViewModel()
        {
            DataModel = new AuthDataModel(ConType.Hybrid);
            InitViewModel();
        }

        [RelayCommand]
        private async void InitViewModel()
        {
            DataModel.Connect();
            DataModel.Mode = DBType.Local;

            UpdateEntities(await DataModel.GetItems());
            Title = "Users";
            GuestName = "No user ";
            DefaultSortedColName = nameof(User.UserName);
            
        }
        //[RelayCommand]
        private void UpdateEntities(List<User> users)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<User>();
            foreach (var user in users)
            {
                Entities.Add(user);
            }
            RecordCount=Entities.Count();
        }
        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override Task<List<User>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<User> Get(string id)
        {
            throw new NotImplementedException();
        }

        protected override Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<User>> GetList()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Save(bool isNew = false)
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        private async Task<bool> SignIn()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                ShowErrors();
                return false;
            }
            else
            {
                var user = DataModel.SignIn(_userName, _password);
                if (user != null)
                {
                    var store = await DataModel.GetStore(user.StoreId);
                    CurrentSession.StoreCode = user.StoreId;
                    CurrentSession.UserName = user.UserName;
                    CurrentSession.GuestName = user.GuestName;
                    CurrentSession.IsLoggedIn = true;
                    CurrentSession.LoggedTime = DateTime.Now;
                    CurrentSession.UserType = user.UserType;
                    CurrentSession.EmployeeId = string.IsNullOrEmpty(user.EmployeeId) ? null:user.EmployeeId ;
                    
                    GuestName = user.GuestName;
                    if (store != null)
                    {
                        CurrentSession.Address = store.City + "\t" + store.State;
                        CurrentSession.TaxNumber = store.GSTIN;
                        CurrentSession.StoreName = store.StoreName;
                        CurrentSession.PhoneNo = store.StorePhoneNumber;
                        CurrentSession.CityName = store.City;
                        await Toast.Make($"Welcome {CurrentSession.GuestName}, from {CurrentSession.StoreName}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                        Application.Current.MainPage = AppShell;
                        return true;
                    }
                }
                await Toast.Make($"User {UserName} not Found ....", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return false;
            }
        }

        [RelayCommand]
        private async Task<bool> SignOut()
        {
            CurrentSession.Clear();
            return true;
        }

        [RelayCommand]
        private async Task<bool> SignUP()
        {
            await Toast.Make($"Welcome {CurrentSession.GuestName}, from {CurrentSession.StoreName}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            Application.Current.MainPage = AppShell;
            return true;
        }

        [RelayCommand]
        private void ShowErrors()
        {
            string message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
        }

        protected override void AddButton()
        {
            Toast.Make("Add Button Presed", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }

        protected override void DeleteButton()
        {
            Toast.Make("Delete Button Presed", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            //throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            Toast.Make("Refresh Button Presed", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }

        
    }
}