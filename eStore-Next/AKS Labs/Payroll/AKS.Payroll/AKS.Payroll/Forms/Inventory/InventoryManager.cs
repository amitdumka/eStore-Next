using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using System.Data;
using System.Text.Json;

namespace AKS.Payroll.Forms.Inventory
{
    public class InventoryManager
    {
        public AzurePayrollDbContext azureDb;
        public LocalPayrollDbContext localDb;
        public string StoreCode;
        private static List<string> enumList;
        private List<ProductType> productTypeList;
        private List<ProductSubCategory> productSubCategories;
        private List<string> sizeList;
        private List<ProductItem> pItemList;
        private List<Brand> brandList;

        /// <summary>
        /// Default constr
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ldb"></param>
        /// <param name="sc"></param>
        public InventoryManager(AzurePayrollDbContext db, LocalPayrollDbContext ldb, string sc)
        {
            azureDb = db; localDb = ldb; StoreCode = sc;
        }

        /// <summary>
        /// Checks if product code exist
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public bool ProuctItemExist(string barcode)
        {
            return pItemList.Any(c => c.Barcode == barcode);
            //return azureDb.ProductItems.Any(c => c.Barcode == barcode);
        }

        /// <summary>
        /// Set Brand Code
        /// </summary>
        /// <param name="style"></param>
        /// <param name="cat"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string SetBrandCode(string style, string cat, string type)
        {
            string bcode = "";
            if (cat == "Apparel")
            {
                if (style.StartsWith("FM"))
                {
                    bcode = "FM";
                }
                else if (style.StartsWith("ARI")) bcode = "ADR";
                else if (style.StartsWith("HA")) bcode = "HAN";
                else if (style.StartsWith("AA")) bcode = "ARN";
                else if (style.StartsWith("AF")) bcode = "ARR";
                else if (style.StartsWith("US")) bcode = "USP";
                else if (style.StartsWith("AB")) bcode = "ARR";
                else if (style.StartsWith("AK")) bcode = "ARR";
                else if (style.StartsWith("AN")) bcode = "ARR";
                else if (style.StartsWith("ARE")) bcode = "ARR";
                else if (style.StartsWith("ARG")) bcode = "ARR";
                else if (style.StartsWith("AS")) bcode = "ARS";
                else if (style.StartsWith("AT")) bcode = "ARR";
                else if (style.StartsWith("F2")) bcode = "FM";
                else if (style.StartsWith("UD")) bcode = "UD";
            }
            else if (cat == "Shirting" || cat == "Suiting")
            {
                bcode = "ARD";
            }
            else

            if (cat == "Promo")
            {
                if (type == "Free GV") { bcode = "AGV"; }
                else bcode = "ARP";
            }
            else if (cat == "Suit Cover")
            {
                bcode = "ARA";
            }
            return bcode;
        }

        /// <summary>
        /// set Unit for item
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Unit SetUnit(string name)
        {
            if (name.StartsWith("Apparel")) { return Unit.Pcs; }
            else if (name.StartsWith("Promo") || name.StartsWith("Suit Cover")) { return Unit.Nos; }
            else if (name.StartsWith("Shirting") || name.StartsWith("Suiting")) { return Unit.Meters; }
            return Unit.NoUnit;
        }

        /// <summary>
        /// Set Catgory of Product
        /// </summary>
        /// <param name="pi"></param>
        private ProductItem SetCatgoryAndSize(ProductItem pi)
        {
            var cats = pi.Name.Split("/");

            int x = 0;
            if (cats[0] == "Suiting" || cats[0] == "Shirting") x = 0;
            else if (cats[0] == "Promo") x = (int)ProductCategory.PromoItems;
            else if (cats[0] == "Suit Cover") x = (int)ProductCategory.SuitCovers;
            else { x = enumList.IndexOf(cats[0]); }
            pi.ProductCategory = (ProductCategory)x;
            string ptid = productTypeList.Where(c => c.ProductTypeName == cats[1]).First().ProductTypeId;
            if (ptid != null)
                pi.ProductTypeId = ptid;
            else
            {
                pi.ProductTypeId = "PT00013";
            }
            pi.SubCategory = cats[2];
            if (pi.Unit == Unit.Meters)
            {
                pi.Size = Size.NS;
                pi.BrandCode = "ARD";
            }
            else if (pi.Unit == Unit.Nos || pi.Unit == Unit.NoUnit)
            {
                pi.Size = Size.NS;
                pi.BrandCode = SetBrandCode(pi.StyleCode, cats[0], cats[2]);
            }
            else
            {
                pi.Size = SetSize(pi.StyleCode, cats[2]);
                pi.BrandCode = SetBrandCode(pi.StyleCode, cats[0], cats[2]);
            }
            return pi;
        }

