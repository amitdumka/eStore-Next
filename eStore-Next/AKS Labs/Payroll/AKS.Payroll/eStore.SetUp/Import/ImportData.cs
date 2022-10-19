using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Syncfusion.XlsIO;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text.Json;

namespace eStore.SetUp.Import
{
    /*
     Setup How to generate Data, 

    1) Category, Sub Category, Product Type, 
    2) Product Item, 
    3) Purhcase Invoice, Purchase Item 
    4) Sale Invoice , Sale Item 
    5) Payments
    6) Stocks 
     */

    /*
     
    json={ PurchaseFiles:[{filename:dasdas},{filename:dasdasda}], SaleFiles=[{filename:dasdasd}, {filename:adasdasd}], 

           PurchaseJson:dasdasd, SaleJson:dasdasdas, PurchaseItemJson:dasdasd
    
        }
     
     */


    public class ImportProcessor
    {
        private SortedDictionary<string, string> Salesman = new SortedDictionary<string, string>();
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
                    flag = await CreateCategoriesAsync(Settings.GetValueOrDefault("Purchase"));
                    break; ;
                case "ProductItem":
                case "PurchaseInvoice":
                    flag = await GeneratePurchaseInvoice(store, Settings.GetValueOrDefault("Purchase"));
                    break;
                case "PurchaseItem":
                    flag = await GeneratePurchaseItemAsync(store, Settings.GetValueOrDefault("Purchase")); break;
                case "ToVoyPurchase":
                    flag = await ToVoyPurchaseAsync();
                    break;
                case "SaleInvoice":
                case "SaleInvoiceItem":
                case "Stock":
                case "InnerWearPurchase":

                default:
                    break;
            }
            if(false) UpdateConfigFile();
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

            using FileStream createStream = File.Create(Path.GetDirectoryName(filename) + @"/SubCategory.json");
            await JsonSerializer.SerializeAsync(createStream, catList);
            await createStream.DisposeAsync();

            using FileStream createStream2 = File.Create(Path.GetDirectoryName(filename) + @"/productTypes.json");
            await JsonSerializer.SerializeAsync(createStream2, pTypes);
            await createStream.DisposeAsync();

            using FileStream createStream3 = File.Create(Path.GetDirectoryName(filename) + @"/productcategory.json");
            await JsonSerializer.SerializeAsync(createStream3, categoriesList);
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

