using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payroll
{
    public partial class SalaryPaymentForm : Form
    {
        private readonly AzurePayrollDbContext azureDb;
        private readonly LocalPayrollDbContext localDb;
        private ObservableListSource<SalaryPaymentVM> Payments { get; set; }
        private ObservableListSource<StaffAdvanceReceiptVM> Reciepts { get; set; }
        public SalaryPaymentForm()
        {
            InitializeComponent();
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateGridView("",DateTime.Now);
        }
        private void LoadData()
        {
            azureDb.Employees.Load();
            Payments = new ObservableListSource<SalaryPaymentVM>();
            Reciepts = new ObservableListSource<StaffAdvanceReceiptVM>();
            if (cbAllEmployees.Checked) lbEmoloyees.DataSource = azureDb.Employees.Local.ToBindingList();
            else lbEmoloyees.DataSource = azureDb.Employees.Local.Where(c => c.IsWorking).ToList();
            lbEmoloyees.ValueMember = "EmployeeId";
            lbEmoloyees.DisplayMember = "StaffName";

            UpdateSalaryPaymentList(azureDb.SalaryPayment.Include(c=>c.Employee).Where(c => c.OnDate.Year == DateTime.Today.Year
            ).ToList());

            dgvPayments.DataSource = Payments.ToBindingList();

            dgvPayments.Columns[0].Visible = false;
            dgvPayments.Columns["EmployeeId"].Visible = false;
            dgvPayments.Columns["StoreId"].Visible = false;


        }
        private void UpdateSalaryPaymentList(List<SalaryPayment> payments)
        {
            foreach (var pay in payments)
            {
                Payments.Add(DMMapper.Mapper.Map<SalaryPaymentVM>(pay));

            }
            if (Payments != null && Payments.Count > 0)
              Payments.Distinct();
        }
        private void SalaryPaymentForm_Load(object sender, EventArgs e)
        {
            DMMapper.InitializeAutomapper();
            LoadData();
        }

        private void lbEmoloyees_DoubleClick(object sender, EventArgs e)
        {
            var x = ((System.Windows.Forms.ListBox)sender);
            UpdateGridView(x.SelectedValue.ToString(), DateTime.Now);
        }

        private void UpdateGridView(string empId, DateTime date)
        {
            if (string.IsNullOrEmpty(empId)){
                dgvPayments.DataSource = Payments.ToBindingList();
            }
            else if(!Payments.Any(c => c.EmployeeId == empId))
            {
                UpdateSalaryPaymentList(azureDb.SalaryPayment.Where(c=>c.EmployeeId == empId).ToList());
                dgvPayments.DataSource = Payments.Where(c => c.EmployeeId == empId).ToList();
            }
            else
            dgvPayments.DataSource = Payments.Where(c => c.EmployeeId == empId).ToList();

        }

        private void cbAllEmployees_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbAllEmployees.Checked)
                lbEmoloyees.DataSource = azureDb.Employees.Local.ToBindingList();
            else
                lbEmoloyees.DataSource = azureDb.Employees.Local.Where(c => c.IsWorking).ToList();
            lbEmoloyees.Refresh();
        }
    }
}
