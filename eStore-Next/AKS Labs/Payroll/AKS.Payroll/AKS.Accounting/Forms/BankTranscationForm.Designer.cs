namespace AKS.Accounting.Forms
{
    partial class BankTranscationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankTranscationForm));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGrid = new System.Windows.Forms.TabPage();
            this.dgvBankTranscation = new System.Windows.Forms.DataGridView();
            this.tpEntry = new System.Windows.Forms.TabPage();
            this.panelEntryForm = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxAccountNumber = new System.Windows.Forms.ComboBox();
            this.dtpOnDate = new System.Windows.Forms.DateTimePicker();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.rbDeposit = new System.Windows.Forms.RadioButton();
            this.rbWithdrawal = new System.Windows.Forms.RadioButton();
            this.tpView = new System.Windows.Forms.TabPage();
            this.pdfViewer = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbAccountList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankTranscation)).BeginInit();
            this.tpEntry.SuspendLayout();
            this.panelEntryForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.tpView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(140, 61);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 367);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGrid);
            this.tabControl1.Controls.Add(this.tpEntry);
            this.tabControl1.Controls.Add(this.tpView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(654, 345);
            this.tabControl1.TabIndex = 0;
            // 
            // tpGrid
            // 
            this.tpGrid.Controls.Add(this.dgvBankTranscation);
            this.tpGrid.Location = new System.Drawing.Point(4, 24);
            this.tpGrid.Name = "tpGrid";
            this.tpGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrid.Size = new System.Drawing.Size(646, 317);
            this.tpGrid.TabIndex = 0;
            this.tpGrid.Text = "Transcations";
            this.tpGrid.UseVisualStyleBackColor = true;
            // 
            // dgvBankTranscation
            // 
            this.dgvBankTranscation.AllowUserToAddRows = false;
            this.dgvBankTranscation.AllowUserToOrderColumns = true;
            this.dgvBankTranscation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBankTranscation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBankTranscation.Location = new System.Drawing.Point(3, 3);
            this.dgvBankTranscation.Name = "dgvBankTranscation";
            this.dgvBankTranscation.ReadOnly = true;
            this.dgvBankTranscation.RowTemplate.Height = 25;
            this.dgvBankTranscation.Size = new System.Drawing.Size(640, 311);
            this.dgvBankTranscation.TabIndex = 0;
            this.dgvBankTranscation.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBankTranscation_CellContentClick);
            // 
            // tpEntry
            // 
            this.tpEntry.Controls.Add(this.panelEntryForm);
            this.tpEntry.Location = new System.Drawing.Point(4, 24);
            this.tpEntry.Name = "tpEntry";
            this.tpEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntry.Size = new System.Drawing.Size(646, 317);
            this.tpEntry.TabIndex = 1;
            this.tpEntry.Text = "Enrty Form";
            this.tpEntry.UseVisualStyleBackColor = true;
            // 
            // panelEntryForm
            // 
            this.panelEntryForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelEntryForm.Controls.Add(this.tableLayoutPanel1);
            this.panelEntryForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEntryForm.Location = new System.Drawing.Point(3, 3);
            this.panelEntryForm.Name = "panelEntryForm";
            this.panelEntryForm.Size = new System.Drawing.Size(640, 311);
            this.panelEntryForm.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbxAccountNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpOnDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtChequeNo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtNaration, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxMode, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.nudAmount, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.rbDeposit, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.rbWithdrawal, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(640, 311);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cheque No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Naration";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Amount";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Transcation Type";
            // 
            // cbxAccountNumber
            // 
            this.cbxAccountNumber.FormattingEnabled = true;
            this.cbxAccountNumber.Location = new System.Drawing.Point(108, 3);
            this.cbxAccountNumber.Name = "cbxAccountNumber";
            this.cbxAccountNumber.Size = new System.Drawing.Size(121, 23);
            this.cbxAccountNumber.TabIndex = 7;
            // 
            // dtpOnDate
            // 
            this.dtpOnDate.Location = new System.Drawing.Point(326, 3);
            this.dtpOnDate.Name = "dtpOnDate";
            this.dtpOnDate.Size = new System.Drawing.Size(200, 23);
            this.dtpOnDate.TabIndex = 8;
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(108, 32);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(121, 23);
            this.txtChequeNo.TabIndex = 9;
            // 
            // txtNaration
            // 
            this.txtNaration.Location = new System.Drawing.Point(326, 32);
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(200, 23);
            this.txtNaration.TabIndex = 10;
            // 
            // cbxMode
            // 
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Location = new System.Drawing.Point(108, 61);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(121, 23);
            this.cbxMode.TabIndex = 11;
            // 
            // nudAmount
            // 
            this.nudAmount.DecimalPlaces = 2;
            this.nudAmount.Increment = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.nudAmount.Location = new System.Drawing.Point(326, 61);
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(120, 23);
            this.nudAmount.TabIndex = 12;
            this.nudAmount.ThousandsSeparator = true;
            // 
            // rbDeposit
            // 
            this.rbDeposit.AutoSize = true;
            this.rbDeposit.Location = new System.Drawing.Point(108, 90);
            this.rbDeposit.Name = "rbDeposit";
            this.rbDeposit.Size = new System.Drawing.Size(65, 19);
            this.rbDeposit.TabIndex = 13;
            this.rbDeposit.TabStop = true;
            this.rbDeposit.Text = "Deposit";
            this.rbDeposit.UseVisualStyleBackColor = true;
            // 
            // rbWithdrawal
            // 
            this.rbWithdrawal.AutoSize = true;
            this.rbWithdrawal.Location = new System.Drawing.Point(235, 90);
            this.rbWithdrawal.Name = "rbWithdrawal";
            this.rbWithdrawal.Size = new System.Drawing.Size(85, 19);
            this.rbWithdrawal.TabIndex = 14;
            this.rbWithdrawal.TabStop = true;
            this.rbWithdrawal.Text = "Withdrawal";
            this.rbWithdrawal.UseVisualStyleBackColor = true;
            // 
            // tpView
            // 
            this.tpView.Controls.Add(this.pdfViewer);
            this.tpView.Location = new System.Drawing.Point(4, 24);
            this.tpView.Name = "tpView";
            this.tpView.Size = new System.Drawing.Size(646, 317);
            this.tpView.TabIndex = 2;
            this.tpView.Text = "Reports";
            this.tpView.UseVisualStyleBackColor = true;
            // 
            // pdfViewer
            // 
            this.pdfViewer.AllowDrop = true;
            this.pdfViewer.AutoScroll = true;
            this.pdfViewer.AutoSize = true;
            this.pdfViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.pdfViewer.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.EnableContextMenu = true;
            this.pdfViewer.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.pdfViewer.HorizontalScrollOffset = 0;
            this.pdfViewer.IsTextSearchEnabled = true;
            this.pdfViewer.IsTextSelectionEnabled = true;
            this.pdfViewer.Location = new System.Drawing.Point(0, 0);
            messageBoxSettings1.EnableNotification = true;
            this.pdfViewer.MessageBoxSettings = messageBoxSettings1;
            this.pdfViewer.MinimumZoomPercentage = 50;
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            this.pdfViewer.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfViewer.ReferencePath = null;
            this.pdfViewer.ScrollDisplacementValue = 0;
            this.pdfViewer.ShowHorizontalScrollBar = true;
            this.pdfViewer.ShowVerticalScrollBar = true;
            this.pdfViewer.Size = new System.Drawing.Size(646, 317);
            this.pdfViewer.SpaceBetweenPages = 8;
            this.pdfViewer.TabIndex = 0;
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfViewer.TextSearchSettings = textSearchSettings1;
            this.pdfViewer.ThemeName = "Default";
            this.pdfViewer.VerticalScrollOffset = 0;
            this.pdfViewer.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfViewer.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbAccountList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 367);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account List";
            // 
            // lbAccountList
            // 
            this.lbAccountList.DisplayMember = "Account Number";
            this.lbAccountList.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbAccountList.FormattingEnabled = true;
            this.lbAccountList.ItemHeight = 15;
            this.lbAccountList.Location = new System.Drawing.Point(3, 19);
            this.lbAccountList.Name = "lbAccountList";
            this.lbAccountList.Size = new System.Drawing.Size(125, 345);
            this.lbAccountList.TabIndex = 0;
            this.lbAccountList.ValueMember = "Account Number";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnCancle);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(794, 39);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(84, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(165, 3);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 17;
            this.btnCancle.Text = "Cancle";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(246, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.radioButton3);
            this.flowLayoutPanel2.Controls.Add(this.radioButton4);
            this.flowLayoutPanel2.Controls.Add(this.radioButton5);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(327, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(401, 23);
            this.flowLayoutPanel2.TabIndex = 19;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(3, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(104, 19);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Current Month";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(113, 3);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(85, 19);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "Last Month";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(204, 3);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(90, 19);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.Text = "Current Year";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // BankTranscationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "BankTranscationForm";
            this.Text = "Bank Transcation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BankTranscationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankTranscation)).EndInit();
            this.tpEntry.ResumeLayout(false);
            this.panelEntryForm.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.tpView.ResumeLayout(false);
            this.tpView.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox3;
        private TabControl tabControl1;
        private TabPage tpGrid;
        private DataGridView dgvBankTranscation;
        private TabPage tpEntry;
        private Panel panelEntryForm;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cbxAccountNumber;
        private DateTimePicker dtpOnDate;
        private TextBox txtChequeNo;
        private TextBox txtNaration;
        private ComboBox cbxMode;
        private NumericUpDown nudAmount;
        private RadioButton rbDeposit;
        private RadioButton rbWithdrawal;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnCancle;
        private Button btnPrint;
        private TabPage tpView;
        private Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView pdfViewer;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private StatusStrip statusStrip1;
        private ListBox lbAccountList;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
    }
}