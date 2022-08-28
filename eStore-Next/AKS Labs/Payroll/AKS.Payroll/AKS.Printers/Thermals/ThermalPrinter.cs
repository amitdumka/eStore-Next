using AKS.Shared.Commons.Ops;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

//using PDFtoPrinter;
using Path = System.IO.Path;

namespace AKS.Printers.Thermals
{
    public enum PrintType
    {
        Invoice, PaymentVoucher,
        ReceiptVocuher, CashPaymentVoucher,
        CashReceiptVocucher, Contra, JV, DebitNote, CreditNote,
        Expenses, Note
    }

    public class ThermalPrinter
    {
        public PrintType PrintType { get; set; }
        public string StoreCode { get; set; } = "ARD";
        protected int PageWith = 150;
        protected int PageHeight = 1170;
        protected int FontSize = 8;
        
        public bool Page2Inch { get; set; } = false;
        
        protected const string DotedLine = "---------------------------------\n";
        protected const string DotedLineLong = "--------------------------------------------------\n";

        protected const string FooterFirstMessage = "** Amount Inclusive GST **";

        protected string StoreName { get; set; }
        protected string Address { get; set; }
        protected string City { get; set; }
        protected string Phone { get; set; }
        protected string TaxNo { get; set; }

        public int NoOfCopy { get; set; }
        public bool Reprint { get; set; }

        public string PathName { get; set; }
        public string FileName { get; set; }

        protected string TitleName { get; set; }
        protected bool SubTitle { get; set; }
        protected string SubTitleName { get; set; }

        protected void GenrateFileName(string number)
        {
            if (string.IsNullOrEmpty(PathName))
            {
                PathName = $@"D:\{StoreCode}\";
                switch (PrintType)
                {
                    case PrintType.Invoice:
                        PathName = Path.Combine(PathName, "SaleInvoices");
                        break;

                    case PrintType.PaymentVoucher:
                        PathName = Path.Combine(PathName, "Vouchers\\Payments");
                        break;

                    case PrintType.ReceiptVocuher:
                        PathName = Path.Combine(PathName, "Vouchers\\Receipts");
                        break;

                    case PrintType.CashPaymentVoucher:
                        PathName = Path.Combine(PathName, "Vouchers\\CashPayments");
                        break;

                    case PrintType.CashReceiptVocucher:
                        PathName = Path.Combine(PathName, "Vouchers\\CashReciepts");
                        break;

                    case PrintType.DebitNote:
                        PathName = Path.Combine(PathName, "Vouchers\\DebitNotes");
                        break;

                    case PrintType.CreditNote:
                        PathName = Path.Combine(PathName, "Vouchers\\CreditNotes");
                        break;

                    case PrintType.Expenses:
                        PathName = Path.Combine(PathName, "Vouchers\\ExpensesSlips");
                        break;

                    case PrintType.Note:
                        PathName = Path.Combine(PathName, "Others\\Notes");
                        break;

                    default:
                        PathName = Path.Combine(PathName, "Others\\UnSorted");
                        break;
                }
               // Directory.CreateDirectory(PathName);
                FileName = Path.Combine(PathName, $"{number}.pdf");
                Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
            }
            else
            {
                Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
            }
        }

        protected void SetStoreInfo()
        {
            this.StoreCode = CurrentSession.StoreCode;
            this.Address = CurrentSession.Address;
            this.City = CurrentSession.CityName;
            this.Phone = CurrentSession.PhoneNo;
            this.StoreName = CurrentSession.StoreName;
            this.TaxNo = CurrentSession.TaxNumber;
        }
        protected Document CreateDocument()
        {
            if (!Page2Inch)
            {
                PageWith = 240;
                FontSize = 10;
            }
            PdfWriter pdfWriter = new PdfWriter(FileName);
            PdfDocument pdf = new PdfDocument(pdfWriter);

            Document pdfDoc = new Document(pdf, new PageSize(PageWith, PageHeight));
        
            this.StoreCode = CurrentSession.StoreCode;
            this.Address = CurrentSession.Address;
            this.City = CurrentSession.CityName;
            this.Phone = CurrentSession.PhoneNo;
            this.StoreName = CurrentSession.StoreName;
            this.TaxNo = CurrentSession.TaxNumber;

            if (Page2Inch)
                pdfDoc.SetMargins(90, 25, 90, 8);
            else
                pdfDoc.SetMargins(170, 25, 90, 35);
            Style code = new Style();
            PdfFont timesRoman = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_BOLDITALIC);
            code.SetFont(timesRoman).SetFontSize(FontSize);

            //Header
            Paragraph header = new Paragraph(StoreName + "\n").SetFontSize(FontSize);
            header.AddStyle(code);
            header.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            header.Add(Address + "\n");
            header.Add(City + "\n");
            header.Add("Ph No: " + Phone + "\n");
            header.Add(TaxNo + "\n");
            
            pdfDoc.Add(header);

            Paragraph title = new Paragraph().SetFontSize(FontSize);
            title.AddStyle(code);
            title.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED_ALL);

            if (!Page2Inch) title.Add(DotedLineLong);
            else title.Add(DotedLine);
            
            title.Add("  " + TitleName + "\n");
            
            if (!Page2Inch) title.Add(DotedLineLong);
            else title.Add(DotedLine);
            
            if (SubTitle)
            {
                title.Add($"  {SubTitleName}\n");
                if (!Page2Inch) title.Add(DotedLineLong);
                else title.Add(DotedLine);
            }
            pdfDoc.Add(title);
            return pdfDoc;
        }
    }
}