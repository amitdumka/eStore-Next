using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting.Banking
{
    public class BankTranscationViewModel : BaseViewModel<BankTranscation, BankingDataModel>
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
            DataModel = new BankingDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<BankTranscation>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Transcations";
            DataModel.Connect();
            DefaultSortedColName = nameof(BankTranscation.OnDate);
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
                    var data = await DataModel.GetZItems (CurrentSession.StoreCode);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.OnDate), MappingName = nameof(BankTranscation.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.AccountNumber), MappingName = nameof(BankTranscation.AccountNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.Amount), MappingName = nameof(BankTranscation.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.Balance), MappingName = nameof(BankTranscation.Balance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.DebitCredit), MappingName = nameof(BankTranscation.DebitCredit) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.Naration), MappingName = nameof(BankTranscation.Naration) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTranscation.Verified), MappingName = nameof(BankTranscation.Verified) });
            return gridColumns;
        }
    }
}

