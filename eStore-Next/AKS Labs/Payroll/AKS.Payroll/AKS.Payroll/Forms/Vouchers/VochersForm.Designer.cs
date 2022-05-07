namespace AKS.Payroll.Forms.Vouchers
{
    partial class VochersForm
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
            this.button4 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbYearList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpExpenses = new System.Windows.Forms.TabPage();
            this.dgvExpenses = new System.Windows.Forms.DataGridView();
            this.tpPayments = new System.Windows.Forms.TabPage();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.tpReceipts = new System.Windows.Forms.TabPage();
            this.dgvReceipts = new System.Windows.Forms.DataGridView();
            this.tpCashReceipts = new System.Windows.Forms.TabPage();
            this.dgvCashReceipts = new System.Windows.Forms.DataGridView();
            this.tpCashPayments = new System.Windows.Forms.TabPage();
            this.dgvCashPayments = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpExpenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).BeginInit();
            this.tpPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.tpReceipts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
            this.tpCashReceipts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashReceipts)).BeginInit();
            this.tpCashPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(975, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(361, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Process Data";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(280, 22);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(199, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(118, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbYearList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(111, 405);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Menu";
            // 
            // lbYearList
            // 
            this.lbYearList.DisplayMember = "Year";
            this.lbYearList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbYearList.FormattingEnabled = true;
            this.lbYearList.ItemHeight = 15;
            this.lbYearList.Location = new System.Drawing.Point(3, 19);
            this.lbYearList.Name = "lbYearList";
            this.lbYearList.Size = new System.Drawing.Size(105, 383);
            this.lbYearList.TabIndex = 0;
            this.lbYearList.ValueMember = "Year";
            this.lbYearList.SelectedIndexChanged += new System.EventHandler(this.lbYearList_SelectedIndexChanged);
            this.lbYearList.DoubleClick += new System.EventHandler(this.lbYearList_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(111, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(864, 405);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vouchers";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpExpenses);
            this.tabControl1.Controls.Add(this.tpPayments);
            this.tabControl1.Controls.Add(this.tpReceipts);
            this.tabControl1.Controls.Add(this.tpCashReceipts);
            this.tabControl1.Controls.Add(this.tpCashPayments);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(858, 383);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tpExpenses
            // 
            this.tpExpenses.Controls.Add(this.dgvExpenses);
            this.tpExpenses.Location = new System.Drawing.Point(4, 24);
            this.tpExpenses.Name = "tpExpenses";
            this.tpExpenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpExpenses.Size = new System.Drawing.Size(850, 355);
            this.tpExpenses.TabIndex = 0;
            this.tpExpenses.Text = "Expenses";
            this.tpExpenses.UseVisualStyleBackColor = true;
            // 
            // dgvExpenses
            // 
            this.dgvExpenses.AllowUserToAddRows = false;
            this.dgvExpenses.AllowUserToDeleteRows = false;
            this.dgvExpenses.AllowUserToOrderColumns = true;
            this.dgvExpenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExpenses.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExpenses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvExpenses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpenses.Location = new System.Drawing.Point(3, 3);
            this.dgvExpenses.Name = "dgvExpenses";
            this.dgvExpenses.ReadOnly = true;
            this.dgvExpenses.RowTemplate.Height = 25;
            this.dgvExpenses.Size = new System.Drawing.Size(844, 349);
            this.dgvExpenses.TabIndex = 0;
            this.dgvExpenses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpenses_CellContentClick);
            // 
            // tpPayments
            // 
            this.tpPayments.Controls.Add(this.dgvPayments);
            this.tpPayments.Location = new System.Drawing.Point(4, 24);
            this.tpPayments.Name = "tpPayments";
            this.tpPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tpPayments.Size = new System.Drawing.Size(850, 355);
            this.tpPayments.TabIndex = 1;
            this.tpPayments.Text = "Payment";
            this.tpPayments.UseVisualStyleBackColor = true;
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            this.dgvPayments.AllowUserToOrderColumns = true;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.Location = new System.Drawing.Point(3, 3);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowTemplate.Height = 25;
            this.dgvPayments.Size = new System.Drawing.Size(844, 349);
            this.dgvPayments.TabIndex = 0;
            this.dgvPayments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPayments_CellContentClick);
            // 
            // tpReceipts
            // 
            this.tpReceipts.Controls.Add(this.dgvReceipts);
            this.tpReceipts.Location = new System.Drawing.Point(4, 24);
            this.tpReceipts.Name = "tpReceipts";
            this.tpReceipts.Padding = new System.Windows.Forms.Padding(3);
            this.tpReceipts.Size = new System.Drawing.Size(850, 355);
            this.tpReceipts.TabIndex = 2;
            this.tpReceipts.Text = "Receipts";
            this.tpReceipts.UseVisualStyleBackColor = true;
            // 
            // dgvReceipts
            // 
            this.dgvReceipts.AllowUserToAddRows = false;
            this.dgvReceipts.AllowUserToDeleteRows = false;
            this.dgvReceipts.AllowUserToOrderColumns = true;
            this.dgvReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceipts.Location = new System.Drawing.Point(3, 3);
            this.dgvReceipts.Name = "dgvReceipts";
            this.dgvReceipts.ReadOnly = true;
            this.dgvReceipts.RowTemplate.Height = 25;
            this.dgvReceipts.Size = new System.Drawing.Size(844, 349);
            this.dgvReceipts.TabIndex = 0;
            this.dgvReceipts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipts_CellContentClick);
            // 
            // tpCashReceipts
            // 
            this.tpCashReceipts.Controls.Add(this.dgvCashReceipts);
            this.tpCashReceipts.Location = new System.Drawing.Point(4, 24);
            this.tpCashReceipts.Name = "tpCashReceipts";
            this.tpCashReceipts.Padding = new System.Windows.Forms.Padding(3);
            this.tpCashReceipts.Size = new System.Drawing.Size(850, 355);
            this.tpCashReceipts.TabIndex = 3;
            this.tpCashReceipts.Text = "Cash Receipts";
            this.tpCashReceipts.UseVisualStyleBackColor = true;
            // 
            // dgvCashReceipts
            // 
            this.dgvCashReceipts.AllowUserToAddRows = false;
            this.dgvCashReceipts.AllowUserToDeleteRows = false;
            this.dgvCashReceipts.AllowUserToOrderColumns = true;
            this.dgvCashReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCashReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCashReceipts.Location = new System.Drawing.Point(3, 3);
            this.dgvCashReceipts.Name = "dgvCashReceipts";
            this.dgvCashReceipts.ReadOnly = true;
            this.dgvCashReceipts.RowTemplate.Height = 25;
            this.dgvCashReceipts.Size = new System.Drawing.Size(844, 349);
            this.dgvCashReceipts.TabIndex = 0;
            this.dgvCashReceipts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCashReceipts_CellContentClick);
            // 
            // tpCashPayments
            // 
            this.tpCashPayments.Controls.Add(this.dgvCashPayments);
            this.tpCashPayments.Location = new System.Drawing.Point(4, 24);
            this.tpCashPayments.Name = "tpCashPayments";
            this.tpCashPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tpCashPayments.Size = new System.Drawing.Size(850, 355);
            this.tpCashPayments.TabIndex = 4;
            this.tpCashPayments.Text = "Cash Payments";
            this.tpCashPayments.UseVisualStyleBackColor = true;
            // 
            // dgvCashPayments
            // 
            this.dgvCashPayments.AllowUserToAddRows = false;
            this.dgvCashPayments.AllowUserToDeleteRows = false;
            this.dgvCashPayments.AllowUserToOrderColumns = true;
            this.dgvCashPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCashPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCashPayments.Location = new System.Drawing.Point(3, 3);
            this.dgvCashPayments.Name = "dgvCashPayments";
            this.dgvCashPayments.ReadOnly = true;
            this.dgvCashPayments.RowTemplate.Height = 25;
            this.dgvCashPayments.Size = new System.Drawing.Size(844, 349);
            this.dgvCashPayments.TabIndex = 0;
            this.dgvCashPayments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCashPayments_CellContentClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(111, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(864, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // VochersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(975, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "VochersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vouchers";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VochersForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpExpenses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).EndInit();
            this.tpPayments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.tpReceipts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).EndInit();
            this.tpCashReceipts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashReceipts)).EndInit();
            this.tpCashPayments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Button button4;
        private Button btnRefresh;
        private Button btnDelete;
        private Button btnAdd;
        private GroupBox groupBox2;
        private ListBox lbYearList;
        private GroupBox groupBox3;
        private TabControl tabControl1;
        private TabPage tpExpenses;
        private DataGridView dgvExpenses;
        private TabPage tpPayments;
        private DataGridView dgvPayments;
        private TabPage tpReceipts;
        private DataGridView dgvReceipts;
        private TabPage tpCashReceipts;
        private DataGridView dgvCashReceipts;
        private TabPage tpCashPayments;
        private DataGridView dgvCashPayments;
        private StatusStrip statusStrip1;
    }
}