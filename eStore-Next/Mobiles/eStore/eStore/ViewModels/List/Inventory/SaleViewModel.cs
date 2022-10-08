using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eStore.MAUILib.DataModels.Inventory;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Inventory
{
    public partial class SaleViewModel : BaseViewModel<ProductSale, SaleDataModel>
    {
        #region Fields

        [ObservableProperty]
        private bool _synced = false;

        [ObservableProperty]
        private InvoiceType _invoiceType = InvoiceType.Sales;

        #endregion Fields

        [ObservableProperty]
        private List<string> _invTypes;// = Enum.GetNames(typeof(InvoiceType)).ToList();

        partial void OnSyncedChanged(bool value)
        {
            if (value)
            {
                Entities.Clear();
                FetchAsync();
            }
        }

        partial void OnInvoiceTypeChanged(InvoiceType value)
        {
            Entities.Clear();
            FetchAsync();
        }

        [RelayCommand]
        protected async void Sync()
        {
            Synced = await DataModel.SyncInvoices(_invoiceType);
        }

        protected override async void AddButton()
        {
            await Shell.Current.GoToAsync($"sale/Entry?vm={this}&invType={_invoiceType}");
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            InvTypes = Enum.GetNames(typeof(InvoiceType)).ToList();
            Icon = Resources.Styles.IconFont.FileInvoiceDollar;

            DataModel = new SaleDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<ProductSale>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = " Sale's";
            DataModel.Connect();
            DefaultSortedColName = nameof(ProductSale.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        protected override void RefreshButton()
        {
            Entities.Clear();
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.InvoiceNo), MappingName = nameof(ProductSale.InvoiceNo) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.OnDate), MappingName = nameof(ProductSale.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalBasicAmount), MappingName = nameof(ProductSale.TotalBasicAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalTaxAmount), MappingName = nameof(ProductSale.TotalTaxAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalPrice), MappingName = nameof(ProductSale.TotalPrice) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalQty), MappingName = nameof(ProductSale.TotalQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.Tailoring), MappingName = nameof(ProductSale.Tailoring) });

            return gridColumns;
        }

        private async void FetchAsync()
        {
            switch (Role)
            {
                case UserType.Admin:
                case UserType.Owner:
                case UserType.StoreManager:
                case UserType.Accountant:
                case UserType.CA:
                case UserType.PowerUser:
                    var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode, _invoiceType,13);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }
    }
}