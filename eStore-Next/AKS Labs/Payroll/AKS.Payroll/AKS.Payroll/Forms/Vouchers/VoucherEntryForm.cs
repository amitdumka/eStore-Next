using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Accounts;
using System.Data;

namespace AKS.Payroll.Forms.Vouchers
{
    public partial class VoucherEntryForm : Form
    {
        public VoucherType voucherType;
        public CashVoucher cashVoucher;
        public Voucher voucher;
        private bool isNew = false;
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
        }
        public VoucherEntryForm(VoucherType voucherType, CashVoucher voucher)
        {
            InitializeComponent();
            this.voucherType = voucherType;
            cashVoucher = voucher;
            isNew = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
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
            DisplayData();
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

            cbxBankAccounts.DataSource = azureDb.BankAccounts.Select(c => new { c.StoreId, c.AccountNumber, c.IsActive }).ToList();
            cbxBankAccounts.DisplayMember = "AccountNumber";
            cbxBankAccounts.ValueMember = "AccountNumber";

            //cbxParties.DataSource = azureDb.BankAccounts.Select(c => new { c.StoreId, c.AccountNumber, c.IsActive }).ToList();
            //cbxParties.DisplayMember = "AccountNumber";
            //cbxParties.ValueMember = "AccountNumber";
            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PaymentMode)));

            if (!isNew) DisplayData();

        }
        private void DisplayData()
        {

            if (voucherType == VoucherType.CashPayment || voucherType == VoucherType.CashReceipt)
            {
                //cbxStores.SelectedValue = cashVoucher.StoreId;
                voucherType = cashVoucher.VoucherType;
                dtpOnDate.Value = cashVoucher.OnDate;
                txtAmount.Text = cashVoucher.Amount.ToString();
                txtSlipNo.Text = cashVoucher.SlipNumber;
                txtPartyName.Text = cashVoucher.PartyName;
                txtRemarks.Text = cashVoucher.Remarks;
                cbxParties.SelectedValue = cashVoucher.PartyId;
                cbxEmployees.SelectedValue = cashVoucher.EmployeeId;
            }
            else
            {
                //cbxStores.SelectedValue = voucher.StoreId;
                voucherType = voucher.VoucherType;
                dtpOnDate.Value = voucher.OnDate;
                txtAmount.Text = voucher.Amount.ToString();
                txtSlipNo.Text = voucher.SlipNumber;
                txtPartyName.Text = voucher.PartyName;
                txtRemarks.Text = voucher.Remarks;
                cbxParties.SelectedValue = voucher.PartyId;
                cbxEmployees.SelectedValue = voucher.EmployeeId;

                cbxPaymentMode.SelectedIndex = (int)voucher.PaymentMode;
                cbxBankAccounts.SelectedValue = voucher.AccountId;
                txtPaymentDetails.Text = voucher.PaymentDetails;


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
                case VoucherType.Contra:
                    break;
                case VoucherType.DebitNote:
                    break;
                case VoucherType.CreditNote:
                    break;
                case VoucherType.JV:
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
                if (cashVoucher == null) cashVoucher = new CashVoucher();

                cashVoucher.VoucherType = voucherType;
                cashVoucher.OnDate = dtpOnDate.Value;
                cashVoucher.Remarks = txtRemarks.Text;
                cashVoucher.SlipNumber = txtSlipNo.Text;
                cashVoucher.PartyName = txtPartyName.Text;
                cashVoucher.Amount = decimal.Parse(txtAmount.Text.Trim());
                cashVoucher.EmployeeId = (string)cbxEmployees.SelectedValue;
                cashVoucher.StoreId = (string)cbxStores.SelectedValue;
                cashVoucher.PartyId = (string)cbxParties.SelectedValue;

                if (isNew) cashVoucher.EntryStatus = EntryStatus.Added;
                else cashVoucher.EntryStatus = EntryStatus.Updated;

                cashVoucher.IsReadOnly = false;
                cashVoucher.MarkedDeleted = false;
                cashVoucher.UserId = "WinUI";
            }
            else
            {

                if (voucher == null) voucher = new Voucher();

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
                voucher.AccountId = (string)cbxBankAccounts.SelectedValue;
                voucher.PaymentMode = (PaymentMode)cbxPaymentMode.SelectedValue;
                if (isNew) voucher.EntryStatus = EntryStatus.Added;
                else voucher.EntryStatus = EntryStatus.Updated;
                voucher.IsReadOnly = false;
                voucher.MarkedDeleted = false;
                voucher.UserId = "WinUI";

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
                return azureDb.SaveChanges() > 0;
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
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 0F);
            }
            else
            {
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.AutoSize);
            }
        }
    }
}
