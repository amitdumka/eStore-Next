using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //LoadData();
        }

        private void LoadFormData()
        {
            cbxMmobile.DataSource = azureDb.Customers.Select(c => new { c.MobileNo, c.CustomerName }).OrderBy(c => c.CustomerName).ToList();
            cbxMmobile.DisplayMember = "MobieNo";
            cbxMmobile.ValueMember = "CustomerName";

            cbxInvType.Items.AddRange(Enum.GetNames(typeof(InvoiceType)));

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
            if (ReturnKey)
            {
                MessageBox.Show(txtBarcode.Text);
                ReturnKey = false;
            }
        }
        private bool ReturnKey = false;
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if(e.KeyChar==Keys.Enter)
            MessageBox.Show(e.KeyChar + "");
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter Is Pressed!");
                ReturnKey = true;
            }

        }

        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                MessageBox.Show("Tab is pressed");
                ReturnKey = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter");
                ReturnKey = true;
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
            decimal price = (100*mrp)/(100+taxRate);
            decimal taxAmount = mrp - price;
        }

        private void UpdateCart()
        {

        }
        private void DisplayStockInfo(StockInfo info)
        {
            txtQty.Text = info.Qty.ToString();
            txtRate.Text = info.Rate.ToString();
            txtValue.Text = (info.Qty * info.Rate).ToString(); 
            txtProductItem.Text=info.ProductItem.ToString();
        }
        private void AddToCart()
        {
            var si = new SaleItemVM
            {
                Barcode = txtBarcode.Text.Trim(),
                Rate = decimal.Parse(txtRate.Text.Trim()),
                Discount = decimal.Parse(txtDiscount.Text.Trim()),
                ProductItem = txtProductItem.Text.Trim(),
                Qty = decimal.Parse(txtQty.Text.Trim()),
                Amount = 0,
                Tax = 0,
            };
            si.Amount = (si.Rate * si.Qty) - si.Discount;

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
               TaxRate = SetTaxRate(item.Product.ProductCategory,item.Product.MRP)
           }).FirstOrDefault();
            return item;
        }
        /// <summary>
        /// make static and common functions
        /// </summary>
        /// <param name="category"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        private int SetTaxRate(ProductCategory category, decimal Price)
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
