using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using System.Data;

namespace AKS.Accounting.Forms
{
    public partial class DailySaleEntryForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;

        private static string StoreCode = "ARD";
        public DailySale sale;
        public CustomerDue CustomerDue;
        private bool isNew;
        public string DeletedI { get; set; }
        public bool IsSaved { get; set; }
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
            btnAdd.Text = "Edit";
            txtInvoiceNumber.Enabled = false;

        }

        private void DisplayData()
        {
            try
            {
                cbxStores.SelectedValue = sale.StoreId;
                txtAmount.Text = sale.Amount.ToString();
                txtCash.Text = sale.CashAmount.ToString();
                txtNonCash.Text = sale.NonCashAmount.ToString();
                dtpOnDate.Value = sale.OnDate;
                cbxPaymentMode.SelectedIndex = (int)sale.PayMode;
                cbxPOS.SelectedValue = sale.EDCTerminalId != null ? sale.EDCTerminalId : "";
                cbxSaleman.SelectedValue = sale.SalesmanId;
                cbManual.Checked = sale.ManualBill;
                cbSalesReturn.Checked = sale.SalesReturn;
                cbTailoring.Checked = sale.TailoringBill;
                cbDue.Checked = sale.IsDue;
                txtInvoiceNumber.Text = sale.InvoiceNumber.ToString();
                txtRemarks.Text = sale.Remarks.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


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

            if (!isNew) DisplayData();
        }

        private void ReloadComoboData()
        {
            cbxSaleman.DataSource = azureDb.Salesmen.Where(c => c.StoreId == StoreCode && c.IsActive).Select(c => new { c.SalesmanId, c.Name }).ToList();
            cbxPOS.DataSource = azureDb.EDCTerminals.Where(c => c.StoreId == StoreCode && c.Active).Select(c => new { c.EDCTerminalId, c.Name }).ToList();
        }

        private bool ReadData()
        {
            try
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
                        SalesmanId = (string)cbxSaleman.SelectedValue,
                        SalesReturn = cbSalesReturn.Checked,
                        StoreId = (string)cbxStores.SelectedValue,
                        TailoringBill = cbTailoring.Checked,
                        UserId = CurrentSession.UserName
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
                    sale.Remarks = txtRemarks.Text; sale.SalesmanId = (string)cbxSaleman.SelectedValue; sale.SalesReturn = cbSalesReturn.Checked;
                    sale.StoreId = (string)cbxStores.SelectedValue; sale.TailoringBill = cbTailoring.Checked; sale.UserId = CurrentSession.UserName;
                }

                if (sale.PayMode != PayMode.Card && sale.PayMode != PayMode.UPI && sale.PayMode == PayMode.Wallets && sale.PayMode != PayMode.MixPayments || sale.PayMode == PayMode.Cash)
                {
                    sale.EDCTerminalId = null;
                }
                else if (string.IsNullOrWhiteSpace(sale.EDCTerminalId) || string.IsNullOrEmpty(sale.EDCTerminalId))
                {
                    sale.EDCTerminalId = null;
                }
                else
                {
                    Console.Write("");
                }

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
        }
        private void SaveDue()
        {
            //TODO: Ideal is meet, when due is not present then add, when due present and edit due removed. 
            CustomerDue = new()
            {
                InvoiceNumber = sale.InvoiceNumber,
                Amount = sale.Amount,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = false,
                MarkedDeleted = false,
                OnDate = sale.OnDate,
                Paid = false,
                StoreId = sale.StoreId,
                UserId = sale.UserId,

            };
            if (isNew)
                azureDb.CustomerDues.Add(CustomerDue);
            else azureDb.CustomerDues.Update(CustomerDue);
        }
        private void SavePaymentData()
        {
            //TODO: Implement this . for banking input. 
        }
        private bool SaveData()
        {
            try
            {
                if (sale.IsDue) SaveDue();

                if (isNew)
                    azureDb.DailySales.Add(sale);
                else azureDb.DailySales.Update(sale);

                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void ClearFields() { }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Edit") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Save")
            {

                if (ReadData())
                {
                    if (SaveData())
                    {
                        MessageBox.Show("Invoice is saved");
                        btnAdd.Text = "Add";
                        ClearFields();
                        if (isNew) this.DialogResult = DialogResult.OK;
                        else this.DialogResult = DialogResult.Yes;
                        IsSaved = true;
                    }
                    else
                    {
                        azureDb.DailySales.Remove(sale);
                        if (sale.IsDue) azureDb.CustomerDues.Remove(CustomerDue);
                        MessageBox.Show("An error occured while saving");
                    }
                }
                else MessageBox.Show("An error occured while reading");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = DeleteBox("Sale ");
            if (confirmResult == DialogResult.Yes)
            {
                if (sale.OnDate.Date > new DateTime(2022, 3, 31))
                {
                    azureDb.DailySales.Remove(sale);
                    if (sale.IsDue)
                    {
                        var d = azureDb.CustomerDues.Where(c => c.InvoiceNumber == sale.InvoiceNumber).FirstOrDefault();
                        if (d != null) { azureDb.CustomerDues.Remove(d); }
                    }
                    if (azureDb.SaveChanges() > 0)
                    {
                        MessageBox.Show("Sale is deleted!", "Delete");
                        this.DeletedI = sale.InvoiceNumber;
                        this.DialogResult = DialogResult.No;
                        this.Close();
                    }
                    else MessageBox.Show("Failed to delete, please try again!", "Delete");
                }
                else
                {
                    MessageBox.Show("Sale before 1st April 2022 cannot be deleted. You don't have accessed", "Access Denied");
                }

            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
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


        private void cbxPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((PayMode)cbxPaymentMode.SelectedIndex == PayMode.Cash)
            {
                cbxPOS.Enabled = false;
                txtNonCash.Enabled = false;
                txtNonCash.Text = "0";
                txtCash.Text = "0";
            }
            else
            {
                cbxPOS.Enabled = true;
                txtNonCash.Enabled = true;
                txtNonCash.Text = "0";
                txtCash.Text = "0";
            }
        }

        public DialogResult DeleteBox(string name)
        {
            return MessageBox.Show($"Are you sure to delete this {name} ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            //if (confirmResult == DialogResult.Yes)
            //{
            //    if (newAtt.OnDate.Date > new DateTime(2022, 3, 31))
            //    {
            //        db.Attendances.Remove(newAtt);
            //        if (db.SaveChanges() > 0)
            //        {
            //            MessageBox.Show("Attendance is deleted!", "Delete");
            //            this.DeletedAttednance = newAtt.AttendanceId;
            //            this.DialogResult = DialogResult.OK;
            //            this.Close();
            //        }
            //        else MessageBox.Show("Failed to delete, please try again!", "Delete");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Attendace before 1st April 2022 cannot be deleted. You don't have accessed", "Access Denied");
            //    }

            //}
        }

    }
}
