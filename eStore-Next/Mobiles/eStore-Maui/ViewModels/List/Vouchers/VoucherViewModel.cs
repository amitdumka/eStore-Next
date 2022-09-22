﻿using System;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using eStore_MauiLib.DataModels.Accounting;
using CommunityToolkit.Mvvm.ComponentModel;


namespace eStore_Maui.ViewModels.List.Vouchers
{
    public partial class VoucherViewModel : BaseViewModel<Voucher, VoucherDataModel>
    {
        [ObservableProperty]
        private VoucherType _voucherType;

        public VoucherViewModel()
        {
            DataModel = new VoucherDataModel(ConType.Hybrid);
            DataModel.Mode = DBType.Local;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            InitViewModel();
        }
        protected override void InitViewModel()
        {
            Title = "Vouchers";
            DataModel.Connect();
            FetchAsync();
            Icon = eStore_Maui.Resources.Styles.IconFont.FileInvoice;
        }

        partial void OnVoucherTypeChanged(VoucherType value)
        {
            // Use filter here to change the view. 
            throw new NotImplementedException();
        }
        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Voucher>> Filter(string fitler)
        {
            throw new NotImplementedException();
        }

        protected override Task<Voucher> Get(string id)
        {
           return  DataModel.GetById(id);
        }

        protected override Task<Voucher> GetById(int id)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Voucher>> GetList()
        {
           return DataModel.GetItems(CurrentSession.StoreCode);
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }


         

        protected override void UpdateEntities(List<Voucher> vouchers)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<Voucher>();
            foreach (var item in vouchers)
            {
                Entities.Add(item);
            }
            RecordCount = _entities.Count;
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
                    var data = await DataModel.GetItems();
                    UpdateEntities(data);

                    break;
                default:
                    Toast.Make("You are not authorised to access!", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    break;
            }
        }

        protected override Task<bool> Edit(Voucher value)
        {

            //TODO: Call EntryView for Edit and wait till get result from there and update record based on that.
            throw new NotImplementedException();
        }

         
    }
}

