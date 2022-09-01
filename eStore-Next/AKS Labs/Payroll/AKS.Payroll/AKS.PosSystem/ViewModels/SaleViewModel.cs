/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.Payroll.Database;
using AKS.PosSystem.DataModels;
using AKS.PosSystem.Helpers;
using AKS.PosSystem.Models.VM;
using AKS.Printers.Thermals;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;

namespace AKS.PosSystem.ViewModels
{
    #region SaleModel

    public class SalesViewModel : ViewModel<ProductSale, SaleItem, SaleVM, SaleDataModel>
    {
        #region Constructor

        public SalesViewModel(InvoiceType? iType, Action<string, AlertType> alertFunction)
        {
            StoreCode = CurrentSession.StoreCode;
            if (iType == null) InvoiceType = InvoiceType.ManualSale;
            else InvoiceType = iType.Value;
            AlertCallBack = alertFunction;
        }

        #endregion Constructor

        #region DeclartionSection

        private static Action<string, AlertType> AlertCallBack = null;

        #region DataModels

        private ProductStockDataModel _stockDataModel;
        private PaymentDataModel _paymentDM;

        #endregion DataModels

        public InvoiceType InvoiceType;

        private ObservableListSource<ProductSale> ProductSales;
        private ObservableListSource<SaleItem> SaleItems;

        private ObservableListSource<SaleVM> SaleVMs;
        public ObservableListSource<SaleItemVM> SaleItemVMs;

        public List<PaymentDetail> PaymentDetails;

        // Cart Information
        public decimal TotalQty, TotalFreeQty, TotalTax, TotalDiscount, TotalAmount, TotalItem;

        public List<int> YearList;
        private List<StockInfo> SearchedStockedList;
        private int SeletedYear;
        private int TotalCount;
        public bool ReturnKey = false;

        public string LastInvoicePath = "";
        public ProductSale LastInvoice = null;

        private List<string> _barcodeList;

        #endregion DeclartionSection

        #region Methods

        /// <summary>
        /// Init view model
        /// </summary>
        /// <returns></returns>
        public override bool InitViewModel()
        {
            //InitManager
            SeletedYear = DateTime.Today.Year;

            if (DataModel == null) DataModel = new SaleDataModel();
            if (_stockDataModel == null) _stockDataModel = new ProductStockDataModel();
            if (_paymentDM == null) _paymentDM = new PaymentDataModel();

            YearList = DataModel.GetYearList(CurrentSession.StoreCode);
            UpdateSaleList(DataModel.Filters(c => c.OnDate.Year == SeletedYear, x => x.OnDate, true));
            return true;
        }

        /// <summary>
        /// Load Barcode List for use for AutoComplete
        /// </summary>
        public List<string> LoadBarcodeList()
        {
            // For using in autocomplete text box, not enabled .
            if (_barcodeList.Count <= 0)
                _barcodeList = _stockDataModel.GetBarcodeList();
            return _barcodeList;

        }

        /// <summary>
        /// Reset Cart to zero
        /// </summary>
        public void ResetCart()
        {
            TotalAmount = TotalItem = TotalDiscount = TotalTax = TotalQty = TotalFreeQty = 0;
            TotalCount = 0;
        }

