using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
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


        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

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

        }
       private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }
 
    }
}