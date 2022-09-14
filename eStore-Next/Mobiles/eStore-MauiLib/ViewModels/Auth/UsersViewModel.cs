using AKS.Shared.Commons.Models.Auth;
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
        [MinLength(8)]
        [MaxLength(12)]
        private string _password;

        #endregion PropertyFields

        public UsersViewModel()
        {
            DataModel = new AuthDataModel(ConType.Hybrid);
            DataModel.Connect();
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
                return true;
            }
        }

        [RelayCommand]
        private async Task<bool> SignOut()
        { return false; }

        [RelayCommand]
        private async Task<bool> SignUP()
        { return false; }

        [RelayCommand]
        private void ShowErrors()
        {
            string message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            //_ = DialogService.ShowMessageDialogAsync("Validation errors", message);
            Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
        }
    }
}