namespace AKS.Payroll.Forms.Inventory
{
    public class SaleItemVM
    {
        public decimal Amount { get; set; }
        public string Barcode { get; set; }
        public decimal Discount { get; set; }
        public string ProductItem { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Tax { get; set; }
    }
}