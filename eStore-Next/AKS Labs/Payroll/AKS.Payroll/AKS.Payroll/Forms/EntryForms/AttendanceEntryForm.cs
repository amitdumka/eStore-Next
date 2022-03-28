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
    public partial class AttendanceEntryForm : Form
    {
        private PayrollDbContext db;
        public AttendanceEntryForm()
        {
            InitializeComponent();
            newAtt = new Attendance { EntryStatus=EntryStatus.Added, EntryTime="", IsReadOnly=false, OnDate= DateTime.Now,
             IsTailoring=false, Remarks="Entry ", StoreId=1, StoreCode="1", Status=AttUnit.Present, UserId="WinUI"
            };
            db= new PayrollDbContext();
        }
        private Attendance newAtt;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add") { }
            else if(btnAdd.Text =="Edit"){}
            else if(btnAdd.Text == "Save") { }
        }

        private void ClearFiled()
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void AttendanceEntryForm_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = newAtt;
   
        }
        private void LoadData()
        {
            var empList= db.Employees.Where(c=>c.IsWorking).Select(c=>new {c.EmployeeId,c.StaffName,c.IsTailors}).ToList();
            cbxEmployees.DataSource = empList;
            
        
        }
    }
}
