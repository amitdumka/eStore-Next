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
    public partial class PurchaseForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb; 

        public PurchaseForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            azureDb= new AzurePayrollDbContext();
        }
        public void LoadStock()
        {
            if (azureDb.Stocks.Local == null && azureDb.Stocks.Local.Count<=0)
                azureDb.Stocks.Load();

            dgvPurchase.DataSource = azureDb.Stocks.Local.ToBindingList();
        }
        public void LoadInvoice()
        {
            if(  azureDb.PurchaseItems.Local.Count<=0)
            azureDb.PurchaseItems.Load();

            dgvPurchase.DataSource = azureDb.PurchaseItems.Local.OrderBy(c=>c.Barcode).ToList(); 
        }

        private void rbInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInvoices.Checked)
                LoadInvoice();
        }

        private void rbStocks_CheckedChanged(object sender, EventArgs e)
        {
            if(rbStocks.Checked) LoadStock();
        }

        private void ProcessStockUpdate()
        {
            var stock = azureDb.PurchaseItems                
                .Select(c => new Stock { Barcode=c.Barcode, CostPrice=c.CostPrice, EntryStatus=EntryStatus.Added,
                 IsReadOnly=true, MarkedDeleted=false, StoreId="ARD", HoldQty=0, SoldQty=0, Unit=c.Unit, UserId="AUTO", PurhcaseQty=c.Qty
            }).ToList();

            List<Stock> Stocks = new List<Stock>(); 
            //search for dplicate barcode

            //var dup = stock.GroupBy(c => c.Barcode).Select(c=> new {c.Key , ctr=c.Key.Count()}).Where(c => c.ctr > 1).ToList(); 
            foreach (var item in stock)
            {
                if (Stocks.Any(c => c.Barcode == item.Barcode))
                {
                    var s = Stocks.Find(c => c.Barcode == item.Barcode);
                    Stocks.Remove(s);
                    s.PurhcaseQty += item.PurhcaseQty;
                    Stocks.Add(s); 
                }
                else
                {
                    Stocks.Add(item); 

                }

            }
            azureDb.Stocks.AddRange(Stocks);
            var c = azureDb.SaveChanges();
            if (c > 0)
            {
                Console.WriteLine(c);
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ProcessStockUpdate(); 
        }
    }
}
