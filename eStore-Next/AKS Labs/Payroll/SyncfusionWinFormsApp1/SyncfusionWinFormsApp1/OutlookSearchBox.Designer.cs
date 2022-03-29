#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
namespace SyncfusionWinFormsApp1
{
    partial class OutlookSearchBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutlookSearchBox));
            this.searchbuttonEdit = new Syncfusion.Windows.Forms.Tools.ButtonEdit();
            this.searchbuttonEditChildButton = new Syncfusion.Windows.Forms.Tools.ButtonEditChildButton();
            this.searchtextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.comboBoxAdv1 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.sortByDatebuttonEdit = new Syncfusion.Windows.Forms.Tools.ButtonEdit();
            this.sortByDatebuttonEditChildButton = new Syncfusion.Windows.Forms.Tools.ButtonEditChildButton();
            this.orderByNewerbuttonEdit = new Syncfusion.Windows.Forms.Tools.ButtonEdit();
            this.orderByNewerbuttonEditChildButton = new Syncfusion.Windows.Forms.Tools.ButtonEditChildButton();
            this.alllabel = new System.Windows.Forms.Label();
            this.unreadlabel = new System.Windows.Forms.Label();
            this.bannerTextProvider1 = new Syncfusion.Windows.Forms.BannerTextProvider(this.components);
            this.imageListAdv1 = new Syncfusion.Windows.Forms.Tools.ImageListAdv(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.searchbuttonEdit)).BeginInit();
            this.searchbuttonEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchtextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortByDatebuttonEdit)).BeginInit();
            this.sortByDatebuttonEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderByNewerbuttonEdit)).BeginInit();
            this.orderByNewerbuttonEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchbuttonEdit
            // 
            this.searchbuttonEdit.BeforeTouchSize = new System.Drawing.Size(220, 28);
            this.searchbuttonEdit.Buttons.Add(this.searchbuttonEditChildButton);
            this.searchbuttonEdit.ButtonStyle = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.searchbuttonEdit.Controls.Add(this.searchbuttonEditChildButton);
            this.searchbuttonEdit.Controls.Add(this.searchtextBox);
            this.searchbuttonEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbuttonEdit.Location = new System.Drawing.Point(13, 3);
            this.searchbuttonEdit.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.searchbuttonEdit.Name = "searchbuttonEdit";
            this.searchbuttonEdit.Size = new System.Drawing.Size(220, 28);
            this.searchbuttonEdit.TabIndex = 0;
            this.searchbuttonEdit.UseVisualStyle = true;
            // 
            // searchbuttonEditChildButton
            // 
            this.searchbuttonEditChildButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.searchbuttonEditChildButton.BeforeTouchSize = new System.Drawing.Size(18, 24);
            this.searchbuttonEditChildButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.searchbuttonEditChildButton.ComboEditBackColor = System.Drawing.SystemColors.Window;
            this.searchbuttonEditChildButton.ForeColor = System.Drawing.Color.White;
            this.searchbuttonEditChildButton.Image = null;
            this.searchbuttonEditChildButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.searchbuttonEditChildButton.IsBackStageButton = false;
            this.searchbuttonEditChildButton.Name = "searchbuttonEditChildButton";
            this.searchbuttonEditChildButton.PreferredWidth = 18;
            this.searchbuttonEditChildButton.TabIndex = 1;
            // 
            // searchtextBox
            // 
            this.searchtextBox.BeforeTouchSize = new System.Drawing.Size(100, 22);
            this.searchtextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchtextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.searchtextBox.Location = new System.Drawing.Point(2, 4);
            this.searchtextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.searchtextBox.Name = "searchtextBox";
            this.searchtextBox.Size = new System.Drawing.Size(187, 20);
            this.searchtextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.searchtextBox.TabIndex = 0;
            // 
            // comboBoxAdv1
            // 
            this.comboBoxAdv1.BackColor = System.Drawing.Color.White;
            this.comboBoxAdv1.BeforeTouchSize = new System.Drawing.Size(112, 28);
            this.comboBoxAdv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdv1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAdv1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.comboBoxAdv1.Items.AddRange(new object[] {
            "Current Folder",
            "SubFolders",
            "Current MailBox",
            "All MailBoxes",
            "All Outlook Items"});
            this.comboBoxAdv1.Location = new System.Drawing.Point(239, 4);
            this.comboBoxAdv1.Name = "comboBoxAdv1";
            this.comboBoxAdv1.Size = new System.Drawing.Size(112, 28);
            this.comboBoxAdv1.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.comboBoxAdv1.TabIndex = 1;
            this.comboBoxAdv1.Text = "Current Folder";
            // 
            // sortByDatebuttonEdit
            // 
            this.sortByDatebuttonEdit.BeforeTouchSize = new System.Drawing.Size(75, 30);
            this.sortByDatebuttonEdit.Buttons.Add(this.sortByDatebuttonEditChildButton);
            this.sortByDatebuttonEdit.ButtonStyle = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.sortByDatebuttonEdit.Controls.Add(this.sortByDatebuttonEditChildButton);
            this.sortByDatebuttonEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sortByDatebuttonEdit.Location = new System.Drawing.Point(189, 41);
            this.sortByDatebuttonEdit.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.sortByDatebuttonEdit.Name = "sortByDatebuttonEdit";
            this.sortByDatebuttonEdit.Size = new System.Drawing.Size(75, 30);
            this.sortByDatebuttonEdit.TabIndex = 2;
            this.sortByDatebuttonEdit.UseVisualStyle = true;
            // 
            // sortByDatebuttonEditChildButton
            // 
            this.sortByDatebuttonEditChildButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.sortByDatebuttonEditChildButton.BeforeTouchSize = new System.Drawing.Size(18, 26);
            this.sortByDatebuttonEditChildButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.sortByDatebuttonEditChildButton.ComboEditBackColor = System.Drawing.SystemColors.Window;
            this.sortByDatebuttonEditChildButton.ForeColor = System.Drawing.Color.White;
            this.sortByDatebuttonEditChildButton.Image = null;
            this.sortByDatebuttonEditChildButton.IsBackStageButton = false;
            this.sortByDatebuttonEditChildButton.Name = "sortByDatebuttonEditChildButton";
            this.sortByDatebuttonEditChildButton.PreferredWidth = 18;
            this.sortByDatebuttonEditChildButton.TabIndex = 1;
            // 
            // orderByNewerbuttonEdit
            // 
            this.orderByNewerbuttonEdit.BeforeTouchSize = new System.Drawing.Size(73, 30);
            this.orderByNewerbuttonEdit.Buttons.Add(this.orderByNewerbuttonEditChildButton);
            this.orderByNewerbuttonEdit.ButtonStyle = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.orderByNewerbuttonEdit.Controls.Add(this.orderByNewerbuttonEditChildButton);
            this.orderByNewerbuttonEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderByNewerbuttonEdit.Location = new System.Drawing.Point(277, 41);
            this.orderByNewerbuttonEdit.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.orderByNewerbuttonEdit.Name = "orderByNewerbuttonEdit";
            this.orderByNewerbuttonEdit.Size = new System.Drawing.Size(73, 30);
            this.orderByNewerbuttonEdit.TabIndex = 3;
            this.orderByNewerbuttonEdit.UseVisualStyle = true;
            // 
            // orderByNewerbuttonEditChildButton
            // 
            this.orderByNewerbuttonEditChildButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.orderByNewerbuttonEditChildButton.BeforeTouchSize = new System.Drawing.Size(18, 26);
            this.orderByNewerbuttonEditChildButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.orderByNewerbuttonEditChildButton.ComboEditBackColor = System.Drawing.SystemColors.Window;
            this.orderByNewerbuttonEditChildButton.ForeColor = System.Drawing.Color.White;
            this.orderByNewerbuttonEditChildButton.Image = null;
            this.orderByNewerbuttonEditChildButton.IsBackStageButton = false;
            this.orderByNewerbuttonEditChildButton.Name = "orderByNewerbuttonEditChildButton";
            this.orderByNewerbuttonEditChildButton.PreferredWidth = 18;
            this.orderByNewerbuttonEditChildButton.TabIndex = 1;
            // 
            // alllabel
            // 
            this.alllabel.AutoSize = true;
            this.alllabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alllabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.alllabel.Location = new System.Drawing.Point(11, 43);
            this.alllabel.Name = "alllabel";
            this.alllabel.Size = new System.Drawing.Size(34, 25);
            this.alllabel.TabIndex = 4;
            this.alllabel.Text = "All";
            this.alllabel.Click += new System.EventHandler(this.label1_Click);
            this.alllabel.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.alllabel.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            // 
            // unreadlabel
            // 
            this.unreadlabel.AutoSize = true;
            this.unreadlabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unreadlabel.Location = new System.Drawing.Point(44, 43);
            this.unreadlabel.Name = "unreadlabel";
            this.unreadlabel.Size = new System.Drawing.Size(74, 25);
            this.unreadlabel.TabIndex = 5;
            this.unreadlabel.Text = "Unread";
            this.unreadlabel.Click += new System.EventHandler(this.label2_Click);
            this.unreadlabel.MouseEnter += new System.EventHandler(this.label2_MouseEnter);
            this.unreadlabel.MouseLeave += new System.EventHandler(this.label2_MouseLeave);
            // 
            // imageListAdv1
            // 
            this.imageListAdv1.Images.AddRange(new System.Drawing.Image[] {
            ((System.Drawing.Image)(resources.GetObject("imageListAdv1.Images"))),
            ((System.Drawing.Image)(resources.GetObject("imageListAdv1.Images1"))),
            ((System.Drawing.Image)(resources.GetObject("imageListAdv1.Images2")))});
            // 
            // OutlookSearchBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.unreadlabel);
            this.Controls.Add(this.alllabel);
            this.Controls.Add(this.orderByNewerbuttonEdit);
            this.Controls.Add(this.sortByDatebuttonEdit);
            this.Controls.Add(this.comboBoxAdv1);
            this.Controls.Add(this.searchbuttonEdit);
            this.Name = "OutlookSearchBox";
            this.Size = new System.Drawing.Size(363, 75);
            ((System.ComponentModel.ISupportInitialize)(this.searchbuttonEdit)).EndInit();
            this.searchbuttonEdit.ResumeLayout(false);
            this.searchbuttonEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchtextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortByDatebuttonEdit)).EndInit();
            this.sortByDatebuttonEdit.ResumeLayout(false);
            this.sortByDatebuttonEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderByNewerbuttonEdit)).EndInit();
            this.orderByNewerbuttonEdit.ResumeLayout(false);
            this.orderByNewerbuttonEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ButtonEdit searchbuttonEdit;
        private TextBoxExt searchtextBox;
        private Syncfusion.Windows.Forms.Tools.ButtonEditChildButton searchbuttonEditChildButton;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxAdv1;
        private Syncfusion.Windows.Forms.Tools.ButtonEdit sortByDatebuttonEdit;
        private Syncfusion.Windows.Forms.Tools.ButtonEditChildButton sortByDatebuttonEditChildButton;
        private Syncfusion.Windows.Forms.Tools.ButtonEdit orderByNewerbuttonEdit;
        private Syncfusion.Windows.Forms.Tools.ButtonEditChildButton orderByNewerbuttonEditChildButton;
        private System.Windows.Forms.Label alllabel;
        private System.Windows.Forms.Label unreadlabel;
        public Syncfusion.Windows.Forms.BannerTextProvider bannerTextProvider1;
        private ImageListAdv imageListAdv1;
    }
}
