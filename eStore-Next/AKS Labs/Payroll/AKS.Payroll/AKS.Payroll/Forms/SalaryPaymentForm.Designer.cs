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
            this.panel1.SuspendLayout();
            this.tcSalaryPayments.SuspendLayout();
            this.tpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.tpReciepts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1105, 584);
            this.panel1.TabIndex = 0;
            // 
            // tcSalaryPayments
            // 
            this.tcSalaryPayments.Controls.Add(this.tpPayment);
            this.tcSalaryPayments.Controls.Add(this.tpReciepts);
            this.tcSalaryPayments.Controls.Add(this.tpReports);
            this.tcSalaryPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSalaryPayments.Location = new System.Drawing.Point(205, 77);
            this.tcSalaryPayments.Name = "tcSalaryPayments";
            this.tcSalaryPayments.SelectedIndex = 0;
            this.tcSalaryPayments.Size = new System.Drawing.Size(900, 481);
            this.tcSalaryPayments.TabIndex = 3;
            // 
            // tpPayment
            // 
            this.tpPayment.Controls.Add(this.dgvPayments);
            this.tpPayment.Location = new System.Drawing.Point(4, 29);
            this.tpPayment.Name = "tpPayment";
            this.tpPayment.Padding = new System.Windows.Forms.Padding(3);
            this.tpPayment.Size = new System.Drawing.Size(892, 448);
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
            this.dgvPayments.Location = new System.Drawing.Point(3, 3);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvPayments.RowTemplate.Height = 29;
            this.dgvPayments.Size = new System.Drawing.Size(886, 442);
            this.dgvPayments.TabIndex = 0;
            this.dgvPayments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPayments_CellContentClick);
            // 
            // tpReciepts
            // 
            this.tpReciepts.Controls.Add(this.dgvReceipts);
            this.tpReciepts.Location = new System.Drawing.Point(4, 29);
            this.tpReciepts.Name = "tpReciepts";
            this.tpReciepts.Padding = new System.Windows.Forms.Padding(3);
            this.tpReciepts.Size = new System.Drawing.Size(892, 448);
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
            this.dgvReceipts.Location = new System.Drawing.Point(3, 3);
            this.dgvReceipts.Name = "dgvReceipts";
            this.dgvReceipts.ReadOnly = true;
            this.dgvReceipts.RowHeadersWidth = 51;
            this.dgvReceipts.RowTemplate.Height = 29;
            this.dgvReceipts.Size = new System.Drawing.Size(886, 442);
            this.dgvReceipts.TabIndex = 0;
            // 
            // tpReports
            // 
            this.tpReports.Location = new System.Drawing.Point(4, 29);
            this.tpReports.Name = "tpReports";
            this.tpReports.Size = new System.Drawing.Size(892, 448);
            this.tpReports.TabIndex = 2;
            this.tpReports.Text = "Reports";
            this.tpReports.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(205, 558);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 20);
            this.toolStripStatusLabel1.Text = "Count";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(17, 20);
            this.toolStripStatusLabel2.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmoloyees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 507);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee List";
            // 
            // lbEmoloyees
            // 
            this.lbEmoloyees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmoloyees.FormattingEnabled = true;
            this.lbEmoloyees.ItemHeight = 20;
            this.lbEmoloyees.Location = new System.Drawing.Point(3, 23);
            this.lbEmoloyees.Name = "lbEmoloyees";
            this.lbEmoloyees.Size = new System.Drawing.Size(199, 481);
            this.lbEmoloyees.TabIndex = 0;
            this.lbEmoloyees.DoubleClick += new System.EventHandler(this.lbEmoloyees_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAllEmployees);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnAddReciept);
            this.groupBox1.Controls.Add(this.btnAddPayment);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1105, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // cbAllEmployees
            // 
            this.cbAllEmployees.AutoSize = true;
            this.cbAllEmployees.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAllEmployees.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbAllEmployees.Location = new System.Drawing.Point(977, 23);
            this.cbAllEmployees.Name = "cbAllEmployees";
            this.cbAllEmployees.Size = new System.Drawing.Size(125, 51);
            this.cbAllEmployees.TabIndex = 3;
            this.cbAllEmployees.Text = "All Empolyees";
            this.cbAllEmployees.UseVisualStyleBackColor = true;
            this.cbAllEmployees.CheckStateChanged += new System.EventHandler(this.cbAllEmployees_CheckStateChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(294, 29);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 29);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddReciept
            // 
            this.btnAddReciept.Location = new System.Drawing.Point(180, 29);
            this.btnAddReciept.Name = "btnAddReciept";
            this.btnAddReciept.Size = new System.Drawing.Size(94, 29);
            this.btnAddReciept.TabIndex = 1;
            this.btnAddReciept.Text = "Reciept";
            this.btnAddReciept.UseVisualStyleBackColor = true;
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Location = new System.Drawing.Point(66, 29);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(94, 29);
            this.btnAddPayment.TabIndex = 0;
            this.btnAddPayment.Text = "Payment";
            this.btnAddPayment.UseVisualStyleBackColor = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // SalaryPaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 584);
            this.Controls.Add(this.panel1);
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
    }
}