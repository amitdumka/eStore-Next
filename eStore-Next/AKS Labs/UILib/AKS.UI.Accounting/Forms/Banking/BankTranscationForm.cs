using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.UI.Accounting.Forms
{
    public partial class BankTranscationForm : Form
    {
        private BankTranscationViewModel _viewModel;
        private BankTranscation transcation;
        public BankTranscationForm()
        {
            InitializeComponent();
        }

        private void BankTranscationForm_Load(object sender, EventArgs e)
        {
            _viewModel = new BankTranscationViewModel();
            LoadData();
        }
        private void LoadData()
        {
            cbxMode.DataSource = _viewModel.GetModeList();
            lbAccountList.DataSource = _viewModel.GetAccountList();
            cbxAccountNumber.DataSource = _viewModel.GetAccountList();
            dgvBankTranscation.DataSource = _viewModel.GetTranscations(DateTime.Today, true, false);

            dgvBankTranscation.Columns["BankAccount"].Visible = false;
            dgvBankTranscation.Columns["IsReadOnly"].Visible = false;
            dgvBankTranscation.Columns["EntryStatus"].Visible = false;
            dgvBankTranscation.Columns["UserId"].Visible = false;
            dgvBankTranscation.Columns["MarkedDeleted"].Visible = false;
            // dgvBankTranscation.Columns.Add("Store.StoreName", "Store Name");


        }
        private BankTranscation ReadData()
        {
            BankTranscation bt = new BankTranscation
            {
                AccountNumber = cbxAccountNumber.Text.ToString(),
                Amount = (decimal)nudAmount.Value,
                Balance = 0,
                EntryStatus = EntryStatus.Added,
                IsReadOnly = false,
                Naration = cbxMode.Text.Trim() + " # " + txtNaration.Text.Trim(),
                RefNumber = txtChequeNo.Text.Trim(),
                OnDate = dtpOnDate.Value,
                MarkedDeleted = false,
                StoreId = CurrentSession.StoreCode,
                UserId = CurrentSession.UserName,
                Verified = false
            };



            if (rbDeposit.Checked) bt.DebitCredit = DebitCredit.In;
            else if (rbWithdrawal.Checked)
            {
                bt.DebitCredit = DebitCredit.Out;
                if (nudAmount.Value < 0)

                    bt.Amount = nudAmount.Value;

                else
                    bt.Amount = 0 - bt.Amount;
            }
            else bt.DebitCredit = DebitCredit.In;
            return bt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save"; ResetForm();
                tabControl1.SelectedTab = tpEntry;
                _viewModel.SetEditMode(false);
            }
            else if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
                _viewModel.SetEditMode(true);
            }
            else if (btnAdd.Text == "Save")
            {
                if (validate())
                {
                    if (_viewModel.Save(ReadData(), _viewModel.GetEditMode()))
                    {
                        MessageBox.Show("Bank Transcation is saved!");
                        btnAdd.Text = "Add";
                        ResetForm();
                        RefreshData();
                    }
                }
            }
        }
        private void RefreshData()
        {
            dgvBankTranscation.DataSource = null;
            dgvBankTranscation.DataSource = _viewModel.GetTranscation();
            //dgvBankTranscation.Refresh();
        }
        private void ResetForm()
        {
            tabControl1.SelectedTab = tpGrid;
            txtNaration.Text = txtChequeNo.Text = "";
            cbxMode.SelectedIndex = 0;
            nudAmount.Value = 0;
            rbDeposit.Checked = rbWithdrawal.Checked = false;

        }
        private bool validate()
        {
            if (rbDeposit.Checked == false && rbWithdrawal.Checked == false)
            {
                MessageBox.Show("Select Deposit or Withdrawal");
                return false;
            }
            return true;
        }

        private void dgvBankTranscation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBankTranscation.CurrentCell.Selected)
            {
                var row = (BankTranscation)dgvBankTranscation.CurrentRow.DataBoundItem;
                if (row != null)
                {
                    _viewModel.SetEnableDelete(true);
                    DisplayData(row);
                    btnAdd.Text = "Edit";
                    transcation = row;


                }
            }
        }

        private void DisplayData(BankTranscation bt)
        {
            try
            {

                tabControl1.SelectedTab = tpEntry;
                cbxAccountNumber.SelectedText = cbxAccountNumber.Text = bt.AccountNumber;
                var nar = bt.Naration.Split("#");
                if (nar != null && nar.Count() > 1)
                {
                    txtNaration.Text = nar != null ? nar[1] : bt.Naration;
                    cbxMode.SelectedText = cbxMode.Text = nar != null ? nar[0] : " ";
                }
                else
                {
                    txtNaration.Text = bt.Naration;
                    cbxMode.SelectedText = cbxMode.Text = " ";
                }

                txtChequeNo.Text = bt.RefNumber;

                nudAmount.Value = Math.Abs(bt.Amount);

                if (bt.DebitCredit == DebitCredit.In)
                    rbDeposit.Checked = true;
                else if (bt.DebitCredit == DebitCredit.Out)
                    rbWithdrawal.Checked = true;
            }
            catch (Exception e)
            {

                MessageBox.Show("Error\n" + e.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Enable role based operation
            if (_viewModel.GetEnableDelete())
            {
                if (transcation != null)
                {
                    if (_viewModel.Delete(transcation))
                    {
                        RefreshData();
                        MessageBox.Show("Deleted");

                        tabControl1.SelectedTab = tpGrid;
                        ResetForm();


                    }
                }
            }
        }
    }
    public class BankTranscationVM : BankTranscation
    {
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string StoreName { get; set; }
    }
    public class BankTranscationViewModel
    {
        AzurePayrollDbContext azuredb;
        LocalPayrollDbContext localdb;
        List<BankTranscation> transcations;
        List<BankTranscationVM> vmList;
        BankTranscation Transcation { get; set; }
        bool enableEditDelete = false;
        public bool GetEnableDelete() { return enableEditDelete; }
        public void SetEnableDelete(bool enable) { enableEditDelete = enable; }

        string AccountNumber = "";
        bool Yearly = false, Monthly = false, isNew = false;
        List<string> ModeList = new List<string>
        {
            "Cheque","ATM","Over Counter","NEFT/RTGS/IMPS","UPI","Others"
        };
        public List<string> GetModeList() { return ModeList; }
        public List<string> accountList;
        public void SetEditMode(bool mode = true)
        {
            isNew = !mode;
        }
        public bool GetEditMode() { return isNew; }
        public BankTranscationViewModel()
        {
            azuredb = new AzurePayrollDbContext();
            localdb = new LocalPayrollDbContext();
            transcations = new List<BankTranscation>();
        }
        public List<string> GetAccountList(bool active = true)
        {
            if (accountList != null) return accountList;
            if (active)
                return azuredb.BankAccounts.Where(c => c.IsActive).Select(c => c.AccountNumber).ToList();
            else return azuredb.BankAccounts.Select(c => c.AccountNumber).ToList();
        }
        public List<BankTranscation> GetTranscations(string AccountNumber, DateTime ondate, bool monthly = true, bool yearly = false)
        {
            this.AccountNumber = AccountNumber;
            Yearly = yearly; Monthly = monthly;
            if (yearly)
                transcations = azuredb.BankTranscations.Include(c => c.BankAccount).Where(c => c.AccountNumber == AccountNumber && c.OnDate.Year == ondate.Year).ToList();
            else if (monthly)
                transcations = azuredb.BankTranscations.Include(c => c.BankAccount).Where(c => c.AccountNumber == AccountNumber && c.OnDate.Month == ondate.Month && c.OnDate.Year == ondate.Year).ToList();
            else transcations = azuredb.BankTranscations.Include(c => c.BankAccount).Where(c => c.AccountNumber == AccountNumber && c.OnDate == ondate).ToList();

            return transcations;
        }
        public List<BankTranscation> GetTranscations(DateTime ondate, bool monthly = true, bool yearly = false)
        {
            this.AccountNumber = "";
            Yearly = yearly; Monthly = monthly;
            if (yearly)
                transcations = azuredb.BankTranscations.Include(c => c.BankAccount).Include(c => c.Store).Where(c => c.OnDate.Year == ondate.Year).ToList();
            else if (monthly)
                transcations = azuredb.BankTranscations.Include(c => c.BankAccount).Include(c => c.Store).Where(c => c.OnDate.Month == ondate.Month && c.OnDate.Year == ondate.Year).ToList();
            else transcations = azuredb.BankTranscations.Include(c => c.BankAccount).Include(c => c.Store).Where(c => c.OnDate == ondate).ToList();
            return transcations;
        }
        public List<BankTranscation> GetTranscation() { return transcations; }
        public bool Save(BankTranscation bt, bool isnew = true)
        {
            this.isNew = isnew;
            Transcation = bt;
            if (isNew)
                azuredb.BankTranscations.Add(Transcation);
            else
                azuredb.BankTranscations.Update(Transcation);
            var flag = azuredb.SaveChanges() > 0;
            if (flag)
            {
                if (transcations == null)
                    transcations = new List<BankTranscation>();
                transcations.Add(Transcation);
            }
            return flag;
        }
        public bool Delete(BankTranscation bt)
        {
            azuredb.BankTranscations.Remove(bt);
            bool x = azuredb.SaveChanges() > 0;
            transcations.Remove(bt);
            return x;
        }



    }
}
