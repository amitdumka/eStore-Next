﻿using System;
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
            Entities = new System.Collections.ObjectModel.ObservableCollection<Bank>();
            DataModel.Mode = DBType.Azure;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Bank";
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

        protected override Task<global::Syncfusion.Maui.DataGrid.ColumnCollection> SetGridCols()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEntities(List<Bank> values)
        {
            throw new NotImplementedException();
        }
        
    }
}

