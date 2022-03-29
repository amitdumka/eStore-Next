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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using System.Collections;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;

namespace SyncfusionWinFormsApp1
{
    public partial class OutlookForm : RibbonForm
    {
        #region Fields
        int mailSplitterDistance = 0;
        int readerSplitterDistance = 0;
        Label lbl;
        Label draftLbl;
        private SuperAccelerator SuperAccelerator1;
        bool isMoving = false;
        bool isUpArrowPressed = false;
        Rectangle screenBounds = new Rectangle();
        private int degreeOfPercentage = 100;
        bool isUnreadClicked = false;
        List<int> category = new List<int>();
        DataSet ds;
        DataTable dt;
        public List<int> unreadMessage = new List<int>();
        int rowIndex = 0;
        Rectangle closeImage = new Rectangle();
        Rectangle categoryRect = new Rectangle();
        int moveRowIndex = 0, moveColIndex = 0;
        int indentRI, indentCI = 0;
        private bool IsColWidthChanged = false;
        int dist = 30;
        int hoveredRowIndex = -1;
        bool emptyData = false;
        bool sentItems = false;
        int selectedRow = 0;
        #endregion

        #region constructor
        public OutlookForm()
        {
            InitializeComponent();
            InitializeUserControl();
            lbl = new Label();
            lbl.Text = "2";
            lbl.ForeColor = Color.DeepSkyBlue;
            draftLbl = new Label();
            draftLbl.Text = "15";
            draftLbl.ForeColor = Color.DeepSkyBlue;
            lbl.AutoSize = true;
            draftLbl.AutoSize = true;
            this.outlookribbonControlAdv.MenuButtonVisible = true;
            this.outlookribbonControlAdv.SelectedTab = this.homeTabItem;
            this.SuperAccelerator();
            using (Graphics g = this.CreateGraphics())
            {
                if (g.DpiX >= 120)
                {
                    this.outlookribbonControlAdv.Size = new System.Drawing.Size(351, 163);
                    this.outlookribbonControlAdv.MinimumSize = new Size(0, 200);
                }
                else
                {
                    this.outlookribbonControlAdv.Size = new System.Drawing.Size(351, 153);
                }
            }
            lbl.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            draftLbl.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            this.foldertreeViewAdv.FullRowSelect = true;
            this.foldertreeViewAdv.SelectedNode = this.foldertreeViewAdv.Nodes[0];
            this.foldertreeViewAdv.SelectedNodeBackground = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(225, 225, 225));
            this.foldertreeViewAdv.SelectedNodeForeColor = Color.Black;
            this.foldertreeViewAdv.MouseUp += new MouseEventHandler(treeViewAdv1_MouseUp);
            this.foldertreeViewAdv.MouseMove += new MouseEventHandler(treeViewAdv1_MouseMove);
            this.foldertreeViewAdv.NodeMouseClick += new TreeNodeAdvMouseClickArgs(treeViewAdv1_NodeMouseClick);
            this.mainsplitContainerAdv.Panel1.Size = new Size(600, this.mainsplitContainerAdv.Panel1.Size.Height);
            this.foldertreeViewAdv.MouseLeave += new EventHandler(treeViewAdv1_MouseLeave);
            #region Grid initialization
            ds = new DataSet();
            ReadXml(ds, "data.xml");
            this.messagelistgridGroupingControl.DataSource = ds.Tables[0];
            this.messagelistgridGroupingControl.TableDescriptor.GroupedColumns.Add("Today");
            this.messagelistgridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowCaption = false;
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("ContactTitle");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Address");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("City");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("PostalCode");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("To");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Greetings");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("ContactName");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("ContactID");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Phone");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("CompanyName");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Message");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Time");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Date");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Day");
            this.messagelistgridGroupingControl.TableDescriptor.VisibleColumns.Remove("Size");

            this.messagelistgridGroupingControl.TableDescriptor.TableOptions.ShowRowHeader = false;
            this.messagelistgridGroupingControl.Table.ExpandAllGroups();
            this.messagelistgridGroupingControl.ChildGroupOptions.CaptionText = this.messagelistgridGroupingControl.TableDescriptor.GroupedColumns[0].Name.ToString();
            this.messagelistgridGroupingControl.ShowRowHeaders = false;
            this.messagelistgridGroupingControl.ShowColumnHeaders = false;
            this.messagelistgridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordAfterDetails = false;
            this.messagelistgridGroupingControl.TableDescriptor.AllowEdit = false;
            this.messagelistgridGroupingControl.TableDescriptor.AllowNew = false;
            this.messagelistgridGroupingControl.TopLevelGroupOptions.ShowCaption = false;
            this.messagelistgridGroupingControl.TableControlCurrentCellStartEditing += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCancelEventHandler(gridGroupingControl1_TableControlCurrentCellStartEditing);
            this.messagelistgridGroupingControl.Table.DefaultRecordRowHeight = 57;
            this.messagelistgridGroupingControl.Table.ExpandAllGroups();
            this.messagelistgridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = Syncfusion.Windows.Forms.Grid.Grouping.GridListBoxSelectionCurrentCellOptions.None;
            this.messagelistgridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            this.messagelistgridGroupingControl.QueryCellStyleInfo += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableCellStyleInfoEventHandler(gridGroupingControl1_QueryCellStyleInfo);
            this.messagelistgridGroupingControl.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.None;
            this.messagelistgridGroupingControl.TableModel.CellModels.Add("OutlookHeaderCell", new OutlookCell.OutlookHeaderCellModel(this.messagelistgridGroupingControl.TableModel));
            this.messagelistgridGroupingControl.TableModel.QueryColWidth += new Syncfusion.Windows.Forms.Grid.GridRowColSizeEventHandler(TableModel_QueryColWidth);
            this.messagelistgridGroupingControl.TableControlCellClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(gridGroupingControl1_TableControlCellClick);
            this.setMessageTextBoxText();
            this.messagelistgridGroupingControl.TableOptions.SelectionBackColor = ColorTranslator.FromHtml("#CDE6F7");
            this.messagelistgridGroupingControl.TableOptions.ListBoxSelectionOutlineBorder = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.Standard);
            this.messagelistgridGroupingControl.TableControlCellMouseHover += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellMouseEventHandler(gridGroupingControl1_TableControlCellMouseHover);
            this.messagelistgridGroupingControl.TableModel.QueryRowHeight += new GridRowColSizeEventHandler(TableModel_QueryRowHeight);
            this.messagelistgridGroupingControl.TableControl.MouseWheel += new MouseEventHandler(TableControl_MouseWheel);
            this.messagelistgridGroupingControl.TableControlDrawCellDisplayText += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlDrawCellDisplayTextEventHandler(gridGroupingControl1_TableControlDrawCellDisplayText);
            this.messagelistgridGroupingControl.TableControl.ScrollbarsVisibleChanged += new EventHandler(TableControl_ScrollbarsVisibleChanged);
            this.messagelistgridGroupingControl.DefaultGridBorderStyle = GridBorderStyle.None;
            this.messagelistgridGroupingControl.BorderStyle = BorderStyle.None;
            this.InnerSplitterContainer.SplitterDistance = this.outlookSearchBox1.Width - 40;
            mailSplitterDistance = this.mainsplitContainerAdv.SplitterDistance;
            readerSplitterDistance = this.InnerSplitterContainer.SplitterDistance;
            this.mainsplitContainerAdv.SplitterMoved += new Syncfusion.Windows.Forms.Tools.Events.SplitterMoveEventHandler(splitContainerAdv1_SplitterMoved);
            this.InnerSplitterContainer.SplitterMoved += new Syncfusion.Windows.Forms.Tools.Events.SplitterMoveEventHandler(InnerSplitterContainer_SplitterMoved);
            this.ribbonstatusStripEx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.messagelistgridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.messagelistgridGroupingControl.TableOptions.SelectionBackColor = Color.White;//ColorTranslator.FromHtml("#CDE6F7");

            this.messagelistgridGroupingControl.Appearance.GroupCaptionPlusMinusCell.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            this.messagelistgridGroupingControl.TableControlCellMouseHoverEnter += new GridTableControlCellMouseEventHandler(gridGroupingControl1_TableControlCellMouseHoverEnter);
            this.messagelistgridGroupingControl.TableControlCellMouseHoverLeave += new GridTableControlCellMouseEventHandler(gridGroupingControl1_TableControlCellMouseHoverLeave);
            this.messagelistgridGroupingControl.TableControl.VScrollPixelPosChanging += new GridScrollPositionChangingEventHandler(TableControl_VScrollPixelPosChanging);
            this.messagelistgridGroupingControl.TableControlCellDrawn += new GridTableControlDrawCellEventHandler(gridGroupingControl1_TableControlCellDrawn);
            dt = ds.Tables[0];
            this.messagelistgridGroupingControl.TableModel.QueryColWidth += new GridRowColSizeEventHandler(TableModel_QueryColWidth);
            this.messagelistgridGroupingControl.TableControlCellMouseHover += new GridTableControlCellMouseEventHandler(gridGroupingControl1_TableControlCellMouseHover);
            this.messagelistgridGroupingControl.TableControl.CellClick += new GridCellClickEventHandler(TableControl_CellClick);
            this.messagelistgridGroupingControl.VisibleChanged += new EventHandler(gridGroupingControl1_VisibleChanged);
            this.messagelistgridGroupingControl.AllowProportionalColumnSizing = true;
            this.messagelistgridGroupingControl.TableControl.Refresh();
            #endregion

            foreach (Control ctrl in this.outlookSearchBox1.Controls)
            {
                if (ctrl is Label)
                {
                    if ((ctrl as Label).Text.ToLower() == "unread")
                    {
                        (ctrl as Label).ForeColor = Color.FromArgb(68, 68, 68);
                    }
                    else if ((ctrl as Label).Text.ToLower() == "all")
                    {
                        (ctrl as Label).ForeColor = Color.FromArgb(58, 187, 246);
                        outlookSearchBox1.Label1Clicked = true;
                    }
                    ((Label)ctrl).Click += new EventHandler(OutlookForm_Click);
                }
            }
            unreadMessage.Add(1);
            unreadMessage.Add(2);
            unreadMessage.Add(3);
            lbl.BackColor = Color.Transparent;
            draftLbl.BackColor = Color.Transparent;
            this.foldertreeViewAdv.Nodes[0].Nodes[0].CustomControl = lbl;
            this.foldertreeViewAdv.BackColor = Color.FromArgb(252, 252, 252);
            this.collapsepanel.BackColor = Color.FromArgb(252, 252, 252);
            this.folderbottompanel.BackColor = Color.FromArgb(252, 252, 252);
            this.mainsplitContainerAdv.BackColor = Color.FromArgb(252, 252, 252);
            this.searchtoolStripTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.searchtoolStripTextBox.MouseDown += new MouseEventHandler(toolStripTextBox1_MouseDown);
            this.searchtoolStripTextBox.LostFocus += new EventHandler(toolStripTextBox1_LostFocus);
            this.searchtoolStripTextBox.Text = "Search";
            this.searchtoolStripTextBox.ForeColor = Color.Gray;
            this.outlookribbonControlAdv.QuickPanelVisible = true;
            this.outlookribbonControlAdv.TouchMode = false;
            this.Padding = new Padding(3);
            this.messagelistgridGroupingControl.TableControl.HScrollBar.Enabled = false;
            this.messagelistgridGroupingControl.TableControl.HScrollBehavior = GridScrollbarMode.Disabled;
            this.messagelistgridGroupingControl.Table.DefaultRecordRowHeight = RowHeightOnScaling();
            this.outlookribbonControlAdv.ShowRibbonDisplayOptionButton = false;
            using (Graphics g = this.CreateGraphics())
            {
                if (g.DpiX >= 120)
                {
                    this.foldertreeViewAdv.Size = new Size(210, 232);
                    this.Size = new Size(700, 500);
                    this.outlookSearchBox1.Controls[4].Width += 20;
                }
            }
            this.outlookribbonControlAdv.RibbonHeaderImage = RibbonHeaderImage.None;
            this.Load += new EventHandler(OutlookForm_Load);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.mainsplitContainerAdv.IsSplitterFixed = true;
            this.InnerSplitterContainer.SplitterDistance = this.outlookSearchBox1.Controls[3].Location.X + this.outlookSearchBox1.Controls[3].Width + 10;
            screenBounds = Screen.PrimaryScreen.Bounds;
            this.messagelistgridGroupingControl.TableControl.VScrollBehavior = GridScrollbarMode.Enabled;
            this.messagelistgridGroupingControl.TableControlCellMouseHoverLeave += new GridTableControlCellMouseEventHandler(gridGroupingControl1_TableControlCellMouseHoverLeave);
            this.messagelistgridGroupingControl.TableControlKeyDown += new GridTableControlKeyEventHandler(gridGroupingControl1_TableControlKeyDown);
            this.messagelistgridGroupingControl.TableControl.CurrentCellMoving += new GridCurrentCellMovingEventHandler(TableControl_CurrentCellMoving);

            this.HiddenPanel.BackgroundImage = this.ImageListAdv1.Images[47];
            this.pointerControl2.BackgroundImage = this.ImageListAdv1.Images[48];
            this.openexportbackStageTab.BackgroundImage = this.ImageListAdv1.Images[49];
            this.openbutton.BackgroundImage = this.ImageListAdv1.Images[50];
            this.otheruserbutton.BackgroundImage = this.ImageListAdv1.Images[51];
            this.importexportbutton.BackgroundImage = this.ImageListAdv1.Images[52];
            this.openoutlookdatafilebutton.BackgroundImage = this.ImageListAdv1.Images[53];
            this.opencalendarbutton.BackgroundImage = this.ImageListAdv1.Images[54];
            this.infobackStageTab.BackgroundImage = this.ImageListAdv1.Images[55];
            this.printbackStageTab.BackgroundImage = this.ImageListAdv1.Images[57];
            this.officeAccountsbackStageTab.BackgroundImage = this.ImageListAdv1.Images[56];
            this.newtoolStripEx.Image = this.ImageListAdv1.Images[0];
            this.respondtoolStripEx.Image = this.ImageListAdv1.Images[8];
            this.movetoolStripEx.Image = this.ImageListAdv1.Images[11];
            this.tagstoolStripEx.Image = this.ImageListAdv1.Images[13];
            this.findtoolStripEx.Image = this.ImageListAdv1.Images[15];
            this.openInNewWindowButton.Image = this.ImageListAdv1.Images[46];
            this.closeAllItemsButton.Image = this.ImageListAdv1.Images[45];
            this.pointerControl1.BackgroundImage = this.ImageListAdv1.Images[58];
            this.pointerControl7.BackgroundImage = this.ImageListAdv1.Images[59];
            this.pointerControl6.BackgroundImage = this.ImageListAdv1.Images[60];
            this.pointerControl5.BackgroundImage = this.ImageListAdv1.Images[62];
            this.pointerControl4.BackgroundImage = this.ImageListAdv1.Images[61];
            this.pointerControl3.BackgroundImage = this.ImageListAdv1.Images[63];
            this.userImage.BackgroundImage = this.ImageListAdv1.Images[64];




            AssignImageforHomeTab();
            AssignImageforSendTab();
            AssignImageforFolderTab();
            AssignImageforViewTab();

        }
        #endregion

        #region Initialize UserControl
        private void InitializeUserControl()
        {
            this.pointerControl2 = new PointerControl();
            this.pointerControl1 = new PointerControl();
            this.pointerControl7 = new PointerControl();
            this.pointerControl6 = new PointerControl();
            this.pointerControl5 = new PointerControl();
            this.pointerControl4 = new PointerControl();
            this.pointerControl3 = new PointerControl();

            // 
            // pointerControl2
            // 
            this.pointerControl2.BackColor = System.Drawing.Color.Transparent;
            this.pointerControl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pointerControl2.Location = new System.Drawing.Point(16, 5);
            this.pointerControl2.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl2.Name = "pointerControl2";
            this.pointerControl2.Size = new System.Drawing.Size(20, 26);
            this.pointerControl2.TabIndex = 5;
            this.pointerControl2.Click += new System.EventHandler(this.pointerControl2_Click);
            // 
            // pointerControl1
            // 
            this.pointerControl1.BackColor = System.Drawing.Color.Transparent;
            this.pointerControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pointerControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pointerControl1.Location = new System.Drawing.Point(249, 0);
            this.pointerControl1.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl1.Name = "pointerControl1";
            this.pointerControl1.Size = new System.Drawing.Size(23, 26);
            this.pointerControl1.TabIndex = 7;
            this.pointerControl1.Click += new System.EventHandler(this.pointerControl1_Click_2);
            // 
            // pointerControl7
            // 
            this.pointerControl7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pointerControl7.Location = new System.Drawing.Point(275, 20);
            this.pointerControl7.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl7.Name = "pointerControl7";
            this.pointerControl7.Size = new System.Drawing.Size(39, 28);
            this.pointerControl7.TabIndex = 4;
            // 
            // pointerControl6
            // 
            this.pointerControl6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pointerControl6.Location = new System.Drawing.Point(217, 21);
            this.pointerControl6.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl6.Name = "pointerControl6";
            this.pointerControl6.Size = new System.Drawing.Size(39, 28);
            this.pointerControl6.TabIndex = 3;
            // 
            // pointerControl5
            // 
            this.pointerControl5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pointerControl5.Location = new System.Drawing.Point(155, 20);
            this.pointerControl5.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl5.Name = "pointerControl5";
            this.pointerControl5.Size = new System.Drawing.Size(39, 28);
            this.pointerControl5.TabIndex = 2;
            // 
            // pointerControl4
            // 
            this.pointerControl4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pointerControl4.Location = new System.Drawing.Point(92, 20);
            this.pointerControl4.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl4.Name = "pointerControl4";
            this.pointerControl4.Size = new System.Drawing.Size(39, 28);
            this.pointerControl4.TabIndex = 1;
            // 
            // pointerControl3
            // 
            this.pointerControl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pointerControl3.Location = new System.Drawing.Point(36, 20);
            this.pointerControl3.Margin = new System.Windows.Forms.Padding(5);
            this.pointerControl3.Name = "pointerControl3";
            this.pointerControl3.Size = new System.Drawing.Size(39, 28);
            this.pointerControl3.TabIndex = 0;

            this.collapsepanel.Controls.Add(this.pointerControl7);
            this.collapsepanel.Controls.Add(this.pointerControl6);
            this.collapsepanel.Controls.Add(this.pointerControl5);
            this.collapsepanel.Controls.Add(this.pointerControl4);
            this.collapsepanel.Controls.Add(this.pointerControl3);
            this.folderbottompanel.Controls.Add(this.pointerControl1);
            this.HiddenPanel.Controls.Add(this.pointerControl2);

            this.outlookSearchBox1 = new OutlookSearchBox();


            // 
            // outlookSearchBox1
            // 
            this.outlookSearchBox1.BackColor = System.Drawing.Color.White;
            this.outlookSearchBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outlookSearchBox1.Label1Clicked = false;
            this.outlookSearchBox1.Label2Clicked = false;
            this.outlookSearchBox1.Location = new System.Drawing.Point(0, 0);
            this.outlookSearchBox1.Margin = new System.Windows.Forms.Padding(5);
            this.outlookSearchBox1.Name = "outlookSearchBox1";
            this.outlookSearchBox1.SearchString = "";
            this.outlookSearchBox1.Size = new System.Drawing.Size(410, 81);
            this.outlookSearchBox1.TabIndex = 2;


            this.searchtextboxpanel.Controls.Add(this.outlookSearchBox1);
        }
        #endregion

        #region Images for tabs
        private void AssignImageforHomeTab()
        {
            this.newmail.Image = this.ImageListAdv1.Images[0];
            this.newmailitems.Image = this.ImageListAdv1.Images[1];
            this.IgnoreButton.Image = this.ImageListAdv1.Images[2];
            this.CleanUpSplitButton.Image = this.ImageListAdv1.Images[3];
            this.deleteButton.Image = this.ImageListAdv1.Images[5];
            this.JunkSplitButton.Image = this.ImageListAdv1.Images[4];
            this.deleteHomeButton.Image = this.ImageListAdv1.Images[5];
            this.replybutton.Image = this.ImageListAdv1.Images[6];
            this.replyall.Image = this.ImageListAdv1.Images[7];
            this.forward.Image = this.ImageListAdv1.Images[8];
            this.MeetingButton.Image = this.ImageListAdv1.Images[9];
            this.MoreButton.Image = this.ImageListAdv1.Images[10];
            this.movetn.Image = this.ImageListAdv1.Images[11];
            this.rules.Image = this.ImageListAdv1.Images[12];
            this.followupsptbtn.Image = this.ImageListAdv1.Images[13];
            this.readunread.Image = this.ImageListAdv1.Images[14];
            this.categorize.Image = this.ImageListAdv1.Images[15];
            this.AddressBookButton.Image = this.ImageListAdv1.Images[16];
            this.FilterEmailButton.Image = this.ImageListAdv1.Images[17];
        }
        private void AssignImageforSendTab()
        {
            this.sendreceiveButton.Image = this.ImageListAdv1.Images[18];
            this.UpdateFolderButton.Image = this.ImageListAdv1.Images[20];
            this.SendAllButton.Image = this.ImageListAdv1.Images[19];
            this.SendReceiveGroupsSplit.Image = this.ImageListAdv1.Images[21];
            this.cancelAllButton.Image = this.ImageListAdv1.Images[23];
            this.showprogressButton.Image = this.ImageListAdv1.Images[22];
        }

        private void AssignImageforFolderTab()
        {
            this.newFolderButton.Image = this.ImageListAdv1.Images[24];
            this.newsearchButton.Image = this.ImageListAdv1.Images[25];
            this.renametoolStripTabItem.Image = this.ImageListAdv1.Images[26];
            this.copyfolderButton.Image = this.ImageListAdv1.Images[12];
            this.moveFolderButton.Image = this.ImageListAdv1.Images[11];
            this.deleteButton.Image = this.ImageListAdv1.Images[5];
            this.markAllAsReadButton.Image = this.ImageListAdv1.Images[14];
            this.runrulesnowButton.Image = this.ImageListAdv1.Images[27];
            this.showallButton.Image = this.ImageListAdv1.Images[28];
            this.cleanUpFolderButton14.Image = this.ImageListAdv1.Images[29];
            this.deleteAllButton.Image = this.ImageListAdv1.Images[30];
            this.recoverDeletedItemsSplitButton.Image = this.ImageListAdv1.Images[31];
            this.showInFavouritesButton.Image = this.ImageListAdv1.Images[32];
            this.viewOnServerButton.Image = this.ImageListAdv1.Images[33];
            this.autoarchieveSettingsButton.Image = this.ImageListAdv1.Images[34];
            this.folderpermissionButton.Image = this.ImageListAdv1.Images[35];
            this.folderpropertiesButton.Image = this.ImageListAdv1.Images[36];


        }
        private void AssignImageforViewTab()
        {
            this.changeViewSplitButton.Image = this.ImageListAdv1.Images[37];
            this.viewsettingsButton.Image = this.ImageListAdv1.Images[38];
            this.resetViewButton.Image = this.ImageListAdv1.Images[39];
            this.folderpaneSplitButton.Image = this.ImageListAdv1.Images[42];
            this.readingPaneSplitButton.Image = this.ImageListAdv1.Images[40];
            this.toDoBarSplitButton.Image = this.ImageListAdv1.Images[41];
            this.peoplePaneSplitButton.Image = this.ImageListAdv1.Images[43];
            this.remainderwindowButton.Image = this.ImageListAdv1.Images[44];
            this.openInNewWindowButton.Image = this.ImageListAdv1.Images[46];
            this.closeAllItemsButton.Image = this.ImageListAdv1.Images[45];
        }
        #endregion

        #region SuperAccelerator
        private void SuperAccelerator()
        {
            //SuperAccelerator
            this.SuperAccelerator1 = new SuperAccelerator(this);
            this.SuperAccelerator1.Appearance = Syncfusion.Windows.Forms.Tools.Appearance.Advanced;
            this.SuperAccelerator1.BackColor = Color.Black;
            this.SuperAccelerator1.ForeColor = Color.White;
            this.outlookribbonControlAdv.SuperAccelerator = this.SuperAccelerator1;
            this.SuperAccelerator1.SetAccelerator(homeTabItem, "F");
            this.SuperAccelerator1.SetMenuButtonAccelerator(this.outlookribbonControlAdv, "F");
            this.SuperAccelerator1.SetAccelerator(this.homeTabItem, "H");
            this.SuperAccelerator1.SetAccelerator(this.newmail, "N");
            this.SuperAccelerator1.SetAccelerator(this.newmailitems, "I");
            this.SuperAccelerator1.SetAccelerator(this.IgnoreButton, "X");
            this.SuperAccelerator1.SetAccelerator(this.CleanUpSplitButton, "C");
            this.SuperAccelerator1.SetAccelerator(this.JunkSplitButton, "J");
            this.SuperAccelerator1.SetAccelerator(this.deleteHomeButton, "D");
            this.SuperAccelerator1.SetAccelerator(this.replybutton, "RP");
            this.SuperAccelerator1.SetAccelerator(this.replyall, "RA");
            this.SuperAccelerator1.SetAccelerator(this.forward, "FW");
            this.SuperAccelerator1.SetAccelerator(this.MeetingButton, "MR");
            this.SuperAccelerator1.SetAccelerator(this.MoreButton, "ME");
            this.SuperAccelerator1.SetAccelerator(this.movetn, "MV");
            this.SuperAccelerator1.SetAccelerator(this.rules, "RR");
            this.SuperAccelerator1.SetAccelerator(this.readunread, "W");
            this.SuperAccelerator1.SetAccelerator(this.categorize, "G");
            this.SuperAccelerator1.SetAccelerator(this.viewOnServerButton, "V");
            this.SuperAccelerator1.SetAccelerator(this.searchtoolStripTextBox, "FC");
            this.SuperAccelerator1.SetAccelerator(this.AddressBookButton, "AB");
            this.SuperAccelerator1.SetAccelerator(this.FilterEmailButton, "L");
            this.SuperAccelerator1.SetAccelerator(this.sendreceiveTabItem, "S");
            this.SuperAccelerator1.SetAccelerator(this.sendreceiveButton, "S");
            this.SuperAccelerator1.SetAccelerator(this.UpdateFolderButton, "D");
            this.SuperAccelerator1.SetAccelerator(this.SendAllButton, "A");
            this.SuperAccelerator1.SetAccelerator(this.SendReceiveGroupsSplit, "G");
            this.SuperAccelerator1.SetAccelerator(this.showprogressButton, "P");
            this.SuperAccelerator1.SetAccelerator(this.cancelAllButton, "C");
            this.SuperAccelerator1.SetAccelerator(this.followupsptbtn, "U");
            this.SuperAccelerator1.SetAccelerator(this.sendreceiveTabItem, "S");
            this.SuperAccelerator1.SetAccelerator(this.FolderTabItem, "O");
            this.SuperAccelerator1.SetAccelerator(this.newFolderButton, "N");
            this.SuperAccelerator1.SetAccelerator(this.newsearchButton, "SF");
            this.SuperAccelerator1.SetAccelerator(this.renametoolStripTabItem, "RN");
            this.SuperAccelerator1.SetAccelerator(this.copyfolderButton, "CF");
            this.SuperAccelerator1.SetAccelerator(this.moveFolderButton, "MV");
            this.SuperAccelerator1.SetAccelerator(this.deleteButton, "DF");
            this.SuperAccelerator1.SetAccelerator(this.markAllAsReadButton, "MA");
            this.SuperAccelerator1.SetAccelerator(this.runrulesnowButton, "RR");
            this.SuperAccelerator1.SetAccelerator(this.showallButton, "H");
            this.SuperAccelerator1.SetAccelerator(this.cleanUpFolderButton14, "CU");
            this.SuperAccelerator1.SetAccelerator(this.deleteAllButton, "DA");
            this.SuperAccelerator1.SetAccelerator(this.recoverDeletedItemsSplitButton, "RD");
            this.SuperAccelerator1.SetAccelerator(this.showInFavouritesButton, "FA");
            this.SuperAccelerator1.SetAccelerator(this.viewOnServerButton, "V");
            this.SuperAccelerator1.SetAccelerator(this.autoarchieveSettingsButton, "A");
            this.SuperAccelerator1.SetAccelerator(this.folderpermissionButton, "PP");
            this.SuperAccelerator1.SetAccelerator(this.folderpropertiesButton, "FP");
            this.SuperAccelerator1.SetAccelerator(this.viewTabItem, "V");
            this.SuperAccelerator1.SetAccelerator(this.changeViewSplitButton, "CV");
            this.SuperAccelerator1.SetAccelerator(this.viewsettingsButton, "V");
            this.SuperAccelerator1.SetAccelerator(this.resetViewButton, "X");
            this.SuperAccelerator1.SetAccelerator(this.closeAllItemsButton, "CA");
            this.SuperAccelerator1.SetAccelerator(this.showConversationCheckBox, "GC");
            this.SuperAccelerator1.SetAccelerator(this.conversationSettingsSplitButton, "CS");
            this.SuperAccelerator1.SetAccelerator(this.folderpaneSplitButton, "F");
            this.SuperAccelerator1.SetAccelerator(this.readingPaneSplitButton, "PN");
            this.SuperAccelerator1.SetAccelerator(this.toDoBarSplitButton, "B");
            this.SuperAccelerator1.SetAccelerator(this.peoplePaneSplitButton, "PP");
            this.SuperAccelerator1.SetAccelerator(this.remainderwindowButton, "M");
            this.SuperAccelerator1.SetAccelerator(this.openInNewWindowButton, "ON");
            this.SuperAccelerator1.SetAccelerator(this.openexportbackStageTab, "O");
            this.SuperAccelerator1.SetAccelerator(this.infobackStageTab, "I");
            this.SuperAccelerator1.SetAccelerator(this.saveAsbackStageTab, "A");
            this.SuperAccelerator1.SetAccelerator(this.saveAttachmentsbackStageTab, "M");
            this.SuperAccelerator1.SetAccelerator(this.printbackStageTab, "P");
            this.SuperAccelerator1.SetAccelerator(this.officeAccountsbackStageTab, "D");
            this.SuperAccelerator1.SetAccelerator(this.optionsbackStageTab, "X");
            this.SuperAccelerator1.SetAccelerator(this.exitbackStageButton, "X");

            ToolStripButton btn = new ToolStripButton(this.ImageListAdv1.Images[0]);
            this.SuperAccelerator1.SetAccelerator(btn, "1");
            this.outlookribbonControlAdv.Header.AddQuickItem(btn);
            ToolStripButton btn1 = new ToolStripButton(this.ImageListAdv1.Images[5]);
            this.SuperAccelerator1.SetAccelerator(btn1, "2");
            this.outlookribbonControlAdv.Header.AddQuickItem(btn1);
        }
        #endregion

        #region GridControl actions
        void TableControl_CurrentCellMoving(object sender, GridCurrentCellMovingEventArgs e)
        {
            GridCurrentCell cc = this.messagelistgridGroupingControl.TableControl.GetNestedCurrentCell();
            GridTableCellStyleInfo tableStyle = this.messagelistgridGroupingControl.TableControl.GetTableViewStyleInfo(cc.RowIndex, cc.ColIndex);
            if (tableStyle != null)
            {
                if (cc.ColIndex == 0)
                {
                    if (isUpArrowPressed)
                    {
                        GridTableCellStyleInfo nextCellStyle = this.messagelistgridGroupingControl.TableControl.GetTableViewStyleInfo(cc.RowIndex - 1, cc.ColIndex);
                        if (nextCellStyle.TableCellIdentity.TableCellType == GridTableCellType.GroupCaptionRowHeaderCell && isMoving)
                        {
                            isMoving = false;
                            e.Cancel = true;
                            if (cc.RowIndex - 2 >= 1)
                                cc.MoveTo(cc.RowIndex - 2, cc.ColIndex);
                        }
                    }
                }
            }
        }


        void gridGroupingControl1_TableControlKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            isMoving = true;
            isUpArrowPressed = false;
            if (e.Inner.KeyCode == Keys.Up)
                isUpArrowPressed = true;
        }
        /// <summary>
        /// When DPI is greater than 100 then the DefaultRowHeight will be set based on the font size.
        /// </summary>
        /// <returns>The Height Value</returns>
        internal int RowHeightOnScaling()
        {
            if (this.messagelistgridGroupingControl.TableModel.ActiveGridView != null)
                using (Graphics graph = this.messagelistgridGroupingControl.TableModel.ActiveGridView.CreateGraphics())
                {
                    if (graph.DpiY > 100)
                    {
                        degreeOfPercentage = (int)graph.DpiY - 100;
                        return 60;
                    }
                }
            return this.messagelistgridGroupingControl.Table.DefaultRecordRowHeight;
        }
        void gridGroupingControl1_VisibleChanged(object sender, EventArgs e)
        {
            this.messagelistgridGroupingControl.Invalidate(true);
        }
        void TableControl_CellClick(object sender, GridCellClickEventArgs e)
        {
            Element el = this.messagelistgridGroupingControl.Table.GetInnerMostCurrentElement();
            GridTable table;
            if (el != null)
            {
                table = el.ParentTable as GridTable;
                GridTableControl tableControl = this.messagelistgridGroupingControl.GetTableControl(table.TableDescriptor.Name);
                GridCurrentCell cc = tableControl.CurrentCell;
                GridTableCellStyleInfo style = table.GetTableCellStyle(cc.RowIndex, cc.ColIndex);
                GridRecord rec = el as GridRecord;
            }
            if (this.messagelistgridGroupingControl.TableControl.Table.CurrentRecord != null)
            {
                selectedRow = this.messagelistgridGroupingControl.TableControl.Table.CurrentRecord.Id;
            }
            GridTableCellStyleInfo styleInfo = this.messagelistgridGroupingControl.TableModel[e.RowIndex, e.ColIndex];
            int rowIndex = e.RowIndex;
            int colIndex = e.ColIndex;
            Rectangle rect = this.messagelistgridGroupingControl.TableControl.RangeInfoToRectangle(GridRangeInfo.Cell(rowIndex, colIndex));
            rect.Width = 50;
            if (unreadMessage != null)
            {
                if (unreadMessage.Contains(rowIndex))
                {
                    unreadMessage.Remove(rowIndex);
                    if (el != null)
                    {
                        table = el.ParentTable as GridTable;
                        if (table.CurrentRecord != null)
                        {
                            int temp = el.ParentGroup.Records.Count;
                            for (int i = 0; i < el.ParentGroup.Records.Count; i++)
                            {
                                if (el.ParentGroup.Records[i] == table.CurrentRecord)
                                {
                                    temp = i;
                                    break;
                                }
                            }
                            int ids = table.CurrentRecord.GetRowIndex() - temp - 1;
                            if (unreadMessage.Contains(ids))
                                unreadMessage.Remove(ids);
                        }
                    }
                    int rest = 0;
                    if (int.TryParse(this.lbl.Text, out rest))
                    {
                        lbl.Text = (int.Parse(this.lbl.Text) - 1).ToString();
                        if (lbl.Text == "0")
                            lbl.Text = "";
                    }
                }
            }
            if (rect.Contains(e.MouseEventArgs.Location))
            {
                if (unreadMessage.Contains(rowIndex))
                {
                    unreadMessage.Remove(rowIndex);
                    lbl.Text = (int.Parse(this.lbl.Text) - 1).ToString();
                    if (lbl.Text == "0")
                        lbl.Text = string.Empty;
                }
                else
                {
                    unreadMessage.Add(rowIndex);
                    if (this.messagelistgridGroupingControl.TableControl.Table.CurrentRecord != null)
                    {
                        if (el != null)
                        {
                            table = el.ParentTable as GridTable;
                            if (table.CurrentRecord != null)
                            {
                                int temp = el.ParentGroup.Records.Count;
                                for (int i = 0; i < el.ParentGroup.Records.Count; i++)
                                {
                                    if (el.ParentGroup.Records[i] == table.CurrentRecord)
                                    {
                                        temp = i;
                                        break;
                                    }
                                }
                                int ids = table.CurrentRecord.GetRowIndex() - temp - 1;
                                unreadMessage.Add(ids);
                            }
                        }
                    }
                    string text = this.lbl.Text;
                    if (text == string.Empty)
                        text = "0";
                    lbl.Text = (int.Parse(text) + 1).ToString();
                }
            }
            if (styleInfo.CellType == "OutlookHeaderCell")
            {
                if (categoryRect.X < e.MouseEventArgs.Location.X && (categoryRect.X + categoryRect.Width) > e.MouseEventArgs.Location.X)
                {
                    if (category.Contains(rowIndex))
                    {
                        category.Remove(rowIndex);
                    }
                    else
                    {
                        category.Add(rowIndex);
                    }
                }
                if (closeImage.X < e.MouseEventArgs.Location.X && (closeImage.X + closeImage.Width) > e.MouseEventArgs.Location.X)
                {
                    if (this.messagelistgridGroupingControl.TableControl.Table.CurrentRecord != null)
                        this.messagelistgridGroupingControl.TableControl.Table.CurrentRecord.Delete();
                    this.MessageRichTextBox.Clear();
                }
            }
            if (isUnreadClicked)
                this.messagelistgridGroupingControl.TableControl.Refresh();
        }
        void gridGroupingControl1_TableControlCellDrawn(object sender, GridTableControlDrawCellEventArgs e)
        {
            int ht = 20;
            if (degreeOfPercentage != 100)
                ht = ht + ht * degreeOfPercentage / 100 + 2;
            if (e.Inner.Style.CellType == "OutlookHeaderCell")
            {
                rowIndex = e.Inner.RowIndex;
                Graphics g = e.Inner.Graphics;
                Rectangle clRect = e.Inner.Bounds;
                int result = 0;
                if (int.TryParse(e.Inner.Style.ValueMember, out result))
                {
                    if (!unreadMessage.Contains(rowIndex))
                        unreadMessage.Add(rowIndex);
                }

                Rectangle firstDrawString = new Rectangle(clRect.X + 2, clRect.Y + 1, clRect.Width - 110, ht);
                Rectangle secondDrawString = new Rectangle(clRect.X + 2, clRect.Y + 22, clRect.Width - 110, ht - 4);
                Rectangle thirdDrawString = new Rectangle(clRect.X + 2, clRect.Y + 38, clRect.Width - 110, ht - 6);
                Rectangle fourthDrawString = new Rectangle(clRect.Width - 90, clRect.Y + 22, 100, ht - 6);
                Rectangle fifthDrawString = new Rectangle();
                Font firstFont;
                Font secondFont;
                Font thirdFont;
                Font fourthFont;
                using (Graphics g1 = Graphics.FromImage(new Bitmap(10, 10)))
                {
                    if (g1.DpiX > 120)
                    {
                        firstFont = new Font("Segoe UI", 7.5f);
                        secondFont = new Font("Segoe UI", 6f);
                        thirdFont = new Font("Segoe UI", 6f);
                        fourthFont = new Font("Segoe UI", 6f);
                    }
                    else
                    {
                        firstFont = new Font("Segoe UI", 11.5f);
                        secondFont = new Font("Segoe UI", 9f);
                        thirdFont = new Font("Segoe UI", 8f);
                        fourthFont = new Font("Segoe UI", 8f);
                    }
                }
                SolidBrush firstBrush = new SolidBrush(ColorTranslator.FromHtml("#0E0E0E"));
                SolidBrush secondBrush = new SolidBrush(ColorTranslator.FromHtml("#0E0E0E"));
                SolidBrush thirdBrush = new SolidBrush(ColorTranslator.FromHtml("#0E0E0E"));
                SolidBrush fourthBrush = new SolidBrush(ColorTranslator.FromHtml("#0E0E0E"));
                string firstString = "Customer Support";
                string secondString = "Please schedule the meeting on tomorrow";
                string thirdString = "<http.customersupport.com>";
                string fourthString = "11.58 AM";
                closeImage = new Rectangle(clRect.X + clRect.Width - 25, clRect.Y + 20, 20, 20);

                if (e.Inner.RowIndex < dt.Rows.Count + 2)
                {
                    firstString = dt.Rows[e.Inner.RowIndex - 2].ItemArray[2].ToString();
                    secondString = dt.Rows[e.Inner.RowIndex - 2].ItemArray[3].ToString();
                    thirdString = dt.Rows[e.Inner.RowIndex - 2].ItemArray[5].ToString();
                    if (char.IsNumber(dt.Rows[e.Inner.RowIndex - 2].ItemArray[12].ToString(), 0))
                    {
                        fourthString = "    " + dt.Rows[e.Inner.RowIndex - 2].ItemArray[12].ToString();
                    }
                    else
                        fourthString = dt.Rows[e.Inner.RowIndex - 2].ItemArray[12].ToString();
                }

                if (e.TableControl.Table.CurrentRecord != null)
                {
                    if (e.TableControl.Table.CurrentRecord.GetRowIndex() == e.Inner.RowIndex)
                    {
                        Rectangle paintRect = new Rectangle(e.Inner.Bounds.X, e.Inner.Bounds.Y + 1, e.Inner.Bounds.Width, e.Inner.Bounds.Height - 2);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(205, 230, 247)), paintRect);
                    }
                }

                if (unreadMessage.Contains(rowIndex))
                {
                    using (Graphics g1 = Graphics.FromImage(new Bitmap(10, 10)))
                    {
                        if (g1.DpiX > 120)
                        {
                            firstFont = new Font("Segoe UI", 7.55f);
                            secondFont = new Font("Segoe UI", 6f, FontStyle.Bold);// Semilight
                            fourthFont = new Font("Segoe UI", 6f, FontStyle.Bold);
                        }
                        else
                        {
                            firstFont = new Font("Segoe UI", 11.55f);
                            secondFont = new Font("Segoe UI", 9f, FontStyle.Bold);// Semilight
                            fourthFont = new Font("Segoe UI", 9f, FontStyle.Bold);
                        }
                    }
                    firstBrush = new SolidBrush(Color.Black);
                    secondBrush = new SolidBrush(ColorTranslator.FromHtml("#006FC4"));
                    g.FillRectangle(secondBrush, clRect.X, clRect.Y + 1, 3, clRect.Height - 2);
                    if (IsColWidthChanged)
                    {
                        firstBrush = new SolidBrush(ColorTranslator.FromHtml("#006FC4"));
                    }
                }
                if (IsColWidthChanged && screenBounds.Width < 1500)
                {
                    firstDrawString = new Rectangle(clRect.X + 2, clRect.Y + 6, clRect.Width * 20 / 100, ht);
                    secondDrawString = new Rectangle(clRect.Width * 20 / 100 + 12, clRect.Y + 6, clRect.Width * 30 / 100, ht);
                    thirdDrawString = new Rectangle(clRect.X + 2, clRect.Y + 28, clRect.Width * 80 / 100, ht - 2);
                    fourthDrawString = new Rectangle(clRect.Width * 50 / 100 + 12, clRect.Y + 6, clRect.Width * 20 / 100, ht - 6);
                    fifthDrawString = new Rectangle(clRect.Width * 70 / 100 + 12, clRect.Y + 6, clRect.Width * 10 / 100, ht - 4);
                    using (Graphics g1 = Graphics.FromImage(new Bitmap(10, 10)))
                    {
                        if (g1.DpiX > 120)
                        {
                            firstFont = new Font("Segoe UI", 6.25f);
                            secondFont = new Font("Segoe UI", 6.25f);
                            thirdFont = new Font("Segoe UI", 6.25f);
                            fourthFont = new Font("Segoe UI", 6.25f);
                        }
                        else
                        {
                            firstFont = new Font("Segoe UI", 9.25f);
                            secondFont = new Font("Segoe UI", 9.25f);
                            thirdFont = new Font("Segoe UI", 9.25f);
                            fourthFont = new Font("Segoe UI", 9.25f);
                        }
                    }
                    if (unreadMessage.Contains(rowIndex))
                    {
                        using (Graphics g1 = Graphics.FromImage(new Bitmap(10, 10)))
                        {
                            if (g1.DpiX > 120)
                            {
                                firstFont = new Font("Segoe UI", 6.25f, FontStyle.Bold);
                                secondFont = new Font("Segoe UI", 6.25f, FontStyle.Bold);
                                fourthFont = new Font("Segoe UI", 6.25f, FontStyle.Bold);
                            }
                            else
                            {
                                firstFont = new Font("Segoe UI", 9.25f, FontStyle.Bold);
                                secondFont = new Font("Segoe UI", 9.25f, FontStyle.Bold);
                                fourthFont = new Font("Segoe UI", 9.25f, FontStyle.Bold);
                            }
                        }
                    }
                    string fifthString = "54KB";
                    if (e.Inner.RowIndex < dt.Rows.Count + 2)
                        fifthString = dt.Rows[e.Inner.RowIndex - 2].ItemArray[15].ToString();
                    g.DrawString(fifthString, fourthFont, secondBrush, fifthDrawString);
                    categoryRect = new Rectangle(clRect.Width * 80 / 100 + 6, clRect.Y + 6, 8, ht - 6);
                    g.DrawRectangle(new Pen(Color.LightGray), categoryRect);
                    if (category.Contains(rowIndex))
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(124, 206, 110)), categoryRect);
                        Rectangle sixthDrawString = new Rectangle(clRect.Width * 80 / 100 + 6 + categoryRect.Width, clRect.Y + 6, 90, ht - 6);
                        g.DrawRectangle(new Pen(Color.FromArgb(57, 125, 42)), categoryRect);
                        string sixthString = "Green..";
                        g.DrawString(sixthString, firstFont, firstBrush, sixthDrawString);
                    }
                    //
                }
                g.DrawString(firstString, firstFont, firstBrush, firstDrawString);
                g.DrawString(secondString, secondFont, secondBrush, secondDrawString);
                g.DrawString(thirdString, thirdFont, thirdBrush, thirdDrawString);
                g.DrawString(fourthString, fourthFont, secondBrush, fourthDrawString);
            }
        }

        void TableControl_VScrollPixelPosChanging(object sender, GridScrollPositionChangingEventArgs e)
        {
            this.messagelistgridGroupingControl.TableControl.Refresh();
        }
        void gridGroupingControl1_TableControlDrawCellDisplayText(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlDrawCellDisplayTextEventArgs e)
        {
            if (this.messagelistgridGroupingControl.TableModel[e.Inner.RowIndex, e.Inner.ColIndex].CellType == "IndentCell")
            {
                using (Graphics g = this.messagelistgridGroupingControl.TableControl.CreateGridGraphics())
                {
                }
            }
        }

        void TableControl_ScrollbarsVisibleChanged(object sender, EventArgs e)
        {
            this.messagelistgridGroupingControl.TableModel.Refresh();
        }

        void TableControl_MouseWheel(object sender, MouseEventArgs e)
        {
            this.messagelistgridGroupingControl.TableModel.Refresh();
            this.messagelistgridGroupingControl.Refresh();
        }

        void TableModel_QueryRowHeight(object sender, GridRowColSizeEventArgs e)
        {
            if (this.messagelistgridGroupingControl.TableModel[e.Index, 2].CellType == "Static")
            {
                e.Size = 22;
                e.Handled = true;
            }
            if (isUnreadClicked)
            {
                if (!unreadMessage.Contains(e.Index))
                {
                    e.Size = 0;
                    e.Handled = true;
                }
            }
            if (emptyData)
            {
                e.Size = 0;
                e.Handled = true;
            }
            if (sentItems)
            {
                if (e.Index > 6)
                {
                    e.Size = 0;
                    e.Handled = true;
                }
            }

        }

        void gridGroupingControl1_TableControlCellMouseHover(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellMouseEventArgs e)
        {
            int rowIndex, colIndex;
            rowIndex = e.Inner.RowIndex;
            colIndex = e.Inner.ColIndex;
            moveRowIndex = rowIndex;
            moveColIndex = colIndex;
            int result = 0;
            GridStyleInfo style = this.messagelistgridGroupingControl.TableModel[rowIndex, colIndex];
            if (int.TryParse(style.ValueMember, out result))
            {
                if (!unreadMessage.Contains(rowIndex))
                    unreadMessage.Add(rowIndex);
            }
            if (style.CellType == "IndentCell")
            {
                this.messagelistgridGroupingControl.TableControl.Refresh();
            }

            if (this.messagelistgridGroupingControl.TableModel[e.Inner.RowIndex, e.Inner.ColIndex].CellType == "IndentCell"
                || this.messagelistgridGroupingControl.TableModel[e.Inner.RowIndex, e.Inner.ColIndex].CellType == "OutlookHeaderCell")
            {
                indentRI = e.Inner.RowIndex;
                indentCI = e.Inner.ColIndex;
                this.messagelistgridGroupingControl.TableModel[e.Inner.RowIndex, e.Inner.ColIndex].BackColor = ColorTranslator.FromHtml("#E6F2FA");
            }
            else
                if (e.Inner.ColIndex == 3)
            {
                e.TableControl.RefreshRange(GridRangeInfo.Cols(2, 2));
            }
        }

        void TableModel_QueryColWidth(object sender, Syncfusion.Windows.Forms.Grid.GridRowColSizeEventArgs e)
        {
            if (e.Index == 2)
            {
                if (screenBounds.Width > 1500)
                    dist = 40;
                e.Size = this.outlookSearchBox1.Width - dist;
                e.Handled = true;
            }
            if (e.Index == 3)
            {
                e.Size = 100;
                e.Handled = true;
            }
            if (e.Size > 400)
            {
                IsColWidthChanged = true;
            }
            else
            {
                IsColWidthChanged = false;
            }
        }
        void gridGroupingControl1_QueryCellStyleInfo(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.CellType == "TextBox")
                e.Style.CellType = "OutlookHeaderCell";
            if (e.Style.CellType == "OutlookHeaderCell")
                e.Style.Text = "";
            if (e.Style.CellType == "Header")
                e.Style.CellType = GridCellTypeName.Static;
            e.Style.Borders.All = GridBorder.Empty;
            if (e.Style.CellType == GridCellTypeName.Static)
            {
                Font newFont = new Font(e.Style.GdipFont.Name, e.Style.GdipFont.Size, FontStyle.Bold);
                e.Style.Font = new GridFontInfo(newFont);
                e.Style.VerticalAlignment = GridVerticalAlignment.Middle;
                e.Style.HorizontalAlignment = GridHorizontalAlignment.Left;
            }
            //selection
            if (e.TableCellIdentity.Column != null && e.TableCellIdentity.DisplayElement.IsRecord() && selectedRows != null && selectedRows.Count > 0)
            {
                int key = e.TableCellIdentity.DisplayElement.GetRecord().Id;
                if (selectedRows.Contains(key) && (bool)selectedRows[key])
                {
                    e.Style.BackColor = Color.FromArgb(230, 242, 250);// ColorTranslator.FromHtml("#CDE6F7");
                    e.Style.TextColor = Color.White;
                    hoveredRowIndex = e.TableCellIdentity.RowIndex;
                }
            }

            this.messagelistgridGroupingControl.TableControl.Selections.Clear();

            if (e.Style.TableCellIdentity.TableCellType == GridTableCellType.GroupCaptionCell)
            {
                GridCaptionRow capRow = e.Style.TableCellIdentity.DisplayElement as GridCaptionRow;
                Group g = capRow.ParentGroup;
                if (!g.IsTopLevelGroup)
                {
                    e.Style.CellValue = g.Category.ToString().Substring(2);
                }
            }
        }
        void gridGroupingControl1_TableControlCurrentCellStartEditing(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCancelEventArgs e)
        {
            e.Inner.Cancel = false;
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            if (e.Inner.ColIndex == 1)
            {
                this.messagelistgridGroupingControl.TableModel[e.Inner.RowIndex, e.Inner.ColIndex + 1].ValueMember = e.Inner.RowIndex.ToString();
            }
            //use the Nested display elements to know the selection is of record or caption
            int row = 0;
            row = e.Inner.RowIndex;
            this.MessageRichTextBox.Clear();
            this.messagepanel.Visible = true;
            if (this.messagelistgridGroupingControl.Table.NestedDisplayElements.Count > e.Inner.RowIndex)
            {
                // If the selection is of caption hide the reading pane
                if (this.messagelistgridGroupingControl.Table.NestedDisplayElements[row].IsCaption())
                {
                    this.messagepanel.Visible = false;
                    this.MessageRichTextBox.Clear();
                }
                if (this.messagelistgridGroupingControl.Table.NestedDisplayElements[row].IsRecord())
                {
                    Record rec = this.messagelistgridGroupingControl.Table.CurrentRecord;
                    this.DateTimeLabel.Text = DateTime.Now.ToString();
                    this.Tolabel.Text = rec["To"].ToString();
                    this.Maillabel.Text = rec["ContactName"].ToString();
                    string rtf2 =
                        @"{\rtf1\ansi" +
                        // font table
                        @"{\fonttbl" +
                        @"\f0\fswiss Segoe UI;}" +
                        @"\highlight\ql\f0\f0\fs20   " + "\\plain\\par" +
                        @"\highlight1\ql\cf0\f0\fs20 {    Hi }" + rec["To"] + "," + "\\plain\\par" + Environment.NewLine +
                        @"\highlight1\ql\f0\f2\fs20 " + "\\plain\\par" +
                        // third line
                        @"\highlight1\ql\cf0\f0\fs20     " + rec["Message"] + "." + "\\plain\\par" +
                        @"\highlight\ql\f0\f0\fs20   " + "\\plain\\par" +
                        @"\highlight1\ql\cf0\f0\fs20     " + "Thanks" + "," + "\\plain\\par" +
                        @"\highlight1\ql\cf0\f0\fs20     " + rec["ContactName"] + "." + "\\plain\\par" +
                        // closing bracket
                        @"}";

                    // Use display to show the content..
                    this.MessageRichTextBox.Rtf = rtf2;
                }
            }
        }
        #endregion

        #region Form load and click event
        void OutlookForm_Load(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                if (Screen.PrimaryScreen.Bounds.Size == new Size(1920, 1080))
                {
                    this.InnerSplitterContainer.SplitterDistance = 192;
                }
                else if (Screen.PrimaryScreen.Bounds.Size == new Size(1440, 900))
                {
                    this.InnerSplitterContainer.SplitterDistance = this.outlookSearchBox1.Controls[3].Location.X + this.outlookSearchBox1.Controls[3].Width;
                }
                if (g.DpiX >= 120)
                {
                    this.InnerSplitterContainer.SplitterDistance = this.outlookSearchBox1.Width / 2 + 45;
                }
            }

            this.WindowState = FormWindowState.Maximized;
        }

        void OutlookForm_Click(object sender, EventArgs e)
        {
            if (((Label)sender).Text == "Unread")
            {
                isUnreadClicked = true;
                this.messagelistgridGroupingControl.TableControl.Refresh();
            }
            if (((Label)sender).Text == "All")
            {
                isUnreadClicked = false;
                this.messagelistgridGroupingControl.TableControl.Refresh();
            }
        }
        #endregion

        #region ToolstripText Box(search text box actions)
        void toolStripTextBox1_LostFocus(object sender, EventArgs e)
        {
            this.searchtoolStripTextBox.Text = "Search";
        }

        void toolStripTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.searchtoolStripTextBox.Text = "";
        }
        #endregion

        #region "Selection BackColor Customization"

        int currentRowIndex = 0, currentColIndex = 0;
        Hashtable selectedRows = new Hashtable();
        Hashtable selectionIndents = new Hashtable();

        void gridGroupingControl1_TableControlCellMouseHoverLeave(object sender, GridTableControlCellMouseEventArgs e)
        {
            GridTableCellStyleInfo styleInfo = e.TableControl.GetTableViewStyleInfo(e.Inner.RowIndex, e.Inner.ColIndex);
            if (styleInfo.TableCellIdentity != null && styleInfo.TableCellIdentity.TableCellType.ToString() != "Static")
            {
                if (e.Inner.ColIndex > 1)
                {
                    MouseHoverCheck(e.Inner.RowIndex, e.Inner.ColIndex, false);
                    this.messagelistgridGroupingControl.TableControl.RefreshRange(GridRangeInfo.Row(e.Inner.RowIndex), GridRangeOptions.None);
                }
            }
        }

        void gridGroupingControl1_TableControlCellMouseHoverEnter(object sender, GridTableControlCellMouseEventArgs e)
        {
            GridTableCellStyleInfo styleInfo = e.TableControl.GetTableViewStyleInfo(e.Inner.RowIndex, e.Inner.ColIndex);
            if (styleInfo.TableCellIdentity.TableCellType.ToString() != "Static")
            {
                if (e.Inner.ColIndex > 1 && e.Inner.ColIndex <= 2)
                {
                    MouseHoverCheck(e.Inner.RowIndex, e.Inner.ColIndex, true);
                    currentRowIndex = e.Inner.RowIndex;
                    currentColIndex = e.Inner.ColIndex;
                    this.messagelistgridGroupingControl.TableControl.RefreshRange(GridRangeInfo.Row(e.Inner.RowIndex), GridRangeOptions.None);
                }
            }
            this.messagelistgridGroupingControl.TableControl.Refresh();
        }

        private void MouseHoverCheck(int row, int col, bool isHover)
        {
            if (col > 1)
            {
                GridTableCellStyleInfoIdentity id = this.messagelistgridGroupingControl.TableControl.GetTableViewStyleInfo(row, col).TableCellIdentity;
                if (id.DisplayElement.IsRecord())
                {
                    int key = id.DisplayElement.GetRecord().Id;
                    selectedRows.Clear();
                    selectedRows.Add(key, isHover);
                    selectionIndents.Clear();
                    selectionIndents.Add(col - 2, isHover);
                    this.messagelistgridGroupingControl.TableControl.RefreshRange(GridRangeInfo.Row(row));
                }
                if (this.messagelistgridGroupingControl.TableControl.Selections.Count > 0)
                    this.messagelistgridGroupingControl.TableControl.Selections.Clear();
            }
        }

        #endregion

        #region SplitContainerAdv
        void InnerSplitterContainer_SplitterMoved(object sender, Syncfusion.Windows.Forms.Tools.Events.SplitterMoveEventArgs e)
        {
            readerSplitterDistance = this.InnerSplitterContainer.SplitterIncrement;
        }

        void splitContainerAdv1_SplitterMoved(object sender, Syncfusion.Windows.Forms.Tools.Events.SplitterMoveEventArgs e)
        {
            outlookSearchBox1.Width += 10;
            mailSplitterDistance = this.mainsplitContainerAdv.SplitterDistance;
            if (this.mainsplitContainerAdv.SplitterDistance > this.HiddenPanel.Width)
            {
                this.HiddenPanel.Visible = false;
            }
        }
        #endregion

        #region Set message for the message panel
        private void setMessageTextBoxText()
        {
            this.DateTimeLabel.Text = DateTime.Now.ToString();
            this.Tolabel.Text = "Katrina";
            this.Maillabel.Text = "John Peter";
            string rtf2 =
                @"{\rtf1\ansi" +
                // font table
                @"{\fonttbl" +
                @"\f0\fswiss Segoe UI;}" +
                @"\highlight\ql\f0\f0\fs20   " + "\\plain\\par" +
                @"\highlight1\ql\cf0\f0\fs20 {    Hi }" + "Katrina" + "," + "\\plain\\par" + Environment.NewLine +
                @"\highlight1\ql\f0\f2\fs20 " + "\\plain\\par" +
                // third line
                @"\highlight1\ql\cf0\f0\fs20     " + "Your appointment has been schedulded on today @ 9.00Am" + "." + "\\plain\\par" +
                @"\highlight\ql\f0\f0\fs20   " + "\\plain\\par" +
                @"\highlight1\ql\cf0\f0\fs20     " + "Thanks" + "," + "\\plain\\par" +
                @"\highlight1\ql\cf0\f0\fs20     " + "John" + "." + "\\plain\\par" +
                // closing bracket
                @"}";

            // Use display to show the content..
            this.MessageRichTextBox.Rtf = rtf2;
        }
        #endregion

        #region XML data read
        void ReadXml(DataSet ds, string xmlFileName)
        {
            for (int n = 0; n < 10; n++)
            {
                if (System.IO.File.Exists(xmlFileName))
                {
                    ds.ReadXml(xmlFileName);
                    break;
                }
                xmlFileName = @"..\" + xmlFileName;
            }
        }
        #endregion

        #region TreeView actions
        void treeViewAdv1_MouseLeave(object sender, EventArgs e)
        {
            if ((sender as TreeViewAdv) == this.foldertreeViewAdv)
            {
                this.foldertreeViewAdv.Nodes[0].Background = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(0, 255, 255, 255));
                foreach (TreeNodeAdv item in this.foldertreeViewAdv.Nodes[0].Nodes)
                {
                    item.Background = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(0, 255, 255, 255));
                }
            }
        }
        void treeViewAdv1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as TreeViewAdv) == this.foldertreeViewAdv)
            {
                this.foldertreeViewAdv.Nodes[0].Background = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(0, 255, 255, 255));
                foreach (TreeNodeAdv item in this.foldertreeViewAdv.Nodes[0].Nodes)
                {
                    item.Background = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(0, 255, 255, 255));
                }
                if (this.foldertreeViewAdv.GetNodeAtPoint(new Point(e.X, e.Y)) != null)
                {
                    TreeNodeAdv node = this.foldertreeViewAdv.GetNodeAtPoint(new Point(e.X, e.Y));
                    node.Background = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(205, 230, 247));
                }
            }
        }

        void treeViewAdv1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((sender as TreeViewAdv) == this.foldertreeViewAdv)
            {
                if (this.foldertreeViewAdv.Nodes[0] != this.foldertreeViewAdv.SelectedNode)
                    this.foldertreeViewAdv.Nodes[0].Font = new Font(this.foldertreeViewAdv.Nodes[0].Font.FontFamily, this.foldertreeViewAdv.Nodes[0].Font.Size, FontStyle.Regular);
                foreach (TreeNodeAdv item in this.foldertreeViewAdv.Nodes[0].Nodes)
                {
                    if (item != this.foldertreeViewAdv.SelectedNode)
                    {
                        item.Font = new Font(item.Font.FontFamily, item.Font.Size, FontStyle.Regular);
                    }
                }
            }
        }

        private void treeViewAdv1_NodeMouseClick(object sender, TreeViewAdvMouseClickEventArgs e)
        {
            sentItems = false;
            if (e.Node.Text == "Sent Items")
            {
                emptyData = false;
                sentItems = true;
            }
            if (e.Node.Text == "Deleted Items" || e.Node.Text == "Junk E-Mail" || e.Node.Text == "Outbox" ||
                e.Node.Text == "Drafts")
            {
                emptyData = true;
            }
            else if (e.Node.Text == "Inbox")
            {
                emptyData = false;
            }
            this.messagelistgridGroupingControl.TableControl.Refresh();
            e.Node.Font = new Font(e.Node.Font.FontFamily, e.Node.Font.Size, FontStyle.Bold);
        }
        #endregion

        #region Exit backstage button
        private void backStageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Label mouse actions
        private void unreadlabel_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(58, 187, 246);
        }

        private void unreadlabel_MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(58, 187, 246);
        }

        private void unreadlabel_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.Black;
        }

        private void Alllabel_MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(58, 187, 246);
        }
        #endregion

        #region PointerControl click
        private void pointerControl2_Click(object sender, EventArgs e)
        {
            this.mainsplitContainerAdv.Panel1Collapsed = false;
            this.HiddenPanel.Visible = false;
            this.mainsplitContainerAdv.SplitterDistance = 226;
        }

        private void pointerControl1_Click_2(object sender, EventArgs e)
        {
            this.HiddenPanel.Visible = true;
            this.mainsplitContainerAdv.SplitterDistance = this.HiddenPanel.Width;
        }
        #endregion

        #region Panel mouse down
        private void HiddenPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.HiddenPanel.Visible = false;
            this.mainsplitContainerAdv.SplitterDistance = 226;
        }
        #endregion
    }
}
