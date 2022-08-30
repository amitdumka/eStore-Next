using AKS.Payroll.Database;
using AKS.Printers.Thermals;
using Microsoft.EntityFrameworkCore;

namespace AKS.PosSystem.Test
{//TODO: remove in final version

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
                VoucherNo = payment.SlipNumber + "#" + payment.VoucherNumber,
                TranscationMode = null,
                PaymentDetails = payment.PaymentDetails,
                StoreCode = payment.StoreId,
                Remarks = payment.Remarks,
                IssuedBy = payment.Employee.StaffName,
                PrintType = PrintType.PaymentVoucher
            };
            fileNames.Add(print.PrintPdf());

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
                Voucher = VoucherType.Payment,
                VoucherNo = cashP.SlipNumber + "#" + cashP.VoucherNumber,
                TranscationMode = null,
                PaymentDetails = cashP.TranscationMode.TranscationName,
                StoreCode = cashP.StoreId,
                Remarks = cashP.Remarks,
                IssuedBy = cashP.Employee.StaffName,
                PrintType = PrintType.PaymentVoucher
            };
            fileNames.Add(print.PrintPdf());
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
                VoucherNo = receipts.SlipNumber + "#" + receipts.VoucherNumber,
                TranscationMode = null,
                PaymentDetails = receipts.PaymentDetails,
                StoreCode = receipts.StoreId,
                Remarks = receipts.Remarks,
                IssuedBy = receipts.Employee.StaffName,
                PrintType = PrintType.ReceiptVocuher
            };
            fileNames.Add(print.PrintPdf());
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
                VoucherNo = exp.SlipNumber + "#" + exp.VoucherNumber,
                TranscationMode = null,
                PaymentDetails = exp.PaymentDetails,
                StoreCode = exp.StoreId,
                Remarks = exp.Remarks,
                IssuedBy = exp.Employee.StaffName,
                PrintType = PrintType.Expenses
            };
            fileNames.Add(print.PrintPdf());

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
                Voucher = VoucherType.Payment,
                VoucherNo = cashR.SlipNumber + "#" + cashR.VoucherNumber,
                TranscationMode = null,
                PaymentDetails = cashR.TranscationMode.TranscationName,
                StoreCode = cashR.StoreId,
                Remarks = cashR.Remarks,
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
}
