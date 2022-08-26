namespace AKS.POSBilling.Forms
{
    partial class PurchaseForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings1 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseForm));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpList = new System.Windows.Forms.TabPage();
            this.dgvPurchase = new System.Windows.Forms.DataGridView();
            this.tpEntry = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxMmobile = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtProductItem = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.dgvSaleItems = new System.Windows.Forms.DataGridView();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.lbPd = new System.Windows.Forms.Label();
            this.cbCashBill = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxInvType = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePickerAdv1 = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.dateTimePickerAdv2 = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.tpView = new System.Windows.Forms.TabPage();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pdfViewer = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbYearList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbWithStock = new System.Windows.Forms.CheckBox();
            this.rbStocks = new System.Windows.Forms.RadioButton();
            this.rbInvoices = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            this.tpEntry.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv1.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv2.Calendar)).BeginInit();
            this.tpView.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1254, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpList);
            this.tabControl1.Controls.Add(this.tpEntry);
            this.tabControl1.Controls.Add(this.tpView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1119, 348);
            this.tabControl1.TabIndex = 0;
            // 
            // tpList
            // 
            this.tpList.Controls.Add(this.dgvPurchase);
            this.tpList.Location = new System.Drawing.Point(4, 24);
            this.tpList.Name = "tpList";
            this.tpList.Padding = new System.Windows.Forms.Padding(3);
            this.tpList.Size = new System.Drawing.Size(1111, 320);
            this.tpList.TabIndex = 0;
            this.tpList.Text = "Invoices";
            this.tpList.UseVisualStyleBackColor = true;
            // 
            // dgvPurchase
            // 
            this.dgvPurchase.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dgvPurchase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPurchase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPurchase.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvPurchase.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvPurchase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPurchase.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgvPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchase.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPurchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchase.Location = new System.Drawing.Point(3, 3);
            this.dgvPurchase.Name = "dgvPurchase";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchase.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPurchase.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvPurchase.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.dgvPurchase.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvPurchase.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Red;
            this.dgvPurchase.RowTemplate.Height = 25;
            this.dgvPurchase.Size = new System.Drawing.Size(1105, 314);
            this.dgvPurchase.TabIndex = 0;
            // 
            // tpEntry
            // 
            this.tpEntry.Controls.Add(this.tableLayoutPanel1);
            this.tpEntry.Location = new System.Drawing.Point(4, 24);
            this.tpEntry.Name = "tpEntry";
            this.tpEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntry.Size = new System.Drawing.Size(1111, 320);
            this.tpEntry.TabIndex = 1;
            this.tpEntry.Text = "Entry";
            this.tpEntry.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label19, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxMmobile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label15, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBarcode, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtQty, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtProductItem, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtRate, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtDiscount, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtValue, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label14, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label12, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAddToCart, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvSaleItems, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtCustomerName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddCustomer, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPayment, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbPd, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbCashBill, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxInvType, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label20, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePickerAdv1, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePickerAdv2, 7, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1105, 314);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(660, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 15);
            this.label19.TabIndex = 31;
            this.label19.Text = "Invoice Type";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(109, 366);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 15);
            this.label18.TabIndex = 30;
            this.label18.Text = "Invoice Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Invoice No";
            // 
            // cbxMmobile
            // 
            this.cbxMmobile.FormattingEnabled = true;
            this.cbxMmobile.Location = new System.Drawing.Point(109, 3);
            this.cbxMmobile.Name = "cbxMmobile";
            this.cbxMmobile.Size = new System.Drawing.Size(121, 23);
            this.cbxMmobile.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(448, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Total Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Total Qty";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(109, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Total Item(s)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(236, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Total Discount";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(342, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "Total Tax";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(109, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 15);
            this.label11.TabIndex = 12;
            this.label11.Text = "Qty";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(554, 55);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 15);
            this.label15.TabIndex = 16;
            this.label15.Text = "Value";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(3, 84);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(100, 23);
            this.txtBarcode.TabIndex = 4;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(109, 84);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 23);
            this.txtQty.TabIndex = 5;
            // 
            // txtProductItem
            // 
            this.txtProductItem.Location = new System.Drawing.Point(236, 84);
            this.txtProductItem.Name = "txtProductItem";
            this.txtProductItem.Size = new System.Drawing.Size(100, 23);
            this.txtProductItem.TabIndex = 6;
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(342, 84);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(100, 23);
            this.txtRate.TabIndex = 7;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(448, 84);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 23);
            this.txtDiscount.TabIndex = 8;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(554, 84);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 23);
            this.txtValue.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(236, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 15);
            this.label13.TabIndex = 14;
            this.label13.Text = "Product Item";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(342, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Rate";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(448, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 15);
            this.label12.TabIndex = 13;
            this.label12.Text = "Discount";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(660, 84);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(75, 23);
            this.btnAddToCart.TabIndex = 10;
            this.btnAddToCart.Text = "+";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            // 
            // dgvSaleItems
            // 
            this.dgvSaleItems.AllowUserToOrderColumns = true;
            this.dgvSaleItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvSaleItems, 7);
            this.dgvSaleItems.Location = new System.Drawing.Point(3, 113);
            this.dgvSaleItems.MinimumSize = new System.Drawing.Size(0, 250);
            this.dgvSaleItems.Name = "dgvSaleItems";
            this.dgvSaleItems.RowTemplate.Height = 25;
            this.dgvSaleItems.Size = new System.Drawing.Size(869, 250);
            this.dgvSaleItems.TabIndex = 24;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(342, 3);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(100, 23);
            this.txtCustomerName.TabIndex = 2;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(448, 3);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(75, 23);
            this.btnAddCustomer.TabIndex = 25;
            this.btnAddCustomer.Text = "+";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(3, 369);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(75, 23);
            this.btnPayment.TabIndex = 26;
            this.btnPayment.Text = "Payment";
            this.btnPayment.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(236, 366);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 15);
            this.label17.TabIndex = 27;
            this.label17.Text = "Payment Details";
            // 
            // lbPd
            // 
            this.lbPd.AutoSize = true;
            this.lbPd.Location = new System.Drawing.Point(342, 366);
            this.lbPd.Name = "lbPd";
            this.lbPd.Size = new System.Drawing.Size(0, 15);
            this.lbPd.TabIndex = 28;
            // 
            // cbCashBill
            // 
            this.cbCashBill.AutoSize = true;
            this.cbCashBill.Location = new System.Drawing.Point(448, 369);
            this.cbCashBill.Name = "cbCashBill";
            this.cbCashBill.Size = new System.Drawing.Size(78, 19);
            this.cbCashBill.TabIndex = 29;
            this.cbCashBill.Text = "Cash Paid";
            this.cbCashBill.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Invoice Type";
            // 
            // cbxInvType
            // 
            this.cbxInvType.FormattingEnabled = true;
            this.cbxInvType.Location = new System.Drawing.Point(660, 3);
            this.cbxInvType.Name = "cbxInvType";
            this.cbxInvType.Size = new System.Drawing.Size(121, 23);
            this.cbxInvType.TabIndex = 3;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(660, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 15);
            this.label20.TabIndex = 32;
            this.label20.Text = "Invoice Type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "Barcode";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Maroon;
            this.label9.Location = new System.Drawing.Point(554, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "Free Item(s)";
            // 
            // dateTimePickerAdv1
            // 
            this.dateTimePickerAdv1.BorderColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.dateTimePickerAdv1.Calendar.AllowMultipleSelection = false;
            this.dateTimePickerAdv1.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerAdv1.Calendar.BottomHeight = 25;
            this.dateTimePickerAdv1.Calendar.Culture = new System.Globalization.CultureInfo("en-US");
            this.dateTimePickerAdv1.Calendar.DayNamesFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv1.Calendar.DaysFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv1.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerAdv1.Calendar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv1.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv1.Calendar.Iso8601CalenderFormat = false;
            this.dateTimePickerAdv1.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerAdv1.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerAdv1.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            this.dateTimePickerAdv1.Calendar.NoneButton.AutoSize = true;
            this.dateTimePickerAdv1.Calendar.NoneButton.Location = new System.Drawing.Point(115, 0);
            this.dateTimePickerAdv1.Calendar.NoneButton.Size = new System.Drawing.Size(72, 25);
            this.dateTimePickerAdv1.Calendar.NoneButton.Text = "None";
            this.dateTimePickerAdv1.Calendar.Size = new System.Drawing.Size(187, 174);
            this.dateTimePickerAdv1.Calendar.SizeToFit = true;
            this.dateTimePickerAdv1.Calendar.TabIndex = 0;
            // 
            // 
            // 
            this.dateTimePickerAdv1.Calendar.TodayButton.AutoSize = true;
            this.dateTimePickerAdv1.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerAdv1.Calendar.TodayButton.Size = new System.Drawing.Size(115, 25);
            this.dateTimePickerAdv1.Calendar.TodayButton.Text = "Today";
            this.dateTimePickerAdv1.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv1.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimePickerAdv1.DropDownImage = null;
            this.dateTimePickerAdv1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv1.Location = new System.Drawing.Point(878, 32);
            this.dateTimePickerAdv1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerAdv1.MinValue = new System.DateTime(((long)(0)));
            this.dateTimePickerAdv1.Name = "dateTimePickerAdv1";
            this.dateTimePickerAdv1.Size = new System.Drawing.Size(232, 20);
            this.dateTimePickerAdv1.TabIndex = 33;
            this.dateTimePickerAdv1.Value = new System.DateTime(2022, 7, 22, 15, 36, 41, 983);
            // 
            // dateTimePickerAdv2
            // 
            this.dateTimePickerAdv2.BorderColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.dateTimePickerAdv2.Calendar.AllowMultipleSelection = false;
            this.dateTimePickerAdv2.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerAdv2.Calendar.BottomHeight = 25;
            this.dateTimePickerAdv2.Calendar.Culture = new System.Globalization.CultureInfo("en-US");
            this.dateTimePickerAdv2.Calendar.DayNamesFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv2.Calendar.DaysFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv2.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerAdv2.Calendar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv2.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv2.Calendar.Iso8601CalenderFormat = false;
            this.dateTimePickerAdv2.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerAdv2.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerAdv2.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            this.dateTimePickerAdv2.Calendar.NoneButton.AutoSize = true;
            this.dateTimePickerAdv2.Calendar.NoneButton.Location = new System.Drawing.Point(115, 0);
            this.dateTimePickerAdv2.Calendar.NoneButton.Size = new System.Drawing.Size(72, 25);
            this.dateTimePickerAdv2.Calendar.NoneButton.Text = "None";
            this.dateTimePickerAdv2.Calendar.Size = new System.Drawing.Size(187, 174);
            this.dateTimePickerAdv2.Calendar.SizeToFit = true;
            this.dateTimePickerAdv2.Calendar.TabIndex = 0;
            // 
            // 
            // 
            this.dateTimePickerAdv2.Calendar.TodayButton.AutoSize = true;
            this.dateTimePickerAdv2.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerAdv2.Calendar.TodayButton.Size = new System.Drawing.Size(115, 25);
            this.dateTimePickerAdv2.Calendar.TodayButton.Text = "Today";
            this.dateTimePickerAdv2.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv2.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimePickerAdv2.DropDownImage = null;
            this.dateTimePickerAdv2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerAdv2.Location = new System.Drawing.Point(878, 58);
            this.dateTimePickerAdv2.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerAdv2.MinValue = new System.DateTime(((long)(0)));
            this.dateTimePickerAdv2.Name = "dateTimePickerAdv2";
            this.dateTimePickerAdv2.Size = new System.Drawing.Size(232, 20);
            this.dateTimePickerAdv2.TabIndex = 34;
            this.dateTimePickerAdv2.Value = new System.DateTime(2022, 7, 22, 15, 36, 46, 60);
            // 
            // tpView
            // 
            this.tpView.Controls.Add(this.listBox3);
            this.tpView.Controls.Add(this.listBox2);
            this.tpView.Controls.Add(this.listBox1);
            this.tpView.Controls.Add(this.label16);
            this.tpView.Controls.Add(this.pdfViewer);
            this.tpView.Location = new System.Drawing.Point(4, 24);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(1111, 320);
            this.tpView.TabIndex = 2;
            this.tpView.Text = "View";
            this.tpView.UseVisualStyleBackColor = true;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 15;
            this.listBox3.Location = new System.Drawing.Point(751, 17);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(123, 304);
            this.listBox3.TabIndex = 4;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(576, 13);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(123, 304);
            this.listBox2.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(6, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(549, 304);
            this.listBox1.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 15);
            this.label16.TabIndex = 1;
            this.label16.Text = "label16";
            // 
            // pdfViewer
            // 
            this.pdfViewer.AutoScroll = true;
            this.pdfViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.pdfViewer.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.EnableContextMenu = true;
            this.pdfViewer.HorizontalScrollOffset = 0;
            this.pdfViewer.IsTextSearchEnabled = true;
            this.pdfViewer.IsTextSelectionEnabled = true;
            this.pdfViewer.Location = new System.Drawing.Point(3, 3);
            messageBoxSettings1.EnableNotification = true;
            this.pdfViewer.MessageBoxSettings = messageBoxSettings1;
            this.pdfViewer.MinimumZoomPercentage = 50;
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            this.pdfViewer.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfViewer.ReferencePath = null;
            this.pdfViewer.ScrollDisplacementValue = 0;
            this.pdfViewer.ShowHorizontalScrollBar = true;
            this.pdfViewer.ShowVerticalScrollBar = true;
            this.pdfViewer.Size = new System.Drawing.Size(1105, 314);
            this.pdfViewer.SpaceBetweenPages = 8;
            this.pdfViewer.TabIndex = 0;
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfViewer.TextSearchSettings = textSearchSettings1;
            this.pdfViewer.ThemeName = "Default";
            this.pdfViewer.VerticalScrollOffset = 0;
            this.pdfViewer.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfViewer.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(129, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1125, 370);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbYearList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 370);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Year List";
            // 
            // lbYearList
            // 
            this.lbYearList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbYearList.FormattingEnabled = true;
            this.lbYearList.ItemHeight = 15;
            this.lbYearList.Location = new System.Drawing.Point(3, 19);
            this.lbYearList.Name = "lbYearList";
            this.lbYearList.Size = new System.Drawing.Size(123, 348);
            this.lbYearList.TabIndex = 0;
            this.lbYearList.SelectedIndexChanged += new System.EventHandler(this.lbYearList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1254, 58);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Location = new System.Drawing.Point(228, 19);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 36);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbWithStock);
            this.panel3.Controls.Add(this.rbStocks);
            this.panel3.Controls.Add(this.rbInvoices);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1004, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(247, 36);
            this.panel3.TabIndex = 1;
            // 
            // cbWithStock
            // 
            this.cbWithStock.AutoSize = true;
            this.cbWithStock.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbWithStock.Location = new System.Drawing.Point(68, 0);
            this.cbWithStock.Name = "cbWithStock";
            this.cbWithStock.Size = new System.Drawing.Size(83, 36);
            this.cbWithStock.TabIndex = 2;
            this.cbWithStock.Text = "With Stock";
            this.cbWithStock.UseVisualStyleBackColor = true;
            // 
            // rbStocks
            // 
            this.rbStocks.AutoSize = true;
            this.rbStocks.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbStocks.Location = new System.Drawing.Point(165, 0);
            this.rbStocks.Name = "rbStocks";
            this.rbStocks.Size = new System.Drawing.Size(82, 36);
            this.rbStocks.TabIndex = 1;
            this.rbStocks.TabStop = true;
            this.rbStocks.Text = "Only Stock";
            this.rbStocks.UseVisualStyleBackColor = true;
            this.rbStocks.CheckedChanged += new System.EventHandler(this.rbStocks_CheckedChanged);
            // 
            // rbInvoices
            // 
            this.rbInvoices.AutoSize = true;
            this.rbInvoices.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbInvoices.Location = new System.Drawing.Point(0, 0);
            this.rbInvoices.Name = "rbInvoices";
            this.rbInvoices.Size = new System.Drawing.Size(68, 36);
            this.rbInvoices.TabIndex = 0;
            this.rbInvoices.TabStop = true;
            this.rbInvoices.Text = "Invoices";
            this.rbInvoices.UseVisualStyleBackColor = true;
            this.rbInvoices.CheckedChanged += new System.EventHandler(this.rbInvoices_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.btnCancle);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 36);
            this.panel2.TabIndex = 0;
            // 
            // btnCancle
            // 
            this.btnCancle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancle.Location = new System.Drawing.Point(150, 0);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 36);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "Cancle";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Location = new System.Drawing.Point(75, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 36);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // PurchaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "PurchaseForm";
            this.Text = "PurchaseForm";
            this.Load += new System.EventHandler(this.PurchaseForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            this.tpEntry.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv1.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv2.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerAdv2)).EndInit();
            this.tpView.ResumeLayout(false);
            this.tpView.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private TabControl tabControl1;
        private TabPage tpList;
        private DataGridView dgvPurchase;
        private TabPage tpEntry;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private ComboBox cbxMmobile;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label15;
        private TextBox txtBarcode;
        private TextBox txtQty;
        private TextBox txtProductItem;
        private TextBox txtRate;
        private TextBox txtDiscount;
        private TextBox txtValue;
        private Label label13;
        private Label label14;
        private Label label12;
        private Button btnAddToCart;
        private DataGridView dgvSaleItems;
        private TextBox txtCustomerName;
        private Button btnAddCustomer;
        private Button btnPayment;
        private Label label17;
        private Label lbPd;
        private CheckBox cbCashBill;
        private Label label3;
        private ComboBox cbxInvType;
        private TabPage tpView;
        private Label label16;
        private Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView pdfViewer;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private ListBox lbYearList;
        private GroupBox groupBox1;
        private Button btnRefresh;
        private Panel panel3;
        private CheckBox cbWithStock;
        private RadioButton rbStocks;
        private RadioButton rbInvoices;
        private Panel panel2;
        private Button btnCancle;
        private Button btnDelete;
        private Button btnAdd;
        private Label label19;
        private Label label18;
        private Label label20;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimePickerAdv1;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimePickerAdv2;
        private ListBox listBox3;
        private ListBox listBox2;
        private ListBox listBox1;
    }
}