using System;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace eStore.ViewModels.List.Accounting
{
    public class DailySaleViewMoldel : BaseViewModel<DailySale, DailySaleDataModel>
    {
        public DailySaleViewMoldel()
        {
            InitViewModel();
        }
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
            Icon = Resources.Styles.IconFont.FileInvoiceDollar;
            DataModel = new DailySaleDataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<DailySale>();
            DataModel.Mode = DBType.Azure;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.UserType;
            Title = "Daiy Sale";
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

         

        protected override void UpdateEntities(List<DailySale> values)
        {
            throw new NotImplementedException();
        }

        protected override  async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.InvoiceNumber), MappingName = nameof(DailySale.InvoiceNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.ManualBill), MappingName = nameof(DailySale.ManualBill) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.OnDate), MappingName = nameof(DailySale.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.Amount), MappingName = nameof(DailySale.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.CashAmount), MappingName = nameof(DailySale.CashAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.NonCashAmount), MappingName = nameof(DailySale.NonCashAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.Remarks), MappingName = nameof(DailySale.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.IsDue), MappingName = nameof(DailySale.IsDue) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DailySale.SalesReturn), MappingName = nameof(DailySale.SalesReturn) });
            return gridColumns;
        }
    }

    public class CustomerDueViewModel : BaseViewModel<CustomerDue, DailySaleDataModel>
    {
        public CustomerDueViewModel()
        {
            InitViewModel();
        }
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
            DataModel.Mode = DBType.Azure;
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

        protected override void UpdateEntities(List<CustomerDue> values)
        {
            throw new NotImplementedException();
        }
    }
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
            DataModel.Mode = DBType.Azure;
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

        protected override void UpdateEntities(List<DueRecovery> values)
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

