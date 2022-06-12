using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class PaySlipForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;

        private ObservableListSource<PaySlipVM> PaySlips;

        public PaySlipForm()
        {
            InitializeComponent();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are your sure to generate payslip for current month payable of last working month for all Employees ??",
                                      "Confirm !!",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                UpdateUI(new PayslipManager().GeneratePayslips(null, DateTime.Today.AddMonths(-1)).Result);
            }
        }

        private async void UpdateUI(SortedDictionary<string, PaySlip> paySlips)
        {
            if (paySlips != null)
            {
                foreach (var paySlip in paySlips)
                {
                    PaySlips.Add(DMMapper.Mapper.Map<PaySlipVM>(paySlip.Value));
                }
                PaySlips.Distinct();

                //dgvPayslips.DataSource = PaySlips.Where(c => c.OnDate.Date == date.Date).ToList();
                tsslCountValue.Text = $" {paySlips.Count}/{PaySlips.Count}";
                var date = paySlips.First().Value.OnDate.Date;
                dgvPayslips.DataSource = PaySlips.Where(c => c.OnDate.Date == date.Date).ToList();
            }
        }

        private void btnSelectedEmployee_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are your sure to generate payslip for current month payable of last working month for selected Employee ??",
                                     "Confirm !!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                MessageBox.Show("Not implemented, Kindly contact admin.");
            }
        }

        private void PaySlipForm_Load(object sender, EventArgs e)
        {
            DMMapper.InitializeAutomapper();
            PaySlips = new ObservableListSource<PaySlipVM>();
            using (azureDb = new AzurePayrollDbContext())
            {
                lbEmployees.DataSource = azureDb.Employees.ToList();
                lbEmployees.DisplayMember = "StaffName";
                lbEmployees.ValueMember = "EmployeeId";
                LoadData(azureDb.PaySlips.Where(c => c.Year == DateTime.Today.Year).ToList());
            }
        }

        private async void LoadData(List<PaySlip> slips)
        {
            if (slips != null)
            {
                foreach (var slip in slips)
                {
                    PaySlips.Add(DMMapper.Mapper.Map<PaySlipVM>(slip));
                }
            }
            dgvPayslips.DataSource = PaySlips.ToBindingList();
            tsslCountValue.Text = PaySlips.Count.ToString();
        }

        private void btnPrintPayslip_Click(object sender, EventArgs e)
        {

        }

        private void btnProcessAll_Click(object sender, EventArgs e)
        {

        }
    }
}