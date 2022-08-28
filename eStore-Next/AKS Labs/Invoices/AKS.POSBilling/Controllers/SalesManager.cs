using AKS.Payroll.Database;
using AKS.POSBilling.Functions;
using AKS.POSBilling.ViewModels;
using AKS.Printers.Thermals;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using IronBarCode;
using Microsoft.EntityFrameworkCore;

namespace AKS.POSBilling.Controllers
{
    public class SaleUtils
    {
        public static GeneratedBarcode GenerateBarCode(string invNo)
        {
            try
            {
                Directory.CreateDirectory(@"d:\arp\barcode\");
                GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(invNo, BarcodeEncoding.Code93);
                barcode.SaveAsPng($@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
                return barcode;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static GeneratedBarcode GenerateQRCode(string invNo, DateTime onDate, decimal value)
        {
            Directory.CreateDirectory(@"d:\arp\QRCode\");
            GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter
                .CreateQrCode($"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-");
            Qrcode.SaveAsPng($@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
            return Qrcode;
        }
        public static string BarCodePNG(string invNo)
        {
            Directory.CreateDirectory(@"d:\arp\barcode\");
            GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(invNo, BarcodeEncoding.Code128);
            barcode.SaveAsPng($@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
            return $@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png";
        }
        public static string QRCodePng(string invNo, DateTime onDate, decimal value)
        {
            Directory.CreateDirectory(@"d:\arp\QRCode\");
            GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter
                .CreateQrCode($"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-");
            Qrcode.SaveAsPng($@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");

            return $@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png";
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
        public static decimal BasicRateCalucaltion(decimal mrp, decimal taxRate)
        {
            return Math.Round((100 * mrp / (100 + taxRate)), 2);
        }

        /// <summary>
        /// Helper function if missing
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string[] EnumList(Type t)
        {
            return Enum.GetNames(t);
        }

        public static int SetTaxRate(ProductCategory category, decimal Price)
        {
            int rate = 0;
            switch (category)
            {
                case ProductCategory.Fabric:
                    rate = 5;
                    break;

                case ProductCategory.Apparel:
                    rate = Price > 999 ? 12 : 5;
                    break;

                case ProductCategory.Accessiories:
                    rate = 12;
                    break;

                case ProductCategory.Tailoring:
                    rate = 5;
                    break;

                case ProductCategory.Trims:
                    rate = 5;
                    break;

                case ProductCategory.PromoItems:
                    rate = 0;
                    break;

                case ProductCategory.Coupons:
                    rate = 0;
                    break;

                case ProductCategory.GiftVouchers:
                    rate = 0;
                    break;

                case ProductCategory.Others:
                    rate = 18;
                    break;

                default:
                    rate = 5;
                    break;
            }
            return rate;
        }

        public static decimal TaxCalculation(decimal mrp, decimal taxRate)
        {
            return Math.Round(mrp - (100 * mrp / (100 + taxRate)), 2);
        }
    }

    public class StockHistory
    {
        public DateTime OnDate { get; set; }
        public decimal StockIn { get; set; }
        public decimal StockOut { get; set; }
        public decimal StockBalance { get; set; }

        public static List<StockHistory> History(AzurePayrollDbContext db, string barcode)
        {
            var purchase = db.PurchaseItems.Where(c => c.Barcode == barcode).ToList();
            var sale = db.SaleItems.Where(c => c.Barcode == barcode).ToList();
            List<StockHistory> stockHistories = new List<StockHistory>();
            foreach (var item in purchase)
            {
                StockHistory history = new() { StockIn = item.Qty, StockOut = 0, OnDate = db.PurchaseProducts.Where(c => c.InwardNumber == item.InwardNumber).First().OnDate };
                stockHistories.Add(history);
            }
            foreach (var item in sale)
            {
                StockHistory history = new() { StockIn = 0, StockOut = item.BilledQty, OnDate = db.ProductSales.Where(c => c.InvoiceCode == item.InvoiceCode).First().OnDate };
                stockHistories.Add(history);
            }
            stockHistories = stockHistories.OrderBy(c => c.OnDate).ToList();
            decimal bal = 0;
            foreach (var item in stockHistories)
            {
                bal += item.StockIn - item.StockOut;
                item.StockBalance = bal;
            }
            return stockHistories;
        }
        /// <summary>
        /// All Stock Histories
        /// </summary>
        /// <param name="db"></param>
        /// <param name="storecode"></param>
        /// <returns></returns>
        public static SortedDictionary<string, List<StockHistory>> AllStockHistory(AzurePayrollDbContext db, string storecode)
        {
            var barcodeList = db.Stocks.Where(c => c.StoreId == storecode && c.SoldQty > 0 || c.HoldQty > 0).Select(c => c.Barcode).ToList();

            SortedDictionary<string, List<StockHistory>> histories = new SortedDictionary<string, List<StockHistory>>();
            foreach (var item in barcodeList)
            {
                histories.Add(item, History(db, item));
            }
            return histories;
        }
    }
    /// <summary>
    /// Model/Temeplete class to make manager/VM class
    /// </summary>
    public abstract class Manager
    {
        protected static AzurePayrollDbContext azureDb;
        protected static LocalPayrollDbContext localDb;
        protected static string StoreCode = "ARD";//TODO: Need to Assign

        protected abstract void Delete();

        protected abstract void Get(string id);

        protected abstract void GetList();

        protected abstract void Save();
    }

    /// <summary>
    /// Helps and Manages Sales
    /// </summary>
    public class SalesManager : Manager
    {
        public AutoCompleteStringCollection barcodeList = new AutoCompleteStringCollection();
        public InvoiceType InvoiceType;
        public ObservableListSource<ProductSale> Items;
        public List<PaymentDetail> PaymentDetails;
        public bool ReturnKey = false;

        public string LastInvoicePath = "";
        public ProductSale LastInvoice = null;

        //private List<SaleItem> SalesItems;
        public ObservableListSource<SaleItemVM> SaleItem;

        // Cart Information
        public decimal TotalQty, TotalFreeQty, TotalTax, TotalDiscount, TotalAmount, TotalItem;

        public List<int> YearList;
        private List<StockInfo> SearchedStockedList;
        private int SeletedYear;
        private int TotalCount;

        //private bool IsNew;        private ProductSale Sale;
        public SalesManager(AzurePayrollDbContext db, LocalPayrollDbContext ldb, InvoiceType? iType)
        {
            azureDb = db; localDb = ldb;
            if (iType == null) InvoiceType = InvoiceType.ManualSale;
            else InvoiceType = iType.Value;
        }

        public static decimal CalculateRate(string dis, string qty, string rate)
        {
            try
            {
                if (dis.Contains('%'))
                {
                    var x = decimal.Parse(qty.Trim()) * decimal.Parse(rate.Trim());
                    x -= x * decimal.Parse(dis.Replace('%', ' ').Trim()) / 100;
                    return x;
                }
                else
                {
                    var x = decimal.Parse(qty.Trim()) * decimal.Parse(rate.Trim())
                        - decimal.Parse(dis.Trim());
                    return x;
                }
            }
            catch
            {
                return 0;
            }
        }

        public void AddNewCustomer(string name, string mobile)
        {
            Customer c = new Customer
            {
                City = "Dumka",
                Age = 30,
                DateOfBirth = DateTime.Today.AddYears(-30).Date,
                Gender = Gender.Male,
                MobileNo = mobile,
                NoOfBills = 0,
                OnDate = DateTime.Today,
                TotalAmount = 0
            };

            var cname = name.Trim().Split(' ');
            c.FirstName = cname[0];
            for (int x = 1; x < cname.Length; x++)
                c.LastName += cname[x] + " ";
            if (cname.Length > 1)
                c.LastName = c.LastName.Trim();
            else c.LastName = "";
            if (azureDb.Customers.Any(C => C.MobileNo == mobile))
            {
                MessageBox.Show("Customer Already exist!");
                return;
            }
            else
            {
                azureDb.Customers.Add(c);
                if (azureDb.SaveChanges() > 0) MessageBox.Show("Customer Added");
                else MessageBox.Show("Customer Not added");
            }
        }

        public void AddPayment(PaymentDetail pd)
        {
            if (PaymentDetails == null)
                PaymentDetails = new List<PaymentDetail>();
            PaymentDetails.Add(pd);
        }

        public List<CustomerListVM> GetCustomerList()
        {
            return azureDb.Customers.Select(c => new CustomerListVM { MobileNo = c.MobileNo, CustomerName = c.CustomerName }).OrderBy(c => c.MobileNo).ToList();
        }

        public StockInfo? GetItemDetail(string barcode, bool Tailoring)
        {
            if (barcode.Length < 7) return null;
            if (Tailoring)
            {
                var item = azureDb.ProductItems.Where(c => c.Barcode == barcode)
                    .Select(c => new StockInfo { Barcode = c.Barcode, HoldQty = 1, Qty = 1, Unit = Unit.Nos, TaxRate = 5, TaxType = c.TaxType, Rate = c.MRP, Category = c.ProductCategory })
                    .FirstOrDefault();
                if (SearchedStockedList == null) SearchedStockedList = new List<StockInfo>();
                SearchedStockedList.Add(item);
                return item;
            }
            else return GetItemDetail(barcode);
        }

        /// <summary>
        /// return stock info. Add to API/DataModel
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public StockInfo? GetItemDetail(string barcode)
        {
            if (barcode.Length < 7)
            {
                return null;
            }
            var item = azureDb.Stocks.Include(c => c.Product).Where(c => c.Barcode == barcode)
                .Select(item =>
           new StockInfo()
           {
               Barcode = item.Barcode,
               HoldQty = item.CurrentQtyWH,
               Qty = item.CurrentQty,
               Rate = item.Product.MRP,
               ProductItem = item.Product.Name,
               TaxType = item.Product.TaxType,
               Unit = item.Product.Unit,
               Category = item.Product.ProductCategory,
               TaxRate = SaleUtils.SetTaxRate(item.Product.ProductCategory, item.Product.MRP)
           }).FirstOrDefault();

            if (SearchedStockedList == null) SearchedStockedList = new List<StockInfo>();
            SearchedStockedList.Add(item);
            return item;
        }

        /// <summary>
        /// Init the manager Class
        /// </summary>
        public void InitManager()
        {
            if (azureDb == null) azureDb = new AzurePayrollDbContext();
            if (localDb == null) localDb = new LocalPayrollDbContext();
            if (Items == null)
                Items = new ObservableListSource<ProductSale>();
            SeletedYear = DateTime.Today.Year;
            YearList = azureDb.ProductSales.Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
            UpdateSaleList(azureDb.ProductSales.Include(c => c.Items)
                .Where(c => c.OnDate.Year == SeletedYear).OrderByDescending(c => c.OnDate)
                .ToList());
            //lbYearList.DataSource = YearList;
            //dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
        }

        public void LoadBarcodeList()
        {
            // For using in autocomplete text box, not enabled .
            if (barcodeList.Count > 0) return;
            var l = azureDb.Stocks.Where(c => c.PurhcaseQty > 0).Select(c => new { c.Barcode, c.CurrentQty, c.CurrentQtyWH }).ToList();
            var x = l.Where(c => c.CurrentQty > 0).Select(c => c.Barcode).ToList();
            foreach (var item in x)
            {
                barcodeList.Add(item);
            }
        }

        public void ResetCart()
        {
            TotalAmount = TotalItem = TotalDiscount = TotalTax = TotalQty = TotalFreeQty = 0;
            TotalCount = 0;
        }

        public SortedDictionary<int, List<List<SaleReport>>> SaleReports(string storeCode, InvoiceType iType)
        {
            SortedDictionary<int, List<List<SaleReport>>> report = new SortedDictionary<int, List<List<SaleReport>>>();
            var yearList = azureDb.ProductSales.Where(c => c.StoreId == storeCode && c.InvoiceType == iType).GroupBy(c => c.OnDate.Year).Select(c => c.Key).ToList();
            foreach (var year in YearList)
            {
                report.Add(year, SaleReports(storeCode, year, iType));
            }
            return report;
        }

        public void PrintInvoice(string invoice, ProductSale sale)
        {
            //TODO: Impletement
        }

        // CheckList
        // ProductSale
        // SaleItem
        //Invoice Number and code generated
        // Payment
        // card payment
        // Stock update
        // Sale return

        public bool SaveInvoice(string mobileNo, string customerName, string smId, InvoiceType iType, bool isCashPaid, bool serviceBill)
        {
            var count = azureDb.ProductSales.Where(c => c.StoreId == StoreCode && c.InvoiceType == iType
            && c.OnDate.Year == DateTime.Now.Year && c.OnDate.Month == DateTime.Now.Month).Count();

            ProductSale sale = new ProductSale
            {
                Adjusted = false,
                IsReadOnly = false,
                MarkedDeleted = false,
                EntryStatus = EntryStatus.Added,
                Paid = false,
                UserId = "WinUI",
                Tailoring = serviceBill,
                Taxed = false,
                Items = new List<SaleItem>(),
                InvoiceType = iType,
                SalesmanId = smId,
                OnDate = DateTime.Now,
                StoreId = StoreCode,


                BilledQty = TotalQty,
                FreeQty = TotalFreeQty,

                TotalPrice = TotalAmount,
                TotalDiscountAmount = TotalDiscount,
                TotalTaxAmount = TotalTax,

            };
            sale.TotalMRP = sale.TotalDiscountAmount + sale.TotalPrice;
            sale.TotalBasicAmount = sale.TotalPrice - sale.TotalTaxAmount;
            sale.RoundOff = Math.Round(sale.TotalPrice) - sale.TotalPrice;

            //TODO:handle customer addidtion
            sale.InvoiceNo = GenerateInvoiceNumber(iType, ++count, StoreCode);
            sale.InvoiceCode = $"{StoreCode}/{DateTime.Now.Year}/{DateTime.Now.Month}/{SaleUtils.INCode(count)}";
            if (sale.InvoiceType == InvoiceType.Sales || sale.InvoiceType == InvoiceType.SalesReturn)
                sale.Taxed = true;
            // Adding sale item
            foreach (var si in SaleItem)
            {
                //TODO: Tailoring sale is not implemented till yet and free;
                var info = SearchedStockedList.Find(c => c.Barcode == si.Barcode);
                SaleItem item = new SaleItem
                {
                    Adjusted = false,
                    Barcode = si.Barcode,
                    BilledQty = si.Qty,
                    DiscountAmount = si.Discount,
                    FreeQty = 0,
                    LastPcs = false,
                    TaxType = info.TaxType,
                    Value = si.Amount,
                    InvoiceCode = sale.InvoiceCode,
                    InvoiceType = iType,
                    Unit = info.Unit,
                    BasicAmount = SaleUtils.BasicRateCalucaltion(si.Amount, info.TaxRate),
                    TaxAmount = SaleUtils.TaxCalculation(si.Amount, info.TaxRate)
                };
                //Handling Free Item
                if (item.Value == 0)
                {
                    item.FreeQty = item.BilledQty = 0;
                    item.BilledQty = 0;
                    sale.FreeQty += item.FreeQty;
                    sale.BilledQty -= item.FreeQty;
                }

                if (item.InvoiceType == InvoiceType.ManualSaleReturn || item.InvoiceType == InvoiceType.SalesReturn)
                {
                    item.BilledQty = 0 - item.BilledQty;
                    item.FreeQty = 0 - item.FreeQty;
                    item.BasicAmount = 0 - item.BasicAmount;
                    item.DiscountAmount = 0 - item.DiscountAmount;
                    item.TaxAmount = 0 - item.TaxAmount;
                    item.Value = 0 - item.Value;
                }
                sale.Items.Add(item);
                //Handling service bill
                if (!serviceBill)
                {
                    var stock = azureDb.Stocks.Where(c => c.Barcode == item.Barcode).FirstOrDefault();
                    if (stock != null)
                    {
                        if (item.InvoiceType == InvoiceType.Sales || item.InvoiceType == InvoiceType.SalesReturn)
                        {
                            stock.SoldQty += item.BilledQty + item.FreeQty;
                        }
                        else if (item.InvoiceType == InvoiceType.ManualSale || item.InvoiceType == InvoiceType.ManualSaleReturn)
                        {
                            stock.HoldQty += item.BilledQty + item.FreeQty;
                        }

                        azureDb.Stocks.Update(stock);
                    }
                }
            }
            List<SalePaymentDetail> spds = new List<SalePaymentDetail>();
            CardPaymentDetail card = null; ;
            if (isCashPaid)
            {
                sale.Paid = true;
                SalePaymentDetail spd = new SalePaymentDetail
                {
                    InvoiceCode = sale.InvoiceCode,
                    PaidAmount = sale.TotalPrice - sale.RoundOff,
                    PayMode = PayMode.Cash,
                    RefId = "Cash Paid"
                };
                azureDb.SalePaymentDetails.Add(spd);
                spds.Add(spd);
            }
            else
            {
                sale.Paid = true;
                foreach (var pds in PaymentDetails)
                {
                    SalePaymentDetail spd = new SalePaymentDetail
                    {
                        InvoiceCode = sale.InvoiceCode,
                        PaidAmount = pds.Amount,
                        PayMode = pds.Mode,
                        RefId = pds.RefNumber,
                    };
                    azureDb.SalePaymentDetails.Add(spd);
                    spds.Add(spd);
                    if (spd.PayMode == PayMode.Card)
                    {
                        card = new CardPaymentDetail
                        {
                            AuthCode = Int32.Parse(pds.AuthCode),
                            EDCTerminalId = pds.PosMachineId,
                            InvoiceCode = sale.InvoiceCode,
                            PaidAmount = pds.Amount,
                            CardLastDigit = pds.LastFour,
                            CardType = pds.CardType.Value,
                            Card = pds.Card.Value
                        };
                        azureDb.CardPaymentDetails.Add(card);
                    }
                }
            }

            CustomerSale cs = new CustomerSale { MobileNo = mobileNo, InvoiceCode = sale.InvoiceCode };
            azureDb.CustomerSales.Add(cs);
            azureDb.ProductSales.Add(sale);
            int x = azureDb.SaveChanges();
            if (x > 0)
            {
                string fn = "MIN";
                if (sale.InvoiceType == InvoiceType.Sales || sale.InvoiceType == InvoiceType.SalesReturn)
                    fn = "IN";
                InvoicePrint print = new InvoicePrint
                {
                    ServiceBill = serviceBill,

                    Page2Inch = false,
                    InvoiceSet = true,

                    FileName = $@"d:\AksLabs\{StoreCode}\SaleInvoices\{fn}\{sale.InvoiceNo}.pdf",
                    PathName = $@"d:\AksLabs\{StoreCode}\SaleInvoices\{fn}",
                    CustomerName = customerName,
                    MobileNumber = mobileNo,
                    NoOfCopy = 1,

                    // Use Static session class this info
                    //StoreName = "Aprajita Retails",
                    //Address = "Bhagalpur Road, Dumka",
                    //City = "Dumka",
                    //Phone = "06434-224461",
                    //TaxNo = "20AJHPA7396P1ZV",

                    Reprint = false,
                    ProductSale = sale,
                    CardDetails = card ?? null,
                    PaymentDetails = spds,
                };
                LastInvoicePath = print.InvoicePdf();
                LastInvoice = sale;
                Items.Add(sale);
            }
            else
            {
                LastInvoicePath = null;
                LastInvoice = null;
            }
            return x > 0;
        }

        public List<ProductSale> SetGridView() => Items.Where(c => c.InvoiceType == InvoiceType).ToList();

        public void SetRadioButton(bool regular, bool manual, bool salesReturn)
        {
            if (salesReturn)
            {
                if (manual)
                    InvoiceType = InvoiceType.ManualSaleReturn;
                else if (regular)
                    InvoiceType = InvoiceType.SalesReturn;
            }
            else
            {
                if (manual)
                    InvoiceType = InvoiceType.ManualSale;
                else if (regular)
                    InvoiceType = InvoiceType.Sales;
            }
        }

        public List<CustomerListVM> SetupFormData()
        {
            SaleItem = new ObservableListSource<SaleItemVM>();
            PaymentDetails = null;
            LastInvoice = null;
            LastInvoicePath = null;
            return azureDb.Customers.Select(c => new CustomerListVM { MobileNo = c.MobileNo, CustomerName = c.CustomerName }).OrderBy(c => c.MobileNo).ToList();
        }

        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        protected override void Get(string id)
        {
            throw new NotImplementedException();
        }

        protected override void GetList()
        {
            throw new NotImplementedException();
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }

        //TODO: Move to SaleReport class
        private List<SaleReport> SaleReports(string storeCode, int year, int month, InvoiceType iType)
        {
            var x = azureDb.ProductSales.Where(c => c.StoreId == storeCode
            && c.InvoiceType == iType
            && c.MarkedDeleted == false && !c.Adjusted
            && c.OnDate.Year == year && c.OnDate.Month == month)
           .GroupBy(c => new { c.OnDate.Year, c.InvoiceType, c.Tailoring })
            .Select(c => new SaleReport
            {
                Year = year,
                Month = month,
                InvoiceType = c.Key.InvoiceType,
                Tailoing = c.Key.Tailoring,
                BillQty = c.Sum(x => x.BilledQty),
                FreeQty = c.Sum(x => x.FreeQty),
                TotalMRP = c.Sum(x => x.TotalMRP),
                TotalDiscount = c.Sum(x => x.TotalDiscountAmount),
                TotalTax = c.Sum(x => x.TotalTaxAmount),
                TotalPrice = c.Sum(x => x.TotalPrice),
            })
               .ToList();
            return x;
        }

        private List<List<SaleReport>> SaleReports(string storeCode, int year, InvoiceType iType)
        {
            List<List<SaleReport>> saleReports = new List<List<SaleReport>>();
            for (int i = 1; i <= 12; i++)
            {
                saleReports.Add(SaleReports(storeCode, year, i, iType));
            }
            return saleReports;
        }

        private void UpdateSaleList(List<ProductSale> sales)
        {
            if (sales != null)
                foreach (var item in sales)
                    Items.Add(item);
        }

        private void UpdateSaleList(ProductSale sale)
        {
            if (sale != null)
                Items.Add(sale);
        }

        public static string GenerateInvoiceNumber(InvoiceType iType, int count, string scode)
        {
            string ino = $"{scode}/{DateTime.Now.Year}/{DateTime.Now.Month}/";
            switch (iType)
            {
                case InvoiceType.Sales:
                    ino += $"IN/{SaleUtils.INCode(count)}";
                    break;

                case InvoiceType.SalesReturn:
                    ino += $"SR/{SaleUtils.INCode(count)}";
                    break;

                case InvoiceType.ManualSale:
                    ino += $"MIN/{SaleUtils.INCode(count)}";
                    break;

                case InvoiceType.ManualSaleReturn:
                    ino += $"SRM/{SaleUtils.INCode(count)}";
                    break;

                default:
                    ino += $"IN/{SaleUtils.INCode(count)}";
                    break;
            }
            return ino;
        }
    }
}

public class PrintTest
{
    public static List<string> TestPrintVoucher(AzurePayrollDbContext db)
    {
        var payment = db.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.Payment).FirstOrDefault();
        var receipts = db.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.Receipt).FirstOrDefault();
        var exp = db.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.Expense).FirstOrDefault();

        var cashP = db.CashVouchers.Include(c=>c.TranscationMode).Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.CashPayment).FirstOrDefault();
        var cashR = db.CashVouchers.Include(c => c.TranscationMode).Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.CashReceipt).FirstOrDefault();

        List<string> fileNames = new List<string>();
        VoucherPrint print = new VoucherPrint {
            Amount = payment.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = payment.OnDate,
            Particulars = payment.Particulars,
            PartyName = payment.PartyName,
            LedgerName = payment.Partys.PartyName,
            Reprint = false,
            PaymentMode = payment.PaymentMode,
            Voucher = VoucherType.Payment,
            VoucherNo = payment.SlipNumber + "#" + payment.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = payment.PaymentDetails,
            StoreCode = payment.StoreId,
            Remarks = payment.Remarks,
            IssuedBy = payment.Employee.StaffName,
            PrintType = PrintType.PaymentVoucher
        };
        fileNames.Add(print.PrintPdf());

        print = new VoucherPrint
        {
            Amount = cashP.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = cashP.OnDate,
            Particulars = cashP.Particulars,
            PartyName = cashP.PartyName,
            LedgerName = cashP.Partys.PartyName,
            Reprint = false,
            PaymentMode = PaymentMode.Cash,
            Voucher = VoucherType.Payment,
            VoucherNo = cashP.SlipNumber + "#" + cashP.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = cashP.TranscationMode.TranscationName,
            StoreCode = cashP.StoreId,
            Remarks = cashP.Remarks,
            IssuedBy = cashP.Employee.StaffName,
            PrintType = PrintType.PaymentVoucher
        };
        fileNames.Add(print.PrintPdf());
        print = new VoucherPrint {
            Amount = receipts.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = receipts.OnDate,
            Particulars = receipts.Particulars,
            PartyName = receipts.PartyName,
            LedgerName = receipts.Partys.PartyName,
            Reprint = false,
            PaymentMode = receipts.PaymentMode,
            Voucher = VoucherType.Receipt,
            VoucherNo = receipts.SlipNumber + "#" + receipts.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = receipts.PaymentDetails,
            StoreCode = receipts.StoreId,
            Remarks = receipts.Remarks,
            IssuedBy = receipts.Employee.StaffName,
            PrintType = PrintType.ReceiptVocuher
        };
        fileNames.Add(print.PrintPdf());
        print = new VoucherPrint {
            Amount = exp.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = exp.OnDate,
            Particulars = exp.Particulars,
            PartyName = exp.PartyName,
            LedgerName = exp.Partys.PartyName,
            Reprint = false,
            PaymentMode = exp.PaymentMode,
            Voucher = VoucherType.Expense,
            VoucherNo = exp.SlipNumber + "#" + exp.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = exp.PaymentDetails,
            StoreCode = exp.StoreId,
            Remarks = exp.Remarks,
            IssuedBy = exp.Employee.StaffName,
            PrintType = PrintType.Expenses
        };
        fileNames.Add(print.PrintPdf());
        
        print = new VoucherPrint {
            Amount = cashR.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = cashR.OnDate,
            Particulars = cashR.Particulars,
            PartyName = cashR.PartyName,
            LedgerName = cashR.Partys.PartyName,
            Reprint = false,

            PaymentMode = PaymentMode.Cash,
            Voucher = VoucherType.Payment,
            VoucherNo = cashR.SlipNumber + "#" + cashR.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = cashR.TranscationMode.TranscationName,
            StoreCode = cashR.StoreId,
            Remarks = cashR.Remarks,
            IssuedBy = cashR.Employee.StaffName,
            PrintType = PrintType.CashReceiptVocucher
        };
        fileNames.Add(print.PrintPdf());
        return fileNames;


    }
    public static string TestPrintInvoice(AzurePayrollDbContext db)
    {
        // var inv = db.ProductSales.Include(c => c.Salesman).Where(c => c.InvoiceCode == "ARD/2019/2163").First();
        var inv = db.ProductSales.Include(c => c.Salesman).Where(c => c.OnDate.Month == DateTime.Today.Month && c.OnDate.Year == 2022).First();
        inv.Items = db.SaleItems.Include(c => c.ProductItem).Where(c => c.InvoiceCode == inv.InvoiceCode).ToList();

        InvoicePrint print = new InvoicePrint
        {

            InvoiceSet = true,
            Page2Inch = false,
            CustomerName = "Cash Sale",
            //City = "Dumka",
            FileName = "",
            MobileNumber = "1234567890",
            NoOfCopy = 1,
            //Address = "Bhagalpur Road, Dumka",
            PathName = "",
            //Phone = "06434-224461",
            Reprint = true,
            ProductSale = inv,
            //StoreName = "Aprajita Retails",
            //TaxNo = "20AJHPA7396P1ZV",
            CardDetails = db.CardPaymentDetails.Where(c => c.InvoiceCode == inv.InvoiceCode).FirstOrDefault(),
            PaymentDetails = db.SalePaymentDetails.Where(c => c.InvoiceCode == inv.InvoiceCode).ToList(),
        };

        return print.InvoicePdf();
        //var printDialog1 = new PrintDialog();
        //if (printDialog1.ShowDialog() == DialogResult.OK)
        //{
        //    printDialog1.AllowPrintToFile = true;
        //    PdfDocumentView pdfview = new PdfDocumentView();
        //    pdfview.Load(filename);
        //    pdfview.Print(printDialog1.PrinterSettings.PrinterName);
        //}
    }
}