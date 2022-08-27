using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Payroll.Forms.EntryForms;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AKS.Payroll
{
    //TODO: add salary /payment ledger so advance and payment or anyother thing
    public partial class SalaryPaymentForm : Form
    {
        private readonly AzurePayrollDbContext azureDb;
        private readonly LocalPayrollDbContext localDb;
        private ObservableListSource<SalaryPaymentVM> Payments { get; set; }
        private ObservableListSource<StaffAdvanceReceiptVM> Reciepts { get; set; }

        private bool isPayment, isReciepts;

        public SalaryPaymentForm()
        {
            InitializeComponent();
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            isPayment = true;
        }
        public SalaryPaymentForm(bool receipt)
        {
            InitializeComponent();
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            isReciepts = true;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateGridView("", DateTime.Now);
        }

        private void LoadReceiptData()
        {
            Reciepts = new ObservableListSource<StaffAdvanceReceiptVM>();
            isReciepts = true;
            UpdateRecieptList(azureDb.StaffAdvanceReceipt.Include(c => c.Employee).Where(c => c.OnDate.Year == DateTime.Today.Year).ToList());

            dgvReceipts.DataSource = Reciepts.ToBindingList();
            dgvReceipts.Columns["EmployeeId"].Visible = false;
            dgvReceipts.Columns["StoreId"].Visible = false;
            this.tcSalaryPayments.SelectedIndex = 1;
        }

        public void LoadPaymentData()
        {
            Payments = new ObservableListSource<SalaryPaymentVM>();
            UpdateSalaryPaymentList(azureDb.SalaryPayment.Include(c => c.Employee).Where(c => c.OnDate.Year == DateTime.Today.Year
           ).ToList());

            dgvPayments.DataSource = Payments.ToBindingList();
            dgvPayments.Columns["EmployeeId"].Visible = false;
            dgvPayments.Columns["StoreId"].Visible = false;
            isPayment = true;
        }

        private void LoadData()
        {
            azureDb.Employees.Load();
            if (cbAllEmployees.Checked) lbEmoloyees.DataSource = azureDb.Employees.Local.ToBindingList();
            else lbEmoloyees.DataSource = azureDb.Employees.Local.Where(c => c.IsWorking).ToList();
            lbEmoloyees.ValueMember = "EmployeeId";
            lbEmoloyees.DisplayMember = "StaffName";
        }
        private void LoadLedgerData(string empId)
        {
            var sl = new PayrollManager().GetSalaryLedger(azureDb, empId);
            if (sl.Details != null && sl.Details.Any()) dgvSalaryLedger.DataSource = sl.Details;
            else
            {
                MessageBox.Show("No Record Found!!");
            }

        }
        private void UpdateRecieptList(List<StaffAdvanceReceipt> receipts)
        {
            foreach (var rec in receipts)
            {
                Reciepts.Add(DMMapper.Mapper.Map<StaffAdvanceReceiptVM>(rec));
            }
            if (Reciepts != null && Reciepts.Count > 0)
                Reciepts.Distinct();
        }

        private void UpdateSalaryPaymentList(List<SalaryPayment> payments)
        {
            foreach (var pay in payments)
            {
                Payments.Add(DMMapper.Mapper.Map<SalaryPaymentVM>(pay));
            }
            if (Payments != null && Payments.Count > 0)
                Payments.Distinct();
        }

        private void SalaryPaymentForm_Load(object sender, EventArgs e)
        {
            DMMapper.InitializeAutomapper();
            LoadData();
            if (isPayment) LoadPaymentData();
            else if (isReciepts) LoadReceiptData();
        }

        private void lbEmoloyees_DoubleClick(object sender, EventArgs e)
        {
            var x = ((System.Windows.Forms.ListBox)sender);
            if (tcSalaryPayments.SelectedIndex == 0)
                UpdateGridView(x.SelectedValue.ToString(), DateTime.Now);
            else if (tcSalaryPayments.SelectedIndex == 1)
            {
                UpdateReceiptsGridView(x.SelectedValue.ToString(), DateTime.Now);
            }
            else if (tcSalaryPayments.SelectedIndex == 2)
            {
                LoadLedgerData(x.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("No Operation!!");
            }
        }

        private void UpdateGridView(string empId, DateTime date)
        {
            if (string.IsNullOrEmpty(empId))
            {
                dgvPayments.DataSource = Payments.ToBindingList();
            }
            else if (!Payments.Any(c => c.EmployeeId == empId))
            {
                UpdateSalaryPaymentList(azureDb.SalaryPayment.Where(c => c.EmployeeId == empId).ToList());
                dgvPayments.DataSource = Payments.Where(c => c.EmployeeId == empId).ToList();
            }
            else
                dgvPayments.DataSource = Payments.Where(c => c.EmployeeId == empId).ToList();
        }
        private void UpdateReceiptsGridView(string empId, DateTime date)
        {
            if (string.IsNullOrEmpty(empId))
            {
                dgvReceipts.DataSource = Reciepts.ToBindingList();
            }
            else if (!Reciepts.Any(c => c.EmployeeId == empId))
            {
                UpdateRecieptList(azureDb.StaffAdvanceReceipt.Where(c => c.EmployeeId == empId).ToList());
                dgvReceipts.DataSource = Reciepts.Where(c => c.EmployeeId == empId).ToList();
            }
            else
                dgvPayments.DataSource = Payments.Where(c => c.EmployeeId == empId).ToList();
        }
        private void cbAllEmployees_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbAllEmployees.Checked)
                lbEmoloyees.DataSource = azureDb.Employees.Local.ToBindingList();
            else
                lbEmoloyees.DataSource = azureDb.Employees.Local.Where(c => c.IsWorking).ToList();
            lbEmoloyees.Refresh();
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            SalaryPaymentEntryForm x = new();
            x.ParentForm = this;
            if (x.ShowDialog() == DialogResult.OK)
            {
                if (x.SavedPayment != null)
                {
                    var newAttend = DMMapper.Mapper.Map<SalaryPaymentVM>(x.SavedPayment);
                    newAttend.StaffName = x.EmployeeName;

                    if (x.SavedPayment.EntryStatus == EntryStatus.Added)
                        Payments.Add(newAttend);
                    else
                    {
                        Payments.Remove(Payments.Where(c => c.SalaryPaymentId == newAttend.SalaryPaymentId).First());
                        Payments.Add(newAttend);
                    }
                }
                else if (x.DeletedPayment != null)
                {
                    Payments.Remove(Payments.Where(c => c.SalaryPaymentId == x.DeletedPayment).First());
                }
                UpdateGridView("", DateTime.Now);
            }
            else
            {
                MessageBox.Show(DialogResult.ToString(), "else");
            }
        }

        private void tcSalaryPayments_TabIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString() + e.ToString());
        }

        private void tcSalaryPayments_Selected(object sender, TabControlEventArgs e)
        {
            //MessageBox.Show(sender.ToString() + e.ToString());
        }

        private void tcSalaryPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = (TabControl)sender;

            if (x.SelectedIndex == 0)
            {
                if (!isPayment) LoadPaymentData();
            }
            else if (x.SelectedIndex == 1)
            {
                if (!isReciepts) LoadReceiptData();
            }

        }

        private void tcSalaryPayments_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // MessageBox.Show(sender.ToString() + e.ToString());
        }

        private void btnAddReciept_Click(object sender, EventArgs e)
        {
            StaffAdvanceRecieptEntryForm x = new();
            x.ParentForm = this;

            if (x.ShowDialog() == DialogResult.OK)
            {
                if (x.SaveReciept != null)
                {
                    var newRecpt = DMMapper.Mapper.Map<StaffAdvanceReceiptVM>(x.SaveReciept);
                    newRecpt.StaffName = x.EmployeeName;

                    if (x.SaveReciept.EntryStatus == EntryStatus.Added)
                        Reciepts.Add(newRecpt);
                    else
                    {
                        Reciepts.Remove(Reciepts.Where(c => c.StaffAdvanceReceiptId == newRecpt.StaffAdvanceReceiptId).First());
                        Reciepts.Add(newRecpt);
                    }
                }
                else if (x.DeleteId != null)
                {
                    Reciepts.Remove(Reciepts.Where(c => c.StaffAdvanceReceiptId == x.DeleteId).First());
                }
                UpdateReceiptsGridView("", DateTime.Now);
            }
            else
            {

            }
        }

        private void dgvReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = DMMapper.Mapper.Map<StaffAdvanceReceipt>(dgvReceipts.CurrentRow.DataBoundItem);

            var x = new StaffAdvanceRecieptEntryForm(a)
            {
                ParentForm = this
            };

            if (x.ShowDialog() == DialogResult.OK)
            {
                if (x.SaveReciept != null)
                {
                    var newRec = DMMapper.Mapper.Map<StaffAdvanceReceiptVM>(x.SaveReciept);
                    newRec.StaffName = x.EmployeeName;

                    if (x.SaveReciept.EntryStatus == EntryStatus.Added)
                        Reciepts.Add(newRec);
                    else
                    {
                        Reciepts.Remove(Reciepts.Where(c => c.StaffAdvanceReceiptId == newRec.StaffAdvanceReceiptId).First());
                        Reciepts.Add(newRec);
                    }
                }
                else if (x.DeleteId != null)
                {
                    Reciepts.Remove(Reciepts.Where(c => c.StaffAdvanceReceiptId == x.DeleteId).First());
                }
                UpdateReceiptsGridView("", DateTime.Now);
            }
            else if (x.DialogResult == DialogResult.Yes)
            {
                if (x.DeleteId != null)
                {
                    Reciepts.Remove(Reciepts.Where(c => c.StaffAdvanceReceiptId == x.DeleteId).First());
                }
                UpdateReceiptsGridView("", DateTime.Now);

            }
            else
            {
                MessageBox.Show(DialogResult.ToString());
            }
        }

        private void lbEmoloyees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnProcessLedger_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbEmoloyees.SelectedValue.ToString()))
            {


                if (PayrollBulkProcessor.ProcessSalaryLedger(azureDb, lbEmoloyees.SelectedValue.ToString())) MessageBox.Show("Success");
                else MessageBox.Show("Error occured while processing");
            }
            else
            {
                MessageBox.Show("Select an employee to process!");
            }


        }

        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = DMMapper.Mapper.Map<SalaryPayment>(dgvPayments.CurrentRow.DataBoundItem);

            var x = new SalaryPaymentEntryForm(a)
            {
                ParentForm = this
            };

            if (x.ShowDialog() == DialogResult.OK)
            {
                if (x.SavedPayment != null)
                {
                    var newAttend = DMMapper.Mapper.Map<SalaryPaymentVM>(x.SavedPayment);
                    newAttend.StaffName = x.EmployeeName;

                    if (x.SavedPayment.EntryStatus == EntryStatus.Added)
                        Payments.Add(newAttend);
                    else
                    {
                        Payments.Remove(Payments.Where(c => c.SalaryPaymentId == newAttend.SalaryPaymentId).First());
                        Payments.Add(newAttend);
                    }
                }
                else if (x.DeletedPayment != null)
                {
                    Payments.Remove(Payments.Where(c => c.SalaryPaymentId == x.DeletedPayment).First());
                }
                UpdateGridView("", DateTime.Now);
            }
            else if (x.DialogResult == DialogResult.Yes)
            {
                if (x.DeletedPayment != null)
                {
                    Payments.Remove(Payments.Where(c => c.SalaryPaymentId == x.DeletedPayment).First());
                }
                UpdateGridView("", DateTime.Now);

            }
            else
            {
                MessageBox.Show(DialogResult.ToString());
            }
        }
    }
}