        public bool SaveInvoice(string mobileNo, string customerName, string smId, InvoiceType iType, bool isCashPaid, bool serviceBill)
        {
            var count = DataModel.GetRecordCount(StoreCode, iType, DateTime.Now);
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
            sale.InvoiceNo = SaleStatic.GenerateInvoiceNumber(iType, ++count, StoreCode);
            sale.InvoiceCode = $"{StoreCode}/{DateTime.Now.Year}/{DateTime.Now.Month}/{SaleUtils.INCode(count)}";
            if (sale.InvoiceType == InvoiceType.Sales || sale.InvoiceType == InvoiceType.SalesReturn)
                sale.Taxed = true;

            // Adding sale item
            foreach (var si in SaleItemVMs)
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
                    var stock = _stockDataModel.GetY(item.Barcode);
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
                        _stockDataModel.AddOrUpdate(stock, false);
                    }
                }
            }

            //Handling Payment Details
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
                // azureDb.SalePaymentDetails.Add(spd);
                _paymentDM.AddOrUpdate(spd);
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
                    //azureDb.SalePaymentDetails.Add(spd);
                    _paymentDM.AddOrUpdate(spd);
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
                        // azureDb.CardPaymentDetails.Add(card);
                        _paymentDM.AddOrUpdate(card);
                    }
                }
            }
            CustomerSale cs = new CustomerSale { MobileNo = mobileNo, InvoiceCode = sale.InvoiceCode };
            DataModel.AddorUpdateRecord<CustomerSale>(cs, true);
            //azureDb.ProductSales.Add(sale);
            //int x = azureDb.SaveChanges();
            if (Save(sale))
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
                    Reprint = false,
                    ProductSale = sale,
                    CardDetails = card ?? null,
                    PaymentDetails = spds,
                };
                LastInvoicePath = print.InvoicePdf();
                LastInvoice = sale;
                ProductSales.Add(sale);
                return true;
            }
            else
            {
                LastInvoicePath = null;
                LastInvoice = null;
                return false;
            }
        }

        public override bool Save(ProductSale entity)
        {
            return DataModel.Save(entity) != null;
        }

        public override bool Delete(ProductSale entity)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRange(List<ProductSale> entities)
        {
            throw new NotImplementedException();
        }

        #endregion Methods

        #region OpsMethod

        /// <summary>
        /// return Product Sale of Particular type for GridView  Datasources
        /// </summary>
        /// <returns></returns>
        public List<ProductSale> SetGridView() => ProductSales.Where(c => c.InvoiceType == InvoiceType).ToList();

        /// <summary>
        /// Setup form data for new entry
        /// </summary>
        /// <returns></returns>
        public List<CustomerListVM> SetupFormData()
        {
            SaleItemVMs = new ObservableListSource<SaleItemVM>();
            PaymentDetails = null;
            LastInvoice = null;
            LastInvoicePath = null;
            return DataModel.GetCustomerList();
        }

        /// <summary>
        /// Add Payment to Payment List for new/edit Invoices
        /// </summary>
        /// <param name="pd"></param>
        public void AddToPaymentList(PaymentDetail pd)
        {
            if (PaymentDetails == null)
                PaymentDetails = new List<PaymentDetail>();
            PaymentDetails.Add(pd);
        }

        public StockInfo? GetItemDetail(string barcode, bool Tailoring)
        {
            if (Tailoring)
            {
                var item = _stockDataModel.GetItemDetail(barcode, Tailoring);
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
            var item = _stockDataModel.GetItemDetail(barcode);
            if (SearchedStockedList == null) SearchedStockedList = new List<StockInfo>();
            SearchedStockedList.Add(item);
            return item;
        }

        private void UpdateSaleList(List<ProductSale> sales)
        {
            if (sales != null)
                foreach (var item in sales)
                    ProductSales.Add(item);
        }

        private void UpdateSaleList(ProductSale sale)
        {
            if (sale != null)
                ProductSales.Add(sale);
        }

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

        #endregion OpsMethod

        #region NonModel

        public List<CustomerListVM> GetCustomerList()
        {
            return DataModel.GetCustomerList();
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        public void AddNewCustomer(string name, string mobile)
        {
            Customer c = new Customer
            {
                City = CurrentSession.CityName,
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

            if (DataModel.CustomerExists(mobile))
            {
                LogWrite.LogError("Customer Already exist!");
                Alert("Customer Already Exit", AlertType.Info, AlertCallBack);
                return;
            }
            else
            {
                if (DataModel.AddorUpdateRecord(c, true, true) > 0)
                    Alert("Customer Added", AlertType.Normal, AlertCallBack);
                else
                    Alert("Customer Not added", AlertType.Warning, AlertCallBack);
            }
        }

        #endregion NonModel
    }

    #endregion SaleModel
}