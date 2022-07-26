using AKS.Payroll.Database;
using AKS.Payroll.Ops;
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
    public partial class PurchaseForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        DataTable DataTable;
        InventoryManager _im;
        public PurchaseForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            _im = new InventoryManager(azureDb, localDb, "ARD");
        }
        public void LoadStock()
        {
            if (azureDb.Stocks.Local == null && azureDb.Stocks.Local.Count <= 0)
                azureDb.Stocks.Load();

            dgvPurchase.DataSource = azureDb.Stocks.Local.ToBindingList();
        }
        public void LoadInvoice()
        {
            if (azureDb.PurchaseItems.Local.Count <= 0)
                azureDb.PurchaseItems.Load();

            dgvPurchase.DataSource = azureDb.PurchaseItems.Local.OrderBy(c => c.Barcode).ToList();
        }

        private void rbInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInvoices.Checked)
                LoadInvoice();
        }

        private void rbStocks_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStocks.Checked) LoadStock();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                //DataTable = ImportData.ReadExcelToDatatable("d:\\invoice.xlsx", 
                //    Utils.ReadInt(txtQty), Utils.ReadInt(txtProductItem), 
                //    Utils.ReadInt(txtRate), Utils.ReadInt(txtDiscount));

                DataTable = ImportData.ReadExcelToDatatable("d:\\invoice.xlsx",6, 1,7037, 16);

                dgvPurchase.DataSource = DataTable;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                _im.TryCatnSize(DataTable, dgvPurchase,lbYearList, listBox1);
            }
            catch (Exception ex)
            {

               Console.WriteLine(ex.Message);
            }
            

        }
    }
}
