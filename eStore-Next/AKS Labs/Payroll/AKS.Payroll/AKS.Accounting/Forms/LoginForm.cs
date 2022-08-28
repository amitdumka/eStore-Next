
using AKS.Auths;
using AKS.Payroll.Database;
using AKS.Shared.Commons.Ops;

namespace AKS.Accounting.Forms
{
    public partial class LoginForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;


        public LoginForm()
        {
            InitializeComponent();
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
            //var f = azureDb.Users.Where(c => c.UserName == u && c.Password == p && c.Enabled).FirstOrDefault();
            //if (f != null)
            //{
            //    CurrentSession.StoreName = "Aprajita Retails";
            //    CurrentSession.StoreCode = "ARD";
            //    CurrentSession.UserName = u;
            //    CurrentSession.GuestName = f.GuestName;
            //    CurrentSession.UserType = f.UserType;
            //    CurrentSession.LoggedTime=DateTime.Now;

            //    return true;
            //}
            //return false;
            return Auth.DoLogin(u, p, azureDb);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (InternetStatus.IsConnectedToInternet())
            {
                if (DoLoin(tbUserName.Text.Trim() + lbDomain.Text, tbPassword.Text.Trim()))
                {
                    new MainForm().Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Username/Password does not match!");
                }

            }
            else
            {
                MessageBox.Show("Kindly check Internet Connection! before trying again!.");
            }
        }
    }
}