using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Inventory;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Inventory
{
    public class ProductViewModel : BaseViewModel<ProductItem, ProductDataModel>
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
            DataModel = new ProductDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<ProductItem>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = " Products";
            DataModel.Connect();
            DefaultSortedColName = nameof(ProductSale.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.Barcode), MappingName = nameof(Stock.Barcode) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.OnDate), MappingName = nameof(ProductSale.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.Barcode), MappingName = nameof(Stock.CostPrice) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CurrentQty), MappingName = nameof(Stock.CurrentQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CurrentQtyWH), MappingName = nameof(Stock.CurrentQtyWH) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.StockValue), MappingName = nameof(Stock.StockValue) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.StockValueWH), MappingName = nameof(Stock.StockValueWH) });

            return gridColumns;
        }
        private async void FetchAsync()
        {
            var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode);
            UpdateEntities(data);

        }
    }
}

