namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SVM
    {
        //InvoiceNo	InvoiceDate	InvoiceType	BrandName	ProductName	ItemDesc	HSNCode	BARCODE
        //	StyleCode	Quantity	MRP	DiscountAmt	BasicAmt	TaxAmt	SGSTAmt	CGSTAmt	
        //	CESSAmt	LineTotal	RoundOff	BillAmt	PaymentMode	SalesManName	
        //	Coupon	CouponAmt	SUBTYPE	BillDiscount	LPFlag	InstOrderCD	TAILORINGFLAG

        public string InvType { get; set; }
        public string InvNo { get; set; }
        public DateTime OnDate { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public string HSNCode { get; set; }
        public decimal Qty { get; set; }
        public decimal MRP { get; set; }
        public decimal disc { get; set; }
        public decimal basic { get; set; }
        public decimal Tax { get; set; }
        public decimal SAmt { get; set; }
        public decimal CAmt { get; set; }
        public decimal LineTotal { get; set; }
        public decimal RoundOff { get; set; }
        public decimal BillAmt { get; set; }
        public string PaymentMode { get; set; }
        public string SalesManName { get; set; }
        public string LP { get; set; }
        public string Tailoring { get; set; }

    }
}