using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public class PettyCashViewMoldel : BaseViewModel<PettyCashSheet, PettyCashDataModel>
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
            Icon = Resources.Styles.IconFont.MoneyCheck;
            DataModel = new PettyCashDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<PettyCashSheet>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Petty Cash Sheet";
            DataModel.Connect();
            DefaultSortedColName = nameof(Voucher.OnDate);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.Id), MappingName = nameof(PettyCashSheet.Id) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.OnDate), MappingName = nameof(PettyCashSheet.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.OpeningBalance), MappingName = nameof(PettyCashSheet.OpeningBalance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.DailySale), MappingName = nameof(PettyCashSheet.DailySale) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.ManualSale), MappingName = nameof(PettyCashSheet.ManualSale) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.ClosingBalance), MappingName = nameof(PettyCashSheet.ClosingBalance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.ReceiptsTotal), MappingName = nameof(PettyCashSheet.ReceiptsTotal) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.BankWithdrawal), MappingName = nameof(PettyCashSheet.BankWithdrawal) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.PaymentTotal), MappingName = nameof(PettyCashSheet.PaymentTotal) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.BankDeposit), MappingName = nameof(PettyCashSheet.BankDeposit) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.CardSale), MappingName = nameof(PettyCashSheet.CardSale) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.NonCashSale), MappingName = nameof(PettyCashSheet.NonCashSale) });
            return gridColumns;
        }
    }
}

