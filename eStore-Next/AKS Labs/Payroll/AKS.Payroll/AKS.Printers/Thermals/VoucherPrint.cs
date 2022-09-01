//using PDFtoPrinter;

using iText.IO.Image;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;

namespace AKS.Printers.Thermals
{
    public class VoucherPrint : ThermalPrinter
    {
        public bool IsVoucherSet { get; set; }
        // private string TitleName { get; set; }
        //private string Description { get; set; }

        public string PartyName { get; set; }
        public string VoucherNo { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Amount { get; set; }
        public VoucherType Voucher { get; set; }

        public string Particulars { get; set; }

        public PaymentMode PaymentMode { get; set; }
        public string PaymentDetails { get; set; }
        public string Remarks { get; set; }

        public string IssuedBy { get; set; }

        public string LedgerName { get; set; }
        public string TranscationMode { get; set; }

        public string PrintPdf()
        {
            try
            {
                if (!Page2Inch)
                {
                    PageWith = 240;
                    FontSize = 10;
                    PageHeight = 1170 * 2;

                }
                if (!IsVoucherSet) return null;
                switch (Voucher)
                {
                    case VoucherType.Payment:
                        PrintType = PrintType.PaymentVoucher;
                        TitleName = "PAYMENT VOUCHER";
                        break;

                    case VoucherType.Receipt:
                        TitleName = "RECEIPT VOUCHER";
                        PrintType = PrintType.ReceiptVocuher;
                        break;

                    case VoucherType.Contra:
                        TitleName = "CONTRA VOUCHER";
                        PrintType = PrintType.Contra;
                        break;

                    case VoucherType.DebitNote:
                        TitleName = "DEBIT NOTE";
                        PrintType = PrintType.DebitNote;
                        break;

                    case VoucherType.CreditNote:
                        PrintType = PrintType.CreditNote;
                        TitleName = "CREDIT NOTE";
                        break;

                    case VoucherType.JV:
                        PrintType = PrintType.JV;
                        TitleName = "JOURNAL VOUCHER";
                        break;

                    case VoucherType.Expense:
                        PrintType = PrintType.Expenses;
                        TitleName = "EXPENSE VOUCHER";
                        break;

                    case VoucherType.CashReceipt:
                        PrintType = PrintType.CashReceiptVocucher;
                        TitleName = "RECEIPT VOUCHER";
                        SubTitleName = "      Cash Voucher     ";
                        SubTitle = true;
                        break;

                    case VoucherType.CashPayment:
                        PrintType = PrintType.CashReceiptVocucher;
                        TitleName = "PAYMENT VOUCHER";
                        SubTitle = true; SubTitleName = "      Cash  Voucher    ";
                        break;

                    default:
                        PrintType = PrintType.PaymentVoucher;
                        TitleName = "PAYMENT VOUCHER";
                        break;
                }
                GenrateFileName(VoucherNo);
                SetStoreInfo();

                Style code = new Style();
                PdfFont timesRoman = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                code.SetFont(timesRoman).SetFontSize(FontSize);

                //Details
                _content = new Paragraph().SetFontSize(FontSize);
                _content.AddStyle(code);
                _content.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED_ALL);
                //Footer
                _footer = new Paragraph().SetFontSize(FontSize);
                _footer.AddStyle(code);
                _footer.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                _dupFooter = new Paragraph().SetFontSize(FontSize);
                _dupFooter.AddStyle(code);
                _dupFooter.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("Voucher No: " + VoucherNo + "\n");
                _content.AddTabStops(new TabStop(30));
                _content.Add("  " + "                  Date: " + OnDate.ToString() + "\n");
                _content.AddTabStops(new TabStop(30));

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("Party Name: " + PartyName + "\n");
                _content.Add($"Address: {City}\n");
                if (!string.IsNullOrEmpty(LedgerName))
                    _content.Add($"Ledger: {LedgerName}\n");

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);

                if (Voucher == VoucherType.CashPayment || Voucher == VoucherType.CashReceipt)
                {
                    _content.Add($"Category: {PaymentDetails}\n");//Transcation Mode
                }

                _content.Add($"Particulars: {Particulars} \n\n Amount: Rs. {Amount.ToString("0.##")}");
                _content.AddTabStops(new TabStop(30));

                if (PaymentMode == PaymentMode.Cash)
                {
                    _content.Add(" Paid in cash.");
                }
                else if (PaymentMode == PaymentMode.Card)
                {
                    _content.Add($"Paid by Card. Last Four Digit:{PaymentDetails}");
                }
                else if (PaymentMode == PaymentMode.RTGS || PaymentMode == PaymentMode.NEFT || PaymentMode == PaymentMode.IMPS)
                {
                    _content.Add($"Paid through Bank transfer. Ref Id:{PaymentDetails}");
                }
                else if (PaymentMode == PaymentMode.Cheques)
                {
                    _content.Add($"Paid through Cheque(s). Details: {PaymentDetails}");
                }
                else if (PaymentMode == PaymentMode.UPI)
                {
                    _content.Add($"Paid through UPI/QRCode. Ref Id: {PaymentDetails}");
                }
                else
                {
                    _content.Add($"Payment Mode: {PaymentMode.ToString()} Details: {PaymentDetails}");
                }
                _content.Add("\n");
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add($"Remarks: {Remarks}\n");

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add($"Issued By: {IssuedBy} \n");
                // if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("\n");


                //Footer
                if (!Page2Inch) _footer.Add(DotedLineLong); else _footer.Add(DotedLine);
                _footer.Add(FooterFirstMessage + "\n");
                _footer.Add(DotedLineLong);
                _footer.Add($"For {StoreName}\n\n_______________\n({IssuedBy})\n Signature" + "\n");
                _footer.Add("\n_______________\nParty Signature" + "\n");
                if (!Page2Inch) _footer.Add(DotedLineLong); else _footer.Add(DotedLine);
                _footer.Add("\n");// Just to Check;

                if (Reprint)
                {
                    _footer.Add("(Reprinted Orginal)\n");
                }
                else
                {
                    _footer.Add("(Orignal Copy)\n");
                }

                _footer.Add("Printed on: " + DateTime.Now + "\n\n\n\n\n");
                _footer.Add("\n" + DotedLine + "\n\n\n\n");

                var barcode = Barcodes.QRBarcode.GenerateQRCode(VoucherNo, OnDate, Amount);
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

                if (!Page2Inch) _dupFooter.Add(DotedLineLong); else _dupFooter.Add(DotedLine);
                _dupFooter.Add(FooterFirstMessage + "\n");
                _dupFooter.Add(DotedLineLong);
                _dupFooter.Add($"For {StoreName}\n\n_______________\n({IssuedBy})\n Signature" + "\n");
                _dupFooter.Add("\n_______________\nParty Signature" + "\n");
                if (!Page2Inch) _dupFooter.Add(DotedLineLong); else _dupFooter.Add(DotedLine);
                _dupFooter.Add("\n");// Just to Check;

                if (Reprint)
                {
                    _dupFooter.Add("(Reprinted Duplicate)\n");
                }
                else
                {
                    _dupFooter.Add("(Duplicate Copy)\n");
                }

                _dupFooter.Add("Printed on: " + DateTime.Now + "\n\n\n\n\n");
                _dupFooter.Add("\n" + DotedLine + "\n\n\n\n");


                //Set data first;
                //pdfDoc.Add(ip);
                //pdfDoc.Add(foot);
                // pdfDoc.Close();
                // return FileName;
                return CreateDocument(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}