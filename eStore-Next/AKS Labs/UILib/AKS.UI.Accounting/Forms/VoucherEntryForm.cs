using AKS.AccountingSystem.Helpers;
using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using System.Data;

namespace AKS.UI.Accounting.Forms
{
    public partial class VoucherEntryForm : Form
    {
        private VoucherCashViewModel _viewModel;

        //public VoucherType voucherType;
        //public string voucherNumber;
        //public string deleteVoucherNumber;
        //public CashVoucher SavedCashVoucher { get; set; }
        //public Voucher SavedVoucher { get; set; }
        //private CashVoucher _viewModel.SecondaryEntity;
        //private Voucher voucher;

        //public bool isNew = false;

        // private AzurePayrollDbContext azureDb;
        //private LocalPayrollDbContext localDb;

        private void ClearFields()
        {
            if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
                _viewModel.SecondaryEntity = new CashVoucher
                {
                    Amount = 0,
                    OnDate = DateTime.Now
                };
            else _viewModel.PrimaryEntity = new Voucher
            {
                OnDate = DateTime.Now,
                Amount = 0,
                PaymentMode = PaymentMode.Cash
            };
            // DisplayData();
        }

        private void LoadData()
        {
            //Store

            cbxStores.DataSource = _viewModel.GetStoreList();
            cbxStores.DisplayMember = "StoreName";
            cbxStores.ValueMember = "StoreId";

            cbxEmployees.DataSource = _viewModel.GetEmployeeList();
            cbxEmployees.DisplayMember = "StaffName";
            cbxEmployees.ValueMember = "EmployeeId";

            cbxBankAccount.DataSource = _viewModel.GetBankAccountList();
            cbxBankAccount.DisplayMember = "AccountNumber";
            cbxBankAccount.ValueMember = "AccountNumber";

            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PaymentMode)));

            cbxParties.DataSource = _viewModel.GetPartyList();
            cbxParties.DisplayMember = "PartyName";
            cbxParties.ValueMember = "PartyId";

            cbxTranscationMode.DataSource = _viewModel.GetTranscationList();
            cbxTranscationMode.DisplayMember = "TranscationName";
            cbxTranscationMode.ValueMember = "TranscationId";

            ShowView(_viewModel.voucherType);
            SetEntryType();
            if (!_viewModel.isNew) DisplayData();
        }

        private void DisplayData()
        {
            //TODO; All fields are not updated.

            if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
            {
                cbxStores.SelectedValue = _viewModel.SecondaryEntity.StoreId;
                _viewModel.voucherType = _viewModel.SecondaryEntity.VoucherType;
                dtpOnDate.Value = _viewModel.SecondaryEntity.OnDate;
                txtAmount.Text = _viewModel.SecondaryEntity.Amount.ToString();

                txtSlipNo.Text = _viewModel.SecondaryEntity.SlipNumber;
                txtPartyName.Text = _viewModel.SecondaryEntity.PartyName;
                txtRemarks.Text = _viewModel.SecondaryEntity.Remarks;
                cbxParties.SelectedValue = _viewModel.SecondaryEntity.PartyId;
                cbxEmployees.SelectedValue = _viewModel.SecondaryEntity.EmployeeId;
                cbxTranscationMode.SelectedValue = _viewModel.SecondaryEntity.TranscationId;
                txtParticulars.Text = _viewModel.SecondaryEntity.Particulars;
                this.Text = "Cash Voucher #\t" + _viewModel.SecondaryEntity.VoucherNumber;
            }
            else
            {
                cbxStores.SelectedValue = _viewModel.PrimaryEntity.StoreId;
                _viewModel.voucherType = _viewModel.PrimaryEntity.VoucherType;
                dtpOnDate.Value = _viewModel.PrimaryEntity.OnDate;
                txtAmount.Text = _viewModel.PrimaryEntity.Amount.ToString();
                txtSlipNo.Text = _viewModel.PrimaryEntity.SlipNumber;
                txtPartyName.Text = _viewModel.PrimaryEntity.PartyName;
                txtRemarks.Text = _viewModel.PrimaryEntity.Remarks;
                cbxParties.SelectedValue = _viewModel.PrimaryEntity.PartyId;
                cbxEmployees.SelectedValue = _viewModel.PrimaryEntity.EmployeeId;

                cbxBankAccount.SelectedValue = _viewModel.PrimaryEntity.AccountId;
                cbxPaymentMode.SelectedIndex = (int)_viewModel.PrimaryEntity.PaymentMode;
                txtPaymentDetails.Text = _viewModel.PrimaryEntity.PaymentDetails;
                txtParticulars.Text = _viewModel.PrimaryEntity.Particulars;
            }
            SetEntryType();
        }

        private bool ReadData()
        {
            //TODO: Validation of Data is need
            if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
            {
                if (_viewModel.SecondaryEntity == null)
                {
                    _viewModel.SecondaryEntity = new CashVoucher
                    {
                        VoucherType = _viewModel.voucherType,
                        OnDate = dtpOnDate.Value,
                        Remarks = txtRemarks.Text,
                        SlipNumber = txtSlipNo.Text,
                        PartyName = txtPartyName.Text,
                        Amount = decimal.Parse(txtAmount.Text.Trim()),
                        EmployeeId = (string)cbxEmployees.SelectedValue,
                        StoreId = (string)cbxStores.SelectedValue,
                        PartyId = (string)cbxParties.SelectedValue,
                        IsReadOnly = false,
                        MarkedDeleted = false,
                        UserId = CurrentSession.UserName,
                        EntryStatus = _viewModel.isNew ? EntryStatus.Added : EntryStatus.Updated,
                        TranscationId = (string)cbxTranscationMode.SelectedValue,
                        Particulars = txtParticulars.Text.Trim(),
                    };
                }
                else
                {
                    _viewModel.SecondaryEntity.TranscationId = (string)cbxTranscationMode.SelectedValue;
                    _viewModel.SecondaryEntity.VoucherType = _viewModel.voucherType;
                    _viewModel.SecondaryEntity.OnDate = dtpOnDate.Value;
                    _viewModel.SecondaryEntity.Remarks = txtRemarks.Text.Trim();
                    _viewModel.SecondaryEntity.SlipNumber = txtSlipNo.Text.Trim();
                    _viewModel.SecondaryEntity.PartyName = txtPartyName.Text.Trim();
                    _viewModel.SecondaryEntity.Amount = decimal.Parse(txtAmount.Text.Trim());
                    _viewModel.SecondaryEntity.EmployeeId = (string)cbxEmployees.SelectedValue;
                    _viewModel.SecondaryEntity.StoreId = (string)cbxStores.SelectedValue;
                    _viewModel.SecondaryEntity.PartyId = (string)cbxParties.SelectedValue;
                    _viewModel.SecondaryEntity.Particulars = txtParticulars.Text.Trim();
                    _viewModel.SecondaryEntity.EntryStatus = _viewModel.isNew ? EntryStatus.Added : EntryStatus.Updated;
                    _viewModel.SecondaryEntity.IsReadOnly = false;
                    _viewModel.SecondaryEntity.MarkedDeleted = false;
                    _viewModel.SecondaryEntity.UserId = CurrentSession.UserName;
                }
                _viewModel.SecondaryEntity.VoucherNumber = _viewModel.isNew ?
                    VoucherStatic.GenerateVoucherNumber(_viewModel.SecondaryEntity.VoucherType, _viewModel.SecondaryEntity.OnDate, _viewModel.SecondaryEntity.StoreId) :
                    _viewModel.voucherNumber;
            }
            else
            {
                if (_viewModel.PrimaryEntity == null)
                {
                    _viewModel.PrimaryEntity = new Voucher
                    {
                        VoucherType = _viewModel.voucherType,
                        OnDate = dtpOnDate.Value,
                        Remarks = txtRemarks.Text,
                        SlipNumber = txtSlipNo.Text,
                        PartyName = txtPartyName.Text,
                        Amount = decimal.Parse(txtAmount.Text.Trim()),
                        EmployeeId = (string)cbxEmployees.SelectedValue,
                        StoreId = (string)cbxStores.SelectedValue,
                        PartyId = (string)cbxParties.SelectedValue,
                        PaymentDetails = txtPaymentDetails.Text,
                        AccountId = (string)cbxBankAccount.SelectedValue,
                        PaymentMode = (PaymentMode)cbxPaymentMode.SelectedIndex,
                        Particulars = txtParticulars.Text.Trim(),
                        EntryStatus = _viewModel.isNew ? EntryStatus.Added : EntryStatus.Updated,
                        IsReadOnly = false,
                        MarkedDeleted = false,
                        UserId = CurrentSession.UserName
                    };
                    _viewModel.PrimaryEntity.AccountId = _viewModel.PrimaryEntity.PaymentMode != PaymentMode.Cash ? (string)cbxBankAccount.SelectedValue : "";
                }
                else
                {
                    _viewModel.PrimaryEntity.VoucherType = _viewModel.voucherType;
                    _viewModel.PrimaryEntity.OnDate = dtpOnDate.Value;
                    _viewModel.PrimaryEntity.Remarks = txtRemarks.Text;
                    _viewModel.PrimaryEntity.SlipNumber = txtSlipNo.Text;
                    _viewModel.PrimaryEntity.PartyName = txtPartyName.Text;
                    _viewModel.PrimaryEntity.Amount = decimal.Parse(txtAmount.Text.Trim());
                    _viewModel.PrimaryEntity.EmployeeId = (string)cbxEmployees.SelectedValue;
                    _viewModel.PrimaryEntity.StoreId = (string)cbxStores.SelectedValue;
                    _viewModel.PrimaryEntity.PartyId = (string)cbxParties.SelectedValue;
                    _viewModel.PrimaryEntity.PaymentDetails = txtPaymentDetails.Text;
                    // _viewModel.PrimaryEntity.AccountId = (string)cbxBankAccount.SelectedValue;
                    _viewModel.PrimaryEntity.PaymentMode = (PaymentMode)cbxPaymentMode.SelectedIndex;
                    _viewModel.PrimaryEntity.Particulars = txtParticulars.Text.Trim();
                    _viewModel.PrimaryEntity.EntryStatus = _viewModel.isNew ? EntryStatus.Added : EntryStatus.Updated;
                    _viewModel.PrimaryEntity.IsReadOnly = false;
                    _viewModel.PrimaryEntity.MarkedDeleted = false;
                    _viewModel.PrimaryEntity.UserId = CurrentSession.UserName;
                    _viewModel.PrimaryEntity.AccountId = _viewModel.PrimaryEntity.PaymentMode != PaymentMode.Cash ? (string)cbxBankAccount.SelectedValue : "";
                }
                //TODO: Move VoucherNumber Creation ViewModel/DataModel Side
                _viewModel.PrimaryEntity.VoucherNumber = _viewModel.isNew ? VoucherStatic.GenerateVoucherNumber(_viewModel.PrimaryEntity.VoucherType, _viewModel.PrimaryEntity.OnDate, _viewModel.PrimaryEntity.StoreId) : _viewModel.voucherNumber;
            }
            return true;
        }

        private bool SaveData()
        {
            if (ReadData())
            {
                if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
                {
                    return _viewModel.Save(ReadData());
                }
                else
                {
                    return _viewModel.Save(ReadData());
                }
            }
            else
            {
                return false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Voucher ??",
                                       "Confirm Delete!!",
                                       MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                bool flag = false;
                if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
                {
                    flag = _viewModel.Delete(_viewModel.SecondaryEntity);
                }
                else
                {
                    flag = _viewModel.Delete(_viewModel.PrimaryEntity);
                }

                if (flag)
                {
                    MessageBox.Show("Voucher is deleted!", "Delete");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show("Failed to delete, please try again!", "Delete");
            }
        }

        //Ported
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text == "Add")
                {
                    _viewModel.isNew = true;
                    ClearFields();
                    btnAdd.Text = "Save";
                }
                else if (btnAdd.Text == "Edit")
                {
                    _viewModel.isNew = false;
                    btnAdd.Text = "Save";
                }
                else if (btnAdd.Text == "Save")
                {
                    if (SaveData())
                    {
                        MessageBox.Show("Voucher is saved!!");
                        btnAdd.Text = "Add";
                        ClearFields();
                        if (_viewModel.isNew)
                            DialogResult = DialogResult.OK;
                        else DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        MessageBox.Show("Error occured while saving voucher");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public VoucherEntryForm()
        {
            InitializeComponent();
            _viewModel = new VoucherCashViewModel(VoucherType.Expense);
            _viewModel.isNew = true;
        }

        public VoucherEntryForm(VoucherType voucherType)
        {
            InitializeComponent();
            _viewModel.isNew = true;
            _viewModel = new VoucherCashViewModel(voucherType);
        }

        public VoucherEntryForm(VoucherType voucherType, Voucher voucher)
        {
            InitializeComponent();
            _viewModel = new VoucherCashViewModel(voucherType);
            _viewModel.Update(voucher);
            _viewModel.isNew = false;
            btnAdd.Text = "Edit";
            _viewModel.voucherNumber = voucher.VoucherNumber;
            panel1.Enabled = false;
        }

        public VoucherEntryForm(VoucherType voucherType, CashVoucher voucher)
        {
            InitializeComponent();
            _viewModel = new VoucherCashViewModel(voucherType);
            _viewModel.Update(voucher);
            _viewModel.isNew = false;
            btnAdd.Text = "Edit";
            _viewModel.voucherNumber = voucher.VoucherNumber;
            panel1.Enabled = false;
        }

        private void VoucherEntryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SetEntryType()
        {
            switch (_viewModel.voucherType)
            {
                case VoucherType.Payment:
                    rbPayment.Checked = true;
                    break;

                case VoucherType.Receipt:
                    rbReceipts.Checked = true;
                    break;

                case VoucherType.Expense:
                    rbExpenses.Checked = true;
                    break;

                case VoucherType.CashReceipt:
                    rbCashReceipts.Checked = true;
                    break;

                case VoucherType.CashPayment:
                    rbCashPayment.Checked = true;
                    break;

                default:
                    rbExpenses.Checked = false;
                    break;
            }
            ShowView(_viewModel.voucherType);
        }

        private void ShowView(VoucherType type)
        {
            if (type == VoucherType.CashPayment || type == VoucherType.CashReceipt)
            {
                lbBankAccount.Visible = false;
                lbMode.Visible = false;
                lbDetails.Visible = false;
                cbxBankAccount.Visible = false;
                cbxPaymentMode.Visible = false;
                txtPaymentDetails.Visible = false;

                lbTMode.Visible = true;
                cbxTranscationMode.Visible = true;
            }
            else
            {
                lbBankAccount.Visible = true;
                lbMode.Visible = true;
                lbDetails.Visible = true;
                cbxBankAccount.Visible = true;
                cbxPaymentMode.Visible = true;
                txtPaymentDetails.Visible = true;

                lbTMode.Visible = false;
                cbxTranscationMode.Visible = false;
            }
        }

        private void cbxPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPaymentMode.SelectedIndex > 0)
            {
                cbxBankAccount.Enabled = true;
                txtPaymentDetails.Enabled = true;
            }
            else
            {
                cbxBankAccount.Enabled = false;
                txtPaymentDetails.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbReceipts_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReceipts.Checked)
            {
                ShowView(VoucherType.Receipt);
            }
        }

        private void rbCashPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCashPayment.Checked)
            {
                ShowView(VoucherType.CashPayment);
            }
        }

        private void rbCashReceipts_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCashReceipts.Checked)
            {
                ShowView(VoucherType.CashReceipt);
            }
        }

        private void rbExpenses_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExpenses.Checked)
            {
                ShowView(VoucherType.Expense);
            }
        }

        private void rbPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPayment.Checked)
            {
                ShowView(VoucherType.Payment);
            }
        }
    }
}