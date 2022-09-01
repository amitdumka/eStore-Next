using AKS.Shared.Commons.Ops;
using AKS.UI.Accounting.Forms;
using AKS.UI.Accounting.Forms.Banking;

namespace eStoreAccounts
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        private void depositWithdrawalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new BankTranscationForm());
        }

        private void dailySaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new DailySaleForm());
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new BankForm());
        }

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new VochersForm(VoucherType.Expense));
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new VochersForm(VoucherType.Payment));
        }

        private void receiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new VochersForm(VoucherType.Receipt));
        }

        private void cashPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new VochersForm(VoucherType.CashPayment));
        }

        private void cashReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new VochersForm(VoucherType.CashReceipt));
        }

        private void pettyCashSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new PettyCashSheetForm());
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

       

        private void debitNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void creditNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vendorDebitNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vendorCreditNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

         

        private void bankAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vendorAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void accountListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
        private void LoadForm(Form frm)
        {
            frm.MdiParent = this;
            frm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = CurrentSession.StoreName;
            toolStripStatusLabel2.Text = CurrentSession.StoreCode;
            toolStripStatusLabel3.Text = CurrentSession.GuestName;
            toolStripStatusLabel4.Text = CurrentSession.LoggedTime.ToString();
          toolStripStatusLabel5.Text= CurrentSession.UserType.ToString();

        }

        private void changeStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Change store so accountant do work based on store 
        }
    }
}