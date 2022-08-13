using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Payroll.Forms.EntryForms;
using AKS.Payroll.Forms.Inventory.Functions;
using AKS.Payroll.Ops;
using AKS.Shared.Commons.Models.Sales;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

namespace AKS.Payroll.Forms
{
    public partial class DailySaleForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private ObservableListSource<DailySaleVM> dailySaleVMs;
        private List<int> YearList;
        private List<int> YearDataList;
        private static string StoreCode = "ARD";
        private List<DueRecovery> dueRecoveryList;
        private List<CustomerDue> customerDues;

        public DailySaleForm()
        {
            InitializeComponent();
        }

        private async void DisplaySale()
        {
            await Task.Delay(15000);
            Sales.FetchSaleDetails(azureDb, "ARD");
            tsslMonthly.Text = $"Monthly [Total Sales: Rs. {Sales.MonthlySale} Cash: Rs. {Sales.MonthlyCashSale}  Non Cash: Rs. {Sales.MonthlyNonCashSale}  ] ";
            sslToday.Text = $"Today [Total Sales: Rs. {Sales.TodaySale} Cash: Rs. {Sales.TodayCashSale}  Non Cash: Rs. {Sales.TodayNonCashSale}  ] ";

        }

        private void DailySaleForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            dailySaleVMs = new ObservableListSource<DailySaleVM>();
            YearDataList = new List<int>();
            YearList = new List<int>();
            LoadData();
            DisplaySale(); 

        }

        private void FilterData(RadioButton rb)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (rb == rbCMonth)
                {
                    if (dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.Month
                   && c.OnDate.Year == DateTime.Today.Year).Any() == false)
                    {
                        UpdateSaleList(azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman).Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());
                    }
                    dgvSales.DataSource = dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList();
                }
                else if (rb == rbYearly)
                {
                    if (dailySaleVMs.Where(c => c.OnDate.Year == DateTime.Today.Year).Any() == false)
                    {
                        UpdateSaleList(azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman).Where(c => c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());
                    }
                    dgvSales.DataSource = dailySaleVMs.Where(c => c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList();

                }
                else if (rb == lbLMonth)
                {
                    if (dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).Any() == false)
                    {
                        UpdateSaleList(azureDb.DailySales.Include(c => c.EDC).Include(c => c.Saleman).Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());
                    }
                    dgvSales.DataSource = dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                      && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList();
                }
            }
        }


        private void LoadData()
        {
            DMMapper.InitializeAutomapper();
            var data = azureDb.DailySales.Include(c => c.Store).Include(c => c.EDC).Include(c => c.Saleman).Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year
            && c.OnDate.Month == DateTime.Today.Month).OrderByDescending(c => c.OnDate).ToList();
            UpdateSaleList(data);

            dgvSales.DataSource = dailySaleVMs;
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

            YearList.AddRange(azureDb.DailySales
                .Where(c => c.StoreId == StoreCode).Select(c => c.OnDate.Year)
                .Distinct().OrderBy(c => c).ToList());
            if (YearList.Contains(DateTime.Today.Year) == false)
                YearList.Add(DateTime.Today.Year);
            lbYearList.DataSource = YearList;


        }
        private void UpdateSaleList(List<DailySale> sales)
        {
            foreach (var sale in sales)
                dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(sale));
        }

        private void UpdateSaleList(List<DailySale> sales, int year)
        {
            if (!YearDataList.Any(c => c == year))
            {
                foreach (var sale in sales)
                    DMMapper.Mapper.Map<DailySaleVM>(sale);
            }
        }
        private bool DueDataLoaded = false;
        private void LoadDueData()
        {
            if (customerDues == null || customerDues.Count() == 0)
            {
                if (customerDues == null) customerDues = new List<CustomerDue>();
                customerDues.AddRange(azureDb.CustomerDues
                    .Where(c => c.StoreId == StoreCode && !c.Paid)
                    .OrderByDescending(c => c.OnDate)
                    .ToList());
                if (dueRecoveryList == null) dueRecoveryList = new List<DueRecovery>();
                dueRecoveryList.AddRange(azureDb.DueRecovery.Where(c => c.StoreId == StoreCode &&
                c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());

            }

            if (!DueDataLoaded)
            {
                dgvDues.DataSource = customerDues;
                dgvRecovered.DataSource = dueRecoveryList;
                DueDataLoaded = true;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DailySaleEntryForm form = new DailySaleEntryForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsSaved)
                {
                    dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    dgvSales.Refresh();
                    //TODO: reload Data; 


                }
            }
            else
            {

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
                    dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    dgvSales.Refresh();
                    //TODO: reload Data; 


                }
            }
            else if (form.DialogResult == DialogResult.Yes)
            {
                if (form.IsSaved)
                {
                    dailySaleVMs.Remove(dailySaleVMs.Where(c => c.InvoiceNumber == form.sale.InvoiceNumber).First());
                    dailySaleVMs.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
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
                        customerDues.Remove(form.CustomerDue);
                        dgvDues.Refresh();
                    }
                    dailySaleVMs.Remove(dailySaleVMs.Where(c => c.InvoiceNumber == form.DeletedI).First());
                    dgvSales.Refresh();
                    //TODO; realod data; 
                }
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPage2 || tabControl1.SelectedTab == tabPage3)
            {
                LoadDueData();
            }

        }

        private void btnDueRecovery_Click(object sender, EventArgs e)
        {
            DueRecoveryEntryForm form = new DueRecoveryEntryForm();
            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void dgvRecovered_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // var sale = DMMapper.Mapper.Map<DailySale>(dgvSales.CurrentRow.DataBoundItem);
            var dueRec = (DueRecovery)dgvRecovered.CurrentRow.DataBoundItem;

            DueRecoveryEntryForm form = new DueRecoveryEntryForm(dueRec);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsSave)
                {
                    //dueRecoveryList.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    dueRecoveryList.Add(form.DueRecovery);
                    dgvRecovered.Refresh();
                    //TODO: reload Data; 


                }
            }
            else if (form.DialogResult == DialogResult.Yes)
            {
                if (form.IsSave)
                {
                    //dueRecoveryList.Remove(dailySaleVMs.Where(c => c.InvoiceNumber == form.sale.InvoiceNumber).First());
                    //dueRecoveryList.Add(DMMapper.Mapper.Map<DailySaleVM>(form.sale));
                    dueRecoveryList.Remove(form.DueRecovery);
                    dueRecoveryList.Add(form.DueRecovery);

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
                        var dd = customerDues.Where(c => c.InvoiceNumber == form.DueRecovery.InvoiceNumber).First();
                        if (dd != null) customerDues.Remove(dd);
                        dgvDues.Refresh();
                    }
                    dueRecoveryList.Remove(form.DueRecovery);
                    dgvRecovered.Refresh();
                    //TODO; realod data; 
                }
            }
        }

        private void dgvDues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AddEdc()
        {
            EDCTerminal eDCTerminal = new EDCTerminal
            {
                Active = true,
                BankId = "Bank of Maharastra",
                EDCTerminalId = "EDC/2022/003",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                MID = "NA",
                Name = "PineLab BOM",
                OnDate = DateTime.Now,
                ProviderName = "Pine Labs",
                StoreId = "ARD",
                TID = "NA",
                UserId = "AUTOADMIN"

            };
            EDCTerminal eDCTerminal2 = new EDCTerminal
            {
                Active = true,
                BankId = "AprajitaRetails_ICICI-63005500372",
                EDCTerminalId = "EDC/2022/002",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                MID = "NA",
                Name = "EasyTab ICICI",
                OnDate = DateTime.Now,
                ProviderName = "ICICI Bank",
                StoreId = "ARD",
                TID = "NA",
                UserId = "AUTOADMIN"

            };
            EDCTerminal eDCTerminal3 = new EDCTerminal
            {
                Active = true,
                BankId = "SBI_CC-37604947464",
                EDCTerminalId = "EDC/2022/001",
                EntryStatus = EntryStatus.Added,
                IsReadOnly = true,
                MarkedDeleted = false,
                MID = "NA",
                Name = "SBI",
                OnDate = DateTime.Now,
                ProviderName = "SBI",
                StoreId = "ARD",
                TID = "NA",
                UserId = "AUTOADMIN"

            };
            azureDb.EDCTerminals.Add(eDCTerminal3);
            azureDb.EDCTerminals.Add(eDCTerminal);
            azureDb.EDCTerminals.Add(eDCTerminal2);
            if (azureDb.SaveChanges() > 0) MessageBox.Show("ok"); else MessageBox.Show("no");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {

        }
    }
}
