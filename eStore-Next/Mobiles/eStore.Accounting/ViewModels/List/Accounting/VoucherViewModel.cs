using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore_MauiLib.DataModels.Accounting;
using eStore_MauiLib.Helpers;
using eStore_MauiLib.ViewModels;

namespace eStore.Accounting.ViewModels.List.Accounting
{
    public partial class VoucherViewModel : BaseViewModel<Voucher, VoucherDataModel>
    {
        [ObservableProperty]
        private VoucherType _voucherType;

        public VoucherViewModel() : base()
        {
            DataModel = new VoucherDataModel(ConType.Hybrid, CurrentSession.Role);
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            DataModel.Connect();
            DataModel.Mode = DBType.Local;
            InitViewModel();
        }

        protected override void AddButton()
        {
            var c = Delete();
            Notify.NotifyLong("Delete: ");
        }

        protected override async Task<bool> Delete()
        {
            var dl = DataModel.GetContextLocal()
                .Vouchers.Where(c => c.UserId.Contains("#TESTING")).ToList();

            DataModel.GetContextLocal().Vouchers.RemoveRange(dl);
            DataModel.GetContextAzure().Vouchers.RemoveRange(dl);
            try
            {
                bool local = await DataModel.GetContextLocal().SaveChangesAsync() > 0;
                bool azure = DataModel.GetContextAzure().SaveChanges() > 0;
                if (!azure)
                {
                    Notify.NotifyVLong("Failed to remove on remote");
                }
                if (local)
                {
                    Entities.Clear();
                }
                return local;
            }
            catch (Exception e)
            {
                Notify.NotifyLong($"Error: {e.Message} ");
                return false;
            }
        }

        protected override void DeleteButton()
        {
            var c = Delete();
            Notify.NotifyLong("Deleted: " + c.Result);
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
            DefaultSortedColName = nameof(Voucher.OnDate);
            DefaultSortedOrder = Descending;
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
                    Notify.NotifyVLong("You are not authorised to access!");
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