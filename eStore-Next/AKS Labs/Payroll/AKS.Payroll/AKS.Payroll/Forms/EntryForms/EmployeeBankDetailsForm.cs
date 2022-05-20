using AKS.Payroll.Database;
using System.Data;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class EmployeeBankDetailsForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        bool added = false;
        public EmployeeBankDetailsForm()
        {
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                gbForm.Enabled = true;
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Edit")
            {
                gbForm.Enabled = true;
            }
            else if (btnAdd.Text == "Save")
            {

                if (ReadFormDataAndSave())
                {
                    gbForm.Enabled = false;
                    MessageBox.Show("Bank details of Employee is updated");
                    btnAdd.Text = "Add";
                    added = true;
                }
            }
        }

        private void LoadData()
        {
            cbEmployee.DataSource = azureDb.Employees.Select(c => new { c.EmployeeId, c.StaffName, c.StoreId }).OrderBy(c => c.EmployeeId).ToList();
            cbEmployee.ValueMember = "EmployeeId";
            cbEmployee.DisplayMember = "StaffName";

        }


        private bool ReadFormDataAndSave()
        {
            var empd = azureDb.EmployeeDetails.Where(c => c.EmployeeId == cbEmployee.SelectedValue).FirstOrDefault();

            if (empd != null)
            {
                empd.BankAccountNumber = txtAccountNumber.Text.Trim();
                empd.BankNameWithBranch = txtBranchName.Text.Trim();
                empd.IFSCode = txtIFSC.Text.Trim();

                if (empd.BankAccountNumber.Length > 0 && empd.BankNameWithBranch.Length > 0)
                {
                    azureDb.EmployeeDetails.Update(empd);
                    return azureDb.SaveChanges() > 0;
                }
                else
                {
                    MessageBox.Show("Bank account number and branch is must, Kindly enter and try again. ");
                    return false;
                }

            }
            return false;

        }

        private void EmployeeBankDetailsForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (added)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtAccountNumber.Text = "";
            txtBranchName.Text = "";
            txtIFSC.Text = "";
            btnAdd.Text = "Add";
            //gbForm.Enabled = false;

        }
    }
}
