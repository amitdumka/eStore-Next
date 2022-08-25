using AKS.Payroll.Database;
using AKS.Payroll.Forms.Inventory.Functions;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public class Inventory
    {
        public static List<PurchaseProduct> GeneratePurchaseInvoice(AzurePayrollDbContext db, DataTable dt)
        {
            var imp = dt.AsEnumerable().Select(c => new
            {
                inw = c["GRNNo"].ToString(),
                ind = DateTime.Parse(c["GRNDate"].ToString()),
                inv = c["Invoice No"].ToString(),
                invd = DateTime.Parse(c["Invoice Date"].ToString()),
                bar = c["Barcode"].ToString(),
                qty = decimal.Parse(c["Quantity"].ToString().Trim()),
                tax = string.IsNullOrEmpty(c["TaxAmt"].ToString().Trim()) ? decimal.Parse("0") : decimal.Parse(c["TaxAmt"].ToString().Trim()),
                costv = decimal.Parse(c["Cost Value"].ToString()),
            }).ToList();

            var x = imp.GroupBy(c => new { c.inv, c.inw, c.ind, c.invd }).
                Select(c => new PurchaseProduct
                {
                    InwardNumber = c.Key.inw,
                    InvoiceNo = c.Key.inv,
                    InwardDate = c.Key.ind,
                    OnDate = c.Key.invd,
                    BillQty = c.Sum(p => p.qty),
                    TotalAmount = c.Sum(p => p.costv) + c.Sum(p => p.tax),
                    TaxAmount = c.Sum(p => p.tax),
                    BasicAmount = c.Sum(p => p.costv),
                    Count = c.Count(),
                    TotalQty = c.Sum(p => p.qty),
                    DiscountAmount = 0,
                    EntryStatus = EntryStatus.Added,
                    FreeQty = 0,
                    InvoiceType = PurchaseInvoiceType.Purchase,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    Paid = false,
                    ShippingCost = 0,// 3 * c.Count(),
                    StoreId = "ARD",
                    TaxType = TaxType.GST,
                    UserId = "Auto",
                    VendorId = "ARD/VIN/0001",
                    Warehouse = "",
                    Items = null
                }).ToList();
            return x;
        }

        public static /*List<PurchaseItem>*/ List<string> ProcessPurchaseItem(AzurePayrollDbContext db, DataTable dt)
        {
            var enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            var sizeList = Enum.GetNames(typeof(Size)).ToList();
            List<string> ErrorList = new List<string>();
            List<PurchaseItem> pp = new List<PurchaseItem>();
            var pItm = db.PurchaseItems.Select(c => new
            {
                c.Barcode,
                c.Qty,
                c.CostPrice,
                c.CostValue,
                c.InwardNumber
            }).ToList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var itm = pItm.Where(c => c.Barcode == dt.Rows[i]["Barcode"].ToString()).FirstOrDefault();
                PurchaseItem item = new PurchaseItem
                {
                    Barcode = dt.Rows[i]["Barcode"].ToString(),
                    CostPrice = Decimal.Round(decimal.Parse(dt.Rows[i]["Cost"].ToString().Trim()), 2),
                    CostValue = decimal.Round(decimal.Parse(dt.Rows[i]["Cost Value"].ToString().Trim()), 2),
                    DiscountValue = 0,
                    FreeQty = 0,
                    InwardNumber = dt.Rows[i]["GRNNo"].ToString().Trim(),
                    Qty = decimal.Round(decimal.Parse(dt.Rows[i]["Quantity"].ToString().Trim()), 2),
                    TaxAmount = string.IsNullOrEmpty(dt.Rows[i]["TaxAmt"].ToString().Trim()) ? 0 : decimal.Round(decimal.Parse(dt.Rows[i]["TaxAmt"].ToString().Trim()), 2),
                    Unit = Unit.NoUnit
                };
                pp.Add(item);

                if (itm != null)
                {
                    if (itm.Qty != item.Qty)
                    {
                        ErrorList.Add($"{item.InwardNumber}/{item.Barcode}=> DB.Qty[{itm.Qty}]!=XL.Qty[{item.Qty}]");
                    }
                    else if (itm.CostPrice != item.CostPrice)
                    {
                        ErrorList.Add($"{item.InwardNumber}/{item.Barcode}=> DB.CostPrice[{itm.CostPrice}]!=XL.CostPrice[{item.CostPrice}]");
                    }
                    else if (itm.CostValue != item.CostValue)
                    {
                        ErrorList.Add($"{item.InwardNumber}/{item.Barcode}=> DB.CostValue[{itm.CostValue}]!=XL.CostValue[{item.CostValue}]");
                    }
                    else
                    {
                        // Duplicate
                    }

                }
                else
                {


                    db.PurchaseItems.Add(item);
                }

            }
            //string jsonString = JsonSerializer.Serialize(db.PurchaseItems.Local.ToList());
            string fn = $"d:\\apr\\purchase\\{DateTime.Now.ToShortDateString()}\\fn.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(fn));
            Utils.ToJsonAsync(fn, pp);

            //if (db.SaveChanges() > 0)
            //    return db.PurchaseItems.Local.ToList();
            //else return null;
            return ErrorList;

        }

        /// <summary>
        /// Seeding vendor
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool SeedBasicVendor(AzurePayrollDbContext db)
        {
            List<Vendor> vendors = new List<Vendor>();

            Vendor v1 = new()
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0001",
                VendorType = VendorType.EBO,
                VendorName = "Arvind Limited"
            };
            Vendor v2 = new Vendor
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0002",
                VendorType = VendorType.EBO,
                VendorName = "Arvind Brands Limited"
            };
            Vendor v3 = new Vendor
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0003",
                VendorType = VendorType.EBO,
                VendorName = "Arvind Lifestyle Brands Limited"
            };
            Vendor v4 = new Vendor
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0004",
                VendorType = VendorType.NonSalable,
                VendorName = "Safari Industries India Ltd"
            };
            Vendor v5 = new Vendor
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0005",
                VendorType = VendorType.NonSalable,
                VendorName = "Khush"
            };
            Vendor v6 = new Vendor
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0006",
                VendorType = VendorType.Distributor,
                VendorName = "Satish Mandal, Dhandbad"
            };
            Vendor v7 = new Vendor
            {
                Active = true,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                OnDate = new DateTime(2015, 11, 1),
                StoreId = "ARD",
                UserId = "AUTO",
                VendorId = "ARD/VIN/0007",
                VendorType = VendorType.InHouse,
                VendorName = "Aprajita Retails, Jamshedpur"
            };
            vendors.Add(v1);
            vendors.Add(v2);
            vendors.Add(v3);
            vendors.Add(v4);
            vendors.Add(v5);
            vendors.Add(v6);
            vendors.Add(v7);

            db.Vendors.AddRange(vendors);
            return (db.SaveChanges() == 7);
        }

        public static void UpDateStockList(AzurePayrollDbContext db, DataGridView lb)
        {
            var list = db.PurchaseItems.Select(c => new Stock
            {
                Barcode = c.Barcode,
                CostPrice = c.CostPrice,
                EntryStatus = EntryStatus.Added,
                HoldQty = 0,
                IsReadOnly = true,
                MarkedDeleted = false,
                MRP = 0,
                MultiPrice = false,
                PurhcaseQty = c.Qty,
                SoldQty = 0,
                StoreId = "ARD",
                Unit = Unit.NoUnit,
                UserId = "Auto"
            }).ToList();

            var bList = list.GroupBy(c => c.Barcode).Select(c => new { c.Key, ctr = c.Count() })
                .Where(c => c.ctr > 1)
                .ToList();
            foreach (var item in bList)
            {
                var pItems = list.Where(c => c.Barcode == item.Key).ToList();
                var itm = pItems[0];
                itm.PurhcaseQty = 0;

                for (int i = 0; i < pItems.Count; i++)
                {
                    list.Remove(pItems[i]);
                    itm.PurhcaseQty += pItems[i].PurhcaseQty;

                    if (itm.MRP != pItems[i].MRP)
                    {
                        if (itm.MRP < pItems[i].MRP)
                            itm.MRP = pItems[i].MRP;
                        //else itm.MRP = pItems[i].MRP;
                        itm.MultiPrice = true;
                    }
                    if (itm.CostPrice != pItems[i].CostPrice)
                    {
                        var cp = (itm.CostPrice * itm.PurhcaseQty) + (pItems[i].CostPrice * pItems[i].PurhcaseQty);
                        itm.CostPrice = Math.Round(cp / itm.PurhcaseQty + pItems[i].PurhcaseQty, 2);
                        itm.MultiPrice = true;
                    }
                }
                list.Add(itm);

            }
            lb.DataSource = list;
            db.Stocks.AddRange(list);
            db.SaveChanges();

        }

        public static List<Stock> UpdateUnit(AzurePayrollDbContext db)
        {
            var stocks = db.Stocks.ToList();
            var pis = db.ProductItems.Select(c => new { c.Barcode, c.Unit }).ToList();
            foreach (var stock in stocks)
            {
                stock.Unit = pis.Where(c => c.Barcode == stock.Barcode).First().Unit;

            }
            db.Stocks.UpdateRange(stocks);
            db.SaveChanges();
            return stocks;
        }

        public static List<List<PurchaseProduct>> ValidatePurchaseInvoice(AzurePayrollDbContext db, List<PurchaseProduct> purchases)
        {
            var dbPur = db.PurchaseProducts.ToList();
            List<PurchaseProduct> missing = new List<PurchaseProduct>();
            List<PurchaseProduct> ok = new List<PurchaseProduct>();
            List<PurchaseProduct> incrt = new List<PurchaseProduct>();

            foreach (var im in purchases)
            {
                var p = dbPur.Where(c => c.InvoiceNo == im.InvoiceNo).First();
                p.BasicAmount = Decimal.Round(p.BasicAmount, 1);
                p.TaxAmount = decimal.Round(p.TaxAmount, 1);
                im.BasicAmount = Decimal.Round(im.BasicAmount, 1);
                im.TaxAmount = decimal.Round(im.TaxAmount, 1);


                if (p != null)
                {
                    if (p.BillQty != im.BillQty)
                    {
                        im.EntryStatus = EntryStatus.Updated;
                        incrt.Add(im);
                    }

                    else if (p.TaxAmount != im.TaxAmount)
                    {
                        im.EntryStatus = EntryStatus.Rejected;
                        p.EntryStatus = EntryStatus.Rejected;
                        incrt.Add(im);
                        incrt.Add(p);
                    }
                    else if (p.BasicAmount != im.BasicAmount)
                    {
                        im.EntryStatus = EntryStatus.DeleteApproved;
                        p.EntryStatus = EntryStatus.DeleteApproved;

                        incrt.Add(im);
                        incrt.Add(p);
                    }
                    //else if (p.ShippingCost != im.ShippingCost) { im.EntryStatus = EntryStatus.Deleted; incrt.Add(im); }
                    else ok.Add(im);
                }
                else
                {
                    missing.Add(im);
                }
            }
            List<List<PurchaseProduct>> x = new List<List<PurchaseProduct>>();
            x.Add(ok);
            x.Add(incrt);
            x.Add(missing);
            return x;
        }

        public static async Task<List<string>> ValidatePurchaseItem(AzurePayrollDbContext db)
        {
            var dbPI = db.PurchaseItems.OrderBy(c => c.InwardNumber).ThenBy(c => c.Barcode).ToList();
            var xlPI = await Utils.FromJson<PurchaseItem>(@"d:\purchaseItem.json");
            xlPI = xlPI.OrderBy(c => c.InwardNumber).ThenBy(c => c.Barcode).ToList();
            List<string> error = new List<string>();

            foreach (var item in dbPI)
            {
                var itm = xlPI.Where(c => c.Barcode == item.Barcode && c.InwardNumber == item.InwardNumber).FirstOrDefault();

                if (itm != null)
                {
                    if (item.Qty != itm.Qty)
                    {
                        error.Add($"#1#{item.InwardNumber}/{item.Barcode}/Qty#{item.Qty}#{itm.Qty}");
                    }
                    if (item.CostPrice != itm.CostPrice)
                    {
                        if ((item.CostPrice - itm.CostPrice) > (decimal)0.02)
                            error.Add($"#2#{item.InwardNumber}/{item.Barcode}/CostPrice#{item.CostPrice}#{itm.CostPrice}");
                    }
                    if (item.CostValue != itm.CostValue)
                    {
                        if ((item.CostValue - itm.CostValue) > (decimal)0.02)
                            error.Add($"#3#{item.InwardNumber}/{item.Barcode}/CostValue#{item.CostValue}#{itm.CostValue}");
                    }
                    if (item.TaxAmount != itm.TaxAmount)
                    {
                        if ((item.TaxAmount - itm.TaxAmount) > (decimal)0.02)
                            error.Add($"#4#{item.InwardNumber}/{item.Barcode}/TaxAmount#{item.TaxAmount}#{itm.TaxAmount}");
                    }
                    xlPI.Remove(itm);
                }
            }
            if (error.Count > 0)
                return error;
            else
            {
                foreach (var item in xlPI)
                {
                    error.Add($"Missing Barcode=>{item.Barcode}/{item.Qty}/{item.CostPrice}/{item.CostValue}");
                }
                db.PurchaseItems.AddRange(xlPI);
                int x = db.SaveChanges();
                return error;
            }


        }

        /// <summary>
        /// Map Vendor from Supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public static string VendorMapping(string supplier)
        {
            string id = supplier switch
            {
                "TAS RMG Warehouse - Bangalore" => "ARD/VIN/0003",
                "TAS - Warhouse -FOFO" => "ARD/VIN/0003",
                "Bangalore WH" => "ARD/VIN/0003",
                "Arvind Brands Limited" => "ARD/VIN/0002",
                "TAS RTS -Warhouse" => "ARD/VIN/0002",
                "Arvind Limited" => "ARD/VIN/0001",
                "Khush" => "ARD/VIN/0005",
                "Safari Industries India Ltd" => "ARD/VIN/0004",
                "DTR Packed WH" => "ARD/VIN/0002",
                "DTR - TAS Warehouse" => "ARD/VIN/0002",
                "Aprajita Retails - Jamshedpur" => "ARD/VIN/0007",
                _ => "ARD/VIN/0002",
            };
            return id;
        }
    }

}