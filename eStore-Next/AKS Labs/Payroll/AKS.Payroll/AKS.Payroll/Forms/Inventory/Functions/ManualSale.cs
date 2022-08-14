using AKS.Payroll.Database;
using AKS.Payroll.Forms.Inventory.Functions;
using AKS.Payroll.Ops;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public class ManualSale
    {
        private static DataGridView gridView;
        private static DataTable dt;

        public static bool IsInt(decimal x)
        {
            return x % 1 == 0;
        }

        public static async void ProcessManualImport(AzurePayrollDbContext db, DataGridView dv)
        {
            try
            {
                gridView = dv;
                string exfile = @"d:\manual.xlsx";
                dt = ImportData.ReadExcelToDatatable(exfile, 1, 1, 355, 11);
                dv.DataSource = dt;
                var mivs = await UploadManual(db, dt);
                dv.DataSource = mivs;
                SaveManual(db, mivs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //SN	Date	Inv No	BarCode	Qty	Rate	Discount	 Amount	Bill Amount	Total Amount	Sales Man

        public static decimal GetDiscountAmount(string dis, decimal amount)
        {
            decimal d = decimal.Parse(dis.Replace("%", "").Trim());
            var val = amount - (amount * (d / 100));
            return val;
        }

        public static decimal GetDiscountValue(string dis, decimal amount)
        {
            decimal d = decimal.Parse(dis.Replace("%", "").Trim());
            var val = (amount * (d / 100));
            return val;
        }

        public static async Task<List<MInv>> UploadManual(AzurePayrollDbContext db, DataTable dt)
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
                    inv.LineTotal = GetDiscountAmount(inv.Discount, inv.Amount);
                    LastInv = inv.InvNo;
                    LastInvDate = inv.OnDate;
                    LastSN = inv.SNo;
                    LastSM = inv.Salesman;
                    list.Add(inv);
                }
            }
            Directory.CreateDirectory(@"d:\apr2\ManualInv\");
            await Utils.ToJsonAsync(@"d:\apr2\ManualInv\manual.json", list);
            return list;
        }
        public static async void SaveManual(AzurePayrollDbContext db, List<MInv> invList)
        {
            List<SaleItem> saleList = new List<SaleItem>();
            List<ProductSale> productList = new List<ProductSale>();

            var pL = invList.Where(c => c.TotalAmount > 0).ToList();
            gridView.DataSource = pL;

            var plQ = invList.GroupBy(c => new { c.InvNo })
                .Select(c => new { c.Key.InvNo, TQty = c.Sum(x => x.Qty), TAmt = c.Sum(x => x.Amount) }).ToList();

            var sms = db.Salesmen.Select(c => new { c.SalesmanId, c.Name }).ToList();
            try
            {
                foreach (var inv in pL)
                {
                    ProductSale productSale = new()
                    {
                        InvoiceCode = $"ARD/{inv.OnDate.Year}/{inv.OnDate.Month}/{inv.SNo}",
                        InvoiceNo = $"ARD/{inv.OnDate.Year}/{inv.OnDate.Month}/{inv.InvNo}",
                        TotalBasicAmount = 0,
                        TotalTaxAmount = 0,
                        TotalDiscountAmount = GetDiscountValue(inv.Discount, inv.Amount),
                        TotalMRP = plQ.Where(c => c.InvNo == inv.InvNo).First().TAmt,
                        TotalPrice = inv.TotalAmount,
                        Paid = false,
                        MarkedDeleted = true,
                        IsReadOnly = false,
                        RoundOff = inv.TotalAmount - inv.BillAmount,
                        StoreId = "ARD",
                        Taxed = false,
                        Adjusted = false,
                        EntryStatus = EntryStatus.Added,
                        FreeQty = 0,
                        BilledQty = plQ.Where(c => c.InvNo == inv.InvNo).First().TQty,
                        InvoiceType = InvoiceType.ManualSale,
                        OnDate = inv.OnDate,
                        Tailoring = false,
                        UserId = "Auto",
                    };
                    if (string.IsNullOrEmpty(inv.Salesman))
                    {
                        productSale.SalesmanId = "SMN/2016/001";
                    }
                    else if (inv.Salesman.Contains("Claim"))
                    {
                        productSale.SalesmanId = "SMN/2016/001";
                    }
                    else
                        productSale.SalesmanId = sms.First(c => c.Name.Contains(inv.Salesman)).SalesmanId;

                    productList.Add(productSale);
                }
                gridView.DataSource = productList;



                foreach (var inv in invList)
                {
                    if (inv.Barcode != "CANCLE INV")
                    {
                        SaleItem si = new()
                        {

                            Adjusted = false,
                            Barcode = inv.Barcode,
                            BilledQty = inv.Qty,
                            FreeQty = 0,
                            LastPcs = false,
                            Unit = Unit.NoUnit,
                            DiscountAmount = GetDiscountValue(inv.Discount, inv.Amount),
                            Value = inv.LineTotal,
                            TaxAmount = 0,
                            InvoiceType = InvoiceType.ManualSale,
                            TaxType = inv.OnDate < new DateTime(2017, 7, 1) ? TaxType.VAT : TaxType.GST,
                        };

                        si.InvoiceCode = productList.First(c => c.InvoiceNo.EndsWith(inv.InvNo)).InvoiceCode;
                        if (si.TaxType == TaxType.VAT && IsInt(inv.Qty))
                        {
                            // 5% vat
                            si.BasicAmount = GetBasicRate(inv.LineTotal, 5);
                            si.TaxAmount = inv.LineTotal - si.BasicAmount;
                            si.Unit = Unit.Pcs;
                        }
                        else if (si.TaxType == TaxType.VAT && !IsInt(inv.Qty))
                        {
                            // no tax
                            si.BasicAmount = inv.LineTotal;
                            si.Unit = Unit.Meters;
                        }
                        else if (si.TaxType == TaxType.GST && IsInt(inv.Qty))
                        {
                            //5% tax
                            //si.BasicAmount = (inv.LineTotal * 100) / (100 + 5);
                            si.BasicAmount = GetBasicRate(inv.LineTotal, 5);
                            si.TaxAmount = inv.LineTotal - si.BasicAmount;
                            si.Unit = Unit.Meters;
                        }
                        else if (si.TaxType == TaxType.GST && !IsInt(inv.Qty))
                        {
                            //5%  below 1000 and 12% above
                            if (inv.LineTotal < 1000)
                                si.BasicAmount = GetBasicRate(inv.LineTotal, 5);
                            else
                                si.BasicAmount = GetBasicRate(inv.LineTotal, 12);
                            si.TaxAmount = inv.LineTotal - si.BasicAmount;
                            si.Unit = Unit.Pcs;
                        }
                        saleList.Add(si);
                    }
                    else
                    {
                        Console.WriteLine(inv.Barcode);
                    }
                }
                gridView.DataSource = saleList;

                await Utils.ToJsonAsync(@"D:\apr2\ManualInv\ManualSaleItem.json", saleList); 
                await Utils.ToJsonAsync(@"D:\apr2\ManualInv\ManualSaleInvoice.json", productList);
                
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public static decimal GetBasicRate(decimal amount, decimal rate)
        {
            return Math.Round((amount * 100) / (100 + rate), 2);
        }
    }
}