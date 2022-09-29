using AKS.Shared.Commons.Data.Helpers.Auth;
using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eStore.DatabaseSyncService.Services;
using eStore.MAUILib.DataModels.Auth;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;
using System.ComponentModel.DataAnnotations;

namespace eStore.MAUILib.ViewModels.Auth
{
    public partial class AuthViewModel : BaseViewModel<User, AuthDataModel>
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

        [ObservableProperty]
        private bool _isEnableSign;

        public AuthViewModel()
        {
            DataModel = new AuthDataModel(ConType.Hybrid);
            InitViewModel();
        }


        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }


        protected override void InitViewModel()
        {
            DataModel.Connect();
            DataModel.Mode = DBType.Local;
            //UpdateEntities(await DataModel.GetItems());
            Title = "Login";
            GuestName = "No user ";
            DefaultSortedColName = nameof(User.UserName);
            UserName = "Alok@eStore.in";
            Password = "Alok";
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected new void UpdateEntities(List<User> users)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<User>();
            foreach (var user in users)
            {
                Entities.Add(user);
            }
            RecordCount = Entities.Count();
        }

        public async void SyncLocal()
        {
            if (!CurrentSession.LocalStatus)
            {
                if (!DatabaseStatus.VerifyLocalStatus())
                {
                    await DatabaseStatus.SyncInitial();
                    IsEnableSign = CurrentSession.LocalStatus = true;
                }

                IsEnableSign = CurrentSession.LocalStatus = true;
            }

            CurrentSession.LocalStatus = true;
            IsEnableSign = CurrentSession.LocalStatus = true;

        }

        #region Login

        [RelayCommand]
        private void ShowErrors()
        {
            string message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Notify.NotifyLong(message);
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
                    CurrentSession.UserName = user.UserName + "#TESTING";
                    CurrentSession.GuestName = user.GuestName;
                    CurrentSession.IsLoggedIn = true;
                    CurrentSession.LoggedTime = DateTime.Now;
                    CurrentSession.UserType = user.UserType;
                    CurrentSession.EmployeeId = string.IsNullOrEmpty(user.EmployeeId) ? null : user.EmployeeId;
                    CurrentSession.Role = AuthHelper.GetPermission(user.UserType);
                    CurrentSession.Perimissions = AuthHelper.GetPermission(CurrentSession.Role);
                    GuestName = user.GuestName;

                    Notify.NotifyVLong($"Welcome, {GuestName}!, Now you can operate in , {user.UserType}, mode. ");

                    if (store != null)
                    {
                        CurrentSession.Address = store.City + "\t" + store.State;
                        CurrentSession.TaxNumber = store.GSTIN;
                        CurrentSession.StoreName = store.StoreName;
                        CurrentSession.PhoneNo = store.StorePhoneNumber;
                        CurrentSession.CityName = store.City;
                        Notify.NotifyLong($"Welcome {CurrentSession.GuestName}, from {CurrentSession.StoreName}");
                        Application.Current.MainPage = AppShell;
                        return true;
                    }
                }
                Notify.NotifyVLong($"User {UserName} not Found ....");

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
            Notify.NotifyVLong($"Welcome {CurrentSession.GuestName}, from {CurrentSession.StoreName}");
            Application.Current.MainPage = AppShell;
            return true;
        }

        protected override Task<ColumnCollection> SetGridCols()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
