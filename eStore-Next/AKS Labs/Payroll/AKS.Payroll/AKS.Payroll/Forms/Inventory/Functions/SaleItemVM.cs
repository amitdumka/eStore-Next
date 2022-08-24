namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SaleItemVM
    {
        public string Barcode { get; set; }
        public string ProductItem { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }        
    }
    public class PaymentDetail
    {
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public PayMode Mode { get; set; }
        public string RefNumber { get; set; }
        public Card? Card { get; set; }
        public CardType? CardType { get; set; }
        public string AuthCode { get; set; }
        public int LastFour { get; set; }
        public string? PosMachineId { get; set; }
    }
    public class StockInfo
    {
        public string Barcode { get; set; }
        public ProductCategory Category { get; set; }
        public decimal HoldQty { get; set; }
        public string ProductItem { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxRate { get; set; }
        public TaxType TaxType { get; set; }
        public Unit Unit { get; set; }
    }
}