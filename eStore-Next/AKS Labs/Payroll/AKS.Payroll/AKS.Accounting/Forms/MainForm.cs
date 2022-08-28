﻿using AKS.Accounting.Forms;
using AKS.Accounting.Forms.Banking;
using AKS.Accounting.Forms.Vouchers;

namespace AKS.Accounting.Forms
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

        private void LoadForm(Form frm)
        {
            frm.MdiParent = this;
            frm.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void addReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AboutBox1());
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

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}