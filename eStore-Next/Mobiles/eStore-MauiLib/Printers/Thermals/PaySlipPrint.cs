using Syncfusion.Pdf.Barcode;
using System.Diagnostics;

namespace eStore_MauiLib.Printers.Thermals
{
    public class PaySlipPrint : ThermalPrinter
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

        public override MemoryStream PrintPdf(bool duplicate, bool print = false)
        {
            try
            {
                SetPageType(duplicate);
                if (!IsDataSet) return null;

                this.PrintType = PrintType.Payslip;

                //if (!FileName.Contains(PathName))
                //{
                //    FileName = Path.Combine(PathName, FileName);
                //}
                this.TitleName = $"Pay Slip \nFor Month of {Month}/{Year}";
                SetStoreInfo();
                PathName = $@"{StoreCode}/PaySlips/{StaffName}/{Year}/{Month}/";
                //Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
                Init();
                Content();
                Footer();
                AddSpace();
                if (duplicate)
                {
                    AddDotedLine();
                    AddSpace();
                    HeaderText();
                    TitleText();
                    QRBarcode();
                    Content();
                    DuplicateFooter();
                    AddSpace();
                }
                return Save(print);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        protected override void QRBarcode()
        {
            QRCode($"PaySlip:{PaySlipNo} On {DateTime.Now.ToString()} of Rs. {NetPayableSalary}/-");
        }

        protected override void Content()
        {
            AddDotedLine();
            AddNormalText("PaySlip No:\n " + PaySlipNo + "\n");
            //AddNormalTextTabStops(new TabStop(30));
            AddNormalText("Printed Date: \n" + DateTime.Now.ToString() + "\n");
            //AddNormalTextTabStops(new TabStop(30));

            AddDotedLine();
            AddNormalText("Employee Name: \n" + StaffName + "\n");
            AddNormalText($"Address: {City}\n");
            //AddNormalTextTabStops(new TabStop(30));
            AddNormalText($"Period: {Month} / {Year} \n");
            AddDotedLine();
            AddNormalText($"Working Days: {WorkingDay} \n Basic Rate: Rs. {BasicDayRate.ToString("0.##")}\n");
            AddNormalText($"Present: {Present.ToString("0.##")} \t Absent: {Absent.ToString("0.##")}\n");
            if (WowBill > 0 || LastPcs > 0)
                AddNormalText($"WowBill: Rs. {WowBill.ToString("0.##")} \n Last Pcs: Rs. {LastPcs.ToString("0.##")}\n");
            if (Incentive > 0)
                AddNormalText($"Incentive: Rs. {Incentive.ToString("0.##")} \n");
            //AddNormalTextTabStops(new TabStop(30));
            AddDotedLine();
            AddNormalText("Deductions:\n");
            AddNormalText($"Last Month Advance:\n Rs. {LastMonthAdvance.ToString("0.##")} \n Salary Adavance: Rs. {SalaryAdvance.ToString("0.##")}\n");
            AddNormalText($"Net Deducations:\nRs. {(LastMonthAdvance + SalaryAdvance).ToString("0.##")} \n");
            AddDotedLine();
            AddNormalText("Salary:\n");
            AddNormalText($"Net Salary:\n Rs. {CurrentMonthSalary.ToString("0.##")}\n");
            AddNormalText($"Net Payable:\n Rs. {NetPayableSalary.ToString("0.##")}\n(After deductions)\n");
        }

        protected override void Footer()
        {
            AddDotedLine();
            AddRegularText("************************* \nThis is computer generated payslip\n ");
            AddRegularText(" No Sign is requried\n ************************* \n");

            AddDotedLine();

            AddRegularText("No of present day also includes half day, \npaid leave, sick leaves and sunday(s)\n");
            AddRegularText("Deducations are basic and computer generated. \nIn actual can varries based on request.\n");
            AddRegularText("Last month advance is deducted \nand Salary advance may not be deducted!.\n\n");
            AddRegularText(DotedLineLong);
            AddRegularText($"For {StoreName}\n\n_______________\n(SM/Accounts)\n Signature" + "\n");
            AddRegularText("\n_______________\nEmp Signature" + "\n");
            AddDotedLine();
            if (Reprint)
            {
                AddRegularText("(Reprinted Orginal)\n");
            }
            else
            {
                AddRegularText("(Orignal Copy)\n");
            }
            AddRegularText("Printed on: " + DateTime.Now + "\n\n\n\n\n");
            AddRegularText("\n" + DotedLine + "\n\n\n\n");
        }

        protected override void DuplicateFooter()
        {
            AddDotedLine();
            AddRegularText("************************* \nThis is computer generated payslip\n ", formatMiddleCenter);
            AddRegularText(" No Sign is requried\n ************************* \n", formatMiddleCenter);

            AddDotedLine();

            AddRegularText("No of present day also includes half day, paid leave, sick leaves and sunday(s)\n");
            AddRegularText("Deducations are basic and computer generated. In actual can varries based on request.\n");
            AddRegularText("Last month advance is deducted and Salary advance may not be deducted!.\n\n");
            AddRegularText(DotedLineLong);
            AddRegularText($"For {StoreName}\n\n_______________\n(SM/Accounts)\n Signature" + "\n", formatMiddleCenter);
            AddRegularText("\n_______________\nEmp Signature" + "\n", formatMiddleCenter);
            AddDotedLine();
            if (Reprint)
            {
                AddRegularText("(Reprinted Duplicate)\n", formatMiddleCenter);
            }
            else
            {
                AddRegularText("(Duplicate Copy)\n", formatMiddleCenter);
            }
            AddRegularText("Printed on: " + DateTime.Now + "\n\n\n\n\n");
            AddRegularText("\n" + DotedLine + "\n\n\n\n");
        }
    }
}