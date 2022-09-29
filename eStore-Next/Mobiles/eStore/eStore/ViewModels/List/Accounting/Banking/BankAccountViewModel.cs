using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting.Banking
{
    public class BankAccountViewModel : BaseViewModel<BankAccount, BankingDataModel>
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
            Entities = new System.Collections.ObjectModel.ObservableCollection<BankAccount>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Bank Account";
            DataModel.Connect();
            DefaultSortedColName = nameof(BankAccount.AccountType);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.AccountNumber), MappingName = nameof(BankAccount.AccountNumber) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.AccountHolderName), MappingName = nameof(BankAccount.AccountHolderName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.AccountType), MappingName = nameof(BankAccount.AccountType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.BankId), MappingName = nameof(BankAccount.BankId) });
           // gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.Bank.Name), MappingName = nameof(BankAccount.Bank.Name) });
            
            return gridColumns;
        }
    }
}