        /// <summary>
        /// Set Size based on style code and Category
        /// </summary>
        /// <param name="style"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public Size SetSize(string style, string category)
        {
            Size size = Size.NOTVALID;
            var name = style.Substring(style.Length - 4, 4);

            // Jeans and Trousers

            if (category.Contains("Boxer") || category.Contains("Socks") || category.Contains("H-Shorts") || category.Contains("Shirt") || category.Contains("Vests") || category.Contains("Briefs") || category.Contains("Jackets") || category.Contains("Sweat Shirt") || category.Contains("Sweater") || category.Contains("T shirts"))
            {
                if (name.EndsWith(Size.XXXL.ToString())) size = Size.XXXL;
                else if (name.EndsWith(Size.XXL.ToString())) size = Size.XXL;
                else if (name.EndsWith(Size.XL.ToString())) size = Size.XL;
                else if (name.EndsWith(Size.L.ToString())) size = Size.L;
                else if (name.EndsWith(Size.M.ToString())) size = Size.M;
                else if (name.EndsWith(Size.S.ToString())) size = Size.S;
                else if (name.EndsWith("FS")) size = Size.FreeSize;
                else
                {
                    var nn = name.Substring(name.Length - 2).Trim();
                    int nx = 0;
                    if (Int32.TryParse(nn, out nx))
                    {
                        switch (nx)
                        {
                            case 39: size = Size.S; break;
                            case 40: size = Size.M; break;
                            case 42: size = Size.L; break;
                            case 44: size = Size.XL; break;
                            case 46: size = Size.XXL; break;
                            case 48: size = Size.XXXL; break;
                            default:
                                size = Size.NOTVALID;
                                break;
                        }
                    }
                    else
                    {
                    }
                }
            }
            else if (category.Contains("Shorts") || category.Contains("Jeans") || category.Contains("Trouser") || category.Contains("Trousers"))
            {
                int x = sizeList.IndexOf($"T{name[2]}{name[3]}");
                size = (Size)x;
            }
            else if (category.Contains("Bundis") || category.Contains("Blazer") || category.Contains("Blazers") || category.Contains("Suits"))
            {
                int x = sizeList.IndexOf($"B{name[2]}{name[3]}");
                if (x == -1)
                {
                    x = sizeList.IndexOf($"B{name[1]}{name[2]}{name[3]}");
                }
                if (x == -1)
                {
                    size = Size.NOTVALID;
                }
                else
                    size = (Size)x;
            }
            else if (category.Contains("Accessories"))
            {
                size = Size.NS;
            }
            else
            {
                size = Size.NOTVALID;
            }
            return size;
        }

        /// <summary>
        /// Process Missing Product Item
        /// </summary>
        /// <param name="DataTable"></param>
        /// <returns></returns>
        public List<ProductItem> ProcessProductItem(DataTable DataTable)
        {
            enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            sizeList = Enum.GetNames(typeof(Size)).ToList();
            productSubCategories = azureDb.ProductSubCategories.ToList();
            productTypeList = azureDb.ProductTypes.ToList();
            pItemList = azureDb.ProductItems.ToList();
            List<ProductItem> pList = new List<ProductItem>();
            brandList = azureDb.Brands.ToList();
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                if (!ProuctItemExist(DataTable.Rows[i]["Barcode"].ToString()))
                {
                    ProductItem item = new ProductItem
                    {
                        Barcode = DataTable.Rows[i]["Barcode"].ToString(),
                        StyleCode = DataTable.Rows[i]["Style Code"].ToString(),
                        Description = DataTable.Rows[i]["Item Desc"].ToString(),
                        MRP = decimal.Parse(DataTable.Rows[i]["MRP"].ToString().Trim()),
                        HSNCode = "NA",
                        Name = DataTable.Rows[i]["Product Name"].ToString(),
                        TaxType = TaxType.GST,
                        Unit = SetUnit(DataTable.Rows[i]["Product Name"].ToString()),
                    };
                    item = SetCatgoryAndSize(item);
                    pList.Add(item);
                }
            }
            pList = pList.Distinct().ToList();
            azureDb.ProductItems.AddRange(pList);
            azureDb.SaveChanges();
            return pList;
        }

