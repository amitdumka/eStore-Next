using AKS.Shared.Commons.Models.Inventory;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.ComponentModel.DataAnnotations;
using Image = iText.Layout.Element.Image;

//using PDFtoPrinter;
using Path = System.IO.Path;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    /// <summary>
    /// Invoice Printer : It generate invoice for thermal printer 2/3/4 inch
    /// </summary>
    public class InvoicePrint
    {
        [Required]
        public bool InvoiceSet { get; set; }

        private int PageWith = 150;
        private int PageHeight = 1170;
        private int FontSize = 8;
        public bool Page2Inch { get; set; } = false;

        public const string DotedLine = "---------------------------------\n";
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

        public int NoOfCopy { get; set; }
        public bool Reprint { get; set; }

        public string PathName { get; set; }
        public string FileName { get; set; }

        private const string InvoiceTitle = "                 RETAIL INVOICE";
        private const string ItemLineHeader1 = "SKU Code/Description/ HSN";
        private const string ItemLineHeader2 = "MRP     Qty     Disc     Amount";
        private const string ItemLineHeader3 = "CGST%    AMT     SGST%   AMT";

        private const string FooterFirstMessage = "** Amount Inclusive GST **";
        private const string FooterThanksMessage = "Thank You";
        private const string FooterLastMessage = "Visit Again";

        private const string Employee = "Cashier: M0001      Name: Manager";

        /// <summary>
        /// Invoice Printing to PDF
        /// </summary>
        /// <returns></returns>
        public string InvoicePdf()
        {
            if (!InvoiceSet) return null;

            if (string.IsNullOrEmpty(PathName))
            {
                PathName = @"d:\apr\invoices";
                string fileName = Path.Combine(PathName, $"{ProductSale.InvoiceNo}.pdf");
                FileName = fileName;
                Directory.CreateDirectory(PathName);
            }
            var x = FileName.Replace(Path.GetFileName(FileName), "");
            Directory.CreateDirectory(x);

            if (!Page2Inch)
            {
                PageWith = 240;
                FontSize = 10;
            }
            try
            {
                using PdfWriter pdfWriter = new PdfWriter(FileName);
                using PdfDocument pdf = new PdfDocument(pdfWriter);

                Document pdfDoc = new Document(pdf, new PageSize(PageWith, PageHeight));

                if(Page2Inch)
                pdfDoc.SetMargins(90, 25, 90, 8);
                else
                    pdfDoc.SetMargins(170, 25, 90, 35);
                Style code = new Style();
                PdfFont timesRoman = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                code.SetFont(timesRoman).SetFontSize(FontSize);

                //Header
                Paragraph p = new Paragraph(StoreName + "\n").SetFontSize(FontSize);
                p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                p.Add(Address + "\n");
                p.Add(City + "\n");
                p.Add("Ph No: " + Phone + "\n");
                p.Add(TaxNo + "\n");
                p.AddStyle(code);

                pdfDoc.Add(p);

                //Details
                Paragraph ip = new Paragraph().SetFontSize(FontSize);
                ip.AddStyle(code);
                ip.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED_ALL);
                if (!Page2Inch) ip.Add(DotedLineLong);
                else ip.Add(DotedLine);
                ip.AddTabStops(new TabStop(50));
                ip.Add("  " + InvoiceTitle + "\n");
                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);

                ip.Add(Employee + "\n");
                ip.Add("Bill No: " + ProductSale.InvoiceNo + "\n");
                ip.AddTabStops(new TabStop(30));
                ip.Add("  " + "                  Date: " + ProductSale.OnDate.ToString() + "\n");
                ip.AddTabStops(new TabStop(30));
                //ip.Add("  " + "                  Time: " + ProductSale.OnDate.ToShortTimeString() + "\n");

                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add("Customer Name: " + CustomerName + "\n");
                ip.Add("Customer Mobile: " + MobileNumber + "\n");
                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);

                ip.Add(ItemLineHeader1 + "\n");
                ip.Add(ItemLineHeader2 + "\n");

                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);

                decimal gstPrice = 0;
                decimal basicPrice = 0;
                string tab = "    ";

                foreach (var itemDetails in ProductSale.Items)
                {
                    //TODO: Need to implement HSNCode.
                    if (itemDetails != null)
                    {
                        ip.Add($"{itemDetails.Barcode} / {itemDetails.ProductItem.Description}/{itemDetails.ProductItem.HSNCode} /\n");
                        ip.Add((itemDetails.Value + itemDetails.DiscountAmount).ToString("0.##") + tab + tab);
                        if (itemDetails.Value == 0)
                        {
                            ip.Add(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount.ToString("0.##") + tab + tab + "Free\n");

                        }
                        else
                        {
                            ip.Add(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount.ToString("0.##") + tab + tab + itemDetails.Value.ToString("0.##")+"\n");

                        }
                        //ip.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + tab + tab);
                        //ip.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + "\n");
                        gstPrice += itemDetails.TaxAmount;
                        basicPrice += itemDetails.BasicAmount;
                    }
                }
                if (!Page2Inch)
                    ip.Add("\n" + DotedLineLong);
                else ip.Add("\n" + DotedLine);

                ip.Add("Total: " + ProductSale.BilledQty + tab + tab + tab + tab + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##") + "\n");
                ip.Add("item(s): " + ProductSale.TotalQty + tab + "Net Amount:" + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##") + "\n");

                if (!Page2Inch) ip.Add(DotedLineLong);
                else ip.Add(DotedLine);

                ip.Add("Tender (s)\t\n Paid Amount:\t\t Rs. " + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##"));

                ip.Add("\n" + DotedLine);
                ip.Add("Basic Price:\t\t" + basicPrice.ToString("0.##"));
                ip.Add("\nCGST:\t\t" + gstPrice.ToString("0.##"));
                ip.Add("\nSGST:\t\t" + gstPrice.ToString("0.##") + "\n");
                ip.Add(DotedLine);

                if (PaymentDetails.Count > 0)
                {
                    ip.Add(DotedLine);
                    foreach (var pd in PaymentDetails)
                    {
                        ip.Add($"Paid Rs. {pd.PaidAmount.ToString("0.##")} in {pd.PayMode}\n ");
                        if (pd.PayMode == PayMode.Card)
                        {
                            if (CardDetails != null)
                                ip.Add($"{CardDetails.CardType}/{CardDetails.CardLastDigit}");
                        }
                        else if (pd.PayMode == PayMode.UPI || pd.PayMode == PayMode.Wallets)
                        {
                            ip.Add($"RefNo:{pd.RefId}\n ");
                        }
                    }
                    ip.Add(DotedLine);
                }

                pdfDoc.Add(ip);

                //Footer
                Paragraph foot = new Paragraph().SetFontSize(FontSize);
                foot.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                foot.AddStyle(code);
                foot.Add(FooterFirstMessage + "\n");
                foot.Add(DotedLineLong);
                foot.Add(FooterThanksMessage + "\n");
                foot.Add(FooterLastMessage + "\n");
                foot.Add(DotedLineLong);

                foot.Add("\n");// Just to Check;

                if (Reprint)
                {
                    foot.Add("(Reprinted Duplicate)\n");
                }
                else
                {
                    foot.Add("(Customer Copy)\n");
                }

                foot.Add("Printed on: " + DateTime.Now + "\n\n\n\n\n");
                foot.Add("\n" + DotedLine + "\n\n\n\n");


                // var barcode = SaleUtils.GenerateQRCode(ProductSale.InvoiceNo,ProductSale.OnDate,ProductSale.TotalPrice);
                var barcode = SaleUtils.GenerateBarCode(ProductSale.InvoiceNo);//,ProductSale.OnDate,ProductSale.TotalPrice);
               if(barcode != null)
                {
                    var img = ImageDataFactory.CreatePng(barcode.ToPngBinaryData());
                    var imges = new Image(img);
                    imges.Scale((float)0.5, (float)0.1);
                    imges.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    imges.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    pdfDoc.Add(imges);
                }
               
                pdfDoc.Add(foot);
                pdfDoc.Close();
                return FileName;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

//Helper links
//https://www.syncfusion.com/blogs/post/create-zugferd-compliant-pdf-invoices-in-c.aspx
//https://help.syncfusion.com/file-formats/pdf/working-with-zugferd-invoice
//https://www.aspsnippets.com/Articles/Generate-Invoice-Bill-Receipt-PDF-from-database-in-ASPNet-using-C-and-VBNet.aspx
//https://docs.microsoft.com/en-us/samples/microsoft/windows-universal-samples/posprinter/
//https://www.tillpoint.com/gb/how-to-create-print-templates--1