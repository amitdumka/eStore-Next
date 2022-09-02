using AKS.AccountingSystem.DTO;
using AKS.AccountingSystem.ViewModels;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Sales;
using AKS.UI.Accounting.Forms.EntryForms;
using System.Data;

namespace AKS.UI.Accounting.Forms
{
    public partial class DailySaleForm : Form
    {
        private static string StoreCode = "ARD";

        private DueViewModel _dueViewModel;
        private DailySaleViewModel _viewModel;

        // private AzurePayrollDbContext azureDb;

        public DailySaleForm()
        {
            InitializeComponent();
            _viewModel = new DailySaleViewModel();
            _dueViewModel = new DueViewModel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DailySaleEntryForm form = new DailySaleEntryForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsSaved)
                {
                    MessageBox.Show("Daily Sale is saved!");
                    return;
                }
            }
            else
            {
            }
        }

        private void btnDueRecovery_Click(object sender, EventArgs e)
        {
            DueRecoveryEntryForm form = new DueRecoveryEntryForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void DailySaleForm_Load(object sender, EventArgs e)
        {
            LoadData();
            DisplaySale();
        }

        private void dgvRecovered_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dueRec = (DueRecovery)dgvRecovered.CurrentRow.DataBoundItem;
            DueRecoveryEntryForm form = new DueRecoveryEntryForm(dueRec);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsSave)
                {
                    //_dueViewModel.SecondayEntites.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    _dueViewModel.SecondayEntites.Add(form.DueRecovery);
                    dgvRecovered.Refresh();
                    //TODO: reload Data;
                }
            }
            else if (form.DialogResult == DialogResult.Yes)
            {
                if (form.IsSave)
                {
                    //_dueViewModel.SecondayEntites.Remove(_viewModel.dailySaleVMs.Where(c => c.InvoiceNumber == form.sale.InvoiceNumber).First());
                    //_dueViewModel.SecondayEntites.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    _dueViewModel.SecondayEntites.Remove(form.DueRecovery);
                    _dueViewModel.SecondayEntites.Add(form.DueRecovery);

                    dgvRecovered.Refresh();
                    //TODO: reload Data;
                }
            }
            else if (form.DialogResult == DialogResult.No)
            {
                if (!string.IsNullOrEmpty(form.DeleteId))
                {
                    if (!form.DueRecovery.ParticialPayment)
                    {
                        var dd = _viewModel.SecondayEntites.Where(c => c.InvoiceNumber == form.DueRecovery.InvoiceNumber).First();
                        if (dd != null) _viewModel.SecondayEntites.Remove(dd);
                        dgvDues.Refresh();
                    }
                    _dueViewModel.SecondayEntites.Remove(form.DueRecovery);
                    dgvRecovered.Refresh();
                    //TODO; realod data;
                }
            }
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var sale = DMMapper.Mapper.Map<DailySale>(dgvSales.CurrentRow.DataBoundItem);
            DailySaleEntryForm form = new DailySaleEntryForm(sale);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsSaved)
                {
                    _viewModel.dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    dgvSales.Refresh();
                    //TODO: reload Data;
                }
            }
            else if (form.DialogResult == DialogResult.Yes)
            {
                if (form.IsSaved)
                {
                    _viewModel.dailySaleVMs.Remove(_viewModel.dailySaleVMs.Where(c => c.InvoiceNumber == form.sale.InvoiceNumber).First());
                    _viewModel.dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    dgvSales.Refresh();
                    //TODO: reload Data;
                }
            }
            else if (form.DialogResult == DialogResult.No)
            {
                if (!string.IsNullOrEmpty(form.DeletedI))
                {
                    if (form.sale.IsDue || form.CustomerDue != null)
                    {
                        _viewModel.SecondayEntites.Remove(form.CustomerDue);
                        dgvDues.Refresh();
                    }
                    _viewModel.dailySaleVMs.Remove(_viewModel.dailySaleVMs.Where(c => c.InvoiceNumber == form.DeletedI).First());
                    dgvSales.Refresh();
                    //TODO; realod data;
                }
            }
        }

        private async void DisplaySale()
        {
            _viewModel.SetDisplaySale();
            await Task.Delay(15000);

            tsslMonthly.Text = _viewModel.tsslMonthly;//.ToString();
            sslToday.Text = _viewModel.sslToday;
        }

        private void FilterData(RadioButton rb)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (rb == rbCMonth)
                {
                    dgvSales.DataSource = _viewModel.GetCurrentMonthSale();
                }
                else if (rb == rbYearly)
                {
                    dgvSales.DataSource = _viewModel.GetCurrentYearSale();
                }
                else if (rb == lbLMonth)
                {
                    dgvSales.DataSource = _viewModel.GetLastMonthSale(); ;
                }
            }
        }

        private void LoadData()
        {
            //DMMapper.InitializeAutomapper();
            //var data = azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman).Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year
            //&& c.OnDate.Month == DateTime.Today.Month).OrderByDescending(c => c.OnDate).ToList();
            //UpdateSaleList(data);

            dgvSales.DataSource = _viewModel.dailySaleVMs;
            dgvSales.ScrollBars = ScrollBars.Both;

            dgvSales.Columns["SalesmanId"].Visible = false;
            dgvSales.Columns["EDCTerminalId"].Visible = false;
            dgvSales.Columns["StoreId"].Visible = false;
            dgvSales.Columns["EntryStatus"].Visible = false;
            dgvSales.Columns["UserId"].Visible = false;
            dgvSales.Columns["IsReadOnly"].Visible = false;
            dgvSales.Columns["MarkedDeleted"].Visible = false;
            dgvSales.Columns["Store"].Visible = false;
            // dgvSales.Columns["EDC"].Visible = false;
            // dgvSales.Columns["Saleman"].Visible = false;

            //YearList.AddRange(azureDb.DailySales
            //    .Where(c => c.StoreId == StoreCode).Select(c => c.OnDate.Year)
            //    .Distinct().OrderBy(c => c).ToList());
            //if (YearList.Contains(DateTime.Today.Year) == false)
            //    YearList.Add(DateTime.Today.Year);
            lbYearList.DataSource = _viewModel.YearList;
        }

        private void LoadDueData()
        {
            if (_viewModel.SecondayEntites == null || _viewModel.SecondayEntites.Count() == 0)
            {
                if (_viewModel.SecondayEntites == null) _viewModel.SecondayEntites = new List<CustomerDue>();
                _viewModel.SecondayEntites.AddRange(azureDb.CustomerDues
                    .Where(c => c.StoreId == StoreCode && !c.Paid)
                    .OrderByDescending(c => c.OnDate)
                    .ToList());
                if (_dueViewModel.SecondayEntites == null) _dueViewModel.SecondayEntites = new List<DueRecovery>();
                _dueViewModel.SecondayEntites.AddRange(azureDb.DueRecovery.Where(c => c.StoreId == StoreCode &&
                c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());
            }

            if (!_viewModel.DueDataLoaded)
            {
                dgvDues.DataSource = _viewModel.SecondayEntites;
                dgvRecovered.DataSource = _dueViewModel.SecondayEntites;
                _viewModel.DueDataLoaded = true;
                dgvDues.Columns["Store"].Visible = false;
                dgvDues.Columns["IsReadOnly"].Visible = false;
                dgvDues.Columns["EntryStatus"].Visible = false;
                dgvDues.Columns["USerId"].Visible = false;
                dgvDues.Columns["MarkedDeleted"].Visible = false;

                dgvRecovered.Columns["Store"].Visible = false;
                dgvRecovered.Columns["IsReadOnly"].Visible = false;
                dgvRecovered.Columns["EntryStatus"].Visible = false;
                dgvRecovered.Columns["USerId"].Visible = false;
                dgvRecovered.Columns["MarkedDeleted"].Visible = false;
            }
        }

        private void RadioBoxes_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                this.FilterData((RadioButton)sender);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2 || tabControl1.SelectedTab == tabPage3)
            {
                LoadDueData();
            }
        }
    }
}