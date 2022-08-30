/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.Payroll.Database;
using AKS.PosSystem.Helpers;
using AKS.PosSystem.Models.VM;
using AKS.PosSystem.Reports;
using AKS.PosSystem.ViewModels;
using AKS.Shared.Commons.Ops;
using System.Data;

namespace AKS.UI.POS.Forms
{
    public partial class SalesForm : Form
    {
        // private SalesManager _salesManager;

        private SalesViewModel _saleViewModel;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private AutoCompleteStringCollection barcodeAutoSource;
        private void Alerts(string msg, AlertType type)
        {
            string alertT = "Alert";
            MessageBoxIcon icon = MessageBoxIcon.Asterisk;
            switch (type)
            {
                case AlertType.Normal:
                    break;

                case AlertType.Info:
                    icon = MessageBoxIcon.Information;
                    alertT = "Infomation";
                    break;

                case AlertType.Error:
                    icon = MessageBoxIcon.Error;
                    alertT = "Error";
                    break;

                case AlertType.Warning:
                    icon = MessageBoxIcon.Warning;
                    alertT = "Warning";
                    break;

                default:
                    break;
            }
            MessageBox.Show(msg, alertT, MessageBoxButtons.OK, icon);
        }

        public SalesForm()
        {
            InitializeComponent();
            _saleViewModel = new SalesViewModel(InvoiceType.ManualSale, Alerts);
        }

        public SalesForm(InvoiceType type)
        {
            InitializeComponent();
            _saleViewModel = new SalesViewModel(type, Alerts);
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
                _saleViewModel.SaleItemVMs.Add(si);
                //Update Cart total

                //TOD:Count based on unit if fabric then 1 and if rmz then stock unit
                _saleViewModel.TotalItem++;
                //TODO: 100% discount

                _saleViewModel.TotalFreeQty += 0;
                _saleViewModel.TotalQty += si.Qty;
                _saleViewModel.TotalDiscount += si.Discount;
                //_saleViewModel.TotalTax += si.Tax;
                _saleViewModel.TotalAmount += si.Amount;
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
                _saleViewModel.ResetCart();

                txtBarcode.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtBarcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtBarcode.AutoCompleteCustomSource = barcodeAutoSource;
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
                        MessageBox.Show($"Invoice is Saved! \n Invoice No is {_saleViewModel.LastInvoice.InvoiceNo}");
                        pdfViewer.Load(_saleViewModel.LastInvoicePath);
                        pdfViewer.Visible = true;
                        tabControl1.SelectedTab = tpView;
                        filename = _saleViewModel.LastInvoicePath;
                        lbLastInvoice.Text = _saleViewModel.LastInvoice.InvoiceNo;
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
                if (_saleViewModel.PaymentDetails == null || _saleViewModel.PaymentDetails.Count == 0)
                    return false;
                else return true;
            }
        }

        private void PostPreFormReset()
        {
            _saleViewModel.ResetCart();
            UpdateCartTotal();
            dgvSaleItems.DataSource = null;
            cbxMmobile.SelectedIndex = 0;

            //TODO:Reset Form to save New Invoice
        }

        private void UpdateCartTotal()
        {
            lbTotalAmount.Text = $"Total Amount: Rs {_saleViewModel.TotalAmount} ";
            lbTotalDiscount.Text = $"Discount Amount: Rs {_saleViewModel.TotalDiscount} ";
            lbTotalFree.Text = $"Free Qty(s): Rs {_saleViewModel.TotalFreeQty} ";
            lbTotalQty.Text = $"Total Qty(s): Rs {_saleViewModel.TotalFreeQty} ";
            lbTotalItem.Text = $"Total Items(s): Rs {_saleViewModel.TotalItem} ";
            lbTotalTax.Text = $"Tax Amt: Rs {_saleViewModel.TotalTax} ";
        }

        /// <summary>
        /// Save Invoice
        /// </summary>
        /// <returns></returns>
        private bool SaveSaleData()
        {
            return _saleViewModel.SaveInvoice(cbxMmobile.Text.Trim(), txtCustomerName.Text.Trim(), cbxSalesman.SelectedValue.ToString(), (InvoiceType)cbxInvType.SelectedIndex, cbCashBill.Checked, rbTailoring.Checked);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            //TODO: Implement a Form or Dialog to take customer details
            _saleViewModel.AddNewCustomer(txtCustomerName.Text.Trim(), cbxMmobile.Text.Trim());
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
            _saleViewModel.ResetCart();
            dgvSaleItems.Rows.Clear();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentForm payForm = new PaymentForm(azureDb);

            if (payForm.ShowDialog() == DialogResult.OK)
            {
                _saleViewModel.AddToPaymentList(payForm.Pd);
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
            _saleViewModel.SetRadioButton(rbRegular.Checked, rbManual.Checked, cbSalesReturn.Checked);
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
            if (_saleViewModel.PaymentDetails == null) return;
            lbPd.Text = "";
            foreach (var pd in _saleViewModel.PaymentDetails)
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
            if (_saleViewModel.ReturnKey)
                if (rbTailoring.Checked)
                    DisplayStockInfo(_saleViewModel.GetItemDetail(txtBarcode.Text.Trim(), true));
                else
                    DisplayStockInfo(_saleViewModel.GetItemDetail(txtBarcode.Text.Trim()));
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
                cbxMmobile.DataSource = _saleViewModel.SetupFormData();
                dgvSaleItems.DataSource = _saleViewModel.SaleItemVMs;
                if (barcodeAutoSource == null || barcodeAutoSource.Count <= 0)
                {
                    var x = _saleViewModel.LoadBarcodeList();
                    foreach (var item in x)
                        barcodeAutoSource.Add(item);
                }


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
            _saleViewModel.SetRadioButton(false, rbManual.Checked, cbSalesReturn.Checked);
        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
            _saleViewModel.SetRadioButton(rbRegular.Checked, false, cbSalesReturn.Checked);
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            _saleViewModel.InitViewModel();
            if (azureDb == null) azureDb = new AzurePayrollDbContext();
            if (localDb == null) localDb = new LocalPayrollDbContext();
            SetupForm();
            lbYearList.DataSource = _saleViewModel.YearList;
            dgvSales.DataSource = _saleViewModel.SetGridView();
        }

        private void SetupForm()
        {
            //TODO: make it salemager and pass controls to function and see it works
            switch (_saleViewModel.InvoiceType)
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
                _saleViewModel.ReturnKey = true;
                HandleBarcodeEntry();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            txtValue.Text = SaleStatic.CalculateRate(txtDiscount.Text, txtQty.Text, txtRate.Text).ToString();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            txtValue.Text = SaleStatic.CalculateRate(txtDiscount.Text, txtQty.Text, txtRate.Text).ToString();
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
                var sr = new SaleReports(_saleViewModel.DataModel);
                SaleReports = sr.SaleReport(CurrentSession.StoreCode, _saleViewModel.InvoiceType);
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

        private SortedDictionary<int, List<List<SaleReportVM>>> SaleReports;
        private List<SaleReportVM> SaleReportsList;
        private DataGridView gv = new DataGridView();

        private void DisplaySaleReport()
        {
            gv.AllowUserToAddRows = false;
            if (SaleReportsList == null || SaleReports.Count == 0)
            {
                SaleReportsList = new List<SaleReportVM>();
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