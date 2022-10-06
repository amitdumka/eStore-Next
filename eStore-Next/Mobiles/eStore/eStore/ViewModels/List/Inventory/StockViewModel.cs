using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Inventory;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Inventory
{
    public class StockViewModel : BaseViewModel<Stock, ProductDataModel>
    {
        protected override void AddButton()
        {
            //TODO: Need to disable  and use for other purpose if desired
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            //TODO: Need to disable  and use for other purpose if desired
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyBillWave;
            DataModel = new ProductDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<Stock>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = " Stock's";
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

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CostPrice), MappingName = nameof(Stock.CostPrice) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.MRP), MappingName = nameof(Stock.MRP) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CurrentQty), MappingName = nameof(Stock.CurrentQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CurrentQtyWH), MappingName = nameof(Stock.CurrentQtyWH) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.StockValue), MappingName = nameof(Stock.StockValue) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.StockValueWH), MappingName = nameof(Stock.StockValueWH) });

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
                    var data = await DataModel.GetYItems(CurrentSession.StoreCode);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }
    }

}

