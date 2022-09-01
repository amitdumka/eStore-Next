
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using System.Data;

namespace AKS.Accounting.Forms.Vouchers
{
    public partial class VoucherEntryForm : Form
    {
        public VoucherType voucherType;
        public string voucherNumber;
        public string deleteVoucherNumber;
        public CashVoucher SavedCashVoucher { get; set; }
        public Voucher SavedVoucher { get; set; }
        private CashVoucher cashVoucher;
        private Voucher voucher;

        public bool isNew = false;

        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        public VoucherEntryForm()
        {
            InitializeComponent();
            isNew = true;
            voucherType = VoucherType.Expense;
        }
        public VoucherEntryForm(VoucherType voucherType)
        {
            InitializeComponent();
            isNew = true;
            this.voucherType = voucherType;
        }
        public VoucherEntryForm(VoucherType voucherType, Voucher voucher)
        {
            InitializeComponent();
            this.voucherType = voucherType;
            this.voucher = voucher;
            isNew = false;
            btnAdd.Text = "Edit";
            voucherNumber = voucher.VoucherNumber;
            panel1.Enabled = false;
        }
        public VoucherEntryForm(VoucherType voucherType, CashVoucher voucher)
        {
            InitializeComponent();
            this.voucherType = voucherType;
            cashVoucher = voucher;
            isNew = false;
            btnAdd.Text = "Edit";
            voucherNumber = voucher.VoucherNumber;
            panel1.Enabled = false;
        }

        public string GenerateVoucherNumber(VoucherType type, DateTime onDate, string StoreCode)
        {

            string vNumber = "";
            int c = 0;


            switch (type)
            {
                case VoucherType.Payment:
                    c = azureDb.Vouchers.Where(c => c.StoreId == StoreCode && c.OnDate.Month == onDate.Month && c.OnDate.Year == onDate.Year).Count() + 1;

                    vNumber = $"{StoreCode}/PYM/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;
                case VoucherType.Receipt:
                    c = azureDb.Vouchers.Where(c => c.StoreId == StoreCode && c.OnDate.Month == onDate.Month && c.OnDate.Year == onDate.Year).Count() + 1;

                    vNumber = $"{StoreCode}/RCT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;
                case VoucherType.Contra:
                    break;
                case VoucherType.DebitNote:
                    break;
                case VoucherType.CreditNote:
                    break;
                case VoucherType.JV:
                    break;
                case VoucherType.Expense:
                    c = azureDb.Vouchers.Where(c => c.StoreId == StoreCode && c.OnDate.Month == onDate.Month && c.OnDate.Year == onDate.Year).Count() + 1;
                    vNumber = $"{StoreCode}/EXP/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;
                case VoucherType.CashReceipt:
                    c = azureDb.CashVouchers.Where(c => c.StoreId == StoreCode && c.OnDate.Month == onDate.Month && c.OnDate.Year == onDate.Year).Count() + 1;
                    vNumber = $"{StoreCode}/PCT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;
                case VoucherType.CashPayment:
                    c = azureDb.CashVouchers.Where(c => c.StoreId == StoreCode && c.OnDate.Month == onDate.Month && c.OnDate.Year == onDate.Year).Count() + 1;
                    vNumber = $"{StoreCode}/CPT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;
                default:
                    break;
            }

            return vNumber;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text == "Add")
                {
                    isNew = true;
                    ClearFields();
                    btnAdd.Text = "Save";
                }
                else if (btnAdd.Text == "Edit")
                {
                    isNew = false;
                    btnAdd.Text = "Save";
                }
                else if (btnAdd.Text == "Save")
                {
                    if (SaveData())
                    {

                        MessageBox.Show("Voucher is saved!!");
                        btnAdd.Text = "Add";
                        ClearFields();
                        if (isNew)
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
        private void ClearFields()
        {
            if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
                cashVoucher = new CashVoucher
                {
                    Amount = 0,
                    OnDate = DateTime.Now
                };
            else voucher = new Voucher
            {
                OnDate = DateTime.Now,
                Amount = 0,
                PaymentMode = PaymentMode.Cash
            };
            // DisplayData();
        }
        private void VoucherEntryForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();

            LoadData();
        }
        private void LoadData()
        {
            //Store
            cbxStores.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName, c.IsActive }).ToList();
            cbxStores.DisplayMember = "StoreName";
            cbxStores.ValueMember = "StoreId";

