using AKS.Payroll.Database;
using AKS.Payroll.Forms.Inventory.Functions;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public partial class SalesForm : Form
    {
        private SalesManager _salesManager;

        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;

        public SalesForm()
        {
            InitializeComponent();
            _salesManager = new SalesManager(azureDb, localDb, null);
        }

        public SalesForm(InvoiceType type)
        {
            InitializeComponent();
            _salesManager = new SalesManager(azureDb, localDb, type);
        }

        private void AddToCart()
        {
            try
            {
                var si = new SaleItemVM
                {
                    Barcode = txtBarcode.Text.Trim(),
                    Rate = decimal.Parse(txtRate.Text.Trim()),
                    ProductItem = txtProductItem.Text.Trim(),
                    Qty = decimal.Parse(txtQty.Text.Trim()),
                    Amount = 0,
                    Tax = 0,
                };

                if (txtDiscount.Text.Contains('%'))
                {
                    si.Discount = si.Qty * si.Rate * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                }
                else
                    si.Discount = decimal.Parse(txtDiscount.Text.Trim());

                si.Amount = (si.Rate * si.Qty) - si.Discount;

                _salesManager.SaleItem.Add(si);
                txtBarcode.Text = "";
                txtQty.Text = "0";
                txtRate.Text = "0";
                txtProductItem.Text = "";
                txtDiscount.Text = "0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                btnAdd.Text = "Save";
                tabControl1.SelectedTab = tpEntry;
                LoadFormData();
                if (rbManual.Checked)
                {
                    if (cbSalesReturn.Checked)
                        cbxInvType.SelectedIndex = (int)InvoiceType.ManualSaleReturn;
                    else
                        cbxInvType.SelectedIndex = (int)InvoiceType.ManualSale;
                }
                else if (rbRegular.Checked)
                {
                    if (cbSalesReturn.Checked)
                        cbxInvType.SelectedIndex = (int)InvoiceType.SalesReturn;
                    else cbxInvType.SelectedIndex = (int)InvoiceType.Sales;
                }
                _salesManager.ResetCart();
                txtBarcode.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtBarcode.AutoCompleteSource = AutoCompleteSource.CustomSource;

                txtBarcode.AutoCompleteCustomSource = _salesManager.barcodeList;

            }
            else if (btnAdd.Text == "Edit")
            {
                LoadFormData();
                btnAdd.Text = "Save";
                tabControl1.SelectedTab = tpEntry;
            }
            else if (btnAdd.Text == "Save")
            {
                tabControl1.SelectedTab = tpView;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            _salesManager.AddNewCustomer(txtCustomerName.Text.Trim(), cbxMmobile.Text.Trim());
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (VerifyProductRow())
                AddToCart();
            else
                MessageBox.Show("Check Field before adding...");
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add";
            tabControl1.SelectedTab = tpList;
            _salesManager.ResetCart();
            dgvSaleItems.Rows.Clear();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentForm payForm = new PaymentForm();

            if (payForm.ShowDialog() == DialogResult.OK)
            {
                _salesManager.AddPayment(payForm.Pd);
                lbPd.Text += $"Mode: {payForm.Pd.Mode}\t Rs. {payForm.Pd.Amount}\n";
                // DisplayPayment();
            }
            else
            {
                MessageBox.Show("Some error occured while fetching payment details");
            }
        }

        private void cbSalesReturn_CheckedChanged(object sender, EventArgs e)
        {
            _salesManager.SetRadioButton(rbRegular.Checked, rbManual.Checked, cbSalesReturn.Checked);
        }

        private void cbxMmobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtCustomerName.Text = (string)cbxMmobile.SelectedValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DisplayPayment()
        {
            //TODO: Check uses and make amed
            if (_salesManager.PaymentDetails == null) return;
            lbPd.Text = "";
            foreach (var pd in _salesManager.PaymentDetails)
            {
                lbPd.Text += $"Mode: {pd.Mode}\t Rs. {pd.Amount}\n";
            }
        }

        private void DisplayStockInfo(StockInfo info)
        {
            if (info != null)
            {
                txtQty.Text = info.Qty.ToString();
                txtRate.Text = info.Rate.ToString();
                txtValue.Text = (info.Qty * info.Rate).ToString();
                txtProductItem.Text = info.ProductItem.ToString();
                txtDiscount.Text = "0";
            }
            else
            {
                MessageBox.Show("Stock Not Found!");
            }
        }

        private void HandleBarcodeEntry()
        {
            if (_salesManager.ReturnKey)
                DisplayStockInfo(_salesManager.GetItemDetail(txtBarcode.Text.Trim()));
        }

        private void LoadFormData()
        {
            try
            {
                lbPd.Text = "";
                cbCashBill.Checked = false;
                cbxInvType.Items.AddRange(Enum.GetNames(typeof(InvoiceType)));
                cbxMmobile.DisplayMember = "MobileNo";
                cbxMmobile.ValueMember = "CustomerName";
                cbxMmobile.DataSource = _salesManager.SetupFormData();
                dgvSaleItems.DataSource = _salesManager.SaleItem;
                _salesManager.LoadBarcodeList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            _salesManager.SetRadioButton(false, rbManual.Checked, cbSalesReturn.Checked);
        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
            _salesManager.SetRadioButton(rbRegular.Checked, false, cbSalesReturn.Checked);
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            _salesManager.InitManager();
            SetupForm();
            lbYearList.DataSource = _salesManager.YearList;
            dataGridView1.DataSource = _salesManager.SetGridView();
        }

        private void SetupForm()
        {
            //TODO: make it salemager and pass controls to function and see it works
            switch (_salesManager.InvoiceType)
            {
                case InvoiceType.Sales:
                    rbRegular.Checked = true;
                    this.Text = "Regular Invoice";
                    break;

                case InvoiceType.SalesReturn:
                    rbRegular.Checked = true;
                    cbSalesReturn.Checked = true;
                    this.Text = "Regular Sale's Invoice";
                    break;

                case InvoiceType.ManualSale:
                    rbManual.Checked = true;
                    this.Text = "Manual Invoice";
                    break;

                case InvoiceType.ManualSaleReturn:
                    rbManual.Checked = true;
                    cbSalesReturn.Checked = true;
                    this.Text = "Manual Sale's Return Invoice";
                    break;

                default:
                    rbManual.Checked = true;
                    this.Text = "Manual Invoice";
                    break;
            }
        }

        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                _salesManager.ReturnKey = true;
                HandleBarcodeEntry();
            }
        }
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            txtValue.Text = SalesManager.CalculateRate(txtDiscount.Text, txtQty.Text, txtRate.Text).ToString();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            txtValue.Text = SalesManager.CalculateRate(txtDiscount.Text, txtQty.Text, txtRate.Text).ToString();
        }

        private bool VerifyProductRow()
        {
            //TODO: Need validation code
            bool flag = true;

            if (txtBarcode.Text.Trim().Length <= 0) flag = false;
            if (txtQty.Text.Trim().Length <= 0)// isNumeric
                flag = false;
            if (txtDiscount.Text.Trim().Length <= 0) flag = false;
            if (txtRate.Text.Trim().Length <= 0) flag = false;
            if (txtValue.Text.Trim().Length <= 0) flag = false;
            return flag;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (SaleReports == null || SaleReports.Count == 0)
            {
                SaleReports = _salesManager.SaleReports("ARD");
            }
            tabControl1.SelectedTab = tpView;
            DisplaySaleReport();
        }
        private SortedDictionary<int, List<List<SaleReport>>> SaleReports;
        List<SaleReport> SaleReportsList;
        private void DisplaySaleReport()
        {
            DataGridView gv = new DataGridView();
            gv.AllowUserToAddRows = false;
            if (SaleReportsList == null || SaleReports.Count == 0)
            {

                SaleReportsList = new List<SaleReport>();
                foreach (var item in SaleReports)
                {
                    if (item.Value != null && item.Value.Count > 0)
                    {


                        foreach (var item1 in item.Value)
                        {
                            if (item1 != null && item1.Count > 0)
                            {
                                foreach (var item2 in item1)
                                {
                                    if (item2 != null)
                                        SaleReportsList.Add(item2);
                                }
                            }
                        }
                    }
                }
            }
            gv.DataSource = SaleReportsList;
            gv.Dock= DockStyle.Fill;
            pdfViewer.Hide();
            pdfViewer.Visible = false;
            tpView.Controls.Add(gv);
            gv.Show();
            gv.Visible = true;

        }
    }
}