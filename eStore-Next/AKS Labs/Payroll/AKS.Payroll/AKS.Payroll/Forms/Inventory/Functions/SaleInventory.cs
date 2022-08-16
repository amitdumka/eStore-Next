﻿using AKS.Payroll.Database;
using AKS.Payroll.Ops;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SaleInventory
    {
        /// <summary>
        /// update stock based on type
        /// </summary>
        /// <param name="db"></param>
        /// <param name="reg"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Count for id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string INCode(int count)
        {
            string a = "";
            if (count < 10) a = $"000{count}";
            else if (count >= 10 && count < 100) a = $"00{count}";
            else if (count >= 100 && count < 1000) a = $"0{count}";
            else a = $"{count}";
            return a;
        }

        /// <summary>
        /// Set Payment mode
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add payment information
        /// </summary>
        /// <param name="db"></param>
        /// <param name="inv"></param>
        /// <param name="types"></param>
        /// <param name="amount"></param>
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
        /// Split invoice json to multiple
        /// </summary>
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

        public static async Task<int> ProcessSaleItemFromJsonFileAsync(AzurePayrollDbContext db)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            var ivList = db.ProductSales
               .Select(c => new { c.InvoiceCode, c.InvoiceNo, c.InvoiceType, c.OnDate })
               .ToList();
            var inCodes = db.SaleItems.GroupBy(c => c.InvoiceCode).Select(c => c.Key).ToList();
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var a = svmList.GroupBy(c => c.InvNo).ToList();
            int x = 0;
            try
            {
                foreach (var item in inCodes)
                {
                    ivList.Remove(ivList.First(c => c.InvoiceCode == item));
                }

                foreach (var im in a)
                {
                    var invList = svmList.Where(c => c.InvNo == im.Key).ToList();
                    var ic = ivList.Where(c => c.InvoiceNo == invList[0].InvNo).FirstOrDefault();//.InvoiceCode;
                    if (ic != null)
                    {
                        foreach (var item in invList)
                        {
                            x++;
                            SaleItem saleItem = new SaleItem
                            {
                                Adjusted = false,
                                Barcode = item.Barcode,
                                BilledQty = item.Qty,
                                DiscountAmount = item.disc,
                                FreeQty = 0,
                                InvoiceCode = ic.InvoiceCode,
                                LastPcs = false,
                                TaxAmount = item.Tax,
                                Unit = Unit.NoUnit,
                                Value = item.LineTotal,
                                BasicAmount = item.basic,
                                InvoiceType = InvoiceType.Sales,
                                TaxType = item.OnDate < new DateTime(2017, 07, 1) ? TaxType.VAT : TaxType.GST
                            };
                            db.SaleItems.Add(saleItem);
                        }
                        x += db.SaveChanges();
                    }

                }

                x += db.SaveChanges();
                return x;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return x;
            }
        }

        public static async Task<int> ProcessSaleItemFromJsonFilesAsync(AzurePayrollDbContext db)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            var ivList = db.ProductSales
               .Select(c => new { c.InvoiceCode, c.InvoiceNo, c.InvoiceType, c.OnDate })
               .ToList();
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var a = svmList.GroupBy(c => c.InvNo).ToList();
            int x = 0;
            try
            {
                foreach (var im in a)
                {
                    var invList = svmList.Where(c => c.InvNo == im.Key).ToList();
                    string ic = ivList.Where(c => c.InvoiceNo == invList[0].InvNo).First().InvoiceCode;
                    foreach (var item in invList)
                    {
                        x++;
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
                            Value = item.LineTotal,
                            BasicAmount = item.basic,
                            InvoiceType = InvoiceType.Sales,
                            TaxType = item.OnDate < new DateTime(2017, 07, 1) ? TaxType.VAT : TaxType.GST
                        };
                        db.SaleItems.Add(saleItem);
                    }
                    x += db.SaveChanges();
                }

                x += db.SaveChanges();
                return x;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return x;
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

        public static async Task<List<ProductItem>> AddTailoringBarcodeAsync(AzurePayrollDbContext db)
        {
            try
            {

                string filename = @"d:\tail.xlsx";

                var dt = ImportData.ReadExcelToDatatable(filename, 1, 1, 46, 4);
                List<ProductItem> itemList = new List<ProductItem>();
                //Product Name	Item Desc	BAR CODE	Style Code	MRP

                var pname = dt.AsEnumerable().GroupBy(c => c["Product Name"]).ToList();
                int oo = 14;
                List<ProductType> ptys = new List<ProductType>();

                List<string> a = new List<string>();
                List<string> b = new List<string>();
                foreach (var item in pname)
                {
                    var names = item.Key.ToString().Split("/");
                    if (names.Count() > 2)
                    {
                        a.Add(names[1]);
                        b.Add($"{names[1]} {names[2]}");
                    }
                }
                a = a.Distinct().ToList();
                foreach (var item in a)
                {
                    ProductType pt = new ProductType { ProductTypeId = "PT000" + oo, ProductTypeName = item };
                    oo++;
                    ptys.Add(pt);
                }
                List<ProductSubCategory> catL = new List<ProductSubCategory>();
                foreach (var item in b)
                {

                    ProductSubCategory cat = new ProductSubCategory
                    {
                        ProductCategory = ProductCategory.Tailoring,
                        SubCategory = item
                    };


                    catL.Add(cat);

                }
                catL = catL.Distinct().ToList();

                //catL.Remove(catL.First(c => c.SubCategory == "Casual Jeans"));
                //db.ProductSubCategories.AddRange(catL); 
                //db.ProductTypes.AddRange(ptys);
                //db.SaveChanges();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var names = dt.Rows[i]["Product Name"].ToString().Split("/");

                    ProductItem p = new ProductItem
                    {
                        Barcode = dt.Rows[i]["BAR CODE"].ToString(),
                        BrandCode = "SAR",
                        Description = dt.Rows[i]["Item Desc"].ToString(),
                        HSNCode = "NA",
                        MRP = 0,// decimal.Parse(dt.Rows[i]["MRP"].ToString()),
                        ProductCategory = ProductCategory.Tailoring,
                        Size = Size.FreeSize,
                        TaxType = TaxType.GST,
                        Unit = Unit.Nos,
                        StyleCode = dt.Rows[i]["Style Code"].ToString(),
                        Name = dt.Rows[i]["Product Name"].ToString(),
                        SubCategory = $"{names[1]} {names[2]}",
                        ProductTypeId = ptys.First(c => c.ProductTypeName == names[1]).ProductTypeId,
                    };
                    itemList.Add(p);
                }

                itemList = itemList.Distinct().ToList();
                db.ProductItems.AddRange(itemList);
                int z = db.SaveChanges();

                return itemList;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static async Task ReverifyAsync(AzurePayrollDbContext db, DataGridView dv)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            var ivList = db.ProductSales
               .Select(c => new { c.InvoiceCode, c.InvoiceNo })
               .ToList();
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var itemList = svmList.Select(c => new { INV = c.InvNo, BAR = c.Barcode, QTY = c.Qty })
                .ToList();
            var dbList = db.SaleItems.Include(c => c.ProductSale).Select(c => new { INV = c.ProductSale.InvoiceNo, BAR = c.Barcode, QTY = c.BilledQty }).ToList();

            foreach (var item in itemList)
            {
                dbList.Remove(item);
            }
            dv.DataSource = dbList;
        }

        public static async Task<int> UpdateTaxAmount(AzurePayrollDbContext db)
        {
            string filename = @"d:\apr\2022\aug\22\svm\sale.json";
            var svmList = await Utils.FromJsonToObject<SVM>(filename);
            var filter = svmList.Where(c => c.SAmt > 0).ToList();
            var sis = db.SaleItems.Include(c => c.ProductSale).ToList();
            int x = 0;
            foreach (var item in filter)
            {
                var si = sis.First(c => c.ProductSale.InvoiceNo == item.InvNo);
                if (si.TaxType == TaxType.GST)
                    si.TaxAmount = item.SAmt * 2;
                else si.TaxAmount = item.SAmt;
                db.SaleItems.Update(si);
                x++;
            }
            return db.SaveChanges();
        }
    }

    public class MISale
    {
        public string Barcode { get; set; }//Barcode
        public string InvType { get; set; }//Bill_Type
        public decimal CostValue { get; set; }//COST_VALUE
        public decimal discAmt { get; set; }//DISC_AMT
        public string HSNCode { get; set; }//HSN_CODE
        public string InvNo { get; set; } //INVOICE_No
        public decimal MRPValue { get; set; } //MRP_Value
        public string SalesManName { get; set; }//NAME
        public string ProductName { get; set; }//HIER_I+HIER_II+HIER_III
        public decimal Qty { get; set; }//QTY


        public string BrandName { get; set; }//Description

        public DateTime OnDate { get; set; }//Transdate

        public string Size { get; set; } //Size
        public string Season { get; set; }//Season
        public string StyleCode { get; set; }//Prinicple Code
        public string ItemDesc { get; set; }//Sku_description
        public decimal TaxC { get; set; }//TOT_PER_I
        public decimal TaxS { get; set; }//TOT_PER_II
        public decimal Tax { get; set; }//Total_Tax_Amt

        public decimal UnitCost { get; set; }//Unit_Cost
        public decimal UnitMPR { get; set; }//Unit_MPR
        public decimal SAmt { get; set; }//Total_AMT_I

        public decimal basic { get; set; }


        public decimal CAmt { get; set; }
        public decimal LineTotal { get; set; }
        public decimal RoundOff { get; set; }
        public decimal BillAmt { get; set; }
        public string PaymentMode { get; set; }

        public string LP { get; set; }
        public string Tailoring { get; set; }
    }
    public class MSSaleInventory
    {
        static DataGridView gridview;
        static DataTable dtS;
        public static void ReadLocalExcel(AzurePayrollDbContext db, DataGridView gv)
        {
            gridview = gv;
            string exfile = @"d:\tasdumka001.xlsm";
            dtS = ImportData.ReadExcelToDatatable(exfile, 1, 1, 37, 32);
            gridview.DataSource = dtS;
            ProcessLocalDataTableToObject(db);

        }
        public static void ReadWebExcel(AzurePayrollDbContext db, DataGridView gv)
        {
            gridview = gv;
            string exfile = @"d:\tasMSSale.xlsx";
            dtS = ImportData.ReadExcelToDatatable(exfile, 1, 1, 1062, 64);
            gridview.DataSource = dtS;

        }
        public static void ProcessWebDataTable(AzurePayrollDbContext db)
        {

            for (int i = 0; i < dtS.Rows.Count; i++)
            {

            }
        }

        public static DateTime ToDateDMY(string ondate)
        {
            var dd = ondate.Split("-");
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

        public static void ProcessLocalDataTableToObject(AzurePayrollDbContext db)
        {
            List<MDSale> saleList = new List<MDSale>();
            var smList = db.Salesmen.Select(c => new { c.SalesmanId, c.Name }).ToList();
            for (int i = 0; i < dtS.Rows.Count; i++)
            {
                MDSale sale = new MDSale
                {
                    BARCODE = dtS.Rows[i]["BARCODE"].ToString(),
                    BASICAMOUNT = decimal.Parse(dtS.Rows[i]["BASICAMOUNT"].ToString()),
                    BILLAMOUNT = decimal.Parse(dtS.Rows[i]["BILLAMOUNT"].ToString()),
                    BRAND = dtS.Rows[i]["BRAND"].ToString(),
                    CATEGORY = dtS.Rows[i]["CATEGORY"].ToString(),
                    CGSTAMOUNT = decimal.Parse(dtS.Rows[i]["CGSTAMOUNT"].ToString()),
                    Discountamount = decimal.Parse(dtS.Rows[i]["Discount amount"].ToString()),
                    HSNCODE = dtS.Rows[i]["HSNCODE"].ToString(),
                    COUPONAMOUNT = 0,
                    COUPONPERCENTAGE = "",
                    LINETOTAL = decimal.Parse(dtS.Rows[i]["LINETOTAL"].ToString()),
                    MRP = decimal.Parse(dtS.Rows[i]["MRP"].ToString()),
                    OnDate = ToDateDMY(dtS.Rows[i]["Date"].ToString()),
                    PAYMENTMODE = dtS.Rows[i]["PAYMENTMODE"].ToString(),
                    Product = dtS.Rows[i]["Product"].ToString(),
                    Productnumber = dtS.Rows[i]["Product number"].ToString(),
                    Quantity = decimal.Parse(dtS.Rows[i]["Quantity"].ToString()),
                    Receiptnumber = dtS.Rows[i]["Receipt number"].ToString(),
                    ROUNDOFFAMT = decimal.Parse(dtS.Rows[i]["ROUNDOFFAMT"].ToString()),
                    SALESMAN = dtS.Rows[i]["SALESMAN"].ToString(),
                    SALESTYPE = dtS.Rows[i]["SALESTYPE"].ToString(),
                    SGSTAMOUNT = decimal.Parse(dtS.Rows[i]["SGSTAMOUNT"].ToString()),
                    STYLECODE = dtS.Rows[i]["STYLECODE"].ToString(),
                    TAILORINGFLAG = dtS.Rows[i]["TAILORINGFLAG"].ToString(),
                    Taxamount = decimal.Parse(dtS.Rows[i]["Tax amount"].ToString()),
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
                    InvoiceCode = $"ARD/{item.OnDate.Year}/{item.OnDate.Month}/IN/{SaleInventory.INCode(++count)}",
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
                        BilledQty = im.Quantity,
                        DiscountAmount = im.Discountamount,
                        FreeQty = 0,
                        InvoiceCode = pSale.InvoiceCode,
                        InvoiceType = InvoiceType.Sales,
                        LastPcs = false,
                        TaxType = TaxType.GST,
                        Unit = Unit.NoUnit,
                        Value = im.LINETOTAL,
                        TaxAmount = im.Taxamount,
                        BasicAmount = im.BASICAMOUNT
                    };
                    //pSale.Items = new List<SaleItem>();
                    pSale.Items.Add(saleItem);
                    pSale.BilledQty += saleItem.BilledQty;
                    pSale.TotalDiscountAmount += saleItem.DiscountAmount;
                    pSale.TotalMRP += im.MRP;
                    pSale.TotalBasicAmount += im.BASICAMOUNT;
                    pSale.TotalTaxAmount += im.Taxamount;

                    var stock = db.Stocks.FirstOrDefault(c => c.Barcode == saleItem.Barcode);
                    if (stock != null)
                    {

                        saleItem.Unit = stock.Unit;
                        stock.SoldQty = saleItem.BilledQty;
                        db.Stocks.Update(stock);

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
                                HSNCode = item.HSNCODE,
                                MRP = item.MRP,
                                Barcode = item.BARCODE,
                                Name = item.Product,
                                Size = Size.NS,
                                TaxType = TaxType.GST,
                                StyleCode = item.STYLECODE,
                                BrandCode = "ARD",
                                Description = item.CATEGORY,
                                ProductCategory = ProductCategory.Fabric,

                            };
                            var p = item.CATEGORY.Split("/").Distinct().ToList();
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
                            db.ProductItems.Add(pi);
                            PList.Add(pi);
                        }

                        db.Stocks.Add(newStock);


                    }
                    //saleItem.Unit = stock.Unit;
                    saleItems.Add(saleItem);
                }

            }

            gridview.DataSource = saleItems;
            db.ProductSales.AddRange(products);
            int s = db.SaveChanges();
            Console.WriteLine("Saved " + s);

        }
    }

    public class MDSale
    {
        //Date	Transaction number	Receipt number	SALESTYPE	SALESMAN	BRAND	CATEGORY	
        //Product number	Product	HSNCODE	BARCODE	STYLECODE	Quantity	MRP	Discount amount	
        //BASICAMOUNT	Tax amount	SGSTAMOUNT	CGSTAMOUNT	CESSAMOUNT	CHARGES	LINETOTAL	
        //ROUNDOFFAMT	BILLAMOUNT	PAYMENTMODE	COUPONPERCENTAGE	COUPONAMOUNT	TAILORINGFLAG

        public DateTime OnDate { get; set; }
        public string TranscationNumber { get; set; }
        public string Receiptnumber { get; set; }
        public string SALESTYPE { get; set; }

        public string SALESMAN { get; set; }
        public string BRAND { get; set; }
        public string CATEGORY { get; set; }

        public string Productnumber { get; set; }
        public string Product { get; set; }
        public string HSNCODE { get; set; }
        public string BARCODE { get; set; }
        public string STYLECODE { get; set; }

        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal Discountamount { get; set; }
        public decimal BASICAMOUNT { get; set; }
        public decimal Taxamount { get; set; }
        public decimal SGSTAMOUNT { get; set; }
        public decimal CGSTAMOUNT { get; set; }
        public decimal LINETOTAL { get; set; }
        public decimal ROUNDOFFAMT { get; set; }
        public decimal BILLAMOUNT { get; set; }
        public string PAYMENTMODE { get; set; }
        public string COUPONPERCENTAGE { get; set; }
        public decimal COUPONAMOUNT { get; set; }
        public string TAILORINGFLAG { get; set; }

    }


}