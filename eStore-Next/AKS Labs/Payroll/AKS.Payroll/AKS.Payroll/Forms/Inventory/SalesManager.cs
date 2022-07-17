
namespace AKS.Payroll.Forms.Inventory
{
	/// <summary>
    /// Helps and Manages Sales
    /// </summary>
	public class SalesManager:Manager
	{
		

		public SalesManager(AzurePayrollDbContext db, LocalPayrollDbContext ldb)
		{
			azureDb = db; localDb = ldb;
		}

		protected void Save()
        {

        }

		protected void Delete()
        {

        }
        protected static int SetTaxRate(ProductCategory category, decimal Price)
        {
            int rate = 0;
            switch (category)
            {
                case ProductCategory.Fabric:
                    rate = 5;
                    break;

                case ProductCategory.ReadyMade:
                    rate = Price > 999 ? 12 : 5;
                    break;

                case ProductCategory.Accessiories:
                    rate = 12;
                    break;

                case ProductCategory.Tailoring:
                    rate = 5;
                    break;

                case ProductCategory.Trims:
                    rate = 5;
                    break;

                case ProductCategory.PromoItems:
                    rate = 0;
                    break;

                case ProductCategory.Coupons:
                    rate = 0;
                    break;

                case ProductCategory.GiftVouchers:
                    rate = 0;
                    break;

                case ProductCategory.Others:
                    rate = 18;
                    break;

                default:
                    rate = 5;
                    break;
            }
            return rate;
        }
        protected static void BasicRateCalucaltion(decimal mrp, decimal taxRate)
        {
            decimal price = (100 * mrp) / (100 + taxRate);
            decimal taxAmount = mrp - price;
        }
        /// <summary>
        /// return stock info. Add to API/DataModel
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private StockInfo? GetItemDetail(string barcode)
        {
            if (barcode.Length < 10)
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
               TaxRate = SetTaxRate(item.Product.ProductCategory, item.Product.MRP)
           }).FirstOrDefault();
            return item;
        }

    }


    public abstract class Manager
    {
		protected static AzurePayrollDbContext azureDb;
		protected static LocalPayrollDbContext localDb;

		protected abstract void Save();
		protected abstract void Delete();
		protected abstract void Get(string id);
		protected abstract void GetList();

       
    }


}

