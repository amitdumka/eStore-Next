using System;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using eStore.MAUILib.DataModels.Base;

namespace eStore.MAUILib.DataModels.Accounting
{ 
    public class PettyCashDataModel : BaseDataModel<PettyCashSheet, CashDetail>
    {
        public PettyCashDataModel(ConType conType) : base(conType)
        {
        }

         

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateYID()
        {
            throw new NotImplementedException();
        }

        public override List<PettyCashSheet> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

         

        public override Task<List<PettyCashSheet>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashDetail>> GetYFiltered(QueryParam query)
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

