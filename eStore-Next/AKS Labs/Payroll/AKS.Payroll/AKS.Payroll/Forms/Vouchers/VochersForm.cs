using AKS.DatabaseMigrator;
using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.ViewModels.Accounts;
using System.Data;

namespace AKS.Payroll.Forms.Vouchers
{
    public partial class VochersForm : Form
    {

        private readonly VoucherType voucherType;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private ObservableListSource<VoucherVM> voucherVMs;
        private ObservableListSource<CashVoucherVM> cashVoucherVMs;


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

        public void UpdateVoucherList(List<Voucher> vouchers)
        {

            foreach (var vou in vouchers)
            {
                voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(vou));
            }

        }
        public void UpdateCashVoucherList(List<CashVoucher> cashVouchers)
        {

            foreach (var vou in cashVoucherVMs)
            {
                cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(vou));
            }

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
                var listData = azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Payment  ).OrderBy(c => c.OnDate).ToList();
                dgvPayments.DataSource = listData;
                DataList.Add(2);
                tabControl1.SelectedTab = tpPayments;

            }

        }
        private void LoadReceiptData(int year)
        {
            if (!DataList.Contains(3))
            {
                var listData =azureDb.Vouchers.Where(c => c.VoucherType == VoucherType.Receipt  ).OrderBy(c => c.OnDate).ToList();
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


        private void RefreshDataView(VoucherType type)
        {
            switch (type)
            {
                case VoucherType.Payment:
                    dgvPayments.DataSource = voucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();
                    break;
                case VoucherType.Receipt:
                    dgvReceipts.DataSource = voucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();

                    break;
                case VoucherType.Contra:
                    break;
                case VoucherType.DebitNote:
                    break;
                case VoucherType.CreditNote:
                    break;
                case VoucherType.JV:
                    break;
                case VoucherType.Expense:
                    dgvExpenses.DataSource = voucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();

                    break;
                case VoucherType.CashReceipt:
                    dgvCashReceipts.DataSource = voucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();

                    break;
                case VoucherType.CashPayment:
                    dgvCashPayments.DataSource = voucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();

                    break;
                default:
                    break;
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            VoucherEntryForm voucherEntryForm;
            VoucherType voucherType = VoucherType.Expense;
            switch (tabControl1.SelectedIndex)
            {
                case 1://Epenses 
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Expense);
                    voucherType = VoucherType.Expense;
                    break;
                case 2://payment 
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Payment);
                    voucherType = VoucherType.Payment;
                    break;
                case 3:
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Receipt);
                    voucherType = VoucherType.Receipt;
                    //Receipts 
                    break;
                case 4://Cash Receipts 
                    voucherEntryForm = new VoucherEntryForm(VoucherType.CashReceipt);
                    voucherType = VoucherType.CashReceipt;
                    break;
                case 5:
                    //Cash Payments 
                    voucherEntryForm = new VoucherEntryForm(VoucherType.CashPayment);
                    voucherType = VoucherType.CashPayment;
                    break;
                default:
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Expense);
                    break;
            }
            if (voucherEntryForm.ShowDialog() == DialogResult.OK)
            {
                // on event of Add

                if (voucherEntryForm.voucherType == VoucherType.CashReceipt || voucherEntryForm.voucherType == VoucherType.CashPayment)
                {
                    // Add to list voucherEntryForm.cashVoucher
                    cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(voucherEntryForm.cashVoucher));
                }
                else
                {
                    // Add to voucherEntryForm.voucher
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(voucherEntryForm.voucher));
                }
                RefreshDataView(voucherEntryForm.voucherType);


            }
            else if (voucherEntryForm.DialogResult == DialogResult.Yes)
            {
                // on De
            }
            else
            {

            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;
            OnSelectedTab(tc.SelectedIndex);
        }

        private void VochersForm_Load(object sender, EventArgs e)
        {
            DMMapper.InitializeAutomapper();
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


        }
    }
}
