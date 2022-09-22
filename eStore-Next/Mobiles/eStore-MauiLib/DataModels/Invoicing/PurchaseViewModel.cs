using System;
using AKS.Shared.Commons.Models.Inventory;

namespace eStore_MauiLib.DataModels.Invoicing
{
    public class PurchaseViewModel : BaseDataModel<PurchaseProduct, PurchaseItem>
    {
        public PurchaseViewModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<PurchaseProduct>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseItem>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseProduct>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseProduct>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseItem>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseItem>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

