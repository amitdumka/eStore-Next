using System;
using AKS.Shared.Commons.Models.Inventory;

namespace eStore_MauiLib.DataModels.Inventory
{
	public class StockViewModel:BaseDataModel<Stock, ProductItem>
	{
        public StockViewModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<Stock>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductItem>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductItem>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductItem>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

