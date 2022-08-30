using AKS.AccountingSystem.DTO;
using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.ViewModels.Accounts;
using Syncfusion.Windows.Forms.PdfViewer;

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
            _voucherViewModel = new VoucherCashViewModel();
            voucherType = VoucherType.Payment;
        }

        public VochersForm(VoucherType type)
        {
            InitializeComponent();
            _voucherViewModel = new VoucherCashViewModel();
            voucherType = type;
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

        private void VochersForm_Load(object sender, EventArgs e)
        {
            _voucherViewModel.InitViewModel();
            LoadData();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;
            OnSelectedTab(tc.SelectedIndex);
        }

        private void PrintVoucher(Voucher voucher)
        {
            var dlg = MessageBox.Show("Do You want to print voucher.", "Print Confirmation", MessageBoxButtons.YesNo);

            if (dlg == DialogResult.Yes)
            {
                ShowPrintDialog(_voucherViewModel.PrintVoucher(voucher));
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

        //On Item Selected on DataGridView

        private void dgvExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowData = DMMapper.Mapper.Map<Voucher>(dgvExpenses.CurrentRow.DataBoundItem);
            var form = new VoucherEntryForm(VoucherType.Expense, rowData);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                if (form.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateVoucherList((VoucherVM)dgvExpenses.CurrentRow.DataBoundItem, form.SavedVoucher, form.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(form.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(form.deleteVoucherNumber);
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
                    _voucherViewModel.UpdateVoucherList((VoucherVM)dgvPayments.CurrentRow.DataBoundItem, form.SavedVoucher, form.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(form.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(form.deleteVoucherNumber);
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
                    _voucherViewModel.UpdateVoucherList((VoucherVM)dgvReceipts.CurrentRow.DataBoundItem, form.SavedVoucher, form.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(form.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(form.deleteVoucherNumber);
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
                if (form.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateCashVoucherList((CashVoucherVM)dgvCashReceipts.CurrentRow.DataBoundItem, form.SavedCashVoucher, form.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(form.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(form.deleteVoucherNumber);
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
                if (form.SavedVoucher != null)
                {
                    _voucherViewModel.UpdateCashVoucherList((CashVoucherVM)dgvCashPayments.CurrentRow.DataBoundItem, form.SavedCashVoucher, form.isNew);
                    RefreshDataView(VoucherType.Payment);
                }
            }
            else if (form.DialogResult == DialogResult.OK)
            {
                //Delete
                if (form.voucherType == VoucherType.CashPayment || form.voucherType == VoucherType.CashReceipt)
                {
                    _voucherViewModel.RemoveCashVoucher(form.deleteVoucherNumber);
                }
                else
                {
                    _voucherViewModel.RemoveVoucher(form.deleteVoucherNumber);
                }
                RefreshDataView(form.voucherType);
            }
            else
            {
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataView(voucherType);
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

        private void lbYearList_DoubleClick(object sender, EventArgs e)
        {
            SelectedYear = (int)lbYearList.SelectedValue;
            OnSelectedTab(tabControl1.SelectedIndex);
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
                    dgvCashReceipts.DataSource = _voucherViewModel.GetVouchers(VoucherType.CashReceipt);
                    tabControl1.SelectedTab = tpCashReceipts;
                    break;

                case VoucherType.CashPayment:
                    dgvCashPayments.DataSource = _voucherViewModel.GetVouchers(VoucherType.CashPayment);
                    tabControl1.SelectedTab = tpCashPayments;
                    break;

                default:
                    break;
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
                    //cashVoucherVMs.Add(DMMapper.Mapper.Map<CashVoucherVM>(voucherEntryForm.SavedCashVoucher));

                    _voucherViewModel.UpdateCashVoucherList(null, voucherEntryForm.SavedCashVoucher, true);

                    PrintCashVoucher(voucherEntryForm.SavedCashVoucher);
                }
                else
                {
                    // Add to voucherEntryForm.voucher
                    //voucherVMs.Add(DMMapper.Mapper.Map<VoucherVM>(voucherEntryForm.SavedVoucher));
                    _voucherViewModel.UpdateVoucherList(null, voucherEntryForm.SavedVoucher, true);
                    PrintVoucher(voucherEntryForm.SavedVoucher);
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