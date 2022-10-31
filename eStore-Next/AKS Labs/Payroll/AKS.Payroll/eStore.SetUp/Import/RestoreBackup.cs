namespace eStore.SetUp.Import
{
    public class RestoreBackup
    {
        public string ZipFilename { get; set; }
        public string Path { get; set; }

        public string ConfigFilename { get; set; }

        private void RestoreInvoiceWithItems() { }
        private void RestoreSaleInvoiceWithItems() { }
        private void RestoreProductItemWithStocks() { }
        private void RestoreCategories() { }

    }
}
