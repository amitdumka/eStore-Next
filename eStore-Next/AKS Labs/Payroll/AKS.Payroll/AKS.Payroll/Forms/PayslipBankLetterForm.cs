namespace AKS.Payroll.Forms
{
    public partial class PayslipBankLetterForm : Form
    {
        public PayslipBankLetterForm()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
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

        private void ReadFormData()
        {
            var dto = new BankLetterDto
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