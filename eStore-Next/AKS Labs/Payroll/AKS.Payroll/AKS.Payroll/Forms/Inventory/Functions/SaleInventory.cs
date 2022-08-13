using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SaleInventory
    {
        public static int StockUpdate(AzurePayrollDbContext db, bool reg = true)
        {
            decimal soldqty = 0;
            decimal tqty = 0;
            if (reg)
            {
                var sale = db.SaleItems.GroupBy(c => new { c.Barcode, c.BilledQty })
                    .Select(c => new { c.Key.Barcode, Total = c.Sum(x => x.BilledQty) })
                    .ToList();
                var stocks = db.Stocks.ToList();
                foreach (var im in sale)
                {
                    try
                    {
                        var st = stocks.FirstOrDefault(c => c.Barcode == im.Barcode);
                        st.SoldQty = im.Total;
                        db.Stocks.Update(st);
                        soldqty += im.Total;
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                        tqty += im.Total;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
            else
            {
                var sale = db.SaleItems.GroupBy(c => new { c.Barcode, c.BilledQty })
                .Select(c => new { c.Key.Barcode, Total = c.Sum(x => x.BilledQty) })
                .ToList();
                var stocks = db.Stocks.ToList();
                foreach (var im in sale)
                {
                    try
                    {
                        var st = stocks.FirstOrDefault(c => c.Barcode == im.Barcode);
                        st.SoldQty = im.Total;
                        db.Stocks.Update(st);
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return db.SaveChanges();
        }

        public static string INCode(int count)
        {
            string a = "";
            if (count < 10) a = $"000{count}";
            else if (count >= 10 && count < 100) a = $"00{count}";
            else if (count >= 100 && count < 1000) a = $"0{count}";
            else a = $"{count}";
            return a;
        }

        public static PayMode SetPaymentMode(string types)
        {
            var mode = types switch
            {
                "CRD" => PayMode.Card,
                "CAS" => PayMode.Cash,
                "SR" => PayMode.SaleReturn,
                "Mix" => PayMode.MixPayments,
                _ => PayMode.Others,
            };
            return mode;
        }

        public static void AddPayment(AzurePayrollDbContext db, string inv, string types, decimal amount)
        {
            var mode = types switch
            {
                "CRD" => PayMode.Card,
                "CAS" => PayMode.Cash,
                "SR" => PayMode.SaleReturn,
                "Mix" => PayMode.MixPayments,
                _ => PayMode.Others,
            };
            SalePaymentDetail detail = new SalePaymentDetail
            {
                InvoiceCode = inv,
                PayMode = mode,
                PaidAmount = amount,
                RefId = "#Imported so Missing#",

            };
            db.SalePaymentDetails.Add(detail);
            if (mode == PayMode.Card)
            {
                //Need to handle EDCTerminal
                CardPaymentDetail card = new CardPaymentDetail
                {
                    AuthCode = 0,
                    CardLastDigit = 0,
                    CardType = CardType.Visa,
                    Card = Card.DebitCard,
                    InvoiceCode = inv,
                    PaidAmount = amount,
                    EDCTerminalId = string.Empty,
                };
                db.CardPaymentDetails.Add(card);
            }
        }

        //Voyger
        public static List<ProductSale> ProcessSaleInvoice(AzurePayrollDbContext db, DataTable dt)
        {
            List<ProductSale> sales = new List<ProductSale>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductSale sale = new ProductSale
                {
                    Adjusted = false,
                    EntryStatus = EntryStatus.Added,
                    FreeQty = 0,
                    InvoiceType = InvoiceType.Sales,
                    IsReadOnly = false,
                    MarkedDeleted = false,
                    StoreId = "ARD",
                    OnDate = DateTime.Parse(dt.Rows[i]["Invoice Date"].ToString()),
                    InvoiceNo = dt.Rows[i]["Invoice No"].ToString(),
                    BilledQty = decimal.Parse(dt.Rows[i]["Quantity"].ToString()),
                    TotalDiscountAmount = decimal.Parse(dt.Rows[i]["Discount Amt"].ToString()),
                    Paid = true,
                    Tailoring = false,
                    Taxed = false,
                    UserId = "Auto",
                    TotalTaxAmount = decimal.Parse(dt.Rows[i]["Tax Amt"].ToString()),
                    TotalPrice = decimal.Parse(dt.Rows[i]["Bill Amt"].ToString()),
                    TotalMRP = decimal.Parse(dt.Rows[i]["MRP"].ToString()),
                    TotalBasicAmount = decimal.Parse(dt.Rows[i]["Basic Amt"].ToString()),
                    RoundOff = decimal.Parse(dt.Rows[i]["Round Off"].ToString()),
                    SalesmanId = "SMN/2016/001"
                };
                sale.InvoiceCode = $"ARD/{sale.OnDate.Year}/{INCode(i + 1)}";

                if (dt.Rows[i]["Invoice Type"].ToString() == "SALES")
                    sale.InvoiceType = InvoiceType.Sales;
                else if (dt.Rows[i]["Invoice Type"].ToString() == "SALES RETURN")
                    sale.InvoiceType = InvoiceType.SalesReturn;

                //AddPayment(db, sale.InvoiceCode, dt.Rows[i]["Payment Type"].ToString(), sale.TotalPrice);
                var mode = SetPaymentMode(dt.Rows[i]["Payment Type"].ToString());
                SalePaymentDetail detail = new SalePaymentDetail
                {
                    InvoiceCode = sale.InvoiceCode,
                    PayMode = mode,
                    PaidAmount = sale.TotalPrice,
                    RefId = "#Imported so Missing#",

                };
                db.SalePaymentDetails.Add(detail);
                if (mode == PayMode.Card)
                {
                    //Need to handle EDCTerminal
                    CardPaymentDetail card = new CardPaymentDetail
                    {
                        AuthCode = 0,
                        CardLastDigit = 0,
                        CardType = CardType.Visa,
                        Card = Card.DebitCard,
                        InvoiceCode = sale.InvoiceCode,
                        PaidAmount = sale.TotalPrice,
                        EDCTerminalId = "EDC/2016/999",
                    };
                    db.CardPaymentDetails.Add(card);
                }
                sales.Add(sale);

            }

            db.ProductSales.AddRange(sales);
            int x = db.SaveChanges();
            return sales;
        }

        //public static void ProcessSaleEntry(AzurePayrollDbContext db, DataTable dt)
        //{
        //    var ivList = db.ProductSales
        //        .Select(c => new { c.InvoiceCode, c.InvoiceNo, c.InvoiceType, c.OnDate })
        //        .ToList();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        SaleItem item = new SaleItem
        //        {
        //            Adjusted = false,
        //            Barcode = dt.Rows[i]["BARCODE"].ToString(),
        //            BilledQty = decimal.Parse(dt.Rows[i]["Quantity"].ToString()),
        //            DiscountAmount = 0,
        //            FreeQty = 0,
        //            InvoiceCode = ivList.Where(c => c.InvoiceNo == dt.Rows[i]["Barcode"].ToString()).First().InvoiceNo,
        //            LastPcs = false,
        //            TaxAmount = 0,
        //            Unit = Unit.NoUnit,
        //            Value = 0
        //        };
        //    }
        //}

        /// <summary>
        ///  Convert Datatable to JSon File
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int JsonSaleEntry(AzurePayrollDbContext db, DataTable dt)
        {
            var ivList = db.ProductSales
                .Select(c => new { c.InvoiceCode, c.InvoiceNo, c.InvoiceType, c.OnDate })
                .ToList();

            List<SVM> svmList = new List<SVM>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                SVM item = new SVM();
                //{
                item.basic = Utils.ToDecimal(dt.Rows[i]["Basic Amt"].ToString());
                item.Barcode = dt.Rows[i]["BAR CODE"].ToString();
                item.Qty = decimal.Parse(dt.Rows[i]["Quantity"].ToString());
                item.BillAmt = decimal.Parse(dt.Rows[i]["Bill Amt"].ToString());
                item.BrandName = dt.Rows[i]["Brand Name"].ToString();

                item.CAmt = string.IsNullOrEmpty(dt.Rows[i]["CGST Amt"].ToString()) ? 0 : decimal.Parse(dt.Rows[i]["CGST Amt"].ToString());
                item.disc = decimal.Parse(dt.Rows[i]["Discount Amt"].ToString());
                item.HSNCode = dt.Rows[i]["HSN Code"].ToString();
                item.LineTotal = string.IsNullOrEmpty(dt.Rows[i]["Line Total"].ToString()) ? 0 : decimal.Parse(dt.Rows[i]["Line Total"].ToString());
                item.MRP = decimal.Parse(dt.Rows[i]["MRP"].ToString());
                item.RoundOff = decimal.Parse(dt.Rows[i]["Round Off"].ToString());
                item.SAmt = decimal.Parse(dt.Rows[i]["SGST Amt"].ToString());
                item.Tailoring = dt.Rows[i]["TAILORING FLAG"].ToString();
                item.Tax = string.IsNullOrEmpty(dt.Rows[i]["Tax Amt"].ToString()) ? 0 : decimal.Parse(dt.Rows[i]["Tax Amt"].ToString());
                item.SalesManName = dt.Rows[i]["SalesMan Name"].ToString();
                item.OnDate = DateTime.Parse(dt.Rows[i]["Invoice Date"].ToString());
                item.ProductName = dt.Rows[i]["Product Name"].ToString();
                item.InvNo = dt.Rows[i]["Invoice No"].ToString();
                item.PaymentMode = dt.Rows[i]["Payment Mode"].ToString();
                item.InvType = dt.Rows[i]["Invoice Type"].ToString();
                item.LP = dt.Rows[i]["LP Flag"].ToString();
                // };
                svmList.Add(item);
            }
            string filname = @"d:\apr\2022\aug\22\svm";

            Directory.CreateDirectory(filname);
            _ = Utils.ToJsonAsync(filname + @"\sale.json", svmList);
            string filname2 = @"d:\apr\2022\aug\22\svm\invoice.json";
            _ = Utils.ToJsonAsync(filname2, ivList);
            return 101;
        }

        /// <summary>
        /// Update Salesman
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task<int> UpdateSM(AzurePayrollDbContext db)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var invList = svmList.GroupBy(c => new { c.InvNo, NAME = c.SalesManName.ToLower() }).ToList();
            var smList = db.Salesmen.Select(c => new { c.SalesmanId, NAME = c.Name.ToLower() }).ToList();
            var ivs = db.ProductSales.ToList();
            foreach (var item in invList)
            {
                try
                {
                    var smid = smList.Where(c => c.NAME.Contains(item.Key.NAME)).FirstOrDefault().SalesmanId;
                    var inv = ivs.Where(c => c.InvoiceNo == item.Key.InvNo).First();
                    inv.SalesmanId = smid;
                    db.ProductSales.Update(inv);
                }
                catch (Exception)
                {


                }

            }
            return db.SaveChanges();
        }


        public static async Task ProcessSaleItemFromJsonFileAsync(AzurePayrollDbContext db)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            var ivList = db.ProductSales
               .Select(c => new { c.InvoiceCode, c.InvoiceNo, c.InvoiceType, c.OnDate })
               .ToList();
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var a = svmList.GroupBy(c => c.InvNo).ToList();
            foreach (var item in a)
            {

            }

        }
        public static async void SplitSaleItem()
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            string filepath = @"d:\apr\2022\aug\22\svm\sale";
            Directory.CreateDirectory(filepath);
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var a = svmList.GroupBy(c => c.InvNo).ToList();
            foreach (var item in a)
            {
                var list = svmList.Where(c => c.InvNo == item.Key).ToList();
                _ = Utils.ToJsonAsync<SVM>(Path.Combine(filepath, $"{item.Key}.json"), list);
            }
        }

        public static async Task<int> SaleItems(AzurePayrollDbContext db)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            string filepath = @"d:\apr\2022\aug\22\svm\sale";

            var ivList = db.ProductSales
                .Select(c => new { c.InvoiceCode, c.InvoiceNo, c.InvoiceType, c.OnDate })
                .ToList();
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            string name = "";
            int z = 0;
            int y = 0;
            string fn = "";
            int x = 0;
            try
            {
                string[] filePaths = Directory.GetFiles(filepath, "*.json",
                                         SearchOption.TopDirectoryOnly);
                foreach (var nm in filePaths)
                {
                    fn = nm;
                    z++;
                    var invList = await Utils.FromJsonToObject<SVM>(nm);
                    string ic = ivList.Where(c => c.InvoiceNo == invList[0].InvNo).First().InvoiceCode;
                    name = invList[0].InvNo;
                    foreach (var item in invList)
                    {
                        x++;
                        name += "#" + item.Barcode;
                        SaleItem saleItem = new SaleItem
                        {
                            Adjusted = false,
                            Barcode = item.Barcode,
                            BilledQty = item.Qty,
                            DiscountAmount = item.disc,
                            FreeQty = 0,
                            InvoiceCode = ic,
                            LastPcs = false,
                            TaxAmount = item.Tax,
                            Unit = Unit.NoUnit,
                            Value = item.LineTotal
                        };
                        db.SaleItems.Add(saleItem);
                    }
                    x += db.SaveChanges();
                }

                return x;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(name);
                Console.WriteLine(x);
                Console.WriteLine(z);
                Console.WriteLine(y);
                Console.WriteLine(fn);

                return x;
            }

        }

        /// <summary>
        /// Update HSNCode
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task<int> UpdateHSNCode(AzurePayrollDbContext db)
        {
            string filname = @"d:\apr\2022\aug\22\svm\sale.json";
            var svmList = await Utils.FromJsonToObject<SVM>(filname);
            var filter = svmList.Where(c => string.IsNullOrEmpty(c.HSNCode) == false)
            .GroupBy(c => new { c.Barcode, c.HSNCode })
                .Select(c => new { c.Key.Barcode, c.Key.HSNCode })
                .ToList();
            var pList = db.ProductItems.ToList();
            foreach (var item in filter)
            {
                var pi = pList.Where(c => c.Barcode == item.Barcode).FirstOrDefault();
                pi.HSNCode = item.HSNCode;
                db.ProductItems.Update(pi);
            }
            int x = db.SaveChanges();
            return x;
        }

        /// <summary>
        /// Update Unit
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int UpdateUnit(AzurePayrollDbContext db)
        {
            var si = db.SaleItems.ToList();
            var pi = db.ProductItems.Select(c => new { c.Barcode, c.Unit }).ToList();
            foreach (var im in si)
            {
                try
                {
                    im.Unit = pi.First(c => c.Barcode == im.Barcode).Unit;
                    db.SaleItems.Update(im);
                }
                catch (NullReferenceException)
                {
                    im.Unit = Unit.Nos;
                    db.SaleItems.Update(im);
                }
                catch (Exception)
                {
                    im.Unit = Unit.Nos;
                    db.SaleItems.Update(im);
                }

            }
            return db.SaveChanges();

        }



    }
}