        /// <summary>
        /// Process Stock Entry
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Stock> ProcessStocks(DataTable dt)
        {
            List<Stock> stocks = new List<Stock>();
            List<string> barcode = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (barcode.Any(c => c == dt.Rows[i]["Barcode"].ToString()))
                {
                    var s = stocks.Find(c => c.Barcode == dt.Rows[i]["Barcode"].ToString());
                    decimal cost = decimal.Parse(dt.Rows[i]["Cost"].ToString().Trim());
                    stocks.Remove(s);
                    if (cost != s.CostPrice)
                    {
                        var tc = s.CostPrice * s.PurhcaseQty;
                        var newQty = decimal.Parse(dt.Rows[i]["Quantity"].ToString().Trim());
                        tc = tc + (newQty * cost);
                        var avc = Decimal.Round((tc / (newQty + s.PurhcaseQty)), 2);
                        s.PurhcaseQty += newQty;
                        s.CostPrice = avc;
                        s.MultiPrice = true;
                        s.EntryStatus = EntryStatus.Approved;
                        // s.HoldQty = newQty;
                        var newMRP = decimal.Parse(dt.Rows[i]["MRP"].ToString().Trim());
                        if (newMRP != s.MRP)
                        {
                            if (newMRP > s.MRP)
                                s.MRP = newMRP;
                        }
                    }
                    else
                    {
                        s.PurhcaseQty += decimal.Parse(dt.Rows[i]["Quantity"].ToString().Trim());
                        s.EntryStatus = EntryStatus.Updated;
                    }
                    stocks.Add(s);
                }
                else
                {
                    barcode.Add(dt.Rows[i]["Barcode"].ToString());
                    Stock stock = new Stock
                    {
                        Barcode = dt.Rows[i]["Barcode"].ToString(),
                        EntryStatus = EntryStatus.Added,
                        IsReadOnly = true,
                        StoreId = "ARD",
                        UserId = "AUTO",
                        MarkedDeleted = false,
                        HoldQty = 0,
                        SoldQty = 0,
                        CostPrice = decimal.Parse(dt.Rows[i]["Cost"].ToString().Trim()),
                        PurhcaseQty = decimal.Parse(dt.Rows[i]["Quantity"].ToString().Trim()),
                        Unit = SetUnit(dt.Rows[i]["Product Name"].ToString().Trim()),
                        MultiPrice = false,
                        MRP = decimal.Parse(dt.Rows[i]["MRP"].ToString().Trim())
                    };
                    stocks.Add(stock);
                }
            }
            //stocks = stocks.Where(c => c.EntryStatus == EntryStatus.Approved).OrderByDescending(c => c.EntryStatus).
            //       ToList();
            azureDb.Stocks.AddRange(stocks);
            azureDb.SaveChangesAsync();
            return stocks;
        }

        public List<Stock> UpdateFabricCostPriceWithFreigtCharge()
        {
            var Stocks = azureDb.Stocks.Where(c => c.Unit == Unit.Meters).ToList();
            foreach (var item in Stocks)
            {
                item.CostPrice += (item.PurhcaseQty * 3);
                item.IsReadOnly = false;
            }
            azureDb.Stocks.UpdateRange(Stocks);
            azureDb.SaveChangesAsync();
            return Stocks;
        }

        /// <summary>
        /// Obsolute
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="sub"></param>
        private void UpdateSubCate(string cat, string sub)
        {
            var subcat = azureDb.ProductSubCategories.Find(sub);
            int x = enumList.IndexOf(cat);
            if (x == -1)
            {
                x = enumList.IndexOf(cat);
                if (cat == "Suiting" || cat == "Shirting") x = 0;
                else if (cat == "Promo") x = (int)ProductCategory.PromoItems;
                else if (cat == "Suit Cover") x = (int)ProductCategory.SuitCovers;
                else
                {
                    x = enumList.IndexOf(cat);
                }
            }
            subcat.ProductCategory = (ProductCategory)x;
            azureDb.ProductSubCategories.Update(subcat);
        }

        /// <summary>
        /// Onbsolute
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="lb1"></param>
        /// <param name="lb2"></param>
        /// <param name="lb3"></param>
        public void CreateCategoryList(DataTable dataTable, ListBox lb1, ListBox lb2, ListBox lb3)
        {
            enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            var data = dataTable.AsEnumerable().GroupBy(c => c["Product Name"]).Select(c => c.Key.ToString()).ToList();
            List<string> a = new List<string>();
            List<string> b = new List<string>();
            List<string> c = new List<string>();

            foreach (var item in data)
            {
                var x = item.Split("/");
                a.Add(x[0]);
                b.Add(x[1]);
                c.Add(x[2]);
                //  UpdateSubCate(x[0], x[2]);
            }

            a = a.Distinct().ToList();
            b = b.Distinct().ToList();
            c = c.Distinct().ToList();
            c.Sort();
            b.Sort();
            // Apprel, shirting, suiting
            lb1.DataSource = a;
            // Color or stle name
            lb2.DataSource = b;
            // Type of Product.
            lb3.DataSource = c;
            //Store In table.
            //int ctr = 0;
            //foreach (var item in b)
            //{
            //    ctr++;
            //    string p = "";
            //    if (ctr >= 10) { p = "PT000" + ctr; }
            //    else p = "PT0000" + ctr;
            //    ProductType pt = new ProductType {
            //        ProductTypeName = item, ProductTypeId = p
            //    };
            //    azureDb.ProductTypes.Add(pt);
            //}
            //foreach (var ic in c)
            //{
            //    if (!azureDb.ProductSubCategories.Any(c => c.SubCategory == ic))
            //    {
            //        azureDb.ProductSubCategories.Add(new ProductSubCategory { SubCategory=ic, ProductCategory =ProductCategory.Others });
            //    }
            //}
            // azureDb.SaveChanges();
        }

        /// <summary>
        /// obsolute
        /// </summary>
        /// <param name="d"></param>
        /// <param name="dgv"></param>
        /// <param name="lb1"></param>
        /// <param name="lb2"></param>
        public void TryCatnSize(DataTable d, DataGridView dgv, ListBox lb1, ListBox lb2)
        {
            //enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            //sizeList = Enum.GetNames(typeof(Size)).ToList();
            //productSubCategories = azureDb.ProductSubCategories.ToList();
            //productTypeList = azureDb.ProductTypes.ToList();
            //var data = d.AsEnumerable().Select(c => new { Name = c["Product Name"].ToString(), Style = c["Style Code"].ToString() })
            //    .ToList();
            //List<ProductItem> pList = new List<ProductItem>();
            //foreach (var item in data)
            //{
            //    ProductItem pi = new ProductItem
            //    {
            //        StyleCode = item.Style,
            //        Name = item.Name,
            //        Unit = SetUnit(item.Name)
            //    };
            //    pi = SetCatgoryAndSize(pi);
            //    pList.Add(pi);
            //}
            //dgv.DataSource = pList;//.Where(c=>c.Size==Size.NOTVALID);
            //try
            //{
            //    int ex= 0,mis=0;
            //    pItemList = azureDb.ProductItems.ToList();
            //    List<string> mBarcode = new List<string>();
            //    DataTable dataTable = d.Clone();
            //    for (int i = 0; i < d.Rows.Count; i++)
            //    {
            //        if (ProuctItemExist(d.Rows[i]["Barcode"].ToString()))
            //        {
            //            d.Rows[i]["ExmillCost"] = "Exist";
            //            ex++;
            //        }
            //        else
            //        {
            //            d.Rows[i]["ExmillCost"] = "Missing";
            //            mis++;
            //            mBarcode.Add(d.Rows[i]["Barcode"].ToString());
            //            dataTable.ImportRow(d.Rows[i]);
            //        }
            //    }

            //    mBarcode= mBarcode.Distinct().ToList();
            //    mBarcode.Sort();
            //    dgv.DataSource = dataTable;
            //    lb1.Items.Add($"Total:{d.Rows.Count}");
            //    lb1.Items.Add($"Missing:{mis}");
            //    lb1.Items.Add($"Exsit:{ex}");
            //    lb2.DataSource = mBarcode;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }

        /// <summary>
        /// Obsulute
        /// </summary>
        private void ProcessStockUpdate()
        {
            var stock = azureDb.PurchaseItems
                .Select(c => new Stock
                {
                    Barcode = c.Barcode,
                    CostPrice = c.CostPrice,
                    EntryStatus = EntryStatus.Added,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    StoreId = "ARD",
                    HoldQty = 0,
                    SoldQty = 0,
                    Unit = c.Unit,
                    UserId = "AUTO",
                    PurhcaseQty = c.Qty
                }).ToList();

            List<Stock> Stocks = new List<Stock>();
            //search for dplicate barcode

            //var dup = stock.GroupBy(c => c.Barcode).Select(c=> new {c.Key , ctr=c.Key.Count()}).Where(c => c.ctr > 1).ToList();
            foreach (var item in stock)
            {
                if (Stocks.Any(c => c.Barcode == item.Barcode))
                {
                    var s = Stocks.Find(c => c.Barcode == item.Barcode);
                    Stocks.Remove(s);
                    s.PurhcaseQty += item.PurhcaseQty;
                    Stocks.Add(s);
                }
                else
                {
                    Stocks.Add(item);
                }
            }
            azureDb.Stocks.AddRange(Stocks);
            var c = azureDb.SaveChanges();
            if (c > 0)
            {
                Console.WriteLine(c);
            }
        }

        public void ImportInvoice(DataTable dt)
        {
            enumList = Enum.GetNames(typeof(ProductCategory)).ToList();
            sizeList = Enum.GetNames(typeof(Size)).ToList();

            // Filter Invoice Numbers
            var invoiceList = dt.AsEnumerable().GroupBy(c => new { Inv = c["Invoice No"].ToString(), Date = c["Invoice Date"] }).Select(c => new { c.Key.Inv, c.Key.Date }).ToList();
            List<PurchaseProduct> invoice = new List<PurchaseProduct>();
            foreach (var item in invoiceList)
            {
                PurchaseProduct p = new PurchaseProduct
                {
                    BasicAmount = 0,
                    BillQty = 0,
                    Count = 0,
                    DiscountAmount = 0,
                    EntryStatus = EntryStatus.Added,
                    FreeQty = 0,
                    InvoiceNo = item.Inv,
                    InvoiceType = PurchaseInvoiceType.Purchase,
                    InwardDate = DateTime.Today,
                    InwardNumber = "",
                    IsReadOnly = false,
                    MarkedDeleted = false,
                    OnDate = (DateTime)item.Date,
                    Paid = false,
                    ShippingCost = 0,
                    StoreId = "ARD",
                    TaxAmount = 0,
                    TaxType = TaxType.GST,
                    TotalAmount = 0,
                    TotalQty = 0,
                    UserId = "AUTO",
                    VendorId = "",
                    Warehouse = "",
                };
                invoice.Add(p);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
            }
        }
    }

    public class Utils
    {
        public static int ReadInt(TextBox t)
        {
            return Int32.Parse(t.Text.Trim());
        }
        public static decimal ReadDecimal(TextBox t)
        {
            return decimal.Parse(t.Text.Trim());
        }

        public static decimal ToDecimal(string val)
        {
            return Decimal.Round(decimal.Parse(val.Trim()), 2);
        }

        public static async Task ToJsonAsync<T>(string fileName, List<T> ObjList)
        {
            // string fileName = "WeatherForecast.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, ObjList);
            await createStream.DisposeAsync();
        }
        public static async Task<List<PurchaseItem>?> FromJson<T>(string filename)
        {
            using FileStream openStream = File.OpenRead(filename);
            return await JsonSerializer.DeserializeAsync<List<PurchaseItem>>(openStream);
        }
    }


    public class Inventory
    {
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

        /// <summary>
        /// Seeding vendor
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool SeedBasicVendor(AzurePayrollDbContext db)
        {
            List<Vendor> vendors = new List<Vendor>();

            Vendor v1 = new Vendor
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
            var xlPI = await Utils.FromJson<PurchaseItem>("purchaseItem.json");
            xlPI=xlPI.OrderBy(c => c.InwardNumber).ThenBy(c => c.Barcode).ToList();
            List<string> error = new List<string>();
            
            foreach (var item in dbPI)
            {
                var itm = xlPI.Where(c => c.Barcode == item.Barcode && c.InwardNumber == item.InwardNumber).FirstOrDefault(); 
                if(itm != null)
                {
                    if (item.Qty != itm.Qty)
                    {
                        error.Add($"{item.InwardNumber}/{item.Barcode}/Qty#{item.Qty}#{itm.Qty}");
                    }
                    if (item.CostPrice != itm.CostPrice)
                    {
                        error.Add($"{item.InwardNumber}/{item.Barcode}/CostPrice#{item.CostPrice}#{itm.CostPrice}");
                    }
                    if(item.CostValue!=itm.CostValue)
                        error.Add($"{item.InwardNumber}/{item.Barcode}/CostValue#{item.CostValue}#{itm.CostValue}");
                    if(item.TaxAmount!=itm.TaxAmount)
                        error.Add($"{item.InwardNumber}/{item.Barcode}/TaxAmount#{item.TaxAmount}#{itm.TaxAmount}");
                    
                }
            }
            return error; 
           
           
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
    }
}