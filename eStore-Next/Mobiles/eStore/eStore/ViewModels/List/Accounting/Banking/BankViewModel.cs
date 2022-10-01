using System;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting.Banking
{
    public class BankViewModel : BaseViewModel<Bank, BankingDataModel>
    {
        protected override async void AddButton()
        {
            await Shell.Current.GoToAsync($"banking/bank/Entry?vm={this}");
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyCheck;
            DataModel = new BankingDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<Bank>();
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Banks";
            DataModel.Connect();
            DefaultSortedColName = nameof(Bank.Name);
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
                    var data = await DataModel.GetItemsAsync();
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Bank.BankId), MappingName = nameof(Bank.BankId) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Bank.Name), MappingName = nameof(Bank.Name) });

            return gridColumns;
        }


    }
}

