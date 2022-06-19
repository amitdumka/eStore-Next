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
        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadData(); 
        }
    }
}
