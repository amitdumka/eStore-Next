using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public partial class NotesViewModel : BaseViewModel<Note, VoucherDataModel>
    {
        [ObservableProperty]
        private VoucherType _voucherType;

        

        protected override void AddButton()
        {
            //  var c = Delete();
            Notify.NotifyLong("Delete: ");
        }

        //protected override async Task<bool> Delete()
        //{
        //    var dl = DataModel.GetContextLocal()
        //        .Vouchers.Where(c => c.UserId.Contains("#TESTING")).ToList();

        //    DataModel.GetContextLocal().Vouchers.RemoveRange(dl);
        //    DataModel.GetContextAzure().Vouchers.RemoveRange(dl);
        //    try
        //    {
        //        bool local = await DataModel.GetContextLocal().SaveChangesAsync() > 0;
        //        bool azure = DataModel.GetContextAzure().SaveChanges() > 0;
        //        if (!azure)
        //        {
        //            Notify.NotifyVLong("Failed to remove on remote");
        //        }
        //        if (local)
        //        {
        //            Entities.Clear();
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
            // Notify.NotifyLong("Deleted: " + c.Result);
        }

        //protected override Task<bool> Edit(Voucher value)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<List<Voucher>> Filter(string fitler)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<Voucher> Get(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<Voucher> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<List<Voucher>> GetList()
        //{
        //    throw new NotImplementedException();
        //}

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyCheck;
            DataModel = new VoucherDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<Note>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Vouchers";
            DataModel.Connect();
            DefaultSortedColName = nameof(Note.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
             
        }

        protected override void RefreshButton()
        {
            // throw new NotImplementedException();
        }

        protected new void UpdateEntities(List<Note> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<Note>();
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
                    var data = await DataModel.GetZItems(CurrentSession.StoreCode);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.NoteNumber), MappingName = nameof(Note.NoteNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.OnDate), MappingName = nameof(Note.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.NotesType), MappingName = nameof(Note.NotesType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.PartyName), MappingName = nameof(Note.PartyName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.Reason), MappingName = nameof(Note.Reason) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.Remarks), MappingName = nameof(Note.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Note.Amount), MappingName = nameof(Note.Amount) });
            return gridColumns;
        }
    }
}