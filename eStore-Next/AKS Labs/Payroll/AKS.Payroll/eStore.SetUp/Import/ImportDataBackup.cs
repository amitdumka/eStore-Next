namespace eStore.SetUp.Import
{
    public class ImportDataBackup
    {
        public string ZipFilename { get; set; }
        public string Path { get; set; }

        public string ConfigFilename { get; set; }

        private void BackupInvoiceWithItems() { }
        private void BackupSaleInvoiceWithItems() { }
        private void BackupProductItemWithStocks() { }
        private void BackupCategories() { }

    }
}
