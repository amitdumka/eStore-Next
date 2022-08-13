namespace AKS.Payroll.Forms.Inventory
{
    public class MInv
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
}