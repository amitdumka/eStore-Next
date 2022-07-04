using AKS.Libs.Widgets;
using AKS.ParyollSystem;
using AKS.Payroll.Database;

namespace AKS.Payroll.Forms
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // PayrollValidator pm = new PayrollValidator();
            // var data = pm.findDuplicateDate(null);
            using (var db = new AzurePayrollDbContext())
            {
                WidgetManager wm = new WidgetManager();
                wm.GenerateFirstPageWidget(db, "ARD");
                var data = wm.FetchEmployeeInfo();
                if (data != null)
                {
                    // var dataData = from row in data select new { Date = row.Key, Count = row.Value };
                    dataGridView1.DataSource = null;
                    dataGridView1.DataBindings.Clear();
                    dataGridView1.DataSource = data.ToList();
                    //dataGridView1.Refresh();
                }


            }
        }
    }
}
