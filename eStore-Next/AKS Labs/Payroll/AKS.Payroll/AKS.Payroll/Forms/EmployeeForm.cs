using AKS.DatabaseMigrator;
using AKS.Payroll.Database;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

//https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/databinding/winforms
namespace AKS.Payroll.Forms
{
    public partial class EmployeeForm : Form
    {
        private PayrollDbContext context;
        public EmployeeForm()
        {
            InitializeComponent();
        }
        private void EmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Dispose();
        }
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
          
            LoadData();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void LoadData()
        {
            context = new PayrollDbContext();
            context.Employees.Load();
            context.EmployeeDetails.Load();
            dgvEmployee.DataSource = context.Employees.Local.ToBindingList();
        }

        private void tbcEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // PayrollMigration pm = new PayrollMigration();
           //pm.Migrate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            context.SaveChanges();

        }
    }
}