using AKS.Shared.Commons.Models.Inventory;
using System.Data;
using System.IO.Compression;
using System.Text.Json;

namespace eStore.SetUp.Import
{
    public class ImportingPurchase
    {
        private string BasePath;
        private ImportSetting ImportSetting;

        private string StoreCode;

        /*
       Move this to Database or JSON Dump
        */

        private async Task<bool> UpdateDatabase()
        {
            var bpath = Path.Combine(ImportBasic.GetSetting("BackUppath"), "Purchases");

            var filename = ImportBasic.GetSetting("PurchaseInvoices");
            var backupfilename = Path.Combine(bpath, "PurchaseInovices.json");
            bool flag = await ImportData.UpdateBackKupJson<PurchaseProduct>(filename, backupfilename);

            filename = ImportBasic.GetSetting("PurchaseItems");
            backupfilename = Path.Combine(bpath, "PurchaseItems.json");
            flag = await ImportData.UpdateBackKupJson<PurchaseItem>(filename, backupfilename);

            filename = ImportBasic.GetSetting("Stocks");
            backupfilename = Path.Combine(bpath, "Stocks.json");
            flag = await ImportData.UpdateBackKupJson<ProductStock>(filename, backupfilename);

            bpath = Path.Combine(ImportBasic.GetSetting("BackUppath"), "Commons");
            filename = ImportBasic.GetSetting("ProductItems");
            backupfilename = Path.Combine(bpath, "ProductItems.json");
            flag = await ImportData.UpdateBackKupJson<ProductItem>(filename, backupfilename);

            filename = ImportBasic.GetSetting("ProductSubCategory");
            backupfilename = Path.Combine(bpath, "ProductSubCategory.json");
            flag = await ImportData.UpdateBackKupJson<ProductSubCategory>(filename, backupfilename);

            filename = ImportBasic.GetSetting("ProductType");
            backupfilename = Path.Combine(bpath, "ProductTypes.json");
            flag = await ImportData.UpdateBackKupJson<ProductType>(filename, backupfilename);

            return flag;
        }

