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
                if(rbManual.Checked)
                    InvoiceType = InvoiceType.ManualSaleReturn;
                else if(rbRegular.Checked)
                    InvoiceType = InvoiceType.SalesReturn;
            }
            else
            {
                if (rbManual.Checked)
                    InvoiceType = InvoiceType.ManualSale;
                else  if (rbManual.Checked)
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
                .Where(c=>c.OnDate.Year==SeletedYear).OrderByDescending(c=>c.OnDate)
                .ToList());

            dataGridView1.DataSource=Items.Where(c=>c.InvoiceType==InvoiceType).ToList();

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
            cbxMmobile.DataSource= azureDb.Customers.Select(c=> new { c.MobileNo, c.CustomerName}).OrderBy(c=>c.CustomerName).ToList();
            cbxMmobile.DisplayMember = "MobieNo";
            cbxMmobile.ValueMember = "CustomerName";
            
            cbxInvType.Items.AddRange(Enum.GetNames(typeof(InvoiceType)));

        }

        private void cbxMmobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomerName.Text = (string) cbxMmobile.SelectedValue;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Show Dailog to Add Customer. 
        }

        private void ResetCard()
        {
            this.TotalAmount=this.TotalDiscount=this.TotalTax=TotalQty=TotalFreeQty=0;
            TotalCount = 0;
            dgvSaleItems.Rows.Clear();

        }

        private void UpdateCart()
        {

        }
        public void AddToCart()
        {
            var si = new SaleItemVM
            {
                Barcode = txtBarcode.Text.Trim(), 
                Rate=decimal.Parse(txtRate.Text.Trim()), 

            };
        }


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
