using AKS.Payroll.Database;
using System.Data;

namespace AKS.Payroll.Forms.Vouchers
{
    public partial class VochersForm : Form
    {

        private readonly VoucherType voucherType;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private List<int> DataList;

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
            DataList = new List<int>();

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

            if (!DataList.Contains(1))
            {
                var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Expense && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                dgvExpenses.DataSource = listData;
                tabControl1.SelectedTab = tpExpenses;
                DataList.Add(1);
            }


        }

        private void LoadPaymentData(int year)
        {
            if (!DataList.Contains(2))
            {
                var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Payment && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                dgvPayments.DataSource = listData;
                DataList.Add(2);
                tabControl1.SelectedTab = tpPayments;

            }

        }
        private void LoadReceiptData(int year)
        {
            if (!DataList.Contains(3))
            {
                var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Receipt && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                dgvReceipts.DataSource = listData;
                tabControl1.SelectedTab = tpReceipts;
                DataList.Add(3);
            }
        }
        private void LoadCashReceiptData(int year)
        {
            if (!DataList.Contains(4))
            {
                var listData = azureDb.CashVouchers.Where(c => c.VoucherType == VoucherType.CashReceipt && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                dgvCashReceipts.DataSource = listData;
                tabControl1.SelectedTab = tpCashReceipts;
                DataList.Add(4);
            }
        }

        private void LoadCashPaymentData(int year)
        {
            if (!DataList.Contains(5))
            {
                var listData = azureDb.CashVouchers.Where(c => c.VoucherType == VoucherType.CashPayment && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                dgvCashPayments.DataSource = listData;
                tabControl1.SelectedTab = tpCashPayments;
                DataList.Add(5);
            }
        }


        private void LoadYearList()
        {
            var years = azureDb.Vouchers.Select(c => c.OnDate.Year).Distinct().ToList();
            years.AddRange(azureDb.CashVouchers.Select(c => c.OnDate.Year).Distinct().ToList());
            years = years.Distinct().OrderBy(c => c).ToList();
            lbYearList.DataSource = years;
        }


        private void OnSelectedTab(int index)
        {
            switch (index)
            {
                case 1:
                    LoadPaymentData(DateTime.Today.Year);
                    break;
                case 2:
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
                case 0:
                    LoadExpensesData(DateTime.Today.Year);
                    break;
                case 3:
                    LoadCashReceiptData(DateTime.Today.Year);
                    break;
                case 4:
                    LoadCashPaymentData(DateTime.Today.Year);
                    break;
                default:
                    LoadExpensesData(DateTime.Today.Year);
                    break;
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;
            OnSelectedTab(tc.SelectedIndex);
        }

        private void VochersForm_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {


        }

        //On Item Selected on DataGridView

        private void dgvExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCashReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCashPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //TODO: Process Data
        }
    }
}
