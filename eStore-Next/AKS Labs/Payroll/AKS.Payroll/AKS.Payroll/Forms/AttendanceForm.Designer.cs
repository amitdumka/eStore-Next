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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbEmployees = new System.Windows.Forms.ListBox();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAllEmployee = new System.Windows.Forms.CheckBox();
            this.btnAddAttendance = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendances)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.dgvAttendances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttendances.Location = new System.Drawing.Point(3, 23);
            this.dgvAttendances.Name = "dgvAttendances";
            this.dgvAttendances.RowHeadersWidth = 51;
            this.dgvAttendances.RowTemplate.Height = 29;
            this.dgvAttendances.Size = new System.Drawing.Size(986, 434);
            this.dgvAttendances.TabIndex = 0;
            this.dgvAttendances.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttendances_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.listBox1);
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
            this.lbEmployees.ForeColor = System.Drawing.SystemColors.HotTrack;
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
            this.groupBox1.Controls.Add(this.cbAllEmployee);
            this.groupBox1.Controls.Add(this.btnAddAttendance);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1126, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // cbAllEmployee
            // 
            this.cbAllEmployee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbAllEmployee.AutoSize = true;
            this.cbAllEmployee.BackColor = System.Drawing.Color.Aquamarine;
            this.cbAllEmployee.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAllEmployee.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.cbAllEmployee.ForeColor = System.Drawing.Color.Red;
            this.cbAllEmployee.Location = new System.Drawing.Point(995, 26);
            this.cbAllEmployee.Name = "cbAllEmployee";
            this.cbAllEmployee.Size = new System.Drawing.Size(125, 24);
            this.cbAllEmployee.TabIndex = 3;
            this.cbAllEmployee.Text = "All &Employee";
            this.cbAllEmployee.UseVisualStyleBackColor = false;
            this.cbAllEmployee.CheckStateChanged += new System.EventHandler(this.cbAllEmployee_CheckStateChanged);
            // 
            // btnAddAttendance
            // 
            this.btnAddAttendance.Location = new System.Drawing.Point(72, 33);
            this.btnAddAttendance.Name = "btnAddAttendance";
            this.btnAddAttendance.Size = new System.Drawing.Size(94, 29);
            this.btnAddAttendance.TabIndex = 0;
            this.btnAddAttendance.Text = "Add";
            this.btnAddAttendance.UseVisualStyleBackColor = true;
            this.btnAddAttendance.Click += new System.EventHandler(this.btnAddAttendance_Click);
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
            // listBox1
            // 
            this.listBox1.DataSource = this.employeeBindingSource;
            this.listBox1.DisplayMember = "StaffName";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(3, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(128, 434);
            this.listBox1.TabIndex = 1;
            this.listBox1.ValueMember = "EmployeeId";
            // 
            // listBox2
            // 
            this.listBox2.DataSource = this.employeeBindingSource;
            this.listBox2.DisplayMember = "StaffName";
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(3, 23);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(128, 434);
            this.listBox2.TabIndex = 2;
            this.listBox2.ValueMember = "EmployeeId";
            // 
            // AttendanceForm
            // 
            this.AcceptButton = this.btnAddAttendance;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1140, 573);
            this.Controls.Add(this.panel1);
            this.Name = "AttendanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Attendance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AttendanceForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendances)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private DataGridView dgvAttendances;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tSSLCount;
        private ToolStripStatusLabel tSSLCountValue;
        private Button btnAddAttendance;
        private BindingSource employeeBindingSource;
        private CheckBox cbAllEmployee;
        private ListBox listBox2;
        private ListBox listBox1;
    }
}