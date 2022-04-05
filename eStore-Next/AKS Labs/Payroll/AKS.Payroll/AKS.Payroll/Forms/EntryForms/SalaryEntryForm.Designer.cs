namespace AKS.Payroll.Forms.EntryForms
{
    partial class SalaryEntryForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxStores = new System.Windows.Forms.ComboBox();
            this.cbxEmployees = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.txtBasicSalary = new System.Windows.Forms.TextBox();
            this.cbIsEffective = new System.Windows.Forms.CheckBox();
            this.clbOptions = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 338);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnCancle);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(869, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(258, 26);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 29);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(352, 26);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(94, 29);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "Cancel";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(446, 26);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 29);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(869, 338);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Form";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxStores, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxEmployees, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpStartDate, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpEndDate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBasicSalary, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbIsEffective, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.clbOptions, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(863, 312);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Store";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Employee";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(581, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "EffectiveDate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "EndDate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Basic Salary";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(581, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Staus";
            // 
            // cbxStores
            // 
            this.cbxStores.FormattingEnabled = true;
            this.cbxStores.Location = new System.Drawing.Point(75, 3);
            this.cbxStores.Name = "cbxStores";
            this.cbxStores.Size = new System.Drawing.Size(250, 28);
            this.cbxStores.TabIndex = 6;
            // 
            // cbxEmployees
            // 
            this.cbxEmployees.FormattingEnabled = true;
            this.cbxEmployees.Location = new System.Drawing.Point(424, 3);
            this.cbxEmployees.Name = "cbxEmployees";
            this.cbxEmployees.Size = new System.Drawing.Size(151, 28);
            this.cbxEmployees.TabIndex = 7;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(685, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(182, 27);
            this.dtpStartDate.TabIndex = 8;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(75, 37);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(250, 27);
            this.dtpEndDate.TabIndex = 9;
            // 
            // txtBasicSalary
            // 
            this.txtBasicSalary.Location = new System.Drawing.Point(424, 37);
            this.txtBasicSalary.Name = "txtBasicSalary";
            this.txtBasicSalary.Size = new System.Drawing.Size(125, 27);
            this.txtBasicSalary.TabIndex = 10;
            // 
            // cbIsEffective
            // 
            this.cbIsEffective.AutoSize = true;
            this.cbIsEffective.Location = new System.Drawing.Point(685, 37);
            this.cbIsEffective.Name = "cbIsEffective";
            this.cbIsEffective.Size = new System.Drawing.Size(88, 24);
            this.cbIsEffective.TabIndex = 11;
            this.cbIsEffective.Text = "Effective";
            this.cbIsEffective.UseVisualStyleBackColor = true;
            // 
            // clbOptions
            // 
            this.clbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbOptions.FormattingEnabled = true;
            this.clbOptions.Items.AddRange(new object[] {
            "WOW Bill",
            "Last Pc Incentive",
            "Sale Incentive",
            "Tailor",
            "Full Month Billable",
            "Sunday Billable"});
            this.clbOptions.Location = new System.Drawing.Point(75, 70);
            this.clbOptions.Name = "clbOptions";
            this.clbOptions.Size = new System.Drawing.Size(250, 239);
            this.clbOptions.TabIndex = 12;
            // 
            // SalaryEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 338);
            this.Controls.Add(this.panel1);
            this.Name = "SalaryEntryForm";
            this.Text = "SalaryEntryForm";
            this.Load += new System.EventHandler(this.SalaryEntryForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox cbxStores;
        private ComboBox cbxEmployees;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private TextBox txtBasicSalary;
        private CheckBox cbIsEffective;
        private CheckedListBox clbOptions;
        private Button btnDelete;
        private Button btnCancle;
        private Button btnAdd;
    }
}