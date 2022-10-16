﻿namespace eStore.SetUp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.Spreadsheet.SpreadsheetCopyPaste spreadsheetCopyPaste1 = new Syncfusion.Windows.Forms.Spreadsheet.SpreadsheetCopyPaste();
            Syncfusion.Windows.Forms.Spreadsheet.FormulaRangeSelectionController formulaRangeSelectionController1 = new Syncfusion.Windows.Forms.Spreadsheet.FormulaRangeSelectionController();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lbFileName = new System.Windows.Forms.Label();
            this.BTNProcess = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTNSelect = new System.Windows.Forms.Button();
            this.TXTSelectedFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TXTFileName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.NUDMaxCol = new System.Windows.Forms.NumericUpDown();
            this.NUDMaxRow = new System.Windows.Forms.NumericUpDown();
            this.NUDCol = new System.Windows.Forms.NumericUpDown();
            this.NUDRow = new System.Windows.Forms.NumericUpDown();
            this.BTNToJSON = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TXTSheetName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbEvents = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tvFileList = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTNReload = new System.Windows.Forms.Button();
            this.BTNSet = new System.Windows.Forms.Button();
            this.TXTOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbSheetNames = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ExcelSheet = new Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet();
            this.BTNShowExcel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDMaxCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDMaxRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDRow)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1265, 832);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(113, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(962, 732);
            this.panel3.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 156);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(962, 576);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ExcelSheet);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(954, 548);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Excel Sheet";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(954, 548);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Json Table";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(948, 542);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel4.Controls.Add(this.groupBox6);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.groupBox4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(962, 156);
            this.panel4.TabIndex = 7;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lbFileName);
            this.groupBox6.Controls.Add(this.BTNProcess);
            this.groupBox6.Controls.Add(this.comboBox1);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(16, 73);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(333, 77);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Operations";
            // 
            // lbFileName
            // 
            this.lbFileName.AutoSize = true;
            this.lbFileName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbFileName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbFileName.Location = new System.Drawing.Point(8, 55);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(64, 17);
            this.lbFileName.TabIndex = 3;
            this.lbFileName.Text = "FileName";
            // 
            // BTNProcess
            // 
            this.BTNProcess.Location = new System.Drawing.Point(235, 22);
            this.BTNProcess.Name = "BTNProcess";
            this.BTNProcess.Size = new System.Drawing.Size(75, 23);
            this.BTNProcess.TabIndex = 2;
            this.BTNProcess.Text = "Process";
            this.BTNProcess.UseVisualStyleBackColor = true;
            this.BTNProcess.Click += new System.EventHandler(this.BTNProcess_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(73, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Operation";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BTNShowExcel);
            this.groupBox1.Controls.Add(this.BTNSelect);
            this.groupBox1.Controls.Add(this.TXTSelectedFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 64);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source File";
            // 
            // BTNSelect
            // 
            this.BTNSelect.Location = new System.Drawing.Point(205, 20);
            this.BTNSelect.Name = "BTNSelect";
            this.BTNSelect.Size = new System.Drawing.Size(62, 23);
            this.BTNSelect.TabIndex = 2;
            this.BTNSelect.Text = "Select";
            this.BTNSelect.UseVisualStyleBackColor = true;
            this.BTNSelect.Click += new System.EventHandler(this.BTNSelect_Click);
            // 
            // TXTSelectedFile
            // 
            this.TXTSelectedFile.Location = new System.Drawing.Point(90, 20);
            this.TXTSelectedFile.Name = "TXTSelectedFile";
            this.TXTSelectedFile.Size = new System.Drawing.Size(100, 23);
            this.TXTSelectedFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source File";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TXTFileName);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.NUDMaxCol);
            this.groupBox4.Controls.Add(this.NUDMaxRow);
            this.groupBox4.Controls.Add(this.NUDCol);
            this.groupBox4.Controls.Add(this.NUDRow);
            this.groupBox4.Controls.Add(this.BTNToJSON);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.TXTSheetName);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(355, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(561, 123);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Import Sheet";
            // 
            // TXTFileName
            // 
            this.TXTFileName.Location = new System.Drawing.Point(75, 87);
            this.TXTFileName.Name = "TXTFileName";
            this.TXTFileName.Size = new System.Drawing.Size(177, 23);
            this.TXTFileName.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "FileName";
            // 
            // NUDMaxCol
            // 
            this.NUDMaxCol.Location = new System.Drawing.Point(368, 56);
            this.NUDMaxCol.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NUDMaxCol.Name = "NUDMaxCol";
            this.NUDMaxCol.Size = new System.Drawing.Size(51, 23);
            this.NUDMaxCol.TabIndex = 19;
            this.NUDMaxCol.ThousandsSeparator = true;
            // 
            // NUDMaxRow
            // 
            this.NUDMaxRow.Location = new System.Drawing.Point(258, 56);
            this.NUDMaxRow.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NUDMaxRow.Name = "NUDMaxRow";
            this.NUDMaxRow.Size = new System.Drawing.Size(51, 23);
            this.NUDMaxRow.TabIndex = 18;
            this.NUDMaxRow.ThousandsSeparator = true;
            // 
            // NUDCol
            // 
            this.NUDCol.Location = new System.Drawing.Point(139, 56);
            this.NUDCol.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NUDCol.Name = "NUDCol";
            this.NUDCol.Size = new System.Drawing.Size(51, 23);
            this.NUDCol.TabIndex = 17;
            this.NUDCol.ThousandsSeparator = true;
            // 
            // NUDRow
            // 
            this.NUDRow.Location = new System.Drawing.Point(42, 56);
            this.NUDRow.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NUDRow.Name = "NUDRow";
            this.NUDRow.Size = new System.Drawing.Size(51, 23);
            this.NUDRow.TabIndex = 16;
            this.NUDRow.ThousandsSeparator = true;
            // 
            // BTNToJSON
            // 
            this.BTNToJSON.Location = new System.Drawing.Point(368, 84);
            this.BTNToJSON.Name = "BTNToJSON";
            this.BTNToJSON.Size = new System.Drawing.Size(75, 23);
            this.BTNToJSON.TabIndex = 15;
            this.BTNToJSON.Text = "To JSON";
            this.BTNToJSON.UseVisualStyleBackColor = true;
            this.BTNToJSON.Click += new System.EventHandler(this.BTNToJSON_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(315, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Max Col";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Col";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Max Row";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Row";
            // 
            // TXTSheetName
            // 
            this.TXTSheetName.Location = new System.Drawing.Point(83, 22);
            this.TXTSheetName.Name = "TXTSheetName";
            this.TXTSheetName.Size = new System.Drawing.Size(327, 23);
            this.TXTSheetName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sheet Name";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbEvents);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(113, 732);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(962, 100);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Events";
            // 
            // lbEvents
            // 
            this.lbEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEvents.FormattingEnabled = true;
            this.lbEvents.ItemHeight = 15;
            this.lbEvents.Location = new System.Drawing.Point(3, 19);
            this.lbEvents.Name = "lbEvents";
            this.lbEvents.Size = new System.Drawing.Size(956, 78);
            this.lbEvents.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tvFileList);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(1075, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(190, 832);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // tvFileList
            // 
            this.tvFileList.CheckBoxes = true;
            this.tvFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFileList.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.tvFileList.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tvFileList.Location = new System.Drawing.Point(3, 110);
            this.tvFileList.Name = "tvFileList";
            this.tvFileList.ShowNodeToolTips = true;
            this.tvFileList.Size = new System.Drawing.Size(184, 719);
            this.tvFileList.TabIndex = 1;
            this.tvFileList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFileList_AfterSelect);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.BTNReload);
            this.panel2.Controls.Add(this.BTNSet);
            this.panel2.Controls.Add(this.TXTOutputFolder);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 91);
            this.panel2.TabIndex = 0;
            // 
            // BTNReload
            // 
            this.BTNReload.Location = new System.Drawing.Point(90, 54);
            this.BTNReload.Name = "BTNReload";
            this.BTNReload.Size = new System.Drawing.Size(75, 23);
            this.BTNReload.TabIndex = 3;
            this.BTNReload.Text = "Reload";
            this.BTNReload.UseVisualStyleBackColor = true;
            this.BTNReload.Click += new System.EventHandler(this.BTNReload_Click);
            // 
            // BTNSet
            // 
            this.BTNSet.Location = new System.Drawing.Point(9, 54);
            this.BTNSet.Name = "BTNSet";
            this.BTNSet.Size = new System.Drawing.Size(75, 23);
            this.BTNSet.TabIndex = 2;
            this.BTNSet.Text = "Set";
            this.BTNSet.UseVisualStyleBackColor = true;
            this.BTNSet.Click += new System.EventHandler(this.BTNSet_Click);
            // 
            // TXTOutputFolder
            // 
            this.TXTOutputFolder.Location = new System.Drawing.Point(3, 25);
            this.TXTOutputFolder.Name = "TXTOutputFolder";
            this.TXTOutputFolder.Size = new System.Drawing.Size(172, 23);
            this.TXTOutputFolder.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output Folder";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbSheetNames);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(113, 832);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sheet List";
            // 
            // lbSheetNames
            // 
            this.lbSheetNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSheetNames.FormattingEnabled = true;
            this.lbSheetNames.ItemHeight = 15;
            this.lbSheetNames.Location = new System.Drawing.Point(3, 19);
            this.lbSheetNames.Name = "lbSheetNames";
            this.lbSheetNames.Size = new System.Drawing.Size(107, 810);
            this.lbSheetNames.TabIndex = 0;
            this.lbSheetNames.SelectedIndexChanged += new System.EventHandler(this.lbSheetNames_SelectedIndexChanged);
            this.lbSheetNames.DoubleClick += new System.EventHandler(this.lbSheetNames_DoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ExcelSheet
            // 
            this.ExcelSheet.AllowCellContextMenu = true;
            this.ExcelSheet.AllowExtendRowColumnCount = true;
            this.ExcelSheet.AllowFiltering = false;
            this.ExcelSheet.AllowFormulaRangeSelection = true;
            this.ExcelSheet.AllowTabItemContextMenu = true;
            this.ExcelSheet.AllowZooming = true;
            this.ExcelSheet.BaseThemeName = "";
            spreadsheetCopyPaste1.AllowPasteOptionPopup = true;
            spreadsheetCopyPaste1.DefaultPasteOption = Syncfusion.Windows.Forms.Spreadsheet.PasteOptions.Paste;
            this.ExcelSheet.CopyPaste = spreadsheetCopyPaste1;
            this.ExcelSheet.DefaultColumnCount = 101;
            this.ExcelSheet.DefaultRowCount = 101;
            this.ExcelSheet.DisplayAlerts = true;
            this.ExcelSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExcelSheet.FileName = "Book1";
            this.ExcelSheet.FormulaBarVisibility = true;
            formulaRangeSelectionController1.AllowMouseSelection = true;
            formulaRangeSelectionController1.AllowSelectionOnEditing = true;
            this.ExcelSheet.FormulaRangeSelectionController = formulaRangeSelectionController1;
            this.ExcelSheet.IsCustomTabItemContextMenuEnabled = false;
            this.ExcelSheet.Location = new System.Drawing.Point(3, 3);
            this.ExcelSheet.Name = "ExcelSheet";
            this.ExcelSheet.SelectedTabIndex = 0;
            this.ExcelSheet.SelectedTabItem = null;
            this.ExcelSheet.ShowBusyIndicator = true;
            this.ExcelSheet.Size = new System.Drawing.Size(948, 542);
            this.ExcelSheet.TabIndex = 0;
            this.ExcelSheet.TabItemContextMenu = null;
            this.ExcelSheet.Text = "OrginalData";
            this.ExcelSheet.ThemeName = "Default";
            // 
            // BTNShowExcel
            // 
            this.BTNShowExcel.Location = new System.Drawing.Point(273, 20);
            this.BTNShowExcel.Name = "BTNShowExcel";
            this.BTNShowExcel.Size = new System.Drawing.Size(49, 23);
            this.BTNShowExcel.TabIndex = 3;
            this.BTNShowExcel.Text = "Show";
            this.BTNShowExcel.UseVisualStyleBackColor = true;
            this.BTNShowExcel.Click += new System.EventHandler(this.BTNShowExcel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1265, 832);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Import Data";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDMaxCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDMaxRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDRow)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox5;
        private GroupBox groupBox3;
        private Panel panel2;
        private Button BTNSet;
        private TextBox TXTOutputFolder;
        private Label label2;
        private GroupBox groupBox2;
        private TreeView tvFileList;
        private Panel panel3;
        private Panel panel4;
        private GroupBox groupBox6;
        private Button BTNProcess;
        private ComboBox comboBox1;
        private Label label8;
        private GroupBox groupBox1;
        private Button BTNSelect;
        private TextBox TXTSelectedFile;
        private Label label1;
        private GroupBox groupBox4;
        private Label label6;
        private Label label4;
        private TextBox TXTSheetName;
        private Label label3;
        private Button BTNToJSON;
        private Label label7;
        private Label label5;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private ListBox lbSheetNames;
        private NumericUpDown NUDMaxCol;
        private NumericUpDown NUDMaxRow;
        private NumericUpDown NUDCol;
        private NumericUpDown NUDRow;
        private Label lbFileName;
        private TextBox TXTFileName;
        private Label label9;
        private Button BTNReload;
        private ListBox lbEvents;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView1;
        private Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet ExcelSheet;
        private Button BTNShowExcel;
    }
}