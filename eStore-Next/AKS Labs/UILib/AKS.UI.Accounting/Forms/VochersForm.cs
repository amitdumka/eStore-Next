using AKS.AccountingSystem.DTO;
using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.ViewModels.Accounts;
using Syncfusion.Windows.Forms.PdfViewer;
//TODO: Add summary report like total Yearly, montly, daily from voucher list of particular type
namespace AKS.UI.Accounting.Forms
{
    public partial class VochersForm : Form
    {
        private readonly VoucherType voucherType;
        private VoucherCashViewModel _voucherViewModel;

        //Old
        private int SelectedYear;

        //private SortedDictionary<string, bool> DataDictionary = new SortedDictionary<string, bool>();

        public VochersForm()
        {
            InitializeComponent();
            _voucherViewModel = new VoucherCashViewModel(VoucherType.Payment);
            voucherType = VoucherType.Payment;
        }

        public VochersForm(VoucherType type)
        {
            InitializeComponent();
            _voucherViewModel = new VoucherCashViewModel(type);
            voucherType = type;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            VoucherEntryForm voucherEntryForm;
            VoucherType voucherType = VoucherType.Expense;
            switch (tabControl1.SelectedIndex)
            {
                case 0://Expenses
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Expense, _voucherViewModel);
                    voucherType = VoucherType.Expense;
                    break;

                case 1://payment
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Payment, _voucherViewModel);
                    voucherType = VoucherType.Payment;
                    break;

                case 2:
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Receipt, _voucherViewModel);
                    voucherType = VoucherType.Receipt;
                    //Receipts
                    break;

                case 3://Cash Receipts
                    voucherEntryForm = new VoucherEntryForm(VoucherType.CashReceipt, _voucherViewModel);
                    voucherType = VoucherType.CashReceipt;
                    break;

                case 4:
                    //Cash Payments
                    voucherEntryForm = new VoucherEntryForm(VoucherType.CashPayment, _voucherViewModel);
                    voucherType = VoucherType.CashPayment;
                    break;

