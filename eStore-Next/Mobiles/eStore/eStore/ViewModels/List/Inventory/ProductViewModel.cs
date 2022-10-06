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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.Barcode), MappingName = nameof(ProductItem.Barcode) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.Name), MappingName = nameof(ProductItem.Name) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.ProductCategory), MappingName = nameof(ProductItem.ProductCategory) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.ProductSubCategory), MappingName = nameof(ProductItem.ProductSubCategory) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.StyleCode), MappingName = nameof(ProductItem.StyleCode) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.Description), MappingName = nameof(ProductItem.Description) });

            return gridColumns;
        }
        private async void FetchAsync()
        {
            var data = await DataModel.GetItemsAsync(CurrentSession.StoreCode);
            UpdateEntities(data);

        }
    }
}

