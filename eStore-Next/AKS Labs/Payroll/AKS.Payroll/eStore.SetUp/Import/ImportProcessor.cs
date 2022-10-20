using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStore.SetUp.Import
{
    public class ImportProcessor
    {
        private SortedDictionary<string, string> Salesman = new SortedDictionary<string, string>();
        private SortedDictionary<string, ProductCategory> ProductCategories = new SortedDictionary<string, ProductCategory>();
        private List<ProductSubCategory> ProductSubCategories;// = new SortedDictionary<string, string>();
        private List<ProductType> ProductTypes;// = new SortedDictionary<string, string>(); 
        private List<string> sizeList;
        private SortedDictionary<string, string> HSNCodes = new SortedDictionary<string, string>();

        private List<string> Cat1 = new List<string>();
        private List<string> Cat2 = new List<string>();
        private List<string> Cat3 = new List<string>();
        public static string ConfigFile = "eStoreConfig.json";

        public static async void InitConfigFile(string baseapath, string storeCode)
        {
            var fn = Path.Combine(baseapath + $@"\{storeCode}", "Configs");
            Directory.CreateDirectory(fn);
            ConfigFile = Path.Combine(fn, ConfigFile);
            if (!File.Exists(ConfigFile))
            {
                var config = new SortedDictionary<string, string>();
                config.Add("BasePath", baseapath + $@"\{storeCode}");
                using FileStream createStream = File.OpenWrite(ConfigFile);
                await JsonSerializer.SerializeAsync(createStream, config);
                await createStream.DisposeAsync();

            }
        }

        public void ReadSetting()
        {
            StreamReader reader = new StreamReader(ConfigFile);
            var json = reader.ReadToEnd();
            Settings = JsonSerializer.Deserialize<SortedDictionary<string, string>>(json);
            reader.Close();
        }
        public async Task<bool> UpdateConfigFile()
        {
            try
            {
                if (Settings != null && Settings.Count > 0)
                {
                    using FileStream createStream = File.OpenWrite(ConfigFile);
                    await JsonSerializer.SerializeAsync(createStream, Settings);
                    await createStream.DisposeAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public static async void SetConfigFile(string key, string value)
        {
            StreamReader reader = new StreamReader(ConfigFile);
            var json = reader.ReadToEnd();
            var config = JsonSerializer.Deserialize<SortedDictionary<string, string>>(json);
            reader.Close();
            if (config == null)
                config = new SortedDictionary<string, string>();
            if (config.ContainsKey(key))
                key = key + $"#{config.Count + 1}";
            config.Add(key, value);
            using FileStream createStream = File.OpenWrite(ConfigFile);
            await JsonSerializer.SerializeAsync(createStream, config);
            await createStream.DisposeAsync();

        }

        public SortedDictionary<string, string> Settings = new SortedDictionary<string, string>();
        public async Task<bool> ProcessOperation(string store, string ops)
        {
            if (Settings == null || Settings.Count <= 0)
                ReadSetting();
            bool flag = false;

            switch (ops)
            {
                case "Category":
                    flag = await CreateCategoriesAsync(Settings.GetValueOrDefault("VoyPurchase"));
                    break; ;
                case "ProductItem":
                    flag = await GenerateProductItemfromPurchase(Settings.GetValueOrDefault("VoyPurchase"));
                    break;
                case "PurchaseInvoice":
                    flag = await GeneratePurchaseInvoice(store, Settings.GetValueOrDefault("VoyPurchase"));
                    break;
                case "PurchaseItem":
                    flag = await GeneratePurchaseItemAsync(store, Settings.GetValueOrDefault("VoyPurchase")); break;
                case "ToVoyPurchase":
                    flag = await ToVoyPurchaseAsync();
                    break;
                case "SaleInvoice":
                case "SaleInvoiceItem":
                case "Stocks":
                    flag = await GenerateStockfromPurchase(Settings.GetValueOrDefault("VoyPurchase"), store); break;
                case "InnerWearPurchase":

                default:
                    break;
            }
            if (flag) UpdateConfigFile();
            return flag;
        }
        public DataTable LoadJsonFile(string ops)
        {
            DataTable dt = null;
            switch (ops)
            {
                case "Category":
                    //flag = await CreateCategoriesAsync(Settings.GetValueOrDefault("Purchase"));
                    break; ;
                case "ProductItem":
                case "PurchaseInvoice":
                    //flag = await GeneratePurchaseInvoice(store, Settings.GetValueOrDefault("Purchase"));
                    break;
                case "PurchaseItem":
                //flag = await GeneratePurchaseItemAsync(store, Settings.GetValueOrDefault("Purchase")); break;
                case "ToVoyPurchase":
                    dt = ImportData.JSONFileToDataTable(Settings.GetValueOrDefault("VoyPurchase")); break;

                case "SaleInvoice":
                case "SaleInvoiceItem":
                case "Stock":
                case "InnerWearPurchase":

                default:
                    break;
            }
            return dt;
        }
        private async Task<bool> ToVoyPurchaseAsync()
        {
            try
            {
                if (Settings == null || Settings.Count <= 0)
                    ReadSetting();

                var datatable = ImportData.JSONFileToDataTable(Settings.GetValueOrDefault("Purchase"));
                var json = ImportData.PurchaseDatatableToJson(datatable);
                string filename = Path.Combine(Settings.GetValueOrDefault("BasePath"), @"Purchase\VoyPurchase.json");
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                Settings.Add("VoyPurchase", filename);
                File.WriteAllText(filename, json);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        private void SetCategoryList()
        {

            if (ProductTypes == null)
                ProductTypes = ImportData.JsonToObject<ProductType>(Settings.GetValueOrDefault("ProductType"));
            if (ProductSubCategories == null)
                ProductSubCategories = ImportData.JsonToObject<ProductSubCategory>(Settings.GetValueOrDefault("SubCategory"));

            if (ProductCategories.Count <= 0)
            {
                var Cat = ImportData.JsonToObject<PCat>(Settings.GetValueOrDefault("ProductCategory"));
                foreach (var item in Cat)
                {
                    ProductCategories.Add(item.Name, SetProductCategory(item.Name));
                }
            }

        }


        /// <summary>
        /// It will import all excel file to json and save to a folder for futher process
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="sheetName"></param>
        /// <param name="startCol"></param>
        /// <param name="startRow"></param>
        /// <param name="maxRow"></param>
        /// <param name="maxCol"></param>
        /// <param name="outputfilename"></param>
        public static async Task<bool> StartImporting(string storecode, string filename, string sheetName, int startCol, int startRow, int maxRow, int maxCol, string outputfilename, string fileType)
        {
            var datatable = ImportData.ReadExcelToDatatable(filename, sheetName, startRow, startCol, maxRow, maxCol);
            var fn = Path.Combine(Path.GetDirectoryName(outputfilename) + $@"\{storecode}\ImportedJSON", Path.GetFileName(outputfilename) + ".json");
            SetConfigFile(fileType, fn);
            return await ImportData.DataTableToJSONFile(datatable, fn);
        }

        public async void StartPorocessor(string store)
        {
            string PurchaseFileName = "";
            string SaleFileName = "";
            // First Create Product and Sale JSON File , 
            // Then Start Processing

            //1st Creating Category/ Size/ Sub Category
            var flag = await CreateCategoriesAsync(PurchaseFileName);
            if (!flag) return;
            //Creating Product Item 

            //flag = await GenerateProductItem(PurchaseFileName,SaleFileName );
            //if(!flag) return; 

            //Creating Purchase Invoice
            flag = await GeneratePurchaseInvoice(store, PurchaseFileName);
            if (!flag) return;
            //Creating Purchase Item
            flag = await GeneratePurchaseItemAsync(store, PurchaseFileName);
            if (!flag) return;
            //Creating Sale Item 
            flag = await GenerateSaleInvoice(store, SaleFileName);
            flag = await GenerateSaleItem(store, SaleFileName);
            flag = await GenerateSalePayment(store, SaleFileName);
            if (!flag) return;
            //Creating Stock 
            flag = await GenerateStockData(store, PurchaseFileName, SaleFileName);
            if (!flag) return;

            // Create DailSale Here

            //Create a Structure and Store in Single Json File So it become easy to parse and process. 
            //Or make a file whcih can process in single go

        }

        private async Task<bool> CreateCategoriesAsync(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);

            var categories = purchases.GroupBy(c => c.ProductName).Select(c => new { KK = c.Key.Split("/") }).ToList();

            foreach (var category in categories)
            {
                Cat1.Add(category.KK[0]);
                Cat2.Add(category.KK[1]);
                Cat3.Add(category.KK[2]);
            }




            Cat1 = Cat1.Distinct().ToList();
            Cat2 = Cat2.Distinct().ToList();
            Cat3 = Cat3.Distinct().ToList();

            List<Category> categoriesList = new List<Category>();
            foreach (var item in Cat1)
            {
                Category c = new Category { Name = item };
                categoriesList.Add(c);
            }
            int count = 0;
            List<ProductType> pTypes = new List<ProductType>();
            foreach (var cat in Cat2)
            {
                ProductType pType = new ProductType { ProductTypeId = $"PT00{++count}", ProductTypeName = cat };
                pTypes.Add(pType);
            }
            count = 0;

            List<ProductSubCategory> catList = new List<ProductSubCategory>();
            foreach (var cat in Cat3)
            {
                ProductSubCategory cato = new ProductSubCategory { SubCategory = cat, ProductCategory = ProductCategory.Others };
                catList.Add(cato);
            }
            var path = Settings.GetValueOrDefault("BasePath") + @"/Category";
            Directory.CreateDirectory(path);

            using FileStream createStream = File.Create(path + @"/SubCategory.json");
            await JsonSerializer.SerializeAsync(createStream, catList);
            Settings.Add("SubCategory", path + @"/SubCategory.json");
            await createStream.DisposeAsync();


            using FileStream createStream2 = File.Create(path + @"/productTypes.json");
            await JsonSerializer.SerializeAsync(createStream2, pTypes);
            Settings.Add("ProductType", path + @"/productTypes.json");
            await createStream.DisposeAsync();

            using FileStream createStream3 = File.Create(path + @"/productcategory.json");
            await JsonSerializer.SerializeAsync(createStream3, categoriesList);
            Settings.Add("ProductCategory", path + @"/productcategory.json");
            await createStream.DisposeAsync();

            return true;


        }


        private void GenerateDailySale(string code, string filename, string filename2)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            var sales = JsonSerializer.Deserialize<List<ProductSale>>(json);
            reader = new StreamReader(filename2);
            var payments = JsonSerializer.Deserialize<List<SalePaymentDetail>>(json);
            List<DailySale> DailySales = new List<DailySale>();

            foreach (var i in sales)
            {
                DailySale sale = new DailySale
                {
                    Amount = i.BilledQty,
                    CashAmount = 0,
                    EntryStatus = EntryStatus.Rejected,
                    IsDue = false,
                    IsReadOnly = false,
                    ManualBill = false,
                    OnDate = i.OnDate,
                    InvoiceNumber = i.InvoiceNo,
                    PayMode = PayMode.Cash,
                    Remarks = "AUTO GENE",
                    SalesmanId = i.SalesmanId,
                    MarkedDeleted = false,
                    NonCashAmount = 0,
                    SalesReturn = i.InvoiceType == InvoiceType.SalesReturn ? true : false,
                    StoreId = i.StoreId,
                    UserId = "AUTOGINI",
                    TailoringBill = false,
                };
                DailySales.Add(sale);
            }
            foreach (var i in payments)
            {
                var d = DailySales.Where(c => c.InvoiceNumber == i.InvoiceCode);
            }
        }

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


        public async Task<bool> GenerateStockfromPurchase(string filename, string code)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);
            var stocks = purchases.GroupBy(x => new { x.Barcode, x.MRP, x.ProductName, x.ItemDesc, x.StyleCode })
                    .Select(c => new
                    {
                        c.Key.StyleCode,
                        c.Key.ItemDesc,
                        c.Key.Barcode,
                        c.Key.ProductName,
                        c.Key.MRP,
                        Costs = c.Select(x => new { x.SupplierName, x.Cost, Qty = c.Sum(z => z.Quantity) }).ToList()
                    }).ToList();
            List<ProductStock> products = new List<ProductStock>();
            SetCategoryList();
            if (sizeList == null) sizeList = Enum.GetNames(typeof(Size)).ToList();
            foreach (var s in stocks)
            {


                if (s.Costs.Count > 1)
                {
                    //Multivalue
                    foreach (var cost in s.Costs)
                    {
                        if (cost.SupplierName.Contains("Aprajita") == false)
                        {
                            ProductStock productStock = new ProductStock
                            {
                                Unit = SetUnit(s.ProductName),
                                StoreId = code,
                                Barcode = s.Barcode,
                                HoldQty = 0,
                                MRP = s.MRP,
                                MultiPrice = true,
                                PurhcaseQty = cost.Qty,
                                SoldQty = 0,
                                CostPrice = cost.Cost,
                            };
                            products.Add(productStock);
                        }
                    }
                }
                else
                {
                    ProductStock productStock = new ProductStock
                    {
                        Unit = SetUnit(s.ProductName),
                        StoreId = code,
                        Barcode = s.Barcode,
                        HoldQty = 0,
                        MRP = s.MRP,

                        MultiPrice = false,
                        PurhcaseQty = s.Costs[0].Qty,
                        SoldQty = 0,
                        CostPrice = s.Costs[0].Cost,
                    };
                    products.Add(productStock);


                }
            }

            var saveFileName = Path.Combine(Path.Combine(Settings.GetValueOrDefault("BasePath"), "Products"), "ProductStocks.json");
            var flag = await ImportData.ObjectsToJSONFile<ProductStock>(products, saveFileName);
            Settings.Add("ProductStocks", saveFileName);
            return flag;


        }

        public async Task<bool> GenerateProductItemfromPurchase(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);

                var stocks = purchases.GroupBy(x => new { x.Barcode, x.MRP, x.ProductName, x.ItemDesc, x.StyleCode })
                    .Select(c => new { c.Key.StyleCode, c.Key.ItemDesc, c.Key.Barcode, c.Key.ProductName, c.Key.MRP, }).ToList();
                List<ProductItem> products = new List<ProductItem>();
                SetCategoryList();
                if (sizeList == null) sizeList = Enum.GetNames(typeof(Size)).ToList();
                foreach (var s in stocks)
                {
                    var cats = s.ProductName.Split('/');
                    ProductItem p = new ProductItem
                    {
                        Barcode = s.Barcode,
                        StyleCode = s.StyleCode,
                        Name = s.ProductName,
                        MRP = s.MRP,
                        TaxType = TaxType.GST,
                        Unit = SetUnit(s.ProductName),
                        Description = s.ItemDesc,
                        SubCategory = cats[2],
                        ProductCategory = ProductCategories.GetValueOrDefault(cats[0]),
                        ProductTypeId = ProductTypes.Where(c => c.ProductTypeName == cats[1]).FirstOrDefault().ProductTypeId,
                        BrandCode = SetBrandCode(s.StyleCode, cats[0], cats[1]),
                        Size = SetSize(s.StyleCode, cats[2]),
                        HSNCode = "NA"

                    };
                    products.Add(p);
                }

                var saveFileName = Path.Combine(Path.Combine(Settings.GetValueOrDefault("BasePath"), "Products"), "ProductItems.json");
                var flag = await ImportData.ObjectsToJSONFile<ProductItem>(products, saveFileName);
                Settings.Add("ProdutItems", saveFileName);
                return flag;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Size SetSize(string style, string category)
        {
            Size size = Size.NOTVALID;
            var name = style.Substring(style.Length - 4, 4);

            // Jeans and Trousers

            if (category.Contains("Boxer") || category.Contains("Socks") || category.Contains("H-Shorts") || category.Contains("Shirt") || category.Contains("Vests") || category.Contains("Briefs") || category.Contains("Jackets") || category.Contains("Sweat Shirt") || category.Contains("Sweater") || category.Contains("T shirts"))
            {
                if (name.EndsWith(Size.M.ToString())) size = Size.M;
                else if (name.EndsWith(Size.S.ToString())) size = Size.S;
                else if (name.EndsWith(Size.XXXL.ToString())) size = Size.XXXL;
                else if (name.EndsWith(Size.XXL.ToString())) size = Size.XXL;
                else if (name.EndsWith(Size.XL.ToString())) size = Size.XL;
                else if (name.EndsWith(Size.L.ToString())) size = Size.L;

                else if (name.EndsWith("FS")) size = Size.FreeSize;
                else
                {
                    var nn = name.Substring(name.Length - 2).Trim();
                    int nx = 0;
                    if (Int32.TryParse(nn, out nx))
                    {
                        size = nx switch
                        {
                            39 => Size.S,
                            40 => Size.M,
                            42 => Size.L,
                            44 => Size.XL,
                            46 => Size.XXL,
                            48 => Size.XXXL,
                            _ => Size.NOTVALID,
                        };
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
        private ProductCategory SetProductCategory(string cat)
        {
            switch (cat)
            {
                case "Shirting":
                case "Suiting": return ProductCategory.Fabric;
                case "Apparel": return ProductCategory.Apparel;
                case "Promo": return ProductCategory.PromoItems;
                case "Suit Cover": return ProductCategory.SuitCovers;
                case "Accessories": return ProductCategory.Accessiories;
                case "Tailoring": return ProductCategory.Tailoring;

                default:
                    return ProductCategory.Others;

            }

        }

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

        private Unit SetUnit(string pname)
        {
            if (pname.StartsWith("Suiting") || pname.StartsWith("Shirting")) return Unit.Meters;
            else if (pname.StartsWith("Apparel")) return Unit.Pcs;
            else if (pname.StartsWith("Promo") || pname.StartsWith("Suit Cover")) return Unit.Nos;
            else return Unit.Nos;
        }


        public async Task<bool> GeneratePurchaseInvoice(string storecode, string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);

                var invoices = purchases.GroupBy(c => new { c.InvoiceNo, c.GRNNo, c.InvoiceDate, c.GRNDate })
                    .Select(c => new PurchaseProduct
                    {
                        VendorId = VendorMapping(c.Select(x => x.SupplierName).First()),
                        InvoiceType = PurchaseInvoiceType.Purchase,
                        Count = c.Count(),
                        TotalQty = c.Sum(x => x.Quantity),
                        InwardDate = c.Key.GRNDate,
                        UserId = "AUTOGINI",
                        Warehouse = c.Select(x => x.SupplierName).First(),
                        TotalAmount = (decimal)-0.0001,
                        OnDate = c.Key.InvoiceDate,
                        InvoiceNo = c.Key.InvoiceNo,
                        InwardNumber = c.Key.GRNNo,
                        TaxType = c.Key.InvoiceDate < new DateTime(2017, 7, 1) ? TaxType.VAT : TaxType.IGST,
                        BillQty = c.Sum(x => x.Quantity),
                        StoreId = storecode,
                        BasicAmount = c.Sum(x => x.CostValue),
                        ShippingCost = 0,
                        TaxAmount = c.Sum(x => x.TaxAmt),
                        DiscountAmount = 0,
                        Paid = true,
                        EntryStatus = EntryStatus.Rejected,
                        FreeQty = 0,
                        IsReadOnly = false,
                        MarkedDeleted = false
                    }).ToList();

                using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/PurchaseInvoice.json");
                await JsonSerializer.SerializeAsync(createStream, invoices);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GeneratePurchaseItemAsync(string storecode, string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);
                List<PurchaseItem> purchaseItems = new List<PurchaseItem>();
                foreach (var inv in purchases)
                {
                    PurchaseItem pItem = new PurchaseItem
                    {
                        Barcode = inv.Barcode,
                        CostPrice = inv.CostValue,
                        CostValue = inv.CostValue,
                        DiscountValue = 0,
                        FreeQty = 0,
                        InwardNumber = inv.GRNNo,
                        Qty = inv.Quantity,
                        TaxAmount = inv.TaxAmt,
                        Unit = Unit.Nos
                    };
                    purchaseItems.Add(pItem);
                }

                using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/PurchaseItem.json");
                await JsonSerializer.SerializeAsync(createStream, purchaseItems);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> GenerateSaleInvoice(string code, string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var sales = JsonSerializer.Deserialize<List<JsonSale>>(json);

                int count = 0;
                var saleinvs = sales.GroupBy(c => new { c.InvoiceNumber, c.InvoiceDate, c.InvoiceType, c.PaymentMode, c.SalesmanName })
                    .Select(c => new ProductSale
                    {
                        InvoiceNo = c.Key.InvoiceNumber,
                        OnDate = DateTime.Parse(c.Key.InvoiceDate),
                        BilledQty = c.Sum(x => x.Quantity),
                        Adjusted = false,
                        EntryStatus = EntryStatus.Rejected,
                        FreeQty = 0,
                        StoreId = code,
                        TotalBasicAmount = c.Sum(x => x.BasicRate),
                        Tailoring = false,
                        Taxed = true,
                        InvoiceType = c.Key.InvoiceType == "SALES" ? InvoiceType.Sales : InvoiceType.SalesReturn,
                        SalesmanId = Salesman.Where(x => x.Value == c.Key.SalesmanName).First().Key,
                        TotalMRP = c.Sum(x => x.MRPValue),
                        TotalDiscountAmount = c.Sum(x => x.Discount),
                        UserId = "AUTOJINI",
                        TotalTaxAmount = c.Sum(x => x.TaxAmount),
                        TotalPrice = c.Sum(x => x.BillAmount),
                        InvoiceCode = $@"{code}/{DateTime.Parse(c.Key.InvoiceDate).ToString("yyyy/MMM/DD")}/00{++count}",
                        IsReadOnly = false,
                        MarkedDeleted = false,
                        Paid = true,
                        RoundOff = c.Sum(x => x.RoundOff),
                    }).ToList();

                using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/saleinvoice.json");
                await JsonSerializer.SerializeAsync(createStream, saleinvs);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> GenerateSaleItem(string code, string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var sales = JsonSerializer.Deserialize<List<JsonSale>>(json);
                // List<SaleItem> saleItems = new List<SaleItem>();
                // int count = 0;
                var saleinvs = sales.Select(c => new SaleItem
                {
                    Barcode = c.Barcode,
                    BasicAmount = c.BasicRate,
                    DiscountAmount = c.Discount,
                    LastPcs = false,
                    TaxAmount = c.TaxAmount,
                    TaxType = TaxType.GST,
                    Unit = Unit.Nos,
                    Value = c.LineTotal,
                    BilledQty = c.Quantity,
                    Adjusted = false,
                    InvoiceType = c.InvoiceType == "SALES" ? InvoiceType.Sales : InvoiceType.SalesReturn,
                    FreeQty = 0,
                    InvoiceCode = c.InvoiceNumber,
                }).ToList();

                using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/saleitems.json");
                await JsonSerializer.SerializeAsync(createStream, saleinvs);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> GenerateSalePayment(string code, string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            var sales = JsonSerializer.Deserialize<List<JsonSale>>(json);

            var cashsale = sales.Where(c => c.PaymentMode == "CAS")
                .Select(c => new SalePaymentDetail
                {
                    InvoiceCode = c.InvoiceNumber,
                    PaidAmount = c.BillAmount,
                    PayMode = PayMode.Cash,
                    RefId = "CASH SALE",
                })
                .ToList();

            var cardsale = sales.Where(c => c.PaymentMode == "CRD").Select(c => new SalePaymentDetail
            {
                InvoiceCode = c.InvoiceNumber,
                PaidAmount = c.BillAmount,
                PayMode = PayMode.Card,
                RefId = "CARD SALE",
            }).ToList();
            var mixsale = sales.Where(c => c.PaymentMode == "MIX").Select(c => new SalePaymentDetail
            {
                InvoiceCode = c.InvoiceNumber,
                PaidAmount = c.BillAmount,
                PayMode = PayMode.MixPayments,
                RefId = "Mix Sale",
            }).ToList();
            var salereturn = sales.Where(c => c.PaymentMode == "SR").Select(c => new SalePaymentDetail
            {
                InvoiceCode = c.InvoiceNumber,
                PaidAmount = c.BillAmount,
                PayMode = PayMode.SaleReturn,
                RefId = "SALE Return",
            }).ToList();
            List<SalePaymentDetail> list = new List<SalePaymentDetail>();
            list.AddRange(cashsale);
            list.AddRange(cardsale);
            list.AddRange(mixsale);
            list.AddRange(salereturn);
            using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/salepayment.json");
            await JsonSerializer.SerializeAsync(createStream, list);
            await createStream.DisposeAsync();
            return true;
        }

        public async Task<bool> GenerateStockData(string code, string pfile, string sfile)
        {
            StreamReader reader = new StreamReader(pfile);
            var json = reader.ReadToEnd();

            var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);
            reader = new StreamReader(sfile);
            json = reader.ReadToEnd();

            var sales = JsonSerializer.Deserialize<List<JsonSale>>(json);

            var pp = purchases.Where(c => !c.SupplierName.Contains("Aprajita")).GroupBy(c => c.Barcode).Select(c => new
            {
                Barcode = c.Key,
                Qty = c.Sum(x => x.Quantity),
                Cost = c.Select(x => x.Cost).First(),
                Mrp = c.Select(x => x.MRP).First()
            }).ToList();

            var ss = sales.GroupBy(c => c.Barcode).Select(c => new
            {
                Barcode = c.Key,
                Qty = c.Sum(x => x.Quantity),
                Mrp = c.Select(x => x.MRP).First(),
            }).ToList();

            var x = from p in pp
                    join s in ss on p.Barcode equals s.Barcode
                    select new
                    {
                        Stock = new Stock
                        {
                            Barcode = p.Barcode,
                            CostPrice = p.Cost,
                            HoldQty = 0,
                            EntryStatus = EntryStatus.Rejected,
                            IsReadOnly = false,
                            MarkedDeleted = false,
                            MRP = p.Mrp,
                            MultiPrice = false,
                            PurhcaseQty = p.Qty,
                            SoldQty = s.Qty,
                            StoreId = code,
                            UserId = "AutoJini",
                            Unit = Unit.Nos
                        }
                    };

            using FileStream createStream = File.Create(Path.GetDirectoryName(pfile) + @"/stocks.json");
            await JsonSerializer.SerializeAsync(createStream, x);
            await createStream.DisposeAsync();
            return true;
        }


        private void CleanupMultiPriceForInterStoreTransfer()
        {
            var Stocks = ImportData.JsonToObject<ProductStock>(Settings.GetValueOrDefault("ProductStocks")).Where(c => c.MultiPrice).ToList();
            var Purchase = ImportData.JsonToObject<VoyPurhcase>(Settings.GetValueOrDefault("VoyPurchase"));
            var filter = Stocks.GroupBy(c => c.Barcode).Select(c => c.Key).ToList();
            foreach (var barcode in filter)
            {
                var purchases = Purchase.Where(c => c.Barcode == barcode).ToList();
                foreach (var item in purchases)
                {
                    if (item.SupplierName.Contains("Aprajita"))
                    {
                        var stk = Stocks.Where(c => c.Barcode == barcode && c.CostPrice == item.Cost).First();
                        Stocks.Remove(stk);
                    }
                }

            }



        }

        private void CleanUpMutliPriceStock()
        {
            var Stocks = ImportData.JsonToObject<ProductStock>(Settings.GetValueOrDefault("ProductStocks")).Where(c => c.MultiPrice);

            var filter = Stocks.GroupBy(c => c.Barcode).Select(c => new { c.Key, CostPrices = c.Select(x => new { x.CostPrice, x.PurhcaseQty }).OrderBy(z => z.CostPrice).ToList() }).ToList();
            List<ProductStock> StockList = new List<ProductStock>();
            foreach (var s in filter)
            {
                var costPrice = s.CostPrices.Select(c => c.CostPrice).Distinct().ToList();
                if (costPrice.Count() != s.CostPrices.Count)
                {

                    if (costPrice.Count() == 1)
                    {
                        ProductStock stock = new ProductStock
                        {
                            Barcode = s.Key,
                            CostPrice = costPrice[0],
                            PurhcaseQty = s.CostPrices.Sum(x => x.PurhcaseQty),
                            MultiPrice = false,
                            HoldQty = 0,
                            SoldQty = 0,

                        };
                        StockList.Add(stock);
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (costPrice.Count() < s.CostPrices.Count)
                    {

                    }
                    else if (costPrice.Count() == s.CostPrices.Count)
                    {

                    }
                    else
                    {

                    }

                }

            }



        }
    }


}