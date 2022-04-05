using AKS.Payroll.Database;
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
    public partial class PaySlipForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;


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

            }


        }

        private void btnSelectedEmployee_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are your sure to generate payslip for current month payable of last working month for selected Employee ??",
                                     "Confirm !!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

            }
        }

        private void PaySlipForm_Load(object sender, EventArgs e)
        {
            using (azureDb = new AzurePayrollDbContext())
            {
                lbEmployees.DataSource = azureDb.Employees.ToList();
                lbEmployees.DisplayMember = "StaffName";
                lbEmployees.ValueMember = "EmployeeId";
            }
        }
        private async void LoadData()
        {
        }
        

    }
}
