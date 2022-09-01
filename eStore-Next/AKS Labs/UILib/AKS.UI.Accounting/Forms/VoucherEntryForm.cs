using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Commons.ViewModels.Accounts;

namespace AKS.UI.Accounting.Forms
{
    public partial class VoucherEntryForm : Form
    {
        private VoucherCashViewModel _viewModel;

        public VoucherEntryForm(VoucherCashViewModel vcvm)
        {
            InitializeComponent();
            if (vcvm == null)
                _viewModel = new VoucherCashViewModel(VoucherType.Expense);
            else _viewModel = vcvm;
            _viewModel.isNew = true;
            this.Text = $"{CurrentSession.StoreName}: Expense Voucher : (New)";
        }

        public VoucherEntryForm(VoucherType voucherType, VoucherCashViewModel vcvm)
        {
            InitializeComponent();

            if (vcvm == null)
                _viewModel = new VoucherCashViewModel(voucherType);
            else _viewModel = vcvm;
            _viewModel.isNew = true;

            this.Text = $"{CurrentSession.StoreName}: " + voucherType.ToString().Replace("Cash","") + $"{(!voucherType.ToString().Contains("Cash")?"":"(Cash)")}  Voucher : (New)";

        }

        public VoucherEntryForm(VoucherType voucherType, Voucher voucher, VoucherCashViewModel vcvm)
        {
            InitializeComponent();
            if (vcvm == null)
                _viewModel = new VoucherCashViewModel(voucherType);
            else _viewModel = vcvm;
            _viewModel.voucherType = voucherType;
            _viewModel.Update(voucher);
            _viewModel.isNew = false;
            btnAdd.Text = "Edit";
            _viewModel.voucherNumber = voucher.VoucherNumber;
            panel1.Enabled = false;
            this.Text = $"{CurrentSession.StoreName}: " + voucherType.ToString() + $" Voucher :{voucher.VoucherNumber}";

        }

        public VoucherEntryForm(VoucherType voucherType, CashVoucher voucher, VoucherCashViewModel vcvm)
        {
            InitializeComponent();
            if (vcvm == null)
                _viewModel = new VoucherCashViewModel(voucherType);
            else _viewModel = vcvm;
            _viewModel.voucherType = voucherType;
            _viewModel.Update(voucher);
            _viewModel.isNew = false;
            btnAdd.Text = "Edit";
            _viewModel.voucherNumber = voucher.VoucherNumber;
            panel1.Enabled = false;
            this.Text=$"{CurrentSession.StoreName}: "+voucherType.ToString().Replace("Cash","")+$" Voucher (Cash):{voucher.VoucherNumber}";
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
                        MessageBox.Show("Error occurred while saving voucher");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                //this.Text = "Cash Voucher #\t" + _viewModel.SecondaryEntity.VoucherNumber;
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

        private void LoadData()
        {
            //Store

            cbxStores.DataSource = _viewModel.GetStoreList();
            cbxStores.DisplayMember = "DisplayData";// "StoreName";
            cbxStores.ValueMember = "StoreId";

            cbxEmployees.DataSource = _viewModel.GetEmployeeList();
            cbxEmployees.DisplayMember = "DisplayData";
            cbxEmployees.ValueMember = "ValueData";

            cbxBankAccount.DataSource = _viewModel.GetBankAccountList();
            cbxBankAccount.DisplayMember = "ValueData";
            cbxBankAccount.ValueMember = "ValueData";

            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PaymentMode)));

            cbxParties.DataSource = _viewModel.GetPartyList();
            cbxParties.DisplayMember = "DisplayData";
            cbxParties.ValueMember = "ValueData";

            cbxTranscationMode.DataSource = _viewModel.GetTranscationList();
            cbxTranscationMode.DisplayMember = "DisplayData";
            cbxTranscationMode.ValueMember = "ValueData";

            ShowView(_viewModel.voucherType);
            SetEntryType();
            if (!_viewModel.isNew) DisplayData();
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

        private void rbReceipts_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReceipts.Checked)
            {
                ShowView(VoucherType.Receipt);
            }
        }

        private bool ReadData()
        {
            if (_viewModel.voucherType != ReadVoucher())
            {
                _viewModel.voucherType = ReadVoucher();
            }

            //TODO: Validation of Data is need
            if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
            {
                _viewModel.SecondaryEntity = new CashVoucher
                {
                    VoucherType = _viewModel.voucherType,
                    OnDate = dtpOnDate.Value,
                    Remarks = txtRemarks.Text,
                    SlipNumber = txtSlipNo.Text,
                    PartyName = txtPartyName.Text.Trim(),
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
                _viewModel.SecondaryEntity.VoucherNumber = _viewModel.isNew ?
                    "" :
                    _viewModel.voucherNumber;
            }
            else
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
                    // AccountId = (string)cbxBankAccount.SelectedValue,
                    AccountId = _viewModel.PrimaryEntity.PaymentMode != PaymentMode.Cash ? (string)cbxBankAccount.SelectedValue : "",
                    PaymentMode = (PaymentMode)cbxPaymentMode.SelectedIndex,
                    Particulars = txtParticulars.Text.Trim(),
                    EntryStatus = _viewModel.isNew ? EntryStatus.Added : EntryStatus.Updated,
                    IsReadOnly = false,
                    MarkedDeleted = false,
                    UserId = CurrentSession.UserName
                };
                _viewModel.PrimaryEntity.AccountId = _viewModel.PrimaryEntity.PaymentMode != PaymentMode.Cash ? (string)cbxBankAccount.SelectedValue : "";

                //TODO: Move VoucherNumber Creation ViewModel/DataModel Side
                _viewModel.PrimaryEntity.VoucherNumber = _viewModel.isNew ? "" : _viewModel.voucherNumber;
            }
            return true;
        }

        private VoucherType ReadVoucher()
        {
            if (rbCashPayment.Checked) return VoucherType.CashPayment;
            if (rbCashReceipts.Checked) return VoucherType.CashReceipt;

            if (rbPayment.Checked) return VoucherType.Payment;
            if (rbReceipts.Checked) return VoucherType.Receipt;
            if (rbExpenses.Checked) return VoucherType.Expense;
            return VoucherType.Expense;
        }

        private bool SaveData()
        {
            if (ReadData())
            {
                if (_viewModel.voucherType == VoucherType.CashPayment || _viewModel.voucherType == VoucherType.CashReceipt)
                {
                    return _viewModel.SaveCashVoucher(ReadData());
                }
                else
                {
                    return _viewModel.SaveVoucher(ReadData());
                }
            }
            else
            {
                return false;
            }
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
            //Changing Voucher Type so at time of Save it dont create problem
            _viewModel.voucherType = type;
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

        private void VoucherEntryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}