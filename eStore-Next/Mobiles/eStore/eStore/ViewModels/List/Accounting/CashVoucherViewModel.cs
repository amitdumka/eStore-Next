using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public partial class CashVoucherViewModel : BaseViewModel<CashVoucher, VoucherDataModel>
    {
        [ObservableProperty]
        private VoucherType _voucherType;

        
        protected override void InitViewModel()
        {
            DataModel = new VoucherDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<CashVoucher>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "CashVouchers";
            DataModel.Connect();
            DefaultSortedColName = nameof(CashVoucher.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
            //Icon = eStore_Maui.Resources.Styles.IconFont.FileInvoice;
        }
        protected override void AddButton()
        {
            //var c = Delete();
            Notify.NotifyLong("Delete: ");
        }

        //protected override async Task<bool> Delete()
        //{
        //    var dl = DataModel.GetContextLocal()
        //        .CashVouchers.Where(c => c.UserId.Contains("#TESTING")).ToList();

        //    DataModel.GetContextLocal().CashVouchers.RemoveRange(dl);
        //    DataModel.GetContextAzure().CashVouchers.RemoveRange(dl);
        //    try
        //    {
        //        bool local = await DataModel.GetContextLocal().SaveChangesAsync() > 0;
        //        bool azure = DataModel.GetContextAzure().SaveChanges() > 0;
        //        if (!azure)
        //        {
        //            Notify.NotifyVLong("Failed to remove on remote");
        //        }
        //        return local;
        //    }
        //    catch (Exception e)
        //    {
        //        Notify.NotifyLong($"Error: {e.Message} ");
        //        return false;
        //    }
        //}

        protected override void DeleteButton()
        {
            //var c = Delete();
            //Notify.NotifyLong("Deleted: " + c.Result);
        }

        //protected override Task<bool> Edit(CashVoucher value)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<List<CashVoucher>> Filter(string fitler)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<CashVoucher> Get(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<CashVoucher> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<List<CashVoucher>> GetList()
        //{
        //    throw new NotImplementedException();
        //}

        

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected new void UpdateEntities(List<CashVoucher> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<CashVoucher>();
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
                    var data = await DataModel.GetYItemsAsync();
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

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.VoucherNumber), MappingName = nameof(Voucher.VoucherNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.OnDate), MappingName = nameof(Voucher.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.VoucherType), MappingName = nameof(Voucher.VoucherType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.PartyName), MappingName = nameof(Voucher.PartyName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Particulars), MappingName = nameof(Voucher.Particulars) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Remarks), MappingName = nameof(Voucher.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Amount), MappingName = nameof(Voucher.Amount) });

            return gridColumns;
        }
    }
}