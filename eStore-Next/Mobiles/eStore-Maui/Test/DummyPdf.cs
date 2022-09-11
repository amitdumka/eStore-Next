using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.Reflection;
using System.Xml.Linq;
using Color = Syncfusion.Drawing.Color;
using eStore_Maui.Pages;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using eStore_Maui.Test;
using eStore_MauiLib.Services.Print;

public class DummyPdf
{
    public static void Get()
    {
        RectangleF TotalPriceCellBounds = RectangleF.Empty;
        RectangleF QuantityCellBounds = RectangleF.Empty;

        //Create a new PDF document.
        PdfDocument document = new PdfDocument();
        //Add a page to the document.
        PdfPage page = document.Pages.Add();
        //Create PDF graphics for the page.
        PdfGraphics graphics = page.Graphics;

        //Get the page width and height.
        float pageWidth = page.GetClientSize().Width;
        float pageHeight = page.GetClientSize().Height;

        //Set the header height.
        float headerHeight = 90;
        //Create a brush with a light blue color. 
        PdfColor lightBlue = Color.FromArgb(255, 91, 126, 215);
        PdfBrush lightBlueBrush = new PdfSolidBrush(lightBlue);
        //Create a brush with a dark blue color. 
        PdfColor darkBlue = Color.FromArgb(255, 65, 104, 209);
        PdfBrush darkBlueBrush = new PdfSolidBrush(darkBlue);
        //Create a brush with a white color. 
        PdfBrush whiteBrush = new PdfSolidBrush(Color.FromArgb(255, 255, 255, 255));
        //Get the font file stream from the assembly. 
        // Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
        //string basePath = "eStore_Maui.Resources.Fonts.";
        //Stream fontStream = assembly.GetManifestResourceStream(basePath + "OpenSans-Regular.ttf");

        //Create a PdfTrueTypeFont from the stream with the different sizes. 
        //PdfTrueTypeFont headerFont = new PdfTrueTypeFont(fontStream, 30, PdfFontStyle.Regular);
        //PdfTrueTypeFont arialRegularFont = new PdfTrueTypeFont(fontStream, 18, PdfFontStyle.Regular);
        //PdfTrueTypeFont arialBoldFont = new PdfTrueTypeFont(fontStream, 9, PdfFontStyle.Bold);
       // PdfFont headerFont = new PdfFont(PdfFontFamily.TimesRoman);
        //Create a string format.
        PdfStringFormat format = new PdfStringFormat();
        format.Alignment = PdfTextAlignment.Center;
        format.LineAlignment = PdfVerticalAlignment.Middle;

        float y = 0;
        float x = 0;

        //Set the margins of the address.
        float margin = 30;

        //Set the line space.
        float lineSpace = 10;

        //Create a border pen and draw the border to on the PDF page. 
        PdfColor borderColor = Color.FromArgb(255, 142, 170, 219);
        PdfPen borderPen = new PdfPen(borderColor, 1f);
        graphics.DrawRectangle(borderPen, new RectangleF(0, 0, pageWidth, pageHeight));
        //Fill the header with a light blue brush. 
        graphics.DrawRectangle(lightBlueBrush, new RectangleF(0, 0, pageWidth, headerHeight));

        string title = "INVOICE";

        //Specificy the bounds for the total value. 
        RectangleF headerTotalBounds = new RectangleF(400, 0, pageWidth - 400, headerHeight);

        //Measure the string size using the font. 
        SizeF textSize = headerFont.MeasureString(title);
        graphics.DrawString(title,headerFont, whiteBrush, new RectangleF(0, 0, textSize.Width + 50, headerHeight), format);
        //Draw a rectangle in the PDF page. 
        graphics.DrawRectangle(darkBlueBrush, headerTotalBounds);
        //Draw the total value to the PDF page. 
        graphics.DrawString("$" + "6000", arialRegularFont, whiteBrush, new RectangleF(400, 0, pageWidth - 400, headerHeight + 10), format);
        //Create a font from the font stream. 
        arialRegularFont = new PdfTrueTypeFont(fontStream, 9, PdfFontStyle.Regular);
        //Set the bottom line alignment and draw the text to the PDF page. 
        format.LineAlignment = PdfVerticalAlignment.Bottom;
        graphics.DrawString("Amount", arialRegularFont, whiteBrush, new RectangleF(400, 0, pageWidth - 400, headerHeight / 2 - arialRegularFont.Height), format);
 
        //Measure the string size using the font. 
        SizeF size = arialRegularFont.MeasureString("Invoice Number: 2058557939");
        y = headerHeight + margin;
        x = (pageWidth - margin) - size.Width;
        //Draw text to a PDF page with the provided font and location. 
        graphics.DrawString("Invoice Number: 2058557939", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
        //Measure the string size using the font.
        size = arialRegularFont.MeasureString("Date :" + DateTime.Now.ToString("dddd dd, MMMM yyyy"));
        x = (pageWidth - margin) - size.Width;
        y += arialRegularFont.Height + lineSpace;
        //Draw text to a PDF page with the provided font and location. 
        graphics.DrawString("Date: " + DateTime.Now.ToString("dddd dd, MMMM yyyy"), arialRegularFont, PdfBrushes.Black, new PointF(x, y));

        y = headerHeight + margin;
        x = margin;
        //Draw text to a PDF page with the provided font and location. 
        graphics.DrawString("Bill To:", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
        y += arialRegularFont.Height + lineSpace;
        graphics.DrawString("Abraham Swearegin,", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
        y += arialRegularFont.Height + lineSpace;
        graphics.DrawString("United States, California, San Mateo,", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
        y += arialRegularFont.Height + lineSpace;
        graphics.DrawString("9920 BridgePointe Parkway,", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
        y += arialRegularFont.Height + lineSpace;
        graphics.DrawString("9365550136", arialRegularFont, PdfBrushes.Black, new PointF(x, y));

        using MemoryStream ms = new();
        //Saves the presentation to the memory stream.
        document.Save(ms);
        ms.Position = 0;
        //Saves the memory stream as a file.
       // DependencyService.Get<ISave>().SaveAndView("Invoice.pdf", "application/pdf", ms);

        ServiceHelper.GetService<ISave>().SaveAndView("Invoice.pdf", "application/pdf", ms);
        var printer = ServiceHelper.GetService<IPrinterService>();
        printer.Print(ms, "invoice.pdf");
    }


}
