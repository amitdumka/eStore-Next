﻿using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Accounts;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace AKS.Payroll.Forms
{
    public partial class PettyCashSheetForm : Form
    {
        private AzurePayrollDbContext azureDb;
        private LocalPayrollDbContext localDb;
        private PettyCashSheet pcs;
        private decimal tPay, tRec, tDue, tdRec;
        private string pNar, rNar, dNar, rcNar;
        private bool isNew = false;

        public PettyCashSheetForm()
        {
            InitializeComponent();
        }

        private void PettyCashSheetForm_Load(object sender, EventArgs e)
        {
            azureDb = new AzurePayrollDbContext();
            localDb = new LocalPayrollDbContext();
            pcs = new PettyCashSheet();
            LoadData();
            tRec = tPay = (decimal)0.0;
        }

        private void LoadData()
        {
            Reset();

            cbxStore.DataSource = azureDb.Stores.Select(c => new { c.StoreId, c.StoreName }).ToList();
            cbxStore.DisplayMember = "StoreName";
            cbxStore.ValueMember = "StoreId";
        }

        private void Reset()
        {
            lbPrimaryKey.Text = "";
            lbPayList.Text = "";
            lbPay.Text = "";
            lbRec.Text = "";
            lbRecList.Text = "";
            dtpOnDate.Value = DateTime.Now;
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            tPay = ReadDec(txtAmount);
            pNar += $"#{txtNaration.Text} : {txtAmount.Text}";
            lbPay.Text = lbPay.Text + "\n" + txtNaration.Text;
            lbPayList.Text = lbPayList.Text + "\n" + txtAmount.Text;
            txtAmount.Text = "0";
            txtNaration.Text = "";
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            tRec = ReadDec(txtAmount);
            rNar += $"#{txtNaration.Text} : {txtAmount.Text}";
            lbRec.Text = lbRec.Text + "\n" + txtNaration.Text;
            lbRecList.Text = lbRecList.Text + "\n" + txtAmount.Text;
            txtAmount.Text = "0";
            txtNaration.Text = "";
        }

        private void DisplayData()
        {
        }

        private void ReadData()
        {
            if (isNew)
            {
                pcs = new PettyCashSheet()
                {
                    BankDeposit = ReadDec(txtBankDeposit),
                    BankWithdrawal = ReadDec(txtWithdrawal),
                    CardSale = ReadDec(txtCardSale),
                    ClosingBalance = ReadDec(txtCashInHand),

                    DailySale = ReadDec(txtSale),
                    ManualSale = ReadDec(txtManualSale),
                    OnDate = dtpOnDate.Value,
                    OpeningBalance = ReadDec(txtOpeningBalance),

                    PaymentTotal = tPay,
                    PaymentNaration = pNar,
                    ReceiptsNaration = rNar,
                    ReceiptsTotal = tRec,

                    DueList = dNar,
                    RecoveryList = rcNar,
                    CustomerDue = tDue,
                    CustomerRecovery = tdRec,

                    NonCashSale = ReadDec(txtNonCashSale),
                    TailoringPayment = ReadDec(txtTailoringPayment),
                    TailoringSale = ReadDec(txtTailoring),
                    Id = "",
                };
                //pcs.Id = $"{StoreId}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                pcs.Id = $"ARD/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Millisecond}";
            }
            else
            {
                pcs.BankDeposit = ReadDec(txtBankDeposit);
                pcs.BankWithdrawal = ReadDec(txtWithdrawal);
                pcs.CardSale = ReadDec(txtCardSale);
                pcs.ClosingBalance = ReadDec(txtCashInHand);

                pcs.CustomerDue = tDue;
                pcs.CustomerRecovery = tdRec;

                pcs.DailySale = ReadDec(txtSale);
                pcs.Id = lbPrimaryKey.Text;
                pcs.ManualSale = ReadDec(txtManualSale);
                pcs.OnDate = dtpOnDate.Value;
                pcs.OpeningBalance = ReadDec(txtOpeningBalance);
                pcs.NonCashSale = ReadDec(txtNonCashSale);

                pcs.PaymentTotal = tPay;
                pcs.PaymentNaration = pNar;

                pcs.ReceiptsNaration = rNar;

                pcs.DueList = dNar;

                pcs.RecoveryList = rcNar;
                pcs.ReceiptsTotal = tdRec;

                pcs.TailoringPayment = ReadDec(txtTailoringPayment);
                pcs.TailoringSale = ReadDec(txtTailoring);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                Reset();
                isNew = true;
            }
            else if (btnAdd.Text == "Save")
            {
                try
                {
                    ReadData();
                    if (SaveData())
                    {
                        btnAdd.Text = "Add";
                        MessageBox.Show("Petty Cash Sheet Add!");
                        ViewPdf();
                    }
                    else
                    {
                        MessageBox.Show("Some error occured while saveing, kindly check data and try again!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Some error occured while saveing, kindly check data and try again!");
                }
            }
        }

        public decimal ReadDec(TextBox text)
        {
            return decimal.Parse(text.Text.Trim());
        }

        private void btnRecovery_Click(object sender, EventArgs e)
        {
            tdRec = ReadDec(txtDueAmount);
            rcNar += $"#{txtDueNaration.Text} : {txtDueAmount.Text}";
            lbRec.Text = lbRec.Text + "\n" + txtDueNaration.Text;
            lbRecList.Text = lbRecList.Text + "\n" + txtDueAmount.Text;
            txtDueAmount.Text = "0";
            txtDueNaration.Text = "";

        }

        private void btnDue_Click(object sender, EventArgs e)
        {//TODO: prend
            tDue = ReadDec(txtDueAmount);
            dNar += $"#{txtDueNaration.Text} : {txtDueAmount.Text}";

            lbPay.Text = lbPay.Text + "\n" + txtDueNaration.Text;
            lbPayList.Text = lbPayList.Text + "\n" + txtDueAmount.Text;
            txtDueAmount.Text = "0";
            txtDueNaration.Text = "";

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            pcs = azureDb.PettyCashSheets.Where(c => c.OnDate.Date == DateTime.Today.Date).FirstOrDefault();
            if(pcs!=null)
            ViewPdf();
            else MessageBox.Show("No Record Found");
        }

        private void btnDueRecovery_Click(object sender, EventArgs e)
        {
            //printDialog1 = new PrintDialog();
            //if (printDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    printDialog1.AllowPrintToFile = true;

            //    pdfDocumentView1.Print(printDialog1.PrinterSettings.PrinterName);
            //}
        }

        public int ReadInt(TextBox text)
        {
            return Int32.Parse(text.Text.Trim());
        }

        public bool SaveData()
        {
            if (isNew)
                azureDb.PettyCashSheets.Add(pcs);
            else azureDb.PettyCashSheets.Update(pcs);
            return azureDb.SaveChanges() > 0;
        }
        private async void ViewPdf()
        {
             pdfView.Load(GeneratePdf());
        }

        private string  GeneratePdf()
        {
            try
            {
                string PKey = pcs.Id;
                
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.All = 50;

                //Add a page to the document.
                PdfPage page = document.Pages.Add();
                
                //Create PDF graphics for the page.
                PdfGraphics graphics = page.Graphics;
                
                //Set the standard font.
                PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                
                //Draw the text.
                graphics.DrawString("Petty Cash Sheet", font, PdfBrushes.Red, new PointF(page.Graphics.ClientSize.Width / 2,0 ));

                PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 25));
                PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

                //Draw Rectangle place on location
                graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 40, graphics.ClientSize.Width, 30));
                var element = new PdfTextElement("Aprajita Retails \t" + PKey, subHeadingFont);
                element.Brush = PdfBrushes.White;
                result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));

                string currentDate = "On: " + DateTime.Now.ToString("MM/dd/yyyy");
                SizeF textSize = subHeadingFont.MeasureString(currentDate);
                graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

                //Draw Bill header
                element = new PdfTextElement("Petty Cash Sheet ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
                element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
                result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

                //Draw Bill address
                element = new PdfTextElement(string.Format("{0}, {1}, {2}", $"Date: {pcs.OnDate.ToString("dd/MM/yyyy")} ",
                    $"\t\tSN: {PKey} ", " Dumka, Jharkhand"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
                element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
                result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

                //Draw Bill line
                graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));

                //Create a PdfGrid
                PdfGrid pdfGrid = new PdfGrid();

                //Create a DataTable
                DataTable dataTable = new DataTable();

                int rI = 0, dI = 0;
                //Assign data source
                pdfGrid.DataSource = ToDataTable(out rI,out dI);


                //Creates the grid cell styles
                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = PdfPens.White;
                PdfGridRow header = pdfGrid.Headers[0];
                
                //Creates the header style
                PdfGridCellStyle headerStyle = new PdfGridCellStyle();
                headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
                headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)); ;
                headerStyle.TextBrush = PdfBrushes.White;
                headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Bold);

                //Adds cell customizations
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    if (i == 0 || i == 2)
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                }

                //Applies the header style
                header.ApplyStyle(headerStyle);
                
                cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
                cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
                cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

                //Creates the layout format for grid
                PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

                // Creates layout format settings to allow the table pagination
                layoutFormat.Layout = PdfLayoutType.Paginate;

                
                PdfGridRow lastRow = pdfGrid.Rows[pdfGrid.Rows.Count - 1];
                

                PdfGridCellStyle firstRowStyle = new PdfGridCellStyle();
                firstRowStyle.TextBrush = PdfBrushes.OrangeRed;
                firstRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
                pdfGrid.Rows[0].ApplyStyle(firstRowStyle);

                PdfGridCellStyle totalRowStyle = new PdfGridCellStyle();
                totalRowStyle.TextBrush = PdfBrushes.DarkBlue;
                totalRowStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
                 

                if (dI>0) pdfGrid.Rows[dI].ApplyStyle(totalRowStyle);
                if(rI>0) pdfGrid.Rows[rI].ApplyStyle(totalRowStyle);
                //pdfGrid.Rows[5].ApplyStyle(firstRowStyle);

                PdfGridCellStyle footerStyle = new PdfGridCellStyle();
                footerStyle.Borders.All = new PdfPen(new PdfColor(Color.RebeccaPurple));
                footerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(Color.LightGray)); ;
                footerStyle.TextBrush = PdfBrushes.Red;
                footerStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 13f, PdfFontStyle.Italic);
                lastRow.ApplyStyle(footerStyle);

                //Draws the grid to the PDF page.
                PdfGridLayoutResult gridResult = pdfGrid.Draw(page,
                    new RectangleF(
                        new PointF(0, result.Bounds.Bottom + 40),
                        new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

                //Save the document.
                document.Save("Output.pdf");

                //Close the document.
                document.Close(true);
                return "Output.pdf";

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return null;
            }
          
        }

        private DataTable ToDataTable(out int rI, out int dI)
        {
            try
            {
                DataTable dataTable = new DataTable();
                //Add columns to the DataTable
                dataTable.Columns.Add("Receipt");
                dataTable.Columns.Add("Amount");
                dataTable.Columns.Add("Payment");
                dataTable.Columns.Add("Amounts");

                //Add rows to the DataTable
                dataTable.Rows.Add(new object[] { "Opening Balance", pcs.OpeningBalance, "Closing Balance", pcs.ClosingBalance });
                dataTable.Rows.Add(new object[] { "Sale", pcs.DailySale, "Card Sale", pcs.CardSale });
                dataTable.Rows.Add(new object[] { "Manual Sale", pcs.ManualSale, "Non Cash Sale", pcs.NonCashSale });
                dataTable.Rows.Add(new object[] { "Tailoring", pcs.TailoringSale, "Tailoring Payment", pcs.TailoringPayment });
                dataTable.Rows.Add(new object[] { "Withdrwal", pcs.TailoringSale, "Deposit", pcs.BankDeposit });

                var nar1 = pcs.ReceiptsNaration.Split('#');
                var nar2 = pcs.PaymentNaration.Split('#');

                List<RowData> rows = new List<RowData>();
                int count = nar1.Length > nar2.Length ? nar1.Length : nar2.Length;
                for (int i = 0; i < count; i++)
                    rows.Add(new RowData());

                int rC = 0;
                foreach (var row in nar1)
                {
                    if (!string.IsNullOrEmpty(row.Trim()))
                    {
                        var x = row.Split(":");
                        rows[rC].Name1 = x[0];
                        rows[rC].Value1 = x[1];
                        rC++;
                    }
                    
                }
                rC = 0;
                foreach (var row in nar2)
                {
                    if (!string.IsNullOrEmpty(row.Trim()))
                    {
                        var x = row.Split(":");
                        rows[rC].Name2 = x[0];
                        rows[rC].Value2 = x[1];
                        rC++;
                    }
                }

                nar1 = pcs.RecoveryList.Split('#');
                nar2 = pcs.DueList.Split('#');
               
                List<RowData> cDue = new List<RowData>();
                count = nar1.Length > nar2.Length ? nar1.Length : nar2.Length;
                for (int i = 0; i < count; i++)
                    cDue.Add(new RowData());

                rC = 0;
                foreach (var row in nar1)
                {
                    if (!string.IsNullOrEmpty(row.Trim()))
                    {
                        var x = row.Split(":");
                        cDue[rC].Name1 = x[0];
                        cDue[rC].Value1 = x[1];
                        rC++;
                    }
                }
                rC = 0;
                foreach (var row in nar2)
                {
                    if (!string.IsNullOrEmpty(row.Trim()))
                    {
                        var x = row.Split(":");
                        cDue[rC].Name2 = x[0];
                        cDue[rC].Value2 = x[1];
                        rC++;
                    }
                }
                
                dI = rI=0;

                if (cDue.Count > 0)
                {
                    dI = 5;
                    dataTable.Rows.Add(new object[] { "Customer Recovered", pcs.CustomerRecovery, "Customer Due", pcs.CustomerDue }); }

                foreach (var item in cDue)
                {
                    if (!string.IsNullOrEmpty(item.Name1) || !string.IsNullOrEmpty(item.Name2))
                    { 
                        dataTable.Rows.Add(new object[] { item.Name1, item.Value1, item.Name2, item.Value2 }); 
                        rI++;
                    }
                }

                if (rows.Count > 0)
                {
                    dataTable.Rows.Add(new object[] { "Receipts", pcs.ReceiptsTotal, "Payment", pcs.PaymentTotal });
                    if (dI == 5) rI += dI+1; else rI = 5; 
                }
                foreach (var item in rows)
                {
                    if (!string.IsNullOrEmpty(item.Name1) || !string.IsNullOrEmpty(item.Name2))
                        dataTable.Rows.Add(new object[] { item.Name1, item.Value1, item.Name2, item.Value2 });
                }


                //TODO: need to create total balance.

                var recT = pcs.DailySale + pcs.TailoringSale + pcs.OpeningBalance + pcs.BankWithdrawal + pcs.ReceiptsTotal + pcs.CustomerRecovery + pcs.ManualSale;
                var payT = pcs.ClosingBalance + pcs.CardSale + pcs.NonCashSale + pcs.PaymentTotal + pcs.BankWithdrawal + pcs.CustomerDue + pcs.TailoringPayment;
                dataTable.Rows.Add(new object[] { "   ", "   ", "    ", "         " });
                dataTable.Rows.Add(new object[] { "Total", recT, "Total",payT });

                return dataTable;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                rI = 0;dI = 0;
                return null;
            }

            
        }
    }

    internal class RowData
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}