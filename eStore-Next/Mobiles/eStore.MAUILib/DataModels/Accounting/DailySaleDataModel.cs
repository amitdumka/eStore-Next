using System;
using AKS.Shared.Commons.Models.Sales;
using eStore.MAUILib.DataModels.Base;

namespace eStore.MAUILib.DataModels.Accounting
{
    public class DailySaleDataModel : BaseDataModel<DailySale, CustomerDue, DueRecovery>
    {
        public DailySaleDataModel(ConType conType) : base(conType)
        {
        }

        public DailySaleDataModel(ConType conType, Permission role) : base(conType, role)
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

        public override Task<string> GenrateZID()
        {
            throw new NotImplementedException();
        }

        public override List<DailySale> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DailySale>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
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

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CustomerDue>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CustomerDue>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DueRecovery>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DueRecovery>> GetZItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

