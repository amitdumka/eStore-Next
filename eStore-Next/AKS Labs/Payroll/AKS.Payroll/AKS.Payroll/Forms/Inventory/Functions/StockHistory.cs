using AKS.Payroll.Database;
using System.Data;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class StockHistory
    {
        public DateTime OnDate { get; set; }
        public decimal StockIn { get; set; }
        public decimal StockOut { get; set; }
        public decimal StockBalance { get; set; }

        public static List<StockHistory> History(AzurePayrollDbContext db, string barcode)
        {
            var purchase = db.PurchaseItems.Where(c => c.Barcode == barcode).ToList();
            var sale = db.SaleItems.Where(c => c.Barcode == barcode).ToList();
            List<StockHistory> stockHistories = new List<StockHistory>();
            foreach (var item in purchase)
            {
                StockHistory history = new() { StockIn = item.Qty, StockOut = 0, OnDate = db.PurchaseProducts.Where(c => c.InwardNumber == item.InwardNumber).First().OnDate };
                stockHistories.Add(history);
            }
            foreach (var item in sale)
            {
                StockHistory history = new() { StockIn = 0, StockOut = item.BilledQty, OnDate = db.ProductSales.Where(c => c.InvoiceCode == item.InvoiceCode).First().OnDate };
                stockHistories.Add(history);
            }
            stockHistories = stockHistories.OrderBy(c => c.OnDate).ToList();
            decimal bal = 0;
            foreach (var item in stockHistories)
            {
                bal += item.StockIn - item.StockOut;
                item.StockBalance = bal;
            }
            return stockHistories;
        }
        /// <summary>
        /// All Stock Histories
        /// </summary>
        /// <param name="db"></param>
        /// <param name="storecode"></param>
        /// <returns></returns>
        public static SortedDictionary<string, List<StockHistory>> AllStockHistory(AzurePayrollDbContext db, string storecode)
        {
            var barcodeList = db.Stocks.Where(c => c.StoreId == storecode && c.SoldQty > 0 || c.HoldQty > 0).Select(c => c.Barcode).ToList();

            SortedDictionary<string, List<StockHistory>> histories = new SortedDictionary<string, List<StockHistory>>();
            foreach (var item in barcodeList)
            {
                histories.Add(item, History(db, item));
            }
            return histories;
        }
    }
}