using AKS.ParyollSystem;

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
            PayrollValidator pm = new PayrollValidator();
            var data = pm.findDuplicateDate(null);
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
