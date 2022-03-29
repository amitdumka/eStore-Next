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
    public partial class PointerControl : UserControl
    {
        public PointerControl()
        {
            InitializeComponent();
        }

        #region Mouse actions
        /// <summary>
        /// occurs when the mouse enters the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.BackColor = Color.FromArgb(205, 230, 247);
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// occurs when the mouse leaves the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.BackColor = Color.Transparent;
            base.OnMouseLeave(e);
        }
        #endregion
    }
}
