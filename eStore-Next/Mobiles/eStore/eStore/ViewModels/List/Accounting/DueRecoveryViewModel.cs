using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public class DueRecoveryViewModel : BaseViewModel<DueRecovery, DailySaleDataModel>
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
            Entities = new System.Collections.ObjectModel.ObservableCollection<DueRecovery>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Dues Recovered";
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
                    var data = await DataModel.GetZItems(CurrentSession.StoreCode);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.InvoiceNumber), MappingName = nameof(DueRecovery.InvoiceNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.OnDate), MappingName = nameof(DueRecovery.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.Amount), MappingName = nameof(DueRecovery.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.Due), MappingName = nameof(DueRecovery.Due) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.ParticialPayment), MappingName = nameof(DueRecovery.ParticialPayment) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.PayMode), MappingName = nameof(DueRecovery.PayMode) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.Remarks), MappingName = nameof(DueRecovery.Remarks) });
            return gridColumns;
        }
    }
}