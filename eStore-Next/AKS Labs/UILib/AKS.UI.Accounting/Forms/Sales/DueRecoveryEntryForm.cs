using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using System.Data;

namespace AKS.UI.Accounting.Forms.EntryForms
{
    public partial class DueRecoveryEntryForm : Form
    {
        class Invoice
        {
            public string InvoiceNumber { get; set; }
            public DateTime OnDate { get; set; }
            public decimal Amount { get; set; }
        }

        public bool IsNew = false;
        public bool IsSave = false;
        public string DeleteId;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        public DueRecovery DueRecovery;
        private static string StoreCode = "ARD";

        public DueRecoveryEntryForm()
        {
            InitializeComponent();
            IsNew = true;
        }
        public DueRecoveryEntryForm(DueRecovery recovery)
        {
            InitializeComponent();
            IsNew = false;
            DueRecovery = recovery;
        }

        private void DueRecoveryEntryForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            LoadData();
        }
        private void ReloadInvoice(string store)
        {
            cbxInvoices.DataSource = azureDb.CustomerDues.Where(c => c.StoreId == store && !c.Paid).Select(c => new Invoice
            {
                InvoiceNumber = c.InvoiceNumber,
                OnDate = c.OnDate,
                Amount = c.Amount
            }).OrderBy(c => c.OnDate).ToList();
            cbxInvoices.DisplayMember = "InvoiceNumber";
        }
        private void LoadData()
        {
            cbxStores.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            cbxStores.DisplayMember = "StoreName";
            cbxStores.ValueMember = "StoreId";
            ReloadInvoice(StoreCode);

            cbxInvoices.DisplayMember = "InvoiceNumber";
            cbxPayMode.Items.AddRange(Enum.GetNames(typeof(PayMode)));

            if (!IsNew) DisplayData();


        }

        private void cbxInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var val = (Invoice)cbxInvoices.SelectedValue;
            txtDueAmount.Text = val.Amount.ToString();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Save")
            {
                ReadData();
                if (SaveData())
                {
                    btnAdd.Text = "Add";
                    if (IsNew)
                        DialogResult = DialogResult.OK;
                    else DialogResult = DialogResult.Yes;
                    MessageBox.Show("Recovery entry saved!!!");
                    IsSave = true;
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Not able to save it!");
                }
            }
        }

        void ReadData()
        {



            if (IsNew)
            {

                DueRecovery = new DueRecovery
                {
                    Amount = decimal.Parse(txtPaidAmount.Text.Trim()),
                    EntryStatus = EntryStatus.Added,
                    InvoiceNumber = ((Invoice)cbxInvoices.SelectedValue).InvoiceNumber,
                    MarkedDeleted = false,
                    IsReadOnly = false,
                    OnDate = dtpOnDate.Value,
                    PayMode = (PayMode)cbxPayMode.SelectedIndex,
                    UserId = CurrentSession.UserName,
                    StoreId = cbxStores.SelectedValue.ToString(),
                    Remarks = txtRemarks.Text.Trim(),
                    ParticialPayment = cbParticalPayment.Checked,
                    Id = $"DR/{dtpOnDate.Value.Year}/{dtpOnDate.Value.Month}/{dtpOnDate.Value.Day}/{cbxInvoices.SelectedText}/{new Random().Next(100)}"
                };
            }
            else
            {
                DueRecovery.Amount = decimal.Parse(txtPaidAmount.Text.Trim());
                DueRecovery.EntryStatus = EntryStatus.Updated;
                DueRecovery.InvoiceNumber = ((Invoice)cbxInvoices.SelectedValue).InvoiceNumber;
                DueRecovery.MarkedDeleted = false;
                DueRecovery.IsReadOnly = false;
                DueRecovery.OnDate = dtpOnDate.Value;
                DueRecovery.PayMode = (PayMode)cbxPayMode.SelectedIndex;
                DueRecovery.UserId = CurrentSession.UserName;
                DueRecovery.StoreId = cbxStores.SelectedValue.ToString();
                DueRecovery.Remarks = txtRemarks.Text.Trim();
                DueRecovery.ParticialPayment = cbParticalPayment.Checked;
            }

        }
        bool SaveData()
        {

            if (IsNew)
                azureDb.DueRecovery.Add(DueRecovery);
            else azureDb.DueRecovery.Update(DueRecovery);
            if (!DueRecovery.ParticialPayment)
            {
                //TODO: marked for Paid bill one payment is done full , add Paid field in DailySale
                var sale = azureDb.CustomerDues.Where(c => c.InvoiceNumber == DueRecovery.InvoiceNumber).FirstOrDefault();
                if (sale != null)
                {
                    sale.Paid = true;
                    azureDb.CustomerDues.Update(sale);
                }
            }
            if (string.IsNullOrEmpty(DueRecovery.InvoiceNumber))
                return false;
            return azureDb.SaveChanges() > 0;
        }

        private void cbxStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadInvoice(cbxStores.SelectedValue.ToString());

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Do you want to delete!", "Delete", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                azureDb.DueRecovery.Remove(DueRecovery);
                if (!DueRecovery.ParticialPayment)
                {
                    var due = azureDb.CustomerDues.Where(c => c.InvoiceNumber == DueRecovery.InvoiceNumber).FirstOrDefault();
                    if (due != null)
                    {
                        due.Paid = false;
                        azureDb.CustomerDues.Update(due);
                    }
                }
                if (azureDb.SaveChanges() > 0)
                {
                    DeleteId = DueRecovery.InvoiceNumber;
                    MessageBox.Show("Deleted");
                    DialogResult = DialogResult = DialogResult.No;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to delete");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DisplayData()
        {
            txtPaidAmount.Text = DueRecovery.Amount.ToString();
            txtRemarks.Text = DueRecovery.Remarks.ToString();
            cbParticalPayment.Checked = DueRecovery.ParticialPayment;
            cbxInvoices.SelectedText = DueRecovery.InvoiceNumber;
            cbxPayMode.SelectedIndex = (int)DueRecovery.PayMode;
            cbxStores.SelectedValue = DueRecovery.StoreId;
            dtpOnDate.Value = DueRecovery.OnDate;

        }
    }
}
