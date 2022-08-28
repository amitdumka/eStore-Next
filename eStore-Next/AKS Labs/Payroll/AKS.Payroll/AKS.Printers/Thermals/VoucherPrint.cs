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
                        TitleName =     "RECEIPT VOUCHER";
                        SubTitleName =  "      Cash Voucher     ";
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
                Paragraph ip = new Paragraph().SetFontSize(FontSize);
                ip.AddStyle(code);
                ip.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED_ALL );
                //Footer
                Paragraph foot = new Paragraph().SetFontSize(FontSize);
                foot.AddStyle(code);
                foot.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER );
                

                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add("Voucher No: " + VoucherNo + "\n");
                ip.AddTabStops(new TabStop(30));
                ip.Add("  " + "                  Date: " + OnDate.ToString() + "\n");
                ip.AddTabStops(new TabStop(30));

                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add("Party Name: " + PartyName + "\n");
                ip.Add($"Address: {City}\n");

                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);

                if (Voucher == VoucherType.CashPayment || Voucher == VoucherType.CashReceipt)
                {
                    ip.Add($"Category: {PaymentDetails}\n");//Transcation Mode
                }

                ip.Add($"Particulars: {Particulars} \n\n Amount: Rs. {Amount.ToString("0.##")}");
                ip.AddTabStops(new TabStop(30));

                if (PaymentMode == PaymentMode.Cash)
                {
                    ip.Add(" Paid in cash.");
                }
                else if (PaymentMode == PaymentMode.Card)
                {
                    ip.Add($"Paid by Card. Last Four Digit:{PaymentDetails}");
                }
                else if (PaymentMode == PaymentMode.RTGS || PaymentMode == PaymentMode.NEFT || PaymentMode == PaymentMode.IMPS)
                {
                    ip.Add($"Paid through Bank transfer. Ref Id:{PaymentDetails}");
                }
                else if (PaymentMode == PaymentMode.Cheques)
                {
                    ip.Add($"Paid through Cheque(s). Details: {PaymentDetails}");
                }
                else if (PaymentMode == PaymentMode.UPI)
                {
                    ip.Add($"Paid through UPI/QRCode. Ref Id: {PaymentDetails}");
                }
                else
                {
                    ip.Add($"Payment Mode: {PaymentMode.ToString()} Details: {PaymentDetails}");
                }
                ip.Add("\n");
                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add($"Remarks: {Remarks}\n");

                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add($"Issued By: {IssuedBy} \n");
               // if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add("\n");

                using var pdfDoc = CreateDocument();
                //Footer
                if (!Page2Inch) ip.Add(DotedLineLong); else ip.Add(DotedLine);
                ip.Add(FooterFirstMessage + "\n");
                foot.Add(DotedLineLong);
                foot.Add($"For {StoreName}\n\n_______________\n({IssuedBy})\n Signature" + "\n");
                foot.Add("\n_______________\nParty Signature" + "\n");
                if (!Page2Inch) foot.Add(DotedLineLong); else foot.Add(DotedLine);
                foot.Add("\n");// Just to Check;

                if (Reprint)
                {
                    foot.Add("(Reprinted Duplicate)\n");
                }
                else
                {
                    foot.Add("(Orignal Copy)\n");
                }

                foot.Add("Printed on: " + DateTime.Now + "\n\n\n\n\n");
                foot.Add("\n" + DotedLine + "\n\n\n\n");



                var barcode = Barcodes.QRBarcode.GenerateQRCode(VoucherNo, OnDate, Amount);
                //var barcode = Barcodes.QRBarcode.GenerateBarCode(ProductSale.InvoiceNo);//,ProductSale.OnDate,ProductSale.TotalPrice);
                if (barcode != null)
                {
                    var img = ImageDataFactory.CreatePng(barcode.ToPngBinaryData());
                    var imges = new Image(img);
                    imges.Scale((float)0.1, (float)0.1);
                    imges.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    imges.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    pdfDoc.Add(imges);
                }

                //Set data first;
                pdfDoc.Add(ip);
                pdfDoc.Add(foot);
                pdfDoc.Close();
                return FileName;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}