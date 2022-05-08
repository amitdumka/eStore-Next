using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payroll.Forms
{
    public partial class DailySaleEntryForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private static string StoreCode = "ARD";
        private DailySale sale;
        private bool isNew;
        public DailySaleEntryForm()
        {
            InitializeComponent();
            isNew = true;
        }
        public DailySaleEntryForm(DailySale daily)
        {
            InitializeComponent();
            sale = daily;
            isNew = false;

        }


        private void LoadData()
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();

            cbxStores.DisplayMember = "StoreName";
            cbxStores.ValueMember = "StoreId";

            cbxPOS.DisplayMember = "Name";
            cbxPOS.ValueMember = "EDCTerminalId";

            cbxSaleman.DisplayMember = "Name";
            cbxSaleman.ValueMember = "SalesmanId";

            cbxStores.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            cbxSaleman.DataSource = azureDb.Salesmen.Where(c => c.StoreId == StoreCode && c.IsActive)
                .Select(c => new { c.SalesmanId, c.Name }).ToList();
            cbxPOS.DataSource = azureDb.EDCTerminals.Where(c => c.StoreId == StoreCode && c.Active).Select(c => new { c.EDCTerminalId, c.Name }).ToList();
            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PayMode)));


        }

        private void ReloadComoboData()
        {
            cbxSaleman.DataSource = azureDb.Salesmen.Where(c => c.StoreId == StoreCode && c.IsActive).Select(c => new { c.SalesmanId, c.Name }).ToList();
            cbxPOS.DataSource = azureDb.EDCTerminals.Where(c => c.StoreId == StoreCode && c.Active).Select(c => new { c.EDCTerminalId, c.Name }).ToList();
        }

        private bool ReadData()
        {
            if (isNew)
            {
                sale = new DailySale
                {
                    Amount = decimal.Parse(txtAmount.Text.Trim()),
                    CashAmount = decimal.Parse(txtCash.Text.Trim()),
                    NonCashAmount = decimal.Parse(txtNonCash.Text.Trim()),
                    EDCTerminalId = (string)cbxPOS.SelectedValue,
                    EntryStatus = EntryStatus.Added,
                    InvoiceNumber = txtInvoiceNumber.Text,
                    IsDue = cbDue.Checked,
                    IsReadOnly = false,
                    ManualBill = cbManual.Checked,
                    MarkedDeleted = false,
                    OnDate = dtpOnDate.Value,
                    PayMode = (PayMode)cbxPaymentMode.SelectedIndex,
                    Remarks = txtRemarks.Text,
                    SalemanId = (string)cbxSaleman.SelectedValue,
                    SalesReturn = cbSalesReturn.Checked,
                    StoreId = (string)cbxStores.SelectedValue,
                    TailoringBill = cbTailoring.Checked,
                    UserId = "WinUI"
                };
            }
            else
            {
                sale.Amount = decimal.Parse(txtAmount.Text.Trim());
                sale.CashAmount = decimal.Parse(txtCash.Text.Trim());
                sale.NonCashAmount = decimal.Parse(txtNonCash.Text.Trim());
                sale.EDCTerminalId = (string)cbxPOS.SelectedValue;
                sale.EntryStatus = EntryStatus.Added; sale.InvoiceNumber = txtInvoiceNumber.Text;
                sale.IsDue = cbDue.Checked; sale.IsReadOnly = false; sale.ManualBill = cbManual.Checked; sale.MarkedDeleted = false;
                sale.OnDate = dtpOnDate.Value; sale.PayMode = (PayMode)cbxPaymentMode.SelectedIndex;
                sale.Remarks = txtRemarks.Text; sale.SalemanId = (string)cbxSaleman.SelectedValue; sale.SalesReturn = cbSalesReturn.Checked;
                sale.StoreId = (string)cbxStores.SelectedValue; sale.TailoringBill = cbTailoring.Checked; sale.UserId = "WinUI";
            }
            return true;
        }
        private bool SaveData()
        {

            if(isNew)
                azureDb.DailySales.Add(sale);
            else azureDb.DailySales.Update(sale);
            return azureDb.SaveChanges() > 0;

        }

        private void ClearFields() { }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Edit") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Save") {

                if (ReadData())
                {
                    if (SaveData())
                    {
                        MessageBox.Show("Invoice is saved");
                        btnAdd.Text = "Add";
                        ClearFields();
                        if(isNew) this.DialogResult = DialogResult.OK;
                        else this.DialogResult = DialogResult.Yes;
                    }
                    else MessageBox.Show("An error occured while saving");
                }
                else MessageBox.Show("An error occured while reading");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void DailySaleEntryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cbxStores_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((string)cbxStores.SelectedValue) != StoreCode)
                {
                    StoreCode = (string)cbxStores.SelectedValue;
                    ReloadComoboData();
                }
            }
            catch (Exception err)
            {

                Console.WriteLine(err.Message);
            }

        }
    }
}
