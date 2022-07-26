using AKS.ParyollSystem;
using AKS.Payroll.Database;
using AKS.Payroll.Forms;
using System.IO;
using Syncfusion.XlsIO;
using AKS.Shared.Commons.Models.Sales;
using System.Data;
using System.Reflection;

namespace AKS.Payroll.Ops
{

    public class EXLS
    {
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        switch (column.ColumnName)
                        {
                            case "Amount":

                            case "CashAmount":
                            case "NonCashAmount":
                                pro.SetValue(obj, decimal.Parse((string)dr[column.ColumnName]), null);
                                break;
                            case "OnDate":
                                pro.SetValue(obj, DateTime.Parse((string)dr[column.ColumnName]), null);
                                break;
                            case "EntryStatus":
                            case "PayMode":
                                pro.SetValue(obj, int.Parse((string)dr[column.ColumnName]), null);
                                break;
                            case "IsDue":
                            case "ManualBill":
                            case "SalesReturn":
                            case "TailoringBill":
                            case "IsReadOnly":
                            case "MarkedDeleted":


                                pro.SetValue(obj, bool.Parse((string)dr[column.ColumnName]), null);
                                break;
                            case "EDCTerminalId":
                                pro.SetValue(obj,null, null);
                                break;
                            default:
                                if (dr[column.ColumnName]!=null)
                                pro.SetValue(obj,dr[column.ColumnName], null);
                                break;
                        }

                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static void UpDateSale(string path)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(path, ExcelOpenType.Automatic);
                IWorksheet worksheet = workbook.Worksheets[0];
                var x = worksheet.ExportDataTable(1, 1, 101, 18, ExcelExportDataTableOptions.ColumnNames);



            }



        }


        public static string Read(string path)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(path, ExcelOpenType.Automatic);

                //workbook.ActiveSheetIndex = 0;
                // workbook.SaveAsJson  ("data.json");
                IWorksheet worksheet = workbook.Worksheets[0];
                //var data=  worksheet.ExportData<DailySale>(1, 1, 101, 18);

                var x = worksheet.ExportDataTable(1, 1, 101, 18, ExcelExportDataTableOptions.ColumnNames);
                var s = x.Rows.Count;
                //DailySale ds =(DailySale) x.Rows[0].;


                List<DailySale> data = new List<DailySale>();
                data = ConvertDataTable<DailySale>(x);
                var oo = data.Count;

                using (var db = new AzurePayrollDbContext())
                {
                    db.DailySales.AddRange(data);
                    int s1 = db.SaveChanges();
                    Console.Write("");
                }
                return "data.json";

                //if (checkBox1.Checked)
                //{
                //    //Saves the workbook to a JSON file, as schema by default
                //    workbook.SaveAsJson("Excel-Workbook-To-JSON-as-schema-default.json");

                //    //Saves the workbook to a JSON file as schema
                //    workbook.SaveAsJson("Excel-Workbook-To-JSON-as-schema.json", true);

                //    //Saves the workbook to a JSON filestream, as schema by default
                //    FileStream stream = new FileStream("Excel-Workbook-To-JSON-filestream-as-schema-default.json", FileMode.Create);
                //    workbook.SaveAsJson(stream);

                //    //Saves the workbook to a JSON filestream as schema
                //    FileStream stream1 = new FileStream("Excel-Workbook-To-JSON-filestream-as-schema.json", FileMode.Create);
                //    workbook.SaveAsJson(stream1, true);
                //}
                //else
                //{
                //    //Saves the workbook to a JSON file without schema
                //    workbook.SaveAsJson("Excel-Workbook-To-JSON-without-schema.json", false);

                //    //Saves the workbook to a JSON filestream without schema
                //    FileStream stream = new FileStream("Excel-Workbook-To-JSON-filestream-without-schema.json", FileMode.Create);
                //    workbook.SaveAsJson(stream, false);
                //}
            }
        }

    }
    public class BasicOperations
    {
        public async void PayrollReport()
        {
            using (var db = new AzurePayrollDbContext())
            {
                PayrollReports pr = new PayrollReports();
                var filename = await pr.PaySlipReportForAllEmpoyeeAsync(db, DateTime.Today.AddMonths(-1));

                PdfForm form = new PdfForm(filename);
                form.Show(); ;
            }
        }
        public async void PrintBankLetterForSalary()
        {
            using (var db = new AzurePayrollDbContext())
            {
                PayrollReports pr = new PayrollReports();
                var filename = await pr.PaySlipReportForAllEmpoyeeAsync(db, DateTime.Today.AddMonths(-1));

                PdfForm form = new PdfForm(filename);
                form.Show(); ;
            }
        }
    }

    public class ImportData
    {
        public static DataTable ReadExcelToDatatable(string filename, int fRow, int fCol, int MaxRow, int MaxCol)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
                IWorksheet worksheet = workbook.Worksheets[0];

                var x = worksheet.ExportDataTable(fRow, fCol, MaxRow, MaxCol, ExcelExportDataTableOptions.ColumnNames);
                var s = x.Rows.Count;
                return x;
            }
        }
    }
}
