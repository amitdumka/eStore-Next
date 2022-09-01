using AKS.Auths;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Ops;

namespace AKS.Payroll
{
    public partial class LoginForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private Form MainForm;

        public LoginForm(Form main)
        {
            InitializeComponent();
            MainForm = main;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Do Login from Database
        /// </summary>
        /// <param name="u"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool DoLoin(string u, string p)
        {

            return Auth.DoLogin(u, p, azureDb);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (InternetStatus.IsConnectedToInternet())
            {
                if (DoLoin(tbUserName.Text.Trim() + lbDomain.Text, tbPassword.Text.Trim()))
                {
                    MainForm.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("User name/Password does not match!");
                }

            }
            else
            {
                MessageBox.Show("Kindly check Internet Connection! before trying again!.");
            }
        }
    }
}