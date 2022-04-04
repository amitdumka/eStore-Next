using AKS.Payroll.Database;
using AKS.Payroll.Forms.EntryForms;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class SalaryForm : Form
    {
        private readonly AzurePayrollDbContext azureDb;
        private readonly LocalPayrollDbContext locaDb;
        private ObservableListSource<Salary> Salaries;
        private string StoreCode = "ARD";

        public SalaryForm()
        {
            InitializeComponent(); azureDb = new AzurePayrollDbContext();
            locaDb = new LocalPayrollDbContext();
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            Salaries = new ObservableListSource<Salary>();
            LoadData();
        }

        private void LoadData()
        {
            azureDb.Employees.Load();

            lbEmployees.DataSource = azureDb.Employees.Local.Select(c => new { c.EmployeeId, c.StaffName, c.StoreId, c.Category }).ToList();
            UpdateSalariesList(azureDb.Salarys.ToList());
            UpdateGridView("");
        }

        private void UpdateSalariesList(List<Salary> sal)
        {
            foreach (var s in sal)
            {
                Salaries.Add(s);
            }
            if (Salaries.Count > 0) Salaries.Distinct();
        }

        private void UpdateGridView(string empId)
        {
            if (string.IsNullOrEmpty(empId))
                dgvSalaries.DataSource = Salaries.Where(s => s.EmployeeId == empId).ToList();
            else dgvSalaries.DataSource = Salaries.ToBindingList();
        }

        private void btnAddSalaryHead_Click(object sender, EventArgs e)
        {
            SalaryEntryForm form = new SalaryEntryForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                //Save/Update
                if (form.IsNew)
                {
                    //add
                    Salaries.Add(form.SavedSalary);
                }
                else
                {
                    //Update

                    Salaries.Add(form.SavedSalary);
                }
                dgvSalaries.DataSource = Salaries.ToBindingList();
            }
            else if (DialogResult == DialogResult.Yes)
            {
                //Delete
                Salaries.Remove(Salaries.Where(c => c.SalaryId == form.DeletedSalaryId).First());
                dgvSalaries.DataSource = Salaries.ToBindingList();
            }
        }

        private void lbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lbEmployees_DoubleClick(object sender, EventArgs e)
        {
            var x = ((System.Windows.Forms.ListBox)sender);
            UpdateGridView(x.SelectedValue.ToString());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Salaries.Clear();
            LoadData();
        }

        private void dgvSalaries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SalaryEntryForm form = new SalaryEntryForm((Salary)dgvSalaries.CurrentRow.DataBoundItem);
            form.SalaryForm = this;
            if (form.ShowDialog() == DialogResult.OK)
            {
                //Save/Update
                if (form.IsNew)
                {
                    //add
                    Salaries.Add(form.SavedSalary);
                }
                else
                {
                    //Update

                    Salaries.Add(form.SavedSalary);
                }
                dgvSalaries.DataSource = Salaries.ToBindingList();
            }
            else if (DialogResult == DialogResult.Yes)
            {
                //Delete
                Salaries.Remove(Salaries.Where(c => c.SalaryId == form.DeletedSalaryId).First());
                dgvSalaries.DataSource = Salaries.ToBindingList();
            }
        }
    }
}