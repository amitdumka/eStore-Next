using eStore.SetUp.Import;
using System.DirectoryServices.ActiveDirectory;
using System.Xml.Serialization;

namespace eStore.SetUp
{
    public partial class MainForm : Form
    {
        string RootPath = "";
        string ExcelFileName = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

            }

        }

        void ReadSheetName(string filename)
        {
            lbSheetNames.DataSource = Import.ImportData.GetSheetNames(filename);
        }

        private void lbSheetNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            TXTSheetName.Text = lbSheetNames.Text;
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

        private void tvFileList_AfterSelect(object sender, TreeViewEventArgs e)
        {


            //MessageBox.Show(x);
            lbFileName.Text = (Path.Combine(RootPath, e.Node.FullPath));

            if (lbFileName.Text.EndsWith(".json"))
            { 
                dataGridView1.DataSource = ImportData.JSONFileToDataTable(lbFileName.Text);
                lbEvents.Items.Add("json file loaded");
                tabControl1.SelectedTab = tabPage2;
            }

        }

        private void BTNReload_Click(object sender, EventArgs e)
        {
            Reload();
        }
        void Reload()
        {
            tvFileList.Nodes.Clear();
            DirectoryInfo di = new DirectoryInfo(TXTOutputFolder.Text);
            TreeNode tds = tvFileList.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(TXTOutputFolder.Text, tds);
            LoadSubDirectories(TXTOutputFolder.Text, tds);
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

        private void BTNProcess_Click(object sender, EventArgs e)
        {

        }

        private async void BTNToJSON_Click(object sender, EventArgs e)
        {
            if (await ImportProcessor.StartImporting(ExcelFileName, TXTSheetName.Text, (int)NUDCol.Value, (int)NUDRow.Value, (int)NUDMaxRow.Value, (int)NUDMaxCol.Value, Path.Combine(TXTOutputFolder.Text, TXTFileName.Text)))
            {
                Reload();
                lbEvents.Items.Add("Json is created");

            }
        }

        private void lbSheetNames_DoubleClick(object sender, EventArgs e)
        {
            // MessageBox.Show();
            ExcelSheet.SetActiveSheet(lbSheetNames.Text);
        }

        private void BTNShowExcel_Click(object sender, EventArgs e)
        {
            if(File.Exists(ExcelFileName))
                ExcelSheet.Open(ExcelFileName);
        }
    }
}