using AKS.Shared.Payroll.Models;

namespace AKS.Payroll.Forms.EntryForms
{
    public partial class StaffAdvanceRecieptEntryForm : Form
    {

        public SalaryPaymentForm ParentForm;
        public StaffAdvanceReceipt SaveReciept;
        public string EmployeeName;
        public string DeleteId;
        public StaffAdvanceReceipt newRec;
        public StaffAdvanceRecieptEntryForm()
        {
            InitializeComponent();
        }
        public StaffAdvanceRecieptEntryForm(StaffAdvanceReceipt rec)
        {
            InitializeComponent();
            newRec = rec;
        }
        private void StaffAdvanceRecieptEntryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
