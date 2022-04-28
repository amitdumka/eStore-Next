using AKS.Payroll.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payroll.Forms.Vouchers
{
    public partial class VochersForm : Form
    {

        private readonly VoucherType voucherType;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;

        public VochersForm()
        {
            InitializeComponent();
            voucherType = VoucherType.Expense;
        }
        public VochersForm(VoucherType type)
        {
            InitializeComponent();
            voucherType = type;
        }

        private void LoadData()
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            LoadYearList();

            switch (voucherType)
            {
                case VoucherType.Payment:
                    LoadPaymentData(DateTime.Today.Year);
                    break;
                case VoucherType.Receipt:
                    LoadReceiptData(DateTime.Today.Year);
                    break;
                //case VoucherType.Contra:
                //    break;
                //case VoucherType.DebitNote:
                //    break;
                //case VoucherType.CreditNote:
                //    break;
                //case VoucherType.JV:
                //    break;
                case VoucherType.Expense:
                    LoadExpensesData(DateTime.Today.Year);
                    break;
                case VoucherType.CashReceipt:
                    LoadCashReceiptData(DateTime.Today.Year);
                    break;
                case VoucherType.CashPayment:
                    LoadCashPaymentData(DateTime.Today.Year);
                    break;
                default:
                    LoadExpensesData(DateTime.Today.Year);
                    break;
            }
        }
        private void LoadExpensesData(int year)
        {

            var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Expense && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
            dgvExpenses.DataSource = listData;
            tabControl1.SelectedTab = tpExpenses;

        }

        private void LoadPaymentData(int year)
        {
            var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Payment && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
            dgvExpenses.DataSource = listData;
            tabControl1.SelectedTab = tpPayments;

        }
        private void LoadCashPaymentData(int year)
        {
            var listData = azureDb.CashVouchers.Where(c => c.VoucherType == VoucherType.CashPayment && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
            dgvExpenses.DataSource = listData;
            tabControl1.SelectedTab = tpCashPayments;
        }
        private void LoadCashReceiptData(int year)
        {
            var listData = azureDb.CashVouchers.Where(c => c.VoucherType == VoucherType.CashReceipt && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
            dgvExpenses.DataSource = listData;
            tabControl1.SelectedTab = tpCashReceipts;
        }
        private void LoadReceiptData(int year)
        {
            var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Receipt && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
            dgvExpenses.DataSource = listData;
            tabControl1.SelectedTab = tpReceipts;
        }

        private void LoadYearList()
        {
            var years = azureDb.Vouchers.Select(c => c.OnDate.Year).Distinct().ToList();
            years.AddRange(azureDb.CashVouchers.Select(c => c.OnDate.Year).Distinct().ToList());
            years = years.Distinct().OrderBy(c => c).ToList();
            lbYearList.DataSource = years;
        }

        









        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString()+"\n"+e.ToString());
        }

        private void VochersForm_Load(object sender, EventArgs e)
        {
            LoadData();

        }
    }
}
