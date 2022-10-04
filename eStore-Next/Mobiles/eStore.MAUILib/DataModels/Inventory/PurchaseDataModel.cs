using AKS.Shared.Commons.Models.Inventory;
using eStore.MAUILib.DataModels.Base;

namespace eStore.MAUILib.DataModels.Inventory
{
    public class PurchaseDataModel : BaseDataModel<PurchaseProduct, PurchaseItem, Stock>
    {
        public PurchaseDataModel(ConType conType) : base(conType)
        {
        }

        public PurchaseDataModel(ConType conType, Permission role) : base(conType, role)
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

        public override List<PurchaseProduct> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseProduct>> GetItemsAsync(string storeid)
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

        public override Task<List<PurchaseItem>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseItem>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetZItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

