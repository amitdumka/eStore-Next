using AKS.Shared.Commons.Ops;
using eStore_MauiLib.Helpers.Interfaces;
using eStore_MauiLib.Helpers;
using eStore_MauiLib.Services.Print;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Storage;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using Path = System.IO.Path;
using eStore_MauiLib.DataModels.Accounting;
using Syncfusion.Pdf.Barcode;

namespace eStore_MauiLib.Printers.Thermals
{
    public abstract class ThermalPrinter
    {
        public PrintType PrintType { get; set; }
        public string StoreCode { get; set; } = "ARD";

        protected int PageWith = 150;
        protected int PageHeight = 1170;
        protected int FontSize = 8;
        protected float MarginTop, MarginBottom, MarginLeft, MarginRight;
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
        
        //Syncfusion Addittion
        protected float Top = 90;
        protected float X = 0, Y = 0;
        protected float LineSpace = 10;
        protected float Margin = 30;
       
        protected PdfStringFormat formatMiddleCenter = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
        protected PdfStringFormat formatMiddleLeft = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
        protected PdfStringFormat formatMiddleRight = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
        protected PdfStringFormat formatMiddleJustify = new PdfStringFormat(PdfTextAlignment.Justify, PdfVerticalAlignment.Middle);
        protected PdfGraphics graphics;
        protected PdfDocument document;
        protected PdfPage page;
        protected PdfMargins pdfMargins;

        //Colors
        protected static PdfColor darkBlue = Color.FromArgb(255, 65, 104, 209);
        protected static PdfBrush darkBlueBrush = new PdfSolidBrush(darkBlue);
        //Create a brush with a white color.
        protected static PdfBrush whiteBrush = new PdfSolidBrush(Color.FromArgb(255, 255, 255, 255));
        protected static PdfBrush blackBrush = new PdfSolidBrush(Color.Black);

        //Fonts
        protected static PdfFont HeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Bold);
        protected static PdfFont RegularFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 9, PdfFontStyle.Regular);
        protected static PdfFont BoldFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 8, PdfFontStyle.Bold);

        protected void SetPageType(bool duplicate)
        {
            if (!Page2Inch)
            {
                PageWith = 240;
                FontSize = 10;
                if (duplicate)
                {
                    PageHeight = 1170 * 2;
                }
            }
            if (Page2Inch)
            { MarginTop = 90; MarginRight = 25; MarginBottom = 90; MarginLeft = 8; }
            else
            { MarginTop = 170; MarginRight = 25; MarginBottom = 90; MarginLeft = 35; }

            X = MarginTop;
            Y = MarginLeft;

            pdfMargins = new PdfMargins();
            pdfMargins.Bottom = MarginBottom;
            pdfMargins.Top = MarginTop;
            pdfMargins.Left = MarginLeft;
            pdfMargins.Right = MarginRight;
            
        }

        protected void GenrateFileName(string number)
        {
            if (string.IsNullOrEmpty(PathName))
            {
                PathName = $@"{StoreCode}\";
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

                    case PrintType.Payslip:
                        PathName = Path.Combine(PathName, "PaySlips");
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

        protected void TitleText()
        {
            AddDotedLine();
            AddRegularText( TitleName,formatMiddleCenter);
            AddDotedLine();
            if (SubTitle)
            {
                AddRegularText($"  {SubTitleName}\n", formatMiddleCenter);
                AddDotedLine();
            }
        }

        protected void HeaderText()
        {
            AddHeaderText(StoreName);
            AddHeaderText(Address);
            AddHeaderText(City);
            AddHeaderText("Ph No: " + Phone);
            AddHeaderText(TaxNo);
        }

        protected void AddHeaderText(string text)
        {
            //Measure the string size using the font.
            SizeF textSize = HeaderFont.MeasureString(text);
            graphics.DrawString(text, HeaderFont, darkBlueBrush, new RectangleF(0, Y, page.Size.Width, textSize.Height + 10), formatMiddleCenter);
            Y += RegularFont.Height + LineSpace;
        }

        protected void AddNormalText(string text)
        {
            //Measure the string size using the font.
            SizeF textSize = BoldFont.MeasureString(text);
            graphics.DrawString(text, BoldFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
            Y += BoldFont.Height + LineSpace;
        }

        protected void AddRegularText(string text)
        {
            graphics.DrawString(text, RegularFont, blackBrush, new PointF(X, Y), formatMiddleCenter);    
            Y += RegularFont.Height + LineSpace;
        }

        protected void AddNormalText(string text, PdfStringFormat format)
        {
            graphics.DrawString(text, BoldFont, blackBrush, new PointF(X, Y), format);
            Y += BoldFont.Height + LineSpace;
            
        }

        protected void AddRegularText(string text, PdfStringFormat format)
        {
            graphics.DrawString(text, RegularFont, blackBrush, new PointF(X, Y), format);
            Y += RegularFont.Height + LineSpace;
        }

        protected void AddDotedLine()
        {
            //if (!Page2Inch) AddNormalText(DotedLineLong,formatMiddleCenter); else AddNormalText(DotedLine, formatMiddleCenter);
            AddLine();
        }
        protected void AddLine()
        {
           
            graphics.DrawRectangle(darkBlueBrush, new RectangleF(0,Y,page.Size.Width,5));
            graphics.DrawLine(PdfPens.Blue, 0, Y+4, page.Size.Width, Y + 2);
            Y += 6;
        }
        protected void AddSpace()
        {
            Y += ((RegularFont.Height + LineSpace) * 3);
        }
        //Abstract Methods
        public abstract MemoryStream PrintPdf(bool duplicate, bool print = false);
        protected abstract void Content();
        protected abstract void Footer();
        protected abstract void DuplicateFooter();
        protected abstract void QRBarcode();
        protected void QRCode(string text)
        {
            //$"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-"
            //Drawing QR Barcode
            PdfQRBarcode barcode = new PdfQRBarcode();
            //Set Error Correction Level
            barcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;
            //Set XDimension
            barcode.XDimension = 3;
            barcode.Text = text;
            SizeF size = new SizeF(10,10);
            barcode.Draw(page, new PointF(X, Y),size);
            Y+=size.Height+LineSpace+Margin;

        }
        //Init 
        protected bool Init(bool duplicate = false)
        {
            try
            {
                SetPageType(duplicate);
                SetStoreInfo();
                document = new PdfDocument();
                //document.PageSettings.Size = PdfPageSize.Note;
                var pSize = new SizeF(PageWith, PageHeight);
                document.PageSettings.Size = pSize;
                document.PageSettings.Margins = pdfMargins;
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;

                //document.PageSettings.Margins;
                page = document.Pages.Add();
                graphics = page.Graphics;
                HeaderText();
                TitleText();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        protected MemoryStream Save(bool print = false)
        {
            using MemoryStream ms = new();
            //Saves the presentation to the memory stream.
            document.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as a file.
            SavePdf(ms, FileName);

            //Print the pdf file
            if (print)
                PrintPdf(ms, FileName);

            return ms;
        }
        public static void SavePdf(MemoryStream memory, string fileName)
        {
            ServiceHelper.GetService<ISave>().SaveAndView(fileName, "application/pdf", memory);
        }

        public static void PrintPdf(MemoryStream memory, string fileName)
        {
            ServiceHelper.GetService<IPrinterService>().Print(memory, fileName);
        }

    }
}