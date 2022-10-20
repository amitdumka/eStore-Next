namespace eStore.SetUp.Import
{
    public class VoySale
    {
        public string InvoiceType { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime OnDate { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }
        public string StyleCode { get; set; }

        public string Barcode { get; set; }
        public string HSNCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal BasicAmt { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal LineTotal { get; set; }
        public decimal RoundOff { get; set; }
        public decimal BillAmt { get; set; }
        public string PaymentMode { get; set; }
        public string SalesManName { get; set; }
        public string LP { get; set; }
        public string Tailoring { get; set; }
    }

    public class ManualInvoice
    {
        public int SNo { get; set; }
        public DateTime OnDate { get; set; }
        public string InvNo { get; set; }
        public string Barcode { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public string Discount { get; set; }
        public decimal Amount { get; set; }
        public decimal LineTotal { get; set; }
        public decimal BillAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Salesman { get; set; }
    }

    public class MDSale
    {
        public string BARCODE { get; set; }
        public decimal BASICAMOUNT { get; set; }
        public decimal BILLAMOUNT { get; set; }
        public string BRAND { get; set; }
        public string CATEGORY { get; set; }
        public decimal CGSTAMOUNT { get; set; }
        public decimal COUPONAMOUNT { get; set; }
        public string COUPONPERCENTAGE { get; set; }
        public decimal Discountamount { get; set; }
        public string HSNCODE { get; set; }
        public decimal LINETOTAL { get; set; }
        public decimal MRP { get; set; }
        public DateTime OnDate { get; set; }
        public string PAYMENTMODE { get; set; }
        public string Product { get; set; }
        public string Productnumber { get; set; }
        public decimal Quantity { get; set; }
        public string Receiptnumber { get; set; }
        public decimal ROUNDOFFAMT { get; set; }
        public string SALESMAN { get; set; }
        public string SALESTYPE { get; set; }
        public decimal SGSTAMOUNT { get; set; }
        public string STYLECODE { get; set; }
        public string TAILORINGFLAG { get; set; }
        public decimal Taxamount { get; set; }
        public string TranscationNumber { get; set; }
    }

    public class MISale
    {
        public DateTime OnDate { get; set; }
        public string InvNo { get; set; }
        public string InvType { get; set; }
        public string Barcode { get; set; }
        public string HSNCode { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitMRP { get; set; }
        public decimal MRPValue { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal LineTotal { get; set; }
        public string SalesManName { get; set; }

        public string BrandName { get; set; }
        public string Brand { get; set; }

        public decimal UnitCost { get; set; }
        public decimal CostValue { get; set; }

        public string ItemDesc { get; set; }

        public string Size { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ProductType { get; set; }
        public string StyleCode { get; set; }
    }

    public class JsonSale
    {
        public string InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal BasicRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGST { get; set; }
        public decimal RoundOff { get; set; }
        public decimal LineTotal { get; set; }
        public decimal BillAmount { get; set; }
        public string PaymentMode { get; set; }
        public string SalesmanName { get; set; }

        public string Brand { get; set; }
        public string BrandName { get; set; }
        public string ItemDesc { get; set; }
        public string ProductName { get; set; }
        public string SytleCode { get; set; }
        public string HSNCODE { get; set; }
        public string LP { get; set; }
        public string Tailoring { get; set; }

        public decimal UnitMRP { get; set; }
        public decimal MRPValue { get; set; }

        public decimal UnitCost { get; set; }
        public decimal CostValue { get; set; }

        public string Size { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ProductType { get; set; }
    }

    public class VoyPurhcase
    {
        public string GRNNo { get; set; }
        public DateTime GRNDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SupplierName { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string StyleCode { get; set; }
        public string ItemDesc { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal MRPValue { get; set; }
        public decimal Cost { get; set; }
        public decimal CostValue { get; set; }
        public decimal TaxAmt { get; set; }
    }
}