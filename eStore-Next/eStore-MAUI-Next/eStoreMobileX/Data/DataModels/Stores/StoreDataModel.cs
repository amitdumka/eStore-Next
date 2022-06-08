using AKS.Shared.Commons.Models;
using eStoreMobileX.Data.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobileX.Data.DataModels.Stores
{
    public class StoreDataModel : HybridDataMode<Store>

    {
        public StoreDataModel(ConType conType) : base(conType)
        {
        }

        public StoreDataModel(ConType conType, string url, string name) : base(conType, url, name)
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
    }
}
