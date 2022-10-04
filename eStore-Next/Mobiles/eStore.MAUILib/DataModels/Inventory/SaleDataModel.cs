using AKS.Shared.Commons.Models.Inventory;
using eStore.MAUILib.DataModels.Base;

namespace eStore.MAUILib.DataModels.Inventory
{
    public class SaleDataModel : BaseDataModel<ProductSale, SaleItem, SalePaymentDetail>
    {
        public SaleDataModel(ConType conType) : base(conType)
        {
        }

        public SaleDataModel(ConType conType, Permission role) : base(conType, role)
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

        public override List<ProductSale> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductSale>> GetItemsAsync(string storeid)
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

        public override Task<List<SaleItem>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItem>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SalePaymentDetail>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SalePaymentDetail>> GetZItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

