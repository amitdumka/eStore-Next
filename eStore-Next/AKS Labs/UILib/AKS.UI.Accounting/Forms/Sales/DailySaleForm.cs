using AKS.AccountingSystem.DTO;
using AKS.AccountingSystem.ViewModels;
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
            //Resuing same datamodel to redue memory
            _dueViewModel = new DueViewModel(_viewModel.DataModel);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DailySaleEntryForm form = new DailySaleEntryForm(_viewModel);

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
            DailySaleEntryForm form = new DailySaleEntryForm(_viewModel, sale);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsSaved)
                {
                    _viewModel.dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(_viewModel.SavedSale));
                    dgvSales.Refresh();
                    //TODO: reload Data;
                }
            }
            else if (form.DialogResult == DialogResult.Yes)
            {
                //TODO: moved to VM
                if (form.IsSaved)
                {
                    _viewModel.dailySaleVMs.Remove(_viewModel.dailySaleVMs.Where(c => c.InvoiceNumber == _viewModel.SavedSale.InvoiceNumber).First());
                    _viewModel.dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(_viewModel.SavedSale));
                    dgvSales.Refresh();
                    //TODO: reload Data;
                }
            }
            else if (form.DialogResult == DialogResult.No)
            {
                if (!string.IsNullOrEmpty(form.DeletedI))
                {
                    //TODO: Move to VM
                    if (_viewModel.SavedSale.IsDue || _viewModel.SecondaryEntity != null)
                    {
                        _viewModel.SecondayEntites.Remove(_viewModel.SecondaryEntity);
                        dgvDues.Refresh();
                    }
                    _viewModel.dailySaleVMs.Remove(_viewModel.dailySaleVMs.Where(c => c.InvoiceNumber == form.DeletedI).First());
                    dgvSales.Refresh();
                    //TODO; reload data;
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
            _viewModel.InitViewModel();
            _dueViewModel.InitViewModel();

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

            lbYearList.DataSource = _viewModel.YearList;
        }

        private void LoadDueData()
        {
            _viewModel.GetDueData();
            _dueViewModel.GetCurrentRecoveryData();

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