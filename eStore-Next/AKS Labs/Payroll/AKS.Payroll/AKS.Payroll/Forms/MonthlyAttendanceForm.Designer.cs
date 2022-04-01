namespace AKS.Payroll.Forms
{
    partial class MonthlyAttendanceForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCountVaue = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvAttendances = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbEmployees = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnProcessAll = new System.Windows.Forms.Button();
            this.btnSelectedEmployee = new System.Windows.Forms.Button();
            this.btnCurrentMonth = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendances)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1119, 529);
            this.panel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.statusStrip1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(152, 461);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(967, 68);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCount,
            this.tsslCountVaue});
            this.statusStrip1.Location = new System.Drawing.Point(3, 39);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(961, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCount
            // 
            this.tsslCount.Name = "tsslCount";
            this.tsslCount.Size = new System.Drawing.Size(48, 20);
            this.tsslCount.Text = "Count";
            // 
            // tsslCountVaue
            // 
            this.tsslCountVaue.Name = "tsslCountVaue";
            this.tsslCountVaue.Size = new System.Drawing.Size(17, 20);
            this.tsslCountVaue.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(152, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(967, 450);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attendances";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(961, 424);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvAttendances);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(953, 391);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Attendances";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvAttendances
            // 
            this.dgvAttendances.AllowUserToAddRows = false;
            this.dgvAttendances.AllowUserToDeleteRows = false;
            this.dgvAttendances.AllowUserToOrderColumns = true;
            this.dgvAttendances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttendances.Location = new System.Drawing.Point(3, 3);
            this.dgvAttendances.Name = "dgvAttendances";
            this.dgvAttendances.ReadOnly = true;
            this.dgvAttendances.RowHeadersWidth = 51;
            this.dgvAttendances.RowTemplate.Height = 29;
            this.dgvAttendances.Size = new System.Drawing.Size(947, 385);
            this.dgvAttendances.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(953, 391);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Reports";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmployees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee List";
            // 
            // lbEmployees
            // 
            this.lbEmployees.DisplayMember = "StaffName";
            this.lbEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmployees.FormattingEnabled = true;
            this.lbEmployees.ItemHeight = 20;
            this.lbEmployees.Location = new System.Drawing.Point(3, 23);
            this.lbEmployees.MinimumSize = new System.Drawing.Size(100, 400);
            this.lbEmployees.Name = "lbEmployees";
            this.lbEmployees.Size = new System.Drawing.Size(146, 424);
            this.lbEmployees.TabIndex = 0;
            this.lbEmployees.ValueMember = "EmployeeId";
            this.lbEmployees.DoubleClick += new System.EventHandler(this.lbEmployees_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProcessAll);
            this.groupBox1.Controls.Add(this.btnSelectedEmployee);
            this.groupBox1.Controls.Add(this.btnCurrentMonth);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1119, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnProcessAll
            // 
            this.btnProcessAll.AutoSize = true;
            this.btnProcessAll.Location = new System.Drawing.Point(524, 36);
            this.btnProcessAll.Name = "btnProcessAll";
            this.btnProcessAll.Size = new System.Drawing.Size(104, 30);
            this.btnProcessAll.TabIndex = 2;
            this.btnProcessAll.Text = "Process All";
            this.btnProcessAll.UseVisualStyleBackColor = true;
            this.btnProcessAll.Click += new System.EventHandler(this.btnProcessAll_Click);
            // 
            // btnSelectedEmployee
            // 
            this.btnSelectedEmployee.AutoSize = true;
            this.btnSelectedEmployee.Location = new System.Drawing.Point(372, 35);
            this.btnSelectedEmployee.Name = "btnSelectedEmployee";
            this.btnSelectedEmployee.Size = new System.Drawing.Size(146, 30);
            this.btnSelectedEmployee.TabIndex = 1;
            this.btnSelectedEmployee.Text = "Selected Employee";
            this.btnSelectedEmployee.UseVisualStyleBackColor = true;
            this.btnSelectedEmployee.Click += new System.EventHandler(this.btnSelectedEmployee_Click);
            // 
            // btnCurrentMonth
            // 
            this.btnCurrentMonth.AutoSize = true;
            this.btnCurrentMonth.Location = new System.Drawing.Point(252, 35);
            this.btnCurrentMonth.Name = "btnCurrentMonth";
            this.btnCurrentMonth.Size = new System.Drawing.Size(114, 30);
            this.btnCurrentMonth.TabIndex = 0;
            this.btnCurrentMonth.Text = "Current Month";
            this.btnCurrentMonth.UseVisualStyleBackColor = true;
            this.btnCurrentMonth.Click += new System.EventHandler(this.btnCurrentMonth_Click);
            // 
            // MonthlyAttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 529);
            this.Controls.Add(this.panel1);
            this.Name = "MonthlyAttendanceForm";
            this.Text = "Monthly Attendance";
            this.Load += new System.EventHandler(this.MonthlyAttendanceForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendances)).EndInit();
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
        private GroupBox groupBox4;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnCurrentMonth;
        private Button btnSelectedEmployee;
        private Button btnProcessAll;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslCount;
        private ToolStripStatusLabel tsslCountVaue;
        private ListBox lbEmployees;
        private DataGridView dgvAttendances;
    }
}