using AKS.Payroll.Database;

namespace AKS.Payroll.Forms
{
    public partial class PayslipBankLetterForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        BankLetterDto bankLetterDto;
        public PayslipBankLetterForm()
        {
            InitializeComponent();
            bankLetterDto = new BankLetterDto();
        }

        private void LoadData()
        {
            var employeesList = azureDb.Employees.Where(c => c.IsWorking || c.Category == EmpType.Owner).ToList();

            
            cbxApprovedBy.DataSource = employeesList;
            cbxApprovedBy.DisplayMember = "StaffName";
            cbxApprovedBy.ValueMember = "EmployeId";

            cbxGeneratedBy.DataSource = employeesList;
            cbxGeneratedBy.DisplayMember = "StaffName";
            cbxGeneratedBy.ValueMember = "EmployeId";


            cbxStores.DataSource = azureDb.Stores.ToList();
            cbxStores.DisplayMember = "StoreName";
            cbxStores.ValueMember = "StoreId";



            cbxIssuedBanks.DataSource = azureDb.BankAccounts.Where(c => c.IsActive).ToList();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Save")
            {
            }
        }

        private BankLetterDto ReadFormData()
        {

            return new BankLetterDto
            {
                ApprovedBy = (string)cbxApprovedBy.SelectedValue,
                BankName = (string)cbxIssuedBanks.SelectedValue,
                BranchName = txtBankName.Text.Trim(),
                ChequeDate = dtpChequeDate.Value,
                ChequeNumber = txtChequeNumber.Text.Trim(),
                GenratedBy = (string)cbxGeneratedBy.SelectedValue,
                Month = (int)nudMonth.Value,
                Year = (int)nudYear.Value,
                OnDate = dtpOnDate.Value,
                StoreId = (string)cbxStores.SelectedValue,
                Status = txtStatus.Text.Trim(),
                OperationMode = (string)cbxOperationMode.SelectedValue

            };
        }
    }

    internal class BankLetterDto
    {
        public DateTime OnDate { get; set; }
        public string StoreId { get; set; }

        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNumber { get; set; }

        public DateTime ChequeDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string ApprovedBy { get; set; }
        public string Status { get; set; }
        public string OperationMode { get; set; }
        public string GenratedBy { get; set; }
    }
}