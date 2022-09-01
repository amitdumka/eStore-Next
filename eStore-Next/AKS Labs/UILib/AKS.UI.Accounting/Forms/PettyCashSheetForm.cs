using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using ASK.UI.Libs;
using System.Data;

namespace AKS.UI.Accounting.Forms
{
    public partial class PettyCashSheetForm : Form
    {
        private PettyCashSheetViewModel _viewModel;
        private bool EnableCashAdd = false;

        public PettyCashSheetForm()
        {
            InitializeComponent();
            _viewModel = new PettyCashSheetViewModel();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                this.tabControl1.SelectedIndex = 1;
                Reset();
                _viewModel.SetAdd();
            }
            else if (btnAdd.Text == "Add Cash")
            {
                if (_viewModel.SaveCashDetail(ReadCashDetails()))
                {
                    btnAdd.Text = "Add";
                    this.tabControl1.SelectedIndex = 3;
                    Reset();
                    MessageBox.Show("Cash Details is saved!!");
                    dgvCashDetails.Rows.Add(_viewModel.SavedCashDetail);
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
                    if (_viewModel.SavePettyCash(ReadData()))
                    {
                        btnAdd.Text = "Add Cash";
                        MessageBox.Show("Petty Cash Sheet Add! Kindly now add Cash Sheet");
                        dgvPettyCashSheet.Refresh();
                        if (isNew)
                        {
                            tabControl1.SelectedIndex = 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Some error occured while saveing, kindly check data and try again!");
                    }
                }
                catch (Exception ex)
                {
                    // azureDb.PettyCashSheets.Remove(pcs);
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
                    if (_viewModel.DeletePettyCash(row))
                    {
                        MessageBox.Show("Deleted");
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

        private void FilterData(int year)
        {
            if (DataList.Contains(year) == false)
            {
                UpdateItemList(azureDb.PettyCashSheets.Where(c => c.OnDate.Year == year).ToList());
                DataList.Add(year);
            }
            dgvPettyCashSheet.DataSource = (ItemList.Where(c => c.OnDate.Year == year).ToList());
            dgvCashDetails.DataSource = azureDb.CashDetails.Where(c => c.OnDate.Year == year).ToList();
        }

        private void lbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData((int)lbYearList.SelectedValue);
        }

        private void LoadData()
        {
            Reset();
            cbxStore.DataSource = _viewModel.GetStoreList();
            cbxStore.DisplayMember = "DisplayValue";
            cbxStore.ValueMember = "StoreId";
            lbYearList.DataSource = _viewModel.YearList;
            dgvPettyCashSheet.DataSource = _viewModel.GetCurrentMonth();
            dgvCashDetails.DataSource = _viewModel.GetCashCurrentMonth();
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
            LoadData();
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

        private bool ReadCashDetails()
        {
            try
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        private bool SaveCashDetails(CashDetail cd)
        {
            if (cd != null)
            {
                return _viewModel.SaveCashDetail(true);
            }
            else
            {
                MessageBox.Show("Error occured while reading Cash Details!!");
                return false;
            }
        }

        private void UpdateLabel(Label lb, decimal value)
        {
            var oldVal = UIManager.GetIntLable(lb);
            lb.Text = value.ToString();
            _viewModel.TotalCurrenyAmount += (int)(value - oldVal);
        }

        private void UpdateView()
        {
            if (rbCMonth.Checked)
                dgvPettyCashSheet.DataSource = _viewModel.GetCurrentMonth();
            else if (rbYearly.Checked)
                dgvPettyCashSheet.DataSource = _viewModel.GetCurrentYearly();
            else if (rbLMonth.Checked)
                dgvPettyCashSheet.DataSource = _viewModel.GetLastMonth();
        }

        private async void ViewPdf()
        {
            string fileName = _viewModel.GeneratePettyCashSheetPdf();

            if (string.IsNullOrEmpty(fileName) == false)
            {
                pdfView.Load(fileName);
                btnPrint.Enabled = true;
                this.tabControl1.SelectedIndex = 3;
            }
        }
    }//end of class

    internal class RowData
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}