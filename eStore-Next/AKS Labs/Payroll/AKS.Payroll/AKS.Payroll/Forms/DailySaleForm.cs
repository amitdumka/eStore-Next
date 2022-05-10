using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Shared.Commons.Models.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        private void DailySaleForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            dailySaleVMs = new ObservableListSource<DailySaleVM>();
            YearDataList = new List<int>();
            YearList = new List<int>();
            LoadData();
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
                        UpdateSaleList(azureDb.DailySales.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());
                    }
                    dgvSales.DataSource = dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList();
                }
                else if (rb == rbYearly)
                {
                    if (dailySaleVMs.Where(c => c.OnDate.Year == DateTime.Today.Year).Any() == false)
                    {
                        UpdateSaleList(azureDb.DailySales.Where(c => c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());
                    }
                    dgvSales.DataSource = dailySaleVMs.Where(c => c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList();

                }
                else if (rb == lbLMonth)
                {
                    if (dailySaleVMs.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
                    && c.OnDate.Year == DateTime.Today.Year).Any() == false)
                    {
                        UpdateSaleList(azureDb.DailySales.Where(c => c.OnDate.Month == DateTime.Today.AddMonths(-1).Month
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
            UpdateSaleList(azureDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year
            && c.OnDate.Month == DateTime.Today.Month).OrderByDescending(c => c.OnDate).ToList());

            dgvSales.DataSource = dailySaleVMs;
            dgvSales.ScrollBars = ScrollBars.Both;

            dgvSales.Columns["SalemanId"].Visible = false;
            dgvSales.Columns["EDCTerminalId"].Visible = false;
            dgvSales.Columns["StoreId"].Visible = false;
            dgvSales.Columns["EntryStatus"].Visible = false;

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
        private void LoadDueData()
        {
            if (customerDues == null || customerDues.Count() == 0)
            {
                customerDues.AddRange(azureDb.CustomerDues
                    .Where(c => c.StoreId == StoreCode && !c.Paid)
                    .OrderByDescending(c => c.OnDate)
                    .ToList());

                dueRecoveryList.AddRange(azureDb.DueRecovery.Where(c => c.StoreId == StoreCode &&
                c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToList());

            }

            dgvDues.DataSource = customerDues;
            dgvRecovered.DataSource = dueRecoveryList;

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
            if (form.ShowDialog() == DialogResult.Yes)
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
                if (string.IsNullOrEmpty(form.DeletedI))
                {
                    dailySaleVMs.Remove(dailySaleVMs.Where(c => c.InvoiceNumber == form.DeletedI).First());
                    dgvSales.Refresh();
                    //TODO; realod data; 
                }
            }
        }
    }
}
