using AKS.Payroll.Ops;

namespace AKS.Payroll
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (InternetStatus.IsConnectedToInternet())
            {
                new MainForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kindly check Internet Connection! before trying again!.");
            }
        }
    }
}