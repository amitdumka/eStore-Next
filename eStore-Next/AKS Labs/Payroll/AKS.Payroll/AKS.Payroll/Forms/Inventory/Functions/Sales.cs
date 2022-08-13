using AKS.Payroll.Database;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class Sales
    {
        public static decimal TodaySale { get; set; }
        public static decimal TodayCashSale { get; set; }

        public static decimal TodayNonCashSale { get; set; }
        public static decimal MonthlyNonCashSale { get; set; }

        public static decimal MonthlySale { get; set; }
        public static decimal MonthlyCashSale { get; set; }

        public static void FetchSaleDetails(AzurePayrollDbContext db, string sc)
        {
            var today = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Date == DateTime.Today.Date)
                //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
                .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
                .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
                .ToList();

            var Monthly = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
               //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
               .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
               .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
               .ToList();

            TodaySale = today.Sum(c => c.AMT);
            MonthlySale = Monthly.Sum(c => c.AMT);

            TodayCashSale = today.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            TodayCashSale += today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            MonthlyCashSale = Monthly.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            MonthlyCashSale += Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            TodayNonCashSale = today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
            MonthlyNonCashSale = Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
        }
        public static void FetchLocalSaleDetails(LocalPayrollDbContext db, string sc)
        {
            var today = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Date == DateTime.Today.Date)
                //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
                .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
                .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
                .ToList();

            var Monthly = db.DailySales.Where(c => c.StoreId == sc && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
               //.Select(c => new { c.Amount, c.CashAmount, c.PayMode })
               .GroupBy(c => new { c.PayMode, c.Amount, c.CashAmount })
               .Select(c => new { MODE = c.Key.PayMode, AMT = c.Sum(x => x.Amount), CASH = c.Sum(x => x.CashAmount) })
               .ToList();

            TodaySale = today.Sum(c => c.AMT);
            MonthlySale = Monthly.Sum(c => c.AMT);

            TodayCashSale = today.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            TodayCashSale += today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            MonthlyCashSale = Monthly.Where(c => c.MODE == PayMode.Cash).Sum(c => c.AMT);
            MonthlyCashSale += Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);

            TodayNonCashSale = today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - today.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
            MonthlyNonCashSale = Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.AMT) - Monthly.Where(c => c.MODE != PayMode.Cash).Sum(c => c.CASH);
        }
    }
}

//        private bool IsNew;
//
//        private List<PaymentDetail> PaymentDetails;
//        private bool ReturnKey = false;
//        private ProductSale Sale;
//        private ObservableListSource<SaleItemVM> SaleItem;
//        private List<SaleItem> SalesItems;
//

//private void AddToCart()
//{
//    try
//    {
//        var si = new SaleItemVM
//        {
//            Barcode = txtBarcode.Text.Trim(),
//            Rate = decimal.Parse(txtRate.Text.Trim()),
//            ProductItem = txtProductItem.Text.Trim(),
//            Qty = decimal.Parse(txtQty.Text.Trim()),
//            Amount = 0,
//            Tax = 0,
//        };

//        if (txtDiscount.Text.Contains('%'))
//        {
//            si.Discount = si.Qty * si.Rate * decimal.Parse(txtDiscount.Text.Replace('%', ' ').Trim()) / 100;
//        }
//        else
//            si.Discount = decimal.Parse(txtDiscount.Text.Trim());

//        si.Amount = (si.Rate * si.Qty) - si.Discount;

//        SaleItem.Add(si);
//        txtBarcode.Text = "";
//        txtQty.Text = "0";
//        txtRate.Text = "0";
//        txtProductItem.Text = "";
//        txtDiscount.Text = "0";
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}

//private void btnAdd_Click(object sender, EventArgs e)
//{
//    if (btnAdd.Text == "Add")
//    {
//        LoadFormData();
//        btnAdd.Text = "Save";
//        tabControl1.SelectedTab = tpEntry;

