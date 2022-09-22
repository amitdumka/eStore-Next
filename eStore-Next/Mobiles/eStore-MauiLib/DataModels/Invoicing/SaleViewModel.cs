using System;
using AKS.Shared.Commons.Models.Inventory;

namespace eStore_MauiLib.DataModels.Invoicing
{
    public class SaleViewModel : BaseDataModel<ProductSale, SaleItem>
    {
        public SaleViewModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<ProductSale>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItem>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductSale>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductSale>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItem>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItem>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

