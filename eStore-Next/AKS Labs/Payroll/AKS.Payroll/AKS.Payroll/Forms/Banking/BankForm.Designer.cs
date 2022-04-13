namespace AKS.Payroll.Forms.Banking
{
    partial class BankForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcBank = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbBankList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAddBank = new System.Windows.Forms.Button();
            this.btnDeleteBank = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tcBank.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(943, 498);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 410);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tcBank);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(144, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(799, 410);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "List";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCount,
            this.tsslCountValue});
            this.statusStrip1.Location = new System.Drawing.Point(144, 472);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(799, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCount
            // 
            this.tsslCount.Name = "tsslCount";
            this.tsslCount.Size = new System.Drawing.Size(48, 20);
            this.tsslCount.Text = "Count";
            // 
            // tsslCountValue
            // 
            this.tsslCountValue.Name = "tsslCountValue";
            this.tsslCountValue.Size = new System.Drawing.Size(17, 20);
            this.tsslCountValue.Text = "0";
            // 
            // tcBank
            // 
            this.tcBank.Controls.Add(this.tabPage1);
            this.tcBank.Controls.Add(this.tabPage2);
            this.tcBank.Controls.Add(this.tabPage3);
            this.tcBank.Controls.Add(this.tabPage4);
            this.tcBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBank.Location = new System.Drawing.Point(3, 23);
            this.tcBank.Name = "tcBank";
            this.tcBank.SelectedIndex = 0;
            this.tcBank.Size = new System.Drawing.Size(793, 384);
            this.tcBank.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView3);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(785, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(785, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(785, 351);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lbBankList);
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(785, 351);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDeleteBank);
            this.groupBox4.Controls.Add(this.btnAddBank);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(779, 95);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Entry";
            // 
            // lbBankList
            // 
            this.lbBankList.DisplayMember = "Name";
            this.lbBankList.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbBankList.FormattingEnabled = true;
            this.lbBankList.ItemHeight = 20;
            this.lbBankList.Location = new System.Drawing.Point(3, 98);
            this.lbBankList.Name = "lbBankList";
            this.lbBankList.Size = new System.Drawing.Size(174, 250);
            this.lbBankList.TabIndex = 1;
            this.lbBankList.ValueMember = "BankId";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bank Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(266, 27);
            this.textBox1.TabIndex = 1;
            // 
            // btnAddBank
            // 
            this.btnAddBank.Location = new System.Drawing.Point(400, 33);
            this.btnAddBank.Name = "btnAddBank";
            this.btnAddBank.Size = new System.Drawing.Size(94, 29);
            this.btnAddBank.TabIndex = 2;
            this.btnAddBank.Text = "Add";
            this.btnAddBank.UseVisualStyleBackColor = true;
            // 
            // btnDeleteBank
            // 
            this.btnDeleteBank.Location = new System.Drawing.Point(510, 33);
            this.btnDeleteBank.Name = "btnDeleteBank";
            this.btnDeleteBank.Size = new System.Drawing.Size(94, 29);
            this.btnDeleteBank.TabIndex = 3;
            this.btnDeleteBank.Text = "Delete";
            this.btnDeleteBank.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(779, 345);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 29;
            this.dataGridView2.Size = new System.Drawing.Size(779, 345);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToOrderColumns = true;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 29;
            this.dataGridView3.Size = new System.Drawing.Size(779, 345);
            this.dataGridView3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(286, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(494, 35);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(136, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(223, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 29);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(366, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 29);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // BankForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 498);
            this.Controls.Add(this.panel1);
            this.Name = "BankForm";
            this.Text = "Banks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tcBank.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslCount;
        private ToolStripStatusLabel tsslCountValue;
        private TabControl tcBank;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private GroupBox groupBox4;
        private ListBox lbBankList;
        private Label label1;
        private TextBox textBox1;
        private Button btnAddBank;
        private Button btnDeleteBank;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}