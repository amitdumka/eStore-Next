using System;
using AKS.Shared.Commons.Models.Sales;

namespace eStore_MauiLib.DataModels.Accounting
{
    public class DailySaleDataModel : BaseDataModel<DailySale, CustomerDue, DueRecovery>
    {
        public DailySaleDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<DailySale>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CustomerDue>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DueRecovery>> FindZAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DailySale>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DailySale>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CustomerDue>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CustomerDue>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DueRecovery>> GetZItems(int storeid)
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

