using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore_Maui.ViewModels;
using eStore_MauiLib.DataModels.Accounting;

namespace eStore.Accounting.ViewModels.List.Accounting
{
    public partial class VoucherViewModel : BaseViewModel<Voucher, VoucherDataModel>
    {

        [ObservableProperty]
        private VoucherType _voucherType;

        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Edit(Voucher value)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Voucher>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<Voucher> Get(string id)
        {
            throw new NotImplementedException();
        }

        protected override Task<Voucher> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Voucher>> GetList()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            DataModel = new VoucherDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<Voucher>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Vouchers";
            DataModel.Connect();
            FetchAsync();
            //Icon = eStore_Maui.Resources.Styles.IconFont.FileInvoice;
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEntities(List<Voucher> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<Voucher>();
            foreach (var item in values)
            {
                Entities.Add(item);
            }
            RecordCount = _entities.Count;
        }
        protected async Task FetchAsync()
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.StoreManager:
                case UserType.Accountant:
                case UserType.CA:
                case UserType.PowerUser:
                    var data = await DataModel.GetItemsAsync();
                    UpdateEntities(data);
                    break;
                default:
                    Toast.Make("You are not authorised to access!", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    break;
            }
        }
        partial void OnVoucherTypeChanged(VoucherType value)
        {
            // Use filter here to change the view. 
            throw new NotImplementedException();
        }
    }
}
