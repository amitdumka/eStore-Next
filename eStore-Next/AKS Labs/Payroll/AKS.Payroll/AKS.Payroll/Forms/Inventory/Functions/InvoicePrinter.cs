
using AKS.Shared.Commons.Models.Inventory;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
//using PDFtoPrinter;
using Path = System.IO.Path;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class InvoicePrint
    {
        public const string DotedLine = "-----------------------------------\n";
        public const string DotedLineLong = "--------------------------------------------------\n";
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string TaxNo { get; set; }


        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }

        public ProductSale ProductSale { get; set; }
        public List<SalePaymentDetail> PaymentDetails { get; set; }
        public CardPaymentDetail CardDetails { get; set; }
        public string InvoicePath { get; set; }
        public int NoOfCopy { get; set; }
        public bool Reprint { get; set; }

        public string PathName { get; set; }
        public string FileName { get; set; }

        public const string InvoiceTitle = "                 RETAIL INVOICE";
        public const string ItemLineHeader1 = "SKU Code/Description/ HSN";
        public const string ItemLineHeader2 = "MRP     Qty     Disc     Amount";
        public const string ItemLineHeader3 = "CGST%    AMT     SGST%   AMT";

        public const string FooterFirstMessage = "** Amount Inclusive GST **";
        public const string FooterThanksMessage = "Thank You";
        public const string FooterLastMessage = "Visit Again";
        public const string Employee = "Cashier: M0001      Name: Manager";
          // BillNo = "Bill No: " + invNo;
           
         
           

        public string SetPdf()
        {
            string pathName = @"d:\apr\salereports";
            string fileName = Path.Combine(pathName, "salereport.pdf");
            Directory.CreateDirectory(pathName);
            return fileName;


        }
        public void InvoicePdf()
        {

            PathName = @"d:\apr\salereports";
            string fileName = Path.Combine(PathName, $"{ProductSale.InvoiceNo}.pdf");
            Directory.CreateDirectory(PathName);
            try
            {
                using PdfWriter pdfWriter = new PdfWriter(fileName);
                using PdfDocument pdf = new PdfDocument(pdfWriter);

                Document pdfDoc = new Document(pdf, new PageSize(240, 1170));

                pdfDoc.SetMargins(10, 5, 10, 5);

                Style code = new Style();
                PdfFont timesRoman = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                code.SetFont(timesRoman).SetFontSize(12);

                //Header
                Paragraph p = new Paragraph(StoreName + "\n").SetFontSize(12);
                p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                p.Add(Address + "\n");
                p.Add(City + "\n");
                p.Add("Ph No: " + Phone + "\n");
                p.Add(TaxNo + "\n");

                pdfDoc.Add(p);
                //Details
                Paragraph ip = new Paragraph().SetFontSize(12);

                ip.Add(DotedLine);
                ip.AddTabStops(new TabStop(50));
                ip.Add(" " + InvoiceTitle + "\n");
                ip.Add(DotedLineLong);

                ip.Add( Employee + "\n");
                ip.Add("Bill No: " + ProductSale.InvoiceNo + "\n");
                ip.AddTabStops(new TabStop(30));
                ip.Add("  " +  "                  Date: " + ProductSale.OnDate.ToShortDateString() + "\n");
                ip.AddTabStops(new TabStop(30));
                ip.Add("  " + "                  Time: " + ProductSale.OnDate.ToShortTimeString() + "\n");

                ip.Add("Customer Name: " + CustomerName + "\n");
                ip.Add("Customer Mobile: " + MobileNumber + "\n");
                ip.Add(DotedLineLong);

                ip.Add(ItemLineHeader1 + "\n");
                ip.Add(ItemLineHeader2 + "\n");

                ip.Add(DotedLineLong);

                decimal gstPrice = 0 ;
                decimal basicPrice = 0 ;
                string tab = "    ";

                foreach (var itemDetails in ProductSale.Items)
                {
                    //TODO: Need to implement HSNCode.
                    if (itemDetails != null)
                    {
                        ip.Add(itemDetails.Barcode + "itemDetails.HSN" + "/\n");
                        ip.Add((itemDetails.Value+itemDetails.DiscountAmount).ToString() + tab + tab);
                        ip.Add(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount + tab + tab + itemDetails.Value);
                        //ip.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + tab + tab);
                        //ip.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + "\n");
                        gstPrice +=  itemDetails.TaxAmount;
                        basicPrice +=  itemDetails.BasicAmount;
                    }
                }
                ip.Add("\n" + DotedLineLong);

                ip.Add("Total: " + ProductSale.BilledQty + tab + tab + tab + tab + tab + (ProductSale.TotalPrice-ProductSale.RoundOff).ToString() + "\n");
                ip.Add("item(s): " + ProductSale.TotalQty + tab + "Net Amount:" + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString() + "\n");
                ip.Add(DotedLineLong);

                ip.Add("Tender(s)\n Paid Amount:\t\t Rs. " + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString()); 
                //TODO: cash/Card option can be changed here

                // ip.Add("\n" + PrintInvoiceLine.DotedLine);
                //ip.Add("Basic Price:\t\t" + basicPrice.ToString("0.##"));
                //ip.Add("\nCGST:\t\t" + gstPrice.ToString("0.##"));
                //ip.Add("\nSGST:\t\t" + gstPrice.ToString("0.##") + "\n");
                //ip.Add (PrintLine.DotedLine);
                pdfDoc.Add(ip);

                //Footer
                Paragraph foot = new Paragraph().SetFontSize(12);
                //foot.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                foot.Add( FooterFirstMessage + "\n");
                foot.Add( DotedLineLong);
                foot.Add(FooterThanksMessage + "\n");
                foot.Add(FooterLastMessage + "\n");
                foot.Add(DotedLineLong);
                foot.Add("\n");// Just to Check;
                if (Reprint)
                {
                    foot.Add("(Reprinted)\n");
                }
                foot.Add("Printed on: " + DateTime.Now + "\n");
                pdfDoc.Add(foot);
                pdfDoc.Close();


            }
            catch (Exception e)
            {

                throw;
            }

        }



    }

    public class InvoicePrinter
    {

        //public static void PrintPDFLocal(string filePath)
        //{
        //    PrinterSettings settings = new PrinterSettings();

        //    string printerName = "Microsoft Print to PDF";

        //    if (!String.IsNullOrEmpty(settings.PrinterName)) printerName = settings.PrinterName;

        //    var printer = new PDFtoPrinterPrinter();
        //    printer.Print(new PrintingOptions(printerName, filePath));

        //}

        //public static void TestPrint()
        //{

        //    string fileName = Path.GetTempPath() + "testprint.pdf";

        //    using PdfWriter pdfWriter = new PdfWriter(fileName);
        //    using PdfDocument pdf = new PdfDocument(pdfWriter);
        //    Document pdfDoc = new Document(pdf);
        //    //Header
        //    Paragraph p = new Paragraph("Hello! \n This is Test Print for testing default printer!. \n\n Aprajita Retails Dev. Team.");
        //    pdfDoc.Add(p);
        //    pdf.AddNewPage();
        //    pdfDoc.Close();

        //   // PrintPDFLocal(fileName);
        //}

       
    }

    
   

    
    //public class ReceiptItemDetails
    //{
    //    public string BasicPrice { get; set; }
    //    public string HSN { get; set; }
    //    public string SKUDescription { get; set; }
    //    public string MRP { get; set; }
    //    public string QTY { get; set; }
    //    public string Discount { get; set; }
    //    public string GSTPercentage { get; set; }
    //    public string GSTAmount { get; set; }
    //    public string Amount { get; set; }
    //}
   

}

//Helper links
//https://www.syncfusion.com/blogs/post/create-zugferd-compliant-pdf-invoices-in-c.aspx
//https://help.syncfusion.com/file-formats/pdf/working-with-zugferd-invoice
//https://www.aspsnippets.com/Articles/Generate-Invoice-Bill-Receipt-PDF-from-database-in-ASPNet-using-C-and-VBNet.aspx
//https://docs.microsoft.com/en-us/samples/microsoft/windows-universal-samples/posprinter/
//https://www.tillpoint.com/gb/how-to-create-print-templates--1