        private bool BackupJSon()
        {
            try
            {
                string filename = "", path = "";
                ZipFile.CreateFromDirectory(path, filename, CompressionLevel.Fastest, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> StartImportingPurchase(string storeCode, string settingName, string basePath)
        {
            BasePath = basePath;
            StoreCode = storeCode;
            string PurchaseFilename = settingName;// ImportBasic.GetSetting(settingName);
            bool flag = await CreateCategoriesAsync(PurchaseFilename);
            flag = SyncWithCategories();
            flag = await GenerateProductItemfromPurchase(PurchaseFilename);
            flag = await GeneratePurchaseInvoice(StoreCode, PurchaseFilename);
            flag = await GeneratePurchaseItemAsync(storeCode, PurchaseFilename);
            flag = await GenerateStockfromPurchase(PurchaseFilename, StoreCode);
            flag = await UpdatePurchaseStock();
            flag = await UpdateShippingCost();
            flag = BackupJSon();
            return flag;

        }

        private async Task<bool> CreateCategoriesAsync(string filename)
        {
            var purchases = ImportData.JsonToObject<VoyPurhcase>(filename);
            var categories = purchases.GroupBy(c => c.ProductName).Select(c => new { KK = c.Key.Split("/") }).ToList();
            List<string> Cat1 = new List<string>();
            List<string> Cat2 = new List<string>();
            List<string> Cat3 = new List<string>();
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
            var path = ImportBasic.GetSetting("BasePath") + @"/Category";
            Directory.CreateDirectory(path);

            using FileStream createStream = File.Create(path + @"/SubCategory.json");
            await JsonSerializer.SerializeAsync(createStream, catList);
            ImportBasic.AddSetting("SubCategory", path + @"/SubCategory.json");
            await createStream.DisposeAsync();

            using FileStream createStream2 = File.Create(path + @"/productTypes.json");
            await JsonSerializer.SerializeAsync(createStream2, pTypes);
            ImportBasic.AddSetting("ProductType", path + @"/productTypes.json");
            await createStream.DisposeAsync();

            using FileStream createStream3 = File.Create(path + @"/productcategory.json");
            await JsonSerializer.SerializeAsync(createStream3, categoriesList);
            ImportBasic.AddSetting("ProductCategory", path + @"/productcategory.json");
            await createStream.DisposeAsync();

            return true;
        }

        private async Task<bool> GenerateProductItemfromPurchase(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);

                var stocks = purchases.GroupBy(x => new { x.Barcode, x.MRP, x.ProductName, x.ItemDesc, x.StyleCode })
                    .Select(c => new { c.Key.StyleCode, c.Key.ItemDesc, c.Key.Barcode, c.Key.ProductName, c.Key.MRP, }).ToList();

                List<ProductItem> products = new List<ProductItem>();
                ImportHelpers.SetCategoryList();

                if (ImportHelpers.SizeList == null) ImportHelpers.SizeList = Enum.GetNames(typeof(Size)).ToList();

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
                        Unit = ImportHelpers.SetUnit(s.ProductName),
                        Description = s.ItemDesc,
                        SubCategory = cats[2],
                        ProductCategory = ImportHelpers.ProductCategories.GetValueOrDefault(cats[0]),
                        ProductTypeId = ImportHelpers.ProductTypes.Where(c => c.ProductTypeName == cats[1]).FirstOrDefault().ProductTypeId,
                        BrandCode = ImportHelpers.SetBrandCode(s.StyleCode, cats[0], cats[1]),
                        Size = ImportHelpers.SetSize(s.StyleCode, cats[2]),
                        HSNCode = "NA"
                    };
                    products.Add(p);
                }

                var saveFileName = Path.Combine(Path.Combine(ImportBasic.GetSetting("BasePath"), "Products"), "ProductItems.json");
                var flag = await ImportData.ObjectsToJSONFile<ProductItem>(products, saveFileName);
                ImportBasic.AddSetting("ProdutItems", saveFileName);
                return flag;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> GeneratePurchaseInvoice(string storecode, string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);

                var invoices = purchases.GroupBy(c => new { c.InvoiceNo, c.GRNNo, c.InvoiceDate, c.GRNDate })
                    .Select(c => new PurchaseProduct
                    {
                        VendorId = ImportHelpers.VendorMapping(c.Select(x => x.SupplierName).First()),
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

                foreach (var item in invoices)
                {
                    item.TotalAmount = item.TaxAmount + item.BasicAmount;
                }

                using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/PurchaseInvoice.json");
                await JsonSerializer.SerializeAsync(createStream, invoices);
                await createStream.DisposeAsync();
                ImportBasic.AddSetting("Purchase-Invoices", Path.GetDirectoryName(filename) + @"/PurchaseInvoice.json");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> GeneratePurchaseItemAsync(string storecode, string filename)
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
                        InwardNumber = inv.InvoiceNo,
                        Qty = inv.Quantity,
                        TaxAmount = inv.TaxAmt,
                        Unit = ImportHelpers.SetUnit(inv.ProductName)
                    };
                    purchaseItems.Add(pItem);
                }

                using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/PurchaseItem.json");
                await JsonSerializer.SerializeAsync(createStream, purchaseItems);
                await createStream.DisposeAsync();
                ImportBasic.AddSetting("PurchaseItem", Path.GetDirectoryName(filename) + @"/PurchaseItem.json");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [Obsolete]
        private async Task<bool> GenerateStockData(string code, string pfile, string sfile)
        {
            StreamReader reader = new StreamReader(pfile);
            var json = reader.ReadToEnd();

            var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);
            reader = new StreamReader(sfile);
            json = reader.ReadToEnd();

            var sales = JsonSerializer.Deserialize<List<JsonSale>>(json);

            var pp = purchases.GroupBy(c => c.Barcode).Select(c => new
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

        private async Task<bool> GenerateStockfromPurchase(string filename, string code)
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
                        Costs = c.Select(x => new { x.SupplierName, x.Cost }).ToList()
                    }).ToList();

            List<ProductStock> products = new List<ProductStock>();
            ImportHelpers.SetCategoryList();

            if (ImportHelpers.SizeList == null) ImportHelpers.SizeList = Enum.GetNames(typeof(Size)).ToList();

