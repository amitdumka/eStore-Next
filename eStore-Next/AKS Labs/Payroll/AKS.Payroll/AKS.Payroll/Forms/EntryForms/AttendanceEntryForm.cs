using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;
using System.Data;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class AttendanceEntryForm : Form
    {
        private Attendance newAtt;

        private PayrollDbContext db;
        private bool isNew { get; set; }

        private void LaxyInit()
        {
           // db = new PayrollDbContext();
           //if(isNew)
           // newAtt = new Attendance
           // {
           //     EntryStatus = EntryStatus.Added,
           //     EntryTime = "AM/PM",
           //     IsReadOnly = false,
           //     OnDate = DateTime.Now,
           //     EmployeeId = "",
           //     AttendanceId = 0,
           //     EmpId = 0,
           //     Status = AttUnit.Present,
           //     IsTailoring = false,
           //     Remarks = "....",
           //     StoreId = 1,
           //     StoreCode = "1",
           //     UserId = "WinUI"
           // };
           // LoadData();
        }
        public AttendanceEntryForm(Attendance att)
        {
            InitializeComponent();
            //isNew = false;
            //newAtt = att;
            //newAtt.StoreId= Int16.Parse(att.StoreCode.Trim());
            //btnAdd.Text = "Edit";
        }
        public AttendanceEntryForm()
        {
            InitializeComponent();
            isNew = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearFiled();
                isNew = true;
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
                isNew = false;
            }
            else if (btnAdd.Text == "Save")
            {
                if (SaveAttendance(ReadFormData()))
                {
                    ClearFiled();
                    isNew = false;
                    MessageBox.Show("Attendance is saved");
                    btnAdd.Text = "Add";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Some error occured while saving attednace, Kindly Try again");
                }
            }
        }

        private bool SaveAttendance(Attendance att)
        {
            att.UserId = "WinFormUI";
            if (isNew)
                db.Attendances.Add(att);
            else db.Attendances.Update(att);

            return db.SaveChanges() > 0;
        }

        private void ClearFiled()
        {
            DefaultValue();
        }

        private void AttendanceEntryForm_Load(object sender, EventArgs e)
        {
            LaxyInit();
        }

        private void LoadData()
        {
            var empList = db.Employees.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName, c.IsTailors }).ToList();
            cbxEmployees.DataSource = empList;

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
            //TODO: ask for confirmation for delete.
            var confirmResult = MessageBox.Show("Are you sure to delete this Attedance ??",
                                      "Confirm Delete!!",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                db.Attendances.Remove(newAtt);
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Attendance is deleted!", "Delete");
                    var xy = ((AttendanceForm)this.ParentForm.MdiChildren.Where(c => c.Text == "Attednance Entry").First());
                    xy.UpdateRecord("Empid", 1, 1);
                    this.Close();
                }
                else MessageBox.Show("Faild to delete, please try again!", "Delete");
            }
            else
            {
                MessageBox.Show("Not Deleted");
            }


        }

        private Attendance ReadFormData()
        {
            newAtt.OnDate = dtpOnDate.Value;
            newAtt.Status = (AttUnit)cbxStatus.SelectedIndex;
            newAtt.StoreId = (string)cbxStores.SelectedText;
            newAtt.Remarks = txtRemarks.Text;
            newAtt.EntryTime = txtEntryTime.Text;
            newAtt.EmployeeId = (string)cbxEmployees.SelectedValue;
            newAtt.IsTailoring = cbIsTailors.Checked;
         
            return newAtt;
        }

        private void DisplayData()
        {
            label8.Text = $"OnDate:{newAtt.OnDate.Date}\t Status:{newAtt.Status}\nRmk:{newAtt.Remarks}\tET:{newAtt.EntryTime}\n Emp:{newAtt.EmployeeId}\n SId:{newAtt.StoreId}\n";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFiled();
        }
    }
}