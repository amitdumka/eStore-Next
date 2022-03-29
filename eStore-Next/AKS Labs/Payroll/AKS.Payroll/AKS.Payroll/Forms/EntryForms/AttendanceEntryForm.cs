using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;
using System.Data;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class AttendanceEntryForm : Form
    {
        private List<string> storeList; private Attendance newAtt;

        private PayrollDbContext db;

        public AttendanceEntryForm()
        {
            InitializeComponent();
            db = new PayrollDbContext();

            newAtt = new Attendance
            {
                EntryStatus = EntryStatus.Added,
                EntryTime = "AM/PM",
                IsReadOnly = false,
                OnDate = DateTime.Now,
                EmployeeId = "",
                AttendanceId = 0,
                EmpId = 0,
                Status = AttUnit.Present,
                IsTailoring = false,
                Remarks = "....",
                StoreId = 1,
                StoreCode = "1",
                UserId = "WinUI"
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearFiled();
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Save")
            {
                ClearFiled();
            }
        }

        private void ClearFiled()
        {
        }

        private void AttendanceEntryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var empList = db.Employees.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName, c.IsTailors }).ToList();
            cbxEmployees.DataSource = empList;
            // storeList = new List<string> { "Aprajita Retails, Dumka", "Aprajita Retails, Jamshedpur" };
            var sl = new Dictionary<int, string>();
            sl.Add(1, "Aprajita Retails, Dumka");
            sl.Add(2, "Aprajita Retails, Jamshedpur");

            cbxStatus.Items.AddRange(Enum.GetNames(typeof(AttUnit)));
            cbxStores.DataSource = sl.ToList();
            cbxStores.DisplayMember = "Value";
            cbxStores.ValueMember = "Key";
            DefaultValue();
        }

        public void DefaultValue()
        {
            if (!String.IsNullOrEmpty(newAtt.EmployeeId)) cbxEmployees.SelectedValue = newAtt.EmployeeId;
            txtRemarks.Text = newAtt.Remarks;
            txtEntryTime.Text = newAtt.EntryTime;
            cbxStatus.SelectedIndex = (int)newAtt.Status;
            cbxStores.SelectedValue = newAtt.StoreId;
            cbIsTailors.Checked = newAtt.IsTailoring;
            DisplayData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //MetroForm1 metroForm1 = new MetroForm1();
            //metroForm1.ShowDialog();
            ReadFormData();
            DisplayData();
        }

        private void ReadFormData()
        {
            newAtt.OnDate = dtpOnDate.Value;
            newAtt.Status = (AttUnit)cbxStatus.SelectedIndex;
            newAtt.StoreCode = (string)cbxStores.SelectedText;
            newAtt.Remarks = txtRemarks.Text;
            newAtt.EntryTime = txtEntryTime.Text;
            newAtt.EmployeeId = (string)cbxEmployees.SelectedValue;
            newAtt.IsTailoring = cbIsTailors.Checked;
            newAtt.StoreId = (int)cbxStores.SelectedValue;
        }

        private void DisplayData()
        {
            label8.Text = $"OnDate:{newAtt.OnDate.Date}\t Status:{newAtt.Status}\nRmk:{newAtt.Remarks}\tET:{newAtt.EntryTime}\n Emp:{newAtt.EmployeeId}\n SId:{newAtt.StoreId}\n{newAtt.StoreCode}";
        }
    }
}