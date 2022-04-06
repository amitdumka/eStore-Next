using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Payroll.Forms;

namespace AKS.Payroll.Ops
{
    public class BasicOperations
    {
        public async void PayrollReport()
        {
            using (var db = new AzurePayrollDbContext())
            {
                PayrollReports pr = new PayrollReports();
                var filename = await pr.PaySlipReportForAllEmpoyeeAsync(db, DateTime.Today.AddMonths(-1));

                PdfForm form = new PdfForm(filename);
                form.Show(); ;
            }
        }
    }
}