            cbxEmployees.DataSource = azureDb.Employees.Select(c => new { c.StoreId, c.EmployeeId, c.StaffName, c.IsWorking }).ToList();
            cbxEmployees.DisplayMember = "StaffName";
            cbxEmployees.ValueMember = "EmployeeId";


            cbxBankAccount.DataSource = azureDb.BankAccounts.Select(c => new { c.StoreId, c.AccountNumber, c.IsActive }).ToList();
            cbxBankAccount.DisplayMember = "AccountNumber";
            cbxBankAccount.ValueMember = "AccountNumber";

            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PaymentMode)));

            cbxParties.DataSource = azureDb.Parties.Select(c => new { c.StoreId, c.PartyId, c.PartyName }).ToList();
            cbxParties.DisplayMember = "PartyName";
            cbxParties.ValueMember = "PartyId";

            cbxTranscationMode.DataSource = azureDb.TranscationModes.Select(c => new { c.TranscationId, c.TranscationName }).ToList();
            cbxTranscationMode.DisplayMember = "TranscationName";
            cbxTranscationMode.ValueMember = "TranscationId";

            ShowView(voucherType);
            SetEntryType();
            if (!isNew) DisplayData();


        }
        private void DisplayData()
        {
            //TODO; All fields are not updated.

            if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
            {
                cbxStores.SelectedValue = cashVoucher.StoreId;
                voucherType = cashVoucher.VoucherType;
                dtpOnDate.Value = cashVoucher.OnDate;
                txtAmount.Text = cashVoucher.Amount.ToString();

                txtSlipNo.Text = cashVoucher.SlipNumber;
                txtPartyName.Text = cashVoucher.PartyName;
                txtRemarks.Text = cashVoucher.Remarks;
                cbxParties.SelectedValue = cashVoucher.PartyId;
                cbxEmployees.SelectedValue = cashVoucher.EmployeeId;
                cbxTranscationMode.SelectedValue = cashVoucher.TranscationId;
                txtParticulars.Text = cashVoucher.Particulars;
                this.Text = "Cash Voucher #\t" + cashVoucher.VoucherNumber;
            }
            else
            {
                cbxStores.SelectedValue = voucher.StoreId;
                voucherType = voucher.VoucherType;
                dtpOnDate.Value = voucher.OnDate;
                txtAmount.Text = voucher.Amount.ToString();
                txtSlipNo.Text = voucher.SlipNumber;
                txtPartyName.Text = voucher.PartyName;
                txtRemarks.Text = voucher.Remarks;
                cbxParties.SelectedValue = voucher.PartyId;
                cbxEmployees.SelectedValue = voucher.EmployeeId;

                cbxBankAccount.SelectedValue = voucher.AccountId;
                cbxPaymentMode.SelectedIndex = (int)voucher.PaymentMode;
                txtPaymentDetails.Text = voucher.PaymentDetails;
                txtParticulars.Text = voucher.Particulars;


            }
            SetEntryType();
        }
        private void SetEntryType()
        {
            switch (voucherType)
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
            ShowView(voucherType);
        }
        private bool ReadData()
        {
            //TODO: Validation of Data is need 
            if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
            {
                if (cashVoucher == null)
                {
                    cashVoucher = new CashVoucher
                    {
                        VoucherType = voucherType,
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
                        EntryStatus = isNew ? EntryStatus.Added : EntryStatus.Updated,
                        TranscationId = (string)cbxTranscationMode.SelectedValue,
                        Particulars = txtParticulars.Text.Trim(),


                    };





                }
                else
                {
                    cashVoucher.TranscationId = (string)cbxTranscationMode.SelectedValue;
                    cashVoucher.VoucherType = voucherType;
                    cashVoucher.OnDate = dtpOnDate.Value;
                    cashVoucher.Remarks = txtRemarks.Text.Trim();
                    cashVoucher.SlipNumber = txtSlipNo.Text.Trim();
                    cashVoucher.PartyName = txtPartyName.Text.Trim();
                    cashVoucher.Amount = decimal.Parse(txtAmount.Text.Trim());
                    cashVoucher.EmployeeId = (string)cbxEmployees.SelectedValue;
                    cashVoucher.StoreId = (string)cbxStores.SelectedValue;
                    cashVoucher.PartyId = (string)cbxParties.SelectedValue;
                    cashVoucher.Particulars = txtParticulars.Text.Trim();
                    cashVoucher.EntryStatus = isNew ? EntryStatus.Added : EntryStatus.Updated;
                    cashVoucher.IsReadOnly = false;
                    cashVoucher.MarkedDeleted = false;
                    cashVoucher.UserId = CurrentSession.UserName;
                }
                cashVoucher.VoucherNumber = isNew ?
                    GenerateVoucherNumber(cashVoucher.VoucherType, cashVoucher.OnDate, cashVoucher.StoreId) :
                    this.voucherNumber;


            }
            else
            {

                if (voucher == null)
                {
                    voucher = new Voucher
                    {
                        VoucherType = voucherType,
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
                        EntryStatus = isNew ? EntryStatus.Added : EntryStatus.Updated,
                        IsReadOnly = false,
                        MarkedDeleted = false,
                        UserId = CurrentSession.UserName
                    };
                    voucher.AccountId = voucher.PaymentMode != PaymentMode.Cash ? (string)cbxBankAccount.SelectedValue : "";
                }
                else
                {
                    voucher.VoucherType = voucherType;
                    voucher.OnDate = dtpOnDate.Value;
                    voucher.Remarks = txtRemarks.Text;
                    voucher.SlipNumber = txtSlipNo.Text;
                    voucher.PartyName = txtPartyName.Text;
                    voucher.Amount = decimal.Parse(txtAmount.Text.Trim());
                    voucher.EmployeeId = (string)cbxEmployees.SelectedValue;
                    voucher.StoreId = (string)cbxStores.SelectedValue;
                    voucher.PartyId = (string)cbxParties.SelectedValue;
                    voucher.PaymentDetails = txtPaymentDetails.Text;
                    // voucher.AccountId = (string)cbxBankAccount.SelectedValue;
                    voucher.PaymentMode = (PaymentMode)cbxPaymentMode.SelectedIndex;
                    voucher.Particulars = txtParticulars.Text.Trim();
                    voucher.EntryStatus = isNew ? EntryStatus.Added : EntryStatus.Updated;
                    voucher.IsReadOnly = false;
                    voucher.MarkedDeleted = false;
                    voucher.UserId = CurrentSession.UserName;
                    voucher.AccountId = voucher.PaymentMode != PaymentMode.Cash ? (string)cbxBankAccount.SelectedValue : "";


                }
                voucher.VoucherNumber = isNew ? GenerateVoucherNumber(voucher.VoucherType, voucher.OnDate, voucher.StoreId) : this.voucherNumber;


            }
            return true;
        }
        private bool SaveData()
        {
            if (ReadData())
            {
                if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
                {
                    if (isNew)
                        azureDb.CashVouchers.Add(cashVoucher);
                    else azureDb.CashVouchers.Update(cashVoucher);
                }
                else
                {
                    if (isNew)
                        azureDb.Vouchers.Add(voucher);
                    else azureDb.Vouchers.Update(voucher);
                }
                if (azureDb.SaveChanges() > 0)
                {
                    if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
                        SavedCashVoucher = cashVoucher;
                    else SavedVoucher = voucher;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Voucher ??",
                                       "Confirm Delete!!",
                                       MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
                {
                    azureDb.CashVouchers.Remove(cashVoucher);
                }
                else
                {
                    azureDb.Vouchers.Remove(voucher);
                }


                if (azureDb.SaveChanges() > 0)
                {
                    MessageBox.Show("Voucher is deleted!", "Delete");
                    this.deleteVoucherNumber = voucherNumber;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show("Failed to delete, please try again!", "Delete");


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
