/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */
using AKS.PosSystem.Models.VM;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;
using System.Drawing;

namespace AKS.PosSystem.Helpers
{

    /// <summary>
    /// Sale Helper 
    /// </summary>
    public class SaleHelper
    {
        static string[] Months = {"","Jan","Feb","Mar","April","May","June","July",
        "Aug","Sept","Oct","Nov","Dec"};

        private PdfPage AddPage(PdfPage page, List<SaleReportVM> saleReports)
        {

            PdfGraphics graphics = page.Graphics;
            // PdfPageNumberField pdfPageNumberField = new PdfPageNumberField();
            // pdfPageNumberField.NumberStyle = PdfNumberStyle.Numeric;

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
            element = new PdfTextElement("Complete Sale Report Year Wise ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 10));

            //Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {DateTime.Now.ToString("dd/MM/yyyy")} ",
                $"\t\tStore Address:", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

            //Draw Bill line
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));
            // Adding Table part

            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

            List<int> monthlyTotal = new List<int>();
            List<int> yearlyTotal = new List<int>();

            pdfGrid.DataSource = ToDataTable(saleReports, out yearlyTotal, out monthlyTotal);


            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            pdfGrid.Style.Font = new PdfStandardFont(PdfFontFamily.Courier, 10f, PdfFontStyle.Italic); ;
            //pdfGrid.Style.TextBrush = PdfBrushes.DarkSlateBlue;
            //pdfGrid.Style.BorderOverlapStyle = PdfBorderOverlapStyle.Overlap;
            //pdfGrid.Style.CellPadding.All = 0.7f;


