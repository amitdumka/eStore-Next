namespace AKS.UI.Accounting.Forms
{
    partial class VoucherEntryForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxStores = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbCashPayment = new System.Windows.Forms.RadioButton();
            this.rbCashReceipts = new System.Windows.Forms.RadioButton();
            this.rbReceipts = new System.Windows.Forms.RadioButton();
            this.rbPayment = new System.Windows.Forms.RadioButton();
            this.rbExpenses = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbDetails = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpOnDate = new System.Windows.Forms.DateTimePicker();
            this.txtSlipNo = new System.Windows.Forms.TextBox();
            this.txtPartyName = new System.Windows.Forms.TextBox();
            this.cbxBankAccount = new System.Windows.Forms.ComboBox();
            this.txtPaymentDetails = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.cbxEmployees = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxParties = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cbxPaymentMode = new System.Windows.Forms.ComboBox();
            this.lbBankAccount = new System.Windows.Forms.Label();
            this.lbMode = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbTMode = new System.Windows.Forms.Label();
            this.cbxTranscationMode = new System.Windows.Forms.ComboBox();
            this.txtParticulars = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxStores);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(571, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(216, 47);
            this.panel2.TabIndex = 1;
            // 
            // cbxStores
            // 
            this.cbxStores.FormattingEnabled = true;
            this.cbxStores.Location = new System.Drawing.Point(54, 10);
            this.cbxStores.Name = "cbxStores";
            this.cbxStores.Size = new System.Drawing.Size(143, 23);
            this.cbxStores.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "Store";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.rbCashPayment);
            this.panel1.Controls.Add(this.rbCashReceipts);
            this.panel1.Controls.Add(this.rbReceipts);
            this.panel1.Controls.Add(this.rbPayment);
            this.panel1.Controls.Add(this.rbExpenses);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 47);
            this.panel1.TabIndex = 0;
            // 
            // rbCashPayment
            // 
            this.rbCashPayment.AutoSize = true;
            this.rbCashPayment.Location = new System.Drawing.Point(14, 14);
            this.rbCashPayment.Name = "rbCashPayment";
            this.rbCashPayment.Size = new System.Drawing.Size(101, 19);
            this.rbCashPayment.TabIndex = 4;
            this.rbCashPayment.TabStop = true;
            this.rbCashPayment.Text = "Cash Payment";
            this.rbCashPayment.UseVisualStyleBackColor = true;
            this.rbCashPayment.CheckedChanged += new System.EventHandler(this.rbCashPayment_CheckedChanged);
            // 
            // rbCashReceipts
            // 
            this.rbCashReceipts.AutoSize = true;
            this.rbCashReceipts.Location = new System.Drawing.Point(115, 14);
            this.rbCashReceipts.Name = "rbCashReceipts";
            this.rbCashReceipts.Size = new System.Drawing.Size(98, 19);
            this.rbCashReceipts.TabIndex = 3;
            this.rbCashReceipts.TabStop = true;
            this.rbCashReceipts.Text = "Cash Receipts";
            this.rbCashReceipts.UseVisualStyleBackColor = true;
            this.rbCashReceipts.CheckedChanged += new System.EventHandler(this.rbCashReceipts_CheckedChanged);
            // 
            // rbReceipts
            // 
            this.rbReceipts.AutoSize = true;
            this.rbReceipts.Location = new System.Drawing.Point(358, 14);
            this.rbReceipts.Name = "rbReceipts";
            this.rbReceipts.Size = new System.Drawing.Size(69, 19);
            this.rbReceipts.TabIndex = 2;
            this.rbReceipts.TabStop = true;
            this.rbReceipts.Text = "Receipts";
            this.rbReceipts.UseVisualStyleBackColor = true;
            this.rbReceipts.CheckedChanged += new System.EventHandler(this.rbReceipts_CheckedChanged);
            // 
            // rbPayment
            // 
            this.rbPayment.AutoSize = true;
            this.rbPayment.Location = new System.Drawing.Point(286, 14);
            this.rbPayment.Name = "rbPayment";
            this.rbPayment.Size = new System.Drawing.Size(72, 19);
            this.rbPayment.TabIndex = 1;
            this.rbPayment.TabStop = true;
            this.rbPayment.Text = "Payment";
            this.rbPayment.UseVisualStyleBackColor = true;
            this.rbPayment.CheckedChanged += new System.EventHandler(this.rbPayment_CheckedChanged);
            // 
            // rbExpenses
            // 
            this.rbExpenses.AutoSize = true;
            this.rbExpenses.Location = new System.Drawing.Point(213, 14);
            this.rbExpenses.Name = "rbExpenses";
            this.rbExpenses.Size = new System.Drawing.Size(73, 19);
            this.rbExpenses.TabIndex = 0;
            this.rbExpenses.TabStop = true;
            this.rbExpenses.Text = "Expenses";
            this.rbExpenses.UseVisualStyleBackColor = true;
            this.rbExpenses.CheckedChanged += new System.EventHandler(this.rbExpenses_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(790, 217);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Form";
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
            this.tableLayoutPanel1.Controls.Add(this.lbDetails, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpOnDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSlipNo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPartyName, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxBankAccount, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPaymentDetails, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRemarks, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxEmployees, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbxParties, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAmount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxPaymentMode, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbBankAccount, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbMode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbTMode, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbxTranscationMode, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtParticulars, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 195);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Slip No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(505, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Party Name";
            // 
            // lbDetails
            // 
            this.lbDetails.AutoSize = true;
            this.lbDetails.Location = new System.Drawing.Point(505, 29);
            this.lbDetails.Name = "lbDetails";
            this.lbDetails.Size = new System.Drawing.Size(92, 15);
            this.lbDetails.TabIndex = 5;
            this.lbDetails.Text = "Payment Details";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Remarks";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(505, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Processed By";
            // 
            // dtpOnDate
            // 
            this.dtpOnDate.Location = new System.Drawing.Point(97, 3);
            this.dtpOnDate.Name = "dtpOnDate";
            this.dtpOnDate.Size = new System.Drawing.Size(161, 23);
            this.dtpOnDate.TabIndex = 9;
            // 
            // txtSlipNo
            // 
            this.txtSlipNo.Location = new System.Drawing.Point(351, 3);
            this.txtSlipNo.Name = "txtSlipNo";
            this.txtSlipNo.Size = new System.Drawing.Size(148, 23);
            this.txtSlipNo.TabIndex = 10;
            // 
            // txtPartyName
            // 
            this.txtPartyName.Location = new System.Drawing.Point(612, 3);
            this.txtPartyName.Name = "txtPartyName";
            this.txtPartyName.Size = new System.Drawing.Size(177, 23);
            this.txtPartyName.TabIndex = 11;
            // 
            // cbxBankAccount
            // 
            this.cbxBankAccount.FormattingEnabled = true;
            this.cbxBankAccount.Location = new System.Drawing.Point(351, 32);
            this.cbxBankAccount.Name = "cbxBankAccount";
            this.cbxBankAccount.Size = new System.Drawing.Size(148, 23);
            this.cbxBankAccount.TabIndex = 13;
            // 
            // txtPaymentDetails
            // 
            this.txtPaymentDetails.Location = new System.Drawing.Point(612, 32);
            this.txtPaymentDetails.Name = "txtPaymentDetails";
            this.txtPaymentDetails.Size = new System.Drawing.Size(177, 23);
            this.txtPaymentDetails.TabIndex = 14;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(351, 61);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(148, 23);
            this.txtRemarks.TabIndex = 16;
            // 
            // cbxEmployees
            // 
            this.cbxEmployees.FormattingEnabled = true;
            this.cbxEmployees.Location = new System.Drawing.Point(612, 61);
            this.cbxEmployees.Name = "cbxEmployees";
            this.cbxEmployees.Size = new System.Drawing.Size(177, 23);
            this.cbxEmployees.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Party Ledger";
            // 
            // cbxParties
            // 
            this.cbxParties.FormattingEnabled = true;
            this.cbxParties.Location = new System.Drawing.Point(97, 90);
            this.cbxParties.Name = "cbxParties";
            this.cbxParties.Size = new System.Drawing.Size(161, 23);
            this.cbxParties.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(97, 61);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(161, 23);
            this.txtAmount.TabIndex = 15;
            // 
            // cbxPaymentMode
            // 
            this.cbxPaymentMode.FormattingEnabled = true;
            this.cbxPaymentMode.Location = new System.Drawing.Point(97, 32);
            this.cbxPaymentMode.Name = "cbxPaymentMode";
            this.cbxPaymentMode.Size = new System.Drawing.Size(161, 23);
            this.cbxPaymentMode.TabIndex = 12;
            this.cbxPaymentMode.SelectedIndexChanged += new System.EventHandler(this.cbxPaymentMode_SelectedIndexChanged);
            // 
            // lbBankAccount
            // 
            this.lbBankAccount.AutoSize = true;
            this.lbBankAccount.Location = new System.Drawing.Point(264, 29);
            this.lbBankAccount.Name = "lbBankAccount";
            this.lbBankAccount.Size = new System.Drawing.Size(81, 15);
            this.lbBankAccount.TabIndex = 6;
            this.lbBankAccount.Text = "Bank Account";
            // 
            // lbMode
            // 
            this.lbMode.AutoSize = true;
            this.lbMode.Location = new System.Drawing.Point(3, 29);
            this.lbMode.Name = "lbMode";
            this.lbMode.Size = new System.Drawing.Size(88, 15);
            this.lbMode.TabIndex = 4;
            this.lbMode.Text = "Payment Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "Particulars";
            // 
            // lbTMode
            // 
            this.lbTMode.AutoSize = true;
            this.lbTMode.Location = new System.Drawing.Point(505, 87);
            this.lbTMode.Name = "lbTMode";
            this.lbTMode.Size = new System.Drawing.Size(101, 15);
            this.lbTMode.TabIndex = 20;
            this.lbTMode.Text = "Transcation Mode";
            // 
            // cbxTranscationMode
            // 
            this.cbxTranscationMode.FormattingEnabled = true;
            this.cbxTranscationMode.Location = new System.Drawing.Point(612, 90);
            this.cbxTranscationMode.Name = "cbxTranscationMode";
            this.cbxTranscationMode.Size = new System.Drawing.Size(148, 23);
            this.cbxTranscationMode.TabIndex = 20;
            // 
            // txtParticulars
            // 
            this.txtParticulars.Location = new System.Drawing.Point(351, 90);
            this.txtParticulars.Name = "txtParticulars";
            this.txtParticulars.Size = new System.Drawing.Size(148, 23);
            this.txtParticulars.TabIndex = 19;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 234);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(790, 52);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(554, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 30);
            this.panel3.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancle";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(5, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(80, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // VoucherEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(790, 286);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "VoucherEntryForm";
            this.Text = "Voucher Entry";
            this.Load += new System.EventHandler(this.VoucherEntryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Panel panel1;
        private RadioButton rbCashPayment;
        private RadioButton rbCashReceipts;
        private RadioButton rbReceipts;
        private RadioButton rbPayment;
        private RadioButton rbExpenses;
        private Panel panel2;
        private ComboBox cbxStores;
        private Label label13;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lbMode;
        private Label lbDetails;
        private Label lbBankAccount;
        private Label label8;
        private Label label9;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnAdd;
        private DateTimePicker dtpOnDate;
        private TextBox txtSlipNo;
        private TextBox txtPartyName;
        private TextBox txtAmount;
        private ComboBox cbxBankAccount;
        private TextBox txtPaymentDetails;
        private ComboBox cbxPaymentMode;
        private TextBox txtRemarks;
        private ComboBox cbxEmployees;
        private Label label10;
        private ComboBox cbxParties;
        private Panel panel3;
        private Label lbTMode;
        private ComboBox cbxTranscationMode;
        private Label label5;
        private TextBox txtParticulars;
    }
}