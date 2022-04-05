using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Payroll.Forms.EntryForms;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class SalaryForm : Form
    {
        private readonly AzurePayrollDbContext azureDb;
        private readonly LocalPayrollDbContext locaDb;
        private ObservableListSource<SalaryVM> Salaries;
        private string StoreCode = "ARD";

        public SalaryForm()
        {
            InitializeComponent(); azureDb = new AzurePayrollDbContext();
            locaDb = new LocalPayrollDbContext();
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            Salaries = new ObservableListSource<SalaryVM>();
            DMMapper.InitializeAutomapper();
            LoadData();
        }

        private void LoadData()
        {
            azureDb.Employees.Load();

            lbEmployees.DataSource = azureDb.Employees.Local.Select(c => new { c.EmployeeId, c.StaffName, c.StoreId, c.Category }).OrderBy(c => c.StaffName).ToList();
            lbEmployees.DisplayMember = "StaffName";
            lbEmployees.ValueMember = "EmployeeId";
            UpdateSalariesList(azureDb.Salaries.Include(c => c.Employee).ToList());
            UpdateGridView("");
        }

        private void UpdateSalariesList(List<Salary> sal)
        {
            foreach (var s in sal)
            {
                Salaries.Add(DMMapper.Mapper.Map<SalaryVM>(s));
            }
            if (Salaries.Count > 0) Salaries.Distinct();
        }

        private void UpdateGridView(string empId)
        {
            if (!string.IsNullOrEmpty(empId))
                dgvSalaries.DataSource = Salaries.Where(s => s.EmployeeId == empId).ToList();
            else dgvSalaries.DataSource = Salaries.ToBindingList();
        }

        private void btnAddSalaryHead_Click(object sender, EventArgs e)
        {
            SalaryEntryForm form = new SalaryEntryForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                //Save/Update
                if (!form.IsNew)
                {
                    Salaries.Remove(Salaries.Where(c => c.SalaryId == form.newSalary.SalaryId).First());
                }
                Salaries.Add(DMMapper.Mapper.Map<SalaryVM>(form.SavedSalary));
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
            var sal = DMMapper.Mapper.Map<Salary>(dgvSalaries.CurrentRow.DataBoundItem);
            sal.Employee = null;
            SalaryEntryForm form = new SalaryEntryForm(sal);
            form.SalaryForm = this;
            if (form.ShowDialog() == DialogResult.OK)
            {
                //Save/Update
                if (!form.IsNew)
                {
                    Salaries.Remove(Salaries.Where(c => c.SalaryId == form.newSalary.SalaryId).First());
                }
                Salaries.Add(DMMapper.Mapper.Map<SalaryVM>(form.SavedSalary));
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