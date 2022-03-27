using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payroll.Forms
{
    public partial class AttendanceForm : Form
    {
        public AttendanceForm()
        {
            InitializeComponent();
        }
        private void AttendanceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Dispose();
        }
        private PayrollDbContext context;
        private ObservableListSource<Attendance> Attendances;
        private ObservableListSource<string> EmpIDs;
        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            context = new PayrollDbContext();
            context.Employees.Load();
            lbEmployees.DataSource = context.Employees.Local.ToBindingList();
            Attendances = new ObservableListSource<Attendance>();
            AddToList( context.Attendances.Where(c=>c.EmpId==1).ToList());
            dgvAttendances.DataSource = Attendances.ToBindingList();
            tSSLCountValue.Text = dgvAttendances.Rows.Count.ToString();
        }
        private void UpdateGridView(string empId)
        {
            AddToList(context.Attendances.Where(c => c.EmployeeId == empId).ToList());
            dgvAttendances.DataBindings.Clear();
            dgvAttendances.DataSource= Attendances.Where(c=>c.EmployeeId==empId).ToList();
           tSSLCountValue.Text= dgvAttendances.Rows.Count.ToString();
        }
        private void AddToList(List<Attendance> attds)
        {
            foreach (var att in attds)
            {
                Attendances.Add(att);
            }
        }

        //private void lbEmployees_Click(object sender, EventArgs e)
        //{
        //  //  MessageBox.Show(sender.ToString(), "Click");
        //}

        private void lbEmployees_DoubleClick(object sender, EventArgs e)
        {
            
            var x = ((System.Windows.Forms.ListBox)sender); UpdateGridView(x.SelectedValue.ToString());
           // MessageBox.Show(x.SelectedValue.ToString() + "\n" + ((Employee)x.SelectedItem).StaffName, $"Employee Name: {((Employee)x.SelectedItem).StaffName}");

        }

        //private void lbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}
    }
}
