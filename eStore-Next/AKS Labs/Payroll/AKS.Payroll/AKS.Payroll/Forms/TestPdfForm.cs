using AKS.Payroll.Database;
using AKS.Printers.Thermals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.SRPMix.Forms
{
    public partial class TestPdfForm : Form
    {
        public TestPdfForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var printDialog1 = new PrintDialog();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.AllowPrintToFile = true;
                pdfViewer.Print(printDialog1.PrinterSettings.PrinterName);
            }
        }

        AzurePayrollDbContext db = new AzurePayrollDbContext();
        List<string> fns;
        int count = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            // Voucher test
            fns = PrintTest.TestPrintVoucher(db);

            if (fns != null && !string.IsNullOrEmpty(fns[0]))
                pdfViewer.Load(fns[0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fn = PrintTest.TestPrintInvoice(db);
            count = 0;
            if (fn != null)
                pdfViewer.Load(fn);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (count < fns.Count - 1)
                pdfViewer.Load(fns[++count]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (count > 0)
                pdfViewer.Load(fns[--count]);
        }

        
    }
}
public class PrintTest
{
    public static List<string> TestPrintVoucher(AzurePayrollDbContext db)
    {
        var payment = db.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.Payment).FirstOrDefault();
        var receipts = db.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.Receipt).FirstOrDefault();
        var exp = db.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.Expense).FirstOrDefault();

        var cashP = db.CashVouchers.Include(c => c.TranscationMode).Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.CashPayment).FirstOrDefault();
        var cashR = db.CashVouchers.Include(c => c.TranscationMode).Include(c => c.Employee).Include(c => c.Partys).Where(c => c.VoucherType == VoucherType.CashReceipt).FirstOrDefault();

        List<string> fileNames = new List<string>();
       //Payment
        VoucherPrint print = new VoucherPrint
        {
            Amount = payment.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = payment.OnDate,
            Particulars = payment.Particulars,
            PartyName = payment.PartyName,
            LedgerName = payment.Partys.PartyName,
            Reprint = false,
            PaymentMode = payment.PaymentMode,
            Voucher = VoucherType.Payment,
            VoucherNo = payment.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = payment.PaymentDetails,
            StoreCode = payment.StoreId,
            Remarks = payment.Remarks + " SlipNo:" + payment.SlipNumber,
            IssuedBy = payment.Employee.StaffName,
            PrintType = PrintType.PaymentVoucher
        };
        fileNames.Add(print.PrintPdf());

        //Cash Payment
        print = new VoucherPrint
        {
            Amount = cashP.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = cashP.OnDate,
            Particulars = cashP.Particulars,
            PartyName = cashP.PartyName,
            LedgerName = cashP.Partys.PartyName,
            Reprint = false,
            PaymentMode = PaymentMode.Cash,
            Voucher = VoucherType.CashPayment,
            VoucherNo = cashP.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = cashP.TranscationMode.TranscationName,
            StoreCode = cashP.StoreId,
            Remarks = cashP.Remarks + " SlipNo:" + cashP.SlipNumber,
            IssuedBy = cashP.Employee.StaffName,
            PrintType = PrintType.PaymentVoucher
        };
        fileNames.Add(print.PrintPdf());
        //Receipts
        print = new VoucherPrint
        {
            Amount = receipts.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = receipts.OnDate,
            Particulars = receipts.Particulars,
            PartyName = receipts.PartyName,
            LedgerName = receipts.Partys.PartyName,
            Reprint = false,
            PaymentMode = receipts.PaymentMode,
            Voucher = VoucherType.Receipt,
            VoucherNo = receipts.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = receipts.PaymentDetails,
            StoreCode = receipts.StoreId,
            Remarks = receipts.Remarks + " SlipNo:" + receipts.SlipNumber,
            IssuedBy = receipts.Employee.StaffName,
            PrintType = PrintType.ReceiptVocuher
        };
        fileNames.Add(print.PrintPdf());
        
        //Expnses
        print = new VoucherPrint
        {
            Amount = exp.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = exp.OnDate,
            Particulars = exp.Particulars,
            PartyName = exp.PartyName,
            LedgerName = exp.Partys.PartyName,
            Reprint = false,
            PaymentMode = exp.PaymentMode,
            Voucher = VoucherType.Expense,
            VoucherNo = exp.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = exp.PaymentDetails,
            StoreCode = exp.StoreId,
            Remarks = exp.Remarks + " SlipNo:" + exp.SlipNumber,
            IssuedBy = exp.Employee.StaffName,
            PrintType = PrintType.Expenses
        };
        fileNames.Add(print.PrintPdf());

        //CashRec   
        print = new VoucherPrint
        {
            Amount = cashR.Amount,
            IsVoucherSet = true,
            NoOfCopy = 1,
            Page2Inch = false,
            OnDate = cashR.OnDate,
            Particulars = cashR.Particulars,
            PartyName = cashR.PartyName,
            LedgerName = cashR.Partys.PartyName,
            Reprint = false,

            PaymentMode = PaymentMode.Cash,
            Voucher = VoucherType.CashReceipt,
            VoucherNo = cashR.VoucherNumber,
            TranscationMode = null,
            PaymentDetails = cashR.TranscationMode.TranscationName,
            StoreCode = cashR.StoreId,
            Remarks = cashR.Remarks + " SlipNo:" + cashR.SlipNumber,
            IssuedBy = cashR.Employee.StaffName,
            PrintType = PrintType.CashReceiptVocucher
        };
        fileNames.Add(print.PrintPdf());
        return fileNames;


    }
    public static string TestPrintInvoice(AzurePayrollDbContext db)
    {
        // var inv = db.ProductSales.Include(c => c.Salesman).Where(c => c.InvoiceCode == "ARD/2019/2163").First();
        var inv = db.ProductSales.Include(c => c.Salesman).Where(c => c.OnDate.Month == DateTime.Today.Month && c.OnDate.Year == 2022).First();
        inv.Items = db.SaleItems.Include(c => c.ProductItem).Where(c => c.InvoiceCode == inv.InvoiceCode).ToList();

        InvoicePrint print = new InvoicePrint
        {

            InvoiceSet = true,
            Page2Inch = false,
            CustomerName = "Cash Sale",
            //City = "Dumka",
            FileName = "",
            MobileNumber = "1234567890",
            NoOfCopy = 1,
            //Address = "Bhagalpur Road, Dumka",
            PathName = "",
            //Phone = "06434-224461",
            Reprint = true,
            ProductSale = inv,
            //StoreName = "Aprajita Retails",
            //TaxNo = "20AJHPA7396P1ZV",
            CardDetails = db.CardPaymentDetails.Where(c => c.InvoiceCode == inv.InvoiceCode).FirstOrDefault(),
            PaymentDetails = db.SalePaymentDetails.Where(c => c.InvoiceCode == inv.InvoiceCode).ToList(),
        };

        return print.InvoicePdf();
        //var printDialog1 = new PrintDialog();
        //if (printDialog1.ShowDialog() == DialogResult.OK)
        //{
        //    printDialog1.AllowPrintToFile = true;
        //    PdfDocumentView pdfview = new PdfDocumentView();
        //    pdfview.Load(filename);
        //    pdfview.Print(printDialog1.PrinterSettings.PrinterName);
        //}
    }
}