        public void GenerateProductItem(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var purchases = JsonSerializer.Deserialize<List<VoyPurhcase>>(json);

                var stocks = purchases.GroupBy(c => c.Barcode)
                     .Select(c =>
                          c.Select(x => new { x.Barcode, x.Cost, x.MRP, x.ProductName, x.SupplierName, QTY = c.Sum(z => z.Quantity), x.StyleCode })
                     ).ToList()[0].ToList();
                List<ProductItem> products = new List<ProductItem>();
                foreach (var s in stocks)
                {
                    ProductItem p = new ProductItem
                    {
                        Barcode = s.Barcode,
                        StyleCode = s.StyleCode,
                        Name = s.ProductName,
                        MRP = s.MRP,
                        TaxType = TaxType.GST,
                        Unit = Unit.Nos,
                    };
                    products.Add(p);
                }
            }
            catch (Exception)
            {
                throw;
            }
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
    }

    public class ImportData
    {


        public static List<string> GetSheetNames(string filename)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
                List<string> names = new List<string>();
                foreach (var item in workbook.Worksheets)
                {
                    names.Add(item.Name);
                }
                return names;


            }
        }

        public static async Task<bool> DataTableToJSONFile(DataTable table, string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                //This help to Save as backup for future use
                using FileStream createStream = File.Create(fileName);

                // Register the custom converter
                JsonSerializerOptions options = new()
                { Converters = { new DataTableJsonConverter() }, WriteIndented = true };

                // Serialize a List<T> object and display the JSON
                //string jsonString = JsonSerializer.Serialize(table, options);
                await JsonSerializer.SerializeAsync(createStream, table, options);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }

        }
        public static SortedList<string, string> ConfigJson(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            var config = JsonSerializer.Deserialize<SortedList<string, string>>(json);
            reader.Close();
            return config;
        }

        public static DataTable JSONFileToDataTable(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            JsonSerializerOptions options = new()
            { Converters = { new DataTableJsonConverter() }, WriteIndented = true };
            var dataTable = JsonSerializer.Deserialize<DataTable>(json, options);
            return dataTable;
        }

        public static DataTable ReadExcelToDatatable(string filename, int fRow, int fCol, int MaxRow, int MaxCol)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
                IWorksheet worksheet = workbook.Worksheets[0];
                var x = worksheet.ExportDataTable(fRow, fCol, MaxRow, MaxCol, ExcelExportDataTableOptions.ColumnNames);
                var s = x.Rows.Count;
                return x;
            }
        }

        public static DateTime ToDateDMY(string ondate)
        {
            DateTime dt;
            if (DateTime.TryParse(ondate, out dt))
                return dt;

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

        public static DataTable ReadExcelToDatatable(string filename, string sheetName, int fRow, int fCol, int MaxRow, int MaxCol)
        {
            try
            {
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2013;
                    IWorkbook workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
                    IWorksheet worksheet = workbook.Worksheets[sheetName];
                    var x = worksheet.ExportDataTable(fRow, fCol, MaxRow, MaxCol, ExcelExportDataTableOptions.ColumnNames);
                    var s = x.Rows.Count;
                    return x;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void ImportPurchaseInvoice()
        {
        }

        public void ImportSaleInvoice()
        {
        }

        public void ImportMSDyncSaleInvoice()
        { }

        private void ProductItem()
        { }

        private void StockItem()
        { }

        private void Category()
        { }

        private void SaleItem()
        { }

        private void PurchaseItem()
        { }

        public enum SaleVMT
        { VOY, MD, MI }

        private string ReadSaleToJSON(string filename, string sheet, SaleVMT vMT)
        {
            List<string> list = new List<string>();
            var dataTable = ReadExcelToDatatable(filename, sheet, 1, 1, 1, 1);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];
                if (vMT == SaleVMT.VOY)
                {
                    var si = ReadVoySale(row);
                    list.Add(JsonSerializer.Serialize(si));
                }
                else if (vMT == SaleVMT.MD)
                {
                    var si = ReadMDSale(row);
                    list.Add(JsonSerializer.Serialize(si));
                }
                else if (vMT == SaleVMT.MI)
                {
                    var si = ReadMISale(row);
                    list.Add(JsonSerializer.Serialize(si));
                }
            }
            return JsonSerializer.Serialize(list);
        }


        public static string PurchaseDatatableToJson(DataTable dataTable)
        {

            List<VoyPurhcase> list = new List<VoyPurhcase>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
                list.Add(ReadPurchase(dataTable.Rows[i]));

            return JsonSerializer.Serialize(list);
        }

        private string ReadPurchaseToJSON(string filename, string sheet)
        {
            var dataTable = ReadExcelToDatatable(filename, sheet, 1, 1, 1, 1);
            List<VoyPurhcase> list = new List<VoyPurhcase>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
                list.Add(ReadPurchase(dataTable.Rows[i]));

            return JsonSerializer.Serialize(list);
        }

        private MISale ReadMISale(DataRow row)
        {
            return new MISale
            {
                Barcode = row["Barcode"].ToString(),
                BasicAmount = decimal.Parse(row["BasicAmount"].ToString()),
                LineTotal = decimal.Parse(row["BillAmount"].ToString()),
                DiscAmt = decimal.Parse(row["DiscAmt"].ToString()),
                BrandName = row["BrandName"].ToString(),
                Brand = row["BRAND"].ToString(),
                HSNCode = row["HSNCODE"].ToString(),
                InvNo = row["InvNo"].ToString(),
                InvType = row["BILL_TYPE"].ToString(),
                GSTAmount = decimal.Parse(row["GSTAmt"].ToString()),
                MRPValue = decimal.Parse(row["MRPValue"].ToString()),
                CostValue = decimal.Parse(row["CostValue"].ToString()),
                OnDate = DateTime.Parse(row["Date"].ToString()),
                Qty = decimal.Parse(row["Qty"].ToString()),
                TaxRate = decimal.Parse(row["TotalTaxRate"].ToString()),
                TotalTaxAmount = decimal.Parse(row["TotalTaxAmt"].ToString()),
                UnitCost = decimal.Parse(row["UnitCost"].ToString()),
                UnitMRP = decimal.Parse(row["UnitMRP"].ToString()),
                ItemDesc = row["DESCRIPTION"].ToString(),
                SalesManName = row["Salesman"].ToString(),
                Size = row["Size"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCate"].ToString(),
                ProductType = row["ProductType"].ToString(),
                StyleCode = row["PRINCIPALCODE"].ToString(),
            };
        }

        private VoySale ReadVoySale(DataRow row)
        {
            //Convert to JSONSale
            VoySale item = new VoySale();
            //{
            item.BasicAmt = Utils.ToDecimal(row["Basic Amt"].ToString());
            item.Barcode = row["BAR CODE"].ToString();
            item.Quantity = decimal.Parse(row["Quantity"].ToString());
            item.BillAmt = decimal.Parse(row["Bill Amt"].ToString());
            item.BrandName = row["Brand Name"].ToString();

            item.CGSTAmt = string.IsNullOrEmpty(row["CGST Amt"].ToString()) ? 0 : decimal.Parse(row["CGST Amt"].ToString());
            item.DiscountAmt = decimal.Parse(row["Discount Amt"].ToString());
            item.HSNCode = row["HSN Code"].ToString();
            item.LineTotal = string.IsNullOrEmpty(row["Line Total"].ToString()) ? 0 : decimal.Parse(row["Line Total"].ToString());
            item.MRP = decimal.Parse(row["MRP"].ToString());
            item.RoundOff = decimal.Parse(row["Round Off"].ToString());
            item.SGSTAmt = decimal.Parse(row["SGST Amt"].ToString());
            item.Tailoring = row["TAILORING FLAG"].ToString();
            item.TaxAmt = string.IsNullOrEmpty(row["Tax Amt"].ToString()) ? 0 : decimal.Parse(row["Tax Amt"].ToString());
            item.SalesManName = row["SalesMan Name"].ToString();
            item.OnDate = DateTime.Parse(row["Invoice Date"].ToString());
            item.ProductName = row["Product Name"].ToString();
            item.InvoiceNo = row["Invoice No"].ToString();
            item.PaymentMode = row["Payment Mode"].ToString();
            item.InvoiceType = row["Invoice Type"].ToString();
            item.LP = row["LP Flag"].ToString();
            // };
            return item;
        }

        private MDSale ReadMDSale(DataRow row)
        {
            return new MDSale
            {
                BARCODE = row["BARCODE"].ToString(),
                BASICAMOUNT = decimal.Parse(row["BASICAMOUNT"].ToString()),
                BILLAMOUNT = Math.Round(decimal.Parse(row["BILLAMOUNT"].ToString()), 2),
                BRAND = row["BRAND"].ToString(),
                CATEGORY = row["CATEGORY"].ToString(),
                CGSTAMOUNT = Math.Round(decimal.Parse(row["CGSTAMOUNT"].ToString()), 2),
                Discountamount = Math.Round(decimal.Parse(row["Discount amount"].ToString()), 2),
                HSNCODE = row["HSNCODE"].ToString(),
                COUPONAMOUNT = 0,
                COUPONPERCENTAGE = "",
                LINETOTAL = Math.Round(decimal.Parse(row["LINETOTAL"].ToString()), 2),
                MRP = Math.Round(decimal.Parse(row["MRP"].ToString())),
                OnDate = ToDateDMY(row["Date"].ToString()),
                PAYMENTMODE = row["PAYMENTMODE"].ToString(),
                Product = row["Product"].ToString(),
                Productnumber = row["Product number"].ToString(),
                Quantity = Math.Round(decimal.Parse(row["Quantity"].ToString()), 2),
                Receiptnumber = row["Receipt number"].ToString(),
                ROUNDOFFAMT = Math.Round(decimal.Parse(row["ROUNDOFFAMT"].ToString()), 2),
                SALESMAN = row["SALESMAN"].ToString(),
                SALESTYPE = row["SALESTYPE"].ToString(),
                SGSTAMOUNT = Math.Round(decimal.Parse(row["SGSTAMOUNT"].ToString()), 2),
                STYLECODE = row["STYLECODE"].ToString(),
                TAILORINGFLAG = row["TAILORINGFLAG"].ToString(),
                Taxamount = Math.Round(decimal.Parse(row["Tax amount"].ToString()), 2),
                TranscationNumber = row["Transaction number"].ToString(),
            };
        }

        private static VoyPurhcase ReadPurchase(DataRow row)
        {
            return new VoyPurhcase
            {
                Barcode = row["Barcode"].ToString(),

                Cost = Math.Round(decimal.Parse(row["Cost"].ToString()), 2),
                CostValue = Math.Round(decimal.Parse(row["Cost Value"].ToString()), 2),
                MRP = Math.Round(decimal.Parse(row["MRP"].ToString()), 2),
                MRPValue = Math.Round(decimal.Parse(row["MRP Value"].ToString()), 2),
                GRNDate = ToDateDMY(row["GRNDate"].ToString()),
                InvoiceDate = ToDateDMY(row["Invoice Date"].ToString()),
                GRNNo = row["GRNNo"].ToString(),
                InvoiceNo = row["Invoice No"].ToString(),
                ItemDesc = row["Item Desc"].ToString(),
                ProductName = row["Product Name"].ToString(),
                StyleCode = row["Style Code"].ToString(),
                SupplierName = row["Supplier Name"].ToString(),
                Quantity = Math.Round(decimal.Parse(row["Quantity"].ToString()), 2),
                TaxAmt = string.IsNullOrEmpty(row["TaxAmt"].ToString()) ? 0 : Math.Round(decimal.Parse(row["TaxAmt"].ToString()), 2),
            };
        }

        //TODO:
        // Convert everthing to JSON
        // store all josn file to a directory
        // befercate all items to each category of data segment and store in json.
        // Convert everything to particular object and store in json.
        // read data and store in database.

        private JsonSale ReadMISale(DataRow row, bool a)
        {
            return new JsonSale
            {
                Barcode = row["Barcode"].ToString(),
                BasicRate = decimal.Parse(row["BasicAmount"].ToString()),
                LineTotal = decimal.Parse(row["BillAmount"].ToString()),
                Discount = decimal.Parse(row["DiscAmt"].ToString()),
                BrandName = row["BrandName"].ToString(),
                Brand = row["BRAND"].ToString(),
                HSNCODE = row["HSNCODE"].ToString(),
                InvoiceNumber = row["InvNo"].ToString(),
                InvoiceType = row["BILL_TYPE"].ToString(),
                CGSTAmount = decimal.Parse(row["GSTAmt"].ToString()),//need to Check
                MRPValue = decimal.Parse(row["MRPValue"].ToString()),
                CostValue = decimal.Parse(row["CostValue"].ToString()),
                InvoiceDate = row["Date"].ToString(),
                Quantity = decimal.Parse(row["Qty"].ToString()),
                TaxRate = decimal.Parse(row["TotalTaxRate"].ToString()),
                TaxAmount = decimal.Parse(row["TotalTaxAmt"].ToString()),

                UnitCost = decimal.Parse(row["UnitCost"].ToString()),
                UnitMRP = decimal.Parse(row["UnitMRP"].ToString()),
                ItemDesc = row["DESCRIPTION"].ToString(),
                SalesmanName = row["Salesman"].ToString(),
                Size = row["Size"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCate"].ToString(),
                ProductType = row["ProductType"].ToString(),
                SytleCode = row["PRINCIPALCODE"].ToString(),
            };
        }

        private JsonSale ReadVoySale(DataRow row, bool a)
        {
            //Convert to JSONSale
            JsonSale item = new JsonSale();
            //{
            item.BasicRate = Utils.ToDecimal(row["Basic Amt"].ToString());
            item.Barcode = row["BAR CODE"].ToString();
            item.Quantity = decimal.Parse(row["Quantity"].ToString());
            item.BillAmount = decimal.Parse(row["Bill Amt"].ToString());
            item.BrandName = row["Brand Name"].ToString();

            item.CGSTAmount = string.IsNullOrEmpty(row["CGST Amt"].ToString()) ? 0 : decimal.Parse(row["CGST Amt"].ToString());
            item.Discount = decimal.Parse(row["Discount Amt"].ToString());
            item.HSNCODE = row["HSN Code"].ToString();
            item.LineTotal = string.IsNullOrEmpty(row["Line Total"].ToString()) ? 0 : decimal.Parse(row["Line Total"].ToString());
            item.MRP = decimal.Parse(row["MRP"].ToString());
            item.RoundOff = decimal.Parse(row["Round Off"].ToString());
            item.SGST = decimal.Parse(row["SGST Amt"].ToString());
            item.Tailoring = row["TAILORING FLAG"].ToString();
            item.TaxAmount = string.IsNullOrEmpty(row["Tax Amt"].ToString()) ? 0 : decimal.Parse(row["Tax Amt"].ToString());
            item.SalesmanName = row["SalesMan Name"].ToString();
            item.InvoiceDate = row["Invoice Date"].ToString();
            item.ProductName = row["Product Name"].ToString();
            item.InvoiceNumber = row["Invoice No"].ToString();
            item.PaymentMode = row["Payment Mode"].ToString();
            item.InvoiceType = row["Invoice Type"].ToString();
            item.LP = row["LP Flag"].ToString();
            // };
            return item;
        }

        private JsonSale ReadMDSale(DataRow row, bool a)
        {
            return new JsonSale
            {
                Barcode = row["BARCODE"].ToString(),
                BasicRate = decimal.Parse(row["BASICAMOUNT"].ToString()),
                BillAmount = Math.Round(decimal.Parse(row["BILLAMOUNT"].ToString()), 2),
                Brand = row["BRAND"].ToString(),
                Category = row["CATEGORY"].ToString(),
                CGSTAmount = Math.Round(decimal.Parse(row["CGSTAMOUNT"].ToString()), 2),
                Discount = Math.Round(decimal.Parse(row["Discount amount"].ToString()), 2),
                HSNCODE = row["HSNCODE"].ToString(),
                //COUPONAMOUNT = 0,
                //COUPONPERCENTAGE = "",
                LineTotal = Math.Round(decimal.Parse(row["LINETOTAL"].ToString()), 2),
                MRP = Math.Round(decimal.Parse(row["MRP"].ToString())),
                InvoiceDate = row["Date"].ToString(),
                PaymentMode = row["PAYMENTMODE"].ToString(),
                ProductName = row["Product"].ToString(),
                // Productnumber = row["Product number"].ToString(),
                Quantity = Math.Round(decimal.Parse(row["Quantity"].ToString()), 2),
                InvoiceNumber = row["Receipt number"].ToString(),
                RoundOff = Math.Round(decimal.Parse(row["ROUNDOFFAMT"].ToString()), 2),
                SalesmanName = row["SALESMAN"].ToString(),
                InvoiceType = row["SALESTYPE"].ToString(),
                SGST = Math.Round(decimal.Parse(row["SGSTAMOUNT"].ToString()), 2),
                SytleCode = row["STYLECODE"].ToString(),
                Tailoring = row["TAILORINGFLAG"].ToString(),
                TaxAmount = Math.Round(decimal.Parse(row["Tax amount"].ToString()), 2),
                // TranscationNumber = row["Transaction number"].ToString(),
            };
        }
    }


}