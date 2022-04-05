namespace AKS.Payroll.Forms
{
    partial class PdfForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdfForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbPdf = new System.Windows.Forms.GroupBox();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEmail = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbPdf.SuspendLayout();
            this.gbControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbControls);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbPdf);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 115;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // gbPdf
            // 
            this.gbPdf.Controls.Add(this.printPreviewControl1);
            this.gbPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPdf.Location = new System.Drawing.Point(0, 0);
            this.gbPdf.Name = "gbPdf";
            this.gbPdf.Size = new System.Drawing.Size(681, 450);
            this.gbPdf.TabIndex = 0;
            this.gbPdf.TabStop = false;
            this.gbPdf.Text = "Document";
            // 
            // gbControls
            // 
            this.gbControls.Controls.Add(this.btnEmail);
            this.gbControls.Controls.Add(this.btnSave);
            this.gbControls.Controls.Add(this.btnPrint);
            this.gbControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbControls.Location = new System.Drawing.Point(0, 0);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(115, 450);
            this.gbControls.TabIndex = 0;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "Controls";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(12, 35);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(94, 29);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 84);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.Enabled = false;
            this.btnEmail.Location = new System.Drawing.Point(12, 133);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(94, 29);
            this.btnEmail.TabIndex = 2;
            this.btnEmail.Text = "EMail";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl1.Location = new System.Drawing.Point(3, 23);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(675, 424);
            this.printPreviewControl1.TabIndex = 1;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // PdfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "PdfForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PdfForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbPdf.ResumeLayout(false);
            this.gbControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private SplitContainer splitContainer1;
        private GroupBox gbPdf;
        private GroupBox gbControls;
        private Button btnPrint;
        private Button btnSave;
        private Button btnEmail;
        private PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintPreviewControl printPreviewControl1;
        private SaveFileDialog saveFileDialog1;
        private PrintPreviewDialog printPreviewDialog1;
    }
}