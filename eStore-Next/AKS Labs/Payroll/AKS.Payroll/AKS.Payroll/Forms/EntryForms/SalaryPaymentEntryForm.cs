using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Ops;
using AKS.Shared.Payroll.Models;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class SalaryPaymentEntryForm : Form
    {
        public SalaryPaymentForm ParentForm = null;

        public bool IsDeleted;

        private SalaryPayment newPayment;

        public SalaryPayment SavedPayment;
        public string DeletedPayment;
        public string EmployeeName;
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private bool IsNew { get; set; }

        private void LaxyInit()
        {
            azureDb = new AzurePayrollDbContext();
            if (IsNew)
                newPayment = new SalaryPayment
                {
                    EntryStatus = EntryStatus.Added,
                    IsReadOnly = false,
                    OnDate = DateTime.Now,
                    EmployeeId = "",
                    StoreId = "ARD",// TODO: read from Global Variable/session
                    MarkedDeleted = false,
                    UserId=CurrentSession.UserName,
                    Amount = 0,
                    Details = "",
                    PayMode = PayMode.Cash,
                    SalaryComponet = SalaryComponet.NetSalary,
                    SalaryMonth = (DateTime.Today.Year * 100) + DateTime.Today.Month,
                };
            LoadData();
        }

        public SalaryPaymentEntryForm(SalaryPayment payment)
        {
            InitializeComponent();
            IsNew = false;
            newPayment = payment;
            newPayment.StoreId = payment.StoreId;
            btnAdd.Text = "Edit";
        }

        public SalaryPaymentEntryForm()
        {
            InitializeComponent();
            IsNew = true;
        }

        private void SalaryPaymentEntryForm_Load(object sender, EventArgs e)
        {
            LaxyInit();
        }

        private void LoadData()
        {
            var empList = azureDb.Employees.Where(c => c.IsWorking).Select(c => new { c.EmployeeId, c.StaffName, c.IsTailors }).ToList();
            cbxEmployee.DataSource = empList;
            cbxEmployee.DisplayMember = "StaffName";
            cbxEmployee.ValueMember = "EmployeeId";

            var sl = new Dictionary<string, string>
            {
                { "ARD", "Aprajita Retails, Dumka" },
                { "ARJ", "Aprajita Retails, Jamshedpur" }
            };

            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PayMode)));
            cbxSalaryComponent.Items.AddRange(Enum.GetNames(typeof(SalaryComponet)));

            cbxStore.DataSource = sl.ToList();
            cbxStore.DisplayMember = "Value";
            cbxStore.ValueMember = "Key";


            DefaultValue();
        }

        private void ClearFiled()
        {
            DefaultValue();
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
                if (SavePayment(ReadPaymentFormData()))
                {
                    ClearFiled();
                    IsNew = false;
                    MessageBox.Show("Payment is saved", "Alert");
                    btnAdd.Text = "Add";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Some error occured while saving payment, Kindly Try again", "Error");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //TODO: ask for confirmation for delete.
            var confirmResult = MessageBox.Show("Are you sure to delete this Salary payemnt ??",
                                      "Confirm Delete!!",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (newPayment.OnDate.Date > new DateTime(2022, 3, 31))
                {
                    azureDb.SalaryPayment.Remove(newPayment);
                    if (azureDb.SaveChanges() > 0)
                    {
                        MessageBox.Show("Salary payment is deleted!", "Delete");
                        this.DeletedPayment = newPayment.SalaryPaymentId;
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else MessageBox.Show("Faild to delete, please try again!", "Delete");
                }
                else
                {
                    MessageBox.Show("Salary Payment before 1st April 2022 cannot be deleted. You don't have accessed", "Access Denied");
                }

            }

        }

        public void DefaultValue()
        {
            if (!String.IsNullOrEmpty(newPayment.EmployeeId)) cbxEmployee.SelectedValue = newPayment.EmployeeId;
            txtDetails.Text = newPayment.Details;
            txtSalaryMonth.Text = newPayment.SalaryMonth.ToString();
            cbxPaymentMode.SelectedIndex = (int)newPayment.PayMode;
            cbxStore.SelectedValue = newPayment.StoreId;
            cbxSalaryComponent.SelectedIndex = (int)SalaryComponet.NetSalary;
            txtAmount.Text = newPayment.Amount.ToString();


        }

        private SalaryPayment ReadPaymentFormData()
        {
            newPayment.OnDate = dtpOnDate.Value;
            newPayment.StoreId = (string)cbxStore.SelectedValue;
            newPayment.Details = txtDetails.Text;
            newPayment.EmployeeId = (string)cbxEmployee.SelectedValue;
            newPayment.Amount = decimal.Parse(txtAmount.Text.Trim());
            if (IsNew) newPayment.EntryStatus = EntryStatus.Added;
            else newPayment.EntryStatus = EntryStatus.Updated;
            newPayment.OnDate = dtpOnDate.Value;
            newPayment.PayMode = (PayMode)cbxPaymentMode.SelectedIndex;
            newPayment.PayMode = (PayMode)cbxPaymentMode.SelectedIndex;
            newPayment.SalaryMonth = Int32.Parse(txtSalaryMonth.Text.Trim());
            newPayment.SalaryComponet = (SalaryComponet)cbxSalaryComponent.SelectedIndex;

            return newPayment;
        }

        private bool SavePayment(SalaryPayment sp)
        {
            try
            {
                if (sp.OnDate.Date > new DateTime(2022, 3, 31))
                {
                    sp.UserId = "WinFormUI";
                    if (IsNew)
                    {
                        int count = azureDb.SalaryPayment.Where(c => c.StoreId == sp.StoreId
                         && c.OnDate.Date == sp.OnDate.Date).Count();
                        sp.EntryStatus = EntryStatus.Added;
                        sp.SalaryPaymentId = IdentityGenerator.GenerateSalaryPayment(sp.OnDate, sp.StoreId, count);
                        azureDb.SalaryPayment.Add(sp);
                    }
                    else
                    {
                        sp.EntryStatus = EntryStatus.Updated;
                        azureDb.SalaryPayment.Update(sp);
                    }

                    int x = azureDb.SaveChanges();
                    if (x > 0)
                    {
                        SavedPayment = sp;
                        EmployeeName = cbxEmployee.Text;
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
                    MessageBox.Show($"Attendance for Current Employee for {sp.OnDate} is already stored", "Error");

                return false;
            }
        }
    }
}