using AKS.Payroll.Database;
using Microsoft.EntityFrameworkCore;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SaleTest
    {
        public static string TestPrint(AzurePayrollDbContext db)
        {
            var inv = db.ProductSales.Include(c => c.Salesman).Where(c => c.InvoiceCode== "ARD/2019/2163").First();
            //var inv = db.ProductSales.Include(c => c.Salesman).Where(c => c.OnDate.Month == 4 && c.OnDate.Year == 2022).First();
            inv.Items = db.SaleItems.Include(c => c.ProductItem).Where(c => c.InvoiceCode == inv.InvoiceCode).ToList();
            
           
            InvoicePrint print = new InvoicePrint
            {
                //FontSize = 12,  PageWith=240,
                //PageHeight=1170,
                //InvoicePath = "",
                InvoiceSet = true,
                Page2Inch = false,
                CustomerName = "Cash Sale",
                City = "Dumka",FileName="", MobileNumber="1234567890", NoOfCopy=1, Address="Bhagalpur Road, Dumka", PathName="", 
                Phone="06434-224461", Reprint=true, ProductSale=inv,
                StoreName = "Aprajita Retails",
                TaxNo = "20AJHPA7396P1ZV",CardDetails=db.CardPaymentDetails.Where(c=>c.InvoiceCode==inv.InvoiceCode).FirstOrDefault(),
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