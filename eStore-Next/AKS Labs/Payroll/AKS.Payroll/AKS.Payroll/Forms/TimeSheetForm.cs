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
    public partial class TimeSheetForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private ObservableListSource<TimeSheet> ItemList;
        private TimeSheet timeSheet;

        public TimeSheetForm()
        {
            InitializeComponent();
            // timeSheet= new TimeSheet();
            ItemList = new ObservableListSource<TimeSheet>();
        }

        private void LoadData()
        {
            
            cbxStores.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            cbxStores.ValueMember = "StoreId";
            cbxStores.DisplayMember = "StoreName";
            cbxEmployees.ValueMember = "EmployeeId";
            cbxEmployees.DisplayMember = "StaffName"; 
            var empList = azureDb.Employees.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName, c.StoreId }).ToList();
            cbxEmployees.DataSource = empList;
            lbEmployees.DataSource = empList;
            dtpTime.MaxDate = DateTime.Now.AddHours(1);
            ItemUpdate(azureDb.TimeSheets.Include(c=>c.Employee).Where(c => c.OutTime.Date == DateTime.Today.Date).ToList());
            dgvTimeSheet.DataSource = ItemList.ToBindingList();  ;
            dgvTimeSheet.Columns["IsReadOnly"].Visible = false;
            dgvTimeSheet.Columns["UserId"].Visible = false;
            dgvTimeSheet.Columns["MarkedDeleted"].Visible = false;
            dgvTimeSheet.Columns["Employee"].Visible=false;
            dtpTime.Format = DateTimePickerFormat.Time;


        }

        private void ItemUpdate(List<TimeSheet> ts)
        {
            foreach (var item in ts)
            {
                ItemList.Add(item); 
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                this.tabControl1.SelectedIndex = 1;
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Save")
            {
                if (!Read())
                {


                }
                else
                {


                    if (Save())
                    {
                        MessageBox.Show("Time Sheet Added/Update!");
                        if (rbIn.Checked)
                        {
                            ItemList.Remove(ItemList.Where(c => c.Id == timeSheet.Id).FirstOrDefault());
                        }
                        ItemList.Add(timeSheet);
                        dgvTimeSheet.Refresh();
                        this.tabControl1.SelectedIndex = 0;
                        btnAdd.Text = "Add";
                    }
                    else
                    {
                        azureDb.TimeSheets.Remove(timeSheet);
                    }
                }
            }
        }

        private bool Read()
        {


            if (rbIn.Checked)
            {
                var ts = azureDb.TimeSheets.Where(c => c.EmployeeId == (string)cbxEmployees.SelectedValue &&
                            c.OutTime.Date == DateTime.Today.Date && c.InTime == null).FirstOrDefault();
                if (ts != null)
                {
                    timeSheet = ts;
                    timeSheet.InTime = dtpTime.Value;
                    timeSheet.Reason += "\t In: \t " + txtReason.Text;
                    timeSheet.IsReadOnly = true;
                    azureDb.TimeSheets.Update(timeSheet);


                }
                else
                {
                    MessageBox.Show("No Out is entery made, Kindly check before entring!");
                    return false;

                }
                


            }
            else if (rbOut.Checked)
            {
                timeSheet = new()
                {
                    EmployeeId = (string)cbxEmployees.SelectedValue,
                    InTime = null,
                    OutTime = dtpTime.Value,
                    IsReadOnly = false,
                    MarkedDeleted = false,
                    Reason = txtReason.Text,
                    UserId = "WinUI",
                    Id = $"ARD/{(string)cbxEmployees.SelectedValue}/{DateTime.Today.Year}/{DateTime.Today.Month}/{DateTime.Today.Day}/{new Random(DateTime.Now.Millisecond).NextInt64()}"

                };
                azureDb.TimeSheets.Add(timeSheet);
            }

            return true;

        }
        private bool Save()
        {
            return azureDb.SaveChanges() > 0;

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add";
            this.tabControl1.SelectedIndex = 0; 

        }

        private void lbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTimeSheet.DataSource = ItemList.Where(c => c.EmployeeId == (string)lbEmployees.SelectedValue).ToList();
        }

        private void TimeSheetForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext(); 
            LoadData();
        }
    }
}