//        if (rbManual.Checked)
//        {
//            if (cbSalesReturn.Checked)
//                cbxInvType.SelectedIndex = (int)InvoiceType.ManualSaleReturn;
//            else

//                cbxInvType.SelectedIndex = (int)InvoiceType.ManualSale;
//        }
//        else if (rbRegular.Checked)
//        {
//            if (cbSalesReturn.Checked)

//                cbxInvType.SelectedIndex = (int)InvoiceType.SalesReturn;
//            else cbxInvType.SelectedIndex = (int)InvoiceType.Sales;
//        }
//        ResetCart();
//    }
//    else if (btnAdd.Text == "Edit")
//    {
//        LoadFormData();
//        btnAdd.Text = "Save";
//        tabControl1.SelectedTab = tpEntry;
//    }
//    else if (btnAdd.Text == "Save")
//    {
//        tabControl1.SelectedTab = tpView;
//    }
//}

//private void btnAddCustomer_Click(object sender, EventArgs e)
//{
//    //TODO: SaleManager.AddCustomer(txtCustomerName.Text.Trim(), cbxMobil.Text.Trim());
//    Customer c = new Customer
//    {
//        City = "Dumka",
//        Age = 30,
//        DateOfBirth = DateTime.Today.AddYears(-30).Date,
//        Gender = Gender.Male,
//        MobileNo = cbxMmobile.Text.Trim(),
//        NoOfBills = 0,
//        OnDate = DateTime.Today,
//        TotalAmount = 0
//    };
//    var cname = txtCustomerName.Text.Trim().Split(' ');
//    c.FirstName = cname[0];
//    for (int x = 1; x < cname.Length; x++)
//        c.LastName += cname[x] + " ";
//    c.LastName = c.LastName.Trim();
//    if (azureDb.Customers.Any(C => C.MobileNo == cbxMmobile.Text.Trim()))
//    {
//        return;
//    }
//    else
//    {
//        azureDb.Customers.Add(c);
//        if (azureDb.SaveChanges() > 0) MessageBox.Show("Customer Added");
//    }
//}

//private bool VerifyProductRow()
//{
//    bool flag = true;

//    if (txtBarcode.Text.Trim().Length <= 0) flag = false;
//    if (txtQty.Text.Trim().Length <= 0)// isNumeric
//        flag = false;
//    if (txtDiscount.Text.Trim().Length <= 0) flag = false;
//    if (txtRate.Text.Trim().Length <= 0) flag = false;
//    if (txtValue.Text.Trim().Length <= 0) flag = false;
//    return flag;
//}

//private void btnAddToCart_Click(object sender, EventArgs e)
//{
//    if (VerifyProductRow())
//        AddToCart();
//    else
//        MessageBox.Show("Check Field before adding...");
//}

//private void cbSalesReturn_CheckedChanged(object sender, EventArgs e)
//{
//    if (cbSalesReturn.Checked)
//    {
//        if (rbManual.Checked)
//            InvoiceType = InvoiceType.ManualSaleReturn;
//        else if (rbRegular.Checked)
//            InvoiceType = InvoiceType.SalesReturn;
//    }
//    else
//    {
//        if (rbManual.Checked)
//            InvoiceType = InvoiceType.ManualSale;
//        else if (rbManual.Checked)
//            InvoiceType = InvoiceType.Sales;
//    }
//}

//private void cbxMmobile_SelectedIndexChanged(object sender, EventArgs e)
//{
//    try
//    {
//        txtCustomerName.Text = (string)cbxMmobile.SelectedValue;
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

//private void DisplayStockInfo(StockInfo info)
//{
//    if (info != null)
//    {
//        txtQty.Text = info.Qty.ToString();
//        txtRate.Text = info.Rate.ToString();
//        txtValue.Text = (info.Qty * info.Rate).ToString();
//        txtProductItem.Text = info.ProductItem.ToString();
//        txtDiscount.Text = "0";
//    }
//    else
//    {
//        MessageBox.Show("Stock Not Found!");
//    }
//}

