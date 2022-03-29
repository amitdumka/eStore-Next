#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncfusionWinFormsApp1
{
    public partial class OutlookSearchBox : UserControl
    {
        public OutlookSearchBox()
        {
            InitializeComponent();

            this.sortByDatebuttonEdit.TextBox.Text = "By Date";
            this.orderByNewerbuttonEdit.TextBox.Text = "Newest";
            searchbuttonEditChildButton.Image = this.imageListAdv1.Images[0];
            sortByDatebuttonEditChildButton.Image = this.imageListAdv1.Images[1];
            orderByNewerbuttonEditChildButton.Image = this.imageListAdv1.Images[2];
            this.searchbuttonEdit.TextBox.ForeColor = Color.FromArgb(68, 68, 68);
            this.sortByDatebuttonEdit.TextBox.ForeColor = Color.FromArgb(68, 68, 68);
            this.orderByNewerbuttonEdit.TextBox.ForeColor = Color.FromArgb(68, 68, 68);
            this.searchbuttonEditChildButton.BackColor = Color.White;
            this.sortByDatebuttonEditChildButton.BackColor = Color.White;
            this.orderByNewerbuttonEditChildButton.BackColor = Color.White;
            this.sortByDatebuttonEdit.MetroColor = Color.White;
            this.orderByNewerbuttonEdit.MetroColor = Color.White;
            this.sortByDatebuttonEdit.TextBox.BackColor = Color.White;
            this.orderByNewerbuttonEdit.TextBox.BackColor = Color.White;
            this.orderByNewerbuttonEdit.TextBox.Enabled = false;
            this.sortByDatebuttonEdit.TextBox.Enabled = false;
            this.alllabel.ForeColor = Color.FromArgb(58, 187, 246);
            this.Invalidate();
            this.searchbuttonEdit.TextBox.ForeColor = Color.Gray;
            this.comboBoxAdv1.ForeColor = Color.FromArgb(68, 68, 68);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            using (Graphics g = this.CreateGraphics())
            {
                if (g.DpiX > 96)
                {
                    this.sortByDatebuttonEdit.Location = new Point(this.sortByDatebuttonEdit.Location.X + 40, this.sortByDatebuttonEdit.Location.Y);
                    this.orderByNewerbuttonEdit.Location = new Point(this.orderByNewerbuttonEdit.Location.X + 40, this.orderByNewerbuttonEdit.Location.Y);
                }
            }
            this.searchbuttonEditChildButton.Image = this.imageListAdv1.Images[0];
            this.sortByDatebuttonEditChildButton.Image = this.imageListAdv1.Images[1];
            this.orderByNewerbuttonEditChildButton.Image = this.imageListAdv1.Images[2];
        }

        #region All Label action

        // Search for the string
        private string m_SearchString = string.Empty;
        /// <summary>
        /// Search for the string
        /// </summary>
        public string SearchString
        {
            get { return m_SearchString; }
            set { m_SearchString = value; }
        }


        //Detect the label click
        private bool m_Label1Clicked = false;
        /// <summary>
        /// Detect the label click
        /// </summary>
        public bool Label1Clicked
        {
            get { return m_Label1Clicked; }
            set
            {
                m_Label1Clicked = value;
                alllabel.ForeColor = Color.FromArgb(58, 187, 246);
                unreadlabel.ForeColor = Color.FromArgb(68, 68, 68);
            }
        }
        //Detect the label click
        private bool m_Label2Clicked = false;
        /// <summary>
        /// Detect the label click
        /// </summary>
        public bool Label2Clicked
        {
            get { return m_Label2Clicked; }
            set
            {
                m_Label2Clicked = value;
                alllabel.ForeColor = Color.FromArgb(68, 68, 68);
                unreadlabel.ForeColor = Color.FromArgb(58, 187, 246);
            }
        }


        // All


        private void label1_MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(58, 187, 246);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (!this.Label1Clicked)
                (sender as Label).ForeColor = Color.FromArgb(68, 68, 68);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label2Clicked = false;
            Label1Clicked = true;
        }

        // Unread

        private void label2_Click(object sender, EventArgs e)
        {
            Label1Clicked = false;
            Label2Clicked = true;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(58, 187, 246);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            if (!Label2Clicked)
                (sender as Label).ForeColor = Color.FromArgb(68, 68, 68);
        }
        #endregion
    }
}
