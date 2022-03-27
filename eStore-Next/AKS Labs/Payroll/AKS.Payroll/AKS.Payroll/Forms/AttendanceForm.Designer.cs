namespace AKS.Payroll.Forms
{
    partial class AttendanceForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSSLCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSLCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvAttendances = new System.Windows.Forms.DataGridView();
            this.attendanceIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isTailoringDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.storeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storeCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReadOnlyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.attendanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbEmployees = new System.Windows.Forms.ListBox();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1140, 573);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1140, 573);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1132, 540);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Attendances";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLCount,
            this.tSSLCountValue});
            this.statusStrip1.Location = new System.Drawing.Point(137, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(992, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tSSLCount
            // 
            this.tSSLCount.Name = "tSSLCount";
            this.tSSLCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tSSLCount.Size = new System.Drawing.Size(48, 20);
            this.tSSLCount.Text = "Count";
            // 
            // tSSLCountValue
            // 
            this.tSSLCountValue.Name = "tSSLCountValue";
            this.tSSLCountValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tSSLCountValue.Size = new System.Drawing.Size(17, 20);
            this.tSSLCountValue.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvAttendances);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(137, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(992, 460);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attendances";
            // 
            // dgvAttendances
            // 
            this.dgvAttendances.AutoGenerateColumns = false;
            this.dgvAttendances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendances.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.attendanceIdDataGridViewTextBoxColumn,
            this.employeeIdDataGridViewTextBoxColumn,
            this.empIdDataGridViewTextBoxColumn,
            this.onDateDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.entryTimeDataGridViewTextBoxColumn,
            this.remarksDataGridViewTextBoxColumn,
            this.isTailoringDataGridViewCheckBoxColumn,
            this.storeIdDataGridViewTextBoxColumn,
            this.storeCodeDataGridViewTextBoxColumn,
            this.entryStatusDataGridViewTextBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.isReadOnlyDataGridViewCheckBoxColumn});
            this.dgvAttendances.DataSource = this.attendanceBindingSource;
            this.dgvAttendances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttendances.Location = new System.Drawing.Point(3, 23);
            this.dgvAttendances.Name = "dgvAttendances";
            this.dgvAttendances.RowHeadersWidth = 51;
            this.dgvAttendances.RowTemplate.Height = 29;
            this.dgvAttendances.Size = new System.Drawing.Size(986, 434);
            this.dgvAttendances.TabIndex = 0;
            // 
            // attendanceIdDataGridViewTextBoxColumn
            // 
            this.attendanceIdDataGridViewTextBoxColumn.DataPropertyName = "AttendanceId";
            this.attendanceIdDataGridViewTextBoxColumn.HeaderText = "AttendanceId";
            this.attendanceIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.attendanceIdDataGridViewTextBoxColumn.Name = "attendanceIdDataGridViewTextBoxColumn";
            this.attendanceIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // employeeIdDataGridViewTextBoxColumn
            // 
            this.employeeIdDataGridViewTextBoxColumn.DataPropertyName = "EmployeeId";
            this.employeeIdDataGridViewTextBoxColumn.HeaderText = "EmployeeId";
            this.employeeIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.employeeIdDataGridViewTextBoxColumn.Name = "employeeIdDataGridViewTextBoxColumn";
            this.employeeIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // empIdDataGridViewTextBoxColumn
            // 
            this.empIdDataGridViewTextBoxColumn.DataPropertyName = "EmpId";
            this.empIdDataGridViewTextBoxColumn.HeaderText = "EmpId";
            this.empIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.empIdDataGridViewTextBoxColumn.Name = "empIdDataGridViewTextBoxColumn";
            this.empIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // onDateDataGridViewTextBoxColumn
            // 
            this.onDateDataGridViewTextBoxColumn.DataPropertyName = "OnDate";
            this.onDateDataGridViewTextBoxColumn.HeaderText = "OnDate";
            this.onDateDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.onDateDataGridViewTextBoxColumn.Name = "onDateDataGridViewTextBoxColumn";
            this.onDateDataGridViewTextBoxColumn.Width = 125;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.Width = 125;
            // 
            // entryTimeDataGridViewTextBoxColumn
            // 
            this.entryTimeDataGridViewTextBoxColumn.DataPropertyName = "EntryTime";
            this.entryTimeDataGridViewTextBoxColumn.HeaderText = "EntryTime";
            this.entryTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.entryTimeDataGridViewTextBoxColumn.Name = "entryTimeDataGridViewTextBoxColumn";
            this.entryTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // remarksDataGridViewTextBoxColumn
            // 
            this.remarksDataGridViewTextBoxColumn.DataPropertyName = "Remarks";
            this.remarksDataGridViewTextBoxColumn.HeaderText = "Remarks";
            this.remarksDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.remarksDataGridViewTextBoxColumn.Name = "remarksDataGridViewTextBoxColumn";
            this.remarksDataGridViewTextBoxColumn.Width = 125;
            // 
            // isTailoringDataGridViewCheckBoxColumn
            // 
            this.isTailoringDataGridViewCheckBoxColumn.DataPropertyName = "IsTailoring";
            this.isTailoringDataGridViewCheckBoxColumn.HeaderText = "IsTailoring";
            this.isTailoringDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.isTailoringDataGridViewCheckBoxColumn.Name = "isTailoringDataGridViewCheckBoxColumn";
            this.isTailoringDataGridViewCheckBoxColumn.Width = 125;
            // 
            // storeIdDataGridViewTextBoxColumn
            // 
            this.storeIdDataGridViewTextBoxColumn.DataPropertyName = "StoreId";
            this.storeIdDataGridViewTextBoxColumn.HeaderText = "StoreId";
            this.storeIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.storeIdDataGridViewTextBoxColumn.Name = "storeIdDataGridViewTextBoxColumn";
            this.storeIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // storeCodeDataGridViewTextBoxColumn
            // 
            this.storeCodeDataGridViewTextBoxColumn.DataPropertyName = "StoreCode";
            this.storeCodeDataGridViewTextBoxColumn.HeaderText = "StoreCode";
            this.storeCodeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.storeCodeDataGridViewTextBoxColumn.Name = "storeCodeDataGridViewTextBoxColumn";
            this.storeCodeDataGridViewTextBoxColumn.Width = 125;
            // 
            // entryStatusDataGridViewTextBoxColumn
            // 
            this.entryStatusDataGridViewTextBoxColumn.DataPropertyName = "EntryStatus";
            this.entryStatusDataGridViewTextBoxColumn.HeaderText = "EntryStatus";
            this.entryStatusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.entryStatusDataGridViewTextBoxColumn.Name = "entryStatusDataGridViewTextBoxColumn";
            this.entryStatusDataGridViewTextBoxColumn.Width = 125;
            // 
            // userIdDataGridViewTextBoxColumn
            // 
            this.userIdDataGridViewTextBoxColumn.DataPropertyName = "UserId";
            this.userIdDataGridViewTextBoxColumn.HeaderText = "UserId";
            this.userIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.userIdDataGridViewTextBoxColumn.Name = "userIdDataGridViewTextBoxColumn";
            this.userIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // isReadOnlyDataGridViewCheckBoxColumn
            // 
            this.isReadOnlyDataGridViewCheckBoxColumn.DataPropertyName = "IsReadOnly";
            this.isReadOnlyDataGridViewCheckBoxColumn.HeaderText = "IsReadOnly";
            this.isReadOnlyDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.isReadOnlyDataGridViewCheckBoxColumn.Name = "isReadOnlyDataGridViewCheckBoxColumn";
            this.isReadOnlyDataGridViewCheckBoxColumn.Width = 125;
            // 
            // attendanceBindingSource
            // 
            this.attendanceBindingSource.DataSource = typeof(AKS.Shared.Payroll.Models.Attendance);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbEmployees);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(3, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 460);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employees";
            // 
            // lbEmployees
            // 
            this.lbEmployees.DataSource = this.employeeBindingSource;
            this.lbEmployees.DisplayMember = "StaffName";
            this.lbEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmployees.FormattingEnabled = true;
            this.lbEmployees.ItemHeight = 20;
            this.lbEmployees.Location = new System.Drawing.Point(3, 23);
            this.lbEmployees.Name = "lbEmployees";
            this.lbEmployees.Size = new System.Drawing.Size(128, 434);
            this.lbEmployees.TabIndex = 0;
            this.lbEmployees.ValueMember = "EmployeeId";
            this.lbEmployees.DoubleClick += new System.EventHandler(this.lbEmployees_DoubleClick);
            // 
            // employeeBindingSource
            // 
            this.employeeBindingSource.DataSource = typeof(AKS.Shared.Payroll.Models.Employee);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1126, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage2.Size = new System.Drawing.Size(1132, 540);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Add/Update";
            // 
            // AttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 573);
            this.Controls.Add(this.panel1);
            this.Name = "AttendanceForm";
            this.Text = "Attendance";
            this.Load += new System.EventHandler(this.AttendanceForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ListBox lbEmployees;
        private BindingSource employeeBindingSource;
        private DataGridView dgvAttendances;
        private DataGridViewTextBoxColumn attendanceIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn employeeIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn empIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn onDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn entryTimeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn remarksDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn isTailoringDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn storeIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn storeCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn entryStatusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn isReadOnlyDataGridViewCheckBoxColumn;
        private BindingSource attendanceBindingSource;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tSSLCount;
        private ToolStripStatusLabel tSSLCountValue;
    }
}