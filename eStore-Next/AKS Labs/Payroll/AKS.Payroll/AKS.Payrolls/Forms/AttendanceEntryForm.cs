using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;
using System.Data;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class AttendanceEntryForm : Form
    {
        private Attendance newAtt;
        public new AttendanceForm ParentForm;
        public Attendance SavedAtt;
        public string DeletedAttednance;
        public string EmployeeName;
        private AzurePayrollDbContext db;
        private bool IsNew { get; set; }

        private void LaxyInit()
        {
            db = new AzurePayrollDbContext();
            if (IsNew)
                newAtt = new Attendance
                {
                    EntryStatus = EntryStatus.Added,
                    EntryTime = "AM/PM",
                    IsReadOnly = false,
                    OnDate = DateTime.Now,
                    EmployeeId = "",
                    AttendanceId = "",
                    Status = AttUnit.Present,
                    IsTailoring = false,
                    Remarks = "",
                    StoreId = "ARD",// TODO: read from Global Variable/session
                    MarkedDeleted = false,
                    UserId = CurrentSession.UserName
                };
            LoadData();
        }
        public AttendanceEntryForm(Attendance att)
        {
            InitializeComponent();
            IsNew = false;
            newAtt = att;
            newAtt.StoreId = att.StoreId;
            btnAdd.Text = "Edit";
        }
        public AttendanceEntryForm()
        {
            InitializeComponent();
            IsNew = true;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearFiled();
                IsNew = true;
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
                IsNew = false;
            }
            else if (btnAdd.Text == "Save")
            {
                if (SaveAttendance(ReadFormData()))
                {
                    ClearFiled();
                    IsNew = false;
                    MessageBox.Show("Attendance is saved", "Alert");
                    btnAdd.Text = "Add";
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Some error occured while saving attednace, Kindly Try again", "Error");
                }
            }
        }

        private bool SaveAttendance(Attendance att)
        {
            try
            {
                if (att.OnDate.Date > new DateTime(2022, 3, 31))
                {
                    att.UserId = "WinFormUI";
                    if (IsNew)
                    {
                        att.EntryStatus = EntryStatus.Added;
                        att.AttendanceId = IdentityGenerator.GenerateAttendanceId(att);
                        db.Attendances.Add(att);
                    }
                    else
                    {


                        att.EntryStatus = EntryStatus.Updated;
                        db.Attendances.Update(att);



                    }

                    int x = db.SaveChanges();
                    if (x > 0)
                    {
                        SavedAtt = att;
                        EmployeeName = cbxEmployees.Text;

                    }
                    return x > 0;
                }
                else
                {
                    MessageBox.Show("You cannot change/add attendance before 1st April 2022", "Accessed denied");
                    return false;
                }


            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.ToLower().Contains("duplicate"))
                    MessageBox.Show($"Attendance for Current Employee for {att.OnDate} is already stored", "Error");

                return false;
            }
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

            var sl = new Dictionary<string, string>
            {
                { "ARD", "Aprajita Retails, Dumka" },
                { "ARJ", "Aprajita Retails, Jamshedpur" }
            };

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

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //  ask for confirmation for delete.
            var confirmResult = MessageBox.Show("Are you sure to delete this Attedance ??",
                                      "Confirm Delete!!",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (newAtt.OnDate.Date > new DateTime(2022, 3, 31))
                {
                    db.Attendances.Remove(newAtt);
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Attendance is deleted!", "Delete");
                        this.DeletedAttednance = newAtt.AttendanceId;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else MessageBox.Show("Failed to delete, please try again!", "Delete");
                }
                else
                {
                    MessageBox.Show("Attendace before 1st April 2022 cannot be deleted. You don't have accessed", "Access Denied");
                }

            }



        }

        private Attendance ReadFormData()
        {
            newAtt.OnDate = dtpOnDate.Value;
            newAtt.Status = (AttUnit)cbxStatus.SelectedIndex;
            newAtt.StoreId = (string)cbxStores.SelectedValue;
            newAtt.Remarks = txtRemarks.Text;
            newAtt.EntryTime = txtEntryTime.Text;
            newAtt.EmployeeId = (string)cbxEmployees.SelectedValue;
            newAtt.IsTailoring = cbIsTailors.Checked;
            newAtt.UserId = CurrentSession.UserName;

            return newAtt;
        }

        //private void DisplayData()
        //{
        //    label8.Text = $"OnDate:{newAtt.OnDate.Date}\t Status:{newAtt.Status}\nRmk:{newAtt.Remarks}\tET:{newAtt.EntryTime}\n Emp:{newAtt.EmployeeId}\n SId:{newAtt.StoreId}\n";
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFiled();
        }
    }
}