using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Accounting.Forms
{
    public partial class BankTranscationForm : Form
    {
        private BankTranscationViewModel _viewModel;
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
                Naration = txtNaration.Text.Trim(),
                RefNumber = txtChequeNo.Text.Trim(),
                OnDate = dtpOnDate.Value,
                MarkedDeleted = false,
                StoreId = CurrentSession.StoreCode,
                UserId = CurrentSession.UserName,
                Verified = false
            };
            if (rbDeposit.Checked) bt.DebitCredit = DebitCredit.In;
            else if (rbDeposit.Checked) bt.DebitCredit = DebitCredit.Out;
            else bt.DebitCredit = DebitCredit.In;
            return bt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save"; ResetForm();
            }
            else if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Edit";
            }
            else if (btnAdd.Text == "Save")
            {
                if (validate())
                {
                    if (_viewModel.Save(ReadData()))
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
            dgvBankTranscation.DataSource = _viewModel.GetTranscation();
            dgvBankTranscation.Refresh();
        }
        private void ResetForm() { }
        private bool validate()
        {
            if (rbDeposit.Checked == false && rbWithdrawal.Checked == false)
            {
                MessageBox.Show("Select Deposit or Withdrawal");
                return false;
            }
            return true;
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
        string AccountNumber = "";
        bool Yearly = false, Monthly = false;
        List<string> ModeList = new List<string>
        {
            "Cheque","ATM","Over Counter","NEFT/RTGS/IMPS","UPI","Others"
        };
        public List<string> GetModeList() { return ModeList; }
        public List<string> accountList;

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
                return azuredb.BankTranscations.Include(c => c.BankAccount).Where(c => c.OnDate.Year == ondate.Year).ToList();
            if (monthly)
                return azuredb.BankTranscations.Include(c => c.BankAccount).Where(c => c.OnDate.Month == ondate.Month && c.OnDate.Year == ondate.Year).ToList();
            return azuredb.BankTranscations.Include(c => c.BankAccount).Where(c => c.OnDate == ondate).ToList();

        }
        public List<BankTranscation> GetTranscation() { return transcations; }
        public bool Save(BankTranscation bt, bool isNew = true)
        {
            Transcation = bt;
            if (isNew)
                azuredb.BankTranscations.Add(Transcation);
            else
                azuredb.BankTranscations.Update(Transcation);
            var flag = azuredb.SaveChanges() > 0;
            if (flag)
            {
                if (transcations == null) transcations = new List<BankTranscation>();
                transcations.Add(Transcation);
            }
            return flag;
        }



    }
}
