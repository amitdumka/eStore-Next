/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.PosSystem.Helpers;
using AKS.PosSystem.Models.VM;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace AKS.PosSystem.DataModels
{
    public class ProductStockDataModel : DataModel<ProductItem, Stock>
    {
        public StockInfo? GetItemDetail(string barcode, bool Tailoring)
        {
            if (barcode.Length < 7) return null;
            if (Tailoring)
            {
                var item = azureDb.ProductItems.Where(c => c.Barcode == barcode)
                    .Select(c => new StockInfo { Barcode = c.Barcode, HoldQty = 1, Qty = 1, Unit = Unit.Nos, TaxRate = 5, TaxType = c.TaxType, Rate = c.MRP, Category = c.ProductCategory })
                    .FirstOrDefault();
                return item;
            }
            else return GetItemDetail(barcode);
        }

        /// <summary>
        /// return stock info. Add to API/DataModel
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public StockInfo? GetItemDetail(string barcode)
        {
            if (barcode.Length < 7)
            {
                return null;
            }
            var item = azureDb.Stocks.Include(c => c.Product).Where(c => c.Barcode == barcode)
                .Select(item =>
           new StockInfo()
           {
               Barcode = item.Barcode,
               HoldQty = item.CurrentQtyWH,
               Qty = item.CurrentQty,
               Rate = item.Product.MRP,
               ProductItem = item.Product.Name,
               TaxType = item.Product.TaxType,
               Unit = item.Product.Unit,
               Category = item.Product.ProductCategory,
               TaxRate = SaleUtils.SetTaxRate(item.Product.ProductCategory, item.Product.MRP)
           }).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Init View Model
        /// </summary>
        /// <returns></returns>

        public override ProductItem Get(string id)
        {
            throw new NotImplementedException();
        }

        public override ProductItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetBarcodeList()
        {
            return azureDb.Stocks.Where(c => c.PurhcaseQty > 0).Select(c => new { c.Barcode, c.CurrentQty, c.CurrentQtyWH })
                    .Where(c => c.CurrentQty > 0).Select(c => c.Barcode).ToList();
        }

        public override List<ProductItem> GetList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Return Stock for barcode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Stock GetY(string id)
        {
            return azureDb.Stocks.Find(id);
        }

        public override Stock GetY(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Stock> GetYList()
        {
            return azureDb.Stocks.ToList();
        }
    }
}