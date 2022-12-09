using eStore.SetUp.Export;
using eStore.SetUp.Import;
using Syncfusion.XlsIO.Parser.Biff_Records;

namespace eStore.SetUp
{
    public partial class MainForm : Form
    {
        private string ExcelFileName = "";
        private Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet ExcelSheet;
        private ImportProcessor ImportProcessor;
        private string RootPath = "d:\\Ard";
        public MainForm()
        {
            InitializeComponent();
            TXTOutputFolder.Text = RootPath;
        }

        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            TreeNode tds = tvFileList.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }

        private async void BTNLoad_ClickAsync(object sender, EventArgs e)
        {
            //TODO:dataGridView1.DataSource = ImportProcessor.LoadJsonFile(CBXOperations.Text);
            //dataGridView1.DataSource = SQLJson.Test();
            SQLJson sQL = new SQLJson();
            var result = await sQL.BackupDatabase();
            if (result == true) MessageBox.Show("Ok"); else MessageBox.Show("Error");
        }

        private async void BTNProcess_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Kindly check properly before going futher, Kinldy  check you have selected correct file from list and correct process. Do You want to continue",
                "Confirm", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            if (ImportProcessor == null) ImportProcessor = new ImportProcessor();

            if (await ImportProcessor.ProcessOperation(TXTStoreCode.Text.Trim(), CBXOperations.Text, lbFileName.Text,TXTSetting.Text, TXTBasePath.Text))
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void BTNReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void BTNSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select Excel File only..";
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TXTSelectedFile.Text = ExcelFileName = openFileDialog1.FileName;
                lbSheetNames.DataSource = Import.ImportData.GetSheetNames(openFileDialog1.FileName);
                lbEvents.Items.Add("Source file open and sheet name list...");
            }
            else
                lbEvents.Items.Add("Source file  could not  open and sheet name listing failed...");
        }

        private void BTNSet_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TXTOutputFolder.Text = folderBrowserDialog1.SelectedPath;
                RootPath = Path.GetDirectoryName(TXTOutputFolder.Text);
                LoadDirectory(folderBrowserDialog1.SelectedPath);
                lbEvents.Items.Add("Output folder set");
                TXTBasePath.Text = folderBrowserDialog1.SelectedPath;
                ImportBasic.InitSettingAsync(TXTOutputFolder.Text, TXTStoreCode.Text);
            }
        }

        private void BTNShowExcel_Click(object sender, EventArgs e)
        {
            if (File.Exists(ExcelFileName))
            {
                if (ExcelSheet == null)
                {
                    ExcelSheet = new Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet();
                    ExcelSheet.Dock = DockStyle.Fill;
                    ExcelSheet.FileName = ExcelFileName;
                    tabPage1.Controls.Add(ExcelSheet);
                }
                ExcelSheet.Open(ExcelFileName);
            }
        }

        private async void BTNToJSON_Click(object sender, EventArgs e)
        {
            if (await ImportProcessor.StartImporting(TXTStoreCode.Text, ExcelFileName, TXTSheetName.Text, (int)NUDCol.Value, (int)NUDRow.Value, (int)NUDMaxRow.Value,
                (int)NUDMaxCol.Value, Path.Combine(TXTOutputFolder.Text, TXTFileName.Text), CBFileType.Text, ImportData.SaleVMT.VOY))
            {
                Reload();
                lbEvents.Items.Add("Json is created");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void lbSheetNames_DoubleClick(object sender, EventArgs e)
        {
            // MessageBox.Show();
            if (ExcelSheet != null)
                ExcelSheet.SetActiveSheet(lbSheetNames.Text);
        }

        private void lbSheetNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            TXTSheetName.Text = lbSheetNames.Text;
        }

        private int LoadFiles(string dir, TreeNode td)
        {
            // Made for JSON file
            string[] Files = Directory.GetFiles(dir, "*.json");
            // Loop through them to see files
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;

                //UpdateProgress();
            }
            return Files.Length;
        }

        private int LoadSubDirectories(string dir, TreeNode td)
        {
            try
            {
                int count = 0;
                // Get all subdirectories
                string[] subdirectoryEntries = Directory.GetDirectories(dir);
                // Loop through them to see if they have any other subdirectories
                foreach (string subdirectory in subdirectoryEntries)
                {
                    DirectoryInfo di = new DirectoryInfo(subdirectory);
                    TreeNode tds = td.Nodes.Add(di.Name);
                    tds.StateImageIndex = 0;
                    tds.Tag = di.FullName;
                    count = LoadFiles(subdirectory, tds);
                    count += LoadSubDirectories(subdirectory, tds);

                    if (count == 0) td.Nodes.Remove(tds);
                }

                return count;
            }
            catch (Exception e)
            {
                return 0;
                Console.WriteLine("error" + e.Message);
            }
        }

        private void ReadSheetName(string filename)
        {
            lbSheetNames.DataSource = Import.ImportData.GetSheetNames(filename);
        }
        private void Reload()
        {
            tvFileList.Nodes.Clear();
            DirectoryInfo di = new DirectoryInfo(TXTOutputFolder.Text);
            TreeNode tds = tvFileList.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(TXTOutputFolder.Text, tds);
            LoadSubDirectories(TXTOutputFolder.Text, tds);
        }

        private void tvFileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(x);
            lbFileName.Text = (Path.Combine(RootPath, e.Node.FullPath));

            if (lbFileName.Text.Contains("Config") && lbFileName.Text.EndsWith(".json"))
            {
                var config = ImportData.ConfigJson(lbFileName.Text);
                // LBKey0.Text = "ConfigFile";
                //.Text = lbFileName.Text;

                tableLayoutPanel1.Controls.Clear();

                int row = 1;
                foreach (var item in config)
                {
                    Label lb = new Label();
                    lb.Text = item.Key;
                    lb.ForeColor = Color.BlueViolet;
                    TextBox tb = new TextBox();
                    tb.Text = item.Value;
                    tb.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(lb, 0, row);
                    tableLayoutPanel1.Controls.Add(tb, 1, row++);
                }
            }
            else if (lbFileName.Text.EndsWith(".json"))
            {
                dataGridView1.DataSource = ImportData.JSONFileToDataTable(lbFileName.Text);
                lbEvents.Items.Add("json file loaded");
                tabControl1.SelectedTab = tabPage2;
               if(ImportBasic.Settings.ContainsValue(lbFileName.Text))
                {
                   var key= ImportBasic.Settings.Where(c=>c.Value==lbFileName.Text).FirstOrDefault().Key;
                    if(key!=null) TXTSetting.Text = key;    
                } else  TXTSetting.Text = "";
            }
        }
    }
}