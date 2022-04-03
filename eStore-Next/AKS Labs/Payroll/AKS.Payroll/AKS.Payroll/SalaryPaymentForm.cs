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

            UpdateSalaryPaymentList(azureDb.SalaryPayment.Where(c => c.OnDate.Year == DateTime.Today.Year
            && c.OnDate.Month == DateTime.Today.Month).ToList());

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
    }
}
