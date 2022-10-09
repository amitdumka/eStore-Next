using AKS.Payroll.Database;
using AKS.Payroll.Forms.Inventory.Functions;
using AKS.Payroll.Ops;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public partial class PurchaseForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private DataTable DataTable;
        private InventoryManager _im;

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

        private List<List<PurchaseProduct>> x;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //dgvPurchase.DataSource = _im.UpdateFabricCostPriceWithFreigtCharge();
            // x=Inventory.ValidatePurchaseInvoice(azureDb, pp);
            // dgvPurchase.DataSource = x[1].OrderBy(c => c.EntryStatus).ThenBy(c=>c.InvoiceNo).ToList();
            //dgvPurchase.DataSource= azureDb.SaleItems.ToList();
            //Histories = StockHistory.AllStockHistory(azureDb, "ARD");
            // foreach (var item in Histories)
            // {
            //    lbYearList.Items.Add(item.Key);
            //    _ = Utils.ToJsonAsync(@"d:\arp\his\" + item.Key + ".json", item.Value);
            // }
        }

        private SortedDictionary<string, List<StockHistory>> Histories;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
               // DataTable = ImportData.ReadExcelToDatatable("d:\\invoice.xlsx",
                //    Utils.ReadInt(txtQty), Utils.ReadInt(txtProductItem),
                //    Utils.ReadInt(txtRate), Utils.ReadInt(txtDiscount));

                // DataTable =  //sale summary
                //DataTable = ImportData.ReadExcelToDatatable("d:\\salebill.xlsx", 7, 1, 6900, 12);
                // DataTable = ImportData.ReadExcelToDatatable(@"d:\saleinv.xlsx", 7, 1, 13767, 29);
                //DataTable = ImportData.ReadExcelToDatatable(@"d:\manual.xlsx", 1, 1, 335, 11);
                //dgvPurchase.DataSource = DataTable;
                //dgvPurchase.DataSource = SaleInventory.AddTailoringBarcodeAsync(azureDb);

                // ManualSale.ProcessManualImport(azureDb, dgvPurchase);
                // MSSaleInventory.ReadWebExcel(azureDb, dgvPurchase);
               // MSSaleInventory.ReadWebExcel(azureDb, dgvPurchase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private List<PurchaseProduct> pp;

        private async void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                //_im.TryCatnSize(DataTable, dgvPurchase,lbYearList, listBox1);
                // dgvPurchase.DataSource = _im.ProcessStocks(DataTable);
                // MessageBox.Show("Rows="+dgvPurchase.Rows.Count);
                // dgvPurchase.DataSource = pp= Inventory.GeneratePurchaseInvoice(azureDb, DataTable);
                // var data = Inventory.ProcessPurchaseItem(azureDb, DataTable);
                //
                // listBox1.DataSource= data;

                ///dgvPurchase.DataSource = data;
                //listBox1.DataSource = Inventory.ValidatePurchaseItem(azureDb).Result;
                // Inventory.UpDateStockList(azureDb, dgvPurchase);
                // dgvPurchase.DataSource= Inventory.UpdateUnit(azureDb);
                // dgvPurchase.DataSource = SaleInventory.ProcessSaleInvoice(azureDb, DataTable);
                // MessageBox.Show("saved:x= "+SaleInventory.JsonSaleEntry(azureDb,DataTable));
                //MessageBox.Show("saved:x= "+SaleInventory.JsonSaleEntry(azureDb,DataTable));
                //int x = SaleInventory.StockUpdate(azureDb);
                //MessageBox.Show("saved:x= " + x);
                // dgvPurchase.DataSource = ManualSale.UploadManual(azureDb, DataTable);
                // MessageBox.Show("saved:x= " + SaleInventory.ProcessSaleItemFromJsonFileAsync(azureDb));
                //SaleInventory.ReverifyAsync(azureDb, dgvPurchase);
                //int x= SaleInventory.UpdateUnit(azureDb);
                // MessageBox.Show("UNIT:x= " + x);
                // x = await SaleInventory.UpdateSM(azureDb);
                // MessageBox.Show("SM:x= " + x);
                //int x = await SaleInventory.UpdateTaxAmount(azureDb);
                // MessageBox.Show("SM:x= " + x);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dgvPurchase.DataSource = Histories.First(c => c.Key == lbYearList.SelectedValue).Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}