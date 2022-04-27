namespace AKS.Payroll.Forms
{
    public partial class PdfForm : Form
    {
        private string FileName = "";
        public PdfForm()
        {
            InitializeComponent();
        }
        public PdfForm(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDialog1 = new PrintDialog();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.AllowPrintToFile = true;

                pdfDocumentView1.Print(printDialog1.PrinterSettings.PrinterName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fName = saveFileDialog1.FileName;
                File.Move(FileName, fName);
                FileName = fName;
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {

        }

        private void PdfForm_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FileName))
            {
                //pdfViewerControl1.Load("Sample.pdf");
                pdfDocumentView1.Load(FileName);
            }
        }

        private void recordNavigationControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
