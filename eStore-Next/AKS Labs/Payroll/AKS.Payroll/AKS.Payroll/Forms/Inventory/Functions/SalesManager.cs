using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using IronBarCode;
using Microsoft.EntityFrameworkCore;

namespace AKS.Payroll.Forms.Inventory.Functions
{

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
            var yearList = azureDb.ProductSales.Where(c => c.StoreId == storeCode && c.InvoiceType== iType).GroupBy(c => c.OnDate.Year).Select(c => c.Key).ToList();
            foreach (var year in YearList)
            {
                report.Add(year, SaleReports(storeCode, year,iType));
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

        public bool SaveInvoice(string mobileNo, string customerName, string smId, InvoiceType iType, bool isCashPaid)
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
                Tailoring = false,
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
                    item.FreeQty = item.BilledQty=0;
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
                    //FontSize = 12,
                    Page2Inch = false,
                    InvoiceSet = true,
                    //PageWith = 240,
                    //PageHeight = 1170,

                    FileName = $@"d:\AksLabs\{StoreCode}\SaleInvoices\{fn}\{sale.InvoiceNo}.pdf",
                    //FileName = $"{sale.InvoiceNo}.pdf",
                    PathName = $@"d:\AksLabs\{StoreCode}\SaleInvoices\{fn}",
                    CustomerName = customerName,
                    MobileNumber = mobileNo,
                    NoOfCopy = 1,
                    // Use Static session class this info
                    StoreName = "Aprajita Retails",
                    Address = "Bhagalpur Road, Dumka",
                    City = "Dumka",
                    Phone = "06434-224461",
                    TaxNo = "20AJHPA7396P1ZV",

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
            && c.InvoiceType==iType
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

        private List<List<SaleReport>> SaleReports(string storeCode, int year,InvoiceType iType)
        {
            List<List<SaleReport>> saleReports = new List<List<SaleReport>>();
            for (int i = 1; i <= 12; i++)
            {
                saleReports.Add(SaleReports(storeCode, year, i,iType));
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