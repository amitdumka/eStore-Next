namespace AKS.Payroll.Forms
{
    partial class PaySlipForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvPayslips = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbEmployees = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrintPayslip = new System.Windows.Forms.Button();
            this.btnProcessAll = new System.Windows.Forms.Button();
            this.btnSelectedEmployee = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayslips)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 391);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCount,
            this.tsslCountValue});
            this.statusStrip1.Location = new System.Drawing.Point(153, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCount
            // 
            this.tsslCount.Name = "tsslCount";
            this.tsslCount.Size = new System.Drawing.Size(40, 17);
            this.tsslCount.Text = "Count";
            // 
            // tsslCountValue
            // 
            this.tsslCountValue.Name = "tsslCountValue";
            this.tsslCountValue.Size = new System.Drawing.Size(13, 17);
            this.tsslCountValue.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvPayslips);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(153, 56);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(632, 335);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payslip List";
            // 
            // dgvPayslips
            // 
            this.dgvPayslips.AllowUserToAddRows = false;
            this.dgvPayslips.AllowUserToDeleteRows = false;
            this.dgvPayslips.AllowUserToOrderColumns = true;
            this.dgvPayslips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayslips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayslips.Location = new System.Drawing.Point(3, 18);
            this.dgvPayslips.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPayslips.Name = "dgvPayslips";
            this.dgvPayslips.ReadOnly = true;
            this.dgvPayslips.RowHeadersWidth = 51;
            this.dgvPayslips.RowTemplate.Height = 29;
            this.dgvPayslips.Size = new System.Drawing.Size(626, 315);
            this.dgvPayslips.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmployees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(153, 335);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee List";
            // 
            // lbEmployees
            // 
            this.lbEmployees.DisplayMember = "StaffName";
            this.lbEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmployees.FormattingEnabled = true;
            this.lbEmployees.ItemHeight = 15;
            this.lbEmployees.Location = new System.Drawing.Point(3, 18);
            this.lbEmployees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbEmployees.Name = "lbEmployees";
            this.lbEmployees.Size = new System.Drawing.Size(147, 315);
            this.lbEmployees.TabIndex = 0;
            this.lbEmployees.ValueMember = "EmployeeId";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrintPayslip);
            this.groupBox1.Controls.Add(this.btnProcessAll);
            this.groupBox1.Controls.Add(this.btnSelectedEmployee);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(785, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnPrintPayslip
            // 
            this.btnPrintPayslip.AutoSize = true;
            this.btnPrintPayslip.Location = new System.Drawing.Point(362, 22);
            this.btnPrintPayslip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintPayslip.Name = "btnPrintPayslip";
            this.btnPrintPayslip.Size = new System.Drawing.Size(86, 25);
            this.btnPrintPayslip.TabIndex = 3;
            this.btnPrintPayslip.Text = "Print Payslip";
            this.btnPrintPayslip.UseVisualStyleBackColor = true;
            this.btnPrintPayslip.Click += new System.EventHandler(this.btnPrintPayslip_Click);
            // 
            // btnProcessAll
            // 
            this.btnProcessAll.AutoSize = true;
            this.btnProcessAll.Enabled = false;
            this.btnProcessAll.Location = new System.Drawing.Point(280, 22);
            this.btnProcessAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProcessAll.Name = "btnProcessAll";
            this.btnProcessAll.Size = new System.Drawing.Size(82, 25);
            this.btnProcessAll.TabIndex = 2;
            this.btnProcessAll.Text = "Process All";
            this.btnProcessAll.UseVisualStyleBackColor = true;
            this.btnProcessAll.Click += new System.EventHandler(this.btnProcessAll_Click);
            // 
            // btnSelectedEmployee
            // 
            this.btnSelectedEmployee.AutoSize = true;
            this.btnSelectedEmployee.Location = new System.Drawing.Point(152, 22);
            this.btnSelectedEmployee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectedEmployee.Name = "btnSelectedEmployee";
            this.btnSelectedEmployee.Size = new System.Drawing.Size(128, 25);
            this.btnSelectedEmployee.TabIndex = 1;
            this.btnSelectedEmployee.Text = "Selected Employee";
            this.btnSelectedEmployee.UseVisualStyleBackColor = true;
            this.btnSelectedEmployee.Click += new System.EventHandler(this.btnSelectedEmployee_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.AutoSize = true;
            this.btnGenerate.Location = new System.Drawing.Point(52, 22);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 25);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Current Month";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // PaySlipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 391);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PaySlipForm";
            this.Text = "Payslip";
            this.Load += new System.EventHandler(this.PaySlipForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayslips)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslCount;
        private ToolStripStatusLabel tsslCountValue;
        private ListBox lbEmployees;
        private DataGridView dgvPayslips;
        private Button btnGenerate;
        private Button btnSelectedEmployee;
        private Button btnProcessAll;
        private Button btnPrintPayslip;
    }
}