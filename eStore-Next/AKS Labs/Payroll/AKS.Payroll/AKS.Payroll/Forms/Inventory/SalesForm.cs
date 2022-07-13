using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms.Inventory
{
    public partial class SalesForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private InvoiceType InvoiceType;
        private bool IsNew;
        private ObservableListSource<ProductSale> Items;
        private LocalPayrollDbContext localDb;
        private bool ReturnKey = false;
        private ProductSale Sale;
        private ObservableListSource<SaleItemVM> SaleItem;
        private List<SaleItem> SalesItems;
        private int SeletedYear;
        private int TotalCount;
        private List<PaymentDetail> PaymentDetails;

        // Cart Information
        private decimal TotalQty, TotalFreeQty, TotalTax, TotalDiscount, TotalAmount;

        private List<int> YearList;

        public SalesForm()
        {
            InitializeComponent();
            InvoiceType = InvoiceType.ManualSale;
        }

        public SalesForm(InvoiceType type)
        {
            InitializeComponent();
            InvoiceType = type;
        }

        private static int SetTaxRate(ProductCategory category, decimal Price)
        {
            int rate = 0;
            switch (category)
            {
                case ProductCategory.Fabric:
                    rate = 5;
                    break;

                case ProductCategory.ReadyMade:
                    rate = Price > 999 ? 12 : 5;
                    break;

                case ProductCategory.Accessiories:
                    rate = 12;
                    break;

                case ProductCategory.Tailoring:
                    rate = 5;
                    break;

                case ProductCategory.Trims:
                    rate = 5;
                    break;

                case ProductCategory.PromoItems:
                    rate = 0;
                    break;

                case ProductCategory.Coupons:
                    rate = 0;
                    break;

                case ProductCategory.GiftVouchers:
                    rate = 0;
                    break;

                case ProductCategory.Others:
                    rate = 18;
                    break;

                default:
                    rate = 5;
                    break;
            }
            return rate;
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

                SaleItem.Add(si);
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

        private void BasicRateCalucaltion(decimal mrp, decimal taxRate)
        {
            decimal price = (100 * mrp) / (100 + taxRate);
            decimal taxAmount = mrp - price;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                LoadFormData();
                btnAdd.Text = "Save";
                tabControl1.SelectedTab = tpEntry;
               
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
                ResetCart();

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
            Customer c = new Customer
            {
                City = "Dumka",
                Age = 30,
                DateOfBirth = DateTime.Today.AddYears(-30).Date,
                Gender = Gender.Male,
                MobileNo = cbxMmobile.Text.Trim(),
                NoOfBills = 0,
                OnDate = DateTime.Today,
                TotalAmount = 0
            };
            var cname = txtCustomerName.Text.Trim().Split(' ');
            c.FirstName = cname[0];
            for (int x = 1; x < cname.Length; x++)
                c.LastName += cname[x] + " ";
            c.LastName = c.LastName.Trim();
            if (azureDb.Customers.Any(C => C.MobileNo == cbxMmobile.Text.Trim()))
            {
                return;
            }
            else
            {
                azureDb.Customers.Add(c);
                if (azureDb.SaveChanges() > 0) MessageBox.Show("Customer Added");
            }
        }

        private bool VerifyProductRow()
        {
            bool flag = true;

            if (txtBarcode.Text.Trim().Length <= 0) flag = false;
            if (txtQty.Text.Trim().Length <= 0)// isNumeric
                flag = false;
            if (txtDiscount.Text.Trim().Length <= 0) flag = false;
            if (txtRate.Text.Trim().Length <= 0) flag = false;
            if (txtValue.Text.Trim().Length <= 0) flag = false;
            return flag;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (VerifyProductRow())
                AddToCart();
            else
                MessageBox.Show("Check Field before adding...");
        }

        private void cbSalesReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSalesReturn.Checked)
            {
                if (rbManual.Checked)
                    InvoiceType = InvoiceType.ManualSaleReturn;
                else if (rbRegular.Checked)
                    InvoiceType = InvoiceType.SalesReturn;
            }
            else
            {
                if (rbManual.Checked)
                    InvoiceType = InvoiceType.ManualSale;
                else if (rbManual.Checked)
                    InvoiceType = InvoiceType.Sales;
            }
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

        /// <summary>
        /// return stock info. Add to API/DataModel
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private StockInfo? GetItemDetail(string barcode)
        {
            if (barcode.Length < 10)
            {
                return null;
            }
            var item = azureDb.Stocks.Include(c => c.Product).Where(c => c.Barcode == barcode)
                .Select(item =>
           new StockInfo()
           {
               Barcode = item.Barcode,
               HoldQty = item.CurrentQtyWH,
               Qty = item.CurrentQty,
               Rate = item.Product.MRP,
               ProductItem = item.Product.Name,
               TaxType = item.Product.TaxType,
               Unit = item.Product.Unit,
               Category = item.Product.ProductCategory,
               TaxRate = SetTaxRate(item.Product.ProductCategory, item.Product.MRP)
           }).FirstOrDefault();
            return item;
        }

        private void HandleBarcodeEntry()
        {
            if (ReturnKey)
                DisplayStockInfo(GetItemDetail(txtBarcode.Text.Trim()));
        }

        private void LoadData()
        {
            SetupForm();
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            Items = new ObservableListSource<ProductSale>();

            SeletedYear = DateTime.Today.Year;
            YearList = azureDb.ProductSales.Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
            lbYearList.DataSource = YearList;

            UpdateSaleList(azureDb.ProductSales.Include(c => c.Items)
                .Where(c => c.OnDate.Year == SeletedYear).OrderByDescending(c => c.OnDate)
                .ToList());

            dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
        }

        private void LoadFormData()
        {
            try
            {
                cbxInvType.Items.AddRange(Enum.GetNames(typeof(InvoiceType)));

                cbxMmobile.DisplayMember = "MobileNo";
                cbxMmobile.ValueMember = "CustomerName";
                cbxMmobile.DataSource = azureDb.Customers.Select(c => new { c.MobileNo, c.CustomerName }).OrderBy(c => c.MobileNo).ToList();
                SaleItem = new ObservableListSource<SaleItemVM>();
                dgvSaleItems.DataSource = SaleItem;
                PaymentDetails = null;
                lbPd.Text = "";
                cbCashBill.Checked = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManual.Checked)
            {
                if (cbSalesReturn.Checked)
                    InvoiceType = InvoiceType.ManualSaleReturn;
                else
                    InvoiceType = InvoiceType.ManualSale;
            }
        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRegular.Checked)
            {
                if (cbSalesReturn.Checked)
                    InvoiceType = InvoiceType.SalesReturn;
                else
                    InvoiceType = InvoiceType.Sales;
            }
        }

        private void ResetCart()
        {
            this.TotalAmount = this.TotalDiscount = this.TotalTax = TotalQty = TotalFreeQty = 0;
            TotalCount = 0;
            dgvSaleItems.Rows.Clear();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SetGridView()
        {
            dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
        }

        private void SetupForm()
        {
            switch (InvoiceType)
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

        //private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //if (e.KeyCode == Keys.Enter)
        //    //{
        //    //    MessageBox.Show("Enter Is Pressed!");
        //    //    ReturnKey = true;
        //    //}
        //}

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if(e.KeyChar==Keys.Enter)
            // MessageBox.Show(e.KeyChar + "");
        }

        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                MessageBox.Show("Tab is pressed\n" + txtBarcode.Text.Trim());
                ReturnKey = true;
                HandleBarcodeEntry();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter\n" + txtBarcode.Text.Trim());
                ReturnKey = true;
                HandleBarcodeEntry();
            }
        }

        //private void txtBarcode_TextChanged(object sender, EventArgs e)
        //{
        //    //if (ReturnKey)
        //    //{
        //    //    MessageBox.Show(txtBarcode.Text);
        //    //    ReturnKey = false;
        //    //}
        //}

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentForm payForm = new PaymentForm();

            if (payForm.ShowDialog() == DialogResult.OK)
            {
                if (PaymentDetails == null) PaymentDetails = new List<PaymentDetail>();
                PaymentDetails.Add(payForm.Pd);
                lbPd.Text += $"Mode: {payForm.Pd.Mode}\t Rs. {payForm.Pd.Amount}\n";
                // DisplayPayment(); 
            }
            else
            {
                MessageBox.Show("Some error occured while fetching payment details");
            }
        }
        private void DisplayPayment()
        {

            if (PaymentDetails == null) return;
            lbPd.Text = "";
            foreach (var pd in PaymentDetails)
            {
                lbPd.Text += $"Mode: {pd.Mode}\t Rs. {pd.Amount}\n";
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add";
            ResetCart();
            tabControl1.SelectedTab = tpList;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text.Contains('%'))
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()));
                    x -= x * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                    txtValue.Text = x.ToString();
                }
                else
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()))
                        - decimal.Parse(txtDiscount.Text.Trim());
                    txtValue.Text = x.ToString();
                }
            }
            catch
            {
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text.Contains('%'))
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()));
                    x -= x * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
                    txtValue.Text = x.ToString();
                }
                else
                {
                    var x = (decimal.Parse(txtQty.Text.Trim()) * decimal.Parse(txtRate.Text.Trim()))
                        - decimal.Parse(txtDiscount.Text.Trim());
                    txtValue.Text = x.ToString();
                }
            }
            catch
            {
            }
        }

        private void UpdateCart()
        {
        }

        private void UpdateSaleList(List<ProductSale> sales)
        {
            foreach (var item in sales)
            {
                Items.Add(item);
            }
        }

        /// <summary>
        /// make static and common functions
        /// </summary>
        /// <param name="category"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
    }


}