namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SaleReport
    {
        public decimal BillQty { get; set; }
        public decimal FreeQty { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int Month { get; set; }
        public bool Tailoing { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalMRP { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalTax { get; set; }
        public int Year { get; set; }
    }


}