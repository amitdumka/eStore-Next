using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Accounts;

namespace AKS.Payroll.Forms
{
    public partial class PettyCashSheetForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private PettyCashSheet pcs;
        private int NoOfRows, NoOfPay, NoOfRec,tPay,tRec;
        private string pNar, rNar;

        public PettyCashSheetForm()
        {
            InitializeComponent();
        }

        private void PettyCashSheetForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();  
            pcs = new PettyCashSheet();
            LoadData();
            tRec=tPay=NoOfPay = NoOfRows=NoOfRec=0;
        }

        

        private void btnReceipt_Click(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            Reset();
            
            cbxStore.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            cbxStore.DisplayMember = "StoreName";
            cbxStore.ValueMember = "StoreId";
            


        }

        

        private void Reset()
        {
            lbPrimaryKey.Text = "";
            lbPayList.Text = "";
            lbPay.Text = "";
            lbRec.Text = "";
            lbRecList.Text = "";
            dtpOnDate.Value = DateTime.Now;
        }
        private void DisplayData()
        {
            
        }
        private void ReadData()
        {
            pcs = new PettyCashSheet() {
            BankDeposit=ReadDec(txtBankDeposit), BankWithdrawal=ReadDec(txtWithdrawal),
            CardSale=ReadDec(txtCardSale), ClosingBalance=ReadDec(txtCashInHand), 
            CustomerDue=ReadDec(txtDues), CustomerRecovery=ReadDec(txtDueRecovered), 
            DailySale=ReadDec(txtSale), Id="", ManualSale=ReadDec(txtManualSale),
            OnDate=dtpOnDate.Value, OpeningBalance=ReadDec(txtOpeningBalance), 
            PaymentTotal=0, PaymentNaration="", ReceiptsNaration="", ReceiptsTotal=0,
            TailoringPayment=ReadDec(txtTailoringPayment), TailoringSale=ReadDec(txtTailoring), 
            
            };
        }
        private decimal ReadDec(TextBox text)
        {
            return decimal.Parse(text.Text.Trim());
        }
        private int ReadInt(TextBox text)
        {
            return Int32.Parse(text.Text.Trim());
        }
    }

    
}
