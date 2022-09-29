using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting.Banking
{
    public class VendorAccountViewModel : BaseViewModel<VendorBankAccount, BankInfoDataModel>
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
            DataModel = new BankInfoDataModel (ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<VendorBankAccount>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Vendor Bank Account";
            DataModel.Connect();
            DefaultSortedColName = nameof(VendorBankAccount.AccountHolderName);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.AccountNumber), MappingName = nameof(VendorBankAccount.AccountNumber) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.AccountHolderName), MappingName = nameof(VendorBankAccount.AccountHolderName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.BranchName), MappingName = nameof(VendorBankAccount.BranchName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.AccountType), MappingName = nameof(VendorBankAccount.AccountType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.OpenningBalance), MappingName = nameof(VendorBankAccount.OpenningBalance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.IsActive), MappingName = nameof(VendorBankAccount.IsActive) });
            return gridColumns;
        }
    }
}

