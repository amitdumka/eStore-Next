using System;
using AKS.Shared.Commons.Models;

namespace eStore_MauiLib.DataModels.Stores
{
    public class StoreDataModel : BaseDataModel<Store>
    {
        public StoreDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<Store>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Store>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Store>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotSupportedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

