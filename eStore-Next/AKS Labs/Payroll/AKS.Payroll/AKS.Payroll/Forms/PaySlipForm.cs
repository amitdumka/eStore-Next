using AKS.ParyollSystem;
using AKS.ParyollSystem.Reports;
using AKS.Payroll.Database;
using AKS.Payroll.DTOMapping;
using AKS.Shared.Payroll.Models;
using AKS.Shared.Payrolls.ViewModels;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Windows.Forms.PdfViewer;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class PaySlipForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;

        private ObservableListSource<PaySlipVM> PaySlips;

        public PaySlipForm()
        {
            InitializeComponent();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are your sure to generate payslip for current month payable of last working month for all Employees ??",
                                      "Confirm !!",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                UpdateUI(new PayslipManager().GeneratePayslips(null, DateTime.Today.AddMonths(-1)).Result);
            }
        }

        private async void UpdateUI(SortedDictionary<string, PaySlip> paySlips)
        {
            if (paySlips != null)
            {
                foreach (var paySlip in paySlips)
                {
                    PaySlips.Add(DMMapper.Mapper.Map<PaySlipVM>(paySlip.Value));
                }
                PaySlips.Distinct();

                //dgvPayslips.DataSource = PaySlips.Where(c => c.OnDate.Date == date.Date).ToList();
                tsslCountValue.Text = $" {paySlips.Count}/{PaySlips.Count}";
                var date = paySlips.First().Value.OnDate.Date;
                dgvPayslips.DataSource = PaySlips.Where(c => c.OnDate.Date == date.Date).ToList();
            }
        }

        private void btnSelectedEmployee_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are your sure to generate payslip for current month payable of last working month for selected Employee ??",
                                     "Confirm !!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                MessageBox.Show("Not implemented, Kindly contact admin.");
            }
        }

        private void PaySlipForm_Load(object sender, EventArgs e)
        {
            DMMapper.InitializeAutomapper();
            PaySlips = new ObservableListSource<PaySlipVM>();
            //using (
            azureDb = new AzurePayrollDbContext();
                //)
            //{
                lbEmployees.DataSource = azureDb.Employees.ToList();
                lbEmployees.DisplayMember = "StaffName";
                lbEmployees.ValueMember = "EmployeeId";
                LoadData(azureDb.PaySlips.Where(c => c.Year == DateTime.Today.Year).ToList());
            //}
        }

        private async void LoadData(List<PaySlip> slips)
        {
            if (slips != null)
            {
                foreach (var slip in slips)
                {
                    PaySlips.Add(DMMapper.Mapper.Map<PaySlipVM>(slip));
                }
            }
            dgvPayslips.DataSource = PaySlips.ToBindingList();
            tsslCountValue.Text = PaySlips.Count.ToString();
        }
        private void PrintFile(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;
            PdfViewerControl pdfViewer = new PdfViewerControl();
            pdfViewer.Load(filename);
            pdfViewer.Dock = DockStyle.Fill;
            Form printForm = new Form();
            printForm.Text = "Print";
            printForm.WindowState = FormWindowState.Maximized;
            //printForm.MdiParent=this.MdiParent;
            GroupBox footerBox = new GroupBox();
            footerBox.Dock = DockStyle.Bottom;
            Button okButton = new Button();
            okButton.Dock = DockStyle.Left;
            okButton.Text = "OK";
            Button printButton = new Button();
            printButton.Text = "Print";
            printButton.Dock = DockStyle.Left;
            Button cancleButton = new Button();
            cancleButton.Text = "Cancle";
            cancleButton.Dock = DockStyle.Right;
            footerBox.Controls.Add(okButton);
            footerBox.Controls.Add(printButton);
            footerBox.Controls.Add(cancleButton);
            GroupBox groupBox = new GroupBox();
            groupBox.Dock = DockStyle.Fill;
            groupBox.Name = "Print";
            groupBox.Controls.Add(pdfViewer);
            okButton.DialogResult = DialogResult.OK;
            cancleButton.DialogResult = DialogResult.Cancel;
            printButton.DialogResult = DialogResult.Yes;
            printForm.Controls.Add(footerBox);
            printForm.Controls.Add(groupBox);
            //footerBox.AutoSize = true;
            //footerBox.AutoSizeMode = AutoSizeMode.GrowOnly;

            DialogResult dialogResult = printForm.ShowDialog();
            if(dialogResult == DialogResult.Yes)
            {
                Print(pdfViewer);
               // return true;
            }
            else if (dialogResult == DialogResult.OK)
            {
                Print(pdfViewer);
               // return true;
            }
            else if(dialogResult == DialogResult.Cancel)
            {
                //return false;
            }
            else
            {
                MessageBox.Show("Form Closed");
            }

        }
        private void Print(PdfViewerControl pdfViewer)
        {
            var printDialog1 = new PrintDialog();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.AllowPrintToFile = true;
                pdfViewer.Print(printDialog1.PrinterSettings.PrinterName);
            }
        }
        private void btnPrintPayslip_Click(object sender, EventArgs e)
        {
            if (dgvPayslips.CurrentRow.DataBoundItem == null)
            {
                if (lbEmployees.SelectedValue != null)
                {
                    var confirmResult2 = MessageBox.Show("Are your sure to print payslip for current month payable of last working month for selected Employee ??",
                                     "Confirm !!",
                                     MessageBoxButtons.YesNo);
                    if (confirmResult2 == DialogResult.Yes)
                    {

                       var file= PrintPaySlip.Print(azureDb, lbEmployees.SelectedValue.ToString(), DateTime.Today.Year, DateTime.Today.AddMonths(-1).Month, false, true);
                        lbEmployees.ClearSelected();
                        PrintFile(file);
                    }
                }
                else
                {

                    var confirmResult = MessageBox.Show("Are your sure to print payslip for current month payable of last working month for all Employee ??",
                                        "Confirm !!",
                                        MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                      var files = PrintPaySlip.PrintCurrentMonth(azureDb, false, false);
                        foreach (var file in files)
                        {
                            PrintFile(file);
                        }
                    }
                }
            }
            else
            {
                var confirmResult = MessageBox.Show("Do you want to print selected Payslip ??",
                                    "Confirm !!",
                                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var row = (PaySlipVM)dgvPayslips.CurrentRow.DataBoundItem;
                    dgvPayslips.ClearSelection();
                   var file= PrintPaySlip.Print(azureDb, row.EmployeeId, row.Year, row.Month, false, true);
                    PrintFile(file);
                }
            }
        }

        private void btnProcessAll_Click(object sender, EventArgs e)
        {

        }

        private void dgvPayslips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}