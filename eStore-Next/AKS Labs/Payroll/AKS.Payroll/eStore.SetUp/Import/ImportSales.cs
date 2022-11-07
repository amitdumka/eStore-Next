using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Models.Sales;
using Syncfusion.XlsIO.Parser.Biff_Records.PivotTable;
using System.Data;
using System.Text.Json;

namespace eStore.SetUp.Import
{
    public class ImportSales
    {
        private string BasePath = "", StoreCode = "";
        private SortedDictionary<string, string> Salesman = new SortedDictionary<string, string>();

        public async void StartImportingSales(string storeCode, string filename, string basePath)
        {
            BasePath = basePath;
            StoreCode = storeCode;
            string SalesFileName = ImportBasic.GetSetting("VoySale");
            string SaleSummary = ImportBasic.GetSetting("VoySaleSummary");

            bool flag = await GenerateSaleInvoice(StoreCode, SalesFileName);
            flag = await GenerateSaleItem(StoreCode, SalesFileName);
            flag = await GenerateSalePayment(StoreCode, SalesFileName);
            flag = await SaleCleanUp();
            ProcessSaleSummary(SalesFileName, SaleSummary);
            //flag =  await GenerateDailySale(StoreCode, SalesFileName, "");
        }


        private async Task<bool> CreatePayment()
        {
            var voySales = ImportData.JsonToObject<JsonSale>(ImportBasic.GetSetting("VoySale")).ToList();
            var payFilter = voySales.Where(c => string.IsNullOrEmpty(c.PaymentMode) == false).ToList();
            List<CardPaymentDetail> cards = new List<CardPaymentDetail>();
            List<SalePaymentDetail> sales = new List<SalePaymentDetail>();
            foreach (var itm in payFilter)
            {
                SalePaymentDetail detail = new SalePaymentDetail
                {
                    InvoiceCode = itm.InvoiceNumber,
                    PaidAmount = itm.BillAmount,
                    RefId = "",
                    PayMode = ImportHelpers.GetPaymode(itm.PaymentMode),
                };
                sales.Add(detail);
                if (detail.PayMode == PayMode.Card)
                {
                    CardPaymentDetail card = new CardPaymentDetail
                    {
                        CardLastDigit = 0,
                        AuthCode = 0,
                        CardType = CardType.Visa,
                        Card = Card.DebitCard,
                        InvoiceCode = itm.InvoiceNumber,
                        PaidAmount = itm.BillAmount,
                        EDCTerminalId = "NA",
                    };
                    cards.Add(card);
                }
            }

            var fnsc = Path.Combine(ImportBasic.GetSetting("BasePath"), @"Sales\CardPayments.json");
            Directory.CreateDirectory(Path.GetDirectoryName(fnsc));
            var fnss = Path.Combine(ImportBasic.GetSetting("BasePath"), @"Sales\SalePayments.json");
            bool flag = await ImportData.ObjectsToJSONFile<CardPaymentDetail>(cards, fnsc);
            flag = await ImportData.ObjectsToJSONFile<SalePaymentDetail>(sales, fnss);
            ImportBasic.AddSetting("CardPayment", fnsc);
            ImportBasic.AddSetting("SalePayment", fnss);
            return flag;
        }

        private void GenerateDailySale(string code, string filename, string filename2)
        {
            //TODO: Pending 
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

        private async Task<bool> GenerateSaleInvoice(string code, string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                var json = reader.ReadToEnd();
                var sales = JsonSerializer.Deserialize<List<JsonSale>>(json).OrderBy(c => c.InvoiceDate);

                int count = 0;
                var saleinvs = sales.GroupBy(c => new { c.InvoiceNumber, c.InvoiceDate, c.InvoiceType, c.SalesmanName })
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
                        SalesmanId = c.Key.SalesmanName,// Salesman.Where(x => x.Value == c.Key.SalesmanName).First().Key,
                        TotalMRP = c.Sum(x => x.MRP),
                        TotalDiscountAmount = c.Sum(x => x.Discount),
                        UserId = "AUTOJINI",
                        TotalTaxAmount = c.Sum(x => x.TaxAmount),
                        TotalPrice = c.Sum(x => x.BillAmount),
                        InvoiceCode = $@"{code}/{DateTime.Parse(c.Key.InvoiceDate).ToString("yyyy/MM")}/00{++count}",
                        IsReadOnly = false,
                        MarkedDeleted = false,
                        Paid = true,
                        RoundOff = c.Sum(x => x.RoundOff),
                    }).ToList();

