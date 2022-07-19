using AKS.Payroll.Database;

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add") {

                btnAdd.Text = "Save";
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
            }
            else if (btnAdd.Text == "Edit") { }
            else if (btnAdd.Text == "Save") { }
        }

       // Func<int> SetForm =()=> {return 0; }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
        }

        private void cbSalesReturn_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbxMmobile_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            _salesManager.InitManager();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add";
            tabControl1.SelectedTab = tpList;
            _salesManager.ResetCart();
            dgvSaleItems.Rows.Clear();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            txtValue.Text = SalesManager.CalculateRate(txtDiscount.Text, txtQty.Text, txtRate.Text).ToString();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            txtValue.Text = SalesManager.CalculateRate(txtDiscount.Text, txtQty.Text, txtRate.Text).ToString();
        }
    }
}