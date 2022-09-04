using System;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;

namespace eStore_MauiLib.DataModels.Accounting
{
    public class PettyCashDataModel : BaseDataModel<PettyCashSheet, CashDetail>
    {
        public PettyCashDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<PettyCashSheet>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashDetail>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PettyCashSheet>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PettyCashSheet>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashDetail>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashDetail>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

