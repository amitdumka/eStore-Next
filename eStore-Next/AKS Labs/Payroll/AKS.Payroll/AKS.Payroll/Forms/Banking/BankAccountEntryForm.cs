using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Banking;

namespace AKS.Payroll.Forms.Banking
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?redirectedfrom=MSDN&view=net-6.0
    // Backgroud worker doc
    public partial class BankAccountEntryForm : Form
    {
        private readonly int _FormMode;
        private ObservableListSource<BankAccountList> AccountLists;
        private AzurePayrollDbContext azureDb;
        private ObservableListSource<BankAccount> BankAccounts;
        private LocalPayrollDbContext localDb;
        private ObservableListSource<VendorBankAccount> VendorBankAccounts;
        private BankAccount _bankAccount;
        private VendorBankAccount _vendorBankAccount;
        private BankAccountList _bankAccountList;

        public BankAccountEntryForm(int mode)
        {
            InitializeComponent();
            _FormMode = mode;
        }
        public BankAccountEntryForm(int mode, BankAccount account)
        {
            InitializeComponent();
            _FormMode = mode;
        }
        public BankAccountEntryForm(int mode, VendorBankAccount account)
        {
            InitializeComponent();
            _FormMode = mode;
        }
        public BankAccountEntryForm(int mode, BankAccountList account)
        {
            InitializeComponent();
            _FormMode = mode;
        }

        public BankAccountEntryForm()
        {
            InitializeComponent();
        }

        private void BankAccountEntryForm_Load(object sender, EventArgs e)
        {
            EnableFormMode();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Edit") { btnAdd.Text = "Save"; }
            else if (btnAdd.Text == "Save")
            {
                if (rbCompanyAccount.Checked)
                {
                    azureDb.BankAccounts.Add(ReadCompanyAccount());

                }
                else if (rbThirdPartyAccount.Checked)
                {
                    azureDb.AccountLists.Add(ReadThirdPartyAccount());
                }
                else if (rbVendorAccount.Checked)
                {
                    azureDb.VendorBankAccounts.Add(ReadVendorAccount());
                }
                else
                {
                    MessageBox.Show("Kindly Select which type of Bank account need to be saved!");
                    return;
                }
                if (azureDb.SaveChanges() > 0)
                {
                    MessageBox.Show("Account information is saved succesfully!!");
                    btnAdd.Text = "Add"; 
                    this.DialogResult = DialogResult.OK;


                }
                else
                {
                    MessageBox.Show("It failed to save the account information, kindly check values and try again!!");
                }

            }
        }

        private void EnableFormMode()
        {
            switch (_FormMode)
            {
                case 1: // Bank Account Mode;
                    rbCompanyAccount.Checked = true;
                    break;

                case 2: //Account List mode
                    rbThirdPartyAccount.Checked = true;
                    break;

                case 3: //Vendor Mode
                    rbVendorAccount.Checked = true;
                    break;

                default:
                    rbCompanyAccount.Checked = false;
                    rbVendorAccount.Checked = false;
                    rbThirdPartyAccount.Checked = false;
                    break;
            }
        }

        private void FormElementView()
        {
            switch (_FormMode)
            {
                case 1:
                case 2:
                case 3:
                    break;

                default:
                    break;
            }
        }

        private BankAccount ReadCompanyAccount()
        {
            BankAccount account = new BankAccount
            {
                AccountHolderName = txtName.Text.Trim(),
                AccountNumber = txtAccountNumber.Text.Trim(),
                AccountType = (AccountType)cbxAccountType.SelectedValue,
                BankId = (string)cbxBanks.SelectedValue,
                BranchName = txtBranch.Text.Trim(),
                IsActive = cbIsActive.Checked,
                IFSCCode = txtIFSCCode.Text.Trim(),
                SharedAccount = cbSharedAccount.Checked,
                MarkedDeleted = false,
                OpenningDate = dtpOpeningDate.Value,
                StoreId = (string)cbxStores.SelectedValue,
                DefaultBank = cbDefaultAccount.Checked,
                OpenningBalance = Decimal.Parse(txtOpeningBalance.Text.Trim()),
                CurrentBalance = Decimal.Parse(txtCurrentBalance.Text.Trim()),
                ClosingDate = dtpClosingDate.Value,
            };
            if (account.IsActive) account.ClosingDate = null;
            return account;
        }

        private BankAccountList ReadThirdPartyAccount()
        {
            BankAccountList account = new BankAccountList
            {
                AccountHolderName = txtName.Text.Trim(),
                AccountNumber = txtAccountNumber.Text.Trim(),
                AccountType = (AccountType)cbxAccountType.SelectedValue,
                BankId = (string)cbxBanks.SelectedValue,
                BranchName = txtBranch.Text.Trim(),
                IsActive = cbIsActive.Checked,
                IFSCCode = txtIFSCCode.Text.Trim(),
                SharedAccount = cbSharedAccount.Checked,
                MarkedDeleted = false,
                StoreId = (string)cbxStores.SelectedValue
            };
            return account;
        }

        private VendorBankAccount ReadVendorAccount()
        {
            VendorBankAccount account = new VendorBankAccount
            {
                AccountHolderName = txtName.Text.Trim(),
                AccountNumber = txtAccountNumber.Text.Trim(),
                AccountType = (AccountType)cbxAccountType.SelectedValue,
                BankId = (string)cbxBanks.SelectedValue,
                BranchName = txtBranch.Text.Trim(),
                IsActive = cbIsActive.Checked,
                IFSCCode = txtIFSCCode.Text.Trim(),
                VendorId = (string)cbxVendors.SelectedValue,
                MarkedDeleted = false,
                OpenningDate = dtpOpeningDate.Value,
                StoreId = (string)cbxStores.SelectedValue,

                OpenningBalance = Decimal.Parse(txtOpeningBalance.Text.Trim()),

                ClosingDate = dtpClosingDate.Value,
            };
            if (account.IsActive) account.ClosingDate = null;
            return account;
        }
    
        private void FillFormValue()
        {
            if (rbCompanyAccount.Checked) { }
            else if (rbVendorAccount.Checked) { }
            else if(rbThirdPartyAccount.Checked) { }

        }
    
    
    }
}