                default:
                    voucherEntryForm = new VoucherEntryForm(VoucherType.Expense, _voucherViewModel);
                    break;
            }
            if (voucherEntryForm.ShowDialog() == DialogResult.OK)
            {
                // on event of Add

                if (_voucherViewModel.voucherType == VoucherType.CashReceipt || _voucherViewModel.voucherType == VoucherType.CashPayment)
                {
                    // Add to list voucherEntryForm.cashVoucher
                    //cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(voucherEntry_voucherViewModel.SavedCashVoucher));

                    _voucherViewModel.UpdateCashVoucherList(null, _voucherViewModel.SavedCashVoucher, true);

                    PrintCashVoucher(_voucherViewModel.SavedCashVoucher);
                }
                else
                {
                    // Add to voucherEntryForm.voucher
                    //voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(voucherEntryForm.SavedVoucher));
                    _voucherViewModel.UpdateVoucherList(null, _voucherViewModel.SavedVoucher, true);
                    PrintVoucher(_voucherViewModel.SavedVoucher);
                }
                RefreshDataView(_voucherViewModel.voucherType);
            }
            else if (voucherEntryForm.DialogResult == DialogResult.Yes)
            {
                // on De
            }
            else
            {
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataView(voucherType);
        }

        private void dgvCashPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<CashVoucher>(dgvCashPayments.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.CashPayment, rowData, _voucherViewModel);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (_voucherViewModel.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateCashVoucherList((CashVoucherVM)dgvCashPayments.CurrentRow.DataBoundItem, _voucherViewModel.SavedCashVoucher, _voucherViewModel.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (_voucherViewModel.voucherType == VoucherType.CashPayment || _voucherViewModel.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                RefreshDataView(_voucherViewModel.voucherType);
            }
            else
            {
            }
        }

        private void dgvCashReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<CashVoucher>(dgvCashReceipts.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.CashReceipt, rowData, _voucherViewModel);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (_voucherViewModel.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateCashVoucherList((CashVoucherVM)dgvCashReceipts.CurrentRow.DataBoundItem, _voucherViewModel.SavedCashVoucher, _voucherViewModel.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (_voucherViewModel.voucherType == VoucherType.CashPayment || _voucherViewModel.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                RefreshDataView(_voucherViewModel.voucherType);
            }
            else
            {
            }
        }

        private void dgvExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvExpenses.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Expense, rowData, _voucherViewModel);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (_voucherViewModel.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateVoucherList((VoucherVM)dgvExpenses.CurrentRow.DataBoundItem, _voucherViewModel.SavedVoucher, _voucherViewModel.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (_voucherViewModel.voucherType == VoucherType.CashPayment || _voucherViewModel.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                RefreshDataView(_voucherViewModel.voucherType);
            }
            else
            {
            }
        }

        //On Item Selected on DataGridView
        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvPayments.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Payment, rowData, _voucherViewModel);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (_voucherViewModel.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateVoucherList((VoucherVM)dgvPayments.CurrentRow.DataBoundItem, _voucherViewModel.SavedVoucher, _voucherViewModel.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (_voucherViewModel.voucherType == VoucherType.CashPayment || _voucherViewModel.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                RefreshDataView(_voucherViewModel.voucherType);
            }
            else
            {
            }
        }

        private void dgvReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvReceipts.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Receipt, rowData, _voucherViewModel);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (_voucherViewModel.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateVoucherList((VoucherVM)dgvReceipts.CurrentRow.DataBoundItem, _voucherViewModel.SavedVoucher, _voucherViewModel.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (_voucherViewModel.voucherType == VoucherType.CashPayment || _voucherViewModel.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(_voucherViewModel.deleteVoucherNumber);
                }
                RefreshDataView(_voucherViewModel.voucherType);
            }
            else
            {
            }
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

        private void lbYearList_DoubleClick(object sender, EventArgs e)
        {
            SelectedYear = (int)lbYearList.SelectedValue;
            OnSelectedTab(tabControl1.SelectedIndex);
        }

        private void lbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedYear = lbYearList.SelectedValue != null ? (int)lbYearList.SelectedValue : DateTime.Today.Year;
            }
            catch (Exception)
            {
                SelectedYear = DateTime.Today.Year;
            }
        }

        //Not Ported
        private void LoadData()
        {
            SelectedYear = DateTime.Now.Year;
            LoadYearList();
            LoadDataGrid(voucherType, DateTime.Today.Year);
        }

        private void LoadDataGrid(VoucherType type, int year)
        {
            _voucherViewModel.LoadGridData(type, year);

            switch (type)
            {
                case VoucherType.Payment:
                    dgvPayments.DataSource = _voucherViewModel.GetVouchers(VoucherType.Payment);
                    tabControl1.SelectedTab = tpPayments;
                    break;

                case VoucherType.Receipt:
                    dgvReceipts.DataSource = _voucherViewModel.GetVouchers(VoucherType.Receipt);
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
                    dgvExpenses.DataSource = _voucherViewModel.GetVouchers(VoucherType.Expense);
                    tabControl1.SelectedTab = tpExpenses;
                    dgvExpenses.ScrollBars = ScrollBars.Both;
                    dgvExpenses.AutoSize = true;
                    break;

                case VoucherType.CashReceipt:
                    dgvCashReceipts.DataSource = _voucherViewModel.GetCashVouchers(VoucherType.CashReceipt);
                    tabControl1.SelectedTab = tpCashReceipts;
                    break;

                case VoucherType.CashPayment:
                    dgvCashPayments.DataSource = _voucherViewModel.GetCashVouchers(VoucherType.CashPayment);
                    tabControl1.SelectedTab = tpCashPayments;
                    break;

                default:
                    break;
            }
        }

        private void LoadYearList()
        {
            var years = _voucherViewModel.GetYearList();

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

        private void PrintCashVoucher(CashVoucher voucher)
        {
            var dlg = MessageBox.Show("Do You want to print voucher.", "Print Confirmation", MessageBoxButtons.YesNo);

            if (dlg == DialogResult.Yes)
            {
                ShowPrintDialog(_voucherViewModel.PrintCashVoucher(voucher));
            }
        }

        private void PrintVoucher(Voucher voucher)
        {
            var dlg = MessageBox.Show("Do You want to print voucher.", "Print Confirmation", MessageBoxButtons.YesNo);

            if (dlg == DialogResult.Yes)
            {
                ShowPrintDialog(_voucherViewModel.PrintVoucher(voucher));
            }
        }

        private void RefreshDataView(VoucherType type)
        {
            switch (type)
            {
                case VoucherType.Payment:
                    dgvPayments.DataSource = _voucherViewModel.GetVouchers(type);

                    break;

                case VoucherType.Receipt:
                    dgvReceipts.DataSource = _voucherViewModel.GetVouchers(type);

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
                    dgvExpenses.DataSource = _voucherViewModel.GetVouchers(type);
                    dgvExpenses.Refresh();
                    dgvExpenses.ScrollBars = ScrollBars.Both;
                    dgvExpenses.AutoSize = true;
                    break;

                case VoucherType.CashReceipt:
                    dgvCashReceipts.DataSource = _voucherViewModel.GetCashVouchers(type);

                    break;

                case VoucherType.CashPayment:
                    dgvCashPayments.DataSource = _voucherViewModel.GetCashVouchers(type);

                    break;

                default:
                    break;
            }
        }

        private void ShowPrintDialog(string filename)
        {
            Form printForm = new Form();
            printForm.WindowState = FormWindowState.Maximized;
            PdfDocumentView docView = new PdfDocumentView();
            docView.Load(filename);
            docView.Dock = DockStyle.Fill;
            printForm.Controls.Add(docView);

            var result = printForm.ShowDialog();
            var printDialog1 = new PrintDialog();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.AllowPrintToFile = true;
                docView.Print(printDialog1.PrinterSettings.PrinterName);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;
            OnSelectedTab(tc.SelectedIndex);
        }

        private void VochersForm_Load(object sender, EventArgs e)
        {
            _voucherViewModel.InitViewModel();
            LoadData();
        }
    }
}