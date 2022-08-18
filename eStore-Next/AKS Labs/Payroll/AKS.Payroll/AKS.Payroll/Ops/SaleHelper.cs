using AKS.Payroll.Forms.Inventory.Functions;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace AKS.Payroll.Ops
{
    public class SaleHelper
    {
        public void AddPage(PdfPage page)
        {
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
            //Draw the text.
            graphics.DrawString("Sale Report", font, PdfBrushes.Red, new PointF(page.Graphics.ClientSize.Width / 2, 0));

            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 0));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Draw Rectangle place on location
            graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 20, graphics.ClientSize.Width, 30));
            var element = new PdfTextElement("Aprajita Retails \t", subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 28));

            string currentDate = "On: " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));
            //Draw Bill header
            element = new PdfTextElement("Petty Cash Sheet ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 10));

            //Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {DateTime.Now.ToString("dd/MM/yyyy")} ",
                $"\t\tSN: ID_No ", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

            //Draw Bill line
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));
            // Adding Table part

            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

        }

        public DataTable ToDataTable(List<SaleReport> saleReports)
        {
            DataTable dataTable = new DataTable();
            try
            {
                //Add columns to the DataTable
                dataTable.Columns.Add("Year");
                dataTable.Columns.Add("Month");
                dataTable.Columns.Add("Invoice Type");
                dataTable.Columns.Add("Product");
                dataTable.Columns.Add("Bill Qty");
                dataTable.Columns.Add("Free Qty");
                dataTable.Columns.Add("MRP Value");
                dataTable.Columns.Add("Tax Value");
                dataTable.Columns.Add("Invoice Value");

                //Do group based on year and month then make sub total . 
                //then year wise total 
                //then total  total
                foreach (var item in saleReports)
                {
                    dataTable.Rows.Add(new object[] {});
                }


                return dataTable;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return dataTable;
            }
            

        }
        public string ToPdf()
        {
            try
            {
                PdfDocument document = new PdfDocument();
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.All = 50;

                PdfPage pdfPage = document.Pages.Add();

                //Save the document.
                document.Save("Output.pdf");
                //Close the document.
                document.Close(true);
                return "Output.pdf";
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}