using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;

namespace AKS.UI.Accounting.Forms
{
    public partial class DailySaleEntryForm : Form
    {
        private DailySaleViewModel _viewModel;

        public DailySaleEntryForm(DailySaleViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            _viewModel.isNew = true;

        }

        public DailySaleEntryForm(DailySaleViewModel vm, DailySale daily)
        {
            InitializeComponent();
            _viewModel.PrimaryEntity = daily;
            _viewModel.isNew = false;
            btnAdd.Text = "Edit";
            txtInvoiceNumber.Enabled = false;
            _viewModel = vm;
        }

        public string DeletedI { get; set; }
        public bool IsSaved { get; set; }

        public DialogResult DeleteBox(string name)
        {
            return MessageBox.Show($"Are you sure to delete this {name} ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Edit") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Save")
            {
                if (_viewModel.SaveSale(ReadData()))
                {
                    MessageBox.Show("Invoice is saved");
                    btnAdd.Text = "Add";
                    //TODO: ClearFields();
                    if (_viewModel.isNew) this.DialogResult = DialogResult.OK;
                    else this.DialogResult = DialogResult.Yes;
                    IsSaved = true;
                }
                else
                {
                    MessageBox.Show("An error occurred while saving");
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            //TODO: ClearFields();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = DeleteBox("Sale ");
            if (confirmResult == DialogResult.Yes)
            {
                if (_viewModel.PrimaryEntity.OnDate.Date > new DateTime(2022, 3, 31))
                {
                    if (_viewModel.DeleteSale())
                    {
                        MessageBox.Show("Sale is deleted!", "Delete");
                        this.DeletedI = _viewModel.PrimaryEntity.InvoiceNumber;
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

        private void cbxStores_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((string)cbxStores.SelectedValue) != CurrentSession.StoreCode)
                {
                    CurrentSession.StoreCode = (string)cbxStores.SelectedValue;
                    ReloadComoboData();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void DailySaleEntryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DisplayData()
        {
            try
            {
                cbxStores.SelectedValue = _viewModel.PrimaryEntity.StoreId;
                txtAmount.Text = _viewModel.PrimaryEntity.Amount.ToString();
                txtCash.Text = _viewModel.PrimaryEntity.CashAmount.ToString();
                txtNonCash.Text = _viewModel.PrimaryEntity.NonCashAmount.ToString();
                dtpOnDate.Value = _viewModel.PrimaryEntity.OnDate;
                cbxPaymentMode.SelectedIndex = (int)_viewModel.PrimaryEntity.PayMode;
                cbxPOS.SelectedValue = _viewModel.PrimaryEntity.EDCTerminalId != null ? _viewModel.PrimaryEntity.EDCTerminalId : "";
                cbxSaleman.SelectedValue = _viewModel.PrimaryEntity.SalesmanId;
                cbManual.Checked = _viewModel.PrimaryEntity.ManualBill;
                cbSalesReturn.Checked = _viewModel.PrimaryEntity.SalesReturn;
                cbTailoring.Checked = _viewModel.PrimaryEntity.TailoringBill;
                cbDue.Checked = _viewModel.PrimaryEntity.IsDue;
                txtInvoiceNumber.Text = _viewModel.PrimaryEntity.InvoiceNumber.ToString();
                txtRemarks.Text = _viewModel.PrimaryEntity.Remarks.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {
            cbxStores.Enabled = false;
            cbxStores.DisplayMember = "DisplayData";
            cbxStores.ValueMember = "StoreId";

            cbxPOS.DisplayMember = "DisplayData";
            cbxPOS.ValueMember = "ValueData";

            cbxSaleman.DisplayMember = "DisplayData";
            cbxSaleman.ValueMember = "ValueData";

            cbxStores.DataSource = _viewModel.GetStoreList();

            cbxSaleman.DataSource = _viewModel.GetSalesManList();

            cbxPOS.DataSource = _viewModel.GetPosList();
            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PayMode)));
            cbxStores.SelectedValue = CurrentSession.StoreCode;

            if (!_viewModel.isNew) DisplayData();
        }

        private bool ReadData()
        {
            try
            {
                if (_viewModel.isNew)
                {
                    _viewModel.PrimaryEntity = new DailySale
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
                    _viewModel.PrimaryEntity.Amount = decimal.Parse(txtAmount.Text.Trim());
                    _viewModel.PrimaryEntity.CashAmount = decimal.Parse(txtCash.Text.Trim());
                    _viewModel.PrimaryEntity.NonCashAmount = decimal.Parse(txtNonCash.Text.Trim());
                    _viewModel.PrimaryEntity.EDCTerminalId = (string)cbxPOS.SelectedValue;
                    _viewModel.PrimaryEntity.EntryStatus = EntryStatus.Added; _viewModel.PrimaryEntity.InvoiceNumber = txtInvoiceNumber.Text;
                    _viewModel.PrimaryEntity.IsDue = cbDue.Checked; _viewModel.PrimaryEntity.IsReadOnly = false; _viewModel.PrimaryEntity.ManualBill = cbManual.Checked; _viewModel.PrimaryEntity.MarkedDeleted = false;
                    _viewModel.PrimaryEntity.OnDate = dtpOnDate.Value; _viewModel.PrimaryEntity.PayMode = (PayMode)cbxPaymentMode.SelectedIndex;
                    _viewModel.PrimaryEntity.Remarks = txtRemarks.Text; _viewModel.PrimaryEntity.SalesmanId = (string)cbxSaleman.SelectedValue; _viewModel.PrimaryEntity.SalesReturn = cbSalesReturn.Checked;
                    _viewModel.PrimaryEntity.StoreId = (string)cbxStores.SelectedValue; _viewModel.PrimaryEntity.TailoringBill = cbTailoring.Checked; _viewModel.PrimaryEntity.UserId = CurrentSession.UserName;
                }

                if (_viewModel.PrimaryEntity.PayMode != PayMode.Card && _viewModel.PrimaryEntity.PayMode != PayMode.UPI && _viewModel.PrimaryEntity.PayMode == PayMode.Wallets && _viewModel.PrimaryEntity.PayMode != PayMode.MixPayments || _viewModel.PrimaryEntity.PayMode == PayMode.Cash)
                {
                    _viewModel.PrimaryEntity.EDCTerminalId = null;
                }
                else if (string.IsNullOrWhiteSpace(_viewModel.PrimaryEntity.EDCTerminalId) || string.IsNullOrEmpty(_viewModel.PrimaryEntity.EDCTerminalId))
                {
                    _viewModel.PrimaryEntity.EDCTerminalId = null;
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

        private void ReloadComoboData()
        {
            cbxSaleman.DataSource = _viewModel.GetSalesManList();
            cbxPOS.DataSource = _viewModel.GetPosList(); ;
        }
    }
}