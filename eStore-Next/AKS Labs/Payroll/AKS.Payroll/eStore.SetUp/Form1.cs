using System.DirectoryServices.ActiveDirectory;

namespace eStore.SetUp
{
    public partial class Form1 : Form
    {
        string RootPath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select Excel File only..";
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TXTSelectedFile.Text = openFileDialog1.FileName;
                lbSheetNames.DataSource = Import.ImportData.GetSheetNames(openFileDialog1.FileName);
            }
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

        private void button3_Click(object sender, EventArgs e)
        {

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
                   count= LoadFiles(subdirectory, tds);
                   count+= LoadSubDirectories(subdirectory, tds);

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
        }
    }
}