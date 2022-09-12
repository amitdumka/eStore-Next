using iText.IO.Image;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;

namespace AKS.Printers.Thermals
{
    public class PayslipPrint : ThermalPrinter
    {
        public bool IsDataSet { get; set; }
        public string PaySlipNo { get; set; }
        public string StaffName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal WorkingDay { get; set; }
        public decimal Present { get; set; }
        public decimal Absent { get; set; }
        public decimal BasicDayRate { get; set; }
        public decimal Incentive { get; set; }
        public decimal WowBill { get; set; }
        public decimal LastPcs { get; set; }

        public decimal LastMonthAdvance { get; set; }
        public decimal SalaryAdvance { get; set; }
        public decimal CurrentMonthSalary { get; set; }
        public decimal NetPayableSalary { get; set; }

        public string PrintPdf(bool duplicate)
        {
            try
            {
                SetPageType(duplicate);
                if (!IsDataSet) return null;
                this.PrintType = PrintType.Payslip;
                if (!FileName.Contains(PathName))
                {
                    FileName = Path.Combine(PathName, FileName);
                }
                this.TitleName = $" \t   Pay   Slip   \t         \nFor Month of {Month}/{Year}";
                SetStoreInfo();
                PathName = $@"d:\{StoreCode}\PaySlips\{StaffName}\{Year}\{Month}\";
                Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
                Style code = new Style();
                PdfFont timesRoman = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                code.SetFont(timesRoman).SetFontSize(FontSize);
                //Details
                _content = new Paragraph().SetFontSize(FontSize);
                _content.AddStyle(code);
                _content.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
                //Footer
                _footer = new Paragraph().SetFontSize(FontSize);
                _footer.AddStyle(code);
                _footer.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                _dupFooter = new Paragraph().SetFontSize(FontSize);
                _dupFooter.AddStyle(code);
                _dupFooter.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("PaySlip No: " + PaySlipNo + "\n");
                _content.AddTabStops(new TabStop(30));
                _content.Add("  " + "           Printed Date: " + DateTime.Now.ToString() + "\n");
                _content.AddTabStops(new TabStop(30));

                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("Employee Name: " + StaffName + "\n");
                _content.Add($"Address: {City}\n");
                _content.AddTabStops(new TabStop(30));
                _content.Add($"Period: {Month} / {Year} \n");
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add($"Working Days: {WorkingDay} \n Basic Rate: Rs. {BasicDayRate.ToString("0.##")}\n");
                _content.Add($"Present: {Present.ToString("0.##")} \t Absent: {Absent.ToString("0.##")}\n");
                if (WowBill > 0 || LastPcs > 0)
                    _content.Add($"WowBill: Rs. {WowBill.ToString("0.##")} \n Last Pcs: Rs. {LastPcs.ToString("0.##")}\n");
                if (Incentive > 0)
                    _content.Add($"Incentive: Rs. {Incentive.ToString("0.##")} \n");
                _content.AddTabStops(new TabStop(30));
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("Deductions:\n");
                _content.Add($"Last Month Advance: Rs. {LastMonthAdvance.ToString("0.##")} \n Salary Adavance: Rs. {SalaryAdvance.ToString("0.##")}\n");
                _content.Add($"Net Deducations:Rs. {(LastMonthAdvance + SalaryAdvance).ToString("0.##")} \n");
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _content.Add("Salary:\n");
                _content.Add($"Net Salary: Rs. {CurrentMonthSalary.ToString("0.##")}\n");
                _content.Add($"Net Payable: Rs. {NetPayableSalary.ToString("0.##")}\n(After deductions)\n");
                
                if (!Page2Inch) _content.Add(DotedLineLong); else _content.Add(DotedLine);
                _footer.Add("************************* \nThis is computer generated payslip\n ");
                _footer.Add(" No Sign is requried\n ************************* \n");
                if (!Page2Inch) _footer.Add(DotedLineLong); else _footer.Add(DotedLine);
                _footer.Add("No of present day also includes half day, paid leave, sick leaves and sunday(s)\n");
                _footer.Add("Deducations are basic and computer generated. In actual can varries based on request.\n");
                _footer.Add("Last month advance is deducted and Salary advance may not be deducted!.\n\n");
                _footer.Add(DotedLineLong);
                _footer.Add($"For {StoreName}\n\n_______________\n(SM/Accounts)\n Signature" + "\n");
                _footer.Add("\n_______________\nEmp Signature" + "\n");
                if (!Page2Inch) _footer.Add(DotedLineLong); else _footer.Add(DotedLine);
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
                var barcode = Barcodes.QRBarcode.GenerateQRCode(PaySlipNo, DateTime.Now, NetPayableSalary);
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
                _dupFooter.Add("**** This is computer generated payslip ****\n ");
                _dupFooter.Add("***** No Sign is requried  ****\n");
                if (!Page2Inch) _dupFooter.Add(DotedLineLong); else _dupFooter.Add(DotedLine);
                _dupFooter.Add("No of present day also includes half day, paid leave, sick leaves and sunday(s)\n");
                _dupFooter.Add("Deducations are basic and computer generated. In actual can varries based on request.\n");
                _dupFooter.Add("Last month advance is deducted and Salary advance may not be deducted!.\n\n");
                _dupFooter.Add(DotedLineLong);
                _dupFooter.Add($"For {StoreName}\n\n_______________\n(SM/Accounts)\n Signature" + "\n");
                _dupFooter.Add("\n_______________\nEmp Signature" + "\n");
                if (!Page2Inch) _dupFooter.Add(DotedLineLong); else _dupFooter.Add(DotedLine);

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
                return CreateDocument(duplicate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}