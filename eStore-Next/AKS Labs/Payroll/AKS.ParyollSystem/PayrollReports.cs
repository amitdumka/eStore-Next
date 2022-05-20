using AKS.Libs.Docs.Pdfs;
using AKS.Payroll.Database;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace AKS.ParyollSystem
{
    public class PayrollReports
    {
        public void   AttendanceSheet(AzurePayrollDbContext db, DateTime onDate, string empid)
        {
            if (db == null) db = new AzurePayrollDbContext();
            var list= new PayrollManager().GetMonthlyAttendance(db,empid,onDate);
            if(list!=null && list.Count>0)
            {
                ReportPDFGenerator pdfGen = new ReportPDFGenerator();
                List<Object> pList = new List<Object>();
                var P1 = pdfGen.AddParagraph($"No of Working Days: {DateTime.DaysInMonth(onDate.Year, onDate.Month)}", iText.Layout.Properties.TextAlignment.CENTER, ColorConstants.BLUE);
                
                pList.Add(P1);
                var P2=pdfGen.AddParagraph($"Employee Name:{db.Employees.Find(empid).StaffName}", iText.Layout.Properties.TextAlignment.CENTER, ColorConstants.MAGENTA);
                float[] columnWidths = { 1, 5, 5, 1 };

                Cell[] HeaderCell = new Cell[]{
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("#")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Date").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Status").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Unit").SetTextAlignment(TextAlignment.CENTER)),
                   
            };

                var table = pdfGen.GenerateTable(columnWidths, HeaderCell);


            }
            else
            {

            }
        }
        public void PaySlipReportForEmployee(AzurePayrollDbContext db, DateTime onDate, string empId)
        {
            var emp = db.Employees.Find(empId);

            // TODO: get Salary before hand for multiple month.
            var paySlips = new PayslipManager().GeneratePayslipForEmployee(db, empId, onDate);
        }

        public void PaySlipFinYearReport(AzurePayrollDbContext db, int empId, int SYear, int EYear)
        {

        }

        /// <summary>
        /// Generate Pay Slip for All Emp
        /// </summary>
        /// <param name="db"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public async Task<string> PaySlipReportForAllEmpoyeeAsync(AzurePayrollDbContext db, DateTime onDate)
        {
            ReportPDFGenerator pdfGen = new ReportPDFGenerator();
            List<Object> pList = new List<Object>();
            var P1 = pdfGen.AddParagraph($"No of Working Days: {DateTime.DaysInMonth(onDate.Year, onDate.Month)}", iText.Layout.Properties.TextAlignment.CENTER, ColorConstants.BLUE);
            pList.Add(P1);
            float[] columnWidths = { 1, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 };

            Cell[] HeaderCell = new Cell[]{
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("#")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Staff Name").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Working Days / Count").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("@Salary(PD)").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Attendance").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Absent").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Sunday").SetTextAlignment(TextAlignment.CENTER)),
                new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Weekly").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Salary").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Advance").SetTextAlignment(TextAlignment.CENTER)),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Net Salary").SetTextAlignment(TextAlignment.CENTER)),
            };

            var table = pdfGen.GenerateTable(columnWidths, HeaderCell);

            //TODO: Check for usablilty.
            int count = 0;
            decimal totalPayment = 0;
            bool isValid = true;
            int nDays = DateTime.DaysInMonth(onDate.Year, onDate.Month);

            //Adding Data to Table.
            // Fetching Salary Calculation for the date. 
            var salaries = await new PayslipManager().GetPaySlips(db, onDate);
            foreach (var item in salaries)
            {
                try
                {

                    //var StaffName = item.Key;
                    /// Calculating Total Advance paid in last month
                    var sa = db.SalaryPayment.Where(c => c.EmployeeId == item.Value.EmployeeId && c.SalaryComponet == SalaryComponet.Advance
                        && c.OnDate.Month == onDate.Month && c.OnDate.Year == onDate.Year)
                        .Select(c => c.Amount).Sum();

                    // Calculatig no of Attendance recorded in record.
                    var noa = item.Value.HalfDay + item.Value.Absent + item.Value.PaidLeave + item.Value.Present +
                        item.Value.Sunday;

                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph((++count) + "")));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Key)));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.NoOfWorkingDays.ToString() + "/" + item.Value.NoOfAttendance.ToString())));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.SalaryPerDay.ToString("0.##"))));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.BillableDays.ToString("0.##"))));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.Absent.ToString())));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.Sunday.ToString())));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.BillableDays.ToString())));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Value.GrossSalary.ToString("0.##"))));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(sa.ToString("0.##"))));
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph((item.Value.GrossSalary - sa).ToString("0.##"))));

                    // Total Salary Payable in particulat month
                    totalPayment += (item.Value.GrossSalary - sa);

                    //TODO: salary advance in last month noofattenance.
                    if (nDays != item.Value.NoOfAttendance)
                        isValid = false;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Error=> " + ex.Message);
                    var pError = pdfGen.AddParagraph($"Error=>\t {ex.Message}",
                        iText.Layout.Properties.TextAlignment.CENTER, ColorConstants.RED);
                    pList.Add(pError);
                }
            }
            //Setting up caption.
            var P3 = pdfGen.AddParagraph($"Total Monthly Salary:Rs. {totalPayment} /-",
                iText.Layout.Properties.TextAlignment.CENTER, ColorConstants.BLACK);
            Div d = new Div(); d.Add(P3);
            table.SetCaption(d);

            //Adding table collection pList.
            pList.Add(table);

            if (!isValid)
            {
                // Adding Note If the Calculation detect some calculation mistake due to incorrect data.
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER_OBLIQUE);
                Paragraph pRrr = new Paragraph("\nImportant Note: In one or few or all Employee Salary Calculation is incorrect as No. of Attendance and No. of Days in Month in matching. So which ever Employee's No. of attendance and days not matching, there attendance need to be corrected and again this report need to be generated! \n").SetFontColor(ColorConstants.RED).SetTextAlignment(TextAlignment.CENTER);
                pRrr.SetItalic();
                pRrr.SetFont(font);

                var Br = new SolidBorder(1);
                Br.SetColor(ColorConstants.BLUE);
                pRrr.SetBorder(Br);
                pList.Add(pRrr);
            }

            //Adding Advisory note
            Paragraph P2 = new Paragraph("Note: Salary Advances and any other deducation has not be been considered. That is will be deducated in actuals if applicable").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontColor(ColorConstants.RED);
            pList.Add(P2);

            return pdfGen.CreatePdf("Salary Report", $"Salary Report of Month {onDate.Month}/{onDate.Year}", pList, true);
        }
    }

}
