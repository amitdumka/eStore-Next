using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public class ManualSale
    {
        //SN	Date	Inv No	BarCode	Qty	Rate	Discount	 Amount	Bill Amount	Total Amount	Sales Man

        public static decimal GetDiscount(string dis, decimal amount)
        {
            decimal d = decimal.Parse(dis.Replace("%", "").Trim());
            var val = amount - (amount * (d / 100));
            return val;
        }

        public static void SaveManual(AzurePayrollDbContext db, List<MInv> invList)
        {
            List<SaleItem> saleList = new List<SaleItem>();
            List<ProductSale> productList = new List<ProductSale>();

            foreach (var inv in invList)
            {
                ProductSale productSale = new()
                {
                    InvoiceCode = $"ARD/{inv.OnDate.Year}/{inv.OnDate.Month}/{inv.SNo}",
                    InvoiceNo = $"ARD/{inv.OnDate.Year}/{inv.OnDate.Month}/{inv.InvNo}",

                    TotalBasicAmount = 0,
                    TotalTaxAmount = 0,

                    TotalDiscountAmount = GetDiscount(inv.Discount, inv.Amount),

                    TotalMRP = inv.Amount,
                    TotalPrice = inv.TotalAmount,

                    Paid = false,
                    MarkedDeleted = false,
                    IsReadOnly = false,
                    RoundOff = 0,
                    StoreId = "ARD",
                    Taxed = true,
                    Adjusted = false,
                    EntryStatus = EntryStatus.Added,
                    FreeQty = 0,
                    BilledQty = inv.Qty,
                    InvoiceType = InvoiceType.ManualSale,
                    OnDate = inv.OnDate,
                    Tailoring = false,
                    UserId = "Auto",
                };
                SaleItem si = new()
                {
                    InvoiceCode = productSale.InvoiceCode,
                    Adjusted = false,
                    Barcode = inv.Barcode,
                    BilledQty = inv.Qty,
                    FreeQty = 0,
                    LastPcs = false,
                    Unit = Unit.NoUnit,
                    DiscountAmount = GetDiscount(inv.Discount, inv.Amount),
                    Value = inv.LineTotal,
                    TaxAmount = 0,
                };
            }
        }

        public static List<MInv> UploadManual(AzurePayrollDbContext db, DataTable dt)
        {
            List<MInv> list = new List<MInv>();
            int LastSN = 0;
            string LastInv = "";
            DateTime LastInvDate = DateTime.Now;
            string LastSM = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 105)
                {
                    Console.WriteLine(i.ToString());
                }
                if (dt.Rows[i]["Barcode"].ToString().Contains("Cancel"))
                {
                    //Add For Cancel
                    MInv inv = new()
                    {
                        Amount = 0,
                        Barcode = "CANCLE INV",
                        BillAmount = 0,
                        Discount = "0",
                        InvNo = dt.Rows[i]["Inv No"].ToString(),
                        OnDate = Utils.ToDate(dt.Rows[i]["Date"].ToString()),
                        Qty = 0,
                        Rate = 0,
                        Salesman = "",
                        SNo = string.IsNullOrEmpty(dt.Rows[i]["SN"].ToString()) ? LastSN : Int32.Parse(dt.Rows[i]["SN"].ToString()),
                        TotalAmount = 0,
                        LineTotal = 0
                    };
                    list.Add(inv);
                }
                else
                {
                    MInv inv = new()
                    {
                        //Amount = decimal.Parse(dt.Rows[i]["Amount"].ToString().Trim()),
                        Barcode = dt.Rows[i]["BarCode"].ToString(),
                        InvNo = string.IsNullOrEmpty(dt.Rows[i]["Inv No"].ToString()) ? LastInv : dt.Rows[i]["Inv No"].ToString(),
                        LineTotal = 0,
                        SNo = string.IsNullOrEmpty(dt.Rows[i]["SN"].ToString()) ? LastSN : Int32.Parse(dt.Rows[i]["SN"].ToString()),
                        Salesman = string.IsNullOrEmpty(dt.Rows[i]["Sales Man"].ToString()) ? LastSM : dt.Rows[i]["Sales Man"].ToString(),
                    };
                    inv.OnDate = string.IsNullOrEmpty(dt.Rows[i]["Date"].ToString()) ? LastInvDate : Utils.ToDate(dt.Rows[i]["Date"].ToString());
                    inv.Discount = String.IsNullOrEmpty(dt.Rows[i]["Discount"].ToString()) ? 0 + "" : (100 * decimal.Parse(dt.Rows[i]["Discount"].ToString())) + "";
                    inv.Qty = decimal.Parse(dt.Rows[i]["Qty"].ToString().Trim());
                    inv.Rate = decimal.Parse(dt.Rows[i]["Rate"].ToString());
                    inv.TotalAmount = string.IsNullOrEmpty(dt.Rows[i]["Total Amount"].ToString()) ? 0 : decimal.Parse(dt.Rows[i]["Total Amount"].ToString());

                    inv.BillAmount = string.IsNullOrEmpty(dt.Rows[i]["Bill Amount"].ToString()) ? 0 : decimal.Parse(dt.Rows[i]["Bill Amount"].ToString());
                    inv.Amount = inv.Qty * inv.Rate;
                    inv.LineTotal = inv.Amount - GetDiscount(inv.Discount, inv.Amount);
                    LastInv = inv.InvNo;
                    LastInvDate = inv.OnDate;
                    LastSN = inv.SNo;
                    LastSM = inv.Salesman;
                    list.Add(inv);
                }
            }
            Directory.CreateDirectory(@"d:\apr2\ManualInv\");
            _ = Utils.ToJsonAsync(@"d:\arp\ManualInv\manual.json", list);
            return list;
        }
    }
}