using AKS.AccountingSystem.ViewModels;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using ASK.UI.Libs;

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
                        if (_viewModel.isNew)

                            tabControl1.SelectedIndex = 2;
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
            _viewModel.tDue += UIManager.ReadDec(txtDueAmount);
            _viewModel.dNar += $"#{txtDueNaration.Text} : {txtDueAmount.Text}";

            lbPay.Text = lbPay.Text + "\n" + txtDueNaration.Text;
            lbPayList.Text = lbPayList.Text + "\n" + txtDueAmount.Text;
            txtDueAmount.Text = "0";
            txtDueNaration.Text = "";
        }

        private void btnMissingReport_Click(object sender, EventArgs e)
        {
            string filename = _viewModel.MissingReport();

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
            _viewModel.tPay += UIManager.ReadDec(txtAmount);
            _viewModel.pNar += $"#{txtNaration.Text} : {txtAmount.Text}";
            lbPay.Text = lbPay.Text + "\n" + txtNaration.Text;
            lbPayList.Text = lbPayList.Text + "\n" + txtAmount.Text;
            txtAmount.Text = "0";
            txtNaration.Text = "";
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (_viewModel.FetchTodayOrYesterday())

                ViewPdf();
            else

                MessageBox.Show("No Record Found");
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
            _viewModel.tRec += UIManager.ReadDec(txtAmount);
            _viewModel.rNar += $"#{txtNaration.Text} : {txtAmount.Text}";
            lbRec.Text = lbRec.Text + "\n" + txtNaration.Text;
            lbRecList.Text = lbRecList.Text + "\n" + txtAmount.Text;
            txtAmount.Text = "0";
            txtNaration.Text = "";
        }

        private void btnRecovery_Click(object sender, EventArgs e)
        {
            _viewModel.tdRec += UIManager.ReadDec(txtDueAmount);
            _viewModel.rcNar += $"#{txtDueNaration.Text} : {txtDueAmount.Text}";
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
                    _viewModel.PrimaryEntity = row;
                    _viewModel.SecondaryEntity = _viewModel.GetSecondary(row.OnDate);

                    ViewPdf();
                }
            }
        }

        private void FilterData(int year)
        {
            _viewModel.Filter(year);
            dgvPettyCashSheet.DataSource = _viewModel.GetYearly(year);
            dgvCashDetails.DataSource = _viewModel.GetCashYearly(year);
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
            _viewModel.TotalCurreny += (int)field.Value;
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
            lbTotalAmount.Text = _viewModel.TotalCurrenyAmount.ToString();
            lbCount.Text = _viewModel.TotalCurreny.ToString();
        }

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

        private bool ReadData()
        {
            try
            {
                if (_viewModel.isNew)
                {
                    if (_viewModel.dNar == null)
                        _viewModel.dNar = "#";

                    if (_viewModel.pNar == null)
                        _viewModel.pNar = "#";

                    if (_viewModel.rcNar == null)
                        _viewModel.rcNar = "#";

                    if (_viewModel.rNar == null)
                        _viewModel.rNar = "#";

                    _viewModel.PrimaryEntity = new PettyCashSheet()
                    {
                        BankDeposit = UIManager.ReadDec(txtBankDeposit),
                        BankWithdrawal = UIManager.ReadDec(txtWithdrawal),
                        CardSale = UIManager.ReadDec(txtCardSale),
                        ClosingBalance = UIManager.ReadDec(txtCashInHand),

                        DailySale = UIManager.ReadDec(txtSale),
                        ManualSale = UIManager.ReadDec(txtManualSale),
                        OnDate = dtpOnDate.Value,
                        OpeningBalance = UIManager.ReadDec(txtOpeningBalance),

                        PaymentTotal = _viewModel.tPay,
                        PaymentNaration = _viewModel.pNar,
                        ReceiptsNaration = _viewModel.rNar,
                        ReceiptsTotal = _viewModel.tRec,

                        DueList = _viewModel.dNar,
                        RecoveryList = _viewModel.rcNar,
                        CustomerDue = _viewModel.tDue,
                        CustomerRecovery = _viewModel.tdRec,

                        NonCashSale = UIManager.ReadDec(txtNonCashSale),
                        TailoringPayment = UIManager.ReadDec(txtTailoringPayment),
                        TailoringSale = UIManager.ReadDec(txtTailoring),
                        Id = "",
                    };
                    // _viewModel.PrimaryEntity .Id = $"{StoreId}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                    _viewModel.PrimaryEntity.Id = $"ARD/{_viewModel.PrimaryEntity.OnDate.Year}/{_viewModel.PrimaryEntity.OnDate.Month}/{_viewModel.PrimaryEntity.OnDate.Day}";
                }
                else
                {
                    _viewModel.PrimaryEntity.BankDeposit = UIManager.ReadDec(txtBankDeposit);
                    _viewModel.PrimaryEntity.BankWithdrawal = UIManager.ReadDec(txtWithdrawal);
                    _viewModel.PrimaryEntity.CardSale = UIManager.ReadDec(txtCardSale);
                    _viewModel.PrimaryEntity.ClosingBalance = UIManager.ReadDec(txtCashInHand);

                    _viewModel.PrimaryEntity.CustomerDue = _viewModel.tDue;
                    _viewModel.PrimaryEntity.CustomerRecovery = _viewModel.tdRec;

                    _viewModel.PrimaryEntity.DailySale = UIManager.ReadDec(txtSale);
                    _viewModel.PrimaryEntity.Id = lbPrimaryKey.Text;
                    _viewModel.PrimaryEntity.ManualSale = UIManager.ReadDec(txtManualSale);
                    _viewModel.PrimaryEntity.OnDate = dtpOnDate.Value;
                    _viewModel.PrimaryEntity.OpeningBalance = UIManager.ReadDec(txtOpeningBalance);
                    _viewModel.PrimaryEntity.NonCashSale = UIManager.ReadDec(txtNonCashSale);

                    _viewModel.PrimaryEntity.PaymentTotal = _viewModel.tPay;
                    _viewModel.PrimaryEntity.PaymentNaration = _viewModel.pNar;

                    _viewModel.PrimaryEntity.ReceiptsNaration = _viewModel.rNar;

                    _viewModel.PrimaryEntity.DueList = _viewModel.dNar;

                    _viewModel.PrimaryEntity.RecoveryList = _viewModel.rcNar;
                    _viewModel.PrimaryEntity.ReceiptsTotal = _viewModel.tdRec;

                    _viewModel.PrimaryEntity.TailoringPayment = UIManager.ReadDec(txtTailoringPayment);
                    _viewModel.PrimaryEntity.TailoringSale = UIManager.ReadDec(txtTailoring);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
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

    
}