namespace AKS.Payroll.CustomControls
{
    partial class ListUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbDataList = new System.Windows.Forms.GroupBox();
            this.tcTabPane = new System.Windows.Forms.TabControl();
            this.tpFirstPage = new System.Windows.Forms.TabPage();
            this.tpSecondPage = new System.Windows.Forms.TabPage();
            this.gbListMenu = new System.Windows.Forms.GroupBox();
            this.lbLeftMenuList = new System.Windows.Forms.ListBox();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.btnButtonRight = new System.Windows.Forms.Button();
            this.btnButtonMiddle = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvPage1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.gbDataList.SuspendLayout();
            this.tcTabPane.SuspendLayout();
            this.tpFirstPage.SuspendLayout();
            this.gbListMenu.SuspendLayout();
            this.gbControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPage1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(147, 577);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(845, 26);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // gbDataList
            // 
            this.gbDataList.Controls.Add(this.tcTabPane);
            this.gbDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDataList.Location = new System.Drawing.Point(147, 81);
            this.gbDataList.Name = "gbDataList";
            this.gbDataList.Size = new System.Drawing.Size(845, 522);
            this.gbDataList.TabIndex = 6;
            this.gbDataList.TabStop = false;
            this.gbDataList.Text = "Data List";
            // 
            // tcTabPane
            // 
            this.tcTabPane.Controls.Add(this.tpFirstPage);
            this.tcTabPane.Controls.Add(this.tpSecondPage);
            this.tcTabPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTabPane.Location = new System.Drawing.Point(3, 23);
            this.tcTabPane.Name = "tcTabPane";
            this.tcTabPane.SelectedIndex = 0;
            this.tcTabPane.Size = new System.Drawing.Size(839, 496);
            this.tcTabPane.TabIndex = 0;
            // 
            // tpFirstPage
            // 
            this.tpFirstPage.Controls.Add(this.dgvPage1);
            this.tpFirstPage.Location = new System.Drawing.Point(4, 29);
            this.tpFirstPage.Name = "tpFirstPage";
            this.tpFirstPage.Padding = new System.Windows.Forms.Padding(3);
            this.tpFirstPage.Size = new System.Drawing.Size(831, 463);
            this.tpFirstPage.TabIndex = 0;
            this.tpFirstPage.Text = "Data List";
            this.tpFirstPage.UseVisualStyleBackColor = true;
            // 
            // tpSecondPage
            // 
            this.tpSecondPage.Location = new System.Drawing.Point(4, 29);
            this.tpSecondPage.Name = "tpSecondPage";
            this.tpSecondPage.Padding = new System.Windows.Forms.Padding(3);
            this.tpSecondPage.Size = new System.Drawing.Size(831, 463);
            this.tpSecondPage.TabIndex = 1;
            this.tpSecondPage.Text = "Reports";
            this.tpSecondPage.UseVisualStyleBackColor = true;
            // 
            // gbListMenu
            // 
            this.gbListMenu.Controls.Add(this.lbLeftMenuList);
            this.gbListMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbListMenu.Location = new System.Drawing.Point(0, 81);
            this.gbListMenu.Name = "gbListMenu";
            this.gbListMenu.Size = new System.Drawing.Size(147, 522);
            this.gbListMenu.TabIndex = 5;
            this.gbListMenu.TabStop = false;
            this.gbListMenu.Text = "List Menu";
            // 
            // lbLeftMenuList
            // 
            this.lbLeftMenuList.DisplayMember = "StaffName";
            this.lbLeftMenuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLeftMenuList.FormattingEnabled = true;
            this.lbLeftMenuList.ItemHeight = 20;
            this.lbLeftMenuList.Location = new System.Drawing.Point(3, 23);
            this.lbLeftMenuList.Name = "lbLeftMenuList";
            this.lbLeftMenuList.Size = new System.Drawing.Size(141, 496);
            this.lbLeftMenuList.Sorted = true;
            this.lbLeftMenuList.TabIndex = 0;
            this.lbLeftMenuList.ValueMember = "EmployeeId";
            // 
            // gbControls
            // 
            this.gbControls.Controls.Add(this.btnButtonRight);
            this.gbControls.Controls.Add(this.btnButtonMiddle);
            this.gbControls.Controls.Add(this.btnAdd);
            this.gbControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbControls.Location = new System.Drawing.Point(0, 0);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(992, 81);
            this.gbControls.TabIndex = 4;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "Controls";
            // 
            // btnButtonRight
            // 
            this.btnButtonRight.Location = new System.Drawing.Point(267, 34);
            this.btnButtonRight.Name = "btnButtonRight";
            this.btnButtonRight.Size = new System.Drawing.Size(94, 29);
            this.btnButtonRight.TabIndex = 2;
            this.btnButtonRight.Text = "button3";
            this.btnButtonRight.UseVisualStyleBackColor = true;
            this.btnButtonRight.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnButtonMiddle
            // 
            this.btnButtonMiddle.Location = new System.Drawing.Point(158, 34);
            this.btnButtonMiddle.Name = "btnButtonMiddle";
            this.btnButtonMiddle.Size = new System.Drawing.Size(94, 29);
            this.btnButtonMiddle.TabIndex = 1;
            this.btnButtonMiddle.Text = "button2";
            this.btnButtonMiddle.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(58, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 29);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // dgvPage1
            // 
            this.dgvPage1.AllowUserToAddRows = false;
            this.dgvPage1.AllowUserToDeleteRows = false;
            this.dgvPage1.AllowUserToOrderColumns = true;
            this.dgvPage1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPage1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPage1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPage1.Location = new System.Drawing.Point(3, 3);
            this.dgvPage1.Name = "dgvPage1";
            this.dgvPage1.ReadOnly = true;
            this.dgvPage1.RowHeadersWidth = 51;
            this.dgvPage1.RowTemplate.Height = 29;
            this.dgvPage1.Size = new System.Drawing.Size(825, 457);
            this.dgvPage1.TabIndex = 0;
            // 
            // ListUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbDataList);
            this.Controls.Add(this.gbListMenu);
            this.Controls.Add(this.gbControls);
            this.Name = "ListUI";
            this.Size = new System.Drawing.Size(992, 603);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbDataList.ResumeLayout(false);
            this.tcTabPane.ResumeLayout(false);
            this.tpFirstPage.ResumeLayout(false);
            this.gbListMenu.ResumeLayout(false);
            this.gbControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPage1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private GroupBox gbDataList;
        private TabControl tcTabPane;
        private TabPage tpFirstPage;
        private TabPage tpSecondPage;
        private GroupBox gbListMenu;
        private ListBox lbLeftMenuList;
        private GroupBox gbControls;
        private Button btnButtonRight;
        private Button btnButtonMiddle;
        private Button btnAdd;
        private DataGridView dgvPage1;
    }
}
