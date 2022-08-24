using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.ViewModels.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms.Vouchers
{
    public partial class VochersForm : Form
    {
        private readonly VoucherType voucherType;

        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private int SelectedYear;

        private ObservableListSource<VoucherVM> voucherVMs;
        private ObservableListSource<CashVoucherVM> cashVoucherVMs;

        private List<int> DataList;
        private SortedDictionary<string, bool> DataDictionary = new SortedDictionary<string, bool>();

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
            foreach (var vou in cashVouchers)
            {
                cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(vou));
            }
        }

        private void LoadData()
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            SelectedYear = DateTime.Now.Year;

            LoadYearList();

            DataList = new List<int>();

            LoadDataGrid(voucherType, DateTime.Today.Year);
        }

        private void LoadDataGrid(VoucherType type, int year)
        {
            if (!DataDictionary.ContainsKey(type.ToString() + year))
            {
                if (type == VoucherType.CashPayment || type == VoucherType.CashReceipt)
                {
                    var listData = azureDb.CashVouchers
                        .Include(c => c.Employee).Include(c => c.Partys).Include(c => c.TranscationMode)
                        .Where(c => c.VoucherType == type && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                    UpdateCashVoucherList(listData);
                }
                else
                {
                    var listData = azureDb.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == type && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();
                    UpdateVoucherList(listData);
                }

                DataDictionary.Add(type.ToString() + year, true);
            }

            switch (type)
            {
                case VoucherType.Payment:
                    dgvPayments.DataSource = voucherVMs.Where(c => c.VoucherType == VoucherType.Payment && c.OnDate.Year == year).ToList();
                    tabControl1.SelectedTab = tpPayments;
                    break;

                case VoucherType.Receipt:
                    dgvReceipts.DataSource = voucherVMs.Where(c => c.VoucherType == VoucherType.Receipt && c.OnDate.Year == year).ToList();
                    tabControl1.SelectedTab = tpReceipts;
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
                    dgvExpenses.DataSource = voucherVMs.Where(c => c.VoucherType == VoucherType.Expense && c.OnDate.Year == year).ToList();
                    tabControl1.SelectedTab = tpExpenses;
                    dgvExpenses.ScrollBars = ScrollBars.Both;
                    dgvExpenses.AutoSize = true;
                    break;

                case VoucherType.CashReceipt:
                    dgvCashReceipts.DataSource = cashVoucherVMs.Where(c => c.VoucherType == VoucherType.CashReceipt && c.OnDate.Year == year).ToList();
                    tabControl1.SelectedTab = tpCashReceipts;
                    break;

                case VoucherType.CashPayment:
                    dgvCashPayments.DataSource = cashVoucherVMs.Where(c => c.VoucherType == VoucherType.CashPayment && c.OnDate.Year == year).ToList();
                    tabControl1.SelectedTab = tpCashPayments;
                    break;

                default:
                    break;
            }
        }

        private void LoadYearList()
        {
            var years = azureDb.Vouchers.Select(c => c.OnDate.Year).Distinct().ToList();
            years.AddRange(azureDb.CashVouchers.Select(c => c.OnDate.Year).Distinct().ToList());
            years = years.Distinct().OrderBy(c => c).ToList();
            lbYearList.DataSource = years;
            lbYearList.SelectedValue = DateTime.Today.Year;
            lbYearList.SetSelected(lbYearList.Items.IndexOf(DateTime.Today.Year), true);
        }

        private void OnSelectedTab(int index)
        {
            switch (index)
            {
                case 1:
                    LoadDataGrid(VoucherType.Payment, SelectedYear);
                    break;

                case 2:
                    LoadDataGrid(VoucherType.Receipt, SelectedYear);
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
                    LoadDataGrid(VoucherType.Expense, SelectedYear);
                    break;

                case 4:
                    LoadDataGrid(VoucherType.CashPayment, SelectedYear);
                    break;

                case 3:
                    LoadDataGrid(VoucherType.CashReceipt, SelectedYear);
                    break;

                default:
                    LoadDataGrid(VoucherType.Expense, SelectedYear);
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
                    dgvExpenses.Refresh();
                    dgvExpenses.ScrollBars = ScrollBars.Both;
                    dgvExpenses.AutoSize = true;
                    break;

                case VoucherType.CashReceipt:
                    dgvCashReceipts.DataSource = cashVoucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();

                    break;

                case VoucherType.CashPayment:
                    dgvCashPayments.DataSource = cashVoucherVMs.Where(c => c.VoucherType == type).OrderByDescending(c => c.OnDate).ToList();

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
                case 0://Epenses
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Expense);
                    voucherType = VoucherType.Expense;
                    break;

                case 1://payment
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Payment);
                    voucherType = VoucherType.Payment;
                    break;

                case 2:
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Receipt);
                    voucherType = VoucherType.Receipt;
                    //Receipts
                    break;

                case 3://Cash Receipts
                    voucherEntryForm = new VoucherEntryForm(VoucherType.CashReceipt);
                    voucherType = VoucherType.CashReceipt;
                    break;

                case 4:
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
                    cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(voucherEntryForm.SavedCashVoucher));
                }
                else
                {
                    // Add to voucherEntryForm.voucher
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(voucherEntryForm.SavedVoucher));
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
            voucherVMs = new ObservableListSource<VoucherVM>();
            cashVoucherVMs = new ObservableListSource<CashVoucherVM>();
            LoadData();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
        }

        //On Item Selected on DataGridView

        private void dgvExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvExpenses.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Expense, rowData);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (form.SavedVoucher != null)
                {
                    if (!form.isNew)
                        voucherVMs.Remove((VoucherVM)dgvExpenses.CurrentRow.DataBoundItem);
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(form.SavedVoucher));
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    cashVoucherVMs.Remove(cashVoucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                else
                {
                    voucherVMs.Remove(voucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                RefreshDataView(form.voucherType);
            }
            else
            {
            }
        }

        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvPayments.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Payment, rowData);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (form.SavedVoucher != null)
                {
                    if (!form.isNew)
                        voucherVMs.Remove((VoucherVM)dgvPayments.CurrentRow.DataBoundItem);
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(form.SavedVoucher));
                    RefreshDataView(VoucherType.Expense);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    cashVoucherVMs.Remove(cashVoucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                else
                {
                    voucherVMs.Remove(voucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                RefreshDataView(form.voucherType);
            }
            else
            {
            }
        }

        private void dgvReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvReceipts.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Receipt, rowData);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (form.SavedVoucher != null)
                {
                    if (!form.isNew)
                        voucherVMs.Remove((VoucherVM)dgvReceipts.CurrentRow.DataBoundItem);
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(form.SavedVoucher));
                    RefreshDataView(VoucherType.Receipt);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    cashVoucherVMs.Remove(cashVoucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                else
                {
                    voucherVMs.Remove(voucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                RefreshDataView(form.voucherType);
            }
            else
            {
            }
        }

        private void dgvCashReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<CashVoucher>(dgvCashReceipts.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.CashReceipt, rowData);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (form.SavedCashVoucher != null)
                {
                    if (!form.isNew)
                        cashVoucherVMs.Remove((CashVoucherVM)dgvCashReceipts.CurrentRow.DataBoundItem);
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(form.SavedVoucher));
                    RefreshDataView(VoucherType.CashReceipt);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    cashVoucherVMs.Remove(cashVoucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                else
                {
                    voucherVMs.Remove(voucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                RefreshDataView(form.voucherType);
            }
            else
            {
            }
        }

        private void dgvCashPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<CashVoucher>(dgvCashPayments.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.CashPayment, rowData);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (form.SavedCashVoucher != null)
                {
                    if (!form.isNew)
                        cashVoucherVMs.Remove((CashVoucherVM)dgvCashReceipts.CurrentRow.DataBoundItem);
                    voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(form.SavedVoucher));
                    RefreshDataView(VoucherType.CashPayment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    cashVoucherVMs.Remove(cashVoucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                else
                {
                    voucherVMs.Remove(voucherVMs.Where(c => c.VoucherNumber == form.deleteVoucherNumber).First());
                }
                RefreshDataView(form.voucherType);
            }
            else
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataView(voucherType);
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void lbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedYear = (int)lbYearList.SelectedValue;
            }
            catch (Exception ex)
            {
                SelectedYear = DateTime.Today.Year;
            }
        }

        private void lbYearList_DoubleClick(object sender, EventArgs e)
        {
            SelectedYear = (int)lbYearList.SelectedValue;
            OnSelectedTab(tabControl1.SelectedIndex);
        }

        private void HideUnwantedCol(List<Object> lst, VoucherType type)
        {
            if (type == VoucherType.CashReceipt || type == VoucherType.CashPayment)
            {
            }
            else
            {
            }
        }
    }
}