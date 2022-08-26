namespace AKS.POSBilling.Forms
{
    partial class PaymentForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxPaymentMode = new System.Windows.Forms.ComboBox();
            this.lbInv = new System.Windows.Forms.Label();
            this.lbAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtRefNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxCardType = new System.Windows.Forms.ComboBox();
            this.cbxCard = new System.Windows.Forms.ComboBox();
            this.txtLastFour = new System.Windows.Forms.TextBox();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxPOSMachine = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 159);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxPaymentMode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbInv, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAmount, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtAmount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRefNumber, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbxCardType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxCard, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLastFour, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtAuthCode, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCancle, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbxPOSMachine, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Payment Mode";
            // 
            // cbxPaymentMode
            // 
            this.cbxPaymentMode.FormattingEnabled = true;
            this.cbxPaymentMode.Location = new System.Drawing.Point(97, 3);
            this.cbxPaymentMode.Name = "cbxPaymentMode";
            this.cbxPaymentMode.Size = new System.Drawing.Size(121, 23);
            this.cbxPaymentMode.TabIndex = 1;
            // 
            // lbInv
            // 
            this.lbInv.AutoSize = true;
            this.lbInv.Location = new System.Drawing.Point(224, 0);
            this.lbInv.Name = "lbInv";
            this.lbInv.Size = new System.Drawing.Size(89, 15);
            this.lbInv.TabIndex = 2;
            this.lbInv.Text = "InvoiceNumber";
            // 
            // lbAmount
            // 
            this.lbAmount.AutoSize = true;
            this.lbAmount.Location = new System.Drawing.Point(319, 0);
            this.lbAmount.Name = "lbAmount";
            this.lbAmount.Size = new System.Drawing.Size(51, 15);
            this.lbAmount.TabIndex = 3;
            this.lbAmount.Text = "Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(224, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ref Number";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(97, 32);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 23);
            this.txtAmount.TabIndex = 6;
            // 
            // txtRefNumber
            // 
            this.txtRefNumber.Location = new System.Drawing.Point(319, 32);
            this.txtRefNumber.Name = "txtRefNumber";
            this.txtRefNumber.Size = new System.Drawing.Size(148, 23);
            this.txtRefNumber.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Card Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "Card";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Last Four Digit";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(224, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "AuthCode";
            // 
            // cbxCardType
            // 
            this.cbxCardType.FormattingEnabled = true;
            this.cbxCardType.Location = new System.Drawing.Point(97, 61);
            this.cbxCardType.Name = "cbxCardType";
            this.cbxCardType.Size = new System.Drawing.Size(121, 23);
            this.cbxCardType.TabIndex = 12;
            // 
            // cbxCard
            // 
            this.cbxCard.FormattingEnabled = true;
            this.cbxCard.Location = new System.Drawing.Point(319, 61);
            this.cbxCard.Name = "cbxCard";
            this.cbxCard.Size = new System.Drawing.Size(148, 23);
            this.cbxCard.TabIndex = 13;
            // 
            // txtLastFour
            // 
            this.txtLastFour.Location = new System.Drawing.Point(97, 90);
            this.txtLastFour.Name = "txtLastFour";
            this.txtLastFour.Size = new System.Drawing.Size(121, 23);
            this.txtLastFour.TabIndex = 14;
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.Location = new System.Drawing.Point(319, 90);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(148, 23);
            this.txtAuthCode.TabIndex = 15;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(224, 119);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Save";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(319, 119);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 17;
            this.btnCancle.Text = "Cancle";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "POS";
            // 
            // cbxPOSMachine
            // 
            this.cbxPOSMachine.FormattingEnabled = true;
            this.cbxPOSMachine.Location = new System.Drawing.Point(97, 119);
            this.cbxPOSMachine.Name = "cbxPOSMachine";
            this.cbxPOSMachine.Size = new System.Drawing.Size(121, 23);
            this.cbxPOSMachine.TabIndex = 19;
            // 
            // PaymentForm
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(473, 159);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ComboBox cbxPaymentMode;
        private Label lbInv;
        private Label lbAmount;
        private Label label4;
        private Label label5;
        private TextBox txtAmount;
        private TextBox txtRefNumber;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ComboBox cbxCardType;
        private ComboBox cbxCard;
        private TextBox txtLastFour;
        private TextBox txtAuthCode;
        private Button btnAdd;
        private Button btnCancle;
        private Label label2;
        private ComboBox cbxPOSMachine;
    }
}