            foreach (var s in stocks)
            {
                if (s.Costs.Count > 1)
                {
                    var costs = s.Costs.DistinctBy(c => c.Cost).ToList();

                    if (costs.Count == 1)
                    {
                        ProductStock productStock = new ProductStock
                        {
                            Unit = ImportHelpers.SetUnit(s.ProductName),
                            StoreId = code,
                            Barcode = s.Barcode,
                            HoldQty = 0,
                            MRP = s.MRP,
                            MultiPrice = false,
                            PurhcaseQty = 0,
                            SoldQty = 0,
                            CostPrice = costs[0].Cost,
                        };
                        products.Add(productStock);
                    }
                    else if (costs.Count == 2)
                    {
                        if (costs[0].SupplierName.Contains("Aprajita") || costs[1].SupplierName.Contains("Aprajita"))
                        {
                            ProductStock productStock = new ProductStock
                            {
                                Unit = ImportHelpers.SetUnit(s.ProductName),
                                StoreId = code,
                                Barcode = s.Barcode,
                                HoldQty = 0,
                                MRP = s.MRP,
                                MultiPrice = false,
                                PurhcaseQty = 0,
                                SoldQty = 0,
                                CostPrice = costs[0].Cost,
                            };
                            products.Add(productStock);
                        }
                        else
                        {
                            foreach (var cost in costs)
                            {
                                if (cost.SupplierName.Contains("Aprajita") == false)
                                {
                                    ProductStock productStock = new ProductStock
                                    {
                                        Unit = ImportHelpers.SetUnit(s.ProductName),
                                        StoreId = code,
                                        Barcode = s.Barcode,
                                        HoldQty = 0,
                                        MRP = s.MRP,
                                        MultiPrice = true,
                                        PurhcaseQty = 0,
                                        SoldQty = 0,
                                        CostPrice = cost.Cost,
                                    };
                                    products.Add(productStock);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Multivalue
                        foreach (var cost in costs)
                        {
                            if (cost.SupplierName.Contains("Aprajita") == false)
                            {
                                ProductStock productStock = new ProductStock
                                {
                                    Unit = ImportHelpers.SetUnit(s.ProductName),
                                    StoreId = code,
                                    Barcode = s.Barcode,
                                    HoldQty = 0,
                                    MRP = s.MRP,
                                    MultiPrice = true,
                                    PurhcaseQty = 0,
                                    SoldQty = 0,
                                    CostPrice = cost.Cost,
                                };
                                products.Add(productStock);
                            }
                        }
                    }
                }
                else
                {
                    if (s.Costs[0].SupplierName.Contains("Aprajita") == false)
                    {
                        ProductStock productStock = new ProductStock
                        {
                            Unit = ImportHelpers.SetUnit(s.ProductName),
                            StoreId = code,
                            Barcode = s.Barcode,
                            HoldQty = 0,
                            MRP = s.MRP,

                            MultiPrice = false,
                            PurhcaseQty = 0,
                            SoldQty = 0,
                            CostPrice = s.Costs[0].Cost,
                        };
                        products.Add(productStock);
                    }
                    else
                    {
                        ProductStock productStock = new ProductStock
                        {
                            Unit = ImportHelpers.SetUnit(s.ProductName),
                            StoreId = code,
                            Barcode = s.Barcode,
                            HoldQty = 0,
                            MRP = s.MRP,

                            MultiPrice = false,
                            PurhcaseQty = 0,
                            SoldQty = 0,
                            CostPrice = s.Costs[0].Cost
                        };
                        products.Add(productStock);
                    }
                }
            }

            products = products.Distinct().ToList();
            products = products.OrderBy(c => c.MultiPrice).ThenBy(c => c.Barcode).ThenBy(c => c.CostPrice).ToList();
            var saveFileName = Path.Combine(Path.Combine(ImportBasic.GetSetting("BasePath"), "Products"), "ProductStocks.json");
            var flag = await ImportData.ObjectsToJSONFile<ProductStock>(products, saveFileName);
            ImportBasic.AddOrUpdateSetting("ProductStocks", saveFileName);
            return flag;
        }

        [Obsolete]
        private async Task<bool> GetMultiPriceStock()
        {
            var Stocks = ImportData.JsonToObject<ProductStock>(ImportBasic.GetSetting("ProductStocks")).Where(c => c.MultiPrice)
                .GroupBy(c => c.Barcode).Select(c => c.Key)
                .ToList();
            var Purchase = ImportData.JsonToObject<VoyPurhcase>(ImportBasic.GetSetting("VoyPurchase"));
            List<VoyPurhcase> mPirce = new List<VoyPurhcase>();
            foreach (var item in Stocks)
            {
                var p = Purchase.Where(c => c.Barcode == item).ToList();
                mPirce.AddRange(p);
            }

            var filename = ImportBasic.GetSetting("BasePath") + @"\test\multistock.json";
            Directory.CreateDirectory(Path.GetDirectoryName(filename));

            var flag = await ImportData.ObjectsToJSONFile<VoyPurhcase>(mPirce, filename);
            return flag;
        }

        private void ReadImportSettings()
        {
            ImportSetting = new ImportSetting();
        }

        private bool SyncWithCategories()
        {
            //TODO: add or Update Current List need is need.
            if (!string.IsNullOrEmpty(ImportSetting.ProductType))
            {
                var pTs = ImportData.JsonToObject<ProductType>(ImportSetting.ProductType);
            }
            if (!string.IsNullOrEmpty(ImportSetting.ProductSubCategory))
            {
                var pscs = ImportData.JsonToObject<ProductSubCategory>(ImportSetting.ProductSubCategory);
            }
            return true;
        }

        /* End ot Json Dump*/

        private async Task<bool> UpdatePurchaseStock()
        {
            var Stocks = ImportData.JsonToObject<ProductStock>(ImportBasic.GetSetting("ProductStocks")).ToList();
            var Purchases = ImportData.JsonToObject<VoyPurhcase>(ImportBasic.GetSetting("VoyPurchase")).ToList();

            foreach (var item in Purchases)
            {
                var stock = Stocks.Where(c => c.Barcode == item.Barcode && c.CostPrice == item.Cost).FirstOrDefault();
                if (stock != null)
                {
                    stock.PurhcaseQty += item.Quantity;
                }
            }

            await ImportData.ObjectsToJSONFile<ProductStock>(Stocks.Where(c => c.PurhcaseQty == 0).ToList(), ImportBasic.GetSetting("ProductStocks") + ".2");
            var flag = await ImportData.ObjectsToJSONFile<ProductStock>(Stocks, ImportBasic.GetSetting("ProductStocks"));

            return flag;
        }

        private async Task<bool> UpdateShippingCost()
        {
            var purchaseItems = ImportData.JsonToObject<PurchaseItem>(ImportBasic.GetSetting("PurchaseItem")).Where(c => c.Unit == Unit.Meters)
                .GroupBy(c => c.InwardNumber).Select(c => new { c.Key, Ship = (3 * c.Sum(x => x.Qty)) })
                .ToList();

            var Purchases = ImportData.JsonToObject<PurchaseProduct>(ImportBasic.GetSetting("Purchase-Invoices")).ToList();

            //var items= purchaseItems.GroupBy(c=>c.InwardNumber).Select(c=>new { c.Key, QTY=(3*c.Sum(x=>x.Qty))}).ToList();
            foreach (var item in purchaseItems)
            {
                var p = Purchases.Where(c => c.InvoiceNo == item.Key).FirstOrDefault();
                if (p != null)
                {
                    p.ShippingCost = Math.Round(item.Ship + (item.Ship * (decimal)0.05), 2);
                    p.TotalAmount += p.ShippingCost;
                    p.IsReadOnly = true;
                    p.MarkedDeleted = false; p.EntryStatus = EntryStatus.Updated;
                }
            }

            return await ImportData.ObjectsToJSONFile<PurchaseProduct>(Purchases, ImportBasic.GetSetting("Purchase-Invoices"));
        }
    }

    public class ImportParams
    {
        public string FileName { get; set; }
        public int MaxCol { get; set; }
        public int MaxRow { get; set; }
        public string SheetName { get; set; }
        public int StartCol { get; set; }
        public int StartRow { get; set; }
    }

    public class ImportSetting
    {
        public string ProductItem { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductType { get; set; }
        public string Vendors { get; set; }
    }
}