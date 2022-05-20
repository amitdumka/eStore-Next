namespace AKS.Payroll
{
    partial class SalaryPaymentForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tcSalaryPayments = new System.Windows.Forms.TabControl();
            this.tpPayment = new System.Windows.Forms.TabPage();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.tpReciepts = new System.Windows.Forms.TabPage();
            this.dgvReceipts = new System.Windows.Forms.DataGridView();
            this.tpReports = new System.Windows.Forms.TabPage();
            this.dgvSalaryLedger = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbEmoloyees = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAllEmployees = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddReciept = new System.Windows.Forms.Button();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.btnProcessLedger = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tcSalaryPayments.SuspendLayout();
            this.tpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.tpReciepts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
            this.tpReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalaryLedger)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tcSalaryPayments);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 438);
            this.panel1.TabIndex = 0;
            // 
            // tcSalaryPayments
            // 
            this.tcSalaryPayments.Controls.Add(this.tpPayment);
            this.tcSalaryPayments.Controls.Add(this.tpReciepts);
            this.tcSalaryPayments.Controls.Add(this.tpReports);
            this.tcSalaryPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSalaryPayments.Location = new System.Drawing.Point(179, 58);
            this.tcSalaryPayments.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tcSalaryPayments.Name = "tcSalaryPayments";
            this.tcSalaryPayments.SelectedIndex = 0;
            this.tcSalaryPayments.Size = new System.Drawing.Size(788, 358);
            this.tcSalaryPayments.TabIndex = 3;
            this.tcSalaryPayments.SelectedIndexChanged += new System.EventHandler(this.tcSalaryPayments_SelectedIndexChanged);
            this.tcSalaryPayments.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcSalaryPayments_Selecting);
            this.tcSalaryPayments.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcSalaryPayments_Selected);
            // 
            // tpPayment
            // 
            this.tpPayment.Controls.Add(this.dgvPayments);
            this.tpPayment.Location = new System.Drawing.Point(4, 24);
            this.tpPayment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpPayment.Name = "tpPayment";
            this.tpPayment.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpPayment.Size = new System.Drawing.Size(780, 330);
            this.tpPayment.TabIndex = 0;
            this.tpPayment.Text = "Payment";
            this.tpPayment.UseVisualStyleBackColor = true;
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            this.dgvPayments.AllowUserToOrderColumns = true;
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPayments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.Location = new System.Drawing.Point(3, 2);
            this.dgvPayments.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvPayments.RowTemplate.Height = 29;
            this.dgvPayments.Size = new System.Drawing.Size(774, 326);
            this.dgvPayments.TabIndex = 0;
            this.dgvPayments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPayments_CellContentClick);
            // 
            // tpReciepts
            // 
            this.tpReciepts.Controls.Add(this.dgvReceipts);
            this.tpReciepts.Location = new System.Drawing.Point(4, 24);
            this.tpReciepts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpReciepts.Name = "tpReciepts";
            this.tpReciepts.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpReciepts.Size = new System.Drawing.Size(780, 330);
            this.tpReciepts.TabIndex = 1;
            this.tpReciepts.Text = "Reciepts";
            this.tpReciepts.UseVisualStyleBackColor = true;
            // 
            // dgvReceipts
            // 
            this.dgvReceipts.AllowUserToAddRows = false;
            this.dgvReceipts.AllowUserToDeleteRows = false;
            this.dgvReceipts.AllowUserToOrderColumns = true;
            this.dgvReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceipts.Location = new System.Drawing.Point(3, 2);
            this.dgvReceipts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvReceipts.Name = "dgvReceipts";
            this.dgvReceipts.ReadOnly = true;
            this.dgvReceipts.RowHeadersWidth = 51;
            this.dgvReceipts.RowTemplate.Height = 29;
            this.dgvReceipts.Size = new System.Drawing.Size(774, 326);
            this.dgvReceipts.TabIndex = 0;
            this.dgvReceipts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipts_CellContentClick);
            // 
            // tpReports
            // 
            this.tpReports.Controls.Add(this.dgvSalaryLedger);
            this.tpReports.Location = new System.Drawing.Point(4, 24);
            this.tpReports.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpReports.Name = "tpReports";
            this.tpReports.Size = new System.Drawing.Size(780, 330);
            this.tpReports.TabIndex = 2;
            this.tpReports.Text = "Reports";
            this.tpReports.UseVisualStyleBackColor = true;
            // 
            // dgvSalaryLedger
            // 
            this.dgvSalaryLedger.AllowUserToAddRows = false;
            this.dgvSalaryLedger.AllowUserToDeleteRows = false;
            this.dgvSalaryLedger.AllowUserToOrderColumns = true;
            this.dgvSalaryLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalaryLedger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalaryLedger.Location = new System.Drawing.Point(0, 0);
            this.dgvSalaryLedger.Name = "dgvSalaryLedger";
            this.dgvSalaryLedger.ReadOnly = true;
            this.dgvSalaryLedger.RowTemplate.Height = 25;
            this.dgvSalaryLedger.Size = new System.Drawing.Size(780, 330);
            this.dgvSalaryLedger.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(179, 416);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(788, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel1.Text = "Count";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel2.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmoloyees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 58);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(179, 380);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee List";
            // 
            // lbEmoloyees
            // 
            this.lbEmoloyees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmoloyees.FormattingEnabled = true;
            this.lbEmoloyees.ItemHeight = 15;
            this.lbEmoloyees.Location = new System.Drawing.Point(3, 18);
            this.lbEmoloyees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbEmoloyees.Name = "lbEmoloyees";
            this.lbEmoloyees.Size = new System.Drawing.Size(173, 360);
            this.lbEmoloyees.TabIndex = 0;
            this.lbEmoloyees.SelectedIndexChanged += new System.EventHandler(this.lbEmoloyees_SelectedIndexChanged);
            this.lbEmoloyees.DoubleClick += new System.EventHandler(this.lbEmoloyees_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProcessLedger);
            this.groupBox1.Controls.Add(this.cbAllEmployees);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnAddReciept);
            this.groupBox1.Controls.Add(this.btnAddPayment);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(967, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // cbAllEmployees
            // 
            this.cbAllEmployees.AutoSize = true;
            this.cbAllEmployees.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAllEmployees.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbAllEmployees.Location = new System.Drawing.Point(864, 18);
            this.cbAllEmployees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAllEmployees.Name = "cbAllEmployees";
            this.cbAllEmployees.Size = new System.Drawing.Size(100, 38);
            this.cbAllEmployees.TabIndex = 3;
            this.cbAllEmployees.Text = "All Empolyees";
            this.cbAllEmployees.UseVisualStyleBackColor = true;
            this.cbAllEmployees.CheckStateChanged += new System.EventHandler(this.cbAllEmployees_CheckStateChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(234, 22);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(82, 22);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddReciept
            // 
            this.btnAddReciept.Location = new System.Drawing.Point(146, 22);
            this.btnAddReciept.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddReciept.Name = "btnAddReciept";
            this.btnAddReciept.Size = new System.Drawing.Size(82, 22);
            this.btnAddReciept.TabIndex = 1;
            this.btnAddReciept.Text = "Reciept";
            this.btnAddReciept.UseVisualStyleBackColor = true;
            this.btnAddReciept.Click += new System.EventHandler(this.btnAddReciept_Click);
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Location = new System.Drawing.Point(58, 22);
            this.btnAddPayment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(82, 22);
            this.btnAddPayment.TabIndex = 0;
            this.btnAddPayment.Text = "Payment";
            this.btnAddPayment.UseVisualStyleBackColor = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // btnProcessLedger
            // 
            this.btnProcessLedger.AutoSize = true;
            this.btnProcessLedger.Location = new System.Drawing.Point(322, 22);
            this.btnProcessLedger.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProcessLedger.Name = "btnProcessLedger";
            this.btnProcessLedger.Size = new System.Drawing.Size(96, 25);
            this.btnProcessLedger.TabIndex = 4;
            this.btnProcessLedger.Text = "Process Ledger";
            this.btnProcessLedger.UseVisualStyleBackColor = true;
            this.btnProcessLedger.Click += new System.EventHandler(this.btnProcessLedger_Click);
            // 
            // SalaryPaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 438);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SalaryPaymentForm";
            this.Text = "Salary Payment/Receipts";
            this.Load += new System.EventHandler(this.SalaryPaymentForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tcSalaryPayments.ResumeLayout(false);
            this.tpPayment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.tpReciepts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).EndInit();
            this.tpReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalaryLedger)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private TabControl tcSalaryPayments;
        private TabPage tpPayment;
        private TabPage tpReciepts;
        private TabPage tpReports;
        private DataGridView dgvPayments;
        private DataGridView dgvReceipts;
        private ListBox lbEmoloyees;
        private Button btnAddPayment;
        private Button btnAddReciept;
        private Button btnRefresh;
        private CheckBox cbAllEmployees;
        private DataGridView dgvSalaryLedger;
        private Button btnProcessLedger;
    }
}