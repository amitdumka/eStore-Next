using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Inventory;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Inventory
{
    public class PurchaseViewModel : BaseViewModel<PurchaseProduct, PurchaseDataModel>
    {
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
            Icon = Resources.Styles.IconFont.MoneyBillWave;
            DataModel = new PurchaseDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<PurchaseProduct>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = " Purchase's";
            DataModel.Connect();
            DefaultSortedColName = nameof(PurchaseProduct.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        protected override void RefreshButton()
        {
            Entities.Clear();
            FetchAsync();
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
                    var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }
        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.InvoiceNo), MappingName = nameof(PurchaseProduct.InvoiceNo) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.OnDate), MappingName = nameof(PurchaseProduct.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.BasicAmount), MappingName = nameof(PurchaseProduct.BasicAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.TaxAmount), MappingName = nameof(PurchaseProduct.TaxAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.TotalAmount), MappingName = nameof(PurchaseProduct.TotalAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.TotalQty), MappingName = nameof(PurchaseProduct.TotalQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PurchaseProduct.Paid), MappingName = nameof(PurchaseProduct.Paid) });

            return gridColumns;
        }
    }
}

