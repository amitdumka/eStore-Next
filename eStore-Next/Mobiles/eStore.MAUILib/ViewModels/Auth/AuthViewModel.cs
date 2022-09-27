using AKS.Shared.Commons.Models.Auth;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore.DatabaseSyncService.Services;
using eStore.MAUILib.DataModels.Auth;
using eStore.MAUILib.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.MAUILib.ViewModels.Auth
{
    public partial class AuthViewModel : BaseViewModel<User, AuthDataModel>
    {
        [ObservableProperty]
        private Shell _appShell;

        [ObservableProperty]
        private bool _isEnableSign;
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
            throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEntities(List<User> values)
        {
            throw new NotImplementedException();
        }

        public async void SyncLocal()
        {
            if (!CurrentSession.LocalStatus)
            {
                if (!DatabaseStatus.VerifyLocalStatus())
                {
                   await DatabaseStatus.SyncInitial();
                    IsEnableSign = CurrentSession.LocalStatus=true;
                }
                
                IsEnableSign = CurrentSession.LocalStatus=true;
            }

            CurrentSession.LocalStatus = true;
            IsEnableSign = CurrentSession.LocalStatus=true;

        }
    }
}
