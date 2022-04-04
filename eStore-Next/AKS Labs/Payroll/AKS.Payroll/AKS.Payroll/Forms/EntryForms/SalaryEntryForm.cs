using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class SalaryEntryForm : Form
    {
        public SalaryForm SalaryForm;
        public Salary newSalary, SavedSalary;
        public string DeletedSalaryId;
        public bool IsNew;
        public bool IsDeleted;

        public AzurePayrollDbContext azureDb;
        public LocalPayrollDbContext localDB;

        public SalaryEntryForm()
        {
            InitializeComponent();
            IsNew = true;
        }
        public SalaryEntryForm(Salary sal)
        {
            InitializeComponent();
            IsNew = false;
            newSalary = sal;
        }
        private bool SaveRecord(Salary sal)
        {
            if (sal != null)
            {
                if (!IsNew)
                {
                    sal.EntryStatus = EntryStatus.Updated;
                    azureDb.Salarys.Update(sal);
                }
                else { azureDb.Salarys.Add(sal); }

                return azureDb.SaveChanges() > 0;

            }
            else 
                return false;

        }
        private Salary ReadFormData()
        {
            return new Salary
            {
                BasicSalary = decimal.Parse(txtBasicSalary.Text.Trim()),
                EffectiveDate = dtpStartDate.Value,
                CloseDate = cbIsEffective.Checked?dtpEndDate.Value:null,
                EmployeeId = (string)cbxEmployees.SelectedValue,
                EntryStatus = newSalary.EntryStatus,
                MarkedDeleted = false,
                UserId = "WinUI",
                IsEffective = cbIsEffective.Checked,

                LastPcs = this.clbOptions.GetItemChecked(1),
                FullMonth = this.clbOptions.GetItemChecked(4),

                Incentive = this.clbOptions.GetItemChecked(2),
                IsTailoring = this.clbOptions.GetItemChecked(3),
                SundayBillable = this.clbOptions.GetItemChecked(5),
                WowBill = this.clbOptions.GetItemChecked(0),

                StoreId = newSalary.StoreId,
                IsReadOnly = false,
                
                //WOW Bill
                //Last Pc Incentive
                //Sale Incentive
                //Tailor
                //Full Month Billable
                //Sunday Billable

            };
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Edit")
            {

                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Save")
            {
                var sal = ReadFormData();

                if (SaveRecord(sal))
                {
                    btnAdd.Text = "Add";
                    MessageBox.Show("New Salary Head is saved.");
                    SavedSalary = sal;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Not able to save the salary , check data and try again!!!");
                }
            }
        }

        private void SalaryEntryForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext(); 
            localDB = new LocalPayrollDbContext();
            LoadData();
        }

        private void LoadData()
        {
            var empList = azureDb.Employees.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName, c.IsTailors }).ToList();
            cbxEmployees.DataSource = empList;

            var sl = new Dictionary<string, string>
            {
                { "ARD", "Aprajita Retails, Dumka" },
                { "ARJ", "Aprajita Retails, Jamshedpur" }
            };

            cbxStores.DataSource = sl.ToList();
            cbxStores.DisplayMember = "Value";
            cbxStores.ValueMember = "Key";
        }
    }
}
