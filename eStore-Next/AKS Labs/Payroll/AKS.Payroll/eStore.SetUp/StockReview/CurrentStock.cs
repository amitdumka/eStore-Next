using AKS.Shared.Commons.Models.Inventory;

namespace eStore.SetUp.StockReview
{
    public class PhyStockHistory
    {
        public string StoreId { get; set; }
        public int PhyStockHistoryId { get; set; }
        public DateTime OnDate { get; set; }
        public string Barcode { get; set; }
        public Category Category { get; set; }
        public decimal Qty { get; set; }
        public string Remark { get; set; }
    }

    public class StockAudit
    {
    }

    public class PurchaseHistory
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal MRP { get; set; }
        public decimal Tax { get; set; }
        public string Location { get; set; }
        public bool IsStockTransfer { get; set; }
    }

    public class SaleHistory
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal SaleValue { get; set; }
        public decimal TaxValue { get; set; }
        public string Location { get; set; }
        public bool IsStockTransfer { get; set; }
        public bool IsManual { get; set; }
    }

    public class ProfitLoss
    {
        public string Barcode { get; set; }
        public decimal Rate { get; set; }
        public decimal PurchaseQty { get; set; }
        public decimal SoldQty { get; set; }
        public decimal StockInHand { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal SaleValue { get; set; }
        public decimal StockValue { get; set; }
        public decimal ProfitAmount { get; set; }
    }
}