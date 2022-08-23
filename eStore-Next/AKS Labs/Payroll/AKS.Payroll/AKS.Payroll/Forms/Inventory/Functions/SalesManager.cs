﻿using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class CustomerListVM
    {
        public string CustomerName { get; set; }

        [Key]
        public string MobileNo { get; set; }
    }

    /// <summary>
    /// Model/Temeplete class to make manager/VM class
    /// </summary>
    public abstract class Manager
    {
        protected static AzurePayrollDbContext azureDb;
        protected static LocalPayrollDbContext localDb;
        protected static string StoreCode = "ARD";//TODO: Need to Assign

        /// <summary>
        /// Helper function if missing
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string[] EnumList(Type t)
        {
            return Enum.GetNames(t);
        }

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
        public InvoiceType InvoiceType;
        public ObservableListSource<ProductSale> Items;
        public List<PaymentDetail> PaymentDetails;
        public AutoCompleteStringCollection barcodeList = new AutoCompleteStringCollection();
        public bool ReturnKey = false;
        
        //private List<SaleItem> SalesItems;
        public ObservableListSource<SaleItemVM> SaleItem;

        public List<int> YearList;
        private int SeletedYear;
        private int TotalCount;
        //        private bool IsNew;        private ProductSale Sale;

        // Cart Information
        public decimal TotalQty, TotalFreeQty, TotalTax, TotalDiscount, TotalAmount,TotalItem;

        public SalesManager(AzurePayrollDbContext db, LocalPayrollDbContext ldb, InvoiceType? iType)
        {
            azureDb = db; localDb = ldb;
            if (iType == null) InvoiceType = InvoiceType.ManualSale;
            else InvoiceType = iType.Value;
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
               TaxRate = SetTaxRate(item.Product.ProductCategory, item.Product.MRP)
           }).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Init the manager Class
        /// </summary>
        /// <param name="type"></param>
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

        public void ResetCart()
        {
            TotalAmount =TotalItem= TotalDiscount = TotalTax = TotalQty = TotalFreeQty = 0;
            TotalCount = 0;
            //dgvSaleItems.Rows.Clear();
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
            
            return azureDb.Customers.Select(c => new CustomerListVM { MobileNo = c.MobileNo, CustomerName = c.CustomerName }).OrderBy(c => c.MobileNo).ToList();
        }

        protected static void BasicRateCalucaltion(decimal mrp, decimal taxRate)
        {
            decimal price = 100 * mrp / (100 + taxRate);
            decimal taxAmount = mrp - price;
        }

        protected static int SetTaxRate(ProductCategory category, decimal Price)
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

        private void UpdateSaleList(List<ProductSale> sales)
        {
            if (sales != null)
                foreach (var item in sales)
                    Items.Add(item);
        }
        public void SaveInvoice(string mobileNo, string smId)
        {
            ProductSale sale = new ProductSale { 
            Adjusted=false,BilledQty=TotalQty, EntryStatus=EntryStatus.Added, 
            FreeQty=TotalFreeQty, IsReadOnly=false, OnDate=DateTime.Now,
            Paid=false, StoreId=StoreCode, MarkedDeleted=false, TotalPrice=TotalAmount, 
            TotalDiscountAmount=TotalDiscount, TotalTaxAmount=TotalTax, 
            UserId="WinUI", Tailoring=false, Taxed=false,
            Items= new List<SaleItem>(), InvoiceType=InvoiceType,  SalesmanId=smId,
            };
            sale.TotalMRP = sale.TotalDiscountAmount + sale.TotalPrice;
            sale.TotalBasicAmount = sale.TotalPrice-sale.TotalTaxAmount;
            sale.RoundOff=Math.Round(sale.TotalPrice)-sale.TotalPrice;
            //TODO:handle customer addidtion

            // Adding sale item 
            foreach (var si in SaleItem)
            {
                SaleItem item = new SaleItem {
                Adjusted=false,Barcode=si.Barcode, BilledQty=si.Qty, 
                DiscountAmount=si.Discount, FreeQty=0 
                };
            }

        }

        //TODO: Move to SaleReport class
        private List<SaleReport> SaleReports(string storeCode, int year, int month)
        {
            var x= azureDb.ProductSales.Where(c => c.StoreId == storeCode
            && c.MarkedDeleted == false && !c.Adjusted
            && c.OnDate.Year == year && c.OnDate.Month == month)
           .GroupBy(c => new { c.OnDate.Year, c.InvoiceType, c.Tailoring })
            .Select(c => new SaleReport
            {
                Year=year,Month=month,
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
        private List<List<SaleReport>> SaleReports(string storeCode, int year)
        {
            List<List<SaleReport>> saleReports = new List<List<SaleReport>>();
            for (int i = 1; i <= 12; i++)
            {
                saleReports.Add(SaleReports(storeCode, year, i));
            }
            return saleReports;
        }
        public SortedDictionary<int, List<List<SaleReport>>> SaleReports(string storeCode)
        {
            SortedDictionary<int, List<List<SaleReport>>> report = new SortedDictionary<int, List<List<SaleReport>>>();
            var yearList = azureDb.ProductSales.Where(c => c.StoreId == storeCode).GroupBy(c => c.OnDate.Year).Select(c => c.Key).ToList();
            foreach (var year in YearList)
            {
               report.Add(year,  SaleReports(storeCode, year));
            }
            return report;

        }
    }
    public class SaleReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public bool Tailoing { get; set; }
        public decimal BillQty { get; set; }
        public decimal FreeQty { get; set; }
        public decimal TotalMRP { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalPrice { get; set; }
    }
}