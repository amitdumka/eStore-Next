﻿using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public partial class SalesForm : Form
    {
        private InvoiceType InvoiceType;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private ObservableListSource<ProductSale> Items;

        private ProductSale Sale;
        private List<SaleItem> SalesItems;
        private int SeletedYear;
        private List<int> YearList;
        private bool IsNew;
        private bool ReturnKey = false;

        // Cart Information
        private decimal TotalQty, TotalFreeQty, TotalTax, TotalDiscount, TotalAmount;

        private int TotalCount;

        public SalesForm()
        {
            InitializeComponent();
            InvoiceType = InvoiceType.ManualSale;
        }

        public SalesForm(InvoiceType type)
        {
            InitializeComponent();
            InvoiceType = type;
        }

        private void SetupForm()
        {
            switch (InvoiceType)
            {
                case InvoiceType.Sales:
                    rbRegular.Checked = true;
                    this.Text = "Regular Invoice";
                    break;

                case InvoiceType.SalesReturn:
                    rbRegular.Checked = true;
                    cbSalesReturn.Checked = true;
                    this.Text = "Regular Sale's Invoice";
                    break;

                case InvoiceType.ManualSale:
                    rbManual.Checked = true;
                    this.Text = "Manual Invoice";
                    break;

                case InvoiceType.ManualSaleReturn:
                    rbManual.Checked = true;
                    cbSalesReturn.Checked = true;
                    this.Text = "Manual Sale's Return Invoice";
                    break;

                default:
                    rbManual.Checked = true;
                    this.Text = "Manual Invoice";
                    break;
            }
        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRegular.Checked)
            {
                if (cbSalesReturn.Checked)
                    InvoiceType = InvoiceType.SalesReturn;
                else
                    InvoiceType = InvoiceType.Sales;
            }
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManual.Checked)
            {
                if (cbSalesReturn.Checked)
                    InvoiceType = InvoiceType.ManualSaleReturn;
                else
                    InvoiceType = InvoiceType.ManualSale;
            }
        }

        private void cbSalesReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSalesReturn.Checked)
            {
                if (rbManual.Checked)
                    InvoiceType = InvoiceType.ManualSaleReturn;
                else if (rbRegular.Checked)
                    InvoiceType = InvoiceType.SalesReturn;
            }
            else
            {
                if (rbManual.Checked)
                    InvoiceType = InvoiceType.ManualSale;
                else if (rbManual.Checked)
                    InvoiceType = InvoiceType.Sales;
            }
        }

        private void UpdateSaleList(List<ProductSale> sales)
        {
            foreach (var item in sales)
            {
                Items.Add(item);
            }
        }

        private void LoadData()
        {
            SetupForm();
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            Items = new ObservableListSource<ProductSale>();

            SeletedYear = DateTime.Today.Year;
            YearList = azureDb.ProductSales.Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
            lbYearList.DataSource = YearList;

            UpdateSaleList(azureDb.ProductSales.Include(c => c.Items)
                .Where(c => c.OnDate.Year == SeletedYear).OrderByDescending(c => c.OnDate)
                .ToList());

            dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
        }

        private void SetGridView()
        {
            dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadFormData()
        {
            cbxMmobile.DataSource = azureDb.Customers.Select(c => new { c.MobileNo, c.CustomerName, c.FirstName }).OrderBy(c => c.FirstName).ToList();
            cbxMmobile.DisplayMember = "MobileNo";
            cbxMmobile.ValueMember = "CustomerName";

            cbxInvType.Items.AddRange(Enum.GetNames(typeof(InvoiceType)));
            
            SaleItem = new ObservableListSource<SaleItemVM>();
            dgvSaleItems.DataSource = SaleItem;
        }

        private void cbxMmobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomerName.Text = (string)cbxMmobile.SelectedValue;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            //if (ReturnKey)
            //{
            //    MessageBox.Show(txtBarcode.Text);
            //    ReturnKey = false;
            //}
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if(e.KeyChar==Keys.Enter)
            // MessageBox.Show(e.KeyChar + "");
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    MessageBox.Show("Enter Is Pressed!");
            //    ReturnKey = true;
            //}
        }

        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                MessageBox.Show("Tab is pressed\n" + txtBarcode.Text.Trim());
                ReturnKey = true;
                HandleBarcodeEntry();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter\n" + txtBarcode.Text.Trim());
                ReturnKey = true;
                HandleBarcodeEntry();
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Show Dailog to Add Customer.
        }

        private void ResetCard()
        {
            this.TotalAmount = this.TotalDiscount = this.TotalTax = TotalQty = TotalFreeQty = 0;
            TotalCount = 0;
            dgvSaleItems.Rows.Clear();
        }

        private void BasicRateCalucaltion(decimal mrp, decimal taxRate)
        {
            decimal price = (100 * mrp) / (100 + taxRate);
            decimal taxAmount = mrp - price;
        }

        private void UpdateCart()
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                LoadFormData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           


        }
        private ObservableListSource<SaleItemVM> SaleItem;
        private void DisplayStockInfo(StockInfo info)
        {
            txtQty.Text = info.Qty.ToString();
            txtRate.Text = info.Rate.ToString();
            txtValue.Text = (info.Qty * info.Rate).ToString();
            txtProductItem.Text = info.ProductItem.ToString();
            txtDiscount.Text = "0";
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text.Contains('%'))
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()));
                    x -= x * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                    txtValue.Text = x.ToString();
                }
                else
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()))
                        - decimal.Parse(txtDiscount.Text.Trim());
                    txtValue.Text = x.ToString();
                }
            }
            catch
            {
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text.Contains('%'))
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()));
                    x -= x * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                    txtValue.Text = x.ToString();
                }
                else
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()))
                        - decimal.Parse(txtDiscount.Text.Trim());
                    txtValue.Text = x.ToString();
                }
            }
            catch
            {
            }
        }

        private void AddToCart()
        {
            try
            {
                var si = new SaleItemVM
                {
                    Barcode = txtBarcode.Text.Trim(),
                    Rate = decimal.Parse(txtRate.Text.Trim()),
                    ProductItem = txtProductItem.Text.Trim(),
                    Qty = decimal.Parse(txtQty.Text.Trim()),
                    Amount = 0,
                    Tax = 0,
                };

                if (txtDiscount.Text.Contains('%'))
                {
                    si.Discount = si.Qty * si.Rate * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                }
                else
                    si.Discount = decimal.Parse(txtDiscount.Text.Trim());

                si.Amount = (si.Rate * si.Qty) - si.Discount;

                SaleItem.Add(si);
                txtBarcode.Text ="";
                txtQty.Text = "0";
                txtRate.Text = "0";
                txtProductItem.Text = "";
                txtDiscount.Text = "0";

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void HandleBarcodeEntry()
        {
            if (ReturnKey)
                DisplayStockInfo(GetItemDetail(txtBarcode.Text.Trim()));
        }

        /// <summary>
        /// return stock info. Add to API/DataModel
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private StockInfo? GetItemDetail(string barcode)
        {
            if (barcode.Length < 10)
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
        /// make static and common functions
        /// </summary>
        /// <param name="category"></param>
        /// <param name="Price"></param>
        /// <returns></returns>

        private static int SetTaxRate(ProductCategory category, decimal Price)
        {
            int rate = 0;
            switch (category)
            {
                case ProductCategory.Fabric:
                    rate = 5;
                    break;

                case ProductCategory.ReadyMade:
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
    }

    public class StockInfo
    {
        public string Barcode { get; set; }
        public string ProductItem { get; set; }
        public decimal Qty { get; set; }
        public decimal HoldQty { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxRate { get; set; }
        public TaxType TaxType { get; set; }
        public Unit Unit { get; set; }
        public ProductCategory Category { get; set; }
    }

    public class SaleItemVM
    {
        public string Barcode { get; set; }
        public string ProductItem { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Amount { get; set; }
    }
}