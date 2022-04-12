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
            var employeesList=azureDb.Employees.ToList();
            cbxApprovedBy.DataSource=employeesList;
            cbxGeneratedBy.DataSource=employeesList;
            cbxStores.DataSource=azureDb.Stores.ToList();
            
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
                ApprovedBy = cbxApprovedBy.Text,
                BankName = cbxBanks.Text,
                BranchName = txtBankName.Text,
                CAccountNumber = cbxChequeBankName.Text,
                CDateTime = dtpChequeDate.Value,
                CheckName = cbxChequeNumber.Text,
                GenratedBy = cbxGeneratedBy.Text,
                Month = (int)nudMonth.Value,
                Year = (int)nudYear.Value,
                OnDate = dtpOnDate.Value,
                StoreId = (string)cbxStores.SelectedValue,
                Status = txtStatus.Text,
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
        public string CheckName { get; set; }
        public string CAccountNumber { get; set; }
        public DateTime CDateTime { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string ApprovedBy { get; set; }
        public string Status { get; set; }
        public string OperationMode { get; set; }
        public string GenratedBy { get; set; }
    }
}