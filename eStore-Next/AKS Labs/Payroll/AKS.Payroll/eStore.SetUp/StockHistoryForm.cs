using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eStore.SetUp
{
    public partial class StockHistoryForm : Form
    {
        public StockHistoryForm()
        {
            InitializeComponent();
        }

        private void StockHistoryForm_Load(object sender, EventArgs e)
        {

        }
    }

    public class StockHistoryManager
    {
        private Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet ExcelSheet;
        public StockHistoryManager() { }
        
        public Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet ReadSourceExcel(string ExcelFileName) {
            if (File.Exists(ExcelFileName))
            {
                if (ExcelSheet == null)
                {
                    ExcelSheet = new Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet();
                    ExcelSheet.Dock = DockStyle.Fill;
                    ExcelSheet.FileName = ExcelFileName;
                    
                }
                ExcelSheet.Open(ExcelFileName);
                return ExcelSheet;
            }
            return null;
        }

        public void WriteTragertExcel() { }
        public void CreateSaleExcel() { }
        public void CreatePurchaseExcel() { }
        public void CreateCurrentStockExcel() { }

        public void ProcessPurchaseHistory() { }
        public void ProcessSaleHistory() { }
        public void ProcessCurrentStockExcel() { }
        public void CalculateProfitLoss() { }
        public void GenerateMissingStock() { }
        public void GenerateStockHistory() { }

    }
}
