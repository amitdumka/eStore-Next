using AKS.Payroll.Database;
using AKS.Payroll.Ops;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class MDSale
    {
        //Date	Transaction number	Receipt number	SALESTYPE	SALESMAN	BRAND	CATEGORY
        //Product number	Product	HSNCODE	BARCODE	STYLECODE	Quantity	MRP	Discount amount
        //BASICAMOUNT	Tax amount	SGSTAMOUNT	CGSTAMOUNT	CESSAMOUNT	CHARGES	LINETOTAL
        //ROUNDOFFAMT	BILLAMOUNT	PAYMENTMODE	COUPONPERCENTAGE	COUPONAMOUNT	TAILORINGFLAG

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
        //Barcode	BasicAmount	BillAmount	
        //BILL_TYPE	BRAND	CostValue	BrandName	DiscAmt	
        //Category	SubCate	ProductTye	HSNCODE	InvNo	
        //MRPValue	Salesman	PRINCIPALCODE	QTY	SIZE	DESCRIPTION	GSTAmt	
        //TotalTaxRate	TotalTaxAmt	Date	UnitCost	UnitMRP

        public DateTime OnDate { get; set; }
        public string InvNo { get; set; }
        public string InvType { get; set; }
        public string Barcode { get; set; }//Barcode
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
    public class MSSaleInventory
    {
        private static DataTable dtS;
        private static DataGridView gridview;
        public static void ProcessLocalDataTableToObject(AzurePayrollDbContext db)
        {
            List<MDSale> saleList = new List<MDSale>();

            List<Stock> pxStocks = new List<Stock>();
            int pps = 0;
            int ppp = 0;
            for (int i = 309; i < 348; i++)
            {
                string item = "JHC000601IN0000" + i;
                var p = db.ProductSales.Where(c => c.InvoiceNo == item).FirstOrDefault();
                if (p != null)
                {
                    var sales = db.SaleItems.Where(c => c.InvoiceCode == p.InvoiceCode).ToList();

                    foreach (var ios in sales)
                    {
                        var stock = db.Stocks.FirstOrDefault(c => c.Barcode == ios.Barcode);
                        stock.SoldQty -= ios.BilledQty;
                        db.Stocks.Update(stock);
                        pps++;
                    }
                    db.SaleItems.RemoveRange(sales);
                    db.ProductSales.Remove(p);
                    ppp++;
                }
            }
            int zz = db.SaveChanges();

            var smList = db.Salesmen.Select(c => new { c.SalesmanId, c.Name }).ToList();
            for (int i = 0; i < dtS.Rows.Count; i++)
            {
                MDSale sale = new MDSale
                {
                    BARCODE = dtS.Rows[i]["BARCODE"].ToString(),
                    BASICAMOUNT = decimal.Parse(dtS.Rows[i]["BASICAMOUNT"].ToString()),
                    BILLAMOUNT = Math.Round(decimal.Parse(dtS.Rows[i]["BILLAMOUNT"].ToString()),2),
                    BRAND = dtS.Rows[i]["BRAND"].ToString(),
                    CATEGORY = dtS.Rows[i]["CATEGORY"].ToString(),
                    CGSTAMOUNT = Math.Round(decimal.Parse(dtS.Rows[i]["CGSTAMOUNT"].ToString()),2),
                    Discountamount = Math.Round(decimal.Parse(dtS.Rows[i]["Discount amount"].ToString()),2),
                    HSNCODE = dtS.Rows[i]["HSNCODE"].ToString(),
                    COUPONAMOUNT = 0,
                    COUPONPERCENTAGE = "",
                    LINETOTAL = Math.Round(decimal.Parse(dtS.Rows[i]["LINETOTAL"].ToString()),2),
                    MRP = Math.Round(decimal.Parse(dtS.Rows[i]["MRP"].ToString())),
                    OnDate = ToDateDMY(dtS.Rows[i]["Date"].ToString()),
                    PAYMENTMODE = dtS.Rows[i]["PAYMENTMODE"].ToString(),
                    Product = dtS.Rows[i]["Product"].ToString(),
                    Productnumber = dtS.Rows[i]["Product number"].ToString(),
                    Quantity = Math.Round(decimal.Parse(dtS.Rows[i]["Quantity"].ToString()),2),
                    Receiptnumber = dtS.Rows[i]["Receipt number"].ToString(),
                    ROUNDOFFAMT = Math.Round(decimal.Parse(dtS.Rows[i]["ROUNDOFFAMT"].ToString()),2),
                    SALESMAN = dtS.Rows[i]["SALESMAN"].ToString(),
                    SALESTYPE = dtS.Rows[i]["SALESTYPE"].ToString(),
                    SGSTAMOUNT = Math.Round(decimal.Parse(dtS.Rows[i]["SGSTAMOUNT"].ToString()),2),
                    STYLECODE = dtS.Rows[i]["STYLECODE"].ToString(),
                    TAILORINGFLAG = dtS.Rows[i]["TAILORINGFLAG"].ToString(),
                    Taxamount = Math.Round(decimal.Parse(dtS.Rows[i]["Tax amount"].ToString()),2),
                    TranscationNumber = dtS.Rows[i]["Transaction number"].ToString(),
                };

                saleList.Add(sale);
            }
            gridview.DataSource = saleList;

            //Generate ProdSale
            var pSaleList = saleList.Where(c => c.BILLAMOUNT > 0).ToList();
            List<ProductSale> products = new List<ProductSale>();
            List<SaleItem> saleItems = new List<SaleItem>();
            List<string> missBarcode = new List<string>();
            //foreach (var item in pSaleList)
            //{
            //    var stock = db.ProductItems.FirstOrDefault(c => c.Barcode == item.BARCODE);
            //    if (stock == null)
            //        missBarcode.Add(item.BARCODE);

            //}

            //gridview.DataSource=pSaleList;

            int count = 0;
            List<ProductItem> PList = new List<ProductItem>();
            foreach (var item in pSaleList)
            {
                ProductSale pSale = new ProductSale()
                {
                    OnDate = item.OnDate,
                    Paid = true,
                    RoundOff = item.ROUNDOFFAMT,
                    StoreId = "ARD",
                    Tailoring = false,
                    Taxed = true,
                    TotalBasicAmount = 0,
                    UserId = "Auto",
                    TotalDiscountAmount = 0,
                    TotalMRP = 0,
                    TotalTaxAmount = 0,
                    TotalPrice = item.BILLAMOUNT,
                    Adjusted = false,
                    BilledQty = 0,
                    EntryStatus = EntryStatus.Added,
                    FreeQty = 0,
                    InvoiceNo = item.Receiptnumber,
                    InvoiceType = InvoiceType.Sales,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    SalesmanId = smList.First(c => c.Name.Contains(item.SALESMAN)).SalesmanId,
                    InvoiceCode = $"ARD/{item.OnDate.Year}/{item.OnDate.Month}/IN/{SaleUtils.INCode(++count)}",
                };

                products.Add(pSale);
                pSale.Items = new List<SaleItem>();
                var invs = saleList.Where(c => c.Receiptnumber == pSale.InvoiceNo).ToList();
                foreach (var im in invs)
                {
                    SaleItem saleItem = new SaleItem()
                    {
                        Adjusted = false,
                        Barcode = im.BARCODE,
                        BilledQty =Math.Round( im.Quantity,2),
                        DiscountAmount = Math.Round(im.Discountamount,2),
                        FreeQty = 0,
                        InvoiceCode = pSale.InvoiceCode,
                        InvoiceType = InvoiceType.Sales,
                        LastPcs = false,
                        TaxType = TaxType.GST,
                        Unit = Unit.NoUnit,
                        Value =Math.Round( im.LINETOTAL,2),
                        TaxAmount = Math.Round(im.Taxamount,2),
                        BasicAmount =Math.Round( im.BASICAMOUNT,2)
                    };
                    //pSale.Items = new List<SaleItem>();
                    pSale.Items.Add(saleItem);
                    pSale.BilledQty +=Math.Round( saleItem.BilledQty,2);
                    pSale.TotalDiscountAmount += saleItem.DiscountAmount;
                    pSale.TotalMRP += Math.Round(im.MRP,2);
                    pSale.TotalBasicAmount += Math.Round(im.BASICAMOUNT,2);
                    pSale.TotalTaxAmount += Math.Round(im.Taxamount,2);

                    var stock = db.Stocks.FirstOrDefault(c => c.Barcode == saleItem.Barcode);
                    if (stock != null)
                    {
                        saleItem.Unit = stock.Unit;
                        stock.SoldQty = Math.Round(saleItem.BilledQty,2);
                        db.Stocks.Update(stock);
                    }
                    else
                    {
                        if (missBarcode.Exists(x => x == saleItem.Barcode))
                        {
                            var ss = pxStocks.First(c => c.Barcode == saleItem.Barcode);
                            pxStocks.Remove(ss);
                            ss.SoldQty += Math.Round(saleItem.BilledQty,2);
                            pxStocks.Add(ss);
                        }
                        else
                        {
                            missBarcode.Add(saleItem.Barcode);

                            Stock newStock = new Stock
                            {
                                Barcode = saleItem.Barcode,
                                EntryStatus = EntryStatus.Added,
                                HoldQty = 0,
                                SoldQty = saleItem.BilledQty,
                                IsReadOnly = false,
                                CostPrice = 0,
                                MRP = item.MRP,
                                MarkedDeleted = true,
                                MultiPrice = false,
                                StoreId = "ARD",
                                UserId = "Auto",
                                PurhcaseQty = 0,
                                Unit = saleItem.Unit,
                            };

                            var pp = db.ProductItems.Where(c => c.Barcode == saleItem.Barcode).FirstOrDefault();
                            if (pp == null)
                            {
                                ProductItem pi = new ProductItem
                                {
                                    Unit = saleItem.Unit,
                                    HSNCode = im.HSNCODE,
                                    MRP = im.MRP,
                                    Barcode = saleItem.Barcode,
                                    Name = im.Product,
                                    Size = Size.NS,
                                    TaxType = TaxType.GST,
                                    StyleCode = im.STYLECODE,
                                    BrandCode = "ARD",
                                    Description = im.CATEGORY,
                                    ProductCategory = ProductCategory.Fabric,
                                };
                                var p = im.CATEGORY.Split("/").Distinct().ToList();
                                foreach (var k in p)
                                {
                                    var x = db.ProductSubCategories.Find(k);
                                    if (x != null)
                                    {
                                        pi.SubCategory = x.SubCategory;
                                        pi.ProductCategory = (ProductCategory)x.ProductCategory;
                                        break;
                                    }
                                }
                                //db.ProductItems.Add(pi);
                                PList.Add(pi);
                            }

                            pxStocks.Add(newStock);
                        }
                    }
                    //saleItem.Unit = stock.Unit;
                    saleItems.Add(saleItem);
                }
            }

            gridview.DataSource = saleItems;
            db.ProductItems.AddRange(PList);
            db.Stocks.AddRange(pxStocks);
            db.ProductSales.AddRange(products);
            int s = db.SaveChanges();
            Console.WriteLine(PList.Count + "");
            Console.WriteLine("Saved " + s);
        }

        public static void ProcessWebDataTable(AzurePayrollDbContext db)
        {
            List<MISale> mISales = new List<MISale>();
            List<Stock> stockList = new List<Stock>();
            List<ProductItem> productItemList = new List<ProductItem>();
            List<ProductSale> productSales = new List<ProductSale>();
            var sizeList = Enum.GetNames(typeof(Size)).ToList();

            for (int i = 0; i < dtS.Rows.Count; i++)
            {
                MISale sale = new MISale
                {
                    Barcode = dtS.Rows[i]["Barcode"].ToString(),
                    BasicAmount = decimal.Parse(dtS.Rows[i]["BasicAmount"].ToString()),
                    LineTotal = decimal.Parse(dtS.Rows[i]["BillAmount"].ToString()),
                    DiscAmt = decimal.Parse(dtS.Rows[i]["DiscAmt"].ToString()),
                    BrandName = dtS.Rows[i]["BrandName"].ToString(),
                    Brand = dtS.Rows[i]["BRAND"].ToString(),
                    HSNCode = dtS.Rows[i]["HSNCODE"].ToString(),
                    InvNo = dtS.Rows[i]["InvNo"].ToString(),
                    InvType = dtS.Rows[i]["BILL_TYPE"].ToString(),
                    GSTAmount = decimal.Parse(dtS.Rows[i]["GSTAmt"].ToString()),
                    MRPValue = decimal.Parse(dtS.Rows[i]["MRPValue"].ToString()),
                    CostValue = decimal.Parse(dtS.Rows[i]["CostValue"].ToString()),
                    OnDate = DateTime.Parse(dtS.Rows[i]["Date"].ToString()),
                    Qty = decimal.Parse(dtS.Rows[i]["Qty"].ToString()),
                    TaxRate = decimal.Parse(dtS.Rows[i]["TotalTaxRate"].ToString()),
                    TotalTaxAmount = decimal.Parse(dtS.Rows[i]["TotalTaxAmt"].ToString()),
                    UnitCost = decimal.Parse(dtS.Rows[i]["UnitCost"].ToString()),
                    UnitMRP = decimal.Parse(dtS.Rows[i]["UnitMRP"].ToString()),
                    ItemDesc = dtS.Rows[i]["DESCRIPTION"].ToString(),
                    SalesManName = dtS.Rows[i]["Salesman"].ToString(),
                    Size = dtS.Rows[i]["Size"].ToString(),
                    Category = dtS.Rows[i]["Category"].ToString(),
                    SubCategory = dtS.Rows[i]["SubCate"].ToString(),
                    ProductType = dtS.Rows[i]["ProductType"].ToString(),
                    StyleCode = dtS.Rows[i]["PRINCIPALCODE"].ToString(),
                };
                mISales.Add(sale);
            }
            var plist = dtS.AsEnumerable().GroupBy(c => c["ProductType"]).Select(c => c.Key.ToString()).ToList();

            var invNoList = mISales.Select(c => c.InvNo).Distinct().ToList();
            int ic = 0;
            var productTypes = db.ProductTypes.ToList();

            //foreach (var item in productTypes)
            //{
             //   plist.Remove(item.ProductTypeName);
            //}
            //var ctr = productTypes.Count + 1;
            //foreach (var item in plist)
            //{
             //   var pts = new ProductType
              //  {
               //     ProductTypeId = $"PT000" + ctr,
                //    ProductTypeName = item
               // };
               // ctr++;
               // productTypes.Add(pts);
               // db.ProductTypes.Add(pts);
            //}
           // int polp = db.SaveChanges();
            gridview.DataSource = mISales;
             

            foreach (var inv in invNoList)
            {
                var sales = mISales.Where(c => c.InvNo == inv).ToList();
                ProductSale pSale = new ProductSale
                {
                    InvoiceNo = inv,
                    OnDate = sales[0].OnDate,
                    MarkedDeleted = false,
                    Paid = true,
                    SalesmanId = Saleid(sales[0].SalesManName),
                    Adjusted = false,
                    StoreId = "ARD",
                    Taxed = true,
                    Tailoring = false,
                    RoundOff = 0,
                    TotalBasicAmount = 0,
                    BilledQty = 0,
                    TotalDiscountAmount = 0,
                    TotalMRP = 0,
                    TotalTaxAmount = 0,
                    UserId = "Auto",
                    TotalPrice = 0,
                    EntryStatus = EntryStatus.Added,
                    FreeQty = 0,
                    InvoiceType = sales[0].InvType == "Sales" ? InvoiceType.Sales : InvoiceType.SalesReturn,
                    IsReadOnly = true,
                    InvoiceCode = $"ARD/{sales[0].OnDate.Year}/{sales[0].OnDate.Month}/IN/{SaleUtils.INCode(++ic)}",
                    Items = new List<SaleItem>()
                };
                foreach (var sale in sales)
                {
                    SaleItem si = new SaleItem
                    {
                        DiscountAmount = sale.DiscAmt,
                        FreeQty = 0,
                        LastPcs = false,
                        TaxType = TaxType.GST,
                        Unit = Unit.NoUnit,
                        TaxAmount = sale.TotalTaxAmount,
                        Value = sale.LineTotal,
                        InvoiceCode = pSale.InvoiceCode,
                        InvoiceType = pSale.InvoiceType,
                        Adjusted = false,
                        Barcode = sale.Barcode,
                        BasicAmount = sale.BasicAmount,
                        BilledQty = sale.Qty,
                    };
                    pSale.Items.Add(si);
                    pSale.TotalTaxAmount += si.TaxAmount;
                    pSale.TotalMRP += sale.MRPValue;
                    pSale.TotalPrice += si.Value;
                    pSale.TotalBasicAmount += si.BasicAmount;
                    pSale.BilledQty += si.BilledQty;
                    pSale.TotalDiscountAmount += si.DiscountAmount;

                    var stock = db.Stocks.Where(c => c.Barcode == si.Barcode).FirstOrDefault();
                    if (stock != null)
                    {
                        stock.SoldQty += si.BilledQty;
                        db.Stocks.Update(stock);
                    }
                    else
                    {
                        stock = stockList.Where(c => c.Barcode == si.Barcode).FirstOrDefault();
                        if (stock != null)
                        {
                            stockList.Remove(stock);
                            stock.SoldQty += si.BilledQty;
                            stockList.Add(stock);
                        }
                        else
                        {
                            // Create Stock;
                            var newstock = new Stock
                            {
                                SoldQty = si.BilledQty,
                                Barcode = si.Barcode,
                                CostPrice = sale.UnitCost,
                                EntryStatus = EntryStatus.Added,
                                HoldQty = 0,
                                IsReadOnly = false,
                                MarkedDeleted = true,
                                MRP = sale.UnitMRP,
                                MultiPrice = false,
                                PurhcaseQty = 0,
                                StoreId = "ARD",
                                UserId = "Auto",
                                Unit = Unit.NoUnit
                            };
                            var newPI = new ProductItem
                            {
                                Unit = Unit.NoUnit,
                                Barcode = si.Barcode,
                                HSNCode = sale.HSNCode,
                                MRP = sale.UnitMRP,
                                Size = Size.NS,
                                TaxType = TaxType.GST,
                                StyleCode = sale.StyleCode,
                                BrandCode = sale.Brand,
                                Description = sale.ItemDesc,
                                SubCategory = sale.SubCategory+" "+sale.ProductType,
                                Name=sale.Category+"/"+sale.SubCategory+"/"+sale.ProductType,

                                ProductTypeId = ""
                            };

                            var pt = productTypes.Where(c => c.ProductTypeName == sale.ProductType).FirstOrDefault();
                            if (pt != null)
                            { newPI.ProductTypeId = pt.ProductTypeId; }

                            switch (sale.Category)
                            {
                                case "Shirting":
                                case "Suiting":
                                    newPI.Size = Size.NS;
                                    newPI.Unit = Unit.Meters;
                                    newstock.Unit = Unit.Meters;
                                    break;
                                case "Apparel":
                                    newstock.Unit = Unit.Pcs;
                                    newPI.Unit = Unit.Pcs;
                                    newPI.Size = (Size)GetSize(sale.Size);
                                    break;
                                case "Tailoring":
                                    newPI.Size = Size.NS;
                                    newPI.Unit = Unit.Nos;
                                    newstock.Unit = Unit.Nos;
                                    pSale.Tailoring = true;
                                    break;
                                default:
                                    newPI.Size = Size.NS;
                                    newPI.Unit = Unit.Meters;
                                    break;
                            }
                            if(newstock.Unit!=Unit.Nos)
                                stockList.Add(newstock);
                            productItemList.Add(newPI);
                        }

                    }
                }
                pSale.RoundOff = (Math.Round(pSale.TotalPrice, 0) - pSale.TotalPrice);
                pSale.TotalPrice += pSale.RoundOff;
                productSales.Add(pSale);
            }
            gridview.DataSource = productSales;
            db.ProductItems.AddRange(productItemList);
            db.ProductSales.AddRange(productSales);
            int lol = db.SaveChanges();
            Console.WriteLine("sav" + lol);
        }

        public static string Saleid(string name)
        {
            //SMN / 2016 / 001    Manager
            //SMN / 2016 / 002    Sanjeev Kumar Mishra
            //SMN / 2016 / 003    Mukesh Kumar Mandal
            // SMN / 2021 / 006  Amit Thakur
            if (name.Contains("Mukesh")) return "SMN/2016/003";
            else if (name.Contains("Amit")) return "SMN/2021/006";
            else if (name.Contains("Sanjeev")) return "SMN/2016/002";
            else return "SMN/2016/001";
        }
        public static void ReadLocalExcel(AzurePayrollDbContext db, DataGridView gv)
        {
            gridview = gv;
            string exfile = @"d:\RestBills.xlsx";
            dtS = ImportData.ReadExcelToDatatable(exfile, 1, 1, 130, 32);
            gridview.DataSource = dtS;
            ProcessLocalDataTableToObject(db);
        }

        public static void ReadWebExcel(AzurePayrollDbContext db, DataGridView gv)
        {
            gridview = gv;
            string exfile = @"d:\tasgst.xlsx";
            dtS = ImportData.ReadExcelToDatatable(exfile, 1, 1, 1062, 26);
            gridview.DataSource = dtS;
            ProcessWebDataTable(db);
        }
        public static DateTime ToDateDMY(string ondate)
        {
            char c = '-';
            if (ondate.Contains("/")) c = '/';
            var dd = ondate.Split(c);
            if (dd.Count() > 2)
            {
                var yt = dd[2].Split(" ");
                var time = yt[1].Split(":");
                if (time[0] == "00")
                {
                    return new DateTime(Int32.Parse(yt[0]), Int32.Parse(dd[1]), Int32.Parse(dd[0]));
                }
                else
                {
                    return new DateTime(Int32.Parse(yt[0]), Int32.Parse(dd[1]), Int32.Parse(dd[0])
                           , Int32.Parse(time[0]), Int32.Parse(time[1]), Int32.Parse(time[2])
                           );
                }
            }
            else return DateTime.Now;
        }

        public static int GetSize(string size)
        {
            var sizes = Enum.GetNames(typeof(Size)).ToList();
            var d = sizes.Where(c => c.Contains(size)).FirstOrDefault();
            if (d != null)
                return sizes.IndexOf(d);
            else return -1;

        }

    }
}