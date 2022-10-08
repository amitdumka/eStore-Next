using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;
using eStore.MAUILib.DataModels.Base;
using Microsoft.EntityFrameworkCore;

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
            return GetContext().ProductSales.Where(c => c.StoreId == storeid).OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override List<int> GetYearList(string storeid)
        {
            return GetContext().ProductSales.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override List<int> GetYearList()
        {
            return GetContext().ProductSales.Select(c => c.OnDate.Year).Distinct().ToList();
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

        public override Task<List<SaleItem>> GetYItems(string storeId)
        {
            return GetContext().SaleItems.Include(c => c.ProductSale).Where(c => c.ProductSale.StoreId == storeId).OrderByDescending(c => c.ProductSale.OnDate).ToListAsync();
        }

        public override Task<List<SalePaymentDetail>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SalePaymentDetail>> GetZItems(string storeid)
        {
            return GetContext().SalePaymentDetails.Include(c => c.ProductSale).Where(c => c.ProductSale.StoreId == storeid).OrderByDescending(c => c.ProductSale.OnDate).ToListAsync();
        }

        public override async Task<bool> InitContext() => Connect();

        #region SaleItemHelpers
        public void GetBarcode(string barcode)
        {
            var stock = GetContext().Stocks.Include(c => c.Product).Where(c => c.StoreId == CurrentSession.StoreCode && c.Barcode == barcode)
                .Select(c => new { c.Barcode, c.CurrentQty, c.CurrentQtyWH, c.Unit, c.MRP, c.Product.Description, c.Product.HSNCode })
                .FirstAsync();
        }
        #endregion
    }
}

