using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public class CashDetailViewModel : BaseViewModel<CashDetail, PettyCashDataModel>
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
            DataModel = new PettyCashDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<CashDetail>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Cash Details";
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.CashDetailId), MappingName = nameof(CashDetail.CashDetailId) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.OnDate), MappingName = nameof(CashDetail.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.Count), MappingName = nameof(CashDetail.Count) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.TotalAmount), MappingName = nameof(CashDetail.TotalAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N2000), MappingName = nameof(CashDetail.N2000) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N500), MappingName = nameof(CashDetail.N500) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N200), MappingName = nameof(CashDetail.N200) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N100), MappingName = nameof(CashDetail.N100) });

            return gridColumns;
        }

        
    }
}

