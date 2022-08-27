using AKS.Payroll.Database;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class PettyCashSheetForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private CashDetail cashDetail;
        private bool EnableCashAdd = false;
        private bool isNew = false;
        private ObservableListSource<PettyCashSheet> ItemList;
        private LocalPayrollDbContext localDb;
        private PettyCashSheet pcs;
        private string pNar, rNar, dNar, rcNar;
        private int TotalCurreny = 0, TotalCurrenyAmount = 0;
        private decimal tPay, tRec, tDue, tdRec;
        private List<int> YearList;
        private List<int> DataList;
        public PettyCashSheetForm()
        {
            InitializeComponent();
        }
        public bool SaveData()
        {
            if (isNew)
                azureDb.PettyCashSheets.Add(pcs);
            else azureDb.PettyCashSheets.Update(pcs);
            return azureDb.SaveChanges() > 0;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                this.tabControl1.SelectedIndex = 1;
                Reset();
                isNew = true;
                tPay = tRec = tDue = tdRec = 0;
            }
            else if (btnAdd.Text == "Add Cash")
            {
                if (SaveCashDetails(ReadCashDetails()))
                {
                    btnAdd.Text = "Add";
                    this.tabControl1.SelectedIndex = 3;
                    Reset();
                    MessageBox.Show("Cash Details is saved!!");
                    dgvCashDetails.Rows.Add(cashDetail);
                    ViewPdf();
                }
                else
                {
                    MessageBox.Show("An Error occured while saving cash details, kindly check and try again!!");
                }
            }
            else if (btnAdd.Text == "Save")
            {
                try
                {
                    ReadData();
                    if (SaveData())
                    {
                        if (!isNew)
                        {
                            ItemList.Remove(ItemList.Where(c => c.Id == pcs.Id).FirstOrDefault());
                        }

                        ItemList.Add(pcs);
                        btnAdd.Text = "Add Cash";
                        MessageBox.Show("Petty Cash Sheet Add! Kindly now add Cash Sheet");

                        dgvPettyCashSheet.Refresh();
                        if (isNew)
                        {
                            EnableCashAdd = true;
                            tabControl1.SelectedIndex = 2;
                        }
                        //ViewPdf();
                    }
                    else
                    {
                        azureDb.PettyCashSheets.Remove(pcs);
                        MessageBox.Show("Some error occured while saveing, kindly check data and try again!");
                    }
                }
                catch (Exception ex)
                {
                    azureDb.PettyCashSheets.Remove(pcs);
                    MessageBox.Show("Some error occured while saveing, kindly check data and try again!\n" + ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add";
            Reset();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPettyCashSheet.CurrentCell.Selected)
            {
                var row = (PettyCashSheet)dgvPettyCashSheet.CurrentRow.DataBoundItem;
                if (row != null)
                {
                    azureDb.PettyCashSheets.Remove(row);
                    if (azureDb.SaveChanges() > 0)
                    {
                        MessageBox.Show("Deleted");

                        ItemList.Remove(row);
                    }
                }
            }
        }
        private void btnDue_Click(object sender, EventArgs e)
        {//TODO: prend
            tDue += UIManager.ReadDec(txtDueAmount);
            dNar += $"#{txtDueNaration.Text} : {txtDueAmount.Text}";

            lbPay.Text = lbPay.Text + "\n" + txtDueNaration.Text;
            lbPayList.Text = lbPayList.Text + "\n" + txtDueAmount.Text;
            txtDueAmount.Text = "0";
            txtDueNaration.Text = "";
        }
        private void btnMissingReport_Click(object sender, EventArgs e)
        {
            string filename = new PettyCashSheetManager(azureDb, localDb).GenReport();

            if (!string.IsNullOrEmpty(filename))
            {
                pdfView.Load(filename);
                btnPrint.Enabled = true;
                this.tabControl1.SelectedIndex = 3;
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }
        private void btnPayment_Click(object sender, EventArgs e)
        {
            tPay += UIManager.ReadDec(txtAmount);
            pNar += $"#{txtNaration.Text} : {txtAmount.Text}";
            lbPay.Text = lbPay.Text + "\n" + txtNaration.Text;
            lbPayList.Text = lbPayList.Text + "\n" + txtAmount.Text;
            txtAmount.Text = "0";
            txtNaration.Text = "";
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (pcs != null && cashDetail != null)
            {
                ViewPdf();
            }
            else
            {
                pcs = azureDb.PettyCashSheets.Where(c => c.OnDate.Date == DateTime.Today.Date).FirstOrDefault();
                cashDetail = azureDb.CashDetails.Where(c => c.OnDate.Date == DateTime.Today.Date).FirstOrDefault();

                if (pcs != null)
                    ViewPdf();
                else
                {
                    pcs = azureDb.PettyCashSheets.Where(c => c.OnDate.Date == DateTime.Today.AddDays(-1).Date).FirstOrDefault();
                    cashDetail = azureDb.CashDetails.Where(c => c.OnDate.Date == DateTime.Today.AddDays(-1).Date).FirstOrDefault();
                    if (pcs != null) ViewPdf();
                    else
                        MessageBox.Show("No Record Found");
                }
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            var printDialog1 = new PrintDialog();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.AllowPrintToFile = true;

                pdfView.Print(printDialog1.PrinterSettings.PrinterName);
            }
        }
        private void btnReceipt_Click(object sender, EventArgs e)
        {
            tRec += UIManager.ReadDec(txtAmount);
            rNar += $"#{txtNaration.Text} : {txtAmount.Text}";
            lbRec.Text = lbRec.Text + "\n" + txtNaration.Text;
            lbRecList.Text = lbRecList.Text + "\n" + txtAmount.Text;
            txtAmount.Text = "0";
            txtNaration.Text = "";
        }
        private void btnRecovery_Click(object sender, EventArgs e)
        {
            tdRec += UIManager.ReadDec(txtDueAmount);
            rcNar += $"#{txtDueNaration.Text} : {txtDueAmount.Text}";
            lbRec.Text = lbRec.Text + "\n" + txtDueNaration.Text;
            lbRecList.Text = lbRecList.Text + "\n" + txtDueAmount.Text;
            txtDueAmount.Text = "0";
            txtDueNaration.Text = "";
        }
        private void CalculateTotalCount()
        {
            //foreach (var item in collection)
            //{
            //}
        }
        private void dgvPettyCashSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPettyCashSheet.CurrentCell.Selected)
            {
                var row = (PettyCashSheet)dgvPettyCashSheet.CurrentRow.DataBoundItem;
                if (row != null)
                {
                    pcs = row;
                    cashDetail = azureDb.CashDetails.Where(c => c.OnDate.Date == pcs.OnDate.Date).FirstOrDefault();
                    ViewPdf();
                }
            }
        }
        private void DisplayData()
        {
        }
        
        private void lbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData((int) lbYearList.SelectedValue);
        }
        private void FilterData(int year)
        {
            if (DataList.Contains(year)==false)           
            {
                UpdateItemList(azureDb.PettyCashSheets.Where(c => c.OnDate.Year == year).ToList());
                DataList.Add(year);
            }
            dgvPettyCashSheet.DataSource = (ItemList.Where(c => c.OnDate.Year == year).ToList());
            dgvCashDetails.DataSource = azureDb.CashDetails.Where(c => c.OnDate.Year == year).ToList();
        }
        private void LoadData()
        {
            Reset();
            DataList = new List<int>();
            cbxStore.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            cbxStore.DisplayMember = "StoreName";
            cbxStore.ValueMember = "StoreId";
            DataList.Add(DateTime.Today.Year);
            UpdateItemList(azureDb.PettyCashSheets.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList());
            dgvPettyCashSheet.DataSource = (ItemList.Where(c => c.OnDate.Month == DateTime.Today.Month).ToList());
            lbYearList.DataSource = YearList;
            dgvCashDetails.DataSource = azureDb.CashDetails.Where(c => c.OnDate.Month == DateTime.Today.Month).ToList();
            YearList = azureDb.PettyCashSheets.Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();

        }
        private void nud2000_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown field = (NumericUpDown)sender;
            TotalCurreny += (int)field.Value;
            switch (field.Name)
            {
                case "nud2000":
                    UpdateLabel(lb2000, 2000 * nud2000.Value);

                    break;

                case "nud1000":
                    UpdateLabel(lb1000, 1000 * nud1000.Value);
                    break;

                case "nud200":
                    UpdateLabel(lb200, 200 * nud200.Value);
                    break;

                case "nud100":
                    UpdateLabel(lb100, 100 * nud100.Value);

                    break;

                case "nud500":

                    UpdateLabel(lb500, 500 * nud500.Value);
                    break;

                case "nud50":

                    UpdateLabel(lb50, 50 * nud50.Value);

                    break;

                case "nud20":

                    UpdateLabel(lb20, 20 * nud20.Value);
                    break;

                case "nud10":
                    UpdateLabel(lb10, 10 * nud10.Value);

                    break;

                case "nudCoin10":
                    UpdateLabel(lbCoin10, 10 * nudCoin10.Value);

                    break;

                case "nudCoin5":
                    UpdateLabel(lbCoin5, 5 * nudCoin5.Value);
                    break;

                case "nudCoin2":
                    UpdateLabel(lbCoin2, 2 * nudCoin2.Value);
                    break;

                case "nudCoin1":
                    UpdateLabel(lbCoin1, 1 * nudCoin1.Value);
                    break;

                default:

                    break;
            }
            lbTotalAmount.Text = TotalCurrenyAmount.ToString();
            lbCount.Text = TotalCurreny.ToString();
        }
        // private List<PettyCashSheet> ItemList;
        private void PettyCashSheetForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            pcs = new PettyCashSheet();
            ItemList = new ObservableListSource<PettyCashSheet>();
            LoadData();
            tRec = tPay = (decimal)0.0;
            YearList = new List<int>();
        }
        private void rbCMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
        private void rbLMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
        private void rbYearly_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
        private CashDetail ReadCashDetails()
        {
            CashDetail cd = new CashDetail
            {
                C1 = UIManager.GetIntField(nudCoin1),
                C10 = UIManager.GetIntField(nudCoin10),
                C2 = UIManager.GetIntField(nudCoin2),
                C5 = UIManager.GetIntField(nudCoin5),
                CashDetailId = $"ARD/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = false,
                MarkedDeleted = false,
                N10 = UIManager.GetIntField(nud10),
                N100 = UIManager.GetIntField(nud100),
                N1000 = UIManager.GetIntField(nud100),
                N50 = UIManager.GetIntField(nud50),
                N20 = UIManager.GetIntField(nud20),
                N200 = UIManager.GetIntField(nud200),
                N2000 = UIManager.GetIntField(nud2000),
                N500 = UIManager.GetIntField(nud500),
                OnDate = DateTime.Now,
                StoreId = "ARD",
                UserId = "WinUI",
                Count = UIManager.GetIntLable(lbCount),
                TotalAmount = UIManager.GetIntLable(lbTotalAmount),
            };
            return cd;
        }
        private void ReadData()
        {
            if (isNew)
            {
                if (dNar == null)
                    dNar = "#";

                if (pNar == null)
                    pNar = "#";

                if (rcNar == null)
                    rcNar = "#";

                if (rNar == null)
                    rNar = "#";

                pcs = new PettyCashSheet()
                {
                    BankDeposit = UIManager.ReadDec(txtBankDeposit),
                    BankWithdrawal = UIManager.ReadDec(txtWithdrawal),
                    CardSale = UIManager.ReadDec(txtCardSale),
                    ClosingBalance = UIManager.ReadDec(txtCashInHand),

                    DailySale = UIManager.ReadDec(txtSale),
                    ManualSale = UIManager.ReadDec(txtManualSale),
                    OnDate = dtpOnDate.Value,
                    OpeningBalance = UIManager.ReadDec(txtOpeningBalance),

                    PaymentTotal = tPay,
                    PaymentNaration = pNar,
                    ReceiptsNaration = rNar,
                    ReceiptsTotal = tRec,

                    DueList = dNar,
                    RecoveryList = rcNar,
                    CustomerDue = tDue,
                    CustomerRecovery = tdRec,

                    NonCashSale = UIManager.ReadDec(txtNonCashSale),
                    TailoringPayment = UIManager.ReadDec(txtTailoringPayment),
                    TailoringSale = UIManager.ReadDec(txtTailoring),
                    Id = "",
                };
                // pcs.Id = $"{StoreId}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                pcs.Id = $"ARD/{pcs.OnDate.Year}/{pcs.OnDate.Month}/{pcs.OnDate.Day}";
            }
            else
            {
                pcs.BankDeposit = UIManager.ReadDec(txtBankDeposit);
                pcs.BankWithdrawal = UIManager.ReadDec(txtWithdrawal);
                pcs.CardSale = UIManager.ReadDec(txtCardSale);
                pcs.ClosingBalance = UIManager.ReadDec(txtCashInHand);

                pcs.CustomerDue = tDue;
                pcs.CustomerRecovery = tdRec;

                pcs.DailySale = UIManager.ReadDec(txtSale);
                pcs.Id = lbPrimaryKey.Text;
                pcs.ManualSale = UIManager.ReadDec(txtManualSale);
                pcs.OnDate = dtpOnDate.Value;
                pcs.OpeningBalance = UIManager.ReadDec(txtOpeningBalance);
                pcs.NonCashSale = UIManager.ReadDec(txtNonCashSale);

                pcs.PaymentTotal = tPay;
                pcs.PaymentNaration = pNar;

                pcs.ReceiptsNaration = rNar;

                pcs.DueList = dNar;

                pcs.RecoveryList = rcNar;
                pcs.ReceiptsTotal = tdRec;

                pcs.TailoringPayment = UIManager.ReadDec(txtTailoringPayment);
                pcs.TailoringSale = UIManager.ReadDec(txtTailoring);
            }
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
        private bool SaveCashDetails(CashDetail cd)
        {
            if (cd != null)
            {
                cashDetail = cd;
                if (isNew) azureDb.CashDetails.Add(cashDetail);
                else azureDb.CashDetails.Update(cashDetail);
                return azureDb.SaveChanges() > 0;
            }
            else
            {
                MessageBox.Show("Error occured while reading Cash Details!!");
                return false;
            }
        }
        private ObservableListSource<PettyCashSheet> UpdateItemList(List<PettyCashSheet> items)
        {
            foreach (var item in items)
            {
                ItemList.Add(item);
            }
            return ItemList;
        }
        private void UpdateLabel(Label lb, decimal value)
        {
            var oldVal = UIManager.GetIntLable(lb);
            lb.Text = value.ToString();
            TotalCurrenyAmount += (int)(value - oldVal);
        }
        private void UpdateView()
        {
            if (rbCMonth.Checked)
                dgvPettyCashSheet.DataSource = (ItemList.Where(c =>c.OnDate.Year==DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).ToList());
            else if (rbYearly.Checked)
                dgvPettyCashSheet.DataSource = ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year).ToList();
            else if (rbLMonth.Checked)
                dgvPettyCashSheet.DataSource = (ItemList.Where(c => c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.AddMonths(-1).Month).ToList());
        }
        private async void ViewPdf()
        {
            string fileName = new PettyCashSheetManager(azureDb, localDb).GeneratePdf(pcs, cashDetail);

            if (string.IsNullOrEmpty(fileName) == false)
            {
                pdfView.Load(fileName);
                btnPrint.Enabled = true;
                this.tabControl1.SelectedIndex = 3;
            }
        }
    }//end of class

    public class PettyCashSheetManager
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;

        private CashDetail cashDetail;
        private PettyCashSheet pcs;

        public PettyCashSheetManager(AzurePayrollDbContext db1, LocalPayrollDbContext db2)
        {
            azureDb = db1;
            localDb = db2;
        }

        public string GeneratePdf(PettyCashSheet p, CashDetail cd)
        {
            pcs = p; cashDetail = cd;
            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();

                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.All = 50;

                PdfPage pdfPage = document.Pages.Add();
                // GenerateFirstPage(pdfPage);
                PdfPage pdfPage2 = document.Pages.Add();
                GenerateCarbonPage(pdfPage, pdfPage2);

                //Save the document.
                document.Save("Output.pdf");

                //Close the document.
                document.Close(true);
                return "Output.pdf";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public string GenReport()
        {
            return GeneratePdfWithMultiple(GenDBSheet());
        }

        private List<PettyCashSheet> GenDBSheet()
        {
            DateTime startDate = new DateTime(2021, 04, 13);
            DateTime endDate = new DateTime(2021, 06, 01);

            var dailySale = azureDb.DailySales.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            var expen = azureDb.Vouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            var cash = azureDb.CashVouchers.Where(c => c.OnDate.Date >= startDate && c.OnDate.Date <= endDate).ToList();
            decimal openbal = 3109;
            List<PettyCashSheet> cashSheet = new List<PettyCashSheet>();

            DateTime onDate = startDate;
            do
            {
                PettyCashSheet pcs = new PettyCashSheet
                {
                    OnDate = onDate,
                    CardSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.PayMode == PayMode.Card).Select(c => c.Amount).Sum(),
                    ManualSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.ManualBill).Select(c => c.Amount).Sum(),
                    DailySale = dailySale.Where(c => c.OnDate.Date == onDate.Date && !c.ManualBill && !c.TailoringBill && !c.SalesReturn).Select(c => c.Amount).Sum(),
                    NonCashSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.PayMode != PayMode.Cash && c.PayMode != PayMode.Card).Select(c => c.Amount).Sum(),

                    BankDeposit = 0,
                    BankWithdrawal = 0,
                    ClosingBalance = 0,
                    CustomerDue = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.IsDue).Select(c => c.Amount).Sum(),
                    CustomerRecovery = 0,
                    TailoringSale = dailySale.Where(c => c.OnDate.Date == onDate.Date && c.TailoringBill).Select(c => c.Amount).Sum(),
                    OpeningBalance = openbal,
                    PaymentNaration = "",
                    ReceiptsNaration = "",
                    DueList = "#",
                    RecoveryList = "#",
                    TailoringPayment = 0,
                    Id = $"ARD/{onDate.Year}/{onDate.Month}/{onDate.Day}",

                    ReceiptsTotal = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt).Select(c => c.Amount).Sum(),
                    PaymentTotal = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashPayment).Select(c => c.Amount).Sum() +
                              expen.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.Expense && c.PaymentMode == PaymentMode.Cash).Select(c => c.Amount).Sum(),
                };

                var recs = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashReceipt)
                    .Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();
                var pay = cash.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.CashPayment).Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();
                var exps = expen.Where(c => c.OnDate.Date == onDate.Date && c.VoucherType == VoucherType.Expense && c.PaymentMode == PaymentMode.Cash).Select(c => new { c.Amount, c.Particulars, c.SlipNumber, c.Remarks }).ToList();

                foreach (var item in recs)
                {
                    pcs.ReceiptsNaration += $"#{item.SlipNumber} / {item.Particulars}/{item.Remarks} :{item.Amount} ";
                }
                var ds = dailySale.Where(c => c.OnDate.Date == onDate.Date).ToList();
                foreach (var item in ds)
                {
                    pcs.ReceiptsNaration += $"#{item.InvoiceNumber} / {item.Remarks}/{item.PayMode} :{item.Amount} ";
                }
                // pcs.PaymentNaration = "$PAYMENT=>";
                foreach (var item in pay)
                {
                    pcs.PaymentNaration += $"#{item.SlipNumber} = {item.Particulars}={item.Remarks} :{item.Amount} ";
                }
                // pcs.PaymentNaration+= "$Expenses=>";
                foreach (var item in exps)
                {
                    pcs.PaymentNaration += $"#{item.SlipNumber} / {item.Particulars}/{item.Remarks} :{item.Amount} ";
                }

                var colbal = pcs.OpeningBalance + pcs.DailySale + pcs.ManualSale + pcs.TailoringSale + pcs.CustomerRecovery + pcs.ReceiptsTotal + pcs.BankWithdrawal;
                colbal = colbal - (pcs.PaymentTotal + pcs.CardSale + pcs.TailoringPayment + pcs.CustomerDue + pcs.BankDeposit);
                pcs.ClosingBalance = colbal;
                cashSheet.Add(pcs);
                onDate = onDate.AddDays(1);
                openbal = colbal;
            }
            while (onDate < endDate);

            return cashSheet;
        }

        private PdfPage GenerateCarbonPage(PdfPage page, PdfPage cPage)
        {
            //Add a page to the document.
            //PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Carbon Copy
            PdfGraphics cGraphics = cPage.Graphics;

            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

            //Draw the text.
            graphics.DrawString("Petty Cash Sheet", font, PdfBrushes.Red, new PointF(page.Graphics.ClientSize.Width / 3, 0));
            cGraphics.DrawString("Petty Cash Sheet", font, PdfBrushes.Red, new PointF(cPage.Graphics.ClientSize.Width / 3, 0));
            cGraphics.DrawString("( Duplicate )", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Brown,
                new PointF(cPage.Graphics.ClientSize.Width - 70, 0));

            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 0));
            PdfLayoutResult resultCarbon = new PdfLayoutResult(cPage, new RectangleF(0, 0, cPage.Graphics.ClientSize.Width / 2, 0));

            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Draw Rectangle place on location
            graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 20, graphics.ClientSize.Width, 30));
            cGraphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, resultCarbon.Bounds.Bottom + 20, cGraphics.ClientSize.Width, 30));

            var element = new PdfTextElement("Aprajita Retails \t" + pcs.Id, subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 28));
            resultCarbon = element.Draw(cPage, new PointF(10, resultCarbon.Bounds.Bottom + 28));

            string currentDate = "On: " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);

            graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));
            cGraphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(cGraphics.ClientSize.Width - textSize.Width - 10, resultCarbon.Bounds.Y));

            //Draw Bill header
            element = new PdfTextElement("Petty Cash Sheet ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 10));
            resultCarbon = element.Draw(cPage, new PointF(10, resultCarbon.Bounds.Bottom + 10));

            //Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {pcs.OnDate.ToString("dd/MM/yyyy")} ",
                $"\t\tSN: {pcs.Id} ", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));
            resultCarbon = element.Draw(cPage, new RectangleF(10, resultCarbon.Bounds.Bottom + 3, cGraphics.ClientSize.Width / 2, 100));

            //Draw Bill line
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));
            cGraphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, resultCarbon.Bounds.Bottom + 3), new PointF(cGraphics.ClientSize.Width, result.Bounds.Bottom + 3));

            // Adding Table part

            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

            int rI = 0, dI = 0;

            //Assign data source
            pdfGrid.DataSource = ToDataTable(out rI, out dI);

            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = pdfGrid.Headers[0];

            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)); ;
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Bold);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 2)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);

            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridRow lastRow = pdfGrid.Rows[pdfGrid.Rows.Count - 1];

            PdfGridCellStyle firstRowStyle = new PdfGridCellStyle();
            firstRowStyle.TextBrush = PdfBrushes.OrangeRed;
            firstRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
            pdfGrid.Rows[0].ApplyStyle(firstRowStyle);

            PdfGridCellStyle totalRowStyle = new PdfGridCellStyle();
            totalRowStyle.TextBrush = PdfBrushes.DarkBlue;
            totalRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);

            if (dI > 0) pdfGrid.Rows[dI].ApplyStyle(totalRowStyle);
            if (rI > 0) pdfGrid.Rows[rI].ApplyStyle(totalRowStyle);
            //pdfGrid.Rows[5].ApplyStyle(firstRowStyle);

            PdfGridCellStyle footerStyle = new PdfGridCellStyle();
            footerStyle.Borders.All = new PdfPen(new PdfColor(Color.RebeccaPurple));
            footerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray)); ;
            footerStyle.TextBrush = PdfBrushes.Red;
            footerStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 13f, PdfFontStyle.Italic);
            lastRow.ApplyStyle(footerStyle);

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = pdfGrid.Draw(page,
                new RectangleF(
                    new PointF(0, result.Bounds.Bottom + 10),
                    new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResultCarbon = pdfGrid.Draw(cPage,
                new RectangleF(
                    new PointF(0, resultCarbon.Bounds.Bottom + 10),
                    new SizeF(cGraphics.ClientSize.Width, cGraphics.ClientSize.Height - 100)), layoutFormat);

            //Draw Bill line Page Break Line
            graphics.DrawLine(new PdfPen(new PdfColor(Color.DarkBlue)), new PointF(0, gridResult.Bounds.Bottom + 20), new PointF(graphics.ClientSize.Width, gridResult.Bounds.Bottom + 20));
            cGraphics.DrawLine(new PdfPen(new PdfColor(Color.DarkBlue)), new PointF(0, gridResultCarbon.Bounds.Bottom + 20), new PointF(cGraphics.ClientSize.Width, gridResultCarbon.Bounds.Bottom + 20));

            //Adding  Cash Details
            PdfGrid pdfCashGrid = new PdfGrid();
            pdfCashGrid.DataSource = ToCashTable();
            if (pdfCashGrid.DataSource != null)
            {
                //Applies the header style
                pdfCashGrid.Headers[0].ApplyStyle(headerStyle);
                pdfCashGrid.Rows[pdfCashGrid.Rows.Count - 1].ApplyStyle(footerStyle);

                //Draw the text.
                graphics.DrawString("Cash Details", font, PdfBrushes.DarkMagenta, new PointF(page.Graphics.ClientSize.Width / 3, gridResult.Bounds.Bottom + 30));
                cGraphics.DrawString("Cash Details", font, PdfBrushes.DarkMagenta, new PointF(cPage.Graphics.ClientSize.Width / 3, gridResultCarbon.Bounds.Bottom + 30));

                //Draws the grid to the PDF page.
                PdfGridLayoutResult gridCashResult = pdfCashGrid.Draw(page,
                    new RectangleF(
                        new PointF(0, gridResult.Bounds.Bottom + 55),
                        new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

                //Draws the grid to the PDF page.
                PdfGridLayoutResult gridCashResultCarbon = pdfCashGrid.Draw(cPage,
                    new RectangleF(
                        new PointF(0, gridResultCarbon.Bounds.Bottom + 55),
                        new SizeF(cGraphics.ClientSize.Width, cGraphics.ClientSize.Height - 100)), layoutFormat);

                //Draw the text. //TODO: in case of flowing to next page use in section
                graphics.DrawString("   Sign StoreManager                                                                     Sign Accountant           ",
                    new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Blue, new PointF(page.Graphics.ClientSize.Width / 8, gridCashResult.Bounds.Bottom + 130));
                //Draw the text. //TODO: in case of flowing to next page use in section
                cGraphics.DrawString("   Sign StoreManager                                                                     Sign Accountant           ",
                    new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Blue, new PointF(cPage.Graphics.ClientSize.Width / 8, gridCashResultCarbon.Bounds.Bottom + 130));
            }

            return page;
        }

        private PdfPage GenerateFirstPage(PdfPage page)
        {
            //Add a page to the document.
            //PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
            //Draw the text.
            graphics.DrawString("Petty Cash Sheet", font, PdfBrushes.Red, new PointF(page.Graphics.ClientSize.Width / 2, 0));

            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 0));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Draw Rectangle place on location
            graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 20, graphics.ClientSize.Width, 30));
            var element = new PdfTextElement("Aprajita Retails \t" + pcs.Id, subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 28));

            string currentDate = "On: " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            //Draw Bill header
            element = new PdfTextElement("Petty Cash Sheet ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 10));

            //Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {pcs.OnDate.ToString("dd/MM/yyyy")} ",
                $"\t\tSN: {pcs.Id} ", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

            //Draw Bill line
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));

            // Adding Table part

            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

            int rI = 0, dI = 0;

            //Assign data source
            pdfGrid.DataSource = ToDataTable(out rI, out dI);
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = pdfGrid.Headers[0];

            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)); ;
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Bold);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 2)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);

            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridRow lastRow = pdfGrid.Rows[pdfGrid.Rows.Count - 1];

            PdfGridCellStyle firstRowStyle = new PdfGridCellStyle();
            firstRowStyle.TextBrush = PdfBrushes.OrangeRed;
            firstRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
            pdfGrid.Rows[0].ApplyStyle(firstRowStyle);

            PdfGridCellStyle totalRowStyle = new PdfGridCellStyle();
            totalRowStyle.TextBrush = PdfBrushes.DarkBlue;
            totalRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);

            if (dI > 0) pdfGrid.Rows[dI].ApplyStyle(totalRowStyle);
            if (rI > 0) pdfGrid.Rows[rI].ApplyStyle(totalRowStyle);
            //pdfGrid.Rows[5].ApplyStyle(firstRowStyle);

            PdfGridCellStyle footerStyle = new PdfGridCellStyle();
            footerStyle.Borders.All = new PdfPen(new PdfColor(Color.RebeccaPurple));
            footerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray)); ;
            footerStyle.TextBrush = PdfBrushes.Red;
            footerStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 13f, PdfFontStyle.Italic);
            lastRow.ApplyStyle(footerStyle);

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = pdfGrid.Draw(page,
                new RectangleF(
                    new PointF(0, result.Bounds.Bottom + 10),
                    new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            //Draw Bill line Page Break Line
            graphics.DrawLine(new PdfPen(new PdfColor(Color.DarkBlue)), new PointF(0, gridResult.Bounds.Bottom + 20), new PointF(graphics.ClientSize.Width, gridResult.Bounds.Bottom + 20));

            //Adding  Cash Details
            PdfGrid pdfCashGrid = new PdfGrid();
            pdfCashGrid.DataSource = ToCashTable();
            //Applies the header style
            pdfCashGrid.Headers[0].ApplyStyle(headerStyle);
            pdfCashGrid.Rows[pdfCashGrid.Rows.Count - 1].ApplyStyle(footerStyle);

            //Draw the text.
            graphics.DrawString("Cash Details", font, PdfBrushes.DarkMagenta, new PointF(page.Graphics.ClientSize.Width / 3, gridResult.Bounds.Bottom + 30));

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridCashResult = pdfCashGrid.Draw(page,
                new RectangleF(
                    new PointF(0, gridResult.Bounds.Bottom + 55),
                    new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            //Draw the text. //TODO: in case of flowing to next page use in section
            graphics.DrawString("   Sign StoreManager                                                                     Sign Accountant           ",
                new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Blue, new PointF(page.Graphics.ClientSize.Width / 8, gridCashResult.Bounds.Bottom + 130));

            return page;
        }

        private PdfGridLayoutResult GenerateFirstPettyCashSheet(PettyCashSheet pcs1, PdfPage page)
        {
            this.pcs = pcs1;

            PdfGraphics graphics = page.Graphics;
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

            //Draw the text.
            graphics.DrawString("Petty Cash Sheet", font, PdfBrushes.Red, new PointF(page.Graphics.ClientSize.Width / 2, 0));

            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 0));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Draw Rectangle place on location
            graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 20, graphics.ClientSize.Width, 30));
            var element = new PdfTextElement("Aprajita Retails \t" + pcs.Id, subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 28));

            string currentDate = "On: " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            //Draw Bill header
            element = new PdfTextElement("Petty Cash Sheet ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 10));

            //Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {pcs.OnDate.ToString("dd/MM/yyyy")} ",
                $"\t\tSN: {pcs.Id} ", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

            //Draw Bill line
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));

            // Adding Table part

            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

            int rI = 0, dI = 0;

            //Assign data source
            pdfGrid.DataSource = ToDataTable(out rI, out dI);
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            cellStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            PdfGridRow header = pdfGrid.Headers[0];

            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)); ;
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Bold);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 2)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);

            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridRow lastRow = pdfGrid.Rows[pdfGrid.Rows.Count - 1];

            PdfGridCellStyle firstRowStyle = new PdfGridCellStyle();
            firstRowStyle.TextBrush = PdfBrushes.OrangeRed;
            firstRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
            pdfGrid.Rows[0].ApplyStyle(firstRowStyle);

            PdfGridCellStyle totalRowStyle = new PdfGridCellStyle();
            totalRowStyle.TextBrush = PdfBrushes.DarkBlue;
            totalRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);

            if (dI > 0) pdfGrid.Rows[dI].ApplyStyle(totalRowStyle);
            if (rI > 0) pdfGrid.Rows[rI].ApplyStyle(totalRowStyle);
            //pdfGrid.Rows[5].ApplyStyle(firstRowStyle);

            PdfGridCellStyle footerStyle = new PdfGridCellStyle();
            footerStyle.Borders.All = new PdfPen(new PdfColor(Color.RebeccaPurple));
            footerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray)); ;
            footerStyle.TextBrush = PdfBrushes.Red;
            footerStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 13f, PdfFontStyle.Italic);
            lastRow.ApplyStyle(footerStyle);

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = pdfGrid.Draw(page,
                new RectangleF(
                    new PointF(0, result.Bounds.Bottom + 10),
                    new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);
            //Draw Bill line Page Break Line
            graphics.DrawLine(new PdfPen(new PdfColor(Color.DarkBlue)), new PointF(0, gridResult.Bounds.Bottom + 20), new PointF(graphics.ClientSize.Width, gridResult.Bounds.Bottom + 20));
            return gridResult;
        }

        private PdfPage GenerateLastPettyCashSheet(PettyCashSheet pcs2, PdfPage page, PdfGridLayoutResult oldGridResult)
        {
            this.pcs = pcs2;
            PdfGraphics graphics = page.Graphics;
            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, oldGridResult.Bounds.Bottom + 30, page.Graphics.ClientSize.Width / 2, 0));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Draw Rectangle place on location
            graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 20, graphics.ClientSize.Width, 30));
            var element = new PdfTextElement("Aprajita Retails \t" + pcs.Id, subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 28));

            string currentDate = "On: " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            //Draw Bill header
            element = new PdfTextElement("Petty Cash Sheet ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 10));

            //Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {pcs.OnDate.ToString("dd/MM/yyyy")} ",
                $"\t\tSN: {pcs.Id} ", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

            //Draw Bill line
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));

            // Adding Table part

            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

            int rI = 0, dI = 0;

            //Assign data source
            pdfGrid.DataSource = ToDataTable(out rI, out dI);
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = pdfGrid.Headers[0];

            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)); ;
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Bold);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 2)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);

            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridRow lastRow = pdfGrid.Rows[pdfGrid.Rows.Count - 1];

            PdfGridCellStyle firstRowStyle = new PdfGridCellStyle();
            firstRowStyle.TextBrush = PdfBrushes.OrangeRed;
            firstRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
            pdfGrid.Rows[0].ApplyStyle(firstRowStyle);

            PdfGridCellStyle totalRowStyle = new PdfGridCellStyle();
            totalRowStyle.TextBrush = PdfBrushes.DarkBlue;
            totalRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);

            if (dI > 0) pdfGrid.Rows[dI].ApplyStyle(totalRowStyle);
            if (rI > 0) pdfGrid.Rows[rI].ApplyStyle(totalRowStyle);
            //pdfGrid.Rows[5].ApplyStyle(firstRowStyle);

            PdfGridCellStyle footerStyle = new PdfGridCellStyle();
            footerStyle.Borders.All = new PdfPen(new PdfColor(Color.RebeccaPurple));
            footerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray)); ;
            footerStyle.TextBrush = PdfBrushes.Red;
            footerStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 13f, PdfFontStyle.Italic);
            lastRow.ApplyStyle(footerStyle);

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = pdfGrid.Draw(page,
                new RectangleF(
                    new PointF(0, result.Bounds.Bottom + 10),
                    new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);
            //Draw Bill line Page Break Line
            graphics.DrawLine(new PdfPen(new PdfColor(Color.DarkBlue)), new PointF(0, gridResult.Bounds.Bottom + 20), new PointF(graphics.ClientSize.Width, gridResult.Bounds.Bottom + 20));
            return page;
        }

        private string GeneratePdfWithMultiple(List<PettyCashSheet> pList)
        {
            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.All = 50;
                int pageCount = 1 + (pList.Count / 2);
                List<PdfPage> pages = new List<PdfPage>();
                for (int i = 0; i < pageCount; i++)
                    pages.Add(document.Pages.Add());

                int pageNo = 0;
                for (int i = 0; i < pList.Count; i++)
                {
                    var result = GenerateFirstPettyCashSheet(pList[i], pages[pageNo]);
                    i++;
                    if (i < pList.Count)
                    {
                        GenerateLastPettyCashSheet(pList[i], pages[pageNo], result);
                    }
                    pageNo++;
                }
                //Save the document.
                document.Save("MissingDailySheet.pdf");
                //Close the document.
                document.Close(true);
                return "MissingDailySheet.pdf";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        private DataTable ToCashTable()
        {
            try
            {
                DataTable dataTable = new DataTable();
                if (cashDetail == null) { return null; }
                //Add columns to the DataTable
                dataTable.Columns.Add("Sn");
                dataTable.Columns.Add("Denomination");
                dataTable.Columns.Add("Count");
                dataTable.Columns.Add("Amount");

                // Adding rows to the Datatable
                dataTable.Rows.Add(new object[] { "1", "2000", cashDetail.N2000, cashDetail.N2000 * 2000 });
                dataTable.Rows.Add(new object[] { "2", "1000", cashDetail.N1000, cashDetail.N1000 * 1000 });
                dataTable.Rows.Add(new object[] { "3", "500", cashDetail.N500, cashDetail.N500 * 500 });
                dataTable.Rows.Add(new object[] { "4", "200", cashDetail.N200, cashDetail.N200 * 200 });
                dataTable.Rows.Add(new object[] { "5", "100", cashDetail.N100, cashDetail.N100 * 100 });
                dataTable.Rows.Add(new object[] { "6", "50", cashDetail.N50, cashDetail.N50 * 50 });
                dataTable.Rows.Add(new object[] { "7", "20", cashDetail.N20, cashDetail.N20 * 20 });
                dataTable.Rows.Add(new object[] { "8", "10", cashDetail.N10, cashDetail.N10 * 10 });
                dataTable.Rows.Add(new object[] { "9", "Coin 10", cashDetail.C10, cashDetail.C10 * 10 });
                dataTable.Rows.Add(new object[] { "10", "Coin 5", cashDetail.C5, cashDetail.C5 * 5 });
                dataTable.Rows.Add(new object[] { "11", "Coin 2", cashDetail.C2, cashDetail.C2 * 2 });
                dataTable.Rows.Add(new object[] { "12", "Coin 1", cashDetail.C1, cashDetail.C1 * 1 });
                //  dataTable.Rows.Add(new object[] { " ", " ", " "," " });
                dataTable.Rows.Add(new object[] { "Total Count", cashDetail.Count, "Total Amount", cashDetail.TotalAmount });

                return dataTable;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private DataTable ToDataTable(out int rI, out int dI)
        {
            try
            {
                DataTable dataTable = new DataTable();

                //Add columns to the DataTable
                dataTable.Columns.Add("Receipt");
                dataTable.Columns.Add("Amount");
                dataTable.Columns.Add("Payment");
                dataTable.Columns.Add("Amounts");

                //Add rows to the DataTable
                dataTable.Rows.Add(new object[] { "Opening Balance", pcs.OpeningBalance, "Closing Balance", pcs.ClosingBalance });
                dataTable.Rows.Add(new object[] { "Sale", pcs.DailySale, "Card Sale", pcs.CardSale });
                dataTable.Rows.Add(new object[] { "Manual Sale", pcs.ManualSale, "Non Cash Sale", pcs.NonCashSale });
                dataTable.Rows.Add(new object[] { "Tailoring", pcs.TailoringSale, "Tailoring Payment", pcs.TailoringPayment });
                dataTable.Rows.Add(new object[] { "Withdrwal", pcs.TailoringSale, "Deposit", pcs.BankDeposit });

                var nar1 = pcs.ReceiptsNaration.Split('#');
                var nar2 = pcs.PaymentNaration.Split('#');

                List<RowData> rows = new List<RowData>();
                int count = nar1.Length > nar2.Length ? nar1.Length : nar2.Length;
                for (int i = 0; i < count; i++)
                    rows.Add(new RowData());

                int rC = 0;
                foreach (var row in nar1)
                {
                    if (!string.IsNullOrEmpty(row.Trim()) && row.Contains(":"))
                    {
                        var x = row.Split(":");
                        rows[rC].Name1 = x[0];
                        rows[rC].Value1 = x[1];
                        rC++;
                    }
                }
                rC = 0;
                foreach (var row in nar2)
                {
                    if (!string.IsNullOrEmpty(row.Trim()) && row.Contains(":"))
                    {
                        var x = row.Split(":");
                        rows[rC].Name2 = x[0];
                        rows[rC].Value2 = x[1];
                        rC++;
                    }
                }

                nar1 = pcs.RecoveryList.Split('#');
                nar2 = pcs.DueList.Split('#');

                List<RowData> cDue = new List<RowData>();
                count = nar1.Length > nar2.Length ? nar1.Length : nar2.Length;
                for (int i = 0; i < count; i++)
                    cDue.Add(new RowData());

                rC = 0;
                foreach (var row in nar1)
                {
                    if (!string.IsNullOrEmpty(row.Trim()) && row.Contains(":"))
                    {
                        var x = row.Split(":");
                        cDue[rC].Name1 = x[0];
                        cDue[rC].Value1 = x[1];
                        rC++;
                    }
                }
                rC = 0;
                foreach (var row in nar2)
                {
                    if (!string.IsNullOrEmpty(row.Trim()) && row.Contains(":"))
                    {
                        var x = row.Split(":");
                        cDue[rC].Name2 = x[0];
                        cDue[rC].Value2 = x[1];
                        rC++;
                    }
                }

                dI = rI = 0;

                if (cDue.Count > 0)
                {
                    dI = 5;
                    dataTable.Rows.Add(new object[] { "Customer Recovered", pcs.CustomerRecovery, "Customer Due", pcs.CustomerDue });
                }

                foreach (var item in cDue)
                {
                    if (!string.IsNullOrEmpty(item.Name1) || !string.IsNullOrEmpty(item.Name2))
                    {
                        dataTable.Rows.Add(new object[] { item.Name1, item.Value1, item.Name2, item.Value2 });
                        rI++;
                    }
                }

                if (rows.Count > 0)
                {
                    dataTable.Rows.Add(new object[] { "Receipts", pcs.ReceiptsTotal, "Payment", pcs.PaymentTotal });
                    if (dI == 5) rI += dI + 1; else rI = 5;
                }
                foreach (var item in rows)
                {
                    if (!string.IsNullOrEmpty(item.Name1) || !string.IsNullOrEmpty(item.Name2))
                        dataTable.Rows.Add(new object[] { item.Name1, item.Value1, item.Name2, item.Value2 });
                }

                //TODO: need to create total balance.

                var recT = pcs.DailySale + pcs.TailoringSale + pcs.OpeningBalance + pcs.BankWithdrawal + pcs.ReceiptsTotal + pcs.CustomerRecovery + pcs.ManualSale;
                var payT = pcs.ClosingBalance + pcs.CardSale + pcs.NonCashSale + pcs.PaymentTotal + pcs.BankWithdrawal + pcs.CustomerDue + pcs.TailoringPayment;
                dataTable.Rows.Add(new object[] { "   ", "   ", "    ", "         " });
                dataTable.Rows.Add(new object[] { "Total", recT, "Total", payT });

                return dataTable;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                rI = 0; dI = 0;
                return null;
            }
        }
    }

    public class UIManager
    {
        public static int GetIntField(NumericUpDown nud)
        {
            return (int)nud.Value;
        }

        public static int GetIntLable(Label lb)
        {
            return Int32.Parse(lb.Text.Trim());
        }

        public static decimal ReadDec(TextBox text)
        {
            return decimal.Parse(text.Text.Trim());
        }

        public static int ReadInt(TextBox text)
        {
            return Int32.Parse(text.Text.Trim());
        }
    }

    internal class RowData
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}