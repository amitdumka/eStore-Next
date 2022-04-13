namespace AKS.Payroll.Forms.Banking
{
    public partial class BankAccountEntryForm : Form
    {
        private readonly int _FormMode;

        public BankAccountEntryForm(int mode)
        {
            InitializeComponent();
            _FormMode = mode;
        }

        public BankAccountEntryForm()
        {
            InitializeComponent();
        }

        private void EnableFormMode()
        {
            switch (_FormMode)
            {
                case 1: // Bank Account Mode;
                    rbCompanyAccount.Checked = true;
                    break;

                case 2: //Account List mode
                    rbThirdPartyAccount.Checked = true;
                    break;

                case 3: //Vendor Mode
                    rbVendorAccount.Checked = true;
                    break;

                default:
                    rbCompanyAccount.Checked = false;
                    rbVendorAccount.Checked = false;
                    rbThirdPartyAccount.Checked = false;
                    break;
            }
        }

        private void BankAccountEntryForm_Load(object sender, EventArgs e)
        {
            EnableFormMode();
        }

        private void FormElementView()
        {
            switch (_FormMode)
            {
                case 1:
                case 2:
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}