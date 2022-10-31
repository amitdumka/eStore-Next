using System.Data;

namespace eStore.SetUp.Import
{

    public class ImportProcessor
    {
        public static async Task<bool> StartImporting(string storecode, string filename, string sheetName, int startCol, int startRow, int maxRow, int maxCol, string outputfilename, string fileType, ImportData.SaleVMT vMT)
        {
            var datatable = ImportData.ReadExcelToDatatable(filename, sheetName, startRow, startCol, maxRow, maxCol);
            var fn = Path.Combine(Path.GetDirectoryName(outputfilename) + $@"\{storecode}\ImportedJSON", Path.GetFileName(outputfilename) + ".json");
            ImportBasic.AddOrUpdateSetting(fileType, fn);
            return await ImportData.DataTableToJSONFile(datatable, fn);
        }

        public DataTable LoadJsonFile(string ops)
        {
            DataTable dt = null;
            switch (ops)
            {
                case "Category":
                    //flag = await CreateCategoriesAsync(ImportBasic.GetSetting("Purchase"));
                    break; ;
                case "ProductItem":
                case "PurchaseInvoice":
                    //flag = await GeneratePurchaseInvoice(store, ImportBasic.GetSetting("Purchase"));
                    break;

                case "PurchaseItem":
                //flag = await GeneratePurchaseItemAsync(store, ImportBasic.GetSetting("Purchase")); break;
                case "ToVoyPurchase":
                    dt = ImportData.JSONFileToDataTable(ImportBasic.GetSetting("VoyPurchase")); break;

                case "SaleInvoice":
                case "SaleInvoiceItem":
                case "Stock":
                case "InnerWearPurchase":

                default:
                    break;
            }
            return dt;
        }

        public async Task<bool> ProcessOperation(string store, string ops)
        {
            if (ImportBasic.Settings == null || ImportBasic.Settings.Count <= 0)
                ImportBasic.ReadSetting();
            bool flag = false;
            switch (ops)
            {
                case "ToVoyPurchase": flag = await ToVoyPurchaseAsync(); break;
                case "ToVoySale": flag = await ToVoySale(); break;
                default: break;
            }
            return flag;
        }

        private async Task<bool> ToVoyPurchaseAsync()
        {
            try
            {
                if (ImportBasic.Settings == null || ImportBasic.Settings.Count <= 0)
                    ImportBasic.ReadSetting();

                var datatable = ImportData.JSONFileToDataTable(ImportBasic.GetSetting("Purchase"));
                var json = ImportData.PurchaseDatatableToJson(datatable);
                string filename = Path.Combine(ImportBasic.GetSetting("BasePath"), @"Purchase\VoyPurchase.json");
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                ImportBasic.AddSetting("VoyPurchase", filename);
                File.WriteAllText(filename, json);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> ToVoySale()
        {
            try
            {
                if (ImportBasic.Settings == null || ImportBasic.Settings.Count <= 0)
                    ImportBasic.ReadSetting();

                var datatable = ImportData.JSONFileToDataTable(ImportBasic.GetSetting("Sale"));

                var json = ImportData.SaleDatatableToJSON(datatable, ImportData.SaleVMT.VOY);
                string filename = Path.Combine(ImportBasic.GetSetting("BasePath"), @"Sales\VoySale.json");

                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                ImportBasic.AddSetting("VoySale", filename);
                File.WriteAllText(filename, json);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    internal class InvoiceError
    {
        public List<string> Errors { get; set; }
        public string InvoiceNo { get; set; }
    }
}
