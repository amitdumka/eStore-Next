using AKS.Shared.Commons.Models.Inventory;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using System.ComponentModel.DataAnnotations;
using Image = iText.Layout.Element.Image;

//using PDFtoPrinter;
using Path = System.IO.Path;

namespace AKS.Printers.Thermals
{
    /// <summary>
    /// Invoice Printer : It generate invoice for thermal printer 2/3/4 inch
    /// </summary>
    public class InvoicePrint : ThermalPrinter
    {
        private const string InvoiceTitle = "                 RETAIL INVOICE";
        private const string ItemLineHeader1 = "SKU Code/Description/ HSN";
        private const string ItemLineHeader2 = "MRP     Qty     Disc     Amount";
        private const string ItemLineHeader3 = "CGST%    AMT     SGST%   AMT";

        private const string FooterThanksMessage = "Thank You";
        private const string FooterLastMessage = "Visit Again";

        private const string Employee = "Cashier: M0001      Name: Manager";

        [Required]
        public bool InvoiceSet { get; set; }

        public bool ServiceBill { get; set; } = false;

        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }

        public ProductSale ProductSale { get; set; }
        public List<SalePaymentDetail> PaymentDetails { get; set; }
        public CardPaymentDetail CardDetails { get; set; }

        /// <summary>
        /// Invoice Printing to PDF
        /// </summary>
        /// <returns></returns>
        public string InvoicePdf()
        {
            try
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

                TitleName = InvoiceTitle;
                if (ServiceBill)
                {
                    SubTitle = true;
                    SubTitleName = "Service Invoice";
                }

                Style code = new Style();
                PdfFont timesRoman = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                code.SetFont(timesRoman).SetFontSize(FontSize);

                //Details
                _content = new Paragraph().SetFontSize(FontSize);
                _content.AddStyle(code);
                _content.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED_ALL);
                _content.Add(Employee + "\n");
                _content.Add("Bill No: " + ProductSale.InvoiceNo + "\n");
                _content.AddTabStops(new TabStop(30));
                _content.Add("  " + "                  Date: " + ProductSale.OnDate.ToString() + "\n");
                _content.AddTabStops(new TabStop(30));
                //_content.Add("  " + "                  Time: " + ProductSale.OnDate.ToShortTimeString() + "\n");

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("Customer Name: " + CustomerName + "\n");
                _content.Add("Customer Mobile: " + MobileNumber + "\n");
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);

                _content.Add(ItemLineHeader1 + "\n");
                _content.Add(ItemLineHeader2 + "\n");

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);

                decimal gstPrice = 0;
                decimal basicPrice = 0;
                string tab = "    ";

                foreach (var itemDetails in ProductSale.Items)
                {
                    //TODO: Need to implement HSNCode.
                    if (itemDetails != null)
                    {
                        _content.Add($"{itemDetails.Barcode} / {itemDetails.ProductItem.Description}/{itemDetails.ProductItem.HSNCode} /\n");
                        _content.Add((itemDetails.Value + itemDetails.DiscountAmount).ToString("0.##") + tab + tab);
                        if (itemDetails.Value == 0)
                        {
                            _content.Add(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount.ToString("0.##") + tab + tab + "Free\n");
                        }
                        else
                        {
                            _content.Add(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount.ToString("0.##") + tab + tab + itemDetails.Value.ToString("0.##") + "\n");
                        }
                        //_content.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + tab + tab);
                        //_content.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + "\n");
                        gstPrice += itemDetails.TaxAmount;
                        basicPrice += itemDetails.BasicAmount;
                    }
                }
                if (!Page2Inch)
                    _content.Add("\n" + DotedLineLong);
                else _content.Add("\n" + DotedLine);

                _content.Add("Total: " + ProductSale.BilledQty + tab + tab + tab + tab + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##") + "\n");
                _content.Add("item(s): " + ProductSale.TotalQty + tab + "Net Amount:" + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##") + "\n");

                if (!Page2Inch) _content.Add(DotedLineLong);
                else _content.Add(DotedLine);

                _content.Add("Tender (s)\t\n Paid Amount:\t\t Rs. " + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##"));

                _content.Add("\n" + DotedLine);
                _content.Add("Basic Price: " + basicPrice.ToString("0.##") + "\n");
                _content.Add("CGST: " + gstPrice.ToString("0.##") + "\n");
                _content.Add("SGST: " + gstPrice.ToString("0.##") + "\n");
                _content.Add(DotedLine);

                if (PaymentDetails.Count > 0)
                {
                    _content.Add(DotedLine);
                    foreach (var pd in PaymentDetails)
                    {
                        _content.Add($"Paid Rs. {pd.PaidAmount.ToString("0.##")} in {pd.PayMode}\n ");
                        if (pd.PayMode == PayMode.Card)
                        {
                            if (CardDetails != null)
                                _content.Add($"{CardDetails.CardType}/{CardDetails.CardLastDigit}");
                        }
                        else if (pd.PayMode == PayMode.UPI || pd.PayMode == PayMode.Wallets)
                        {
                            _content.Add($"RefNo:{pd.RefId}\n ");
                        }
                    }
                    _content.Add(DotedLine);
                }



                //Footer
                _footer = new Paragraph().SetFontSize(FontSize);
                _footer.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                _footer.AddStyle(code);
                _footer.Add(FooterFirstMessage + "\n");
                if (ServiceBill) _footer.Add("** Tailoring Service Invoice **");
                _footer.Add(DotedLineLong);
                _footer.Add(FooterThanksMessage + "\n");
                _footer.Add(FooterLastMessage + "\n");
                _footer.Add(DotedLineLong);

                _footer.Add("\n");// Just to Check;

                if (Reprint)
                {
                    _footer.Add("(Reprinted Duplicate)\n");
                }
                else
                {
                    _footer.Add("(Customer Copy)\n");
                }

                _footer.Add("Printed on: " + DateTime.Now + "\n\n\n\n\n");
                _footer.Add("\n" + DotedLine + "\n\n\n\n");

                var barcode = Barcodes.QRBarcode.GenerateQRCode(ProductSale.InvoiceNo, ProductSale.OnDate, ProductSale.TotalPrice);
                //var barcode = Barcodes.QRBarcode.GenerateBarCode(ProductSale.InvoiceNo);//,ProductSale.OnDate,ProductSale.TotalPrice);
                if (barcode != null)
                {
                    var img = ImageDataFactory.CreatePng(barcode.ToPngBinaryData());
                    _qrBarcode = new Image(img);
                    _qrBarcode.Scale((float)0.1, (float)0.1);
                    _qrBarcode.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    _qrBarcode.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    //pdfDoc.Add(imges);
                }
                //pdfDoc.Add(ip);
                //pdfDoc.Add(foot);
                //pdfDoc.Close();
                //return FileName;
                return CreateDocument();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

}