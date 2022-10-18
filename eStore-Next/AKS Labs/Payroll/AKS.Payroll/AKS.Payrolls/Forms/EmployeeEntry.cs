using AKS.Payroll.Database;
using AKS.Shared.Payroll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payrolls.Forms
{
    public partial class EmployeeEntry : Form
    {
        AzurePayrollDbContext db;
        public EmployeeEntry()
        {
            InitializeComponent();
            db = new AzurePayrollDbContext();
            CBXStore.DataSource = db.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            CBXCategory.DataSource = Enum.GetNames((typeof(EmpType))).ToList();
            CBXGender.DataSource = Enum.GetNames((typeof(Gender))).ToList();
            CBXStore.DisplayMember = "StoreName";
            CBXStore.ValueMember = "StoreId";
        }

        private void BTNAdd_Click(object sender, EventArgs e)
        {
            if (BTNAdd.Text == "Add")
            {
                BTNAdd.Text = "Save";
            }
            else if (BTNAdd.Text == "Edit")
            {

            }
            else if (BTNAdd.Text == "Save")
            { var x = Read();
                db.Employees.Add(x);
                if (db.SaveChanges() > 0)
                {

                    if (CBDetails.Checked)
                    {
                        db.EmployeeDetails.Add(ReadDetails(x.EmployeeId));
                        if (db.SaveChanges() > 0)
                        {
                            MessageBox.Show("Saved");
                            BTNAdd.Text = "Add";
                        }
                        else
                        {
                            db.EmployeeDetails.Local.Clear();
                            db.Employees.Local.Clear();
                            MessageBox.Show("Failed to save");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Saved");
                        BTNAdd.Text = "Add";
                    }
                   
                }
                else
                {
                    db.Employees.Local.Clear();
                    MessageBox.Show("Failed to save");
                }
            }
        }

        EmployeeDetails ReadDetails(string id)
        {
            EmployeeDetails details = new EmployeeDetails
            {
                AdharNumber = TXTAadhar.Text, EmployeeId=id, MarkedDeleted=false, OtherIdDetails=TXTOther.Text, PanNo=TXTPAn.Text,
                BankAccountNumber = TXTAccountNo.Text,
                BankNameWithBranch = TXTBAnkNAme.Text,
                DateOfBirth = DTPDOB.Value,
                EntryStatus = EntryStatus.Added,
                FatherName = TXTFatherName.Text, SpouseName=TXTSpouseName.Text,
                HighestQualification = TXTQuali.Text,
                IFSCode = TXTIFCSCode.Text,
                MaritalStatus = TXTMartialStatus.Text,
                IsReadOnly = false,
                StoreId = CBXStore.SelectedValue.ToString(),
                UserId = "Auto"

            };
            return details;
        }
        Employee Read()
        {
            int count = db.Employees.Count();
            Employee emp = new Employee
            {
                EmpId = count,
                AddressLine = TXTAddressLine.Text,
                Category = (EmpType)CBXCategory.SelectedIndex,
                City = TXTCity.Text,
                Country = TXTCountry.Text,
                DOB = DTPDOB.Value,
                FirstName = TXTFirstName.Text,
                LastName = TXTLastName.Text,
                Gender = (Gender)CBXGender.SelectedIndex,
                IsTailors = CBTailor.Checked,
                IsWorking = CBWorking.Checked,
                JoiningDate = DTPJoining.Value,
                ZipCode = TXTZipCode.Text,
                MarkedDeleted = false,
                State = TXTState.Text,
                StoreId = CBXStore.SelectedValue.ToString(),
                Title = "MR.",
                StreetName = TXTStreetName.Text,
                LeavingDate = CBWorking.Checked ? null : DTPLeaving.Value
            };

            string cat = "";
            switch (emp.Category)
            {
                case EmpType.Salesman:
                    cat = ("SLM");
                    break;

                case EmpType.StoreManager:
                    cat = ("SM");
                    break;

                case EmpType.HouseKeeping:
                    cat = ("HK");
                    break;

                case EmpType.Owner:
                    cat = ("OWN");
                    break;

                case EmpType.Accounts:
                    cat = ("ACC");
                    break;

                case EmpType.TailorMaster:
                    cat = ("TM");
                    break;

                case EmpType.Tailors:
                    cat = ("TLS");
                    break;

                case EmpType.TailoringAssistance:
                    cat = ("TLA");
                    break;

                case EmpType.Others:
                    cat = ("OTH");
                    break;

                default:
                    break;
            }
            emp.EmployeeId = $@"{emp.StoreId}/{DTPJoining.Value.Year}/{cat}/{count}";

            return emp;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXTMartialStatus_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
