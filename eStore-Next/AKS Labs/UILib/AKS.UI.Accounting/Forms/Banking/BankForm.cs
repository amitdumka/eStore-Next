
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Banking;
using Microsoft.EntityFrameworkCore;

namespace AKS.UI.Accounting.Forms.Banking
{
    public partial class BankForm : Form
    {

        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private ObservableListSource<Bank> Banks;
        public BankForm()
        {
            InitializeComponent();
            Banks = new ObservableListSource<Bank>();

        }

        private string GenerateBankId(string name)
        {
            string bankId = "";
            var letters = name.Split(' ');
            foreach (var letter in letters)
            {
                bankId += letter[0];
            }
            return bankId;
        }

        private void btnAddBank_Click(object sender, EventArgs e)
        {
            if (btnAddBank.Text == "Add")
            {
                btnAddBank.Text = "Save";
            }
            else
            if (btnAddBank.Text == "Edit")
            {
                btnAddBank.Text = "Save";
            }
            else
            if (btnAddBank.Text == "Save")
            {
                if (txtBankName.Text.Length > 0)
                {
                    Bank nBank = new Bank
                    {
                        Name = txtBankName.Text.Trim(),
                        BankId = GenerateBankId(txtBankName.Text.Trim()),
                    };
                    nBank.BankId += "00" + (azureDb.Banks.Count() + 1).ToString();
                    azureDb.Banks.Add(nBank);
                    if (azureDb.SaveChanges() > 0)
                    {
                        MessageBox.Show("New Bank is saved!");
                        btnAddBank.Text = "Add";
                        txtBankName.Text = "";
                        Banks.Add(nBank);
                        lbBankList.Refresh();

                    }
                    else MessageBox.Show("Some error occured while saving new bank, Kindly try again");
                }
                else
                {
                    MessageBox.Show("Bank name is required to save it, Kindly enter bank name");
                    txtBankName.Focus();
                }

            }
        }

        private void LoadData()
        {
            UpdateBankList(azureDb.Banks.ToList());
            lbBankList.DataSource = Banks.ToBindingList();
            lbBankList.ValueMember = "BankId";
            lbBankList.DisplayMember = "Name";
        }

        private void UpdateBankList(List<Bank> banks)
        {
            foreach (Bank bank in banks)
                Banks.Add(bank);
        }

        private void BankForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            LoadData();
        }

        private void btnAddBankAccount_Click(object sender, EventArgs e)
        {
            BankAccountEntryForm form = new BankAccountEntryForm();
            if (form.ShowDialog() == DialogResult.OK)
            {

            }
            else if (DialogResult == DialogResult.Yes)
            {

            }
            else
            {

            }

        }

        private void btnAddThirdPartyAccounts_Click(object sender, EventArgs e)
        {
            BankAccountEntryForm form = new BankAccountEntryForm(2);

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
            else if (DialogResult == DialogResult.Yes)
            {

            }
            else
            {

            }
        }

        private void btnAddVendorAccounts_Click(object sender, EventArgs e)
        {
            BankAccountEntryForm form = new BankAccountEntryForm(3);

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
            else if (DialogResult == DialogResult.Yes)
            {

            }
            else
            {

            }
        }
    }
}