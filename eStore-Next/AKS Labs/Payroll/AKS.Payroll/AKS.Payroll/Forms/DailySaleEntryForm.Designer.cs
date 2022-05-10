namespace AKS.Payroll.Forms
{
    partial class DailySaleEntryForm
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
            this.cbxStores = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpOnDate = new System.Windows.Forms.DateTimePicker();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.cbxPaymentMode = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.txtNonCash = new System.Windows.Forms.TextBox();
            this.cbxPOS = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbManual = new System.Windows.Forms.CheckBox();
            this.cbDue = new System.Windows.Forms.CheckBox();
            this.cbTailoring = new System.Windows.Forms.CheckBox();
            this.cbSalesReturn = new System.Windows.Forms.CheckBox();
            this.cbxSaleman = new System.Windows.Forms.ComboBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxStores);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(702, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Store";
            // 
            // cbxStores
            // 
            this.cbxStores.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbxStores.FormattingEnabled = true;
            this.cbxStores.Location = new System.Drawing.Point(578, 19);
            this.cbxStores.Name = "cbxStores";
            this.cbxStores.Size = new System.Drawing.Size(121, 23);
            this.cbxStores.TabIndex = 0;
            this.cbxStores.SelectedValueChanged += new System.EventHandler(this.cbxStores_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(702, 266);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entry Form";
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
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpOnDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtInvoiceNumber, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxPaymentMode, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtAmount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCash, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtNonCash, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxPOS, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbManual, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbDue, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbTailoring, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbSalesReturn, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxSaleman, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtRemarks, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(696, 244);
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
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Amount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "POS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Salesman";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Invoice Number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Cash Amount";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(462, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "Payment Mode";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(462, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 15);
            this.label12.TabIndex = 10;
            this.label12.Text = "Non Cash Amount";
            // 
            // dtpOnDate
            // 
            this.dtpOnDate.Location = new System.Drawing.Point(66, 3);
            this.dtpOnDate.Name = "dtpOnDate";
            this.dtpOnDate.Size = new System.Drawing.Size(151, 23);
            this.dtpOnDate.TabIndex = 14;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(321, 3);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(135, 23);
            this.txtInvoiceNumber.TabIndex = 15;
            // 
            // cbxPaymentMode
            // 
            this.cbxPaymentMode.FormattingEnabled = true;
            this.cbxPaymentMode.Location = new System.Drawing.Point(574, 3);
            this.cbxPaymentMode.Name = "cbxPaymentMode";
            this.cbxPaymentMode.Size = new System.Drawing.Size(121, 23);
            this.cbxPaymentMode.TabIndex = 16;
            this.cbxPaymentMode.SelectedIndexChanged += new System.EventHandler(this.cbxPaymentMode_SelectedIndexChanged);
            this.cbxPaymentMode.SelectedValueChanged += new System.EventHandler(this.cbxPaymentMode_SelectedValueChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(66, 32);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(151, 23);
            this.txtAmount.TabIndex = 17;
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(321, 32);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(135, 23);
            this.txtCash.TabIndex = 18;
            // 
            // txtNonCash
            // 
            this.txtNonCash.Location = new System.Drawing.Point(574, 32);
            this.txtNonCash.Name = "txtNonCash";
            this.txtNonCash.Size = new System.Drawing.Size(100, 23);
            this.txtNonCash.TabIndex = 24;
            // 
            // cbxPOS
            // 
            this.cbxPOS.FormattingEnabled = true;
            this.cbxPOS.Location = new System.Drawing.Point(66, 61);
            this.cbxPOS.Name = "cbxPOS";
            this.cbxPOS.Size = new System.Drawing.Size(121, 23);
            this.cbxPOS.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(223, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "Remarks";
            // 
            // cbManual
            // 
            this.cbManual.AutoSize = true;
            this.cbManual.Location = new System.Drawing.Point(223, 61);
            this.cbManual.Name = "cbManual";
            this.cbManual.Size = new System.Drawing.Size(85, 19);
            this.cbManual.TabIndex = 21;
            this.cbManual.Text = "Manual Bill";
            this.cbManual.UseVisualStyleBackColor = true;
            // 
            // cbDue
            // 
            this.cbDue.AutoSize = true;
            this.cbDue.Location = new System.Drawing.Point(321, 61);
            this.cbDue.Name = "cbDue";
            this.cbDue.Size = new System.Drawing.Size(47, 19);
            this.cbDue.TabIndex = 22;
            this.cbDue.Text = "Due";
            this.cbDue.UseVisualStyleBackColor = true;
            // 
            // cbTailoring
            // 
            this.cbTailoring.AutoSize = true;
            this.cbTailoring.Location = new System.Drawing.Point(462, 61);
            this.cbTailoring.Name = "cbTailoring";
            this.cbTailoring.Size = new System.Drawing.Size(71, 19);
            this.cbTailoring.TabIndex = 23;
            this.cbTailoring.Text = "Tailoring";
            this.cbTailoring.UseVisualStyleBackColor = true;
            // 
            // cbSalesReturn
            // 
            this.cbSalesReturn.AutoSize = true;
            this.cbSalesReturn.Location = new System.Drawing.Point(574, 61);
            this.cbSalesReturn.Name = "cbSalesReturn";
            this.cbSalesReturn.Size = new System.Drawing.Size(90, 19);
            this.cbSalesReturn.TabIndex = 26;
            this.cbSalesReturn.Text = "Sales Return";
            this.cbSalesReturn.UseVisualStyleBackColor = true;
            // 
            // cbxSaleman
            // 
            this.cbxSaleman.FormattingEnabled = true;
            this.cbxSaleman.Location = new System.Drawing.Point(66, 90);
            this.cbxSaleman.Name = "cbxSaleman";
            this.cbxSaleman.Size = new System.Drawing.Size(121, 23);
            this.cbxSaleman.TabIndex = 27;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(321, 90);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(100, 23);
            this.txtRemarks.TabIndex = 28;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnCancle);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 269);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(702, 52);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.Location = new System.Drawing.Point(474, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Location = new System.Drawing.Point(549, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancle.Location = new System.Drawing.Point(624, 19);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 30);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "Cancle";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // DailySaleEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(702, 321);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "DailySaleEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daily Sale Entry";
            this.Load += new System.EventHandler(this.DailySaleEntryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label label10;
        private Label label11;
        private Label label12;
        private DateTimePicker dtpOnDate;
        private TextBox txtInvoiceNumber;
        private ComboBox cbxPaymentMode;
        private TextBox txtAmount;
        private TextBox txtCash;
        private CheckBox cbTailoring;
        private CheckBox cbDue;
        private CheckBox cbManual;
        private TextBox txtNonCash;
        private ComboBox cbxPOS;
        private CheckBox cbSalesReturn;
        private ComboBox cbxSaleman;
        private TextBox txtRemarks;
        private ComboBox cbxStores;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnCancle;
    }
}