                var savefilename = Path.Combine(ImportBasic.GetSetting("BasePath"), "Sales");
                Directory.CreateDirectory(savefilename);
                savefilename = Path.Combine(savefilename, "SaleInvoices.json");
                using FileStream createStream = File.Create(savefilename);
                await JsonSerializer.SerializeAsync(createStream, saleinvs);
                await createStream.DisposeAsync();
                //ImportBasic.AddSetting("SaleInvoice", savefilename);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> GenerateSaleItem(string code, string filename)
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
                    Unit = ImportHelpers.SetUnit(c.ProductName),
                    Value = c.LineTotal == 0 ? c.BasicRate + c.TaxAmount : c.LineTotal,
                    BilledQty = c.Quantity,
                    Adjusted = false,
                    InvoiceType = c.InvoiceType == "SALES" ? InvoiceType.Sales : InvoiceType.SalesReturn,
                    FreeQty = 0,
                    InvoiceCode = c.InvoiceNumber,
                }).ToList();

                var savefilename = Path.Combine(ImportBasic.GetSetting("BasePath"), "Sales");
                Directory.CreateDirectory(savefilename);
                savefilename = Path.Combine(savefilename, "salesItems.json");

                using FileStream createStream = File.Create(savefilename);
                await JsonSerializer.SerializeAsync(createStream, saleinvs);
                await createStream.DisposeAsync();
                //ImportBasic.AddSetting("SaleInvoiceItems", savefilename);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> GenerateSalePayment(string code, string filename)
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

        private List<InvoiceError> ProcessSaleSummary(string saleFileName, string saleSummaaryFileName)
        {
            //Invoice No	Invoice Date	Invoice Type	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	Round Off	Bill Amt	Payment Type

            var saleSummary = ImportData.JSONFileToDataTable(saleSummaaryFileName);
            var sales = ImportData.JsonToObject<ProductSale>(saleFileName);
            List<InvoiceError> Errors = new List<InvoiceError>();
            for (int i = 0; i < saleSummary.Rows.Count; i++)
            {
                var row = saleSummary.Rows[i];
                var inv = sales.Where(c => c.InvoiceNo == saleSummary.Rows[i]["Invoice No"].ToString()).FirstOrDefault();
                if (inv != null)
                {
                    InvoiceError error = new InvoiceError();
                    error.InvoiceNo = inv.InvoiceNo;

                    if (inv.BilledQty != ImportHelpers.ToDecimal(row["Quantity"].ToString()))
                    {
                        if (error.Errors == null) error.Errors = new List<string>();
                        error.Errors.Add($"Bill Qty:{inv.BilledQty}!={row["Quantity"].ToString()}");
                    }
                    if (inv.TotalPrice != ImportHelpers.ToDecimal(row["Bill Amt"]))
                    {
                        if (error.Errors == null) error.Errors = new List<string>();
                        error.Errors.Add($"Bill AMT:{inv.TotalPrice}!={row["Bill Amt"].ToString()}");
                    }
                    if (inv.TotalMRP != ImportHelpers.ToDecimal(row["MRP"]))
                    {
                        if (error.Errors == null) error.Errors = new List<string>();
                        error.Errors.Add($"MRP :{inv.TotalMRP}!={row["MRP"].ToString()}");
                    }
                    if (inv.TotalBasicAmount != ImportHelpers.ToDecimal("Basic Amt"))
                    {
                        if (error.Errors == null) error.Errors = new List<string>();
                        error.Errors.Add($"Basic Amt :{inv.TotalBasicAmount}!={row["Basic Amt"].ToString()}");
                    }
                    if (inv.TotalTaxAmount != ImportHelpers.ToDecimal("Tax Amt"))
                    {
                        if (error.Errors == null) error.Errors = new List<string>();
                        error.Errors.Add($"Tax Amt:{inv.TotalTaxAmount}!={row["TAx Amt"].ToString()}");
                    }

                    if (error.Errors != null && error.Errors.Count > 0) Errors.Add(error);
                }
                else
                {
                    InvoiceError error = new InvoiceError();
                    error.InvoiceNo = saleSummary.Rows[i]["Invoice No"].ToString();
                    error.Errors = new List<string>();
                    error.Errors.Add("Invoice No Is missing");
                    //Inv Missing.
                }
            }

            return Errors;
        }

        private Task<bool> SaleCleanUp()
        {
            var saleItems = ImportData.JsonToObject<SaleItem>(ImportBasic.GetSetting("SaleInvoiceItems")).ToList();
            var saleInvoices = ImportData.JsonToObject<ProductSale>(ImportBasic.GetSetting("SaleInvoice")).OrderBy(c => c.OnDate).ToList();
            var voySales = ImportData.JsonToObject<JsonSale>(ImportBasic.GetSetting("VoySale")).ToList();
            var Stocks = ImportData.JsonToObject<ProductStock>(ImportBasic.GetSetting("ProductStocks")).ToList();

            DateTime july = new DateTime(2017, 7, 1);
            int Count = 0;

            foreach (var inv in saleInvoices)
            {
                inv.InvoiceCode = $@"{inv.StoreId}/{inv.OnDate.ToString("yyyy/MM")}/{++Count}";
                var items = saleItems.Where(c => c.InvoiceCode == inv.InvoiceNo).ToList();

                //Update Stock also here
                foreach (var item in items)
                {
                    item.InvoiceCode = inv.InvoiceCode;
                    if (inv.OnDate < july)
                    {
                        item.TaxType = TaxType.VAT;
                    }
                    else item.TaxType = TaxType.GST;
                    var stock = Stocks.Where(c => c.Barcode == item.Barcode).FirstOrDefault();
                    if (stock != null)
                    {
                        stock.SoldQty += item.BilledQty;
                    }
                }
            }

            var flag = ImportData.ObjectsToJSONFile<SaleItem>(saleItems, ImportBasic.GetSetting("SaleInvoiceItems"));
            flag = ImportData.ObjectsToJSONFile<ProductSale>(saleInvoices, ImportBasic.GetSetting("SaleInvoice"));
            flag = ImportData.ObjectsToJSONFile<ProductStock>(Stocks, ImportBasic.GetSetting("ProductStocks"));

            var products = ImportData.JsonToObject<ProductItem>(ImportBasic.GetSetting("ProdutItems"));

            var hsncodes = voySales.Where(c => !string.IsNullOrEmpty(c.HSNCODE)).Select(c => new { c.Barcode, c.HSNCODE, c.ProductName }).ToList();

            hsncodes = hsncodes.DistinctBy(c => c.Barcode).ToList();
            SortedDictionary<string, string> HSNList = new SortedDictionary<string, string>();
            foreach (var item in hsncodes)
            {
                var pro = products.Where(c => c.Barcode == item.Barcode).FirstOrDefault();
                if (pro != null)
                {
                    pro.HSNCode = item.HSNCODE;
                    try
                    {
                        HSNList.TryAdd(item.ProductName.Split("/")[2], item.HSNCODE);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            var nullHSN = products.Where(c => string.IsNullOrEmpty(c.HSNCode)).Select(c => c.Barcode).ToList();
            foreach (var item in nullHSN)
            {
                var pi = products.Where(c => c.Barcode == item).FirstOrDefault();
                pi.HSNCode = HSNList.GetValueOrDefault(pi.SubCategory, "");
            }

            flag = ImportData.ObjectsToJSONFile<ProductItem>(products, ImportBasic.GetSetting("ProdutItems"));
            var fns = Path.Combine(ImportBasic.GetSetting("BasePath"), @"HSN\HSNCodeList.json");
            Directory.CreateDirectory(Path.GetDirectoryName(fns));
            flag = ImportData.ObjectsToJSONFile<SortedDictionary<string, string>>(HSNList, fns);
            ImportBasic.AddSetting("HSNCodes", fns);
            return flag;
        }
    }
}