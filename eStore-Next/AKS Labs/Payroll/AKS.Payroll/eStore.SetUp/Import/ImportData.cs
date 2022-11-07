using Syncfusion.XlsIO;
using System.Data;
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

    public class ImportData
    {
        public enum SaleVMT
        { VOY, MD, MI }

        public static SortedList<string, string> ConfigJson(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            var config = JsonSerializer.Deserialize<SortedList<string, string>>(json);
            reader.Close();
            return config;
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

        public static DataTable JSONFileToDataTable(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            JsonSerializerOptions options = new()
            { Converters = { new DataTableJsonConverter() }, WriteIndented = true };
            var dataTable = JsonSerializer.Deserialize<DataTable>(json, options);
            return dataTable;
        }

        public static List<T>? JsonToObject<T>(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();
            reader.Close();
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public static async Task<bool> ObjectsToJSONFile<T>(List<T> itemLists, string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                //This help to Save as backup for future use
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, itemLists);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static async Task<bool> ObjectsToJSONFile<T>(T itemLists, string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                //This help to Save as backup for future use
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, itemLists);
                await createStream.DisposeAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static string PurchaseDatatableToJson(DataTable dataTable)
        {
            List<VoyPurhcase> list = new List<VoyPurhcase>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
                list.Add(ReadPurchase(dataTable.Rows[i]));

            return JsonSerializer.Serialize(list);
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

        public static string SaleDatatableToJSON(DataTable dataTable, SaleVMT vMT)
        {
            List<JsonSale> list = new List<JsonSale>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];
                if (vMT == SaleVMT.VOY)
                {
                    list.Add(ReadVoySale(row, true));
                }
                else if (vMT == SaleVMT.MD)
                {
                    list.Add(ReadMDSale(row, true));
                }
                else if (vMT == SaleVMT.MI)
                {
                    list.Add(ReadMISale(row, true));
                }
            }
            return JsonSerializer.Serialize(list);
        }

        public static string SaleDatatableToJSONOld(DataTable dataTable, SaleVMT vMT)
        {
            List<string> list = new List<string>();
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

        public static string SetBrandCode(string style, string cat, string type)
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

        public static Task<bool> UpdateBackKupJson<T>(string currentfilename, string backupfilename)
        {
            var current = ImportData.JsonToObject<T>(currentfilename);
            var backup = ImportData.JsonToObject<T>(backupfilename);
            backup.AddRange(current);
            return ImportData.ObjectsToJSONFile<T>(backup, backupfilename);
        }
        private static MDSale ReadMDSale(DataRow row)
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

        private static JsonSale ReadMDSale(DataRow row, bool a)
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

        private static MISale ReadMISale(DataRow row)
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

        private static JsonSale ReadMISale(DataRow row, bool a)
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

        private static VoySale ReadVoySale(DataRow row)
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

        //TODO:
        // Convert everthing to JSON
        // store all josn file to a directory
        // befercate all items to each category of data segment and store in json.
        // Convert everything to particular object and store in json.
        // read data and store in database.
        private static JsonSale ReadVoySale(DataRow row, bool a)
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

        private string ReadPurchaseToJSON(string filename, string sheet)
        {
            var dataTable = ReadExcelToDatatable(filename, sheet, 1, 1, 1, 1);
            List<VoyPurhcase> list = new List<VoyPurhcase>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
                list.Add(ReadPurchase(dataTable.Rows[i]));

            return JsonSerializer.Serialize(list);
        }

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
    }

    public class PCat
    {
        public string Name { get; set; }
    }
}