//private void HandleBarcodeEntry()
//{
//    if (ReturnKey)
//        DisplayStockInfo(GetItemDetail(txtBarcode.Text.Trim()));
//}

//private void LoadData()
//{
//    SetupForm();
//    lbYearList.DataSource = YearList;
//    dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
//}

//private void LoadFormData()
//{
//    try
//    {
//        cbxInvType.Items.AddRange(Enum.GetNames(typeof(InvoiceType)));
//        cbxMmobile.DisplayMember = "MobileNo";
//        cbxMmobile.ValueMember = "CustomerName";
//        cbxMmobile.DataSource = azureDb.Customers.Select(c => new { c.MobileNo, c.CustomerName }).OrderBy(c => c.MobileNo).ToList();
//        SaleItem = new ObservableListSource<SaleItemVM>();
//        dgvSaleItems.DataSource = SaleItem;
//        PaymentDetails = null;
//        lbPd.Text = "";
//        cbCashBill.Checked = false;
//    }
//    catch (Exception e)
//    {
//        MessageBox.Show(e.Message);
//    }
//}

//private void rbManual_CheckedChanged(object sender, EventArgs e)
//{
//    if (rbManual.Checked)
//    {
//        if (cbSalesReturn.Checked)
//            InvoiceType = InvoiceType.ManualSaleReturn;
//        else
//            InvoiceType = InvoiceType.ManualSale;
//    }
//}

//private void rbRegular_CheckedChanged(object sender, EventArgs e)
//{
//    if (rbRegular.Checked)
//    {
//        if (cbSalesReturn.Checked)
//            InvoiceType = InvoiceType.SalesReturn;
//        else
//            InvoiceType = InvoiceType.Sales;
//    }
//}

//private void SalesForm_Load(object sender, EventArgs e)
//{
//    LoadData();
//}

//private void SetGridView()
//{
//    dataGridView1.DataSource = Items.Where(c => c.InvoiceType == InvoiceType).ToList();
//}

//private void SetupForm()
//{
//    switch (InvoiceType)
//    {
//        case InvoiceType.Sales:
//            rbRegular.Checked = true;
//            this.Text = "Regular Invoice";
//            break;

//        case InvoiceType.SalesReturn:
//            rbRegular.Checked = true;
//            cbSalesReturn.Checked = true;
//            this.Text = "Regular Sale's Invoice";
//            break;

//        case InvoiceType.ManualSale:
//            rbManual.Checked = true;
//            this.Text = "Manual Invoice";
//            break;

//        case InvoiceType.ManualSaleReturn:
//            rbManual.Checked = true;
//            cbSalesReturn.Checked = true;
//            this.Text = "Manual Sale's Return Invoice";
//            break;

//        default:
//            rbManual.Checked = true;
//            this.Text = "Manual Invoice";
//            break;
//    }
//}

//private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
//{
//    if (e.KeyCode == Keys.Tab)
//    {
//        ReturnKey = true;
//        HandleBarcodeEntry();
//    }
//    else if (e.KeyCode == Keys.Enter)
//    {
//        ReturnKey = true;
//        HandleBarcodeEntry();
//    }
//}

//private void btnPayment_Click(object sender, EventArgs e)
//{
//    PaymentForm payForm = new PaymentForm();

//    if (payForm.ShowDialog() == DialogResult.OK)
//    {
//        if (PaymentDetails == null) PaymentDetails = new List<PaymentDetail>();
//        PaymentDetails.Add(payForm.Pd);
//        lbPd.Text += $"Mode: {payForm.Pd.Mode}\t Rs. {payForm.Pd.Amount}\n";
//        // DisplayPayment();
//    }
//    else
//    {
//        MessageBox.Show("Some error occured while fetching payment details");
//    }
//}

//private void DisplayPayment()
//{
//    if (PaymentDetails == null) return;
//    lbPd.Text = "";
//    foreach (var pd in PaymentDetails)
//    {
//        lbPd.Text += $"Mode: {pd.Mode}\t Rs. {pd.Amount}\n";
//    }
//}