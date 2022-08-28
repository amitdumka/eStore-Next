using AKS.Payroll.Database;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using System.Data;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class SalaryEntryForm : Form
    {
        public SalaryForm SalaryForm;
        public Salary newSalary, SavedSalary;
        public string DeletedSalaryId;
        public bool IsNew;
        public bool IsDeleted;

        private readonly AzurePayrollDbContext azureDb;
        private readonly LocalPayrollDbContext localDB;

        public SalaryEntryForm()
        {
            InitializeComponent();
            IsNew = true;
            azureDb = new AzurePayrollDbContext();
            localDB = new LocalPayrollDbContext();
            newSalary = new Salary
            {
                LastPcs = false,
                SundayBillable = false,
                FullMonth = false,
                IsTailoring = false,
                EmployeeId = "",
                Incentive = false,
                BasicSalary = 0,
                CloseDate = null,
                EffectiveDate = DateTime.Today,
                EntryStatus = EntryStatus.Added,
                IsEffective = false,
                IsReadOnly = false,
                MarkedDeleted = false,
                SalaryId = "",
                StoreId = "ARD",
                UserId=CurrentSession.UserName,
                WowBill = false
            };
        }

        public SalaryEntryForm(Salary sal)
        {
            InitializeComponent();
            IsNew = false;
            newSalary = sal;
            btnAdd.Text = "Edit";
            azureDb = new AzurePayrollDbContext();
            localDB = new LocalPayrollDbContext();
        }

        private bool SaveRecord(Salary sal)
        {
            if (sal != null)
            {
                if (!IsNew)
                {
                    sal.EntryStatus = EntryStatus.Updated;

                    azureDb.Salaries.Update(sal);
                }
                else
                {
                    sal.SalaryId = $"{sal.EmployeeId}/{1 + azureDb.Salaries.Where(c => c.EmployeeId == sal.EmployeeId).Count()}";
                    azureDb.Salaries.Add(sal);
                }

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
                CloseDate = cbIsEffective.Checked ? null : dtpEndDate.Value,
                EmployeeId = (string)cbxEmployees.SelectedValue,
                EntryStatus = EntryStatus.Added,// newSalary.EntryStatus,
                MarkedDeleted = false,
                UserId=CurrentSession.UserName,
                IsEffective = cbIsEffective.Checked,

                LastPcs = this.clbOptions.GetItemChecked(1),
                FullMonth = this.clbOptions.GetItemChecked(4),

                Incentive = this.clbOptions.GetItemChecked(2),
                IsTailoring = this.clbOptions.GetItemChecked(3),
                SundayBillable = this.clbOptions.GetItemChecked(5),
                WowBill = this.clbOptions.GetItemChecked(0),

                StoreId = "ARD",// newSalary.StoreId,
                IsReadOnly = false,
                SalaryId = newSalary.SalaryId

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
                    if (IsNew)
                        MessageBox.Show("New Salary Head is saved.");
                    else MessageBox.Show("Salary Head is updated!");
                    SavedSalary = sal;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Not able to save the salary , check data and try again!!!");
                }
            }
        }

        private void UpdateUI()
        {
            cbxEmployees.SelectedValue = newSalary.EmployeeId;
            cbxStores.SelectedValue = newSalary.StoreId;
            this.txtBasicSalary.Text = newSalary.BasicSalary.ToString();
            this.cbIsEffective.Checked = newSalary.IsEffective;
            dtpStartDate.Value = newSalary.EffectiveDate;
            dtpEndDate.Value = newSalary.CloseDate.HasValue ? newSalary.CloseDate.Value : DateTime.Today;
            //newSalary.CloseDate.HasValue?dtpEndDate.Value= newSalary.CloseDate.Value:dtp
            clbOptions.SetItemChecked(0, newSalary.WowBill);
            clbOptions.SetItemChecked(1, newSalary.LastPcs);
            clbOptions.SetItemChecked(2, newSalary.Incentive);
            clbOptions.SetItemChecked(3, newSalary.IsTailoring);
            clbOptions.SetItemChecked(4, newSalary.FullMonth);
            clbOptions.SetItemChecked(5, newSalary.SundayBillable);
        }

        private void SalaryEntryForm_Load(object sender, EventArgs e)
        {

            LoadData();
            UpdateUI();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //TODO: ask for confirmation for delete.
            var confirmResult = MessageBox.Show("Are you sure to delete this Salary Head ??",
                                      "Confirm Delete!!",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                newSalary.Employee = null;
                azureDb.Salaries.Remove(newSalary);
                if (azureDb.SaveChanges() > 0)
                {
                    MessageBox.Show("Salary Head is deleted!", "Delete");
                    DeletedSalaryId = newSalary.SalaryId;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else MessageBox.Show("Faild to delete, please try again!", "Delete");
            }
        }

        private void LoadData()
        {
            var empList = azureDb.Employees.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName, c.IsTailors }).ToList();
            cbxEmployees.DataSource = empList;
            cbxEmployees.ValueMember = "EmployeeId";
            cbxEmployees.DisplayMember = "StaffName";

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