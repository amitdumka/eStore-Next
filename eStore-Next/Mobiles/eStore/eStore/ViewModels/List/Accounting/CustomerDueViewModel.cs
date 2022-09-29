using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public class CustomerDueViewModel : BaseViewModel<CustomerDue, DailySaleDataModel>
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
            Icon = Resources.Styles.IconFont.ChalkboardTeacher;
            DataModel = new DailySaleDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<CustomerDue>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Customer Dues";
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
                    var data = await DataModel.GetYItems(CurrentSession.StoreCode);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.InvoiceNumber), MappingName = nameof(CustomerDue.InvoiceNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.OnDate), MappingName = nameof(CustomerDue.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.Amount), MappingName = nameof(CustomerDue.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.ClearingDate), MappingName = nameof(CustomerDue.ClearingDate) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.Paid), MappingName = nameof(CustomerDue.Paid) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.StoreId), MappingName = nameof(CustomerDue.StoreId) });

            return gridColumns;
        }

        protected new void UpdateEntities(List<CustomerDue> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<CustomerDue>();
            foreach (var item in values)
            {
                Entities.Add(item);
            }
            RecordCount = _entities.Count;
        }
    }
}