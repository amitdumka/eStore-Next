namespace AKS.Payroll.Forms
{
    partial class PettyCashSheetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings1 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PettyCashSheetForm));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvPettyCashSheet = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxStore = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpOnDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpeningBalance = new System.Windows.Forms.TextBox();
            this.txtSale = new System.Windows.Forms.TextBox();
            this.txtManualSale = new System.Windows.Forms.TextBox();
            this.txtTailoring = new System.Windows.Forms.TextBox();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.txtCardSale = new System.Windows.Forms.TextBox();
            this.txtTailoringPayment = new System.Windows.Forms.TextBox();
            this.txtNonCashSale = new System.Windows.Forms.TextBox();
            this.txtBankDeposit = new System.Windows.Forms.TextBox();
            this.btnPayment = new System.Windows.Forms.Button();
            this.lbPrimaryKey = new System.Windows.Forms.Label();
            this.lbRec = new System.Windows.Forms.Label();
            this.lbRecList = new System.Windows.Forms.Label();
            this.lbPayList = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbPay = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnReceipt = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDueNaration = new System.Windows.Forms.TextBox();
            this.txtWithdrawal = new System.Windows.Forms.TextBox();
            this.lbCIH = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRecovery = new System.Windows.Forms.Button();
            this.btnDue = new System.Windows.Forms.Button();
            this.txtCashInHand = new System.Windows.Forms.TextBox();
            this.txtDueAmount = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pdfView = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbYearList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbYearly = new System.Windows.Forms.RadioButton();
            this.lbLMonth = new System.Windows.Forms.RadioButton();
            this.rbCMonth = new System.Windows.Forms.RadioButton();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPettyCashSheet)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 475);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1069, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(112, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(957, 416);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PettyCash";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(951, 394);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvPettyCashSheet);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(943, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Petty Cash Sheet";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvPettyCashSheet
            // 
            this.dgvPettyCashSheet.AllowUserToAddRows = false;
            this.dgvPettyCashSheet.AllowUserToDeleteRows = false;
            this.dgvPettyCashSheet.AllowUserToOrderColumns = true;
            this.dgvPettyCashSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPettyCashSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPettyCashSheet.Location = new System.Drawing.Point(3, 3);
            this.dgvPettyCashSheet.Name = "dgvPettyCashSheet";
            this.dgvPettyCashSheet.ReadOnly = true;
            this.dgvPettyCashSheet.RowHeadersWidth = 51;
            this.dgvPettyCashSheet.RowTemplate.Height = 25;
            this.dgvPettyCashSheet.Size = new System.Drawing.Size(937, 360);
            this.dgvPettyCashSheet.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(943, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Entry";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxStore, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpOnDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label11, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbId, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtOpeningBalance, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSale, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtManualSale, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtTailoring, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtNaration, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtCardSale, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtTailoringPayment, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtNonCashSale, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtBankDeposit, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnPayment, 6, 7);
            this.tableLayoutPanel1.Controls.Add(this.lbPrimaryKey, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbRec, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lbRecList, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lbPayList, 5, 8);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.lbPay, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnReceipt, 5, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtAmount, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtDueNaration, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtWithdrawal, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbCIH, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label15, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnRecovery, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnDue, 6, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtCashInHand, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtDueAmount, 3, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 360);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Store";
            // 
            // cbxStore
            // 
            this.cbxStore.FormattingEnabled = true;
            this.cbxStore.Location = new System.Drawing.Point(187, 5);
            this.cbxStore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxStore.Name = "cbxStore";
            this.cbxStore.Size = new System.Drawing.Size(124, 23);
            this.cbxStore.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Sale";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Opening Balance";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Date";
            // 
            // dtpOnDate
            // 
            this.dtpOnDate.Location = new System.Drawing.Point(400, 5);
            this.dtpOnDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpOnDate.Name = "dtpOnDate";
            this.dtpOnDate.Size = new System.Drawing.Size(219, 23);
            this.dtpOnDate.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Manual Sale";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Tailoring Sale";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(400, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 15);
            this.label11.TabIndex = 18;
            this.label11.Text = "Card Sale";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(400, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 15);
            this.label12.TabIndex = 19;
            this.label12.Text = "Tailoring Payment";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(400, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 15);
            this.label13.TabIndex = 20;
            this.label13.Text = "Non Cash Sale";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(400, 129);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 15);
            this.label14.TabIndex = 21;
            this.label14.Text = "Bank Deposit";
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Location = new System.Drawing.Point(631, 3);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(18, 15);
            this.lbId.TabIndex = 24;
            this.lbId.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Receipt/Payment/Expenses/etc";
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.Location = new System.Drawing.Point(187, 36);
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(144, 23);
            this.txtOpeningBalance.TabIndex = 28;
            // 
            // txtSale
            // 
            this.txtSale.Location = new System.Drawing.Point(187, 68);
            this.txtSale.Name = "txtSale";
            this.txtSale.Size = new System.Drawing.Size(144, 23);
            this.txtSale.TabIndex = 29;
            // 
            // txtManualSale
            // 
            this.txtManualSale.Location = new System.Drawing.Point(187, 100);
            this.txtManualSale.Name = "txtManualSale";
            this.txtManualSale.Size = new System.Drawing.Size(144, 23);
            this.txtManualSale.TabIndex = 30;
            // 
            // txtTailoring
            // 
            this.txtTailoring.Location = new System.Drawing.Point(187, 132);
            this.txtTailoring.Name = "txtTailoring";
            this.txtTailoring.Size = new System.Drawing.Size(144, 23);
            this.txtTailoring.TabIndex = 31;
            // 
            // txtNaration
            // 
            this.txtNaration.Location = new System.Drawing.Point(187, 228);
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(144, 23);
            this.txtNaration.TabIndex = 34;
            // 
            // txtCardSale
            // 
            this.txtCardSale.Location = new System.Drawing.Point(631, 36);
            this.txtCardSale.Name = "txtCardSale";
            this.txtCardSale.Size = new System.Drawing.Size(144, 23);
            this.txtCardSale.TabIndex = 35;
            // 
            // txtTailoringPayment
            // 
            this.txtTailoringPayment.Location = new System.Drawing.Point(631, 68);
            this.txtTailoringPayment.Name = "txtTailoringPayment";
            this.txtTailoringPayment.Size = new System.Drawing.Size(144, 23);
            this.txtTailoringPayment.TabIndex = 36;
            // 
            // txtNonCashSale
            // 
            this.txtNonCashSale.Location = new System.Drawing.Point(631, 100);
            this.txtNonCashSale.Name = "txtNonCashSale";
            this.txtNonCashSale.Size = new System.Drawing.Size(144, 23);
            this.txtNonCashSale.TabIndex = 37;
            // 
            // txtBankDeposit
            // 
            this.txtBankDeposit.Location = new System.Drawing.Point(631, 132);
            this.txtBankDeposit.Name = "txtBankDeposit";
            this.txtBankDeposit.Size = new System.Drawing.Size(144, 23);
            this.txtBankDeposit.TabIndex = 38;
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(784, 228);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(75, 23);
            this.btnPayment.TabIndex = 43;
            this.btnPayment.Text = "+ Payment";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // lbPrimaryKey
            // 
            this.lbPrimaryKey.AutoSize = true;
            this.lbPrimaryKey.Location = new System.Drawing.Point(784, 3);
            this.lbPrimaryKey.Name = "lbPrimaryKey";
            this.lbPrimaryKey.Size = new System.Drawing.Size(70, 15);
            this.lbPrimaryKey.TabIndex = 44;
            this.lbPrimaryKey.Text = "PettyCashId";
            // 
            // lbRec
            // 
            this.lbRec.AutoSize = true;
            this.lbRec.Location = new System.Drawing.Point(6, 257);
            this.lbRec.Name = "lbRec";
            this.lbRec.Size = new System.Drawing.Size(44, 15);
            this.lbRec.TabIndex = 45;
            this.lbRec.Text = "label18";
            // 
            // lbRecList
            // 
            this.lbRecList.AutoSize = true;
            this.lbRecList.Location = new System.Drawing.Point(187, 257);
            this.lbRecList.Name = "lbRecList";
            this.lbRecList.Size = new System.Drawing.Size(44, 15);
            this.lbRecList.TabIndex = 46;
            this.lbRecList.Text = "label19";
            // 
            // lbPayList
            // 
            this.lbPayList.AutoSize = true;
            this.lbPayList.Location = new System.Drawing.Point(631, 257);
            this.lbPayList.Name = "lbPayList";
            this.lbPayList.Size = new System.Drawing.Size(44, 15);
            this.lbPayList.TabIndex = 47;
            this.lbPayList.Text = "label20";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(340, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 15);
            this.label9.TabIndex = 16;
            // 
            // lbPay
            // 
            this.lbPay.AutoSize = true;
            this.lbPay.Location = new System.Drawing.Point(400, 257);
            this.lbPay.Name = "lbPay";
            this.lbPay.Size = new System.Drawing.Size(44, 15);
            this.lbPay.TabIndex = 48;
            this.lbPay.Text = "label21";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(340, 225);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 15);
            this.label16.TabIndex = 23;
            this.label16.Text = "Amount";
            // 
            // btnReceipt
            // 
            this.btnReceipt.Location = new System.Drawing.Point(631, 228);
            this.btnReceipt.Name = "btnReceipt";
            this.btnReceipt.Size = new System.Drawing.Size(75, 23);
            this.btnReceipt.TabIndex = 42;
            this.btnReceipt.Text = "+ Receipt";
            this.btnReceipt.UseVisualStyleBackColor = true;
            this.btnReceipt.Click += new System.EventHandler(this.btnReceipt_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(400, 228);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(144, 23);
            this.txtAmount.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 15);
            this.label10.TabIndex = 17;
            this.label10.Text = "Bill/Customer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Bank Withdrwal";
            // 
            // txtDueNaration
            // 
            this.txtDueNaration.Location = new System.Drawing.Point(187, 196);
            this.txtDueNaration.Name = "txtDueNaration";
            this.txtDueNaration.Size = new System.Drawing.Size(144, 23);
            this.txtDueNaration.TabIndex = 32;
            // 
            // txtWithdrawal
            // 
            this.txtWithdrawal.Location = new System.Drawing.Point(187, 164);
            this.txtWithdrawal.Name = "txtWithdrawal";
            this.txtWithdrawal.Size = new System.Drawing.Size(144, 23);
            this.txtWithdrawal.TabIndex = 33;
            // 
            // lbCIH
            // 
            this.lbCIH.AutoSize = true;
            this.lbCIH.Location = new System.Drawing.Point(400, 161);
            this.lbCIH.Name = "lbCIH";
            this.lbCIH.Size = new System.Drawing.Size(78, 15);
            this.lbCIH.TabIndex = 27;
            this.lbCIH.Text = "Cash In Hand";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(340, 193);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 15);
            this.label15.TabIndex = 22;
            this.label15.Text = "Amount";
            // 
            // btnRecovery
            // 
            this.btnRecovery.Location = new System.Drawing.Point(631, 196);
            this.btnRecovery.Name = "btnRecovery";
            this.btnRecovery.Size = new System.Drawing.Size(75, 23);
            this.btnRecovery.TabIndex = 49;
            this.btnRecovery.Text = "Recovery";
            this.btnRecovery.UseVisualStyleBackColor = true;
            this.btnRecovery.Click += new System.EventHandler(this.btnRecovery_Click);
            // 
            // btnDue
            // 
            this.btnDue.Location = new System.Drawing.Point(784, 196);
            this.btnDue.Name = "btnDue";
            this.btnDue.Size = new System.Drawing.Size(75, 23);
            this.btnDue.TabIndex = 50;
            this.btnDue.Text = "Due";
            this.btnDue.UseVisualStyleBackColor = true;
            this.btnDue.Click += new System.EventHandler(this.btnDue_Click);
            // 
            // txtCashInHand
            // 
            this.txtCashInHand.Location = new System.Drawing.Point(631, 164);
            this.txtCashInHand.Name = "txtCashInHand";
            this.txtCashInHand.Size = new System.Drawing.Size(144, 23);
            this.txtCashInHand.TabIndex = 40;
            // 
            // txtDueAmount
            // 
            this.txtDueAmount.Location = new System.Drawing.Point(400, 196);
            this.txtDueAmount.Name = "txtDueAmount";
            this.txtDueAmount.Size = new System.Drawing.Size(144, 23);
            this.txtDueAmount.TabIndex = 39;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pdfView);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(943, 366);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Print/View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pdfView
            // 
            this.pdfView.AutoScroll = true;
            this.pdfView.AutoSize = true;
            this.pdfView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.pdfView.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            this.pdfView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfView.EnableContextMenu = true;
            this.pdfView.HorizontalScrollOffset = 0;
            this.pdfView.IsTextSearchEnabled = true;
            this.pdfView.IsTextSelectionEnabled = true;
            this.pdfView.Location = new System.Drawing.Point(3, 3);
            messageBoxSettings1.EnableNotification = true;
            this.pdfView.MessageBoxSettings = messageBoxSettings1;
            this.pdfView.MinimumZoomPercentage = 50;
            this.pdfView.Name = "pdfView";
            this.pdfView.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            this.pdfView.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfView.ReferencePath = null;
            this.pdfView.ScrollDisplacementValue = 0;
            this.pdfView.ShowHorizontalScrollBar = true;
            this.pdfView.ShowVerticalScrollBar = true;
            this.pdfView.Size = new System.Drawing.Size(937, 360);
            this.pdfView.SpaceBetweenPages = 8;
            this.pdfView.TabIndex = 0;
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfView.TextSearchSettings = textSearchSettings1;
            this.pdfView.ThemeName = "Default";
            this.pdfView.VerticalScrollOffset = 0;
            this.pdfView.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfView.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbYearList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(112, 416);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Year";
            // 
            // lbYearList
            // 
            this.lbYearList.DisplayMember = "Year";
            this.lbYearList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbYearList.FormattingEnabled = true;
            this.lbYearList.ItemHeight = 15;
            this.lbYearList.Location = new System.Drawing.Point(3, 19);
            this.lbYearList.Name = "lbYearList";
            this.lbYearList.Size = new System.Drawing.Size(106, 394);
            this.lbYearList.TabIndex = 0;
            this.lbYearList.ValueMember = "Year";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnPreview);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1069, 59);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(283, 22);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 30);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnDueRecovery_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.rbYearly);
            this.panel1.Controls.Add(this.lbLMonth);
            this.panel1.Controls.Add(this.rbCMonth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(808, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 37);
            this.panel1.TabIndex = 3;
            // 
            // rbYearly
            // 
            this.rbYearly.AutoSize = true;
            this.rbYearly.Location = new System.Drawing.Point(208, 8);
            this.rbYearly.Name = "rbYearly";
            this.rbYearly.Size = new System.Drawing.Size(47, 19);
            this.rbYearly.TabIndex = 2;
            this.rbYearly.Text = "Year";
            this.rbYearly.UseVisualStyleBackColor = true;
            // 
            // lbLMonth
            // 
            this.lbLMonth.AutoSize = true;
            this.lbLMonth.Location = new System.Drawing.Point(112, 8);
            this.lbLMonth.Name = "lbLMonth";
            this.lbLMonth.Size = new System.Drawing.Size(85, 19);
            this.lbLMonth.TabIndex = 1;
            this.lbLMonth.Text = "Last Month";
            this.lbLMonth.UseVisualStyleBackColor = true;
            // 
            // rbCMonth
            // 
            this.rbCMonth.AutoSize = true;
            this.rbCMonth.Checked = true;
            this.rbCMonth.Location = new System.Drawing.Point(0, 8);
            this.rbCMonth.Name = "rbCMonth";
            this.rbCMonth.Size = new System.Drawing.Size(104, 19);
            this.rbCMonth.TabIndex = 0;
            this.rbCMonth.TabStop = true;
            this.rbCMonth.Text = "Current Month";
            this.rbCMonth.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.AutoSize = true;
            this.btnPreview.Location = new System.Drawing.Point(202, 22);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 30);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "View";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Location = new System.Drawing.Point(122, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new System.Drawing.Point(41, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // PettyCashSheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 497);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Name = "PettyCashSheetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petty Cash Sheet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PettyCashSheetForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPettyCashSheet)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private GroupBox groupBox3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private DataGridView dgvPettyCashSheet;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private GroupBox groupBox2;
        private ListBox lbYearList;
        private GroupBox groupBox1;
        private Button btnPrint;
        private Panel panel1;
        private RadioButton rbYearly;
        private RadioButton lbLMonth;
        private RadioButton rbCMonth;
        private Button btnPreview;
        private Button btnDelete;
        private Button btnAdd;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ComboBox cbxStore;
        private Label label7;
        private Label label2;
        private Label label4;
        private DateTimePicker dtpOnDate;
        private Label label5;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label lbId;
        private Label label3;
        private Label label6;
        private Label lbCIH;
        private TextBox txtOpeningBalance;
        private TextBox txtSale;
        private TextBox txtManualSale;
        private TextBox txtTailoring;
        private TextBox txtDueNaration;
        private TextBox txtWithdrawal;
        private TextBox txtNaration;
        private TextBox txtCardSale;
        private TextBox txtTailoringPayment;
        private TextBox txtNonCashSale;
        private TextBox txtBankDeposit;
        private TextBox txtDueAmount;
        private TextBox txtCashInHand;
        private TextBox txtAmount;
        private Button btnReceipt;
        private Button btnPayment;
        private Label lbPrimaryKey;
        private Label lbRec;
        private Label lbRecList;
        private Label lbPayList;
        private Label label9;
        private Label lbPay;
        private Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView pdfView;
        private Button btnRecovery;
        private Button btnDue;
    }
}