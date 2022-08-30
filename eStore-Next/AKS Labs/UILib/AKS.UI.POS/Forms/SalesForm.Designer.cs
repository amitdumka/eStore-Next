namespace AKS.UI.POS.Forms
{
    partial class SalesForm
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
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings2 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings2 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesForm));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings2 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpList = new System.Windows.Forms.TabPage();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.tpEntry = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxMmobile = new System.Windows.Forms.ComboBox();
            this.lbTotalAmount = new System.Windows.Forms.Label();
            this.lbTotalQty = new System.Windows.Forms.Label();
            this.lbTotalItem = new System.Windows.Forms.Label();
            this.lbTotalDiscount = new System.Windows.Forms.Label();
            this.lbTotalTax = new System.Windows.Forms.Label();
            this.lbTotalFree = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
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
            this.cbxInvType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.rbTailoring = new System.Windows.Forms.RadioButton();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.lbPd = new System.Windows.Forms.Label();
            this.cbCashBill = new System.Windows.Forms.CheckBox();
            this.cbxSalesman = new System.Windows.Forms.ComboBox();
            this.tpView = new System.Windows.Forms.TabPage();
            this.pdfViewer = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbYearList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbLastInvoice = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbSalesReturn = new System.Windows.Forms.CheckBox();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.rbRegular = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.tpEntry.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).BeginInit();
            this.tpView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 469);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(129, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(911, 389);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
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
            this.tabControl1.Size = new System.Drawing.Size(905, 367);
            this.tabControl1.TabIndex = 0;
            // 
            // tpList
            // 
            this.tpList.Controls.Add(this.dgvSales);
            this.tpList.Location = new System.Drawing.Point(4, 24);
            this.tpList.Name = "tpList";
            this.tpList.Padding = new System.Windows.Forms.Padding(3);
            this.tpList.Size = new System.Drawing.Size(897, 339);
            this.tpList.TabIndex = 0;
            this.tpList.Text = "Invoices";
            this.tpList.UseVisualStyleBackColor = true;
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToOrderColumns = true;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSales.Location = new System.Drawing.Point(3, 3);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowTemplate.Height = 25;
            this.dgvSales.Size = new System.Drawing.Size(891, 333);
            this.dgvSales.TabIndex = 0;
            // 
            // tpEntry
            // 
            this.tpEntry.Controls.Add(this.tableLayoutPanel1);
            this.tpEntry.Location = new System.Drawing.Point(4, 24);
            this.tpEntry.Name = "tpEntry";
            this.tpEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntry.Size = new System.Drawing.Size(897, 339);
            this.tpEntry.TabIndex = 1;
            this.tpEntry.Text = "Entry";
            this.tpEntry.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxMmobile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalAmount, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalQty, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalItem, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalDiscount, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalTax, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalFree, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
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
            this.tableLayoutPanel1.Controls.Add(this.cbxInvType, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCustomerName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbTailoring, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddCustomer, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPayment, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbPd, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbCashBill, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbxSalesman, 6, 2);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(891, 333);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "MobileNo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Customer Name";
            // 
            // cbxMmobile
            // 
            this.cbxMmobile.FormattingEnabled = true;
            this.cbxMmobile.Location = new System.Drawing.Point(109, 3);
            this.cbxMmobile.Name = "cbxMmobile";
            this.cbxMmobile.Size = new System.Drawing.Size(121, 23);
            this.cbxMmobile.TabIndex = 1;
            this.cbxMmobile.SelectedIndexChanged += new System.EventHandler(this.cbxMmobile_SelectedIndexChanged);
            // 
            // lbTotalAmount
            // 
            this.lbTotalAmount.AutoSize = true;
            this.lbTotalAmount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotalAmount.Location = new System.Drawing.Point(448, 29);
            this.lbTotalAmount.Name = "lbTotalAmount";
            this.lbTotalAmount.Size = new System.Drawing.Size(79, 15);
            this.lbTotalAmount.TabIndex = 5;
            this.lbTotalAmount.Text = "Total Amount";
            // 
            // lbTotalQty
            // 
            this.lbTotalQty.AutoSize = true;
            this.lbTotalQty.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotalQty.Location = new System.Drawing.Point(3, 29);
            this.lbTotalQty.Name = "lbTotalQty";
            this.lbTotalQty.Size = new System.Drawing.Size(54, 15);
            this.lbTotalQty.TabIndex = 6;
            this.lbTotalQty.Text = "Total Qty";
            // 
            // lbTotalItem
            // 
            this.lbTotalItem.AutoSize = true;
            this.lbTotalItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotalItem.Location = new System.Drawing.Point(109, 29);
            this.lbTotalItem.Name = "lbTotalItem";
            this.lbTotalItem.Size = new System.Drawing.Size(72, 15);
            this.lbTotalItem.TabIndex = 7;
            this.lbTotalItem.Text = "Total Item(s)";
            // 
            // lbTotalDiscount
            // 
            this.lbTotalDiscount.AutoSize = true;
            this.lbTotalDiscount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotalDiscount.Location = new System.Drawing.Point(236, 29);
            this.lbTotalDiscount.Name = "lbTotalDiscount";
            this.lbTotalDiscount.Size = new System.Drawing.Size(82, 15);
            this.lbTotalDiscount.TabIndex = 8;
            this.lbTotalDiscount.Text = "Total Discount";
            // 
            // lbTotalTax
            // 
            this.lbTotalTax.AutoSize = true;
            this.lbTotalTax.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotalTax.Location = new System.Drawing.Point(342, 29);
            this.lbTotalTax.Name = "lbTotalTax";
            this.lbTotalTax.Size = new System.Drawing.Size(52, 15);
            this.lbTotalTax.TabIndex = 9;
            this.lbTotalTax.Text = "Total Tax";
            // 
            // lbTotalFree
            // 
            this.lbTotalFree.AutoSize = true;
            this.lbTotalFree.ForeColor = System.Drawing.Color.Maroon;
            this.lbTotalFree.Location = new System.Drawing.Point(554, 29);
            this.lbTotalFree.Name = "lbTotalFree";
            this.lbTotalFree.Size = new System.Drawing.Size(69, 15);
            this.lbTotalFree.TabIndex = 10;
            this.lbTotalFree.Text = "Free Item(s)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "Barcode";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(109, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 15);
            this.label11.TabIndex = 12;
            this.label11.Text = "Qty";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(554, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 15);
            this.label15.TabIndex = 16;
            this.label15.Text = "Value";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(3, 86);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(100, 23);
            this.txtBarcode.TabIndex = 4;
            this.txtBarcode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtBarcode_PreviewKeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(109, 86);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 23);
            this.txtQty.TabIndex = 5;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            // 
            // txtProductItem
            // 
            this.txtProductItem.Location = new System.Drawing.Point(236, 86);
            this.txtProductItem.Name = "txtProductItem";
            this.txtProductItem.Size = new System.Drawing.Size(100, 23);
            this.txtProductItem.TabIndex = 6;
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(342, 86);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(100, 23);
            this.txtRate.TabIndex = 7;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(448, 86);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 23);
            this.txtDiscount.TabIndex = 8;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(554, 86);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 23);
            this.txtValue.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(236, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 15);
            this.label13.TabIndex = 14;
            this.label13.Text = "Product Item";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(342, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Rate";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(448, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 15);
            this.label12.TabIndex = 13;
            this.label12.Text = "Discount";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(660, 86);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(75, 23);
            this.btnAddToCart.TabIndex = 10;
            this.btnAddToCart.Text = "+";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // dgvSaleItems
            // 
            this.dgvSaleItems.AllowUserToOrderColumns = true;
            this.dgvSaleItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvSaleItems, 7);
            this.dgvSaleItems.Location = new System.Drawing.Point(3, 115);
            this.dgvSaleItems.MinimumSize = new System.Drawing.Size(0, 250);
            this.dgvSaleItems.Name = "dgvSaleItems";
            this.dgvSaleItems.RowTemplate.Height = 25;
            this.dgvSaleItems.Size = new System.Drawing.Size(869, 250);
            this.dgvSaleItems.TabIndex = 24;
            // 
            // cbxInvType
            // 
            this.cbxInvType.FormattingEnabled = true;
            this.cbxInvType.Location = new System.Drawing.Point(660, 3);
            this.cbxInvType.Name = "cbxInvType";
            this.cbxInvType.Size = new System.Drawing.Size(121, 23);
            this.cbxInvType.TabIndex = 3;
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
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(342, 3);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(100, 23);
            this.txtCustomerName.TabIndex = 2;
            // 
            // rbTailoring
            // 
            this.rbTailoring.AutoSize = true;
            this.rbTailoring.Location = new System.Drawing.Point(660, 32);
            this.rbTailoring.Name = "rbTailoring";
            this.rbTailoring.Size = new System.Drawing.Size(89, 19);
            this.rbTailoring.TabIndex = 4;
            this.rbTailoring.TabStop = true;
            this.rbTailoring.Text = "Tailoring Bill";
            this.rbTailoring.UseVisualStyleBackColor = true;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(448, 3);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(75, 23);
            this.btnAddCustomer.TabIndex = 25;
            this.btnAddCustomer.Text = "+";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(3, 371);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(75, 23);
            this.btnPayment.TabIndex = 26;
            this.btnPayment.Text = "Payment";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(109, 368);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 15);
            this.label17.TabIndex = 27;
            this.label17.Text = "Payment Details";
            // 
            // lbPd
            // 
            this.lbPd.AutoSize = true;
            this.lbPd.Location = new System.Drawing.Point(236, 368);
            this.lbPd.Name = "lbPd";
            this.lbPd.Size = new System.Drawing.Size(0, 15);
            this.lbPd.TabIndex = 28;
            // 
            // cbCashBill
            // 
            this.cbCashBill.AutoSize = true;
            this.cbCashBill.Location = new System.Drawing.Point(342, 371);
            this.cbCashBill.Name = "cbCashBill";
            this.cbCashBill.Size = new System.Drawing.Size(78, 19);
            this.cbCashBill.TabIndex = 29;
            this.cbCashBill.Text = "Cash Paid";
            this.cbCashBill.UseVisualStyleBackColor = true;
            // 
            // cbxSalesman
            // 
            this.cbxSalesman.FormattingEnabled = true;
            this.cbxSalesman.Location = new System.Drawing.Point(660, 57);
            this.cbxSalesman.Name = "cbxSalesman";
            this.cbxSalesman.Size = new System.Drawing.Size(121, 23);
            this.cbxSalesman.TabIndex = 30;
            // 
            // tpView
            // 
            this.tpView.Controls.Add(this.pdfViewer);
            this.tpView.Location = new System.Drawing.Point(4, 24);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(897, 339);
            this.tpView.TabIndex = 2;
            this.tpView.Text = "View";
            this.tpView.UseVisualStyleBackColor = true;
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
            messageBoxSettings2.EnableNotification = true;
            this.pdfViewer.MessageBoxSettings = messageBoxSettings2;
            this.pdfViewer.MinimumZoomPercentage = 50;
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.PageBorderThickness = 1;
            pdfViewerPrinterSettings2.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings2.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings2.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings2.PrintLocation")));
            pdfViewerPrinterSettings2.ShowPrintStatusDialog = true;
            this.pdfViewer.PrinterSettings = pdfViewerPrinterSettings2;
            this.pdfViewer.ReferencePath = null;
            this.pdfViewer.ScrollDisplacementValue = 0;
            this.pdfViewer.ShowHorizontalScrollBar = true;
            this.pdfViewer.ShowVerticalScrollBar = true;
            this.pdfViewer.Size = new System.Drawing.Size(891, 333);
            this.pdfViewer.SpaceBetweenPages = 8;
            this.pdfViewer.TabIndex = 0;
            textSearchSettings2.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings2.HighlightAllInstance = true;
            textSearchSettings2.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfViewer.TextSearchSettings = textSearchSettings2;
            this.pdfViewer.ThemeName = "Default";
            this.pdfViewer.VerticalScrollOffset = 0;
            this.pdfViewer.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfViewer.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbYearList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 389);
            this.groupBox2.TabIndex = 1;
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
            this.lbYearList.Size = new System.Drawing.Size(123, 367);
            this.lbYearList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbLastInvoice);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1040, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // lbLastInvoice
            // 
            this.lbLastInvoice.AutoSize = true;
            this.lbLastInvoice.Location = new System.Drawing.Point(519, 27);
            this.lbLastInvoice.Name = "lbLastInvoice";
            this.lbLastInvoice.Size = new System.Drawing.Size(64, 15);
            this.lbLastInvoice.TabIndex = 5;
            this.lbLastInvoice.Text = "No Invoice";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(390, 26);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(309, 26);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
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
            this.panel3.Controls.Add(this.cbSalesReturn);
            this.panel3.Controls.Add(this.rbManual);
            this.panel3.Controls.Add(this.rbRegular);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(829, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(208, 36);
            this.panel3.TabIndex = 1;
            // 
            // cbSalesReturn
            // 
            this.cbSalesReturn.AutoSize = true;
            this.cbSalesReturn.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbSalesReturn.Location = new System.Drawing.Point(65, 0);
            this.cbSalesReturn.Name = "cbSalesReturn";
            this.cbSalesReturn.Size = new System.Drawing.Size(64, 36);
            this.cbSalesReturn.TabIndex = 2;
            this.cbSalesReturn.Text = " Return";
            this.cbSalesReturn.UseVisualStyleBackColor = true;
            this.cbSalesReturn.CheckedChanged += new System.EventHandler(this.cbSalesReturn_CheckedChanged);
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbManual.Location = new System.Drawing.Point(143, 0);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(65, 36);
            this.rbManual.TabIndex = 1;
            this.rbManual.TabStop = true;
            this.rbManual.Text = "Manual";
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.CheckedChanged += new System.EventHandler(this.rbManual_CheckedChanged);
            // 
            // rbRegular
            // 
            this.rbRegular.AutoSize = true;
            this.rbRegular.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbRegular.Location = new System.Drawing.Point(0, 0);
            this.rbRegular.Name = "rbRegular";
            this.rbRegular.Size = new System.Drawing.Size(65, 36);
            this.rbRegular.TabIndex = 0;
            this.rbRegular.TabStop = true;
            this.rbRegular.Text = "Regular";
            this.rbRegular.UseVisualStyleBackColor = true;
            this.rbRegular.CheckedChanged += new System.EventHandler(this.rbRegular_CheckedChanged);
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
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 447);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 469);
            this.Controls.Add(this.panel1);
            this.Name = "SalesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sales";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SalesForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.tpEntry.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).EndInit();
            this.tpView.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private ListBox lbYearList;
        private GroupBox groupBox1;
        private Panel panel3;
        private RadioButton rbManual;
        private RadioButton rbRegular;
        private Panel panel2;
        private Button btnCancle;
        private Button btnDelete;
        private Button btnAdd;
        private StatusStrip statusStrip1;
        private TabControl tabControl1;
        private TabPage tpList;
        private DataGridView dgvSales;
        private TabPage tpEntry;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private ComboBox cbxMmobile;
        private Label label3;
        private RadioButton rbTailoring;
        private Label lbTotalAmount;
        private Label lbTotalQty;
        private Label lbTotalItem;
        private Label lbTotalDiscount;
        private Label lbTotalTax;
        private Label lbTotalFree;
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
        private TabPage tpView;
        private Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView pdfViewer;
        private ComboBox cbxInvType;
        private TextBox txtCustomerName;
        private CheckBox cbSalesReturn;
        private Button btnAddCustomer;
        private Button btnPayment;
        private Label label17;
        private Label lbPd;
        private CheckBox cbCashBill;
        private Button btnRefresh;
        private Button btnView;
        private Button btnPrint;
        private ComboBox cbxSalesman;
        private Label lbLastInvoice;
    }
}