namespace AKS.UI.Accounting.Forms.Banking
{
    partial class BankAccountEntryForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbVendor = new System.Windows.Forms.Label();
            this.cbxBanks = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbSharedAccount = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.txtIFSCCode = new System.Windows.Forms.TextBox();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxAccountType = new System.Windows.Forms.ComboBox();
            this.lbCurrentBalance = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpOpeningDate = new System.Windows.Forms.DateTimePicker();
            this.txtOpeningBalance = new System.Windows.Forms.TextBox();
            this.cbSharedAccount = new System.Windows.Forms.CheckBox();
            this.cbxVendors = new System.Windows.Forms.ComboBox();
            this.txtCurrentBalance = new System.Windows.Forms.TextBox();
            this.lbDefaultAccount = new System.Windows.Forms.Label();
            this.lbClosingDate = new System.Windows.Forms.Label();
            this.cbDefaultAccount = new System.Windows.Forms.CheckBox();
            this.dtpClosingDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxStores = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.rbThirdPartyAccount = new System.Windows.Forms.RadioButton();
            this.rbVendorAccount = new System.Windows.Forms.RadioButton();
            this.rbCompanyAccount = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1065, 568);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 443);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1065, 125);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(294, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(173, 47);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 29);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(38, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 29);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1065, 485);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entry Form";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbVendor, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbxBanks, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtAccountNumber, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbSharedAccount, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbIsActive, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtIFSCCode, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBranch, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxAccountType, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbCurrentBalance, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.dtpOpeningDate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtOpeningBalance, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbSharedAccount, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxVendors, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCurrentBalance, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbDefaultAccount, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbClosingDate, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbDefaultAccount, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.dtpClosingDate, 3, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1059, 459);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bank";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "IFSC Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Branch";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(642, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Account Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(642, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "City";
            // 
            // lbVendor
            // 
            this.lbVendor.AutoSize = true;
            this.lbVendor.Location = new System.Drawing.Point(642, 101);
            this.lbVendor.Name = "lbVendor";
            this.lbVendor.Size = new System.Drawing.Size(56, 20);
            this.lbVendor.TabIndex = 13;
            this.lbVendor.Text = "Vendor";
            // 
            // cbxBanks
            // 
            this.cbxBanks.FormattingEnabled = true;
            this.cbxBanks.Location = new System.Drawing.Point(122, 3);
            this.cbxBanks.Name = "cbxBanks";
            this.cbxBanks.Size = new System.Drawing.Size(130, 28);
            this.cbxBanks.TabIndex = 15;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(386, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(125, 27);
            this.txtName.TabIndex = 16;
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(769, 3);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(125, 27);
            this.txtAccountNumber.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Status";
            // 
            // lbSharedAccount
            // 
            this.lbSharedAccount.AutoSize = true;
            this.lbSharedAccount.Location = new System.Drawing.Point(642, 67);
            this.lbSharedAccount.Name = "lbSharedAccount";
            this.lbSharedAccount.Size = new System.Drawing.Size(113, 20);
            this.lbSharedAccount.TabIndex = 14;
            this.lbSharedAccount.Text = "Shared Account";
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Location = new System.Drawing.Point(122, 70);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(72, 24);
            this.cbIsActive.TabIndex = 18;
            this.cbIsActive.Text = "Active";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // txtIFSCCode
            // 
            this.txtIFSCCode.Location = new System.Drawing.Point(122, 37);
            this.txtIFSCCode.Name = "txtIFSCCode";
            this.txtIFSCCode.Size = new System.Drawing.Size(125, 27);
            this.txtIFSCCode.TabIndex = 19;
            // 
            // txtBranch
            // 
            this.txtBranch.Location = new System.Drawing.Point(386, 37);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(125, 27);
            this.txtBranch.TabIndex = 20;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(769, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 27);
            this.textBox1.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(258, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Account Type";
            // 
            // cbxAccountType
            // 
            this.cbxAccountType.FormattingEnabled = true;
            this.cbxAccountType.Location = new System.Drawing.Point(386, 70);
            this.cbxAccountType.Name = "cbxAccountType";
            this.cbxAccountType.Size = new System.Drawing.Size(151, 28);
            this.cbxAccountType.TabIndex = 22;
            // 
            // lbCurrentBalance
            // 
            this.lbCurrentBalance.AutoSize = true;
            this.lbCurrentBalance.Location = new System.Drawing.Point(3, 135);
            this.lbCurrentBalance.Name = "lbCurrentBalance";
            this.lbCurrentBalance.Size = new System.Drawing.Size(113, 20);
            this.lbCurrentBalance.TabIndex = 7;
            this.lbCurrentBalance.Text = "Current Balance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Opening Date";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(258, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 20);
            this.label10.TabIndex = 9;
            this.label10.Text = "Opening Balance";
            // 
            // dtpOpeningDate
            // 
            this.dtpOpeningDate.Location = new System.Drawing.Point(122, 104);
            this.dtpOpeningDate.Name = "dtpOpeningDate";
            this.dtpOpeningDate.Size = new System.Drawing.Size(130, 27);
            this.dtpOpeningDate.TabIndex = 23;
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.Location = new System.Drawing.Point(386, 104);
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(125, 27);
            this.txtOpeningBalance.TabIndex = 24;
            // 
            // cbSharedAccount
            // 
            this.cbSharedAccount.AutoSize = true;
            this.cbSharedAccount.Location = new System.Drawing.Point(769, 70);
            this.cbSharedAccount.Name = "cbSharedAccount";
            this.cbSharedAccount.Size = new System.Drawing.Size(77, 24);
            this.cbSharedAccount.TabIndex = 25;
            this.cbSharedAccount.Text = "Shared";
            this.cbSharedAccount.UseVisualStyleBackColor = true;
            // 
            // cbxVendors
            // 
            this.cbxVendors.FormattingEnabled = true;
            this.cbxVendors.Location = new System.Drawing.Point(769, 104);
            this.cbxVendors.Name = "cbxVendors";
            this.cbxVendors.Size = new System.Drawing.Size(151, 28);
            this.cbxVendors.TabIndex = 26;
            // 
            // txtCurrentBalance
            // 
            this.txtCurrentBalance.Location = new System.Drawing.Point(122, 138);
            this.txtCurrentBalance.Name = "txtCurrentBalance";
            this.txtCurrentBalance.Size = new System.Drawing.Size(125, 27);
            this.txtCurrentBalance.TabIndex = 27;
            // 
            // lbDefaultAccount
            // 
            this.lbDefaultAccount.AutoSize = true;
            this.lbDefaultAccount.Location = new System.Drawing.Point(642, 135);
            this.lbDefaultAccount.Name = "lbDefaultAccount";
            this.lbDefaultAccount.Size = new System.Drawing.Size(116, 20);
            this.lbDefaultAccount.TabIndex = 11;
            this.lbDefaultAccount.Text = "Default Account";
            // 
            // lbClosingDate
            // 
            this.lbClosingDate.AutoSize = true;
            this.lbClosingDate.Location = new System.Drawing.Point(258, 135);
            this.lbClosingDate.Name = "lbClosingDate";
            this.lbClosingDate.Size = new System.Drawing.Size(94, 20);
            this.lbClosingDate.TabIndex = 12;
            this.lbClosingDate.Text = "Closing Date";
            // 
            // cbDefaultAccount
            // 
            this.cbDefaultAccount.AutoSize = true;
            this.cbDefaultAccount.Location = new System.Drawing.Point(769, 138);
            this.cbDefaultAccount.Name = "cbDefaultAccount";
            this.cbDefaultAccount.Size = new System.Drawing.Size(138, 24);
            this.cbDefaultAccount.TabIndex = 29;
            this.cbDefaultAccount.Text = "Default Account";
            this.cbDefaultAccount.UseVisualStyleBackColor = true;
            // 
            // dtpClosingDate
            // 
            this.dtpClosingDate.Location = new System.Drawing.Point(386, 138);
            this.dtpClosingDate.Name = "dtpClosingDate";
            this.dtpClosingDate.Size = new System.Drawing.Size(250, 27);
            this.dtpClosingDate.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxStores);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.rbThirdPartyAccount);
            this.groupBox1.Controls.Add(this.rbVendorAccount);
            this.groupBox1.Controls.Add(this.rbCompanyAccount);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1065, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entry Type";
            // 
            // cbxStores
            // 
            this.cbxStores.FormattingEnabled = true;
            this.cbxStores.Location = new System.Drawing.Point(640, 38);
            this.cbxStores.Name = "cbxStores";
            this.cbxStores.Size = new System.Drawing.Size(151, 28);
            this.cbxStores.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(590, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 20);
            this.label16.TabIndex = 3;
            this.label16.Text = "Store";
            // 
            // rbThirdPartyAccount
            // 
            this.rbThirdPartyAccount.AutoSize = true;
            this.rbThirdPartyAccount.Location = new System.Drawing.Point(377, 36);
            this.rbThirdPartyAccount.Name = "rbThirdPartyAccount";
            this.rbThirdPartyAccount.Size = new System.Drawing.Size(194, 24);
            this.rbThirdPartyAccount.TabIndex = 2;
            this.rbThirdPartyAccount.TabStop = true;
            this.rbThirdPartyAccount.Text = "Third Party Bank Account";
            this.rbThirdPartyAccount.UseVisualStyleBackColor = true;
            // 
            // rbVendorAccount
            // 
            this.rbVendorAccount.AutoSize = true;
            this.rbVendorAccount.Location = new System.Drawing.Point(242, 36);
            this.rbVendorAccount.Name = "rbVendorAccount";
            this.rbVendorAccount.Size = new System.Drawing.Size(135, 24);
            this.rbVendorAccount.TabIndex = 1;
            this.rbVendorAccount.TabStop = true;
            this.rbVendorAccount.Text = "Vendor Account";
            this.rbVendorAccount.UseVisualStyleBackColor = true;
            // 
            // rbCompanyAccount
            // 
            this.rbCompanyAccount.AutoSize = true;
            this.rbCompanyAccount.Checked = true;
            this.rbCompanyAccount.Location = new System.Drawing.Point(91, 36);
            this.rbCompanyAccount.Name = "rbCompanyAccount";
            this.rbCompanyAccount.Size = new System.Drawing.Size(151, 24);
            this.rbCompanyAccount.TabIndex = 0;
            this.rbCompanyAccount.TabStop = true;
            this.rbCompanyAccount.Text = "Company Account";
            this.rbCompanyAccount.UseVisualStyleBackColor = true;
            // 
            // BankAccountEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 568);
            this.Controls.Add(this.panel1);
            this.Name = "BankAccountEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Account Entry ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BankAccountEntryForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RadioButton rbCompanyAccount;
        private RadioButton rbVendorAccount;
        private RadioButton rbThirdPartyAccount;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lbCurrentBalance;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label lbDefaultAccount;
        private Label lbClosingDate;
        private Label lbVendor;
        private Label lbSharedAccount;
        private ComboBox cbxBanks;
        private TextBox txtName;
        private TextBox txtAccountNumber;
        private CheckBox cbIsActive;
        private TextBox txtIFSCCode;
        private TextBox txtBranch;
        private TextBox textBox1;
        private ComboBox cbxAccountType;
        private DateTimePicker dtpOpeningDate;
        private TextBox txtOpeningBalance;
        private CheckBox cbSharedAccount;
        private ComboBox cbxVendors;
        private TextBox txtCurrentBalance;
        private Label label16;
        private ComboBox cbxStores;
        private CheckBox cbDefaultAccount;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnCancel;
        private DateTimePicker dtpClosingDate;
    }
}