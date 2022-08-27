namespace AKS.Payroll.Forms
{
    partial class SalaryForm
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
            this.tsslCountVauue = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tcSalaryHead = new System.Windows.Forms.TabControl();
            this.tpSalaryHead = new System.Windows.Forms.TabPage();
            this.dgvSalaries = new System.Windows.Forms.DataGridView();
            this.tpReports = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbEmployees = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAddSalaryHead = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tcSalaryHead.SuspendLayout();
            this.tpSalaryHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalaries)).BeginInit();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(958, 583);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCount,
            this.tsslCountVauue});
            this.statusStrip1.Location = new System.Drawing.Point(147, 557);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(811, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCount
            // 
            this.tsslCount.Name = "tsslCount";
            this.tsslCount.Size = new System.Drawing.Size(48, 20);
            this.tsslCount.Text = "Count";
            // 
            // tsslCountVauue
            // 
            this.tsslCountVauue.Name = "tsslCountVauue";
            this.tsslCountVauue.Size = new System.Drawing.Size(17, 20);
            this.tsslCountVauue.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tcSalaryHead);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(147, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(811, 502);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Salaries";
            // 
            // tcSalaryHead
            // 
            this.tcSalaryHead.Controls.Add(this.tpSalaryHead);
            this.tcSalaryHead.Controls.Add(this.tpReports);
            this.tcSalaryHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSalaryHead.Location = new System.Drawing.Point(3, 23);
            this.tcSalaryHead.Name = "tcSalaryHead";
            this.tcSalaryHead.SelectedIndex = 0;
            this.tcSalaryHead.Size = new System.Drawing.Size(805, 476);
            this.tcSalaryHead.TabIndex = 0;
            // 
            // tpSalaryHead
            // 
            this.tpSalaryHead.Controls.Add(this.dgvSalaries);
            this.tpSalaryHead.Location = new System.Drawing.Point(4, 29);
            this.tpSalaryHead.Name = "tpSalaryHead";
            this.tpSalaryHead.Padding = new System.Windows.Forms.Padding(3);
            this.tpSalaryHead.Size = new System.Drawing.Size(797, 443);
            this.tpSalaryHead.TabIndex = 0;
            this.tpSalaryHead.Text = "Salary";
            this.tpSalaryHead.UseVisualStyleBackColor = true;
            // 
            // dgvSalaries
            // 
            this.dgvSalaries.AllowUserToAddRows = false;
            this.dgvSalaries.AllowUserToDeleteRows = false;
            this.dgvSalaries.AllowUserToOrderColumns = true;
            this.dgvSalaries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalaries.Location = new System.Drawing.Point(3, 3);
            this.dgvSalaries.Name = "dgvSalaries";
            this.dgvSalaries.ReadOnly = true;
            this.dgvSalaries.RowHeadersWidth = 51;
            this.dgvSalaries.RowTemplate.Height = 29;
            this.dgvSalaries.Size = new System.Drawing.Size(791, 437);
            this.dgvSalaries.TabIndex = 0;
            this.dgvSalaries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalaries_CellContentClick);
            // 
            // tpReports
            // 
            this.tpReports.Location = new System.Drawing.Point(4, 29);
            this.tpReports.Name = "tpReports";
            this.tpReports.Padding = new System.Windows.Forms.Padding(3);
            this.tpReports.Size = new System.Drawing.Size(797, 443);
            this.tpReports.TabIndex = 1;
            this.tpReports.Text = "Reports";
            this.tpReports.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmployees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 502);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employees List";
            // 
            // lbEmployees
            // 
            this.lbEmployees.DisplayMember = "StaffName";
            this.lbEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmployees.FormattingEnabled = true;
            this.lbEmployees.ItemHeight = 20;
            this.lbEmployees.Location = new System.Drawing.Point(3, 23);
            this.lbEmployees.Name = "lbEmployees";
            this.lbEmployees.Size = new System.Drawing.Size(141, 476);
            this.lbEmployees.TabIndex = 0;
            this.lbEmployees.ValueMember = "EmployeeId";
            this.lbEmployees.SelectedIndexChanged += new System.EventHandler(this.lbEmployees_SelectedIndexChanged);
            this.lbEmployees.DoubleClick += new System.EventHandler(this.lbEmployees_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnAddSalaryHead);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(958, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(346, 34);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 29);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(202, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnAddSalaryHead
            // 
            this.btnAddSalaryHead.Location = new System.Drawing.Point(58, 34);
            this.btnAddSalaryHead.Name = "btnAddSalaryHead";
            this.btnAddSalaryHead.Size = new System.Drawing.Size(94, 29);
            this.btnAddSalaryHead.TabIndex = 0;
            this.btnAddSalaryHead.Text = "Add";
            this.btnAddSalaryHead.UseVisualStyleBackColor = true;
            this.btnAddSalaryHead.Click += new System.EventHandler(this.btnAddSalaryHead_Click);
            // 
            // SalaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 583);
            this.Controls.Add(this.panel1);
            this.Name = "SalaryForm";
            this.Text = "SalaryForm";
            this.Load += new System.EventHandler(this.SalaryForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tcSalaryHead.ResumeLayout(false);
            this.tpSalaryHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalaries)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslCount;
        private ToolStripStatusLabel tsslCountVauue;
        private Button btnAddSalaryHead;
        private Button button2;
        private Button btnRefresh;
        private ListBox lbEmployees;
        private TabControl tcSalaryHead;
        private TabPage tpSalaryHead;
        private TabPage tpReports;
        private DataGridView dgvSalaries;
    }
}