            PdfGridRow header = pdfGrid.Headers[0];

            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)); ;
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 13f, PdfFontStyle.Bold);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                // if (i == 2 || i == 3)
                //  header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                //else
                header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);


            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;


            PdfGridCellStyle firstRowStyle = new PdfGridCellStyle();
            firstRowStyle.TextBrush = PdfBrushes.OrangeRed;
            firstRowStyle.BackgroundBrush = PdfBrushes.Azure;
            firstRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 11f, PdfFontStyle.Bold);
            firstRowStyle.Borders.All = PdfPens.White;// new PdfPen(new PdfColor(126, 151, 173));
            firstRowStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            firstRowStyle.Borders.Right = PdfPens.Black;// new PdfPen(new PdfColor(126, 151, 173));
            firstRowStyle.Borders.Left = PdfPens.Black;// new PdfPen(new PdfColor(126, 151, 173));
            firstRowStyle.Borders.Top = PdfPens.Black;// new PdfPen(new PdfColor(126, 151, 173));
            //firstRowStyle.Borders.Bottom = PdfPens.WhiteSmoke;// new PdfPen(new PdfColor(126, 151, 173));
            pdfGrid.Rows[0].ApplyStyle(firstRowStyle);

            PdfGridCellStyle totalRowStyle = new PdfGridCellStyle();
            totalRowStyle.TextBrush = PdfBrushes.DarkBlue;
            totalRowStyle.BackgroundBrush = PdfBrushes.Azure;
            totalRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 11f, PdfFontStyle.Bold);

            PdfGridCellStyle monthlyRowStyle = new PdfGridCellStyle();
            monthlyRowStyle.TextBrush = PdfBrushes.Blue;
            monthlyRowStyle.BackgroundBrush = PdfBrushes.White;
            monthlyRowStyle.Borders.All = PdfPens.White;
            monthlyRowStyle.Borders.Right = PdfPens.Black;
            monthlyRowStyle.Borders.Left = PdfPens.Black;
            monthlyRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);


            foreach (var row in yearlyTotal)
            {
                pdfGrid.Rows[row].ApplyStyle(totalRowStyle);
            }
            foreach (var row in monthlyTotal)
            {
                pdfGrid.Rows[row].ApplyStyle(monthlyRowStyle);
            }
            // if (dI > 0) pdfGrid.Rows[dI].ApplyStyle(totalRowStyle);
            //if (rI > 0) pdfGrid.Rows[rI].ApplyStyle(totalRowStyle);
            //pdfGrid.Rows[5].ApplyStyle(firstRowStyle);

            PdfGridRow lastRow = pdfGrid.Rows[pdfGrid.Rows.Count - 1];
            PdfGridCellStyle footerStyle = new PdfGridCellStyle();
            footerStyle.Borders.All = new PdfPen(new PdfColor(Color.RebeccaPurple));
            footerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray)); ;
            footerStyle.TextBrush = PdfBrushes.Red;
            footerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Italic);
            lastRow.ApplyStyle(footerStyle);

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = pdfGrid.Draw(page,
                new RectangleF(
                    new PointF(0, result.Bounds.Bottom + 10),
                    new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            //Draw Bill line Page Break Line
            //graphics.DrawLine(new PdfPen(new PdfColor(Color.DarkBlue)), new PointF(0, gridResult.Bounds.Bottom + 20), new PointF(graphics.ClientSize.Width, gridResult.Bounds.Bottom + 20));

            //Draw the text. //TODO: in case of flowing to next page use in section
            //graphics.DrawString("   Sign StoreManager                                                                     Sign Accountant           ",
            //    new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Blue, new PointF(page.Graphics.ClientSize.Width / 8, gridResult.Bounds.Bottom + 130));

            return page;
        }

        private DataTable ToDataTable(List<SaleReportVM> saleReports, out List<int> yearlyTotal, out List<int> monthlyTotal)
        {
            DataTable dataTable = new DataTable();
            yearlyTotal = new List<int>();
            monthlyTotal = new List<int>();
            int RowNo = 0;

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
                dataTable.Columns.Add("Discount Value");
                dataTable.Columns.Add("Tax Value");
                dataTable.Columns.Add("Invoice Value");

                //Do group based on year and month then make sub total . 
                //then year wise total 
                //then total  total
                var reps = saleReports.GroupBy(c => new { c.Year, c.Month }).ToList();

                int curYear = reps[0].Key.Year;
                dataTable.Rows.Add(new object[] {
                            curYear,"", "","","","","","","",""  });
                yearlyTotal.Add(RowNo);
                foreach (var item in reps)
                {
                    if (curYear != item.Key.Year)
                    {
                        var yrData = saleReports.Where(c => c.Year == curYear).
                          GroupBy(c => c.Year)
                         .Select(c => new
                         {
                             c.Key,
                             Tax = c.Sum(x => x.TotalTax),
                             MRP = c.Sum(x => x.TotalMRP),
                             Discount = c.Sum(x => x.TotalDiscount),
                             Qty = c.Sum(x => x.BillQty),
                             Value = c.Sum(x => x.TotalPrice),
                             Free = c.Sum(x => x.FreeQty),
                         })
                          .FirstOrDefault();

                        dataTable.Rows.Add(new object[] {curYear, "Yearly Total", "Sale"," ",
                            yrData.Qty, yrData.Free, yrData.MRP,yrData.Discount, yrData.Tax, yrData.Value    });
                        RowNo++;
                        yearlyTotal.Add(RowNo);
                        // Add a blank if needed
                        curYear = item.Key.Year;
                        dataTable.Rows.Add(new object[] {
                            "","", "","","","","","","",""  });
                        RowNo++;
                        yearlyTotal.Add(RowNo);
                        dataTable.Rows.Add(new object[] {
                            curYear,"", "","","","","","","",""  });
                        RowNo++;
                        yearlyTotal.Add(RowNo);


                    }


                    var datas = saleReports.Where(c => c.Month == item.Key.Month && c.Year == item.Key.Year).ToList();

                    foreach (var sale in datas)
                    {
                        dataTable.Rows.Add(new object[] {
                            " "/*sale.Year*/,"" /*sale.Month*/, sale.InvoiceType,sale.Tailoing?"Service":"Goods",
                            sale.BillQty, sale.FreeQty, sale.TotalMRP,sale.TotalDiscount, sale.TotalTax, sale.TotalPrice });
                        RowNo++;
                    }
                    if (datas.Count() > 0)
                    {
                        dataTable.Rows.Add(new object[] {item.Key.Year, Months[ item.Key.Month], "Monthly Sale"," ",
                            datas.Sum(c=>c.BillQty),  datas.Sum(c=>c.FreeQty),datas.Sum(c=>c.TotalMRP), datas.Sum(c=>c.TotalDiscount),
                             datas.Sum(c=>c.TotalTax),  datas.Sum(c=>c.TotalPrice) });
                        monthlyTotal.Add(++RowNo);
                    }
                    if (reps.IndexOf(item) == reps.Count - 1)
                    {
                        var yrData = saleReports.Where(c => c.Year == curYear).
                          GroupBy(c => c.Year)
                         .Select(c => new
                         {
                             c.Key,
                             Tax = c.Sum(x => x.TotalTax),
                             MRP = c.Sum(x => x.TotalMRP),
                             Discount = c.Sum(x => x.TotalDiscount),
                             Qty = c.Sum(x => x.BillQty),
                             Value = c.Sum(x => x.TotalPrice),
                             Free = c.Sum(x => x.FreeQty),
                         })
                          .FirstOrDefault();

                        dataTable.Rows.Add(new object[] {curYear, "Yearly Total", "Sale"," ",
                            yrData.Qty, yrData.Free, yrData.MRP,yrData.Discount, yrData.Tax, yrData.Value    });
                        RowNo++;
                        yearlyTotal.Add(RowNo);

                    }

                }

                dataTable.Rows.Add(new object[] {
                            " "," ", " "," "," "," "," "," "," "," "  });
                dataTable.Rows.Add(new object[] {" ", "Total", "Sale"," ",
                            saleReports.Sum(x => x.BillQty), saleReports.Sum(x => x.FreeQty), saleReports.Sum(x => x.TotalMRP),saleReports.Sum(x => x.TotalDiscount), saleReports.Sum(x => x.TotalTax), saleReports.Sum(x => x.TotalPrice)    });

                return dataTable;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return dataTable;
            }


        }
        public string ToPdf(List<SaleReportVM> saleReports)
        {
            string pathName = @"d:\apr\salereports";
            string fileName = Path.Combine(pathName, "salereport.pdf");
            Directory.CreateDirectory(pathName);
            try
            {
                PdfDocument document = new PdfDocument();
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;

                document.PageSettings.Margins.All = 50;

                PdfPage pdfPage = document.Pages.Add();

                AddPage(pdfPage, saleReports);
                //Save the document.
                document.Save(fileName);
                //Close the document.
                document.Close(true);
                return fileName;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

}