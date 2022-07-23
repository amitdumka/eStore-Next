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

        private void btnDelete_Click(object sender, EventArgs e)
        {
           DataTable = ImportData.ReadExcelToDatatable(txtBarcode.Text.Trim(), ReadInt(txtQty ), ReadInt(txtProductItem), ReadInt(txtRate), ReadInt(txtDiscount));

            dgvPurchase.DataSource = DataTable; 

        }
        private void CreateCategoryList()
        {
            var data = DataTable.AsEnumerable().GroupBy(c => c["Product Name"] ).Select(c=>c.Key.ToString()).ToList();
            List<string> a = new List<string>();
            List<string> b = new List<string>();
            List<string> c = new List<string>();
            foreach (var item in data)
            {
                var x = item.Split("/");
                a.Add(x[0]); 
                b.Add(x[1]);
                c.Add(x[2]);
            }
            a=a.Distinct().ToList();
            b=b.Distinct().ToList();
            c = c.Distinct().ToList();
            c.Sort();
            listBox1.DataSource = a;
            listBox2.DataSource = b; 
            listBox3.DataSource = c;
        }

        private int ReadInt(TextBox t)
        {
            return Int32.Parse(t.Text.Trim());
        }
        private Unit SetUnit(string name)
        {
            if (name.StartsWith("Apparel")) { return Unit.Pcs; }
            else if (name.StartsWith("Promo") || name.StartsWith("Suit Cover") ) { return Unit.Nos; }
            else if (name.StartsWith("Shirting") || name.StartsWith("Suiting")) { return Unit.Meters; }
            return Unit.NoUnit;
        }
        private void SetSize(string style,string category)
        {
            global::Size size;
            var name = style.Substring(style.Length - 4, 4);
            //if (name[1] == 'T')
            //{
            //   name= name.Substring(1);
            //   size= (global::Size) Enum.GetNames(typeof(global::Size)).ToList().IndexOf(name);
            //}
            if (name.EndsWith(global::Size.XXXL.ToString()))
            {

            }
            else if (name.EndsWith(global::Size.XXL.ToString())) { }
            else if (name.EndsWith(global::Size.XL.ToString())) { }
            else if (name.EndsWith(global::Size.L.ToString())) { }
            else if (name.EndsWith(global::Size.M.ToString())) { }
            else if (name.EndsWith(global::Size.S.ToString())) { }
            
            else if (name.EndsWith(global::Size.T28.ToString())) { }
            else if (name.EndsWith(global::Size.XXXL.ToString())) { }
            else if (name.EndsWith(global::Size.XXXL.ToString())) { }
            else if (name.EndsWith(global::Size.XXXL.ToString())) { }
        }
        private void ProcessProductItem()
        {
            for (int i = 0; i < DataTable.Rows.Count; i++)
             
            {
                if (!ProuctItemExist(DataTable.Rows[i]["Barcode"].ToString()))
                {
                    ProductItem item = new ProductItem {
                        Barcode = DataTable.Rows[i]["Barcode"].ToString(),
                        StyleCode = DataTable.Rows[i]["StyleCode"].ToString(),
                        Description = DataTable.Rows[i]["Item Desc"].ToString(),
                        MRP = decimal.Parse(DataTable.Rows[i]["Item Desc"].ToString().Trim()),
                        HSNCode = "",Name= DataTable.Rows[i]["Product Name"].ToString(), TaxType=TaxType.GST, Unit=SetUnit(DataTable.Rows[i]["Product Name"].ToString()), 
                    };
                    var names = DataTable.Rows[i]["Product Name"].ToString().Split("/");
                    if (item.Unit == Unit.Meters || item.Unit==Unit.Nos||item.Unit==Unit.NoUnit) item.Size = global::Size.NS;
                    else
                    {

                    }
                }
                else
                {
                    DataTable.Rows[i]["ExmilCost"] = "Exist";
                }
            }
        }

        public bool ProuctItemExist(string barcode)
        {
            return azureDb.ProductItems.Any(c => c.Barcode == barcode);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            CreateCategoryList(); 

        }
    }
}
