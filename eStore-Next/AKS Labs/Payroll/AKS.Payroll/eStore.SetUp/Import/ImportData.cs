using Syncfusion.XlsIO;
using System.Data;
using System.Text.Json;
using System.Xml.Linq;

namespace eStore.SetUp.Import
{
    public class ImportData
    {
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

        private void ReadSaleToJSON(string filename)
        {
            var dataTable = ReadExcelToDatatable(filename, "sale", 1, 1, 1, 1);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];

            }
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

        private VoyPurhcase ReadPurchase(DataRow row) {

            return new VoyPurhcase
            {
                Barcode = row["Barcode"].ToString(),

                Cost = Math.Round(decimal.Parse(row["Cost"].ToString()), 2),
                CostValue = Math.Round(decimal.Parse(row["CostValue"].ToString()), 2),
                MRP = Math.Round(decimal.Parse(row["MRP"].ToString()), 2),
                MRPValue = Math.Round(decimal.Parse(row["MRPValue"].ToString()), 2),
                GRNDate = ToDateDMY(row["GRNDate"].ToString()),
                InvoiceDate = ToDateDMY(row["InvoiceDate"].ToString()),
                GRNNo = row["GRNNo"].ToString(), 
                InvoiceNo = row["InvoiceNo"].ToString(),
                ItemDesc = row["ItemDesc"].ToString(),
                ProductName = row["ProductName"].ToString(),
                StyleCode = row["StyleCode"].ToString(),
                SupplierName= row["SupplierName"].ToString(),
                Quantity=Math.Round(decimal.Parse(row["Quantity"].ToString()), 2),
                TaxAmt= Math.Round(decimal.Parse(row["TaxAmt"].ToString()), 2),
            };
        
        }


        //TODO:
        // Convert everthing to JSON
        // store all josn file to a directory
        // befercate all items to each category of data segment and store in json.
        // Convert everything to particular object and store in json.
        // read data and store in database.
    }

    public class VoySale
    {
        public string InvoiceType { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime OnDate { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }
        public string StyleCode { get; set; }

        public string Barcode { get; set; }
        public string HSNCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal BasicAmt { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal LineTotal { get; set; }
        public decimal RoundOff { get; set; }
        public decimal BillAmt { get; set; }
        public string PaymentMode { get; set; }
        public string SalesManName { get; set; }
        public string LP { get; set; }
        public string Tailoring { get; set; }
    }

    public class ManualInvoice
    {
        public int SNo { get; set; }
        public DateTime OnDate { get; set; }
        public string InvNo { get; set; }
        public string Barcode { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public string Discount { get; set; }
        public decimal Amount { get; set; }
        public decimal LineTotal { get; set; }
        public decimal BillAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Salesman { get; set; }
    }

    public class MDSale
    {
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
        public DateTime OnDate { get; set; }
        public string InvNo { get; set; }
        public string InvType { get; set; }
        public string Barcode { get; set; }
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


    public class JsonSale
    {
        public string InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal BasicRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGST { get; set; }
        public decimal RoundOff { get; set; }
        public decimal LineTotal { get; set; }
        public decimal BillAmount { get; set; }
        public string PaymentMode { get; set; }
        public string SalesmanName { get; set; }

        public string Brand { get; set; }
        public string BrandName { get; set; }
        public string ItemDesc { get; set; }
        public string ProductName { get; set; }
        public string SytleCode { get; set; }
        public string HSNCODE { get; set; }
        public string LP { get; set; }
        public string Tailoring { get; set; }

        public decimal UnitMRP { get; set; }
        public decimal MRPValue { get; set; }

        public decimal UnitCost { get; set; }
        public decimal CostValue { get; set; }

        public string Size { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ProductType { get; set; }
    }
    public class VoyPurhcase
    {
        public string GRNNo { get; set; }
        public DateTime GRNDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SupplierName { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string StyleCode { get; set; }
        public string ItemDesc { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal MRPValue { get; set; }
        public decimal Cost { get; set; }
        public decimal CostValue { get; set; }
        public decimal TaxAmt { get; set; }


    }
    public class Utils
    {
        public static DateTime ToDate(string date)
        {
            char c = '-';
            if (date.Contains('/')) c = '/';

            var d = date.Split(c);
            return new DateTime(int.Parse(d[2].Split(" ")[0]), int.Parse(d[1]), int.Parse(d[0]));
        }

        public static int ReadInt(TextBox t)
        {
            return int.Parse(t.Text.Trim());
        }

        public static decimal ReadDecimal(TextBox t)
        {
            return decimal.Parse(t.Text.Trim());
        }

        public static decimal ToDecimal(string val)
        {
            return decimal.Round(decimal.Parse(val.Trim()), 2);
        }

        public static async Task ToJsonAsync<T>(string fileName, List<T> ObjList)
        {
            // string fileName = "WeatherForecast.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, ObjList);
            await createStream.DisposeAsync();
        }

        //public static async Task<List<PurchaseItem>?> FromJson<T>(string filename)
        //{
        //    using FileStream openStream = File.OpenRead(filename);
        //    return JsonSerializer.Deserialize<List<PurchaseItem>>(openStream);
        //}

        public static async Task<List<T>?> FromJsonToObject<T>(string filename)
        {
            using FileStream openStream = File.OpenRead(filename);
            return JsonSerializer.Deserialize<List<T>>(openStream);
        }
    }

}