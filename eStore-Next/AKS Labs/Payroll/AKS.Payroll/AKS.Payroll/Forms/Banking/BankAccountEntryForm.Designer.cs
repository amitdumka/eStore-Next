namespace AKS.Payroll.Forms.Banking
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbCompanyAccount = new System.Windows.Forms.RadioButton();
            this.rbVendorAccount = new System.Windows.Forms.RadioButton();
            this.rbThirdPartyAccount = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            // groupBox1
            // 
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
            // groupBox3
            // 
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 443);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1065, 125);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.60581F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.39419F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.29592F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.70408F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1059, 459);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(967, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(967, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // BankAccountEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 568);
            this.Controls.Add(this.panel1);
            this.Name = "BankAccountEntryForm";
            this.Text = "BankAccountEntryForm";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
    }
}