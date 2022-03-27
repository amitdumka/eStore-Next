using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Forms
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private voiad LoadData()
        {
            using context= new PayrollDbContext();
            context.Employees.Load();
            context.EmployeeDetails.Load();
        }
    }
}
