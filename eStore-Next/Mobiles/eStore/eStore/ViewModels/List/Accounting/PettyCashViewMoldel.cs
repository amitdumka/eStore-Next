using System;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using eStore.MAUILib.DataModels.Accounting;
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
            throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEntities(List<PettyCashSheet> values)
        {
            throw new NotImplementedException();
        }
        protected override Task<ColumnCollection> SetGridCols()
        {
            throw new NotImplementedException();
        }
    }

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
            throw new NotImplementedException();
        }

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }

        protected override Task<ColumnCollection> SetGridCols()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEntities(List<CashDetail> values)
        {
            throw new NotImplementedException();
        }
    }
}

