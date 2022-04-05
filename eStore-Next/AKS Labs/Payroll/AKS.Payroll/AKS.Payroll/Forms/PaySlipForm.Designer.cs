﻿namespace AKS.Payroll.Forms
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbEmployees = new System.Windows.Forms.ListBox();
            this.dgvPayslips = new System.Windows.Forms.DataGridView();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSelectedEmployee = new System.Windows.Forms.Button();
            this.btnProcessAll = new System.Windows.Forms.Button();
            this.btnPrintPayslip = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayslips)).BeginInit();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 521);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrintPayslip);
            this.groupBox1.Controls.Add(this.btnProcessAll);
            this.groupBox1.Controls.Add(this.btnSelectedEmployee);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(897, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmployees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 447);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvPayslips);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(175, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(722, 447);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payslip List";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCount,
            this.tsslCountValue});
            this.statusStrip1.Location = new System.Drawing.Point(175, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(722, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCount
            // 
            this.tsslCount.Name = "tsslCount";
            this.tsslCount.Size = new System.Drawing.Size(48, 20);
            this.tsslCount.Text = "Count";
            // 
            // tsslCountValue
            // 
            this.tsslCountValue.Name = "tsslCountValue";
            this.tsslCountValue.Size = new System.Drawing.Size(17, 20);
            this.tsslCountValue.Text = "0";
            // 
            // lbEmployees
            // 
            this.lbEmployees.DisplayMember = "StaffName";
            this.lbEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmployees.FormattingEnabled = true;
            this.lbEmployees.ItemHeight = 20;
            this.lbEmployees.Location = new System.Drawing.Point(3, 23);
            this.lbEmployees.Name = "lbEmployees";
            this.lbEmployees.Size = new System.Drawing.Size(169, 421);
            this.lbEmployees.TabIndex = 0;
            this.lbEmployees.ValueMember = "EmployeeId";
            // 
            // dgvPayslips
            // 
            this.dgvPayslips.AllowUserToAddRows = false;
            this.dgvPayslips.AllowUserToDeleteRows = false;
            this.dgvPayslips.AllowUserToOrderColumns = true;
            this.dgvPayslips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayslips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayslips.Location = new System.Drawing.Point(3, 23);
            this.dgvPayslips.Name = "dgvPayslips";
            this.dgvPayslips.ReadOnly = true;
            this.dgvPayslips.RowHeadersWidth = 51;
            this.dgvPayslips.RowTemplate.Height = 29;
            this.dgvPayslips.Size = new System.Drawing.Size(716, 421);
            this.dgvPayslips.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.AutoSize = true;
            this.btnGenerate.Location = new System.Drawing.Point(60, 30);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(114, 30);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Current Month";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSelectedEmployee
            // 
            this.btnSelectedEmployee.AutoSize = true;
            this.btnSelectedEmployee.Location = new System.Drawing.Point(174, 30);
            this.btnSelectedEmployee.Name = "btnSelectedEmployee";
            this.btnSelectedEmployee.Size = new System.Drawing.Size(146, 30);
            this.btnSelectedEmployee.TabIndex = 1;
            this.btnSelectedEmployee.Text = "Selected Employee";
            this.btnSelectedEmployee.UseVisualStyleBackColor = true;
            this.btnSelectedEmployee.Click += new System.EventHandler(this.btnSelectedEmployee_Click);
            // 
            // btnProcessAll
            // 
            this.btnProcessAll.AutoSize = true;
            this.btnProcessAll.Enabled = false;
            this.btnProcessAll.Location = new System.Drawing.Point(320, 30);
            this.btnProcessAll.Name = "btnProcessAll";
            this.btnProcessAll.Size = new System.Drawing.Size(94, 30);
            this.btnProcessAll.TabIndex = 2;
            this.btnProcessAll.Text = "Process All";
            this.btnProcessAll.UseVisualStyleBackColor = true;
            // 
            // btnPrintPayslip
            // 
            this.btnPrintPayslip.AutoSize = true;
            this.btnPrintPayslip.Location = new System.Drawing.Point(414, 30);
            this.btnPrintPayslip.Name = "btnPrintPayslip";
            this.btnPrintPayslip.Size = new System.Drawing.Size(98, 30);
            this.btnPrintPayslip.TabIndex = 3;
            this.btnPrintPayslip.Text = "Print Payslip";
            this.btnPrintPayslip.UseVisualStyleBackColor = true;
            // 
            // PaySlipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 521);
            this.Controls.Add(this.panel1);
            this.Name = "PaySlipForm";
            this.Text = "Payslip";
            this.Load += new System.EventHandler(this.PaySlipForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayslips)).EndInit();
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