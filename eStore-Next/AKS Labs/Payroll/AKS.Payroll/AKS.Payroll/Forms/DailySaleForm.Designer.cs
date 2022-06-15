﻿namespace AKS.Payroll.Forms
{
    partial class DailySaleForm
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
            this.btnDueRecovery = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbYearly = new System.Windows.Forms.RadioButton();
            this.lbLMonth = new System.Windows.Forms.RadioButton();
            this.rbCMonth = new System.Windows.Forms.RadioButton();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbYearList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvDues = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvRecovered = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDues)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecovered)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDueRecovery);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnReload);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(914, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnDueRecovery
            // 
            this.btnDueRecovery.Location = new System.Drawing.Point(323, 30);
            this.btnDueRecovery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDueRecovery.Name = "btnDueRecovery";
            this.btnDueRecovery.Size = new System.Drawing.Size(86, 40);
            this.btnDueRecovery.TabIndex = 4;
            this.btnDueRecovery.Text = "Recovery";
            this.btnDueRecovery.UseVisualStyleBackColor = true;
            this.btnDueRecovery.Click += new System.EventHandler(this.btnDueRecovery_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.rbYearly);
            this.panel1.Controls.Add(this.lbLMonth);
            this.panel1.Controls.Add(this.rbCMonth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(612, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 51);
            this.panel1.TabIndex = 3;
            // 
            // rbYearly
            // 
            this.rbYearly.AutoSize = true;
            this.rbYearly.Location = new System.Drawing.Point(238, 11);
            this.rbYearly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbYearly.Name = "rbYearly";
            this.rbYearly.Size = new System.Drawing.Size(58, 24);
            this.rbYearly.TabIndex = 2;
            this.rbYearly.Text = "Year";
            this.rbYearly.UseVisualStyleBackColor = true;
            this.rbYearly.CheckedChanged += new System.EventHandler(this.RadioBoxes_CheckedChanged);
            // 
            // lbLMonth
            // 
            this.lbLMonth.AutoSize = true;
            this.lbLMonth.Location = new System.Drawing.Point(128, 11);
            this.lbLMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbLMonth.Name = "lbLMonth";
            this.lbLMonth.Size = new System.Drawing.Size(103, 24);
            this.lbLMonth.TabIndex = 1;
            this.lbLMonth.Text = "Last Month";
            this.lbLMonth.UseVisualStyleBackColor = true;
            this.lbLMonth.CheckedChanged += new System.EventHandler(this.RadioBoxes_CheckedChanged);
            // 
            // rbCMonth
            // 
            this.rbCMonth.AutoSize = true;
            this.rbCMonth.Checked = true;
            this.rbCMonth.Location = new System.Drawing.Point(0, 11);
            this.rbCMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbCMonth.Name = "rbCMonth";
            this.rbCMonth.Size = new System.Drawing.Size(125, 24);
            this.rbCMonth.TabIndex = 0;
            this.rbCMonth.TabStop = true;
            this.rbCMonth.Text = "Current Month";
            this.rbCMonth.UseVisualStyleBackColor = true;
            this.rbCMonth.CheckedChanged += new System.EventHandler(this.RadioBoxes_CheckedChanged);
            // 
            // btnReload
            // 
            this.btnReload.AutoSize = true;
            this.btnReload.Location = new System.Drawing.Point(231, 30);
            this.btnReload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(86, 40);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "Refresh";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Location = new System.Drawing.Point(139, 30);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 40);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new System.Drawing.Point(47, 30);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbYearList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 79);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(128, 521);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Year";
            // 
            // lbYearList
            // 
            this.lbYearList.DisplayMember = "Year";
            this.lbYearList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbYearList.FormattingEnabled = true;
            this.lbYearList.ItemHeight = 20;
            this.lbYearList.Location = new System.Drawing.Point(3, 24);
            this.lbYearList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbYearList.Name = "lbYearList";
            this.lbYearList.Size = new System.Drawing.Size(122, 493);
            this.lbYearList.TabIndex = 0;
            this.lbYearList.ValueMember = "Year";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(128, 79);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(786, 521);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sale List";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 493);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvSales);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(772, 460);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Daily Sale";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.AllowUserToOrderColumns = true;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSales.Location = new System.Drawing.Point(3, 4);
            this.dgvSales.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersWidth = 51;
            this.dgvSales.RowTemplate.Height = 25;
            this.dgvSales.Size = new System.Drawing.Size(766, 452);
            this.dgvSales.TabIndex = 0;
            this.dgvSales.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSales_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvDues);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(772, 460);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dues";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvDues
            // 
            this.dgvDues.AllowUserToAddRows = false;
            this.dgvDues.AllowUserToDeleteRows = false;
            this.dgvDues.AllowUserToOrderColumns = true;
            this.dgvDues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDues.Location = new System.Drawing.Point(3, 4);
            this.dgvDues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvDues.Name = "dgvDues";
            this.dgvDues.ReadOnly = true;
            this.dgvDues.RowHeadersWidth = 51;
            this.dgvDues.RowTemplate.Height = 25;
            this.dgvDues.Size = new System.Drawing.Size(766, 452);
            this.dgvDues.TabIndex = 0;
            this.dgvDues.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDues_CellContentClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvRecovered);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Size = new System.Drawing.Size(772, 460);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Dues Recovered";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvRecovered
            // 
            this.dgvRecovered.AllowUserToAddRows = false;
            this.dgvRecovered.AllowUserToDeleteRows = false;
            this.dgvRecovered.AllowUserToOrderColumns = true;
            this.dgvRecovered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecovered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecovered.Location = new System.Drawing.Point(3, 4);
            this.dgvRecovered.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvRecovered.Name = "dgvRecovered";
            this.dgvRecovered.ReadOnly = true;
            this.dgvRecovered.RowHeadersWidth = 51;
            this.dgvRecovered.RowTemplate.Height = 25;
            this.dgvRecovered.Size = new System.Drawing.Size(766, 452);
            this.dgvRecovered.TabIndex = 0;
            this.dgvRecovered.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecovered_CellContentClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(128, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(786, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // DailySaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DailySaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daily Sale";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DailySaleForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDues)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecovered)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ListBox lbYearList;
        private StatusStrip statusStrip1;
        private Button btnReload;
        private Button btnDelete;
        private Button btnAdd;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private DataGridView dgvSales;
        private TabPage tabPage2;
        private DataGridView dgvDues;
        private TabPage tabPage3;
        private DataGridView dgvRecovered;
        private Panel panel1;
        private RadioButton rbYearly;
        private RadioButton lbLMonth;
        private RadioButton rbCMonth;
        private Button btnDueRecovery;
    }
}