using AKS.Payroll.Database;
using AKS.POSBilling.Commons;
using AKS.POSBilling.Controllers;
using AKS.POSBilling.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.POSBilling.Forms
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
                    Discount = 0
                };

                if (txtDiscount.Text.Contains('%'))
                {
                    si.Discount = si.Qty * si.Rate * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                }
                else
                    si.Discount = decimal.Parse(txtDiscount.Text.Trim());

                si.Amount = (si.Rate * si.Qty) - si.Discount;

                //Adding to List
                _salesManager.SaleItem.Add(si);
                //Update Cart total

                //TOD:Count based on unit if fabric then 1 and if rmz then stock unit
                _salesManager.TotalItem++;
                //TODO: 100% discount

                _salesManager.TotalFreeQty += 0;
                _salesManager.TotalQty += si.Qty;
                _salesManager.TotalDiscount += si.Discount;
                //_salesManager.TotalTax += si.Tax;
                _salesManager.TotalAmount += si.Amount;
                UpdateCartTotal();
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
                //TODO: Not implement whole.
                LoadFormData();
                btnAdd.Text = "Save";
                tabControl1.SelectedTab = tpEntry;
            }
            else if (btnAdd.Text == "Save")
            {
                //Check for Payment
                if (CheckPayment())
                {
                    if (SaveSaleData())
                    {
                        MessageBox.Show($"Invoice is Saved! \n Invoice No is {_salesManager.LastInvoice.InvoiceNo}");
                        pdfViewer.Load(_salesManager.LastInvoicePath);
                        pdfViewer.Visible = true;
                        tabControl1.SelectedTab = tpView;
                        filename = _salesManager.LastInvoicePath;
                        lbLastInvoice.Text = _salesManager.LastInvoice.InvoiceNo;
                        btnAdd.Text = "Add";
                        PostPreFormReset();

                        // Ask to print or email
                    }
                }
                else
                {
                    MessageBox.Show("Payment Details is not added! Add payment details or select cash paid if cash bill", "Payment Error");
                }
            }
        }

        private bool CheckPayment()
        {
            if (cbCashBill.Checked) return true;
            else
            {
                if (_salesManager.PaymentDetails == null || _salesManager.PaymentDetails.Count == 0)
                    return false;
                else return true;
            }
        }

        private void PostPreFormReset()
        {
            _salesManager.ResetCart();
            UpdateCartTotal();
            dgvSaleItems.DataSource = null;
            cbxMmobile.SelectedIndex = 0;

            //TODO:Reset Form to save New Invoice
        }

        private void UpdateCartTotal()
        {
            lbTotalAmount.Text = $"Total Amount: Rs {_salesManager.TotalAmount} ";
            lbTotalDiscount.Text = $"Discount Amount: Rs {_salesManager.TotalDiscount} ";
            lbTotalFree.Text = $"Free Qty(s): Rs {_salesManager.TotalFreeQty} ";
            lbTotalQty.Text = $"Total Qty(s): Rs {_salesManager.TotalFreeQty} ";
            lbTotalItem.Text = $"Total Items(s): Rs {_salesManager.TotalItem} ";
            lbTotalTax.Text = $"Tax Amt: Rs {_salesManager.TotalTax} ";
        }

        /// <summary>
        /// Save Invoice
        /// </summary>
        /// <returns></returns>
        private bool SaveSaleData()
        {
            return _salesManager.SaveInvoice(cbxMmobile.Text.Trim(), txtCustomerName.Text.Trim(), cbxSalesman.SelectedValue.ToString(), (InvoiceType)cbxInvType.SelectedIndex, cbCashBill.Checked, rbTailoring.Checked);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            //TODO: Implement a Form or Dialog to take customer details
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
            PaymentForm payForm = new PaymentForm(azureDb);

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
            //Implementing Tailoring bill
            if (_salesManager.ReturnKey)
                if(rbTailoring.Checked)
                    DisplayStockInfo(_salesManager.GetItemDetail(txtBarcode.Text.Trim(),true));
                else
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

                //TODO: make static data or use json and store in a file and read it once a day.
                //make less database use on remote machine
                cbxMmobile.DataSource = _salesManager.SetupFormData();
                dgvSaleItems.DataSource = _salesManager.SaleItem;
                _salesManager.LoadBarcodeList();

                cbxSalesman.DataSource = azureDb.Salesmen.Where(c => c.IsActive).Select(c => new { c.SalesmanId, c.Name }).ToList();
                cbxSalesman.DisplayMember = "Name";
                cbxSalesman.ValueMember = "SalesmanId";
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
            if (azureDb == null) azureDb = new AzurePayrollDbContext();
            if (localDb == null) localDb = new LocalPayrollDbContext();
            SetupForm();
            lbYearList.DataSource = _salesManager.YearList;
            dgvSales.DataSource = _salesManager.SetGridView();
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
                SaleReports = _salesManager.SaleReports("ARD", _salesManager.InvoiceType);
            }
            tabControl1.SelectedTab = tpView;
            DisplaySaleReport();

        }
        private void RemoveTestInv()
        {

            var invs = azureDb.ProductSales
                .Where(c => c.InvoiceType == InvoiceType.ManualSale
            && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month
            ).ToList();

            foreach (var inv in invs)
            {
                var items = azureDb.SaleItems.Where(c => c.InvoiceCode == inv.InvoiceCode).ToList();
                //foreach (var item in items)
                //{
                //    var stock = azureDb.Stocks.Find(item.Barcode);
                //    stock.HoldQty -= item.BilledQty + item.FreeQty;
                //    azureDb.Stocks.Update(stock);
                //}
                azureDb.SaleItems.RemoveRange(items);
                var pay = azureDb.SalePaymentDetails.Where(c => c.InvoiceCode == inv.InvoiceCode).ToList();
                azureDb.SalePaymentDetails.RemoveRange(pay);
                var card = azureDb.CardPaymentDetails.Where(c => c.InvoiceCode == inv.InvoiceCode).ToList();
                if (card != null)
                    azureDb.CardPaymentDetails.RemoveRange(card);
                azureDb.ProductSales.Remove(inv);

            }

            int x = azureDb.SaveChanges();
            MessageBox.Show("Removed " + x);
        }

        private SortedDictionary<int, List<List<SaleReport>>> SaleReports;
        private List<SaleReport> SaleReportsList;
        private DataGridView gv = new DataGridView();

        private void DisplaySaleReport()
        {
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
            gv.Dock = DockStyle.Fill;
            pdfViewer.Hide();
            pdfViewer.Visible = false;
            tpView.Controls.Add(gv);
            gv.Show();
            gv.Visible = true;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string fn = new SaleHelper().ToPdf(SaleReportsList);
            if (!string.IsNullOrEmpty(fn))
            {
                gv.Visible = false;
                pdfViewer.Load(fn);
                pdfViewer.Visible = true;
                this.tabControl1.SelectedTab = tpView;
                btnPrint.Enabled = true;
            }
        }

        private string filename = "";
        private int count = 0;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            pdfViewer.Visible = true;
            var result = MessageBox.Show("Want to Print", "Print PDF", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var printDialog1 = new PrintDialog();
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowPrintToFile = true;
                    pdfViewer.Print(printDialog1.PrinterSettings.PrinterName);
                    count++;
                    if (count > 2) { count = 0; filename = ""; }
                }
            }
        }
    }
}

//000000887H 16.00   3.20
//101002  62.00   0.00
//2599625 10.00   2.00
//2608389 10.00   10.00
//2609377 10.00   3.00
//2686388 2.00    0.00
//2700322 10.00   17.00
//2700323 10.00   12.00
//2700324 10.00   6.00
//3635160 16.00   2.90
//3635175 16.00   3.40
//3635177 16.00   3.20
//3635182 90.00   36.00
//3635186 16.00   1.80
//3635193 15.00   4.80
//510000000472    1.00    0.00
//510000000473    2.00    4.00
//510000000474    1.00    1.00
//510000000475    1.00    1.00
//510000000482    1.00    0.00
//510000000483    2.00    1.00
//510000000484    2.00    1.00
//510000000485    1.00    0.00
//510000000486    1.00    0.00
//510000000786    1.00    1.00
//510000000787    2.00    4.00
//510000000788    2.00    0.00
//510000000789    1.00    1.00
//510000001054    1.00    1.00
//510000001055    3.00    7.00