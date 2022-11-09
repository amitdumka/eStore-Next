using System.Data;

namespace eStore.SetUp.Import
{

    public class ImportProcessor
    {
        /// <summary>
        /// Import from Excel Sheet to JSON File For Raw Processing
        /// </summary>
        /// <param name="storecode"></param>
        /// <param name="filename"></param>
        /// <param name="sheetName"></param>
        /// <param name="startCol"></param>
        /// <param name="startRow"></param>
        /// <param name="maxRow"></param>
        /// <param name="maxCol"></param>
        /// <param name="outputfilename"></param>
        /// <param name="fileType"></param>
        /// <param name="vMT"></param>
        /// <returns></returns>
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

        public async Task<bool> ProcessOperation(string store, string ops, string filename, string setting, string basePath)
        {
            if (ImportBasic.Settings == null || ImportBasic.Settings.Count <= 0)
                ImportBasic.ReadSetting();
            bool flag = false;
            switch (ops)
            {
                case "ToVoyPurchase": flag = await ToVoyPurchaseAsync(filename); break;
                case "ToVoySale": flag = await ToVoySale(filename); break;
                case "Purchase":flag=await new ImportingPurchase().StartImportingPurchase(store, filename, basePath); break;
                case "Sale": new ImportSales().StartImportingSales(store, filename, basePath); break;
                case "PurchaseCleanUp": break;
                case "SaleCleanUp": break;
                default: break;
            }
            return flag;
        }

        private async Task<bool> ToVoyPurchaseAsync(string jsonfilename)
        {
            try
            {
                if (ImportBasic.Settings == null || ImportBasic.Settings.Count <= 0)
                    ImportBasic.ReadSetting();
                var datatable = ImportData.JSONFileToDataTable(jsonfilename);
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

        private async Task<bool> ToVoySale(string jsonfilename)
        {
            try
            {
                if (ImportBasic.Settings == null || ImportBasic.Settings.Count <= 0)
                    ImportBasic.ReadSetting();

                var datatable = ImportData.JSONFileToDataTable(jsonfilename);

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
        private bool BackupImportJson(string path)
        {
            string fn = $@"{ImportBasic.GetSetting("Store")}_ImportedJSON_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.json";
            return ImportBasic.BackupJSon("", path);

        }
    }

    internal class InvoiceError
    {
        public List<string> Errors { get; set; }
        public string InvoiceNo { get; set; }
    }
}
