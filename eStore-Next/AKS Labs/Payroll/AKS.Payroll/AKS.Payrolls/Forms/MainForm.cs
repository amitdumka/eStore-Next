using AKS.Payroll.Forms;

namespace AKS.Payroll
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new()
            {
                MdiParent = this,
                Text = "Window " + childFormNumber++
            };
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new EmployeeForm());
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AttendanceForm());
        }

        private void LoadForm(Form frm)
        {
            frm.MdiParent = this;
            frm.Show();
        }

        private void salaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadForm(new )
            LoadForm(new SalaryForm());
        }

        private void monthlyAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new MonthlyAttendanceForm());
        }

        private void addAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new Forms.EntryForms.AttendanceEntryForm());
        }

        private void addSalaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PayrollMigration pm = new PayrollMigration();
            //pm.Migrate();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new SalaryPaymentForm());
        }

        private void recieptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new SalaryPaymentForm(true));
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testFormToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // LoadForm(new TestForm());
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AboutBox1());
        }

        private void paToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new PaySlipForm());
        }

        private void printCurrentPaySlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call From Ops then print.
            //var form = new PdfForm();
            //LoadForm(form);
           // new BasicOperations().PayrollReport();
        }

        private void bankLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new PayslipBankLetterForm());
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadForm(new BankForm());
        }

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadForm(new VochersForm(VoucherType.Expense));
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new VochersForm(VoucherType.Payment));
        }

        private void receiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new VochersForm(VoucherType.Receipt));
        }

        private void cashPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new VochersForm(VoucherType.CashPayment));
        }

        private void cashReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new VochersForm(VoucherType.CashReceipt));
        }

        private void dailySaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new DailySaleForm());
        }

        private void pettyCashSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new PettyCashSheetForm());
        }

        private void timeSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new TimeSheetForm());
        }

        private void regularSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new SalesForm(InvoiceType.Sales));
        }

        private void manualSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadForm(new SalesForm(InvoiceType.ManualSale));
        }

        private void saleReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new SalesForm(InvoiceType.SalesReturn));
        }

        private void manulSaleReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new SalesForm(InvoiceType.ManualSaleReturn));
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // LoadForm(new PurchaseForm( ));
        }
    }
}
