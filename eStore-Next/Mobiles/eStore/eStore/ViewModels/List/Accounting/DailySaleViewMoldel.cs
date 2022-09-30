using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using Android.OS;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Java.Lang;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public partial class DailySaleViewMoldel : BaseViewModel<DailySale, DailySaleDataModel>
    {
        private bool today = false;
        [ObservableProperty]
        private bool synced = false;

        [RelayCommand]
        private async Task Sync()
        {
            if(!synced)
              Synced = await DataModel.SyncCurrentMonth();
        }
        partial void OnSyncedChanged(bool value)
        {
            if (synced)
            {
                if (today)
                    Today("0");
                else
                    Monthly("0");
                Notify.NotifyVShort("Sale's data has been synced with remote");
            }
        }
        [RelayCommand]
        protected void Monthly(string last)
        {
            var l = Int32.Parse(last);
            if (today)
                Filter(false,l);
            today = false;
            if (l < 0)
                Notify.NotifyVShort("Displaying Last Month sale");
            else
                Notify.NotifyVShort("Displaying Current Month sale");
        }
        [RelayCommand]
        protected void Today( string  last)
        {
            var l = Int32.Parse(last);
            if (!today)
                Filter(true,l);
            today = true;
            if(l<0)
            Notify.NotifyVShort("Displaying Yesterday sale");
            else Notify.NotifyVShort("Displaying Current Day sale");
        }



        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }
        protected async void Filter(bool today, int fil)
        {

            var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode, today, fil);
            Entities.Clear();
            UpdateEntities(data);

        }
        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.FileInvoiceDollar;
            DataModel = new DailySaleDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<DailySale>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Daiy Sale";
            DataModel.Connect();
            DefaultSortedColName = nameof(DailySale.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
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
                    var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.InvoiceNumber), MappingName = nameof(DailySale.InvoiceNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.ManualBill), MappingName = nameof(DailySale.ManualBill) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.OnDate), MappingName = nameof(DailySale.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.Amount), MappingName = nameof(DailySale.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.CashAmount), MappingName = nameof(DailySale.CashAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.NonCashAmount), MappingName = nameof(DailySale.NonCashAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.Remarks), MappingName = nameof(DailySale.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.IsDue), MappingName = nameof(DailySale.IsDue) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.SalesReturn), MappingName = nameof(DailySale.SalesReturn) });
            return gridColumns;
        }
    }
}